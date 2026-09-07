<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="ConstructorSetting.aspx.cs" Inherits="HEA.ePTW.Settings.ContructorSetting" %>

<asp:Content runat="server" ContentPlaceHolderID="Head">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/ePTW_Standard.css") %>' />
    <%--<link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/StandardGridview.css") %>' />--%>
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/StandardGridview.js") %>'></script>
    <style type="text/css">
        .templateTable
        {
            border-collapse: collapse;
            width: 98%;
            margin-left: 20px;
        }
        .templateTable td
        {
            border: solid 0px #C2D4DA;
            padding: 6px;
        }
        .templateTable td.value
        {
            font-weight: bold;
        }
        .imageCell
        {
            vertical-align: top; 
            padding-right: 10px;
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
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Size="14pt" Text="Contractor List - (All Projects)" ForeColor="White">
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
                                <dx:MenuItem Name="Edit" Text="Edit" Alignment="Right" AdaptivePriority="2">
                                    <Image Url="~/Content/Images/edit.svg" />
                                </dx:MenuItem>
                                <dx:MenuItem Name="Lock" Text="Lock" Alignment="Right" AdaptivePriority="2">
                                    <Image Url="~/Content/Icons/lock.svg" />
                                </dx:MenuItem>
                                <dx:MenuItem Name="Unlock" Text="Unlock" Alignment="Right" AdaptivePriority="2">
                                    <Image Url="~/Content/Icons/unlock.svg" />
                                </dx:MenuItem>
                            </Items>
                            <Border BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                        </dx:ASPxMenu>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="0px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
            </dx:LayoutItem>
            <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="White">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxGridView ID="gvConstructors" ClientInstanceName="gridView" runat="server" CssClass="grid-view"
                            KeyFieldName="Name" EnablePagingGestures="True" Width="100%" EnableRowsCache="False"
                            Settings-ShowColumnHeaders="True" AutoGenerateColumns="False" EnableCallbackAnimation="True" 
                            OnRowInserting="gvConstructors_RowInserting" 
                            OnRowValidating="gvConstructors_RowValidating" 
                            OnCustomCallback="gvConstructors_CustomCallback" 
                            OnRowUpdating="gvConstructors_RowUpdating" 
                            OnCellEditorInitialize="gvConstructors_CellEditorInitialize">

                            <Settings ShowColumnHeaders="True"></Settings>
                            <SettingsPager Mode="ShowAllRecords" />
                            <SettingsEditing UseFormLayout="True" Mode="PopupEditForm" />
                            <Settings GridLines="Both" ShowGroupPanel="false" ShowFilterRow="false" ShowHeaderFilterButton="True" />
                            <SettingsBehavior AllowEllipsisInText="true" AllowFocusedRow="true" AllowSelectByRowClick="true" AllowDragDrop="false" AllowSelectSingleRowOnly="True" />
                            <SettingsSearchPanel GroupOperator="Or" Visible="True" />
                            <SettingsPopup>
                                <EditForm HorizontalAlign="Center" Modal="True" PopupAnimationType="Auto" VerticalAlign="WindowCenter">
                                </EditForm>
                                <FilterControl AutoUpdatePosition="False"></FilterControl>
                            </SettingsPopup>

                            <ClientSideEvents Init="onGridViewInit" SelectionChanged="onGridViewSelectionChanged" />

                            <EditFormLayoutProperties ShowItemCaptionColon="False">
                                <Items>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Name">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Description">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Address">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Contact Person">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Contact Number">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:EmptyLayoutItem ColSpan="1" Height="5px">
                                    </dx:EmptyLayoutItem>
                                    <dx:EditModeCommandLayoutItem ColSpan="1" HorizontalAlign="Right">
                                    </dx:EditModeCommandLayoutItem>
                                </Items>
                                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                            </EditFormLayoutProperties>
                            <Columns>
                                <dx:GridViewDataMemoColumn Caption="Description" FieldName="Description" VisibleIndex="2" AdaptivePriority="1" Width="250px">
                                    <PropertiesMemoEdit MaxLength="500">
                                    </PropertiesMemoEdit>
                                    <EditFormSettings ColumnSpan="2" VisibleIndex="2" />
                                    <CellStyle VerticalAlign="Top">
                                    </CellStyle>
                                </dx:GridViewDataMemoColumn>
                                <dx:GridViewDataMemoColumn AllowTextTruncationInAdaptiveMode="True" FieldName="Address" VisibleIndex="3" Width="250px">
                                    <PropertiesMemoEdit MaxLength="500">
                                    </PropertiesMemoEdit>
                                    <CellStyle VerticalAlign="Top">
                                    </CellStyle>
                                </dx:GridViewDataMemoColumn>
                                <dx:GridViewDataTextColumn FieldName="ContactPerson" VisibleIndex="4" Width="150px">
                                    <PropertiesTextEdit MaxLength="50">
                                    </PropertiesTextEdit>
                                    <CellStyle VerticalAlign="Top">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ContactNumber" VisibleIndex="5" Width="150px">
                                    <PropertiesTextEdit MaxLength="50">
                                    </PropertiesTextEdit>
                                    <CellStyle VerticalAlign="Top">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="1" Width="150px">
                                    <PropertiesTextEdit MaxLength="150" EnableClientSideAPI="True">
                                        <ClientSideEvents KeyUp="function(s, e) {
                      var txt = s.GetText();
                      txt = txt.replace(/[^a-zA-Z0-9\s.-]/g,'');
                      s.SetText(txt.toUpperCase());
                    }" />
                                    </PropertiesTextEdit>
                                    <CellStyle VerticalAlign="Top">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="StatusText" Caption="Status" VisibleIndex="8" Width="80px">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataComboBoxColumn>
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
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
</asp:Content>
