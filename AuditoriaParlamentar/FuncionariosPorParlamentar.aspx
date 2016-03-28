<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FuncionariosPorParlamentar.aspx.cs" Inherits="AuditoriaParlamentar.FuncionariosPorParlamentar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container">	               
       <div class="panel panel-default">             
           <div class="panel-heading">Secretários Parlamentares por Lotação</div>           
           <asp:GridView ID="grdParlamentar" CssClass="table table-hover table-striped table-responsive"
                GridLines="None" runat="server" AutoGenerateColumns="False" 
                OnRowDataBound="grdParlamentar_RowDataBound" AllowSorting="True" OnSorting="grdParlamentar_Sorting">
               <Columns>                   
                   <asp:TemplateField>
                       <ItemTemplate>                                                      
                           <asp:Button ID="ButtonSite" runat="server" CssClass="btn btn-default btn-sm"  />
                           <asp:Button ID="ButtonFuncionarios" runat="server" CssClass="popover-link btn btn-primary btn-sm"  />
                       </ItemTemplate>
                   </asp:TemplateField>                   
                   <asp:BoundField DataField="nome" ReadOnly="True" HeaderText="Nome" SortExpression="nome" />
                   <asp:BoundField DataField="qtd" ReadOnly="True" HeaderText="Quantidade" SortExpression="qtd" />
                   <asp:BoundField DataField="id"  />                   
               </Columns>
           </asp:GridView>     
       </div>
       <asp:HiddenField ID="HidOrdenacao" runat="server" Value="ASC" />
   </div>
</asp:Content>
