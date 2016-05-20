﻿<%@ Page Title="OPS - Operação Política Supervisionada" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AuditoriaParlamentar.Default" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="<%= ResolveClientUrl("~/") %>assets/css/landing-page.css?v=<%= AuditoriaParlamentar.Classes.Configuracao.VersaoSite %>" rel="stylesheet" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="bg">
        <div class="intro-header">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="intro-message">
                            <h1>Operação Política Supervisionada</h1>
                            <h3>Além do projeto, uma solução real</h3>
                            <hr class="intro-divider" />
                            <ul class="list-inline">
                                <li>
                                    <a href="Sobre.aspx" class="btn btn-primary btn-lg"><i class="glyphicon glyphicon-heart"></i>&nbsp;Conheça</a>
                                </li>
                                <li>
                                    <a id="btnAuditar" runat="server" href="PesquisaInicio.aspx" class="btn btn-success btn-lg"><i class="glyphicon glyphicon-eye-open"></i>&nbsp;Fiscalize</a>
                                </li>
                                <li>
                                    <a href="http://luciobig.com.br/pagina-para-doacoes_16-html/" class="btn btn-default btn-lg" target="_blank"><i class="glyphicon glyphicon-usd"></i>&nbsp;Doe</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        

        <div class="container banner1">
            <div class="row">
                <div class="col-lg-12 text-center text-success">
                    <h2>Mais de <b>R$5,5 MILHÕES</b> de Economia aos Cofres Públicos</h2>
                </div>
            </div>
            <div class="row text-center">
                <div class="col-lg-12">
                    <asp:Repeater runat="server" ID="rptResumoAuditoria">
                        <ItemTemplate>
                            <div class="col-xs-12 col-sm-4 text-center">
                                <div class="bs-callout bs-callout-info">
                                    <h3><%# Eval("Resultado") %></h3>
                                    <p><%# Eval("Nome") %></p>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <br />
        </div>
        <div class="content-section-a">
            <div class="container">
                <div class="row">
                    <div class="col-xs-12 col-sm-4 media-middle">
                        <div class="form-group text-center">
                            <img src="assets/img/ops_fiscalize.png" alt="OPS Fiscalize" />
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-8">
                        <h4>OPS Fiscalize</h4>
                        <br />
                        <p class="text-justify">Já está disponível o OPS Fiscalize, aplicativo que permite a qualquer pessoa fiscalizar os gastos realizados por deputados federais com a verba indenizatória, dinheiro público destinado à reposição de despesas feitas pelos parlamentares no exercício do mandato.</p>
                        <p class="text-justify">Disponível para Android e IOS, o OPS Fiscalize reúne, atualizadamente, os custos gerados por deputados da 55ª legislatura.</p>
                        <br />
                        <div class="ops-fiscalize-button">
                            <div class="form-group">
                                <a href="https://play.google.com/store/apps/details?id=br.net.ops.fiscalize" class="btn btn-primary" target="_blank" rel="nofollow">Android&nbsp;<i class="glyphicon glyphicon-menu-right"></i></a>
                            </div>
                            <div class="form-group">
                                <a href="https://itunes.apple.com/br/app/ops-fiscalize/id1016302793?l=en&mt=8" class="btn btn-primary" target="_blank" rel="nofollow">IOS&nbsp;<i class="glyphicon glyphicon-menu-right"></i></a>
                            </div>
                            <div class="form-group">
                                <a href="https://www.microsoft.com/pt-br/store/apps/ops-fiscalize/9nblggh686vc" class="btn btn-primary" target="_blank" rel="nofollow">Windows Phone&nbsp;<i class="glyphicon glyphicon-menu-right"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="banner">
            <div class="container text-center">
                <div class="fb-comments" data-href="http://ops.net.br" data-numposts="5" data-width="100%"></div>
            </div>
        </div>
        <footer>
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="text-center" style="margin-top: 15px;">
                            <div class="fb-like" data-href="http://ops.net.br" data-layout="button_count" data-action="like" data-show-faces="true" data-share="true"></div>
                        </div>
                        <p style="margin: 10px 0 0;" class="text-muted text-center">O controle social é indispensável para combatermos a corrupção em nosso país.</p>
                    </div>
                </div>
            </div>
        </footer>
    </div>

    <div id="fb-root"></div>
    <script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.5&appId=1033624573364106";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));</script>
</asp:Content>
