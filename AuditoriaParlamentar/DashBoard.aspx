<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="AuditoriaParlamentar.DashBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <link href="assets/css/cards.css" rel="stylesheet" />      
    <script type="text/javascript">
        var vetCards = ["DEPUTADOR_ECONOMICO", "SENADOR_MENOR_DIV", "SENADOR_ECONOMICO",
                        "SENADOR_MAIOR_DIV", "SENADOR_GASTADOR", "DEPUTADOR_GASTADOR"];
        var cont = 1; 
        $(document).ready(function ()
        {
             carregaCartao("#card" + cont, "\{\"tipoCard\": \""+ vetCards.pop() +"\"}");            
        });        
        function carregaCartao(nomeCard,valorCard)
        {
            $(nomeCard).empty();
            $(nomeCard).prepend('<img id="theImg" style="height:32px;width:32px" src="http://sierrafire.cr.usgs.gov/images/loading.gif" />');
            $.ajax({
                type: "POST",
                url: "DashBoard.aspx/UserControlLoader",
                data: valorCard,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response){
                    $(nomeCard).html(response.d);
                    if (vetCards.length > 0)
                    {
                        cont++;
                        carregaCartao("#card" + cont, "\{\"tipoCard\": \"" + vetCards.pop() + "\"}");
                    }
                },
                failure: function (msg) {
                    $(nomeCard).html(msg);
                }
            });
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">	               
            <div class="panel panel-default">
                <div class="panel-heading">Quem mais gastou desde 2015</div>
                <div class="panel-body">                    
                    <div class="col-sm-3">
                       <div id="card1"></div>
                    </div>    
                    <div class="col-sm-3">
                       <div id="card2"></div>
                    </div>
                    <div class="col-sm-3">
                      <div id="card3"></div>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">Quem menos gastou desde 2015</div>
                <div class="panel-body">
                    <div class="col-sm-3">
                        <div id="card4"></div>
                     </div>    
                    <div class="col-sm-3">
                        <div id="card5"></div>
                    </div>
                    <div class="col-sm-3">
                        <div id="card6"></div>
                    </div>
                </div>
            </div>
	</div>	       
</asp:Content>
