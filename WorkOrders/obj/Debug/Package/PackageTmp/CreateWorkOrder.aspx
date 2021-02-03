<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateWorkOrder.aspx.cs" Inherits="WorkOrders.CreateWorkOrder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="workOrderHeader">
    Work Order Information
    </div>
    <div class="workOrderMainDiv">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="inputDivCss">
            <span class="LabelCss">Customer : </span>
            <span class="inputspan">
                <asp:DropDownList ID="ddlCustomerList" runat="server" CssClass="dropdownCss" DataTextField="CustomerName" DataValueField="CustomerId">
                </asp:DropDownList>
            </span>
            </div>
            <div class="inputDivCss">
            <span class="LabelCss">Date : </span>
            <span class="inputspan">
                <asp:CalendarExtender runat="server" ID="approveRefromFromDate" Format="MM/dd/yyyy" TargetControlID="txtDate" OnClientShown="OnClientShown"></asp:CalendarExtender>
                <asp:TextBox ID="txtDate" runat="server" CssClass="txtboxCss"></asp:TextBox>
            </span>
            </div>
            <div class="inputDivCss">
            <span class="LabelCss">Work Order ID : </span>
            <span class="inputspan">
                <asp:TextBox ID="txtWorkOrder" runat="server" CssClass="txtboxCss" 
                    ReadOnly="True"></asp:TextBox>
            </span>
            <span class="imagebtnSpan"> 
                <asp:ImageButton ID="imgbtnSearch" runat="server" CssClass="imagebtnCss" 
                    ImageAlign="Middle" ImageUrl="~/Image/edit.png" CommandArgument="e" 
                    onclick="imgbtnSearch_Click" OnClientClick="cursorBusy();" /></span>
            </div>
            <div class="inputDivCss">
            <span class="LabelCss">Tech : </span>
            <span class="inputspan">
                <asp:DropDownList ID="ddlTechList" runat="server" CssClass="dropdownCss" DataTextField="TechName" DataValueField="TechId">
                </asp:DropDownList>
            </span>
            </div>
            <div class="inputDivCss">
            <span class="LabelCss">Total Charge : </span>
            <span class="inputspan">
                <asp:TextBox ID="txtTotalCharge" runat="server" CssClass="txtboxCss" onkeypress="return CheckDecimalValues(event)">0.00</asp:TextBox>
            </span>
            </div>
            <div class="gridCaptionHeadr">
                Work Type Details
            </div>
            <div class="gridDivCss">
                <asp:GridView ID="gvWorkType" runat="server" 
                    AutoGenerateColumns="False" BorderColor="#004080" BorderStyle="Solid" 
                    BorderWidth="1px" 
                    CellPadding="2" ShowFooter="True" ShowHeaderWhenEmpty="True" 
                    onrowcancelingedit="gvWorkType_RowCancelingEdit" 
                    onrowediting="gvWorkType_RowEditing" 
                    onrowupdating="gvWorkType_RowUpdating" GridLines="Horizontal" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Work Type">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlWorkType" runat="server" 
                                    SelectedValue='<%# Eval("WorkType") %>' Width="100%" Height="100%" 
                                    CssClass="workTypedll">
                                    <asp:ListItem>Server</asp:ListItem>
                                    <asp:ListItem>Desktop</asp:ListItem>
                                    <asp:ListItem>Software</asp:ListItem>
                                    <asp:ListItem>Network</asp:ListItem>
                                    <asp:ListItem>Travel</asp:ListItem>
                                    <asp:ListItem>Admin</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="dllInputWorkType" runat="server" CssClass="workTypedll" 
                                    Width="100%" Height="100%">
                                    <asp:ListItem Selected="True">Desktop</asp:ListItem>
                                    <asp:ListItem>Network</asp:ListItem>
                                    <asp:ListItem>Server</asp:ListItem>
                                    <asp:ListItem>Admin</asp:ListItem>
                                    <asp:ListItem>Software</asp:ListItem>
                                    <asp:ListItem>Travel</asp:ListItem>
                                </asp:DropDownList>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblWorkType" runat="server" Text='<%# Eval("WorkType") %>' 
                                    Height="100%" Width="100%"></asp:Label>
                            </ItemTemplate>
                            <FooterStyle Height="27px" Width="12%" />
                            <ItemStyle Width="12%" Height="27px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDescription" runat="server" 
                                    Text='<%# Eval("Description") %>' Width="99%" Height="99%"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtInputDescription" runat="server" BorderColor="Black" 
                                    BorderStyle="Solid" BorderWidth="1px" Width="99%" Height="99%"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' 
                                    Height="100%" Width="100%"></asp:Label>
                            </ItemTemplate>
                            <FooterStyle Height="27px" Width="40%" />
                            <ItemStyle Width="40%" Height="27px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Used Time hh.mm">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtUsedTime" runat="server" Text='<%# Eval("UsedTime") %>' 
                                    Width="99%" Height="99%" onkeypress="return CheckDecimalValues(event)"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                            <asp:TextBox ID="txtInputUsedTime" runat="server" BorderColor="Black" 
                                    BorderStyle="Solid" BorderWidth="1px" Width="99%" Height="99%"  
                                    CssClass="UsedTimeTextBoxCs" onkeypress="return CheckDecimalValues(event)"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblUsedTime" runat="server" Text='<%# Eval("UsedTime") %>' 
                                    Height="100%" Width="100%"></asp:Label>
                            </ItemTemplate>
                            <FooterStyle Height="27px" Width="14%" />
                            <ItemStyle Width="14%" Height="27px" />
                        </asp:TemplateField>
                       <asp:TemplateField>
                        <FooterTemplate>
                            <asp:Button ID="btnAddNewWorkType" runat="server" 
                                Text="Add" OnClick ="addNewWorkType" CssClass="workTypebutton" 
                                Width="100%" Height="100%"/></FooterTemplate>
                           <FooterStyle Height="27px" Width="6%" />
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" ShowHeader="True" >
                        <ItemStyle Height="27px" Width="17%" HorizontalAlign="Center" 
                            VerticalAlign="Middle" />
                        </asp:CommandField>
                        <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" runat="server" 
                                OnClientClick="return confirm('Do you want to delete this Work Service Line?')" 
                                CommandArgument ='<%# Container.DataItemIndex %>' Text="Delete" 
                                OnClick="DeleteWorkType"></asp:LinkButton>
                        </ItemTemplate>
                            <ItemStyle Height="27px" Width="9%" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#006699" BorderColor="#003366" BorderStyle="Solid" 
                        BorderWidth="1px" ForeColor="White" Height="27px" HorizontalAlign="Left" 
                        VerticalAlign="Middle" />
                </asp:GridView>
                <div class="gridBottomDiv">
                <span class="totalUsedSpan"><asp:Label ID="lblTotalUsedTime" runat="server" 
                        Text="Total Used Time :  0.00" CssClass ="totalUsedTime"></asp:Label></span>
                <span class="lnlMessageSpan"><asp:Label ID="lblMessage" runat="server" Text="" CssClass ="lblMessage"></asp:Label></span>
                </div>
            </div>
            <div class="bottomButtondiv">
                    <span class="bindingNavigator">
                        <asp:Button ID="btnFirst" runat="server" Text="<<" 
                            CssClass="bindingNavigatorbtn" onclick="btnFirst_Click" />
                        <asp:Button ID="btnPrevious" runat="server" Text="<" 
                            CssClass="bindingNavigatorbtn" onclick="btnPrevious_Click" />
                        <asp:Label ID="lblCount" runat="server" Text="0 of 0" 
                            CssClass="bindingNavigatorCount" Font-Bold="True" Font-Names="Segoe UI Light" 
                            Font-Size="10pt" ForeColor="#004080" Height="25px" Width="100px"></asp:Label>
                        <asp:Button ID="btnNext" runat="server" Text=">" 
                            CssClass="bindingNavigatorbtn" onclick="btnNext_Click" />
                        <asp:Button ID="btnLast" runat="server" Text=">>" 
                            CssClass="bindingNavigatorbtn" onclick="btnLast_Click" />
                    </span>
                    <span class="buttonControl">
                    <span class="bottomButtonSpan">
                        <asp:Button ID="btnCreateInvoice" runat="server" Text="Create Invoice Order" 
                            CssClass="bottomButton" CommandArgument="C" 
                            onclick="btnCreateInvoice_Click" OnClientClick="cursorBusy();" />
                    </span>
                    <span class="bottomButtonSpan">
                        <asp:Button ID="btnDownloadInvoiceReport" runat="server" 
                            Text="Download Invoice" CssClass="bottomButton" 
                        onclick="btnDownloadInvoiceReport_Click"/>
                    </span>
                    <span class="bottomButtonSpan">
                        <asp:Button ID="btnSendInvoice" runat="server" Text="Send Invoice" 
                            CssClass="bottomButton" onclick="btnSendInvoice_Click" OnClientClick="cursorBusy();"/>
                    </span>
                    <span class="bottomButtonSpan">
                        <asp:Button ID="btnCreateNewOrder" runat="server" Text="Create New Order" 
                            CssClass="bottomButton" onclick="btnCreateNewOrder_Click" />
                    </span>
                    </span>
            </div>
        </ContentTemplate>
        <Triggers>
        <asp:AsyncPostBackTrigger ControlID="gvWorkType" />
        <asp:AsyncPostBackTrigger ControlID="imgbtnSearch" />
        </Triggers>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        function OnClientShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 99999999999;
        }
        function CheckDecimalValues(evt) {
            var keyCode = evt.keyCode ? evt.keyCode : ((evt.charCode) ? evt.charCode : evt.which);
            //    8   Backspace
            //    9   Tab key 
            //    46  Delete
            //    35  End Key
            //    36  Home Key
            //    37  Left arrow Move
            //    39  Right arrow Move
            if (!(keyCode >= 48 && keyCode <= 57)) {
                if (!(keyCode == 8 || keyCode == 9 || keyCode == 35 || keyCode == 36 || keyCode == 37 || keyCode == 39 || keyCode == 46)) {
                    return false;
                }
                else {
                    return true;
                }
            }

            var velement = evt.target || evt.srcElement
            var fstpart_val = velement.value;
            var fstpart = velement.value.length;
            if (fstpart.length == 2) return false;
            var parts = velement.value.split('.');
            if (parts[0].length >= 14) return false;
            if (parts.length == 2 && parts[1].length >= 2) return false;
        }
        function cursorDefault() {
                                    document.body.style.cursor = 'default';
                                 }
        function cursorBusy() {
                                document.body.style.cursor = 'wait';
                        }
    </script>
</asp:Content>
