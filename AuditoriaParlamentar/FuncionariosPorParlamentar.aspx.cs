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
    public partial class FuncionariosPorParlamentar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable vdt_Parlamentares = new DataTable();
            Parlamentar vo_Parlamentar = new Parlamentar();
            vdt_Parlamentares = vo_Parlamentar.recuperaParlamentares();
            grdParlamentar.DataSource = vdt_Parlamentares;
            grdParlamentar.DataBind();               
        }

        protected void grdParlamentar_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string s = "";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button buttonAuditar = (Button)e.Row.FindControl("ButtonSite");
                buttonAuditar.Text = "Site";
                buttonAuditar.OnClientClick = "window.open('http://www.camara.leg.br/internet/Deputado/dep_Detalhe.asp?id=" + e.Row.Cells[3].Text + "');return false;";

                Button buttonFunc = (Button)e.Row.FindControl("ButtonFuncionarios");
                buttonFunc.Text = "Ver Secretários";
                buttonFunc.OnClientClick = "window.location ='DetalhesSecretarios.aspx?ideCadastro=" + e.Row.Cells[3].Text + "';return false;";

                e.Row.Cells[3].Visible = false;
            }
        }
        protected void grdParlamentar_Sorting(object sender, GridViewSortEventArgs e)
        {
            if ((grdParlamentar.DataSource) != null)
            {
                ((DataTable)(grdParlamentar.DataSource)).DefaultView.Sort = e.SortExpression + " " + gerenciaOrdenacao();
                grdParlamentar.DataBind();
            }
        }
        protected string gerenciaOrdenacao()
        {
            string vs_Hidden = HidOrdenacao.Value;
            if (vs_Hidden == "ASC")
            {
                HidOrdenacao.Value = "DESC";
                return "ASC";
            }
            else
            {
                HidOrdenacao.Value = "ASC";
                return "DESC";
            }
        } 
    }
}