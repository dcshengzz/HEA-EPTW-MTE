<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="UserSetting.aspx.cs" Inherits="HEA.ePTW.Settings.UserSetting" %>

<asp:Content runat="server" ContentPlaceHolderID="Head">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/ePTW_Standard.css") %>' />
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/ePTW_Gridview.js?v=20260907-1") %>'></script>
    <script type="text/javascript">
        function htmlEncodeUserImportValue(value) {
            return String(value == null ? "" : value)
                .replace(/&/g, "&amp;").replace(/</g, "&lt;")
                .replace(/>/g, "&gt;").replace(/"/g, "&quot;")
                .replace(/'/g, "&#39;");
        }
        function showUserExcelUpload() {
            userUploadPopup.Show();
        }
        window.showExcelUpload = showUserExcelUpload;
        function onUserToolbarItemClick(s, e) {
            if (e.item && e.item.name === "UploadExcel") {
                showUserExcelUpload();
                return;
            }
            window.onPageToolbarItemClick(s, e);
        }
        function onUserExcelUploadComplete(s, e) {
            userUploadPopup.Hide();
            if (!e.callbackData) {
                alert("The Excel file could not be imported. Please use a valid .xlsx or .xlsm file.");
                return;
            }
            var result = JSON.parse(e.callbackData);
            if (!result.Issues || result.Issues.length === 0) {
                alert("Import successful. " + result.ImportedCount + " row(s) imported.");
                gridView.PerformCallback("refresh");
                return;
            }
            var html = "<p><strong>" + result.ImportedCount + " row(s) imported successfully.</strong> " +
                result.Issues.length + " issue(s) require attention.</p>" +
                "<div class='import-table-wrap'><table class='import-issues'><thead><tr>" +
                "<th>Row</th><th>Field</th><th>Issue Description</th><th>Suggested Fix</th>" +
                "</tr></thead><tbody>";
            result.Issues.forEach(function (issue) {
                html += "<tr><td>" + htmlEncodeUserImportValue(issue.Row) + "</td><td>" +
                    htmlEncodeUserImportValue(issue.Field) + "</td><td>" +
                    htmlEncodeUserImportValue(issue.Description) + "</td><td>" +
                    htmlEncodeUserImportValue(issue.SuggestedFix) + "</td></tr>";
            });
            html += "</tbody></table></div>";
            document.getElementById("userImportIssues").innerHTML = html;
            userImportResultPopup.Show();
            gridView.PerformCallback("refresh");
        }
    </script>
    <style type="text/css">
        .templateTable {
            border-collapse: collapse;
            width: 98%;
            margin-left: 20px;
        }

        .templateTable td {
            border: solid 0px #C2D4DA;
            padding: 0px;
        }

        .templateTable td.value {
            font-weight: bold;
        }

        .imageCell {
            vertical-align: top;
            padding-right: 10px;
            width: 120px;
        }

        .dxTheme-Office365Dark .templateTable td {
            border-color: black;
        }

        .dxgvGroupPanel_Office365 .dxgvHeader_Office365, .dxgvAdaptiveGroupPanel_Office365 .dxgvHeader_Office365 {
            padding: 10px 22px;
        }
        .import-table-wrap { max-height: 420px; overflow: auto; }
        .import-issues { width: 100%; border-collapse: collapse; }
        .import-issues th, .import-issues td { border: 1px solid #d5d5d5; padding: 8px; text-align: left; vertical-align: top; }
        .import-issues th { background: #494949; color: white; }
    </style>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PageContent" runat="server">
    <dx:ASPxFormLayout runat="server" ID="formLayout" CssClass="formLayout" ShowItemCaptionColon="False" Width="98%">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="650" />
        <Items>
            <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="#494949">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxLabel ID="lblUserTitle" runat="server" Font-Bold="True" Font-Size="14pt" Text="User Management - (All Projects)" ForeColor="White">
                        </dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
            </dx:LayoutItem>
            <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="White">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxMenu runat="server" ID="ASPxMenu1" ClientInstanceName="pageToolbar" Width="100%"
                            ItemAutoWidth="false" ApplyItemStyleToTemplates="true" ItemWrap="false"
                            AllowSelectItem="false" SeparatorWidth="0" BackColor="White">
                            <ClientSideEvents ItemClick="onUserToolbarItemClick" />
                            <SettingsAdaptivity Enabled="true" EnableAutoHideRootItems="true"
                                EnableCollapseRootItemsToIcons="true" CollapseRootItemsToIconsAtWindowInnerWidth="600" />
                            <ItemStyle CssClass="item" VerticalAlign="Middle" />
                            <ItemImage Width="16px" Height="16px" />
                            <Items>
                                <dx:MenuItem Name="UploadExcel" Text="Upload Excel" Alignment="Right" AdaptivePriority="2">
                                    <Image Url="~/Content/Images/export.svg" />
                                </dx:MenuItem>
                                <dx:MenuItem Name="New" Text="New" Alignment="Right" AdaptivePriority="2">
                                    <Image Url="~/Content/Images/add.svg" />
                                </dx:MenuItem>
                                <dx:MenuItem Name="Edit" Text="Edit" Alignment="Right" AdaptivePriority="2" Visible="false">
                                    <Image Url="~/Content/Images/edit.svg" />
                                </dx:MenuItem>
                                <dx:MenuItem Name="Lock" Text="Lock" Alignment="Right" AdaptivePriority="2" Visible="false">
                                    <Image Url="~/Content/Icons/Lock.svg" />
                                </dx:MenuItem>
                                <dx:MenuItem Name="Unlock" Text="Unlock" Alignment="Right" AdaptivePriority="2" Visible="false">
                                    <Image Url="~/Content/Icons/Unlock.svg" />
                                </dx:MenuItem>
                                <dx:MenuItem Name="ResetPassword" Text="Reset Password" Alignment="Right" AdaptivePriority="2" Visible="false">
                                    <Image Url="~/Content/Icons/password-email.svg" />
                                </dx:MenuItem>
                            </Items>
                            <Border BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                        </dx:ASPxMenu>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
            </dx:LayoutItem>
            <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="White">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">

                        <dx:ASPxGridView ID="gvUsers" runat="server" Width="100%" CssClass="grid-view" ClientInstanceName="gridView"
                            AutoGenerateColumns="False" EnableCallbackAnimation="True" KeyFieldName="UserID"
                            OnCellEditorInitialize="gvUsers_CellEditorInitialize"
                            OnRowValidating="gvUsers_RowValidating"
                            OnRowInserting="gvUsers_RowInserting"
                            OnRowUpdating="gvUsers_RowUpdating"
                            OnCustomCallback="gvUsers_CustomCallback"
                            OnHtmlRowPrepared="gvUsers_HtmlRowPrepared">

                            <Settings ShowColumnHeaders="True"></Settings>
                            <Settings GridLines="None" ShowGroupPanel="False" ShowFilterRow="false" ShowHeaderFilterButton="True" />
                            <Settings ShowPreview="True" />
                            <SettingsSearchPanel GroupOperator="And" Visible="True" />
                            <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                            <SettingsEditing UseFormLayout="True" Mode="PopupEditForm" NewItemRowPosition="Bottom" />
                            <SettingsBehavior AllowEllipsisInText="True" AllowFocusedRow="true" AllowSelectByRowClick="true" AllowDragDrop="True" AllowSelectSingleRowOnly="True" AutoExpandAllGroups="True" />
                            <SettingsPopup>
                                <EditForm HorizontalAlign="Center" Modal="True" PopupAnimationType="Auto" VerticalAlign="WindowCenter">
                                </EditForm>
                                <FilterControl AutoUpdatePosition="False"></FilterControl>
                            </SettingsPopup>

                            <Templates>
                                <PreviewRow>
                                    <table class="templateTable">
                                        <tr>
                                            <td class="imageCell" style="" rowspan="5">
                                                <dx:ASPxBinaryImage ID="Photo" Height="90px" Width="100px" runat="server" Value='<%# Eval("Photo") %>' Border-BorderColor="Black" Border-BorderStyle="Solid" Border-BorderWidth="1" />
                                            </td>
                                            <td class="value" style="vertical-align: top; padding-top: 2px; padding-right: 10px; width: 120px;">
                                                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Full Name" Font-Bold="True" />
                                            </td>
                                            <td class="value" style="vertical-align: top; padding-top: 2px; padding-right: 0px;">
                                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text='<%# Eval("FullName") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="value" style="vertical-align: top; padding-top: 2px; padding-right: 10px;">
                                                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Contact" Font-Bold="True" />
                                            </td>
                                            <td class="value" style="vertical-align: top; padding-top: 2px; padding-right: 0px;">
                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text='<%# Eval("ContactNo") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="value" style="vertical-align: top; padding-top: 2px; padding-right: 10px;">
                                                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="Document No" Font-Bold="True" />
                                            </td>
                                            <td class="value" style="vertical-align: top; padding-top: 2px; padding-right: 0px;">
                                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text='<%# Eval("DocumentNo") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="value" style="vertical-align: top; padding-top: 2px; padding-right: 10px;">
                                                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="Position" Font-Bold="True" />
                                            </td>
                                            <td class="value" style="vertical-align: top; padding-top: 2px; padding-right: 0px;">
                                                <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text='<%# Eval("Position") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="value" style="vertical-align: top; padding-top: 2px; padding-right: 10px;">
                                                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="User Roles" Font-Bold="True" />
                                            </td>
                                            <td class="value" style="vertical-align: top; padding-top: 2px; padding-right: 0px;">
                                                <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text='<%# Eval("Roles") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="value" colspan="3" style="text-align:right; vertical-align:middle; padding-top: 10px; padding-bottom: 10px;" colspan="2">
                                                <dx:ASPxButton ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click"></dx:ASPxButton>
                                                <dx:ASPxButton ID="btnLock" runat="server" Text="Delete" OnClick="btnLock_Click" >
                                                    <ClientSideEvents Click="function(s, e) {
                                                        e.processOnServer = confirm('Are you sure you want to proceed?'); }" 
                                                     />
                                                </dx:ASPxButton>
                                                <%--<dx:ASPxButton ID="btnUnlock" runat="server" Text="Unlock" OnClick="btnUnlock_Click"></dx:ASPxButton>--%>
                                                <dx:ASPxButton ID="btnReset" runat="server" Text="Reset Password" OnClick="btnReset_Click"></dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </PreviewRow>
                                <DetailRow>
                                    <div style="padding: 2px 2px 2px 2px">
                                        <dx:ASPxFormLayout runat="server" ID="formLayout" CssClass="formLayout" ShowItemCaptionColon="False" Width="98%">
                                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="650" />
                                            <Items>
                                                <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="#494949">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Size="11pt" Text="User Role Collection" ForeColor="White">
                                                            </dx:ASPxLabel>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                    <Paddings PaddingBottom="8px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="10px" />
                                                </dx:LayoutItem>
                                                <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="White">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxMenu runat="server" ID="DetailToolbar" ClientInstanceName="detailToolbar"
                                                                ItemAutoWidth="false" ApplyItemStyleToTemplates="true" ItemWrap="false"
                                                                AllowSelectItem="false" SeparatorWidth="0"
                                                                Width="100%" CssClass="page-subtoolbar" BackColor="White">
                                                                <ClientSideEvents ItemClick="onPageToolbarItemClick" />
                                                                <SettingsAdaptivity Enabled="true" EnableAutoHideRootItems="true"
                                                                    EnableCollapseRootItemsToIcons="true" CollapseRootItemsToIconsAtWindowInnerWidth="600" />
                                                                <ItemStyle CssClass="item" VerticalAlign="Middle" />
                                                                <ItemImage Width="16px" Height="16px" />
                                                                <Items>
                                                                    <dx:MenuItem Name="NewDetail" Text="New" Alignment="Right" AdaptivePriority="2">
                                                                        <Image Url="~/Content/Images/add.svg" />
                                                                    </dx:MenuItem>
                                                                    <dx:MenuItem Name="DeleteDetail" Text="Delete" Alignment="Right" AdaptivePriority="2">
                                                                        <Image Url="~/Content/Images/delete.svg" />
                                                                    </dx:MenuItem>
                                                                </Items>
                                                            </dx:ASPxMenu>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="White">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxGridView ID="gvRoles" KeyFieldName="UserID" runat="server" Width="100%" ClientInstanceName="detailgridView"
                                                                EnableRowsCache="False" CssClass="grid-view" AutoGenerateColumns="False"
                                                                EnableCallbackAnimation="True" Settings-ShowColumnHeaders="False"
                                                                OnCellEditorInitialize="gvRoles_CellEditorInitialize" OnBeforePerformDataSelect="gvRoles_BeforePerformDataSelect"
                                                                OnRowValidating="gvRoles_RowValidating" OnRowInserting="gvRoles_RowInserting" OnCustomCallback="gvRoles_CustomCallback">
                                                                
                                                                <SettingsEditing UseFormLayout="True" Mode="EditForm" />
                                                                <Settings ShowColumnHeaders="False"></Settings>
                                                                <SettingsBehavior AllowFocusedRow="true" AllowSelectByRowClick="true" AllowDragDrop="false" AllowSelectSingleRowOnly="True" />
                                                                <SettingsPopup>
                                                                    <FilterControl AutoUpdatePosition="False"></FilterControl>
                                                                </SettingsPopup>

                                                                <Columns>
                                                                    <dx:GridViewDataComboBoxColumn FieldName="RoleID" VisibleIndex="2">
                                                                        <PropertiesComboBox TextField="RoleID" ValueField="RoleID">
                                                                        </PropertiesComboBox>
                                                                    </dx:GridViewDataComboBoxColumn>
                                                                </Columns>

                                                                <EditFormLayoutProperties ColumnCount="1" ShowItemCaptionColon="False">
                                                                    <Items>
                                                                        <dx:GridViewColumnLayoutItem ColumnName="Role ID" Caption="User Role" />
                                                                        <dx:EmptyLayoutItem />
                                                                        <dx:EditModeCommandLayoutItem ShowCancelButton="true" ShowUpdateButton="true" HorizontalAlign="Right" />
                                                                    </Items>
                                                                </EditFormLayoutProperties>
                                                                <Styles>
                                                                    <Header Font-Bold="True" BackColor="#454545" ForeColor="White">
                                                                    </Header>
                                                                    <FocusedRow BackColor="#D5D5FF" Font-Bold="False" ForeColor="Black">
                                                                    </FocusedRow>
                                                                    <EditForm VerticalAlign="Middle">
                                                                    </EditForm>
                                                                </Styles>
                                                            </dx:ASPxGridView>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:ASPxFormLayout>
                                    </div>
                                </DetailRow>
                            </Templates>
                            <Columns>
                                <dx:GridViewDataBinaryImageColumn FieldName="Photo" VisibleIndex="0" Width="120px" Visible="false">
                                    <PropertiesBinaryImage ImageHeight="80px" ImageWidth="100px" ShowLoadingImage="True">
                                        <EditingSettings Enabled="True" UploadSettings-UploadValidationSettings-MaxFileSize="4194304">
                                            <UploadSettings>
                                                <UploadValidationSettings MaxFileSize="4194304"></UploadValidationSettings>
                                            </UploadSettings>
                                        </EditingSettings>
                                    </PropertiesBinaryImage>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataBinaryImageColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="Title" VisibleIndex="1" Visible="false">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Text="Mr" Value="Mr" />
                                            <dx:ListEditItem Text="Mrs" Value="Mrs" />
                                            <dx:ListEditItem Text="Miss" Value="Miss" />
                                            <dx:ListEditItem Text="Ms" Value="Ms" />
                                        </Items>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn FieldName="FirstName" VisibleIndex="4" Visible="True" Width="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="DocumentType" VisibleIndex="6" Visible="false">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Text="NRIC" Value="NRIC" />
                                            <dx:ListEditItem Text="FIN" Value="FIN" />
                                            <dx:ListEditItem Text="WP" Value="WP" />
                                        </Items>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn FieldName="DocumentNo" VisibleIndex="7" Visible="True" Width="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ContactNo" VisibleIndex="8" Visible="True" Width="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="EmailAddress" VisibleIndex="9" Visible="True" Width="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="ConstructorName" VisibleIndex="2" Visible="True" Width="150" Caption="Contractor Name">
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn FieldName="Position" VisibleIndex="10" Visible="True" Width="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="UserRoleCategory" Caption="User Roles" VisibleIndex="11" Visible="False">
                                    <PropertiesComboBox DropDownStyle="DropDownList">
                                        <Items>
                                            <dx:ListEditItem Text="Applicant" Value="Applicant" />
                                            <dx:ListEditItem Text="Approver" Value="Approver" />
                                            <dx:ListEditItem Text="Admin" Value="Admin" />
                                        </Items>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn FieldName="UserID" VisibleIndex="3" Width="150">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="StatusText" CellStyle-HorizontalAlign="Center" Width="120" VisibleIndex="13" Visible="true" Caption="Status">
                                    <EditFormSettings Visible="False" />
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn FieldName="LastName" ShowInCustomizationForm="True" VisibleIndex="5" Visible="True" Width="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Password" ShowInCustomizationForm="True" Visible="False" VisibleIndex="11">
                                    <PropertiesTextEdit Password="True">
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Confirm" ShowInCustomizationForm="True" Visible="False" VisibleIndex="12">
                                    <PropertiesTextEdit Password="True">
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Roles" ShowInCustomizationForm="True" VisibleIndex="13" Visible="True" Width="0">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <EditFormLayoutProperties ColumnCount="1" ShowItemCaptionColon="False">
                                <Items>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Title">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="First Name">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Last Name">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Document Type">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Document No">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Contact No">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Email Address">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Contractor Name">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Position">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="User Roles">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="User ID" Visible="False">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColumnName="Photo" ShowCaption="False" HelpText="You can upload JPG, GIF or PNG file. Maximum files size is 4 MB." />
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Password">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Confirm">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:EmptyLayoutItem ColumnSpan="1" />
                                    <dx:EditModeCommandLayoutItem ColumnSpan="1" ShowCancelButton="true" ShowUpdateButton="true" HorizontalAlign="Right" />
                                </Items>
                            </EditFormLayoutProperties>
                            <Styles>
                                <Header Font-Bold="True" BackColor="#454545" ForeColor="White">
                                </Header>
                                <FocusedRow BackColor="#D5D5FF" Font-Bold="False" ForeColor="Black">
                                </FocusedRow>
                                <EditForm VerticalAlign="Middle">
                                </EditForm>
                            </Styles>
                            <ClientSideEvents Init="onGridViewInit" SelectionChanged="onGridViewSelectionChanged" />
                        </dx:ASPxGridView>

                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="5px" PaddingLeft="15px" PaddingRight="8px" PaddingTop="0px" />
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>

    <dx:ASPxPopupControl ID="pcUserUpload" runat="server" ClientInstanceName="userUploadPopup"
        HeaderText="Upload User Excel" Modal="True" CloseAction="CloseButton" CloseOnEscape="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="620px">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <p>Upload an Excel workbook with these Row 1 headers: <strong>Title</strong>, <strong>First Name</strong>, <strong>Last Name</strong>, <strong>Document Type</strong>, <strong>Document No</strong>, <strong>Contact No</strong>, <strong>Email Address</strong>, <strong>Contractor Name</strong>, and <strong>Position</strong>.</p>
                <p>Imported users are assigned the Applicant role by default.</p>
                <dx:ASPxUploadControl ID="ucUserExcel" runat="server" Width="100%" UploadMode="Auto"
                    AutoStartUpload="True" ShowProgressPanel="True" OnFileUploadComplete="ucUserExcel_FileUploadComplete">
                    <AdvancedModeSettings EnableDragAndDrop="True" EnableFileList="False" EnableMultiSelect="False" />
                    <ValidationSettings AllowedFileExtensions=".xlsx,.xlsm" MaxFileSize="10485760" />
                    <ClientSideEvents FileUploadComplete="onUserExcelUploadComplete" />
                </dx:ASPxUploadControl>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="pcUserImportResult" runat="server" ClientInstanceName="userImportResultPopup"
        HeaderText="Import Incomplete: Issues Detected" Modal="True" CloseAction="CloseButton" CloseOnEscape="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="850px">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <div id="userImportIssues"></div>
                <div style="text-align:right; margin-top:12px;">
                    <dx:ASPxButton ID="btnCloseUserImport" runat="server" Text="Close" AutoPostBack="False">
                        <ClientSideEvents Click="function(s, e) { userImportResultPopup.Hide(); }" />
                    </dx:ASPxButton>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</asp:Content>
