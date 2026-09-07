<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="ProjectConstructor.aspx.cs" Inherits="HEA.ePTW.Settings.ProjectConstructor" %>

<asp:Content runat="server" ContentPlaceHolderID="Head">
<%--    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/StandardGridview.css") %>' />--%>
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/ProjectGridview.js") %>'></script>
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/ePTW_Standard.css") %>' />
    <%--<script type="text/javascript" src='<%# ResolveUrl("~/Content/ePTW_Gridview.js") %>'></script>--%>
    <style type="text/css">
        .templateTable {
            border-collapse: collapse;
            width: 98%;
            margin-left: 20px;
        }

            .templateTable td {
                border: solid 0px #C2D4DA;
                padding: 0px;
            }

                .templateTable td.value {
                    font-weight: bold;
                }

        .imageCell {
            vertical-align: top;
            padding-right: 10px;
            width: 120px;
        }

        .dxTheme-Office365Dark .templateTable td {
            border-color: black;
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
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Size="14pt" Text="Project Configuration - (All Projects)" ForeColor="White">
                        </dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
            </dx:LayoutItem>
            <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxGridView ID="gvProjects" ClientInstanceName="gridView" runat="server" CssClass="grid-view"
                            KeyFieldName="Name" EnablePagingGestures="False" Width="100%" EnableRowsCache="False" AutoGenerateColumns="False">
                            
                            <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                            <SettingsBehavior AllowEllipsisInText="true" AllowFocusedRow="true" AllowSelectByRowClick="true" AllowDragDrop="false" AllowSelectSingleRowOnly="True" />                            
                            <Settings ShowColumnHeaders="True"></Settings>
                            <Settings ShowPreview="true" />                            
                            <SettingsSearchPanel GroupOperator="Or" ShowClearButton="False" Visible="True" />
                            <SettingsPopup>
                            <FilterControl AutoUpdatePosition="False"></FilterControl>
                            </SettingsPopup>
                            
                            <EditFormLayoutProperties ColumnCount="1" ShowItemCaptionColon="False">
                                <Items>
                                    <dx:GridViewColumnLayoutItem ColumnName="Photo" ShowCaption="False" HelpText="You can upload JPG, GIF or PNG file. Maximum files size is 4 MB." />
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Project">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Description">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Location">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Main Constructor">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Start Date">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="End Date">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:EmptyLayoutItem ColumnSpan="1" />
                                    <dx:EditModeCommandLayoutItem ColumnSpan="1" ShowCancelButton="true" ShowUpdateButton="true" HorizontalAlign="Right" />
                                </Items>
                            </EditFormLayoutProperties>

                            <ClientSideEvents Init="onGridViewInit" SelectionChanged="onGridViewSelectionChanged" />

                            <Columns>
                                <dx:GridViewDataComboBoxColumn FieldName="StatusText" Caption="Status" VisibleIndex="8" Width="90px">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataBinaryImageColumn FieldName="Photo" VisibleIndex="1" Width="80px" AdaptivePriority="1" Visible="false">
                                    <PropertiesBinaryImage ImageHeight="36px" ImageWidth="36px" ShowLoadingImage="True">
                                        <EditingSettings Enabled="True" UploadSettings-UploadValidationSettings-MaxFileSize="4194304">
                                            <UploadSettings>
                                                <UploadValidationSettings MaxFileSize="4194304"></UploadValidationSettings>
                                            </UploadSettings>
                                        </EditingSettings>
                                        <Style BackColor="White">
                                        </Style>
                                    </PropertiesBinaryImage>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataBinaryImageColumn>
                                <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="2" Caption="Project">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataMemoColumn FieldName="Description" VisibleIndex="3" Width="0px" AdaptivePriority="1">
                                </dx:GridViewDataMemoColumn>
                                <dx:GridViewDataMemoColumn Caption="Location" FieldName="Address" VisibleIndex="4" Width="0px" AdaptivePriority="1">
                                </dx:GridViewDataMemoColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Main Contractor" FieldName="ConstructorName" VisibleIndex="5" Width="0px">
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataDateColumn Caption="Start Date" FieldName="StartDate" VisibleIndex="6" Width="0px">
                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn Caption="End Date" FieldName="EndDate" VisibleIndex="7" Width="0px">
                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                            </Columns>
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
                                <DetailRow>
                                    <div style="padding: 2px 2px 2px 2px">
                                        <dx:ASPxFormLayout runat="server" ID="formLayout" CssClass="formLayout" ShowItemCaptionColon="False" Width="98%">
                                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="650" />
                                            <Items>
                                                <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="#494949">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Size="12pt" Text="1. Contractors" ForeColor="White">
                                                            </dx:ASPxLabel>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                    <Paddings PaddingBottom="8px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="10px" />
                                                </dx:LayoutItem>
                                                <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="White">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxMenu runat="server" ID="ConstructorToolBar" ClientInstanceName="ConstructorToolBar"
                                                                ItemAutoWidth="false" ApplyItemStyleToTemplates="true" ItemWrap="false"
                                                                AllowSelectItem="false" SeparatorWidth="0" 
                                                                Width="100%" CssClass="page-toolbar" BackColor="White">
                                                                <ClientSideEvents ItemClick="onPageToolbarItemClick" />
                                                                <SettingsAdaptivity Enabled="true" EnableAutoHideRootItems="true" EnableCollapseRootItemsToIcons="true" />
                                                                <ItemStyle CssClass="item" VerticalAlign="Middle" />
                                                                <ItemImage Width="16px" Height="16px" />
                                                                <Items>
                                                                    <dx:MenuItem Name="AddConstructor" Text="Add" Alignment="Right" AdaptivePriority="2">
                                                                        <Image Url="~/Content/Images/add.svg" />
                                                                    </dx:MenuItem>
                                                                    <dx:MenuItem Name="DeleteConstructor" Text="Delete" Alignment="Right" AdaptivePriority="2">
                                                                        <Image Url="~/Content/Icons/delete.svg" />
                                                                    </dx:MenuItem>
                                                                </Items>
                                                            </dx:ASPxMenu>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                    <Paddings PaddingBottom="8px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="10px" />
                                                </dx:LayoutItem>
                                                <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="White">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxGridView ID="gvConstructor" ClientInstanceName="gvConstructor" runat="server" Width="100%" 
                                                                 KeyFieldName="ConstructorName"
                                                                OnBeforePerformDataSelect="gvConstructor_BeforePerformDataSelect"
                                                                OnCustomCallback="gvConstructor_CustomCallback" 
                                                                OnCellEditorInitialize="gvConstructor_CellEditorInitialize" 
                                                                OnRowValidating="gvConstructor_RowValidating" 
                                                                OnRowInserting="gvConstructor_RowInserting">
                                                                <SettingsPopup>
                                                                    <FilterControl AutoUpdatePosition="False"></FilterControl>
                                                                </SettingsPopup>
                                                                <EditFormLayoutProperties ShowItemCaptionColon="False">
                                                                    <Items>
                                                                        <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="ConstructorName">
                                                                        </dx:GridViewColumnLayoutItem>
                                                                        <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Description">
                                                                        </dx:GridViewColumnLayoutItem>
                                                                        <dx:EmptyLayoutItem ColSpan="1" Height="10px">
                                                                        </dx:EmptyLayoutItem>
                                                                        <dx:EditModeCommandLayoutItem ColSpan="1" HorizontalAlign="Right">
                                                                        </dx:EditModeCommandLayoutItem>
                                                                    </Items>
                                                                </EditFormLayoutProperties>
                                                                <Settings ShowColumnHeaders="False"></Settings>
                                                                <Columns>
                                                                <dx:GridViewDataComboBoxColumn FieldName="ConstructorName" VisibleIndex="0">
                                                                    <PropertiesComboBox ValueType="System.String" ValueField="Name" TextField="Name" TextFormatString="{0}">  
                                                                        <Columns>  
                                                                            <dx:ListBoxColumn FieldName="Name" />  
                                                                            <dx:ListBoxColumn FieldName="Description" />  
                                                                        </Columns> 
                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                    var&nbsp;selectedItem&nbsp;=&nbsp;s.GetSelectedItem();
	                    txtDescription.SetText(selectedItem.GetColumnText(&quot;Description&quot;));
                    }" />
                                                                    </PropertiesComboBox> 
                                                                </dx:GridViewDataComboBoxColumn>
                                                                <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="1">
                                                                    <PropertiesTextEdit ClientInstanceName="txtDescription">
                                                                    </PropertiesTextEdit>
                                                                </dx:GridViewDataTextColumn>
                                                            </Columns>
                                                                <SettingsBehavior AllowFocusedRow="true" AllowSelectByRowClick="true" AllowDragDrop="false" AllowSelectSingleRowOnly="True" />
                                                                <Border BorderStyle="Solid" BorderWidth="1px" BorderColor="#fff7ff"></Border>
                                                                <BorderBottom BorderColor="#FFF7FF" BorderWidth="1px"></BorderBottom>
                                                                <Styles>
                                                                    <Row BackColor="#F4FDFF">
                                                                    </Row>
                                                                    <AlternatingRow BackColor="#FFFFF4">
                                                                    </AlternatingRow>
                                                                    <FocusedRow BackColor="#004D99" Font-Bold="True" ForeColor="White">
                                                                    </FocusedRow>
                                                                </Styles>
                                                            </dx:ASPxGridView>                                                        
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                    <Paddings PaddingBottom="25px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="0px" />
                                                </dx:LayoutItem>
                                                <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="#494949">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Font-Bold="True" Font-Size="12pt" Text="2. Manpower & Staff" ForeColor="White">
                                                            </dx:ASPxLabel>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                    <Paddings PaddingBottom="8px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="10px" />
                                                </dx:LayoutItem>
                                                <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="White">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxMenu runat="server" ID="ManpowerToolBar" ClientInstanceName="ManpowerToolBar"
                                                                ItemAutoWidth="false" ApplyItemStyleToTemplates="true" ItemWrap="false"
                                                                AllowSelectItem="false" SeparatorWidth="0"
                                                                Width="100%" CssClass="page-toolbar">
                                                                <ClientSideEvents ItemClick="onPageToolbarItemClick" />
                                                                <SettingsAdaptivity Enabled="true" EnableAutoHideRootItems="true" EnableCollapseRootItemsToIcons="true" />
                                                                <ItemStyle CssClass="item" VerticalAlign="Middle" />
                                                                <ItemImage Width="16px" Height="16px" />
                                                                <Items>
                                                                    <dx:MenuItem Name="AddManpower" Text="Add" Alignment="Right" AdaptivePriority="2">
                                                                        <Image Url="~/Content/Images/add.svg" />
                                                                    </dx:MenuItem>
                                                                    <dx:MenuItem Name="DeleteManpower" Text="Delete" Alignment="Right" AdaptivePriority="2">
                                                                        <Image Url="~/Content/Icons/delete.svg" />
                                                                    </dx:MenuItem>
                                                                </Items>
                                                            </dx:ASPxMenu>                                                        
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                    <Paddings PaddingBottom="8px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="10px" />
                                                </dx:LayoutItem>
                                                <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="White">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxGridView ID="gvStaff" ClientInstanceName="gvStaff" runat="server" Width="100%"
                                                                             KeyFieldName="ID"
                                                                             EnablePagingGestures="False" AutoGenerateColumns="False" 
                                                                             OnBeforePerformDataSelect="gvStaff_BeforePerformDataSelect"
                                                                             OnCellEditorInitialize="gvStaff_CellEditorInitialize"
                                                                             OnRowValidating="gvStaff_RowValidating"
                                                                             OnRowInserting="gvStaff_RowInserting"
                                                                             OnCustomCallback="gvStaff_CustomCallback">
                                                                <SettingsPopup>
                                                                    <FilterControl AutoUpdatePosition="False"></FilterControl>
                                                                </SettingsPopup>
                                                                <Settings ShowColumnHeaders="False"></Settings>
                                                                <EditFormLayoutProperties ShowItemCaptionColon="False">
                                                                    <Items>
                                                                        <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="User ID">
                                                                        </dx:GridViewColumnLayoutItem>
                                                                        <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Full Name">
                                                                        </dx:GridViewColumnLayoutItem>
                                                                        <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Position">
                                                                        </dx:GridViewColumnLayoutItem>
                                                                        <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Constructor Name">
                                                                        </dx:GridViewColumnLayoutItem>
                                                                        <dx:EmptyLayoutItem ColSpan="1" Height="10px">
                                                                        </dx:EmptyLayoutItem>
                                                                        <dx:EditModeCommandLayoutItem ColSpan="1" HorizontalAlign="Right">
                                                                        </dx:EditModeCommandLayoutItem>
                                                                    </Items>
                                                                </EditFormLayoutProperties>
                                                                <Columns>
                                                                    <dx:GridViewDataComboBoxColumn FieldName="UserID" VisibleIndex="0" Visible="false">
                                                                        <PropertiesComboBox ValueType="System.String" ValueField="UserID" TextField="UserID" TextFormatString="{0}">  
                                                                            <Columns>  
                                                                                <dx:ListBoxColumn FieldName="UserID" />  
                                                                                <dx:ListBoxColumn FieldName="FullName" />  
                                                                                <dx:ListBoxColumn FieldName="Position" /> 
                                                                                <dx:ListBoxColumn FieldName="ConstructorName" />
                                                                            </Columns> 
                                                    
                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                        var&nbsp;selectedItem&nbsp;=&nbsp;s.GetSelectedItem();
	                        txtFullName.SetText(selectedItem.GetColumnText(&quot;FullName&quot;));
	                        txtPosition.SetText(selectedItem.GetColumnText(&quot;Position&quot;));
	                        txtConstructorName.SetText(selectedItem.GetColumnText(&quot;ConstructorName&quot;));
                        }" />
                                                                        </PropertiesComboBox> 
                                                                    </dx:GridViewDataComboBoxColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="FullName" VisibleIndex="1">
                                                                        <PropertiesTextEdit ClientInstanceName="txtFullName">
                                                                        </PropertiesTextEdit>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Position" VisibleIndex="2">
                                                                        <PropertiesTextEdit ClientInstanceName="txtPosition">
                                                                        </PropertiesTextEdit>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="ConstructorName" VisibleIndex="3">
                                                                        <PropertiesTextEdit ClientInstanceName="txtConstructorName">
                                                                        </PropertiesTextEdit>
                                                                    </dx:GridViewDataTextColumn>
                                                                </Columns>
                                                                <SettingsBehavior AllowFocusedRow="true" AllowSelectByRowClick="true" AllowDragDrop="false" AllowSelectSingleRowOnly="True" />
                                                                <%--<Border BorderStyle="Solid" BorderWidth="1px" BorderColor="#fff7ff"></Border>--%>
                                                                <%--<BorderBottom BorderColor="#FFF7FF" BorderWidth="1px"></BorderBottom>--%>
                                                                <Styles>
                                                                    <Row BackColor="#F4FDFF">
                                                                    </Row>
                                                                    <AlternatingRow BackColor="#FFFFF4">
                                                                    </AlternatingRow>
                                                                    <FocusedRow BackColor="#004D99" Font-Bold="True" ForeColor="White">
                                                                    </FocusedRow>
                                                                </Styles>
                                                            </dx:ASPxGridView>                                                        
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                    <Paddings PaddingBottom="25px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="0px" />
                                                </dx:LayoutItem>
