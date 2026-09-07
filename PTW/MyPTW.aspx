<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="MyPTW.aspx.cs" Inherits="HEA.ePTW.PTW.MyPTW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/ePTW.css") %>' />   
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/ePTW_PTW.js") %>'></script>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PageContent" runat="server">
    <dx:ASPxFormLayout runat="server" ID="flToolboxMeeting" CssClass="formLayout" ShowItemCaptionColon="False" RequiredMark="" Width="98%">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="650" />
        <Items>
            <dx:LayoutItem BackColor="#494949" ColSpan="1" ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxLabel ID="lblTitle" runat="server" Font-Bold="True" Font-Size="14pt" ForeColor="White" Text="Permit To Work List">
                        </dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
            </dx:LayoutItem>
            <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                       <dx:ASPxGridView ID="gvPTW" ClientInstanceName="gridView" runat="server" CssClass="grid-view"
                                            KeyFieldName="Key" EnablePagingGestures="False" EnableRowsCache="False" AutoGenerateColumns="False" Width="100%">
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
                                            <td class="value" style="vertical-align: top; padding-top: 8px; padding-right: 5px;" colspan="4">
                                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text='<%# Eval("Description") %>' />
                                            </td>
                                        </tr>
                                        <tr> 
                                            <td class="value" style="vertical-align: top; padding-top: 5px; padding-right: 5px;" colspan="1">
                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text='<%# Eval("RequestName") %>' />
                                            </td>
                                            <td class="value" style="vertical-align: top; padding-top: 5px; padding-right: 5px;" colspan="1">
                                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text='<%# Eval("RequestPosition") %>' />
                                            </td>
                                            <td class="value" style="vertical-align: top; padding-top: 5px; padding-right: 5px;" colspan="2">
                                                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text='<%# Eval("RequestCompany") %>' />
                                            </td>
                                        </tr>
                                        <tr> 
                                            <td class="value" style="vertical-align: top; padding-top: 8px; padding-right: 5px; text-align: right;" colspan="4">
                                                <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text='<%# Eval("StatusText") %>' Font-Bold="True"/>
                                            </td>
                                        </tr>
                                    </table>
                                </PreviewRow>
                            </Templates>
                            <Columns>
                                <dx:GridViewDataDateColumn FieldName="DateFrom" ShowInCustomizationForm="True" VisibleIndex="0" Width="110px">
                                    <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                                    </PropertiesDateEdit>
                                    <CellStyle Font-Bold="False">
                                    </CellStyle>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn FieldName="DateTo" ShowInCustomizationForm="True" VisibleIndex="1" Width="110px">
                                    <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                                    </PropertiesDateEdit>
                                    <CellStyle Font-Bold="False">
                                    </CellStyle>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="WorkType" ShowInCustomizationForm="True" VisibleIndex="2">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn ShowInCustomizationForm="True" VisibleIndex="7">
                                    <DataItemTemplate>
                                        <dx:ASPxButton ID="btnReport" runat="server" Text="Report" RenderMode="Button" Font-Bold="False" BackColor="Transparent" ForeColor="Black" HoverStyle-BackColor="White" 
                                            HoverStyle-BorderRight-BorderWidth="1px" HoverStyle-BorderRight-BorderColor="#727272" HoverStyle-BorderRight-BorderStyle="Dotted"
                                            HoverStyle-BorderBottom-BorderWidth="1px" HoverStyle-BorderBottom-BorderColor="#727272" HoverStyle-BorderBottom-BorderStyle="Dotted"
                                            OnClick="btnReport_Click">
                                            <Image ToolTip="Report" Url="~/Content/Icons/report.svg" Height="20px" Width="20px" />
                                        </dx:ASPxButton>
                                        <dx:ASPxButton ID="btnView" runat="server" Text="Details" RenderMode="Button" Font-Bold="False" BackColor="Transparent" ForeColor="Black" HoverStyle-BackColor="White" 
                                            HoverStyle-BorderRight-BorderWidth="1px" HoverStyle-BorderRight-BorderColor="#727272" HoverStyle-BorderRight-BorderStyle="Dotted"
                                            HoverStyle-BorderBottom-BorderWidth="1px" HoverStyle-BorderBottom-BorderColor="#727272" HoverStyle-BorderBottom-BorderStyle="Dotted"
                                            OnClick="btnView_Click">
                                            <Image ToolTip="Report" Url="~/Content/Icons/read.svg" Height="20px" Width="20px" />
                                        </dx:ASPxButton>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Right">
                                    </CellStyle>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="Description" ShowInCustomizationForm="True" Width="0px" VisibleIndex="3">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="RequestName" ShowInCustomizationForm="True" Width="0px" VisibleIndex="4">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="RequestPosition" ShowInCustomizationForm="True" Width="0px" VisibleIndex="5">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="RequestCompany" ShowInCustomizationForm="True" Width="0px" VisibleIndex="6">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="StatusText" ShowInCustomizationForm="True" Width="0px" VisibleIndex="6">
                                </dx:GridViewDataTextColumn>
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
</asp:Content>
