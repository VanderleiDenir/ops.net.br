﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuditoriaParlamentar
{
    public partial class PesquisaAbas : System.Web.UI.Page
    {
        public String Pagina { get; set; }
        public String Titulo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            String opcao = HttpUtility.HtmlDecode(Request.QueryString["op"]);

            if (opcao == "D")
            {
                Pagina = "DenunciasGrid.aspx";
                Titulo = "Denúncias";
            }
            else if (opcao == "R")
            {
                Pagina = "RevisaoGrid.aspx";
                Titulo = "Revisão";
            }
            else if (opcao == "A")
            {
                Pagina = "RelParlamentarFornecedor.aspx";
                Titulo = "Parlamentar x Fornecedor";
            }
            else if (opcao == "C")
            {
                Pagina = "CidadesPendenciaGrid.aspx";
                Titulo = "Pendências de Fotos e Documentos";
            }
            else
            {
                Pagina = "PesquisaPrincipal.aspx";
                Titulo = "Auditar";
            }

            if (!IsPostBack)
            {
                DropDownListAgrupamento.Items.Add(new ListItem(Pesquisa.AGRUPAMENTO_PARLAMENTAR, Pesquisa.AGRUPAMENTO_PARLAMENTAR));
                DropDownListAgrupamento.Items.Add(new ListItem(Pesquisa.AGRUPAMENTO_DESPESA, Pesquisa.AGRUPAMENTO_DESPESA));
                DropDownListAgrupamento.Items.Add(new ListItem(Pesquisa.AGRUPAMENTO_FORNECEDOR, Pesquisa.AGRUPAMENTO_FORNECEDOR));
                DropDownListAgrupamento.Items.Add(new ListItem(Pesquisa.AGRUPAMENTO_PARTIDO, Pesquisa.AGRUPAMENTO_PARTIDO));
                DropDownListAgrupamento.Items.Add(new ListItem(Pesquisa.AGRUPAMENTO_UF, Pesquisa.AGRUPAMENTO_UF));
                DropDownListAgrupamento.Items.Add(new ListItem(Pesquisa.AGRUPAMENTO_DOCUMENTO, Pesquisa.AGRUPAMENTO_DOCUMENTO));

            }
        }
    }
}