<%--                                                <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="#494949">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Font-Bold="True" Font-Size="12pt" Text="3. Equipments" ForeColor="White">
                                                            </dx:ASPxLabel>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                    <Paddings PaddingBottom="8px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="10px" />
                                                </dx:LayoutItem>
                                                <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="White">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxMenu runat="server" ID="EquipmentToolBar" ClientInstanceName="EquipmentToolBar"
                                                                ItemAutoWidth="false" ApplyItemStyleToTemplates="true" ItemWrap="false"
                                                                AllowSelectItem="false" SeparatorWidth="0"
                                                                Width="100%" CssClass="page-toolbar">
                                                                <ClientSideEvents ItemClick="onPageToolbarItemClick" />
                                                                <SettingsAdaptivity Enabled="true" EnableAutoHideRootItems="true" EnableCollapseRootItemsToIcons="true" />
                                                                <ItemStyle CssClass="item" VerticalAlign="Middle" />
                                                                <ItemImage Width="16px" Height="16px" />
                                                                <Items>
                                                                    <dx:MenuItem Name="AddEquipment" Text="Add" Alignment="Right" AdaptivePriority="2" Enabled="true">
                                                                        <Image Url="~/Content/Images/add.svg" />
                                                                    </dx:MenuItem>
                                                                    <dx:MenuItem Name="DeleteEquipment" Text="Delete" Alignment="Right" AdaptivePriority="2" Enabled="true">
                                                                        <Image Url="~/Content/Icons/delete.svg" />
                                                                    </dx:MenuItem>
                                                                </Items>
                                                            </dx:ASPxMenu>                                                        
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                    <Paddings PaddingBottom="8px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="10px" />
                                                </dx:LayoutItem>
                                                <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="White">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxGridView ID="gvEquipment" ClientInstanceName="gvEquipment" runat="server" Width="100%"
                                                                             EnablePagingGestures="False" KeyFieldName="ID"
                                                                             OnBeforePerformDataSelect="gvEquipment_BeforePerformDataSelect" 
                                                                             OnCellEditorInitialize="gvEquipment_CellEditorInitialize"
                                                                             OnRowValidating="gvEquipment_RowValidating"
                                                                             OnRowInserting="gvEquipment_RowInserting"
                                                                             OnCustomCallback="gvEquipment_CustomCallback"
                                                                             AutoGenerateColumns="False">
                                                                <SettingsBehavior AllowFocusedRow="true" AllowSelectByRowClick="true" AllowDragDrop="false" AllowSelectSingleRowOnly="True" />
                                                                <SettingsPopup>
                                                                    <FilterControl AutoUpdatePosition="False"></FilterControl>
                                                                </SettingsPopup>
                                                                <Settings ShowColumnHeaders="False"></Settings>
                                                                <EditFormLayoutProperties ShowItemCaptionColon="False">
                                                                    <Items>
                                                                        <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Registration No">
                                                                        </dx:GridViewColumnLayoutItem>
                                                                        <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Equipment Type">
                                                                        </dx:GridViewColumnLayoutItem>
                                                                        <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Equipment Name">
                                                                        </dx:GridViewColumnLayoutItem>
                                                                        <dx:EmptyLayoutItem ColSpan="1" Height="10px">
                                                                        </dx:EmptyLayoutItem>
                                                                        <dx:EditModeCommandLayoutItem ColSpan="1" HorizontalAlign="Right">
                                                                        </dx:EditModeCommandLayoutItem>
                                                                    </Items>
                                                                </EditFormLayoutProperties>
                                                                <Columns>
                                                                    <dx:GridViewDataComboBoxColumn FieldName="RegistrationNo" VisibleIndex="0">
                                                                        <PropertiesComboBox ValueType="System.String" ValueField="RegistrationNo" TextFormatString="{0}">  
                                                                            <Columns>  
                                                                                <dx:ListBoxColumn FieldName="RegistrationNo" />  
                                                                                <dx:ListBoxColumn FieldName="EquipmentType" /> 
                                                                                <dx:ListBoxColumn FieldName="EquipmentName" />
                                                                            </Columns>  
                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                        var&nbsp;selectedItem&nbsp;=&nbsp;s.GetSelectedItem();
	                        txtEquipmentType.SetText(selectedItem.GetColumnText(&quot;EquipmentType&quot;));
	                        txtEquipmentName.SetText(selectedItem.GetColumnText(&quot;EquipmentName&quot;));
                        }" />
                                                                        </PropertiesComboBox> 
                                                                    </dx:GridViewDataComboBoxColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="EquipmentName" VisibleIndex="1">
                                                                        <PropertiesTextEdit ClientInstanceName="txtEquipmentName">
                                                                        </PropertiesTextEdit>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="EquipmentType" VisibleIndex="2">
                                                                        <PropertiesTextEdit ClientInstanceName="txtEquipmentType">
                                                                        </PropertiesTextEdit>
                                                                    </dx:GridViewDataTextColumn>
                                                                </Columns>
                                                                <Border BorderStyle="Solid" BorderWidth="1px" BorderColor="#fff7ff"></Border>
                                                                <BorderBottom BorderColor="#FFF7FF" BorderWidth="1px"></BorderBottom>
                                                                <Styles>
                                                                    <Row BackColor="#F4FDFF">
                                                                    </Row>
                                                                    <AlternatingRow BackColor="#FFFFF4">
                                                                    </AlternatingRow>
                                                                    <FocusedRow BackColor="#004D99" Font-Bold="True" ForeColor="White">
                                                                    </FocusedRow>
                                                                </Styles>
                                                            </dx:ASPxGridView>                                                        
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                    <Paddings PaddingBottom="25px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="0px" />
                                                </dx:LayoutItem>--%>
