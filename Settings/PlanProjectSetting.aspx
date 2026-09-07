<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="PlanProjectSetting.aspx.cs" Inherits="HEA.ePTW.Settings.PlanProjectSetting" %>

<asp:Content runat="server" ContentPlaceHolderID="Head">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/StandardGridview.css") %>' />
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/StandardGridview.js") %>'></script>
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
    </dx:ASPxMenu>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageContent" runat="server">
    <dx:ASPxGridView 
        ID="gvProjects"
        ClientInstanceName="gridView"
        runat="server"
        AutoGenerateColumns="False"
        CssClass="grid-view"
        KeyFieldName="ID"
        Width="100%"
        EnableRowsCache="False">
        <Columns>
            <dx:GridViewDataImageColumn FieldName="ThumbnailUrl" Caption="Photo" CellRowSpan="3" Width="80" VisibleIndex="0">
                <PropertiesImage ImageWidth="64" />
                <EditFormSettings Visible="False" />
            </dx:GridViewDataImageColumn>
            <dx:GridViewDataColumn FieldName="Name" Caption="Building Name" Width="20%" VisibleIndex="1" />
            <dx:GridViewDataMemoColumn FieldName="Description" VisibleIndex="2">
                <EditFormSettings ColumnSpan="2" />
            </dx:GridViewDataMemoColumn>
            <dx:GridViewDataComboBoxColumn FieldName="Status" CellStyle-HorizontalAlign="Center" Width="45" VisibleIndex="3">
                <EditFormSettings Visible="False" />
                <DataItemTemplate>
                    <dx:ASPxImage runat="server" CssClass='<%# string.Format("column-status status{0}", Eval("[Status]")) %>' />
                </DataItemTemplate>
                <CellStyle HorizontalAlign="Center"></CellStyle>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataComboBoxColumn Caption="Main Constructor" FieldName="MainConstructorID" Visible="False" VisibleIndex="4">
                <EditFormSettings Visible="True" />
            </dx:GridViewDataComboBoxColumn>
        </Columns>
        <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />       
        <SettingsBehavior AllowFocusedRow="true" AllowSelectByRowClick="true" AllowEllipsisInText="False" AllowDragDrop="false" />

        <Templates>
            <DetailRow>
                <div style="padding: 3px 3px 2px 3px">
                    <dx:ASPxPageControl runat="server" ID="pageControl" Width="90%" EnableCallBacks="true">
                        <TabPages>
                            <dx:TabPage Text="Details" Visible="true">
                                <ContentCollection>
                                    <dx:ContentControl runat="server">
                                        <table class="templateTable">
                                                        <tr>
                                                            <td class="imageCell">
                                                                <dx:ASPxImageSlider ID="ASPxImageSlider1" runat="server" EnableTheming="false" ImageSourceFolder='<%# Eval("PhotoUrl") %>' Width="100%" BackColor="White" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <h4><%# Eval("Name") %></h4>
                                                                <p><%# Eval("Description") %></p>
                                                            </td>
                                                        </tr>
                                                    </table>                                                   
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                        </TabPages>
                    </dx:ASPxPageControl>
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
