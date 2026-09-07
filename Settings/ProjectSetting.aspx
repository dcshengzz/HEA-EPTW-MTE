<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="ProjectSetting.aspx.cs" Inherits="HEA.ePTW.Settings.ProjectSetting" %>

<asp:Content runat="server" ContentPlaceHolderID="Head">
    <%--    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/StandardGridview.css") %>' />
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/StandardGridview.js") %>'></script>--%>
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/ePTW_Standard.css") %>' />
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/ePTW_Gridview.js") %>'></script>
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
    </style>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PageContent" runat="server">
    <dx:ASPxFormLayout runat="server" ID="formLayout" CssClass="formLayout" ShowItemCaptionColon="False" Width="98%">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="650" />
        <Items>
            <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="#494949">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Size="14pt" Text="Project Management - (All Projects)" ForeColor="White">
                        </dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
            </dx:LayoutItem>
            <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="White">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxMenu runat="server" ID="PageToolbar" ClientInstanceName="pageToolbar" Width="100%"
                            ItemAutoWidth="false" ApplyItemStyleToTemplates="true" ItemWrap="false"
                            AllowSelectItem="false" SeparatorWidth="0" BackColor="White">
                            <ClientSideEvents ItemClick="onPageToolbarItemClick" />
                            <SettingsAdaptivity Enabled="true" EnableAutoHideRootItems="true"
                                EnableCollapseRootItemsToIcons="true" CollapseRootItemsToIconsAtWindowInnerWidth="600" />
                            <ItemStyle CssClass="item" VerticalAlign="Middle" />
                            <ItemImage Width="16px" Height="16px" />
                            <Items>
                                <dx:MenuItem Name="New" Text="New" Alignment="Right" AdaptivePriority="2">
                                    <Image Url="~/Content/Images/add.svg" />
                                </dx:MenuItem>
                                <dx:MenuItem Name="Edit" Text="Edit" Alignment="Right" AdaptivePriority="2" Visible="false">
                                    <Image Url="~/Content/Images/edit.svg" />
                                </dx:MenuItem>
                                <dx:MenuItem Name="Lock" Text="Lock" Alignment="Right" AdaptivePriority="2" Visible="false">
                                    <Image Url="~/Content/Icons/lock.svg" />
                                </dx:MenuItem>
                                <dx:MenuItem Name="Unlock" Text="Unlock" Alignment="Right" AdaptivePriority="2" Visible="false">
                                    <Image Url="~/Content/Icons/unlock.svg" />
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
                        <dx:ASPxGridView ID="gvProjects" ClientInstanceName="gridView" runat="server" CssClass="grid-view" Width="100%"
                            KeyFieldName="Name" AutoGenerateColumns="False" EnableCallbackAnimation="True"
                            OnCellEditorInitialize="gvProjects_CellEditorInitialize"
                            OnBeforePerformDataSelect="gvProjects_BeforePerformDataSelect"
                            OnRowInserting="gvProjects_RowInserting"
                            OnRowValidating="gvProjects_RowValidating"
                            OnCustomCallback="gvProjects_CustomCallback"
                            OnRowUpdating="gvProjects_RowUpdating">

                            <Settings ShowColumnHeaders="True"></Settings>
                            <Settings ShowPreview="True" />
                            <SettingsEditing UseFormLayout="True" Mode="PopupEditForm" />
                            <Settings GridLines="Both" ShowGroupPanel="false" ShowFilterRow="false" ShowHeaderFilterButton="True" />
                            <SettingsBehavior AllowEllipsisInText="true" AllowFocusedRow="true" AllowSelectByRowClick="true" AllowDragDrop="false" AllowSelectSingleRowOnly="True" />
                            <SettingsSearchPanel GroupOperator="Or" Visible="True" />
                            <SettingsPopup>
                                <EditForm HorizontalAlign="Center" Modal="True" PopupAnimationType="Auto" VerticalAlign="WindowCenter">
                                </EditForm>
                                <FilterControl AutoUpdatePosition="False"></FilterControl>
                            </SettingsPopup>

                            <ClientSideEvents Init="onGridViewInit" SelectionChanged="onGridViewSelectionChanged"/>

                            <Templates>
                                <PreviewRow>
                                    <table class="templateTable">
                                        <tr>
                                            <td class="imageCell" style="" rowspan="1">
                                                <dx:ASPxBinaryImage ID="ProjectPhoto" Height="80px" Width="80px" runat="server" Value='<%# Eval("Photo") %>' Border-BorderColor="Black" Border-BorderStyle="Solid" Border-BorderWidth="1" />
                                            </td>
                                            <td class="value" style="vertical-align: top; padding-top: 10px; padding-right: 10px;">
                                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text='<%# Eval("Description") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="value" style="vertical-align: top; padding-top: 10px; padding-bottom: 10px;" colspan="2">
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%# Eval("ConstructorName") %>' Font-Bold="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="value" style="vertical-align: top" colspan="2">
                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text='<%# Eval("StartDate", "{0: dd/MM/yyyy}") + " ~ " + Eval("EndDate", "{0: dd/MM/yyyy}") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="value" style="text-align:right; vertical-align:middle; padding-top: 10px; padding-bottom: 10px;" colspan="2">
                                                <dx:ASPxButton ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click"></dx:ASPxButton>
                                                <dx:ASPxButton ID="btnLock" runat="server" Text="Lock" OnClick="btnLock_Click"></dx:ASPxButton>
                                                <dx:ASPxButton ID="btnUnlock" runat="server" Text="Unlock" OnClick="btnUnlock_Click"></dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </PreviewRow>
                            </Templates>
                            <EditFormLayoutProperties ColumnCount="1" ShowItemCaptionColon="False">
                                <Items>
                                    <dx:GridViewColumnLayoutItem ColumnName="Photo" ShowCaption="False" HelpText="You can upload JPG, GIF or PNG file. Maximum files size is 4 MB." />
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Project Name">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Description">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Location">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Main Constructor">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Start Date">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="End Date">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:EmptyLayoutItem ColumnSpan="1" />
                                    <dx:EditModeCommandLayoutItem ColumnSpan="1" ShowCancelButton="true" ShowUpdateButton="true" HorizontalAlign="Right" />
                                </Items>
                            </EditFormLayoutProperties>
                            <Columns>
                                <dx:GridViewDataComboBoxColumn FieldName="StatusText" Caption="Status" VisibleIndex="8" Width="80px">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataBinaryImageColumn FieldName="Photo" VisibleIndex="1" Width="120px" Visible="false">
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
                                <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="2" Caption="Project Name" Width="220px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataMemoColumn FieldName="Description" VisibleIndex="3" AdaptivePriority="1" Width="0px">
                                </dx:GridViewDataMemoColumn>
                                <dx:GridViewDataMemoColumn Caption="Location" FieldName="Address" VisibleIndex="4" AdaptivePriority="1" Width="0px">
                                </dx:GridViewDataMemoColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Main Constructor" FieldName="ConstructorName" VisibleIndex="5" Width="0px">
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataDateColumn Caption="Start Date" FieldName="StartDate" VisibleIndex="6" Width="0px">
                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn Caption="End Date" FieldName="EndDate" VisibleIndex="7" Width="0px">
                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                            </Columns>
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
                <Paddings PaddingBottom="5px" PaddingLeft="15px" PaddingRight="8px" PaddingTop="0px" />
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
</asp:Content>
