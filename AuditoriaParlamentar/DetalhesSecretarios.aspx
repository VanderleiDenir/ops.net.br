<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalhesSecretarios.aspx.cs" Inherits="AuditoriaParlamentar.DetalhesSecretarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="container">	               
       <div class="panel panel-default">  
           <div class="panel-heading">
               <p> 
                  <asp:Label ID="lblParlamentar" runat="server"></asp:Label>
                  <button class="btn btn-primary btn-lg pull-right btn-sm" onclick="window.history.back();" type="button">Retornar a página anterior</button>
               </p>
           </div>                      
           <asp:GridView ID="grdSecretarios" CssClass="table table-hover table-striped table-responsive" runat="server" AutoGenerateColumns="False">
               <Columns>
                   <asp:BoundField DataField="nome" ReadOnly="True" HeaderText="Secretários" />
               </Columns>
           </asp:GridView>
        </div>
    </div>
</asp:Content>
