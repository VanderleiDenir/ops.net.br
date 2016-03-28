using AuditoriaParlamentar.Classes;
using AuditoriaParlamentar.Componentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AuditoriaParlamentar
{
    public partial class DashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string UserControlLoader(string tipoCard)
        {
            Page page = new Page();
            Parlamentar vo_Parl = new Parlamentar();
            DataTable vdt_Card = vo_Parl.recuperaCards(tipoCard);
            LoadUserControlHelper componenteCard = (LoadUserControlHelper)page.LoadControl("/Componentes/CardPolitico.ascx");
            (componenteCard.FindControl("lblNome") as Label).Text = vdt_Card.Rows[0]["nomeParlamentar"].ToString();
            (componenteCard.FindControl("lblGasto") as Label).Text = vdt_Card.Rows[0]["valorCard"].ToString();
            (componenteCard.FindControl("lblTipoCard") as Label).Text = vdt_Card.Rows[0]["descricaoCartao"].ToString();

            int casaDoParlamentar = Convert.ToInt32(vdt_Card.Rows[0]["casaParlamentar"]);
            string vs_UrlImagem = string.Empty;
            if (casaDoParlamentar == 1)
            {
                vs_UrlImagem = string.Format("http://www.senado.leg.br/senadores/img/fotos-oficiais/senador{0}.jpg", Convert.ToInt32(vdt_Card.Rows[0]["idParlamentar"]));
                (componenteCard.FindControl("lblCasaParlamentar") as Label).Text = " Senador(a)";
            }
            else
            {
                vs_UrlImagem = string.Format("http://www.camara.gov.br/internet/deputado/bandep/{0}.jpg", Convert.ToInt32(vdt_Card.Rows[0]["idParlamentar"]));
                (componenteCard.FindControl("lblCasaParlamentar") as Label).Text = " Deputado(a)";
            }
            string vs_cssCard = string.Empty;
            switch (tipoCard)
            {
                case "SENADOR_MAIOR_DIV": vs_cssCard = "header-bgDesempenhoRuim"; break;
                case "SENADOR_GASTADOR": vs_cssCard = "header-bgDesempenhoRuim"; break;
                case "DEPUTADOR_GASTADOR": vs_cssCard = "header-bgDesempenhoRuim"; break;
                case "DEPUTADOR_ECONOMICO": vs_cssCard = "header-bgDesempenhoBom"; break;
                case "SENADOR_MENOR_DIV": vs_cssCard = "header-bgDesempenhoBom"; break;
                case "SENADOR_ECONOMICO": vs_cssCard = "header-bgDesempenhoBom"; break;
            }
            (componenteCard.FindControl("imgGast") as Image).ImageUrl = vs_UrlImagem;
            componenteCard.EnableViewState = false;
            HtmlForm form = new HtmlForm();
            form.Controls.Add(componenteCard);
            page.Controls.Add(form);
            StringWriter textWriter = new StringWriter();
            HttpContext.Current.Server.Execute(page, textWriter, false);
            return textWriter.ToString();
        }
    }
}