<%--                                                <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="#494949">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" Font-Bold="True" Font-Size="12pt" Text="4. Photos" ForeColor="White">
                                                            </dx:ASPxLabel>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                    <Paddings PaddingBottom="8px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="10px" />
                                                </dx:LayoutItem>--%>
<%--                                                <dx:LayoutItem ColSpan="1" ShowCaption="False" Width="100%" BackColor="White">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxCardView ID="gvImage" ClientInstanceName="gvImage" runat="server" KeyFieldName="ID" Width="99%"
                                                                                OnBeforePerformDataSelect="gvPhoto_BeforePerformDataSelect" 
                                                                                OnCardValidating="gvImage_CardValidating"
                                                                                OnCardInserting="gvImage_CardInserting"
                                                                                OnCardUpdating="gvImage_CardUpdating"
                                                                                OnCardDeleting="gvImage_CardDeleting"
                                                                                AutoGenerateColumns="False" 
                                                                                Settings-ShowTitlePanel="False"
                                                                                EnablePagingCallbackAnimation="True">
                                                                <Settings LayoutMode="Breakpoints" VerticalScrollBarMode="Hidden" />
                                                                <SettingsAdaptivity>
                                                                    <BreakpointsLayoutSettings CardsPerRow="1">
                                                                        <Breakpoints>
                                                                            <dx:CardViewBreakpoint DeviceSize="XLarge" CardsPerRow="3" />
                                                                            <dx:CardViewBreakpoint DeviceSize="Large" CardsPerRow="3" />
                                                                            <dx:CardViewBreakpoint DeviceSize="Medium" CardsPerRow="2" />
                                                                            <dx:CardViewBreakpoint DeviceSize="Small" CardsPerRow="1" />
                                                                            <dx:CardViewBreakpoint DeviceSize="Custom" MaxWidth="450" CardsPerRow="1" />
                                                                        </Breakpoints>
                                                                    </BreakpointsLayoutSettings>
                                                                </SettingsAdaptivity>
                                                                <SettingsPopup>
                                                                    <FilterControl AutoUpdatePosition="False"></FilterControl>
                                                                </SettingsPopup>
                                                                <SettingsExport ExportSelectedCardsOnly="False"></SettingsExport>
                                                                <Columns>
                                                                    <dx:CardViewBinaryImageColumn FieldName="Document" Caption="">
                                                                        <PropertiesBinaryImage ImageHeight="120px">
                                                                            <EditingSettings Enabled="true" UploadSettings-UploadValidationSettings-MaxFileSize="4194304">
                                                                                <UploadSettings>
                                                                                    <UploadValidationSettings MaxFileSize="4194304"></UploadValidationSettings>
                                                                                </UploadSettings>
                                                                            </EditingSettings>
                                                                        </PropertiesBinaryImage>
                                                                    </dx:CardViewBinaryImageColumn>
                                                                </Columns>
                                                                <CardLayoutProperties>
                                                                    <Items>
                                                                        <dx:CardViewCommandLayoutItem ShowNewButton="true" ShowDeleteButton="true" HorizontalAlign="Left" ShowEditButton="True" />
                                                                        <dx:CardViewColumnLayoutItem ColumnName="Document" ShowCaption="False"></dx:CardViewColumnLayoutItem>
                                                                        <dx:EditModeCommandLayoutItem HorizontalAlign="Left" />
                                                                    </Items>
                                                                </CardLayoutProperties>
                                                                <StylesExport>
                                                                    <Card BorderSize="1" BorderSides="All"></Card>
                                                                    <Group BorderSize="1" BorderSides="All"></Group>
                                                                    <TabbedGroup BorderSize="1" BorderSides="All"></TabbedGroup>
                                                                    <Tab BorderSize="1"></Tab>
                                                                </StylesExport>
                                                            </dx:ASPxCardView>                                                        
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                    <Paddings PaddingBottom="25px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="0px" />
                                                </dx:LayoutItem>--%>
                                            </Items>
                                        </dx:ASPxFormLayout>
                                    </div>
                                </DetailRow>
                            </Templates>

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
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
</asp:Content>
