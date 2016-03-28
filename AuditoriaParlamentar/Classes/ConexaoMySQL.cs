using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AuditoriaParlamentar.Classes
{
    public class ConexaoStatica
    {
        private static MySqlCommand vcmd_Comando;

        private static decimal _vd_UltimoRegistro;

        public static decimal Vd_UltimoRegistro
        {
            get { return ConexaoStatica._vd_UltimoRegistro; }
            set { ConexaoStatica._vd_UltimoRegistro = value; }
        }
        private static List<Exception> _lst_Exception;

        public static List<Exception> Lst_Exception
        {
            get { return ConexaoStatica._lst_Exception; }
            set { ConexaoStatica._lst_Exception = value; }
        }
        public static MySqlConnection CriarConexao(string vs_Conexao)
        {
            MySqlConnection conexao = null;
            try
            {
                conexao = new MySqlConnection(vs_Conexao);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return conexao;
        }
        public static MySqlConnection CriarConexao()
        {
            MySqlConnection conexao = null;
            conexao = new MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString);
            return conexao;
        }        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pacoteLista"></param>
        /// <param name="vc_Comando"></param>
        private static void adicionaParametrosLista(TransacaoSQL pacoteLista, ref MySqlCommand vc_Comando)
        {
            bool vb_ParametroExiste = false;
            try
            {
                if (pacoteLista.Vlst_Param.Count > 0)
                {
                    for (int i = 0; i < pacoteLista.Vlst_Param.Count; i++)
                    {
                        vb_ParametroExiste = false;
                        for (int j = 0; j < vc_Comando.Parameters.Count; j++)
                        {
                            if (vc_Comando.Parameters[j].ParameterName.Equals(pacoteLista.Vlst_Param[i].ParameterName)) { vb_ParametroExiste = true; j = vc_Comando.Parameters.Count; }
                        }
                        if (!vb_ParametroExiste)
                        {
                            if (pacoteLista.Vlst_Param[i].Value == null) vc_Comando.Parameters.Add(pacoteLista.Vlst_Param[i].ParameterName, pacoteLista.Vlst_Param[i].SqlDbType).Value = DBNull.Value;
                            else vc_Comando.Parameters.Add(pacoteLista.Vlst_Param[i].ParameterName, pacoteLista.Vlst_Param[i].SqlDbType).Value = pacoteLista.Vlst_Param[i].Value;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                string s = exc.Message;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vetParametros"></param>
        /// <returns></returns>
        private static List<SqlParameter> transformaVetorLista(SqlParameter[] vetParametros)
        {
            List<SqlParameter> vlst_Parametros = new List<SqlParameter>();
            for (int i = 0; i < vetParametros.Length; i++)
                vlst_Parametros.Add(vetParametros[i]);
            return vlst_Parametros;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listaPacotes"></param>
        /// <returns></returns>
        public static bool executaPacotesSQL(List<TransacaoSQL> listaPacotes, bool fecharConexao)
        {
            if (vcmd_Comando == null)
            {
                criaComandoConexao();
            }
            if (vcmd_Comando.Connection.State == ConnectionState.Closed)
                criaComandoConexao();

            bool vb_Retorno = false;
            try { analistaPacotes(listaPacotes, ref vb_Retorno, fecharConexao); vb_Retorno = true; }
            catch (Exception exc) { vcmd_Comando.Transaction.Rollback(); vb_Retorno = false; Lst_Exception.Add(exc); }
            finally { if (fecharConexao) fechaConexao(vcmd_Comando.Connection); }
            return vb_Retorno;
        }

        private static void criaComandoConexao()
        {
            vcmd_Comando = new MySqlCommand();
            vcmd_Comando.Connection = CriarConexao();
            vcmd_Comando.Connection.Open();
            vcmd_Comando.CommandTimeout = 10000;
            vcmd_Comando.CommandType = CommandType.Text;
            MySqlTransaction vo_Transaction = vcmd_Comando.Connection.BeginTransaction();
            vcmd_Comando.Transaction = vo_Transaction;
            _lst_Exception = new List<Exception>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listaPacotes"></param>
        /// <param name="vcmd_Comando"></param>
        /// <param name="vb_Retorno"></param>
        /// <param name="vb_fechar"></param>
        private static void analistaPacotes(List<TransacaoSQL> listaPacotes, ref bool vb_Retorno, bool vb_fechar)
        {
            for (int i = 0; i < listaPacotes.Count; i++)
            {
                // Limpa 
                vcmd_Comando.Parameters.Clear();
                vcmd_Comando.CommandText = listaPacotes[i].Vs_SQL;
                // Em alguns métodos ao invés de ser utilizada uma lista de parametros do tipo SQLParameters, 
                // foi utilizado um vetor. Para evitar retrabalho eu criei um método que transforma um vetor numa lista.
                if (listaPacotes[i].VetParam != null)
                    listaPacotes[i].Vlst_Param = transformaVetorLista(listaPacotes[i].VetParam);

                adicionaParametrosLista(listaPacotes[i], ref vcmd_Comando);
                try
                {
                    if (_vd_UltimoRegistro == 0)
                    {
                        using (MySqlDataReader reader = vcmd_Comando.ExecuteReader())
                            while (reader.Read())
                                _vd_UltimoRegistro = reader.GetDecimal(0);
                    }
                    else
                    {
                        vcmd_Comando.ExecuteNonQuery();
                    }
                }
                catch (Exception exc) { _lst_Exception.Add(exc); }
            }
            if (vb_fechar)
            {
                if (_lst_Exception.Count > 0)
                {
                    vcmd_Comando.Transaction.Rollback();
                    vb_Retorno = false;
                }
                else
                {
                    vcmd_Comando.Transaction.Commit();
                    vb_Retorno = true;
                }
            }
        }
        /// <summary>
        /// Executa comandos DML: INSERT, UPDATE, ou DELETE
        /// </summary>
        /// <param name="as_SQL">Comando SQL para executar</param>
        /// <param name="ao_Param">Parâmetros de condição da query</param>
        /// <param name="ao_Tipo">Query ou Stored Procedure</param>
        /// <returns>Sucesso</returns>
        public static bool executaDML(string as_SQL, SqlParameter[] ao_Param, CommandType ao_Tipo, string vs_Conexao)
        {
            // Varíavel do tipo Boolean que será retornada pela função.
            bool rb_Retorno = false;
            MySqlCommand CmdSQL = null;

            try
            {
                CmdSQL = new MySqlCommand(as_SQL);
                CmdSQL.Connection = CriarConexao();
                CmdSQL.Connection.Open();
                CmdSQL.CommandTimeout = 10000;
                CmdSQL.CommandType = ao_Tipo;
                CmdSQL.Parameters.Clear();
                if (ao_Param != null)
                    for (int i = 0; i < ao_Param.Length; i++)
                        if (ao_Param[i].Value == null)
                            CmdSQL.Parameters.Add(ao_Param[i].ParameterName, ao_Param[i].SqlDbType).Value = DBNull.Value;
                        else
                            CmdSQL.Parameters.Add(ao_Param[i].ParameterName, ao_Param[i].SqlDbType).Value = ao_Param[i].Value;

               MySqlTransaction vo_Transaction = CmdSQL.Connection.BeginTransaction();
                CmdSQL.Transaction = vo_Transaction;
                try
                {
                    CmdSQL.ExecuteNonQuery();
                    vo_Transaction.Commit();
                    rb_Retorno = true;
                }
                catch (Exception ex1)
                {
                    vo_Transaction.Rollback();
                    throw ex1;
                }
            }
            catch (Exception ex2)
            {
                throw ex2;
            }
            finally
            {
                fechaConexao(CmdSQL.Connection);
            }

            return rb_Retorno;
        }
        /// <summary>
        /// Executa comandos DML: INSERT, UPDATE, ou DELETE
        /// </summary>
        /// <param name="as_SQL">Comando SQL para executar</param>
        /// <param name="ao_Param">Parâmetros de condição da query</param>
        /// <param name="ao_Tipo">Query ou Stored Procedure</param>
        /// <returns>Sucesso</returns>
        public static bool executaDML(string as_SQL, SqlParameter[] ao_Param, CommandType ao_Tipo)
        {
            // Varíavel do tipo Boolean que será retornada pela função.
            bool rb_Retorno = false;
            MySqlCommand CmdSQL = null;

            try
            {
                CmdSQL = new MySqlCommand(as_SQL);
                CmdSQL.Connection = CriarConexao();
                CmdSQL.Connection.Open();
                CmdSQL.CommandTimeout = 10000;
                CmdSQL.CommandType = ao_Tipo;
                CmdSQL.Parameters.Clear();
                if (ao_Param != null)
                    for (int i = 0; i < ao_Param.Length; i++)
                        if (ao_Param[i].Value == null)
                            CmdSQL.Parameters.Add(ao_Param[i].ParameterName, ao_Param[i].SqlDbType).Value = DBNull.Value;
                        else
                            CmdSQL.Parameters.Add(ao_Param[i].ParameterName, ao_Param[i].SqlDbType).Value = ao_Param[i].Value;

               MySqlTransaction vo_Transaction = CmdSQL.Connection.BeginTransaction();
                CmdSQL.Transaction = vo_Transaction;
                try
                {
                    CmdSQL.ExecuteNonQuery();
                    vo_Transaction.Commit();
                    rb_Retorno = true;
                }
                catch (Exception ex1)
                {
                    vo_Transaction.Rollback();
                    throw ex1;
                }
            }
            catch (Exception ex2)
            {
                throw ex2;
            }
            finally
            {
                fechaConexao(CmdSQL.Connection);
            }

            return rb_Retorno;
        }
        // <summary>
        /// Fechar e limpar uma conexão SQL-SERVER
        /// </summary>
        /// <param name="ao_ConnDB">Objeto da conexão que será fechado</param>
        public static void fechaConexao(MySqlConnection ao_ConnDB)
        {
            if (ao_ConnDB != null)
            {
                if (ao_ConnDB.State != ConnectionState.Closed)
                    ao_ConnDB.Close();

                ao_ConnDB = null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="parametros"></param>
        /// <param name="tipoComando"></param>
        /// <param name="vs_Conexao"></param>
        /// <returns></returns>
        public static DataTable recuperaDataTable(string SQL, SqlParameter[] parametros, CommandType tipoComando, string vs_Conexao)
        {
            using (MySqlConnection con = CriarConexao())
            {
                con.Open();
                using (MySqlTransaction trans = con.BeginTransaction())
                {
                    using (MySqlCommand command = new MySqlCommand(SQL, con, trans))
                    {
                        if (parametros != null)
                        {
                            // Adiciona os parâmetros.
                            foreach (SqlParameter param in parametros)
                                command.Parameters.Add(param);

                            command.CommandType = tipoComando;
                        }
                        using (MySqlDataAdapter da = new MySqlDataAdapter(command))
                        {
                            DataTable vdt_Retorno = new DataTable();
                            try
                            {
                                da.Fill(vdt_Retorno);
                                trans.Commit();
                            }
                            catch (Exception)
                            {
                                trans.Rollback();
                            }
                            return vdt_Retorno;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="parametros"></param>
        /// <param name="tipoComando"></param>
        /// <returns></returns>
        public static DataTable recuperaDataTable(string SQL, SqlParameter[] parametros, CommandType tipoComando)
        {
            using (MySqlConnection con = CriarConexao())
            {
                con.Open();                                    
                using (MySqlCommand command = new MySqlCommand(SQL, con))                    
                {
                        if (parametros != null)
                        {
                            // Adiciona os parâmetros.
                            foreach (SqlParameter param in parametros)
                            {
                                if (param != null)
                                    command.Parameters.Add(param);
                            }
                            command.CommandType = tipoComando;
                        }
                        using (MySqlDataAdapter da = new MySqlDataAdapter(command))
                        {
                            DataTable vdt_Retorno = new DataTable();
                            try
                            {
                                da.Fill(vdt_Retorno);                                
                            }
                            catch (Exception exc1)
                            {
                                throw exc1; 
                            }
                            return vdt_Retorno;
                        }                    
                }                
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public static DataTable recuperaDataTable(string SQL, MySqlParameter[] parametros)
        {
            using (MySqlConnection con = CriarConexao())
            {
                con.Open();
                
                    using (MySqlCommand command = new MySqlCommand(SQL, con))
                    {
                        if (parametros != null)
                        {
                            // Adiciona os parâmetros.
                            foreach (MySqlParameter param in parametros)
                            {
                                command.Parameters.Add(param);
                            }
                        }
                        using (MySqlDataAdapter da = new MySqlDataAdapter(command))
                        {
                            DataTable vdt_Retorno = new DataTable();
                            try
                            {
                                da.Fill(vdt_Retorno);                                
                            }
                            catch (Exception exc)
                            {                                
                                vdt_Retorno = null;
                                throw exc;
                            }
                            return vdt_Retorno;
                        }
                    }
                
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="lstParametros"></param>
        /// <param name="tipoComando"></param>
        /// <returns></returns>
        public static DataTable recuperaDataTableList(string SQL, System.Collections.Generic.List<SqlParameter> lstParametros, CommandType tipoComando)
        {
            using (MySqlConnection con = CriarConexao())
            {
                con.Open();                
                    using (MySqlCommand command = new MySqlCommand(SQL, con))
                    {
                        if (lstParametros != null)
                        {
                            if (lstParametros.Count > 0)
                            {
                                foreach (SqlParameter param in lstParametros)
                                {
                                    command.Parameters.Add(param);
                                }
                            }
                        }
                        command.CommandType = tipoComando;

                        using (MySqlDataAdapter da = new MySqlDataAdapter(command))
                        {
                            DataTable vdt_Retorno = new DataTable();
                            try
                            {
                                da.Fill(vdt_Retorno);                                
                            }
                            catch (Exception exc)
                            {                                
                                vdt_Retorno = null;
                                throw exc;
                            }
                            return vdt_Retorno;
                        }
                    }                
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="lstParametros"></param>
        /// <param name="tipoComando"></param>
        /// <returns></returns>
        public static DataSet recuperaDataSetList(string SQL, System.Collections.Generic.List<SqlParameter> lstParametros, CommandType tipoComando)
        {
            using (MySqlConnection con = CriarConexao())
            {
                con.Open();
                
                    using (MySqlCommand command = new MySqlCommand(SQL, con))
                    {
                        if (lstParametros != null)
                        {
                            foreach (SqlParameter param in lstParametros)
                            {
                                command.Parameters.Add(param);
                            }                           
                        }
                        command.CommandType = tipoComando;

                        using (MySqlDataAdapter da = new MySqlDataAdapter(command))
                        {
                            DataSet vds_Retorno = new DataSet();
                            try
                            {
                                da.Fill(vds_Retorno);                                
                            }
                            catch (Exception exc)
                            {                                
                                vds_Retorno = null;
                                throw exc;
                            }
                            return vds_Retorno;
                        }
                    }                
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public class TransacaoSQL
        {
            private string _vs_SQL; public string Vs_SQL { get { return _vs_SQL; } set { _vs_SQL = value; } }
            private List<SqlParameter> _vlst_Param; public List<SqlParameter> Vlst_Param { get { return _vlst_Param; } set { _vlst_Param = value; } }
            private SqlParameter[] _vetParam; public SqlParameter[] VetParam { get { return _vetParam; } set { _vetParam = value; } }
        }
    }

}