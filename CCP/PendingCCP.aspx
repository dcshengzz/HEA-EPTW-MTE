<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="PendingCCP.aspx.cs" Inherits="HEA.ePTW.CCP.PendingCCP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/ePTW.css") %>' />    
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/ePTW_TBM.js") %>'></script>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PageContent" runat="server">
    <dx:ASPxFormLayout runat="server" ID="flCCP" CssClass="formLayout" ShowItemCaptionColon="False" RequiredMark="" Width="98%">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="650" />
        <Items>
            <dx:LayoutItem BackColor="#494949" ColSpan="1" ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxLabel ID="lblTitle" runat="server" Font-Bold="True" Font-Size="14pt" ForeColor="White" Text="Compliance Check Points - Pending">
                        </dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
            </dx:LayoutItem>
            <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxGridView ID="gvCCP" ClientInstanceName="gridView" runat="server" CssClass="grid-view"
                                            KeyFieldName="Key" EnablePagingGestures="False" EnableRowsCache="False" AutoGenerateColumns="False">
                            <Settings ShowHeaderFilterButton="True" ShowTitlePanel="False" />
                            <SettingsBehavior AllowEllipsisInText="true" AllowDragDrop="false" AllowSelectSingleRowOnly="True" />
                            <SettingsPopup>
                                <FilterControl AutoUpdatePosition="False"></FilterControl>
                            </SettingsPopup>
                            <SettingsSearchPanel GroupOperator="Or" Visible="True"  />
                            <Settings GridLines="Both" ShowGroupPanel="false" ShowFilterRow="false" />
                            <Settings ShowColumnHeaders="False"></Settings>
                            <Settings ShowPreview="true" />
                            <Templates>
                                <PreviewRow>
                                    <table class="templateTable">
                                        <tr> 
                                            <td class="value" style="width: 100px; vertical-align: top; padding-top: 8px; padding-right: 5px;" colspan="1">
                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text='<%# Eval("EquipmentName") %>' Font-Bold="False" />
                                            </td>
                                            <td class="value" style="vertical-align: top; padding-top: 8px; padding-right: 5px;" colspan="1">
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%# Eval("Description") %>' Font-Bold="False" />
                                            </td>
                                        </tr>
                                        <tr> 
                                            <td class="value" style="vertical-align: top; padding-top: 8px; padding-right: 5px;" colspan="2">
                                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text='<%# Eval("StatusText") %>' Font-Bold="True"/>
                                            </td>
                                        </tr>
                                    </table>
                                </PreviewRow>
                            </Templates>
                            <Columns>
                                <dx:GridViewDataDateColumn FieldName="ActivitiesDate" ShowInCustomizationForm="True" VisibleIndex="0" Width="110px">
                                    <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                                    </PropertiesDateEdit>
                                    <CellStyle Font-Bold="False">
                                    </CellStyle>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="EquipmentName" ShowInCustomizationForm="True" VisibleIndex="1" Width="0px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="KeyActivitiesName" ShowInCustomizationForm="True" VisibleIndex="2" AdaptivePriority="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Description" ShowInCustomizationForm="True" VisibleIndex="3" Width="0px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="StatusText" Caption="Status" ShowInCustomizationForm="True" VisibleIndex="4" AdaptivePriority="1" Width="0px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn ShowInCustomizationForm="True" VisibleIndex="7">
                                    <DataItemTemplate>
                                        <dx:ASPxButton ID="btnView" runat="server" Text="Verify" RenderMode="Button" Font-Bold="False" BackColor="Transparent" ForeColor="Black" HoverStyle-BackColor="White" 
                                            HoverStyle-BorderRight-BorderWidth="1px" HoverStyle-BorderRight-BorderColor="#727272" HoverStyle-BorderRight-BorderStyle="Dotted"
                                            HoverStyle-BorderBottom-BorderWidth="1px" HoverStyle-BorderBottom-BorderColor="#727272" HoverStyle-BorderBottom-BorderStyle="Dotted"
                                            OnClick="btnView_Click">
                                            <Image ToolTip="Report" Url="~/Content/Icons/verify.svg" Height="20px" Width="20px" />
                                        </dx:ASPxButton>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Right">
                                    </CellStyle>
                                </dx:GridViewDataDateColumn>
                            </Columns>
                            <Styles>
                                <Row BackColor="#D4D4D4">
                                </Row>
                                <AlternatingRow BackColor="#D4D4D4">
                                </AlternatingRow>
                            </Styles>
                        </dx:ASPxGridView>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="5px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="5px" />
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>






<%--    <dx:ASPxGridView ID="gvCCP" ClientInstanceName="gridView" runat="server" CssClass="grid-view"
                        KeyFieldName="Key" EnablePagingGestures="False" EnableRowsCache="False" AutoGenerateColumns="False">
        <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowHideDataCellsByColumnMinWidth="True" AllowOnlyOneAdaptiveDetailExpanded="True">
        </SettingsAdaptivity>
        <Settings ShowHeaderFilterButton="True"/>
        <SettingsBehavior AllowEllipsisInText="true" AllowDragDrop="false" AllowSelectSingleRowOnly="True" />
        <SettingsPopup>
        <FilterControl AutoUpdatePosition="False"></FilterControl>
        </SettingsPopup>
        <SettingsSearchPanel GroupOperator="Or" Visible="True" />
        <Columns>
            <dx:GridViewDataTextColumn VisibleIndex="0" Caption="Date" AdaptivePriority="1">
                <DataItemTemplate>
                    <dx:ASPxButton ID="btnDownload" runat="server" Text='<%# Eval("ActivitiesDate", "{0:dd/MM/yyyy}") %>' RenderMode="Link" OnClick="btnDownload_Click"></dx:ASPxButton>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="EquipmentName" ShowInCustomizationForm="True" VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="KeyActivitiesName" ShowInCustomizationForm="True" VisibleIndex="2" AdaptivePriority="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Description" ShowInCustomizationForm="True" VisibleIndex="3">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="StatusText" Caption="Status" ShowInCustomizationForm="True" VisibleIndex="4" AdaptivePriority="1">
            </dx:GridViewDataTextColumn>
        </Columns>
        <Paddings PaddingLeft="10px" PaddingRight="10px" PaddingTop="10px" />
    </dx:ASPxGridView>--%>
</asp:Content>