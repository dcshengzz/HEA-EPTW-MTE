<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="WorkflowControl.aspx.cs" Inherits="HEA.ePTW.Settings.WorkflowControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/ePTW_Gridview.css") %>' />
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/ePTW_Gridview.js") %>'></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageToolbar" runat="server">
    <dx:ASPxMenu runat="server" ID="PageToolbar" ClientInstanceName="pageToolbar"
        ItemAutoWidth="false" ApplyItemStyleToTemplates="true" ItemWrap="false"
        AllowSelectItem="false" SeparatorWidth="0"
        Width="100%" CssClass="page-toolbar">
        <ClientSideEvents ItemClick="onPageToolbarItemClick" />
        <SettingsAdaptivity Enabled="true" EnableAutoHideRootItems="true"
            EnableCollapseRootItemsToIcons="true" CollapseRootItemsToIconsAtWindowInnerWidth="600" />
        <ItemStyle CssClass="item" VerticalAlign="Middle" />
        <ItemImage Width="16px" Height="16px" />
        <Items>
            <dx:MenuItem>
                <Template>
                    <h1>Process Workflow Control</h1>
                </Template>
            </dx:MenuItem>
            <dx:MenuItem Name="New" Text="New" Alignment="Right" AdaptivePriority="2">
                <Image Url="~/Content/Images/add.svg" />
            </dx:MenuItem>
            <dx:MenuItem Name="Edit" Text="Edit" Alignment="Right" AdaptivePriority="2">
                <Image Url="~/Content/Images/edit.svg" />
            </dx:MenuItem>
            <dx:MenuItem Name="MoveUp" Text="Move Up" Alignment="Right" AdaptivePriority="2">
                <Image Url="~/Content/Icons/arrow-up.svg" />
            </dx:MenuItem>
            <dx:MenuItem Name="MoveDown" Text="Move Down" Alignment="Right" AdaptivePriority="2">
                <Image Url="~/Content/Icons/arrow-down.svg" />
            </dx:MenuItem>
        </Items>
    </dx:ASPxMenu>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageContent" runat="server">
    <dx:ASPxFormLayout ID="flContent" runat="server" Width="99%" ShowItemCaptionColon="False">
        <Items>
            <dx:LayoutGroup Caption="" ColSpan="1" ShowCaption="False" GroupBoxDecoration="None" UseDefaultPaddings="False">
                <GridSettings StretchLastItem="True">
                </GridSettings>
                <Items>
                    <dx:LayoutItem ColSpan="1" Caption="Select Module">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxComboBox ID="cbModule" runat="server" ValueField="Module"  AutoPostBack="True" Width="200" NullValueItemDisplayText="{0}" TextFormatString="{0}">
                                    <Columns>
                                        <dx:ListBoxColumn FieldName="Module" Width="100px">
                                        </dx:ListBoxColumn>
                                        <dx:ListBoxColumn FieldName="Description" Width="250px">
                                        </dx:ListBoxColumn>
                                    </Columns>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="1" ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxGridView 
                                    ID="gvWorkFlow" 
                                    runat="server"
                                    KeyFieldName="ID"
                                    EnableRowsCache="False"
                                    Width="100%"
                                    CssClass="grid-view"
                                    ClientInstanceName="gridView"
                                    AutoGenerateColumns="False"
                                    EnableCallbackAnimation="True" 
                                    OnCellEditorInitialize="gvWorkFlow_CellEditorInitialize"
                                    OnRowValidating="gvWorkFlow_RowValidating"
                                    OnRowUpdating="gvWorkFlow_RowUpdating"
                                    OnRowInserting="gvWorkFlow_RowInserting"
                                    OnCustomCallback="gvWorkFlow_CustomCallback">
                                    <SettingsAdaptivity AdaptivityMode="HideDataCells">
                                    </SettingsAdaptivity>
                                    <SettingsEditing UseFormLayout="True" Mode="EditForm" />
                                    <SettingsBehavior AllowFocusedRow="true" AllowSelectByRowClick="true" AllowDragDrop="false" AllowSelectSingleRowOnly="True" />
                                    <SettingsPopup>
                                        <FilterControl AutoUpdatePosition="False"></FilterControl>
                                    </SettingsPopup>
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="ID" VisibleIndex="0" Visible="False">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Module" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="SN" ShowInCustomizationForm="True" VisibleIndex="2">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataComboBoxColumn Caption="User Role" FieldName="Role" ShowInCustomizationForm="True" VisibleIndex="3">
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataComboBoxColumn FieldName="TriggerToNext" ShowInCustomizationForm="True" VisibleIndex="4">
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataComboBoxColumn FieldName="TriggerToCreator" ShowInCustomizationForm="True" VisibleIndex="5">
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataTextColumn FieldName="StatusCode" ShowInCustomizationForm="True" VisibleIndex="6">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Border BorderWidth="0px" />
                                </dx:ASPxGridView>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
        <Paddings PaddingLeft="5px" />
        <Border BorderWidth="0px" />
    </dx:ASPxFormLayout>
</asp:Content>
