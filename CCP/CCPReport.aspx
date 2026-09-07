<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="CCPReport.aspx.cs" Inherits="HEA.ePTW.CCP.CCPReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/ePTW_Report.css") %>' />    
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PageContent" runat="server">
    <dx:ASPxFormLayout runat="server" ID="flCCP" CssClass="formLayout" ShowItemCaptionColon="False" RequiredMark="" Width="98%" ColCount="3" ColumnCount="3">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="650" />
        <Items>
            <dx:LayoutItem BackColor="#494949" ColSpan="3" ShowCaption="False" ColumnSpan="3">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxLabel ID="lblTitle" runat="server" Font-Bold="True" Font-Size="14pt" ForeColor="White" Text="CCP Project Monitoring">
                        </dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Date From" ColSpan="1" Height="30px" VerticalAlign="Middle">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxDateEdit ID="dtDateFrom" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" UseMaskBehavior="True" Width="250px">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Please select the Date" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                <RequiredField ErrorText="Please select the Date" IsRequired="True" />
                            </ValidationSettings>
                            <InvalidStyle BackColor="#FFE6EE">
                            </InvalidStyle>
                        </dx:ASPxDateEdit>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Date To" ColSpan="1" Height="30px" VerticalAlign="Middle">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxDateEdit ID="dtDateTo" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" UseMaskBehavior="True" Width="250px">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Please select the Date" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                <RequiredField ErrorText="Please select the Date" IsRequired="True" />
                            </ValidationSettings>
                            <InvalidStyle BackColor="#FFE6EE">
                            </InvalidStyle>
                        </dx:ASPxDateEdit>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
            </dx:LayoutItem>
            <dx:LayoutItem Caption="" ColSpan="1" HorizontalAlign="Left" ShowCaption="False" Height="30px" VerticalAlign="Middle">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="Refresh" OnClick="btnSearch_Click">
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="5px" PaddingLeft="16px" PaddingTop="5px" />
            </dx:LayoutItem>
            <dx:EmptyLayoutItem ColSpan="3" Height="10px" ColumnSpan="3">
            </dx:EmptyLayoutItem>
            <dx:LayoutItem ColSpan="3" ShowCaption="False" Width="100%" ColumnSpan="3">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxGridView ID="gvCCP" ClientInstanceName="gridView" runat="server" CssClass="grid-view" Width="98%"
                                EnablePagingGestures="False" EnableRowsCache="False" AutoGenerateColumns="False">
                            <Settings ShowGroupPanel="True" />
                            <%--<Border BorderColor="#D4D4D4" BorderStyle="Dotted" BorderWidth="1px" />--%>
                            <Settings ShowFilterRow="true" />
                            <SettingsBehavior EnableCustomizationWindow="true" />
                            <Settings HorizontalScrollBarMode="Auto" GridLines="None" />
                            <SettingsBehavior AllowEllipsisInText="true" />
                            <SettingsPopup>
                            <FilterControl AutoUpdatePosition="False"></FilterControl>
                            </SettingsPopup>
                            <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" />
                            <SettingsText EmptyDataRow=" " />
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="ProjectName" ShowInCustomizationForm="True" VisibleIndex="0" Width="250px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="RegistrationNo" ShowInCustomizationForm="True" VisibleIndex="1"  Width="250px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="EquipmentName" ShowInCustomizationForm="True" VisibleIndex="2" Width="250px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Description" ShowInCustomizationForm="True" VisibleIndex="3" Width="250px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn Caption="Entrance Barricade" FieldName="CCP01" ShowInCustomizationForm="True" VisibleIndex="4" Visible="false" Width="150px">
                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                    </PropertiesDateEdit>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn Caption="Life lines &amp; Safety Hook" FieldName="CCP02" ShowInCustomizationForm="True" VisibleIndex="5" Visible="false" Width="150px">
                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                    </PropertiesDateEdit>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn Caption="Hoisting of Guiderails" FieldName="CCP03" ShowInCustomizationForm="True" VisibleIndex="6" Visible="false" Width="150px">
                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                    </PropertiesDateEdit>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn Caption="Setting up of Traction Machine" FieldName="CCP04" ShowInCustomizationForm="True" VisibleIndex="7" Visible="false" Width="150px">
                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn Caption="Hoisting of Control Panel" FieldName="CCP05" ShowInCustomizationForm="True" VisibleIndex="8" Visible="false" Width="150px">
                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                    </PropertiesDateEdit>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn Caption="Hoisting of Cage Platform" FieldName="CCP06" ShowInCustomizationForm="True" VisibleIndex="9" Visible="false" Width="150px">
                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                    </PropertiesDateEdit>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn Caption="Hoisting &amp; securing of Cwt Frame" FieldName="CCP07" ShowInCustomizationForm="True" VisibleIndex="10" Visible="false" Width="150px">
                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                    </PropertiesDateEdit>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataDateColumn>
                            </Columns>
                            <Toolbars>
                                <dx:GridViewToolbar>
                                    <SettingsAdaptivity Enabled="true" EnableCollapseRootItemsToIcons="true" />
                                    <Items>
                                        <dx:GridViewToolbarItem Text="Export to" Image-IconID="actions_download_16x16office2013" BeginGroup="true" AdaptivePriority="1">
                                            <Items>
                                                <dx:GridViewToolbarItem Command="ExportToPdf" />
                                                <dx:GridViewToolbarItem Command="ExportToCsv" />
                                                <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Export to XLSX" />
                                            </Items>
                                            <Image IconID="actions_download_16x16office2013"></Image>
                                        </dx:GridViewToolbarItem>
                                        <dx:GridViewToolbarItem Command="ShowCustomizationWindow" />
                                    </Items>
                                </dx:GridViewToolbar>
                            </Toolbars>
                            <StylesToolbar>
                                <Style VerticalAlign="Middle">
                                </Style>
                                <Item VerticalAlign="Middle">
                                </Item>
                            </StylesToolbar>
                        </dx:ASPxGridView>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="0px" PaddingLeft="16px" PaddingRight="0px" PaddingTop="0px" />
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
</asp:Content>
