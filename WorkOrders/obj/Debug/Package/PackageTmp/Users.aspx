<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="WorkOrders.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <fieldset class="UsrfieldCss">
        <legend> Add New User Information</legend>
        <div class ="UsrInputdiv"><span class="UsrInputLabelSpan">Username : </span> <span>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="Usrtxtbox" 
                MaxLength="20"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" ErrorMessage="Username Required !" 
                ControlToValidate="txtUsername" CssClass="UsrRequiredCss" Display="Dynamic" 
                SetFocusOnError="True" ValidationGroup="User"></asp:RequiredFieldValidator></span></div>
        <div class="UsrInputdiv"><span class="UsrInputLabelSpan">Full Name : </span> <span>
            <asp:TextBox ID="txtFullName" runat="server" CssClass="Usrtxtbox" 
                MaxLength="100"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                    runat="server" ErrorMessage="Full Name Required !" 
                ControlToValidate="txtFullName" CssClass="UsrRequiredCss" Display="Dynamic" 
                SetFocusOnError="True" ValidationGroup="User"></asp:RequiredFieldValidator></span></div>
        <div class="UsrInputdiv"><span class="UsrInputLabelSpan">Email : </span> <span>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="Usrtxtbox" MaxLength="100"></asp:TextBox><asp:RequiredFieldValidator
                ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="Email Required !" ControlToValidate="txtEmail" 
                CssClass="UsrRequiredCss" Display="Dynamic" SetFocusOnError="True" 
                ValidationGroup="User"></asp:RequiredFieldValidator></span></div>
        <div class="UsrInputdiv"><span class="UsrInputLabelSpan">Password : </span> <span>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="Usrtxtbox" 
                MaxLength="20" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                    runat="server" ErrorMessage="Password Required !" 
                ControlToValidate="txtPassword" CssClass="UsrRequiredCss" Display="Dynamic" 
                SetFocusOnError="True" ValidationGroup="User"></asp:RequiredFieldValidator></span></div>
        <div class="UsrInputdiv"><span class="UsrInputLabelSpan">Confirm Password : </span> <span>
            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="Usrtxtbox" 
                MaxLength="20" TextMode="Password"></asp:TextBox><asp:CompareValidator ID="CompareValidator1"
                    runat="server" ErrorMessage="Confirm Password not match !" 
                ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" 
                CssClass="UsrRequiredCss" Display="Dynamic" SetFocusOnError="True" 
                ValidationGroup="User"></asp:CompareValidator></span></div>
        <div class="UsrInputdiv"><span class="UsrInputLabelSpan">User Type : </span>
         <span> <asp:DropDownList ID="ddlUserType" runat="server" CssClass="UsrdropdownBox" 
                Height="24px">
                <asp:ListItem Selected="True" Value="User">User</asp:ListItem>
                <asp:ListItem Value="Admin">Admin</asp:ListItem>
             </asp:DropDownList>
         </span></div>
        <div class="UsrInputdiv"><span class="UsrInputLabelSpan">Status : </span> <span>
            <asp:CheckBox ID="chbUserStatus" runat="server" Checked="True" 
                CssClass="UsrCheckbox" Text="Active" ValidationGroup="User" /></span></div>
        <div class="UsrInputdiv"><span class="UsrInputLabelSpan"></span> <span>
            <asp:Button ID="btnAddNewUser" runat="server" Text="Add New User" 
                CssClass="UserAddNewUserbtn" ValidationGroup="User" 
                onclick="btnAddNewUser_Click" OnClientClick="cursorBusy();" /></span></div>
        </fieldset>
        <div>
            <asp:Label ID="lblMessage" runat="server" CssClass="UserLabelMessage"></asp:Label>
        </div>
        <div class="UserGridCaption">User Information</div>
        <div class="UsrGriddiv">
            <asp:GridView ID="gvUser" runat="server" AllowPaging="True" AllowSorting="True" 
                AutoGenerateColumns="False" BorderStyle="Solid" BorderWidth="1px" 
                Font-Bold="False" GridLines="Horizontal" 
                ShowFooter="True" onrowcancelingedit="gvUser_RowCancelingEdit" 
                onrowediting="gvUser_RowEditing" onrowupdating="gvUser_RowUpdating">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Username">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="UserGridLabel" 
                                MaxLength="20" ReadOnly="True" Text='<%# Eval("UserName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>' 
                                CssClass="UserGridLabel"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Full Name">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFullName" runat="server" Text='<%# Eval("FullName") %>' 
                                CssClass="UserGridLabel" MaxLength="100"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblFullName" runat="server" Text='<%# Eval("FullName") %>' 
                                CssClass="UserGridLabel"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="30%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEmail" runat="server" Text='<%# Eval("Email") %>' 
                                CssClass="UserGridLabel" MaxLength="100"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>' 
                                CssClass="UserGridLabel"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="30%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlUserType" runat="server" CssClass="UserGridLabel" 
                                SelectedValue='<%# Eval("UserType") %>'>
                                <asp:ListItem Selected="True" Value="User">User</asp:ListItem>
                                <asp:ListItem Value="Admin">Admin</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblUserType" runat="server" CssClass="UserGridLabel" 
                                Text='<%# Eval("UserType") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="7%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <EditItemTemplate>
                            <asp:CheckBox ID="chbIsEnabled" runat="server" 
                                Checked='<%# Eval("IsEnabled") %>' CssClass="UserGridLabel" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblIsEnable" runat="server" CssClass="UserGridLabel" 
                                Text='<%# Eval("IsEnabled") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="3%" />
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="Operation" ShowEditButton="True" 
                        ShowHeader="True">
                    <ItemStyle Width="14%" />
                    </asp:CommandField>
                    <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" 
                            OnClientClick="return confirm('Do you want to delete the selected user?')" 
                            Text="Delete" OnClick="DeleteUser" 
                            CommandArgument='<%# Eval("UserName") %>'></asp:LinkButton>
                    </ItemTemplate>
                        <ItemStyle Width="6%" HorizontalAlign="Center" 
                            VerticalAlign="Middle" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#0080C0" BorderColor="#0080C0" BorderStyle="Solid" 
                    BorderWidth="1px" Font-Bold="True" ForeColor="White" Height="22px" 
                    HorizontalAlign="Left" />
                <RowStyle Height="22px" />
            </asp:GridView>
        </div>
    </ContentTemplate>
    
    </asp:UpdatePanel>
    <script type="text/javascript">
        function cursorDefault() {document.body.style.cursor = 'default';}
        function cursorBusy()
         {
            var user = document.getElementById("txtUsername").value;
            var fullname = document.getElementById("txtFullName").value;
            var email = document.getElementById("txtEmail").value;
            var pass = document.getElementById("txtPassword").value;
            var confirm = document.getElementById("txtConfirmPassword").value;
            if ((user.length != 0 && fullname.length != 0 && email.length != 0 && pass.length != 0) && (pass == confirm))
            { document.body.style.cursor = 'wait'; }
        }
    </script>
</asp:Content>
