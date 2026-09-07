<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="DocumentManagement.aspx.cs" Inherits="HEA.ePTW.Documents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/ePTW_Standard.css") %>' />
<%--    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/ePTW_Gridview.css") %>' />--%>
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/ePTW_Gridview.js") %>'></script>
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/ePTW_UploadFile.js") %>'></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageContent" runat="server">
    <dx:ASPxFormLayout runat="server" ID="formLayout" CssClass="formLayout" ShowItemCaptionColon="False" Width="98%">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="650" />
        <Items>
            <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="#494949">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Size="14pt" Text="Document List" ForeColor="White">
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
                            </Items>
                        </dx:ASPxMenu>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="0px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="0px" />
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>





    <dx:ASPxGridView ID="gvDocuments" KeyFieldName="ID" EnableRowsCache="False" Width="100%"
                    runat="server" CssClass="grid-view" ClientInstanceName="gridView" AutoGenerateColumns="False"
                    EnableCallbackAnimation="True" 
                    OnCellEditorInitialize="gvDocuments_CellEditorInitialize" OnRowValidating="gvDocuments_RowValidating"
                    OnRowInserting="gvDocuments_RowInserting" OnRowUpdating="gvDocuments_RowUpdating" 
                    OnCancelRowEditing="gvDocuments_CancelRowEditing">
        <SettingsAdaptivity AdaptivityMode="HideDataCells">
        </SettingsAdaptivity>
        <SettingsEditing UseFormLayout="True" Mode="EditForm" />
        <Settings ShowColumnHeaders="False" />
        <SettingsBehavior AllowFocusedRow="true" AllowSelectByRowClick="true" AllowDragDrop="false" AllowSelectSingleRowOnly="True" />
        <SettingsPopup>
            <FilterControl AutoUpdatePosition="False"></FilterControl>
        </SettingsPopup>

        <EditFormLayoutProperties ColCount="1" ColumnCount="1" ShowItemCaptionColon="False" AlignItemCaptionsInAllGroups="True">
            <Items>
                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Document Type">
                </dx:GridViewColumnLayoutItem>
                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Title">
                </dx:GridViewColumnLayoutItem>
                <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Document">
                </dx:GridViewColumnLayoutItem>
                <dx:EmptyLayoutItem ColSpan="1">
                </dx:EmptyLayoutItem>
                <dx:EditModeCommandLayoutItem ColSpan="1" HorizontalAlign="Right">
                </dx:EditModeCommandLayoutItem>
            </Items>
        </EditFormLayoutProperties>
        <Columns>
            <dx:GridViewDataComboBoxColumn FieldName="ProjectID" Visible="False" VisibleIndex="3">
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="4" Visible="False" Caption="Title">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn FieldName="DocumentType" ShowInCustomizationForm="True" VisibleIndex="6">
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataTextColumn Visible ="false" VisibleIndex="1" FieldName="DocumentData" Caption="Document">
                <EditFormSettings Visible="True" />
                <EditItemTemplate>
                    <dx:ASPxUploadControl ID="ASPxUploadControl1" runat="server" AutoStartUpload="true"  
                        UploadMode="Auto" OnFileUploadComplete="ASPxUploadControl1_FileUploadComplete">  
                        <ClientSideEvents FileUploadComplete="OnFileUploadComplete" />  
                    </dx:ASPxUploadControl>  
                    <dx:ASPxLabel ID="lblFileName" runat="server" ClientInstanceName="lblFileName" OnInit="lblFileName_Init"/>  
                    <dx:ASPxButton ID="btnRemove" RenderMode="Link" runat="server" Text="Remove"  
                        ClientVisible="false" ClientInstanceName="btnDeleteFile" AutoPostBack="false">  
                        <ClientSideEvents Click="OnClick" />  
                    </dx:ASPxButton>
                    <p class="note">
                        <dx:ASPxLabel ID="AllowedFileExtensionsLabel" runat="server" Text="Allowed file extensions: .jpg, .jpeg, .gif, .png." Font-Size="8pt">
                        </dx:ASPxLabel>
                        <br />
                        <dx:ASPxLabel ID="MaxFileSizeLabel" runat="server" Text="Maximum file size: 4 MB." Font-Size="8pt">
                        </dx:ASPxLabel>
                    </p>
                </EditItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="FileName" Visible="False" VisibleIndex="0">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="FileType" Visible="False" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Status" Visible="False" VisibleIndex="7">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="Description" Caption="Title">
<%--                <DataItemTemplate>
                    <dx:ASPxButton ID="btnDownload" runat="server" Text='<%# Eval("Description") %>' RenderMode="Link" OnClick="btnDownload_Click"></dx:ASPxButton>
                </DataItemTemplate>--%>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn ShowInCustomizationForm="True" VisibleIndex="8">
                <DataItemTemplate>
                    <dx:ASPxButton ID="btnReport" runat="server" Text="Download" RenderMode="Button" Font-Bold="True" Font-Size="12" BackColor="Transparent" ForeColor="Black" HoverStyle-BackColor="White" 
                        HoverStyle-BorderRight-BorderWidth="1px" HoverStyle-BorderRight-BorderColor="#727272" HoverStyle-BorderRight-BorderStyle="Dotted"
                        HoverStyle-BorderBottom-BorderWidth="1px" HoverStyle-BorderBottom-BorderColor="#727272" HoverStyle-BorderBottom-BorderStyle="Dotted"
                        OnClick="btnDownload_Click">
                        <Image ToolTip="Report" Url="~/Content/Icons/report.svg" Height="24px" Width="24px" />
                    </dx:ASPxButton>
                </DataItemTemplate>
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
            </dx:GridViewDataDateColumn>
        </Columns>
        <ClientSideEvents Init="onGridViewInit" SelectionChanged="onGridViewSelectionChanged" />
        <Styles>
            <Row BackColor="#F4FDFF">
            </Row>
            <AlternatingRow BackColor="#FFFFF4">
            </AlternatingRow>
            <FocusedRow BackColor="#C6C6FF" Font-Bold="True" ForeColor="Black">
            </FocusedRow>
        </Styles>
    </dx:ASPxGridView>

    <dx:ASPxCallback ID="ASPxCallbackResult" runat="server" ClientInstanceName="callback" OnCallback="ASPxCallbackResult_Callback">
        <ClientSideEvents CallbackComplete="OnCallbackComplete" />
    </dx:ASPxCallback>
</asp:Content>
