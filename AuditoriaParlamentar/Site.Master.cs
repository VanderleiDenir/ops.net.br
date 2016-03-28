using System;
using System.Web.Security;
using System.Web.UI.HtmlControls;

namespace AuditoriaParlamentar
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        public bool autenticado;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    autenticado = true;

                    plcRevisor.Visible = Roles.IsUserInRole(System.Web.HttpContext.Current.User.Identity.Name, "REVISOR");
                }
                else
                {
                    autenticado = false;
                    plcRevisor.Visible = false;
                }                
                HyperLinkDeputadoFederal.HRef = "PesquisaInicio.aspx"; //?CARGO=" + Pesquisa.CARGO_DEPUTADO_FEDERAL;
                HyperLinkSenador.HRef = "AuditarSenador.aspx";// "PesquisaInicio.aspx?CARGO=" + Pesquisa.CARGO_SENADOR;
                HyperLinkFuncionarios.HRef = "FuncionariosPorParlamentar.aspx";
                HyperLinkDashBoard.HRef = "DashBoard.aspx"; 
            }
        }
    }
}
