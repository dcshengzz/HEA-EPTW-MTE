<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="EquipmentSetting.aspx.cs" Inherits="HEA.ePTW.EquipmentSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/ePTW_Gridview.css") %>' />
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/ePTW_Gridview.js") %>'></script>
    <style type="text/css">
        .dxflGroupCell_Office365 {
            padding: 0 0px;
        }
        .dxflGroup_Office365 {
            padding: 0px 0;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageContent" runat="server">

    <dx:ASPxFormLayout runat="server" ID="flMain" CssClass="formLayout" ShowItemCaptionColon="False" Width="98%">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="650" />
        <Items>
            <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="#494949">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxLabel ID="lblTitle" runat="server" Font-Bold="True" Font-Size="14pt" Text="Equipment List" ForeColor="White">
                        </dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
            </dx:LayoutItem>
            <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="White">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxMenu runat="server" ID="PageToolbar" ClientInstanceName="pageToolbar"
                            ItemAutoWidth="false" ApplyItemStyleToTemplates="true" ItemWrap="false"
                            AllowSelectItem="false" SeparatorWidth="0"
                            Width="100%" CssClass="page-toolbar">
                            <ClientSideEvents ItemClick="onPageToolbarItemClick" />
                            <SettingsAdaptivity Enabled="true" EnableAutoHideRootItems="true" />
                            <ItemStyle CssClass="item" VerticalAlign="Middle" />
                            <ItemImage Width="16px" Height="16px" />
                            <Items>
                                <dx:MenuItem>
                                    <Template>    
                                        <%--<h1>Equipment List</h1>--%>
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
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="0px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="10px" />
            </dx:LayoutItem>
            <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="White">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxGridView
                            ID="gvEquipment" ClientInstanceName="gridView" runat="server" CssClass="grid-view" AutoGenerateColumns="False"
                            KeyFieldName="RegistrationNo" EnableRowsCache="False" EnableCallbackAnimation="True" Width="100%"
                            OnCellEditorInitialize="gvEquipment_CellEditorInitialize" 
                            OnRowValidating="gvEquipment_RowValidating" 
                            OnRowInserting="gvEquipment_RowInserting" 
                            OnRowUpdating="gvEquipment_RowUpdating">

                            <Settings ShowColumnHeaders="True"></Settings>
                            <SettingsPager Mode="ShowAllRecords" />
                            <SettingsEditing UseFormLayout="True" Mode="PopupEditForm" />
                            <Settings GridLines="Both" ShowGroupPanel="false" ShowFilterRow="false" ShowHeaderFilterButton="True" />
                            <SettingsBehavior AllowEllipsisInText="true" AllowFocusedRow="true" AllowSelectByRowClick="true" AllowDragDrop="false" AllowSelectSingleRowOnly="True" />
                            <SettingsSearchPanel GroupOperator="Or" Visible="True" />
                            <SettingsPopup>
                                <EditForm HorizontalAlign="Center" Modal="True" PopupAnimationType="Auto" VerticalAlign="WindowCenter">
                                </EditForm>
                                <FilterControl AutoUpdatePosition="False"></FilterControl>
                            </SettingsPopup>

                            <EditFormLayoutProperties ColCount="1" ColumnCount="1" ShowItemCaptionColon="False" AlignItemCaptionsInAllGroups="True">
                                <Items>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Registration No">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Equipment Type">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Equipment Name">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Description">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Company ID" Visible="False">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Project ID" Visible="False">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewLayoutGroup ColCount="3" ColSpan="1" ColumnCount="3" GroupBoxDecoration="None" ShowCaption="False">
                                        <Items>
                                            <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Photo">
                                                <CaptionSettings Location="Top" />
                                            </dx:GridViewColumnLayoutItem>
                                            <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Document">
                                                <CaptionSettings Location="Top" />
                                            </dx:GridViewColumnLayoutItem>
                                            <dx:EmptyLayoutItem ColSpan="1" Width="100%">
                                            </dx:EmptyLayoutItem>
                                        </Items>
                                    </dx:GridViewLayoutGroup>
                                    <dx:EmptyLayoutItem ColSpan="1">
                                    </dx:EmptyLayoutItem>
                                    <dx:EditModeCommandLayoutItem ColSpan="1" HorizontalAlign="Right">
                                    </dx:EditModeCommandLayoutItem>
                                </Items>
                            </EditFormLayoutProperties>
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="RegistrationNo" VisibleIndex="2" AdaptivePriority="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="EquipmentType" VisibleIndex="3" AdaptivePriority="2">
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn FieldName="EquipmentName" VisibleIndex="4" AdaptivePriority="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="5" AdaptivePriority="2" >
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="CompanyID" Visible="False" VisibleIndex="6">
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="ProjectID" Visible="False" VisibleIndex="7">
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataBinaryImageColumn FieldName="Photo" Width="100px" VisibleIndex="0" AdaptivePriority="2" Visible="false">
                                    <PropertiesBinaryImage ImageHeight="36px" ImageWidth="36px" ShowLoadingImage="True">
                                        <EditingSettings Enabled="True" UploadSettings-UploadValidationSettings-MaxFileSize="4194304">
                                            <UploadSettings>
                                                <UploadValidationSettings MaxFileSize="4194304"></UploadValidationSettings>
                                            </UploadSettings>
                                        </EditingSettings>
                                    </PropertiesBinaryImage>
                                    <Settings AllowFilterBySearchPanel="False" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataBinaryImageColumn>
                                <dx:GridViewDataBinaryImageColumn FieldName="Document" Width="100px" VisibleIndex="1" AdaptivePriority="2" Visible="false">
                                    <PropertiesBinaryImage ImageHeight="36px" ImageWidth="36px" ShowLoadingImage="True">
                                        <EditingSettings Enabled="True" UploadSettings-UploadValidationSettings-MaxFileSize="4194304">
                                            <UploadSettings>
                                                <UploadValidationSettings MaxFileSize="4194304"></UploadValidationSettings>
                                            </UploadSettings>
                                        </EditingSettings>
                                    </PropertiesBinaryImage>
                                    <Settings AllowFilterBySearchPanel="False" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataBinaryImageColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="StatusText" Caption="Status" VisibleIndex="8" Width="80px">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataComboBoxColumn>
                            </Columns>
                            <Styles>
                                <Header Font-Bold="True" BackColor="#454545" ForeColor="White">
                                </Header>
                                <FocusedRow BackColor="#D5D5FF" Font-Bold="False" ForeColor="Black">
                                </FocusedRow>
                                <EditForm VerticalAlign="Middle">
                                </EditForm>
                            </Styles>
                        </dx:ASPxGridView>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="0px" />
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>



</asp:Content>
