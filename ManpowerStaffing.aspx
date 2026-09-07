<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="ManpowerStaffing.aspx.cs" Inherits="HEA.ePTW.ManpowerStaffing" %>

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
                    <h1>Personnel List</h1>
                </Template>
            </dx:MenuItem>
            <dx:MenuItem Name="New" Text="New" Alignment="Right" AdaptivePriority="2">
                <Image Url="~/Content/Images/add.svg" />
            </dx:MenuItem>
        </Items>
    </dx:ASPxMenu>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageContent" runat="server">
    <dx:ASPxGridView
        ID="gvStaffs"
        KeyFieldName="UserID"
        EnableRowsCache="False"
        Width="100%"
        runat="server"
        CssClass="grid-view"
        ClientInstanceName="gridView"
        AutoGenerateColumns="False"
        EnableCallbackAnimation="True">

        <SettingsAdaptivity AdaptivityMode="HideDataCells">
        </SettingsAdaptivity>
        <SettingsEditing UseFormLayout="True" Mode="EditForm" />
        <SettingsBehavior AllowFocusedRow="true" AllowSelectByRowClick="true" AllowDragDrop="false" AllowSelectSingleRowOnly="True" />
        <SettingsPopup>
            <FilterControl AutoUpdatePosition="False"></FilterControl>
        </SettingsPopup>
        <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
        <Settings ShowPreview="true" />

        <Columns>
            <dx:GridViewDataBinaryImageColumn FieldName="Photo" VisibleIndex="0" Width="60px">
                <PropertiesBinaryImage ImageHeight="36px" ImageWidth="36px" ShowLoadingImage="True">
                    <EditingSettings Enabled="True" UploadSettings-UploadValidationSettings-MaxFileSize="4194304">
                        <UploadSettings>
                            <UploadValidationSettings MaxFileSize="4194304"></UploadValidationSettings>
                        </UploadSettings>
                    </EditingSettings>
                </PropertiesBinaryImage>
            </dx:GridViewDataBinaryImageColumn>
            <dx:GridViewDataTextColumn FieldName="FullName" VisibleIndex="1">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn FieldName="Title" VisibleIndex="2" Visible="False">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Text="Mr" Value="Mr" />
                        <dx:ListEditItem Text="Mrs" Value="Mrs" />
                        <dx:ListEditItem Text="Miss" Value="Miss" />
                        <dx:ListEditItem Text="Ms" Value="Ms" />
                    </Items>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataTextColumn FieldName="UserName" VisibleIndex="3" Visible="False">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn FieldName="DocumentType" VisibleIndex="4" Visible="False">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Text="NRIC" Value="NRIC" />
                        <dx:ListEditItem Text="FIN" Value="FIN" />
                        <dx:ListEditItem Text="WP" Value="WP" />
                    </Items>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataTextColumn FieldName="DocumentNo" VisibleIndex="5" Visible="False">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ContactNo" VisibleIndex="6">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="EmailAddress" VisibleIndex="7">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn FieldName="ConstructorID" VisibleIndex="8" Visible="False">
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataTextColumn FieldName="Position" VisibleIndex="9">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="UserID" VisibleIndex="10" Visible="False">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn FieldName="Status" CellStyle-HorizontalAlign="Center" Width="40" VisibleIndex="11">
                <EditFormSettings Visible="False" />
                <DataItemTemplate>
                    <dx:ASPxImage runat="server" CssClass='<%# string.Format("column-status status{0}", Eval("[Status]")) %>' />
                </DataItemTemplate>
                <CellStyle HorizontalAlign="Center"></CellStyle>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewCommandColumn ButtonRenderMode="Button" ButtonType="Button" VisibleIndex="12" ShowInCustomizationForm="False" Caption="Action">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="Download" Text="Download">
                        <Image Height="16px" Url="~/Content/Icons/download.svg" Width="16px">
                        </Image>
                        <Styles>
                            <Style BackColor="White" ForeColor="Black" Wrap="False">
                                <HoverStyle BackColor="#CEE7FF" >
                                </HoverStyle >
                            </Style>
                            <FocusRectStyle BackColor="Black">
                                <HoverStyle BackColor="#ECD9FF">
                                </HoverStyle>
                            </FocusRectStyle>
                        </Styles>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="Documents" Text="Documents">
                        <Image Height="16px" Url="~/Content/Icons/staffdocument.svg" Width="16px">
                        </Image>
                        <Styles>
                            <Style BackColor="White" ForeColor="Black" Wrap="False">
                                <HoverStyle BackColor="#CEE7FF" >
                                </HoverStyle >
                            </Style>
                            <FocusRectStyle BackColor="Black">
                                <HoverStyle BackColor="#ECD9FF">
                                </HoverStyle>
                            </FocusRectStyle>
                        </Styles>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>

        <Templates>
            <PreviewRow>
                <table>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="lblCreated" runat="server" Text='<%# Eval("Created") %>' />
                        </td>
                        <td>
                            <asp:Button ID="test1" runat="server" CommandName="Edit" Text="Edit" />
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" CommandName="Delete" Text="Delete" />
                        </td>
                    </tr>
                </table>
            </PreviewRow>
        </Templates>

    </dx:ASPxGridView>
</asp:Content>
