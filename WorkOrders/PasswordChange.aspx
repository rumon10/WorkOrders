<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PasswordChange.aspx.cs" Inherits="WorkOrders.PasswordChange" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <fieldset class="UsrfieldCss">
        <legend>Change Password</legend>
        
        <div class="UsrInputdiv"><span class="UsrInputLabelSpan">Current Password : </span> <span>
          <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="Usrtxtbox" 
                MaxLength="20" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                    runat="server" ErrorMessage="Current Password Required !" 
                ControlToValidate="txtCurrentPassword" CssClass="UsrRequiredCss" Display="Dynamic" 
                SetFocusOnError="True" ValidationGroup="User"></asp:RequiredFieldValidator></span></div>
        
        <div class="UsrInputdiv"><span class="UsrInputLabelSpan">New Password : </span> <span>
          <asp:TextBox ID="txtNewPassword" runat="server" CssClass="Usrtxtbox" 
                MaxLength="20" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" ErrorMessage="New Password Required !" 
                ControlToValidate="txtNewPassword" CssClass="UsrRequiredCss" Display="Dynamic" 
                SetFocusOnError="True" ValidationGroup="User"></asp:RequiredFieldValidator></span></div>

        <div class="UsrInputdiv"><span class="UsrInputLabelSpan">Confirm Password : </span> <span>
            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="Usrtxtbox" 
                MaxLength="20" TextMode="Password"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator1"
                    runat="server" ErrorMessage="Confirm Password not match !" 
                ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword" 
                CssClass="UsrRequiredCss" Display="Dynamic" SetFocusOnError="True" 
                ValidationGroup="User"></asp:CompareValidator></span></div>

        <div class="UsrInputdiv"><span class="UsrInputLabelSpan"></span> <span>
            <asp:Button ID="btnchangePassword" runat="server" Text="Change Password" 
                CssClass="UserAddNewUserbtn" ValidationGroup="User" 
                onclick="btnAddNewUser_Click" OnClientClick="cursorBusy();"/></span></div>

        <div><asp:Label ID="lblMessage" runat="server" CssClass="UserLabelMessage"></asp:Label></div>
        </fieldset>
    </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function cursorDefault() { document.body.style.cursor = 'default'; }
        function cursorBusy() 
        {
            var curPass = document.getElementById("txtCurrentPassword").value;
            var usernewpass = document.getElementById("txtNewPassword").value;
            var userconfirmpass = document.getElementById("txtConfirmPassword").value;
            if (curPass.length != 0 && usernewpass.length != 0 && userconfirmpass.length != 0)
            { document.body.style.cursor = 'wait'; }
        }
    </script>
</asp:Content>
