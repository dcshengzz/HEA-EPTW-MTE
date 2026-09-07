<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="PendingTBM.aspx.cs" Inherits="HEA.ePTW.TBM.PendingTBM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/ePTW.css") %>' />    
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PageContent" runat="server">
    <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout1" CssClass="formLayout" ShowItemCaptionColon="False" Width="98%">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="650" />
        <Items>
            <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="#494949">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Size="14pt" Text="Toolbox Meeting - Pending" ForeColor="White">
                        </dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
            </dx:LayoutItem>
            <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">


                        <dx:ASPxGridView ID="gvTBM" ClientInstanceName="gridView" runat="server" CssClass="grid-view"
                            KeyFieldName="Key" EnablePagingGestures="False" EnableRowsCache="False" AutoGenerateColumns="False" 
                            OnHtmlDataCellPrepared="gvTBM_HtmlDataCellPrepared">
                            <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowHideDataCellsByColumnMinWidth="False" AllowOnlyOneAdaptiveDetailExpanded="False">
                            </SettingsAdaptivity>
                            <Settings ShowHeaderFilterButton="True" ShowTitlePanel="False" />
                            <SettingsBehavior AllowEllipsisInText="true" AllowDragDrop="false" AllowSelectSingleRowOnly="True" />
                            <SettingsPopup>
                                <FilterControl AutoUpdatePosition="False"></FilterControl>
                            </SettingsPopup>
                            <SettingsSearchPanel GroupOperator="Or" Visible="True" />
                            <Settings GridLines="Both" ShowGroupPanel="false" ShowFilterRow="false" />
                            <Settings ShowColumnHeaders="False"></Settings>
                            <Settings ShowPreview="true" />
                            <Templates>
                                <PreviewRow>
                                    <table class="templateTable">
                                        <tr> 
                                            <td class="value" style="vertical-align: top; padding-top: 8px; padding-right: 5px;" colspan="4">
                                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text='<%# Eval("TodayTeamActionGoal") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="value" style="vertical-align: top; padding-top: 8px; padding-bottom: 4px;" colspan="4">
                                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text='<%# Eval("StatusText") %>' Font-Bold="True" />
                                            </td>
                                        </tr>
                                    </table>
                                </PreviewRow>
                            </Templates>
                            <Columns>
                                <dx:GridViewDataDateColumn FieldName="MeetingDate" ShowInCustomizationForm="True" VisibleIndex="0" Width="110px">
                                    <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                                    </PropertiesDateEdit>
                                    <CellStyle Font-Bold="False">
                                    </CellStyle>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="ConductedByName" ShowInCustomizationForm="True" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn ShowInCustomizationForm="True" VisibleIndex="1">
                                    <DataItemTemplate>
                                        <dx:ASPxButton ID="btnView" runat="server" Text="Verify" RenderMode="Button" Font-Bold="False" BackColor="Transparent" ForeColor="Black" HoverStyle-BackColor="White" 
                                            HoverStyle-BorderRight-BorderWidth="1px" HoverStyle-BorderRight-BorderColor="#727272" HoverStyle-BorderRight-BorderStyle="Dotted"
                                            HoverStyle-BorderBottom-BorderWidth="1px" HoverStyle-BorderBottom-BorderColor="#727272" HoverStyle-BorderBottom-BorderStyle="Dotted"
                                            OnClick="btnView_Click">
                                            <Image ToolTip="Report" Url="~/Content/Icons/verify.svg" Height="24px" Width="24px" />
                                        </dx:ASPxButton>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Right">
                                    </CellStyle>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="TodayTeamActionGoal" ShowInCustomizationForm="True" VisibleIndex="2" Width="0px">
                                    <BatchEditModifiedCellStyle VerticalAlign="Middle">
                                    </BatchEditModifiedCellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="TodayTouchAndCall" ShowInCustomizationForm="True" VisibleIndex="4" Width="0px">
                                    <BatchEditModifiedCellStyle VerticalAlign="Middle">
                                    </BatchEditModifiedCellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ConductedBy" ShowInCustomizationForm="True" VisibleIndex="5" Width="0px">
                                    <BatchEditModifiedCellStyle VerticalAlign="Middle">
                                    </BatchEditModifiedCellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="StatusText" Caption="Status" ShowInCustomizationForm="True" VisibleIndex="6" Width="0px">
                                    <BatchEditModifiedCellStyle VerticalAlign="Middle">
                                    </BatchEditModifiedCellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Styles>
                                <Row BackColor="#D4D4D4">
                                </Row>
                                <AlternatingRow BackColor="#D4D4D4">
                                </AlternatingRow>
                            </Styles>
                            <Paddings PaddingLeft="0px" PaddingRight="10px" PaddingTop="0px" />
                        </dx:ASPxGridView>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="5px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="5px" />
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>




<%--    <dx:ASPxGridView ID="gvTBM" ClientInstanceName="gridView" runat="server" CssClass="grid-view"
                        KeyFieldName="Key" EnablePagingGestures="False" EnableRowsCache="False" AutoGenerateColumns="False">
        <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowHideDataCellsByColumnMinWidth="True" AllowOnlyOneAdaptiveDetailExpanded="True">
        </SettingsAdaptivity>
        <Settings ShowHeaderFilterButton="True" />
        <SettingsBehavior AllowEllipsisInText="true" AllowDragDrop="false" AllowSelectSingleRowOnly="True" />
        <SettingsPopup>
        <FilterControl AutoUpdatePosition="False"></FilterControl>
        </SettingsPopup>
        <SettingsSearchPanel GroupOperator="Or" Visible="True" />
        <Columns>
            <dx:GridViewDataTextColumn VisibleIndex="0" Caption="Meeting Date" AdaptivePriority="1">
                <DataItemTemplate>
                    <dx:ASPxButton ID="btnDownload" runat="server" Text='<%# Eval("MeetingDate") %>' RenderMode="Link" OnClick="btnDownload_Click"></dx:ASPxButton>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="TopicDiscussed" Caption="Topic Discussed" ShowInCustomizationForm="True" VisibleIndex="1" AdaptivePriority="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="StatusText" Caption="Status" ShowInCustomizationForm="True" VisibleIndex="5" AdaptivePriority="1">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>--%>
</asp:Content>