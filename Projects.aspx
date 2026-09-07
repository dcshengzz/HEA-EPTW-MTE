<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="HEA.ePTW.Projects" %>

<asp:Content runat="server" ContentPlaceHolderID="Head">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/ePTW.css") %>' />
    <%--<link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/ePTW_Standard.css") %>' />--%>
    <%--<link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/GridView.css") %>' />--%>
    <%--<script type="text/javascript" src='<%# ResolveUrl("~/Content/GridView.js") %>'></script>--%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PageContent" runat="server">
    <dx:ASPxFormLayout runat="server" ID="formLayout" CssClass="formLayout" ShowItemCaptionColon="False" Width="98%">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="650" />
        <Items>
            <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="#494949">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Size="14pt" Text="Select the Team" ForeColor="White">
                        </dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
            </dx:LayoutItem>
            <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxGridView ID="gvProjects"
                            ClientInstanceName="gridView" runat="server" CssClass="grid-view" KeyFieldName="ID"
                            EnablePagingGestures="True" Width="100%" EnableRowsCache="False" Settings-ShowColumnHeaders="False"
                            AutoGenerateColumns="False"
                            PreviewFieldName="Description"
                            OnCustomButtonCallback="gvProjects_CustomButtonCallback">
                            <Settings GridLines="Both" ShowGroupPanel="false" ShowFilterRow="false" />
                            <Settings ShowColumnHeaders="False"></Settings>
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
                                    </table>
                                </PreviewRow>
                            </Templates>
                            <Settings ShowPreview="true" />
                            <SettingsBehavior AllowEllipsisInText="False" AllowDragDrop="false" />
                            <SettingsPopup>
                                <FilterControl AutoUpdatePosition="False"></FilterControl>
                            </SettingsPopup>
                            <SettingsSearchPanel GroupOperator="Or" ShowClearButton="False" Visible="True" />
                            <Columns>
                                <dx:GridViewDataColumn FieldName="Name" Caption="Building Name" VisibleIndex="0" AdaptivePriority="1">
                                    <CellStyle Font-Bold="True" Font-Size="12pt">
                                    </CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewCommandColumn ButtonRenderMode="Button" ButtonType="Button" AdaptivePriority="1" Width="180px" VisibleIndex="1">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="Select" Text="Click To Select">
                                            <Image ToolTip="Details" Url="~/Content/Icons/details.svg" Height="20px" Width="20px" />
                                            <Styles>
<%--                                                <Style BackColor="Transparent" ForeColor="Black" Font-Size="12pt">
                                                    <HoverStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Underline="False" ForeColor="Black" >
                                                    <BorderRight BorderColor="#727272" BorderStyle="Dotted" BorderWidth="1px" / >
                                                    <BorderBottom BorderColor="#727272" BorderStyle="Dotted" BorderWidth="1px" / >
                                                    </HoverStyle >
                                                </Style>--%>
                                                <Style BackColor="Transparent" ForeColor="Black" Font-Size="12pt">
                                                    <HoverStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Underline="False" ForeColor="Black">
                                                    <BorderRight BorderColor="#727272" BorderStyle="Dotted" BorderWidth="1px" />
                                                    <BorderBottom BorderColor="#727272" BorderStyle="Dotted" BorderWidth="1px" />
                                                    </HoverStyle>
                                                </Style>
                                                
                                                <FocusRectStyle BackColor="#E2E2E2">
                                                    <HoverStyle BackColor="#D9D9D9">
                                                        <BorderBottom BorderColor="Black" BorderStyle="Double" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Border BorderColor="#F1F1F1" BorderStyle="Solid" BorderWidth="1px" />
                                                </FocusRectStyle>
                                            </Styles>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                    <CellStyle VerticalAlign="Middle">
                                    </CellStyle>
                                </dx:GridViewCommandColumn>
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
                <Paddings PaddingBottom="5px" PaddingLeft="5px" PaddingRight="8px" PaddingTop="5px" />
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
</asp:Content>
