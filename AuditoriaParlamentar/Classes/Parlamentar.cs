using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AuditoriaParlamentar.Classes
{
    public class Parlamentar
    {   
        
        public String recuperaIdParlamentar(string vs_Nome)
        {
            DataTable vdt_Nome = new DataTable();
            vdt_Nome = ConexaoStatica.recuperaDataTable(" SELECT ideCadastro from parlamentares where txNomeParlamentar like '%" + vs_Nome + "%' ", null);
            if (vdt_Nome.Rows.Count > 0)
            {
                return vdt_Nome.Rows[0]["ideCadastro"].ToString();
            }
            else{
                return "-1";
            }  
        }         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ao_ParlamentarId"></param>
        /// <returns></returns>
        public DataTable recuperaSecretariosParlamentar(string ao_ParlamentarId)
        {
            String vs_Query = " SELECT nome,auditoria.parlamentares.txNomeParlamentar as nomeParlamentar from secretario " +  
                              " INNER JOIN auditoria.parlamentares on auditoria.parlamentares.ideCadastro = secretario.deputado "+
                              " WHERE auditoria.parlamentares.ideCadastro =" + ao_ParlamentarId;            
            return ConexaoStatica.recuperaDataTable(vs_Query,null);        
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable recuperaParlamentares()
        {
            String vs_Query =" SELECT parlamentares.ideCadastro as id,parlamentares.txNomeParlamentar as nome, count(secretario.deputado) as qtd" +
                             " FROM secretario INNER JOIN parlamentares ON parlamentares.ideCadastro = secretario.deputado" +
                             " GROUP BY parlamentares.ideCadastro,secretario.deputado order by count(secretario.deputado) DESC";                                
            return ConexaoStatica.recuperaDataTable(vs_Query,null);        
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vs_TipoCard"></param>
        /// <returns></returns>
        public DataTable recuperaCards(string vs_TipoCard)
        {
            String vs_Query = "SELECT * FROM cards_bi WHERE tipoCartao = '" + vs_TipoCard + "'";
            return ConexaoStatica.recuperaDataTable(vs_Query, null);        
        } 
    }
}