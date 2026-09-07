<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="ProjectOverview.aspx.cs" Inherits="HEA.ePTW.ProjectOverview" %>
<%@ Register Assembly="DevExpress.XtraCharts.v23.2.Web, Version=23.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Dashboard.v23.2.Web.WebForms, Version=23.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.DashboardWeb" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v23.2, Version=23.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/ePTW.css") %>' />
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PageContent" runat="server">
    <dx:ASPxFormLayout runat="server" ID="formLayout" CssClass="formLayout" ShowItemCaptionColon="False" Width="98%">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="650" />
        <Items>
            <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="#494949">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxLabel ID="lblTitle" runat="server" Font-Bold="True" Font-Size="14pt" Text="Overview" ForeColor="White">
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
                                    <td class="imageCell" rowspan="1">
                                        <dx:ASPxBinaryImage ID="ImgProject" runat="server" Height="320px" Width="480px" Border-BorderColor="Black" Border-BorderStyle="Solid" Border-BorderWidth="1" >
                                        <Border BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></Border>
                                        </dx:ASPxBinaryImage>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="value" style="vertical-align: top; padding-top: 10px; padding-bottom: 5px;" colspan="1">
                                        <dx:ASPxLabel ID="lblProject" runat="server" Text="" Font-Bold="True" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="value" style="vertical-align: top; padding-top: 10px; padding-bottom: 5px;" colspan="1">
                                        <dx:ASPxLabel ID="lblLocation" runat="server" Text="" Font-Bold="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="value" style="vertical-align: top; padding-top: 15px; padding-bottom: 10px;" colspan="1">
                                        <dx:ASPxLabel ID="lblDescription" runat="server" Text="" Font-Bold="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="value" style="vertical-align: top; padding-top: 5px; padding-bottom: 5px;" colspan="1">
                                        <dx:ASPxLabel ID="lblMainCons" runat="server" Text="" Font-Bold="True" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="value" style="vertical-align: top; padding-top: 5px; padding-bottom: 5px;" colspan="1">
                                        <dx:ASPxLabel ID="lblContact" runat="server" Text="" Font-Bold="False" />
                                    </td>
                                </tr>
                                <tr> 
                                    <td class="imageCell" rowspan="1">
                                        <dx:WebChartControl ID="WebChartPTW" runat="server" ClientInstanceName="WebChartPTW" CrosshairEnabled="True" Height="300px" Width="600px">
                                            <BorderOptions Color="Black" Visibility="True" />
                                            <DiagramSerializable>
                                                <dx:SimpleDiagram3D LabelsResolveOverlappingMinIndent="3" RotationType="UseMouseStandard" RotationMatrixSerializable="1;0;0;0;0;0.819152044288992;-0.573576436351046;0;0;0.573576436351046;0.819152044288992;0;0;0;0;1">
                                                </dx:SimpleDiagram3D>
                                            </DiagramSerializable>
                                            <Legend LegendID="-1" DXFont="Arial, 8pt"></Legend>
                                            <SeriesSerializable>
                                                <dx:Series ArgumentDataMember="Status" LegendTextPattern="{A} ({V})" Name="Status" SeriesID="0" ValueDataMembersSerializable="Count">
                                                    <ViewSerializable>
                                                        <dx:Doughnut3DSeriesView>
                                                        </dx:Doughnut3DSeriesView>
                                                    </ViewSerializable>
                                                    <LabelSerializable>
                                                        <dx:Doughnut3DSeriesLabel ColumnIndent="20" DXFont="Tahoma, 8pt, style=Bold" Position="Radial" TextPattern="{VP:P0} ({V})">
                                                        </dx:Doughnut3DSeriesLabel>
                                                    </LabelSerializable>
                                                </dx:Series>
                                            </SeriesSerializable>
                                            <Titles>
                                                <dx:ChartTitle DXFont="Arial, 12pt" Text="Permit To Work" TextColor="Black" TitleID="0" Alignment="Near" />
                                                <dx:ChartTitle Alignment="Far" Dock="Bottom" DXFont="Tahoma, 8pt" Text="" TextColor="Gray" TitleID="1" />
                                            </Titles>
                                        </dx:WebChartControl>
                                    </td>
                                </tr>
                                <tr> 
                                    <td class="imageCell" rowspan="1">
                                        <dx:WebChartControl ID="WebChartTBM" runat="server" ClientInstanceName="WebChartTBM" CrosshairEnabled="True" Height="300px" Width="600px">
                                    <BorderOptions Color="Black" Visibility="True" />
                                    <DiagramSerializable>
                                        <dx:SimpleDiagram3D LabelsResolveOverlappingMinIndent="3" RotationType="UseMouseStandard" RotationMatrixSerializable="1;0;0;0;0;0.819152044288992;-0.573576436351046;0;0;0.573576436351046;0.819152044288992;0;0;0;0;1">
                                        </dx:SimpleDiagram3D>
                                    </DiagramSerializable>
                                    <Legend LegendID="-1" DXFont="Arial, 8pt"></Legend>
                                    <SeriesSerializable>
                                        <dx:Series ArgumentDataMember="Status" LegendTextPattern="{A} ({V})" Name="Status" SeriesID="0" ValueDataMembersSerializable="Count">
                                            <ViewSerializable>
                                                <dx:Doughnut3DSeriesView>
                                                </dx:Doughnut3DSeriesView>
                                            </ViewSerializable>
                                            <LabelSerializable>
                                                <dx:Doughnut3DSeriesLabel ColumnIndent="20" DXFont="Tahoma, 8pt, style=Bold" Position="Radial" TextPattern="{VP:P0} ({V})">
                                                </dx:Doughnut3DSeriesLabel>
                                            </LabelSerializable>
                                        </dx:Series>
                                    </SeriesSerializable>
                                    <Titles>
                                        <dx:ChartTitle DXFont="Arial, 12pt" Text="Toobox Meeting" TextColor="Black" TitleID="0" Alignment="Near" />
                                        <dx:ChartTitle Alignment="Far" Dock="Bottom" DXFont="Tahoma, 8pt" Text="" TextColor="Gray" TitleID="1" />
                                    </Titles>
                                </dx:WebChartControl>
                                    </td>
                                </tr>
                                <tr> 
                                    <td class="auto-style1" rowspan="1">
                                        <dx:WebChartControl ID="WebChartCCP" runat="server" ClientInstanceName="WebChartCCP" CrosshairEnabled="True" Height="300px" Width="600px">
                                    <BorderOptions Color="Black" Visibility="True" />
                                    <DiagramSerializable>
                                        <dx:SimpleDiagram3D LabelsResolveOverlappingMinIndent="3" RotationType="UseMouseStandard" RotationMatrixSerializable="1;0;0;0;0;0.819152044288992;-0.573576436351046;0;0;0.573576436351046;0.819152044288992;0;0;0;0;1">
                                        </dx:SimpleDiagram3D>
                                    </DiagramSerializable>
                                    <Legend LegendID="-1"></Legend>
                                    <SeriesSerializable>
                                        <dx:Series ArgumentDataMember="Status" LegendTextPattern="{A} ({V})" Name="Status" SeriesID="0" ValueDataMembersSerializable="Count">
                                            <ViewSerializable>
                                                <dx:Doughnut3DSeriesView>
                                                </dx:Doughnut3DSeriesView>
                                            </ViewSerializable>
                                            <LabelSerializable>
                                                <dx:Doughnut3DSeriesLabel ColumnIndent="20" DXFont="Tahoma, 8pt, style=Bold" Position="Radial" TextPattern="{VP:P0} ({V})">
                                                </dx:Doughnut3DSeriesLabel>
                                            </LabelSerializable>
                                        </dx:Series>
                                    </SeriesSerializable>
                                    <Titles>
                                        <dx:ChartTitle DXFont="Arial, 12pt" Text="Compliance Check Point" TextColor="Black" TitleID="0" Alignment="Near" />
                                        <dx:ChartTitle Alignment="Far" Dock="Bottom" DXFont="Tahoma, 8pt" Text="" TextColor="Gray" TitleID="1" />
                                    </Titles>
                                </dx:WebChartControl>
                                    </td>
                                </tr>
                                <tr> 
                                    <td class="auto-style1" rowspan="1">
                                        <dx:WebChartControl ID="wcManpower" runat="server" ClientInstanceName="chart" Width="850px" Height="450px" ToolTipEnabled="False" RenderFormat="Svg" SeriesDataMember="RequestCompany">

