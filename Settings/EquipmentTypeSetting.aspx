<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="EquipmentTypeSetting.aspx.cs" Inherits="HEA.ePTW.Settings.EquipmentTypeSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/ePTW_Standard.css") %>' />
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/ePTW_Gridview.js") %>'></script>
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

<asp:Content ID="Content3" ContentPlaceHolderID="PageContent" runat="server">
    <dx:ASPxFormLayout runat="server" ID="formLayout" CssClass="formLayout" ShowItemCaptionColon="False" Width="98%">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="650" />
        <Items>
            <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="#494949">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Size="14pt" Text="Equipment Type - (All Projects)" ForeColor="White">
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
                                    <Image Url="~/Content/Icons/Lock.svg" />
                                </dx:MenuItem>
                                <dx:MenuItem Name="Unlock" Text="Unlock" Alignment="Right" AdaptivePriority="2">
                                    <Image Url="~/Content/Icons/Unlock.svg" />
                                </dx:MenuItem>
                            </Items>
                            <Border BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                        </dx:ASPxMenu>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="0px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="10px" />
            </dx:LayoutItem>
            <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="White">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxGridView
                            ID="gvEquipmentType" KeyFieldName="EquipmentType" EnableRowsCache="False" Width="100%" runat="server"
                            CssClass="grid-view" ClientInstanceName="gridView" AutoGenerateColumns="False" EnableCallbackAnimation="True" 
                            OnCellEditorInitialize="gvEquipmentType_CellEditorInitialize" 
                            OnRowValidating="gvEquipmentType_RowValidating" 
                            OnRowUpdating="gvEquipmentType_RowUpdating" 
                            OnRowInserting="gvEquipmentType_RowInserting" 
                            OnCustomCallback="gvEquipmentType_CustomCallback">

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

                            <EditFormLayoutProperties>
                                <Items>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Category ID">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Equipment Type">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Description">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:EmptyLayoutItem ColSpan="1">
                                    </dx:EmptyLayoutItem>
                                    <dx:EditModeCommandLayoutItem ColSpan="1" HorizontalAlign="Right">
                                    </dx:EditModeCommandLayoutItem>
                                </Items>
                            </EditFormLayoutProperties>

<%--                            <SettingsBehavior AllowEllipsisInText="true" AllowFocusedRow="true" AllowSelectByRowClick="true" AllowDragDrop="false" AllowSelectSingleRowOnly="True" />
                            <SettingsBehavior AllowFocusedRow="true" AllowSelectByRowClick="true" AllowDragDrop="false" AllowSelectSingleRowOnly="True" />
                            <SettingsPopup>
                                <FilterControl AutoUpdatePosition="False"></FilterControl>
                            </SettingsPopup>
                            <SettingsSearchPanel GroupOperator="Or" ShowClearButton="False" Visible="True" />--%>


                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="CategoryID">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="EquipmentType">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="StatusText" Caption="Status" CellStyle-HorizontalAlign="Center" Width="90" VisibleIndex="4">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataMemoColumn FieldName="Description" VisibleIndex="2">
                                </dx:GridViewDataMemoColumn>
                            </Columns>
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
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="0px" />
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
</asp:Content>
