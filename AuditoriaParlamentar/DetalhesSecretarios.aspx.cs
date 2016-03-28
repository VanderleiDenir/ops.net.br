using AuditoriaParlamentar.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuditoriaParlamentar
{
    public partial class DetalhesSecretarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable vdt_Teste = new Parlamentar().recuperaSecretariosParlamentar(Request.QueryString["ideCadastro"]);
            if (vdt_Teste.Rows.Count > 0)
            {
                lblParlamentar.Text = vdt_Teste.Rows[0]["nomeParlamentar"].ToString();
                grdSecretarios.DataSource = vdt_Teste;
                grdSecretarios.DataBind();
            }
            else
            {
                lblParlamentar.Text = "Deputado não encontrado no Banco de dados";
            } 
        }
    }
}