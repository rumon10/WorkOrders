<%@ Page Title="User Login" Language="C#" MasterPageFile="~/WorkOrder.Master" AutoEventWireup="true" CodeBehind="WorkUserLogin.aspx.cs" Inherits="WorkOrders.WorkUserLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <!----- Start Main Login Div ---->
        <div class="LoginMainDivCss">
        <div class="LoginCss">
        <div class ="LoginControlDiv">
            <asp:TextBox ID="txtUsername" runat="server" CssClass="LoginTextBox" 
                ValidationGroup="LoginGroup" onload="btnLogin_Click" MaxLength="20">Username</asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="Username Required !" ControlToValidate="txtUsername" 
                CssClass="LoginValidator" ValidationGroup="LoginGroup"></asp:RequiredFieldValidator>
        </div>
        <div class ="LoginControlDiv">
            <asp:TextBox ID="txtPassword" runat="server" CssClass="LoginTextBox" 
                TextMode="Password" onprerender="txtPinNumber_PreRender" 
                ValidationGroup="LoginGroup" onload="btnLogin_Click" MaxLength="20">Password</asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="Password Required !" ControlToValidate="txtPassword" 
                CssClass="LoginValidator" ValidationGroup="LoginGroup"></asp:RequiredFieldValidator>
        </div>
        <div class ="LoginControlDiv">
            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="LoginButton" 
                onclick="btnLogin_Click" ValidationGroup="LoginGroup" OnClientClick="cursorBusy();" />
        </div>
        <div class ="LoginControlDiv">
            <asp:Label ID="lblLoginMessage" runat="server" Text="" CssClass="LoginValidator"></asp:Label>
        </div>
    </div>
</div>
<!----- End Main Login Div ---->
</ContentTemplate>
        <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnLogin" />
        </Triggers>
        </asp:UpdatePanel>
</div>
    <script type="text/javascript">
    function cursorDefault() { document.body.style.cursor = 'default'; }
    function cursorBusy()
     {
        var user = document.getElementById("txtUsername").value;
        var pass = document.getElementById("txtPassword").value;
        if (user.length != 0 && pass.length != 0)
         {
            document.body.style.cursor = 'wait';
        }
    }
    </script>
</asp:Content>