<Legend LegendID="-1"></Legend>

                                            <SeriesTemplate LabelsVisibility="False"
                                                ArgumentDataMember="RequestDate" ValueDataMembersSerializable="Cnt"
                                                CrosshairLabelPattern = "{S} : {V:N2}">
                                                <ViewSerializable>
                                                    <dx:SideBySideBarSeriesView></dx:SideBySideBarSeriesView>
                                                </ViewSerializable>
                                                <LabelSerializable>
                                                    <dx:SideBySideBarSeriesLabel TextPattern="{V:N2}">
                                                    </dx:SideBySideBarSeriesLabel>
                                                </LabelSerializable>
                                            </SeriesTemplate>
                                            <DiagramSerializable>
                                                <dx:XYDiagram>
                                                    <AxisX Title-Text="Days" VisibleInPanesSerializable="-1" StickToEdge="True" Visibility="True" MinorCount="1">
                                                        <Tickmarks MinorVisible="False" Visible="False" />
                                                        <Label textPattern="{A:dd-MMM-yyyy}" Alignment="Center" Angle="90" DXFont="Tahoma, 6pt">
                                                        </Label>
                                                        <DateTimeScaleOptions MeasureUnit="Day" GridAlignment="Day"/>
                                                    </AxisX>
                                                    <AxisY VisibleInPanesSerializable="-1">
                                                        <GridLines MinorVisible="True"></GridLines>
                                                    </AxisY>

                                                </dx:XYDiagram>
                                            </DiagramSerializable>
                                            <BorderOptions Visibility="False" />
                                            <CrosshairOptions>
                                                <CrosshairLabelTextOptions DXFont="Tahoma, 10pt" />
                                                <GroupHeaderTextOptions DXFont="Tahoma, 10pt, style=Bold"/>
                                            </CrosshairOptions>
                                           <Titles>
                                               <dx:ChartTitle Text="Manpower at Site " DXFont="Tahoma, 15pt" Alignment="Center"></dx:ChartTitle>
                                           </Titles>
                                        </dx:WebChartControl>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxGridView ID="gvDetails" runat="server" Width="99%" AutoGenerateColumns="False">
                                            <Settings ShowGroupPanel="true" ShowFooter="true" ShowGroupFooter="VisibleIfExpanded" />
                                            <SettingsPager Mode="ShowAllRecords" />
                                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="300" ShowHeaderFilterButton="True" />
                                            <SettingsBehavior MergeGroupsMode="Always" AutoExpandAllGroups="true" />
                                            <SettingsPopup>
                                                <FilterControl AutoUpdatePosition="False">
                                                </FilterControl>
                                            </SettingsPopup>
                                            <SettingsExport EnableClientSideExportAPI="True" ExcelExportMode="WYSIWYG">
                                            </SettingsExport>
                                            <Columns>
                                                <dx:GridViewDataDateColumn FieldName="RequestDate" Caption="Applied Date" GroupIndex="0" ShowInCustomizationForm="True" SortIndex="0" SortOrder="Descending" VisibleIndex="0">
                                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                                    </PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>
                                                <dx:GridViewDataTextColumn Caption="Company Name" FieldName="RequestCompany" ShowInCustomizationForm="True" VisibleIndex="1">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="RequestBy" Caption="Applicant Name" ShowInCustomizationForm="True" VisibleIndex="2">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="RequestPosition" Caption="Job Designation" ShowInCustomizationForm="True" VisibleIndex="3">
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
