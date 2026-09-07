<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="TBMReportAll.aspx.cs" Inherits="HEA.ePTW.Settings.TBMReportAll" %>
<%@ Register Assembly="DevExpress.XtraCharts.v23.2.Web, Version=23.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Dashboard.v23.2.Web.WebForms, Version=23.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.DashboardWeb" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v23.2, Version=23.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>

<asp:Content ID="Content5" ContentPlaceHolderID="PageContent" runat="server">
    <dx:ASPxFormLayout runat="server" ID="formLayout" CssClass="formLayout" ShowItemCaptionColon="False" Width="98%">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="650" />
        <Items>
            <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="#494949">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxLabel ID="lblTitle" runat="server" Font-Bold="True" Font-Size="14pt" Text="TBM Report" ForeColor="White">
                        </dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
            </dx:LayoutItem>
            <dx:LayoutItem Caption="" ColSpan="1" ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                            <table class="templateTable">
                                <tr>
                                    <td>
                                        <dx:ASPxGridView ID="gvDetails" runat="server" Width="99%" AutoGenerateColumns="False">
                                            <Settings ShowGroupPanel="true" ShowFooter="true" ShowGroupFooter="VisibleIfExpanded" />
                                            <SettingsPager Mode="ShowAllRecords" />
                                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="550" ShowHeaderFilterButton="True" />
                                            <SettingsBehavior MergeGroupsMode="Always" AutoExpandAllGroups="true" />
                                            <SettingsPopup>
                                                <FilterControl AutoUpdatePosition="False">
                                                </FilterControl>
                                            </SettingsPopup>
                                            <SettingsExport EnableClientSideExportAPI="True" ExcelExportMode="WYSIWYG">
                                            </SettingsExport>
                                            <Columns>
                                                <dx:GridViewDataTextColumn Caption="Project Name" FieldName="ProjectName" ShowInCustomizationForm="True" VisibleIndex="1">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataDateColumn FieldName="RequestDate" Caption="Applied Date" ShowInCustomizationForm="True" SortIndex="0" SortOrder="Descending" VisibleIndex="0">
                                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                                    </PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>
                                                <dx:GridViewDataTextColumn Caption="Company Name" FieldName="RequestCompany" ShowInCustomizationForm="True" VisibleIndex="1">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="RequestBy" Caption="Applicant Name" ShowInCustomizationForm="True" VisibleIndex="2">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="RequestPosition" Caption="Job Designation" ShowInCustomizationForm="True" VisibleIndex="3">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="MfgNo" Caption="MFG No" ShowInCustomizationForm="True" VisibleIndex="3">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Session" FieldName="TBMSession" ShowInCustomizationForm="True" VisibleIndex="4">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Toolbars>
                                                <dx:GridViewToolbar>
                                                    <Items>
                                                        <dx:GridViewToolbarItem Command="ExportToXlsx">
                                                        </dx:GridViewToolbarItem>
                                                    </Items>
                                                </dx:GridViewToolbar>
                                            </Toolbars>
                                            <TotalSummary>
                                                <dx:ASPxSummaryItem FieldName="RequestBy" SummaryType="Count" />
                                            </TotalSummary>
                                            <GroupSummary>
                                                <dx:ASPxSummaryItem FieldName="RequestBy" ShowInGroupFooterColumn="RequestBy" SummaryType="Count" />
                                            </GroupSummary>
                                            <Styles>
                                                <Header Font-Bold="True" BackColor="#454545" ForeColor="White">
                                                </Header>
                                                <FocusedRow BackColor="#D5D5FF" Font-Bold="False" ForeColor="Black">
                                                </FocusedRow>
                                                <EditForm VerticalAlign="Middle">
                                                </EditForm>
                                            </Styles>
                                        </dx:ASPxGridView>

                                    </td>
                                </tr>
                            </table>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="8px" PaddingTop="8px" />
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
</asp:Content>