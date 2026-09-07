<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="Templates.aspx.cs" Inherits="HEA.ePTW.Settings.Templates" %>

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
                    <h1>Templates</h1>
                </Template>
            </dx:MenuItem>
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
    </dx:ASPxMenu>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageContent" runat="server">
    <dx:ASPxGridView
        ID="gvTemplates"
        KeyFieldName="ID"
        EnableRowsCache="False"
        Width="100%"
        runat="server"
        CssClass="grid-view"
        ClientInstanceName="gridView"
        AutoGenerateColumns="False"
        EnableCallbackAnimation="True" 
        OnCellEditorInitialize="gvTemplates_CellEditorInitialize"
        OnRowValidating="gvTemplates_RowValidating"
        OnRowUpdating="gvTemplates_RowUpdating"
        OnRowInserting="gvTemplates_RowInserting"
        OnCustomCallback="gvTemplates_CustomCallback">
        <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True" />
        <SettingsAdaptivity AdaptivityMode="HideDataCells">
        </SettingsAdaptivity>
        <SettingsEditing UseFormLayout="True" Mode="EditForm" />
        <SettingsBehavior AllowFocusedRow="true" AllowSelectByRowClick="true" AllowDragDrop="false" AllowSelectSingleRowOnly="True" />
        <SettingsPopup>
            <FilterControl AutoUpdatePosition="False"></FilterControl>
        </SettingsPopup>
        <EditFormLayoutProperties>
            <Items>
                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Module">
                </dx:GridViewColumnLayoutItem>
                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="TemplateName">
                </dx:GridViewColumnLayoutItem>
                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Description">
                </dx:GridViewColumnLayoutItem>
                <dx:EmptyLayoutItem ColSpan="1">
                </dx:EmptyLayoutItem>
                <dx:EditModeCommandLayoutItem ColSpan="1" HorizontalAlign="Right">
                </dx:EditModeCommandLayoutItem>
            </Items>
        </EditFormLayoutProperties>
        <Columns>
            <dx:GridViewDataComboBoxColumn FieldName="Module" ShowInCustomizationForm="True" VisibleIndex="0" Width="50px" AdaptivePriority="1">
                <PropertiesComboBox ValueField="Module" DropDownStyle="DropDownList" TextFormatString="{0}">
                    <Columns>
                        <dx:ListBoxColumn FieldName="Module" Width="20%" />
                        <dx:ListBoxColumn FieldName="Description" Width="80%" />
                    </Columns>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataTextColumn FieldName="TemplateName" VisibleIndex="2" Width="200px" AdaptivePriority="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="3" AdaptivePriority="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn FieldName="Status" CellStyle-HorizontalAlign="Center" Width="40" VisibleIndex="4" AdaptivePriority="1">
                <EditFormSettings Visible="False" />
                <DataItemTemplate>
                    <dx:ASPxImage runat="server" CssClass='<%# string.Format("column-status status{0}", Eval("[Status]")) %>' />
                </DataItemTemplate>
                <CellStyle HorizontalAlign="Center"></CellStyle>
            </dx:GridViewDataComboBoxColumn>
        </Columns>
        <Templates>
            <DetailRow>
                <div style="padding: 5px;">
                    <dx:ASPxGridView ID="ASPxGridView1"
                        runat="server"
                        CssClass="detail-grid-view"
                        AutoGenerateColumns="False"
                        ClientInstanceName="grid"
                        KeyFieldName="CategoryID"
                        Width="100%" OnBeforePerformDataSelect="ASPxGridView1_BeforePerformDataSelect">
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <Settings ShowColumnHeaders="False" />
                        <SettingsPopup>
                            <FilterControl AutoUpdatePosition="False"></FilterControl>
                        </SettingsPopup>
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="Question" VisibleIndex="0" Width="60%
">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Answer" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Editor" ShowInCustomizationForm="False" Visible="False" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <Styles>
                            <Row Wrap="True">
                            </Row>
                            <DetailRow Wrap="True">
                            </DetailRow>
                            <DetailCell Wrap="True">
                            </DetailCell>
                        </Styles>
                        <Border BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" />
                    </dx:ASPxGridView>
                </div>
            </DetailRow>
        </Templates>
        <Styles>
            <PagerBottomPanel CssClass="pager" />
            <FocusedRow CssClass="focused" />
        </Styles>
        <ClientSideEvents Init="onGridViewInit" SelectionChanged="onGridViewSelectionChanged" />
    </dx:ASPxGridView>



</asp:Content>
