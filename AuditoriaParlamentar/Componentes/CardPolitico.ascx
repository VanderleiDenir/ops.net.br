<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CardPolitico.ascx.cs" Inherits="AuditoriaParlamentar.Componentes.CardPolitico" %>
<div class="card">                        
    <canvas class="header-bgDesempenhoBom" width="250" height="70" id="header_blur"></canvas>                   
    <div class="avatar">        
        <asp:Image ID="imgGast" ImageUrl="https://web.mention.net/v/mqt2zr/bundles/mentionweb/images/user-default-avatar.jpg" runat="server" AlternateText="" />
    </div>    
    <div class="content">        
        <p><asp:Label ID="lblNome" runat="server"></asp:Label> </p>                        
        <p><asp:Label ID="lblCasaParlamentar" runat="server"></asp:Label><br />
        <p><asp:Label ID="Label2" runat="server" Text="Gastos:"></asp:Label>
           <asp:Label ID="lblTipoCard" runat="server"></asp:Label></p>
        <p><asp:Label ID="Label1" runat="server" Text="Valor:(R$)"></asp:Label>
           <asp:Label ID="lblGasto" runat="server"></asp:Label></p>        
    </div>
</div>