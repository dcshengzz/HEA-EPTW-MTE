<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="NewPTW.aspx.cs" Inherits="HEA.ePTW.PTW.NewPTW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <%--    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/ePTW_TBM.css") %>' />    
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/ePTW_PTW.js") %>'></script>--%>
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/ePTW_CCP.css") %>' />  
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/ePTW_CCP.js") %>'></script>
    <script type="text/javascript">  
        if (navigator.geolocation)
        {  
            navigator.geolocation.getCurrentPosition(success);  
        }
        else
        {  
            alert("There is Some Problem on your current browser to get GeoLocation !");  
        }  
  
        function success(position) {  
            var latitude = position.coords.latitude;
            var longitude = position.coords.longitude;
            var city = position.coords.locality;
            var lbllatitude = document.getElementById("<%=hfLatitude.ClientID %>");
            var lbllongitude = document.getElementById("<%=hfLongitude.ClientID %>");
            lbllatitude.value = latitude;
            lbllongitude.value = longitude;
        }  
    </script> 
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PageContent" runat="server">
    <asp:HiddenField ID="hfLatitude" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfLongitude" runat="server" ClientIDMode="Static" />
    <dx:ASPxFormLayout runat="server" ID="flPermitToWork" CssClass="formLayout" ShowItemCaptionColon="False" RequiredMark="" Width="98%">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="650" />
        <Items>
            <dx:LayoutItem BackColor="#494949" ColSpan="1" ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxLabel ID="lblTitle" runat="server" Font-Bold="True" Font-Size="14pt" ForeColor="White" Text="Permit To Work">
                        </dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Project Name" ColSpan="1">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxLabel ID="lblProjectName" runat="server" Width="100%">
                        </dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
                <CaptionStyle Font-Bold="True">
                </CaptionStyle>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Date From" ColSpan="1">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxDateEdit ID="dtFrom" runat="server" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy" OnValidation="dtFrom_Validation">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Please select the Valid Date" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                <RequiredField ErrorText="Please select the Valid Date" IsRequired="True" />
                            </ValidationSettings>
                            <InvalidStyle BackColor="#FFE6EE">
                            </InvalidStyle>
                        </dx:ASPxDateEdit>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
                <CaptionStyle Font-Bold="True">
                </CaptionStyle>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Date To" ColSpan="1">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxDateEdit ID="dtTo" runat="server" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy" OnValidation="dtTo_Validation">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Please select the Valid Date" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                <RequiredField ErrorText="Please select the Valid Date" IsRequired="True" />
                            </ValidationSettings>
                            <InvalidStyle BackColor="#FFE6EE">
                            </InvalidStyle>
                        </dx:ASPxDateEdit>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
                <CaptionStyle Font-Bold="True">
                </CaptionStyle>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Manufacturing Number" ColSpan="1">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxComboBox ID="cbMFG" runat="server" ValueType="System.String" DropDownStyle="DropDownList" IncrementalFilteringMode="Contains" EnableCallbackMode="true" ValueField="RegistrationNo" TextFormatString="{0}" Width="100%" OnCustomFiltering="cbMFG_CustomFiltering">
                            <Columns>
                                <dx:ListBoxColumn FieldName="RegistrationNo" />
                                <dx:ListBoxColumn FieldName="EquipmentName" Caption="Description" />
                                <dx:ListBoxColumn FieldName="EquipmentType" />
                            </Columns>
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Please select the MFG" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                <RequiredField ErrorText="Please select the MFG" IsRequired="True" />
                            </ValidationSettings>
                            <InvalidStyle BackColor="#FFE6EE">
                            </InvalidStyle>
                        </dx:ASPxComboBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
                <CaptionStyle Font-Bold="True">
                </CaptionStyle>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Work Description" ColSpan="1">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxMemo ID="txtJobDescription" runat="server" Height="100px" Width="100%">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Please enter the Description" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                <RequiredField ErrorText="Please enter the Description" IsRequired="True" />
                            </ValidationSettings>
                            <InvalidStyle BackColor="#FFE6EE">
                            </InvalidStyle>
                        </dx:ASPxMemo>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
                <CaptionStyle Font-Bold="True">
                </CaptionStyle>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Total Worker" ColSpan="1">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxSpinEdit ID="seTotalWorker" runat="server" Number="1" NumberType="Integer" MinValue="1" MaxValue="1000" ShowOutOfRangeWarning="False">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Please enter the Valid Number" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                <RequiredField ErrorText="Please enter the Valid Number" IsRequired="True" />
                            </ValidationSettings>
                            <InvalidStyle BackColor="#FF9933">
                            </InvalidStyle>
                        </dx:ASPxSpinEdit>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
                <CaptionStyle Font-Bold="True">
                </CaptionStyle>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Work Type" ColSpan="1">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxComboBox ID="cbType" runat="server" ValueType="System.String" ValueField="TemplateName" TextFormatString="{0}" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="cbType_SelectedIndexChanged">
                            <Columns>
                                <dx:ListBoxColumn FieldName="TemplateName" />
                                <dx:ListBoxColumn FieldName="Description" />
                            </Columns>
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Please select the Work Type" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                <RequiredField ErrorText="Please select the Work Type" IsRequired="True" />
                            </ValidationSettings>
                            <InvalidStyle BackColor="#FFE6EE">
                            </InvalidStyle>
                        </dx:ASPxComboBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
                <CaptionStyle Font-Bold="True">
                </CaptionStyle>
            </dx:LayoutItem>

            <dx:LayoutItem Caption="" ColSpan="1" ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <div>
                            <ol>
                              <li style="margin-top: 2px; margin-bottom: 2px;">Applicant(s) shall comply with WSH Act, WSH Subsidiary Legislations, NEA Legislations, Code of Practices etc</li>
                              <li style="margin-top: 2px; margin-bottom: 2px;">Permit-To-Work is to be submitted 3 working days in advance (minimum) & PTW-maximum period is 7 days and non-automatic renewal and to resubmit PTW for approval. Daily Permit-To-Work shall apply which applicable.</li>
                              <li style="margin-top: 2px; margin-bottom: 2px;">Permit-To-Work is to be approval by Project Manager before commence of work & submit minimum 3 days in advance.</li>
                              <li style="margin-top: 2px; margin-bottom: 2px;">Approved PTW to be displayed prominently at where the working at height is to be carried out, e.g. Access Point</li>
                              <li style="margin-top: 2px; margin-bottom: 2px;">Workers must have complete CSOC & Safety Induction & to adhere with HEA Safety & Health Management System (SHMS), Safe Work Practices (SWP) and comply with all relevant Occupational Safety & Health (OSH) legislations including MOM, NEA, HDB, SCDF etc</li>
                              <li style="margin-top: 2px; margin-bottom: 2px;">PTW will be revoked & shall be considered invalid if any safety non-compliance/lapse is found & communicated.</li>
                            </ol>
                        </div>
                        <dx:ASPxLabel ID="lblText1" runat="server" Text="" Font-Bold="False" Font-Size="9pt">
                        </dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="2px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="2px" />
            </dx:LayoutItem>

            <dx:LayoutGroup Name="QuestionsAndAnswer" Caption="" ColSpan="1" ShowCaption="False" ColCount="2" ColumnCount="2" Width="100%">
                <Items>
                    <dx:LayoutItem Caption="" ColSpan="2" ShowCaption="False" ColumnSpan="2" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxCallbackPanel ID="QuestionsAndAnswerCallbackPanel" ClientInstanceName="QuestionsAndAnswerCallbackPanel" runat="server" Width="100%" OnCallback="QuestionsAndAnswerCallbackPanel_Callback">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent2" runat="server">
                                            <dx:ASPxFormLayout ID="FormLayoutQNA" runat="server" ShowItemCaptionColon="False" RequiredMark="" RequiredMarkDisplayMode="None" Width="100%">
                                                <Items>
                                                    <dx:LayoutGroup Name="QuestionsAndAnswer" Caption="" ColSpan="1" ShowCaption="False" ColCount="1" ColumnCount="1" Width="100%">
                                                        <Items>
                                                        </Items>
                                                    </dx:LayoutGroup>
                                                </Items>
                                            </dx:ASPxFormLayout>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutItem Caption="" ColSpan="1" ShowCaption="False" Visible="false">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxGridView ID="gvEquipment" ClientInstanceName="gvEquipment" runat="server" Width="100%"
                            EnablePagingGestures="False" KeyFieldName="RegistrationNo"
                            OnCellEditorInitialize="gvEquipment_CellEditorInitialize"
                            OnRowValidating="gvEquipment_RowValidating"
                            OnRowInserting="gvEquipment_RowInserting" 
                            OnRowDeleting="gvEquipment_RowDeleting"
                            AutoGenerateColumns="False">
                            <SettingsBehavior AllowFocusedRow="true" AllowSelectByRowClick="true" AllowDragDrop="false" AllowSelectSingleRowOnly="True" />
                            <SettingsPager Visible="False">
                            </SettingsPager>
                            <Settings ShowTitlePanel="True" />
                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                            <SettingsPopup>
                                <FilterControl AutoUpdatePosition="False"></FilterControl>
                            </SettingsPopup>
                            <SettingsText EmptyDataRow=" " Title="Equipments to be Used" />
                            <EditFormLayoutProperties ShowItemCaptionColon="False" AlignItemCaptionsInAllGroups="True">
                                <Items>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Registration No">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:EmptyLayoutItem ColSpan="1" Height="10px">
                                    </dx:EmptyLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Equipment Type">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:EmptyLayoutItem ColSpan="1" Height="10px">
                                    </dx:EmptyLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Equipment Name">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:EmptyLayoutItem ColSpan="1" Height="15px">
                                    </dx:EmptyLayoutItem>
                                    <dx:EditModeCommandLayoutItem ColSpan="1" HorizontalAlign="Right">
                                    </dx:EditModeCommandLayoutItem>
                                </Items>
                                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit">
                                </SettingsAdaptivity>
                            </EditFormLayoutProperties>
                            <Columns>
                                <dx:GridViewDataComboBoxColumn FieldName="RegistrationNo" VisibleIndex="1">
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
                                <dx:GridViewDataTextColumn FieldName="EquipmentName" VisibleIndex="2">
                                    <PropertiesTextEdit ClientInstanceName="txtEquipmentName">
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="EquipmentType" VisibleIndex="3">
                                    <PropertiesTextEdit ClientInstanceName="txtEquipmentType">
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Toolbars>
                                <dx:GridViewToolbar Position="Bottom">
                                    <Items>
                                        <dx:GridViewToolbarItem Command="New" Text="Add Equipment">
                                        </dx:GridViewToolbarItem>
                                        <dx:GridViewToolbarItem Command="Delete" Text="Remove Equipment">
                                        </dx:GridViewToolbarItem>
                                    </Items>
                                    <SettingsAdaptivity EnableCollapseRootItemsToIcons="True" />
                                </dx:GridViewToolbar>
                            </Toolbars>
                            <Styles>
                                <Header BackColor="WhiteSmoke" ForeColor="#717171">
                                    <Border BorderColor="#A4A4A4" BorderStyle="Solid" BorderWidth="1px" />
                                </Header>
                                <TitlePanel BackColor="WhiteSmoke" ForeColor="#717171" HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="12pt" Font-Bold="True">
                                    <Paddings PaddingLeft="10px" PaddingTop="15px" PaddingBottom="10px" />
                                    <BorderBottom BorderColor="#A4A4A4" BorderStyle="Solid" BorderWidth="1px" />
                                </TitlePanel>
                            </Styles>
                            <StylesToolbar>
                                <Item VerticalAlign="Middle">
                                    <Border BorderColor="Transparent" BorderStyle="Solid" BorderWidth="0px" />
                                </Item>
                            </StylesToolbar>
                            <Border BorderColor="#A4A4A4" BorderStyle="Solid" BorderWidth="1px" />
                        </dx:ASPxGridView>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="5px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="5px" />
            </dx:LayoutItem>
            <dx:LayoutItem Caption="" ColSpan="1" ShowCaption="False" Visible="false">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxGridView ID="gvStaff" ClientInstanceName="gvStaff" runat="server" Width="100%"
                            KeyFieldName="UserID" EnablePagingGestures="False" AutoGenerateColumns="False" 
                            OnCellEditorInitialize="gvStaff_CellEditorInitialize"
                            OnRowValidating="gvStaff_RowValidating"
                            OnRowInserting="gvStaff_RowInserting" 
                            OnRowDeleting="gvStaff_RowDeleting">
                            <SettingsBehavior AllowFocusedRow="true" AllowSelectByRowClick="true" AllowDragDrop="false" AllowSelectSingleRowOnly="True" />
                            <SettingsPager Visible="False">
                            </SettingsPager>
                            <Settings ShowTitlePanel="True" />
                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                            <SettingsPopup>
                                <FilterControl AutoUpdatePosition="False"></FilterControl>
                            </SettingsPopup>
                            <SettingsText EmptyDataRow=" " Title="Manpower & Staffs" />
                            <EditFormLayoutProperties ShowItemCaptionColon="False">
                                <Items>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="User ID">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:EmptyLayoutItem ColSpan="1" Height="10px">
                                    </dx:EmptyLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Full Name">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:EmptyLayoutItem ColSpan="1" Height="10px">
                                    </dx:EmptyLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Position">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:EmptyLayoutItem ColSpan="1" Height="10px">
                                    </dx:EmptyLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Constructor Name">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:EmptyLayoutItem ColSpan="1" Height="15px">
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
                            <Toolbars>
                                <dx:GridViewToolbar Position="Bottom">
                                    <Items>
                                        <dx:GridViewToolbarItem Command="New" Text="Add Staff">
                                        </dx:GridViewToolbarItem>
                                        <dx:GridViewToolbarItem Command="Delete" Text="Remove Staff">
                                        </dx:GridViewToolbarItem>
                                    </Items>
                                    <SettingsAdaptivity EnableCollapseRootItemsToIcons="True" />
                                </dx:GridViewToolbar>
                            </Toolbars>
                            <Styles>
                                <Header BackColor="WhiteSmoke" ForeColor="#717171">
                                    <Border BorderColor="#A4A4A4" BorderStyle="Solid" BorderWidth="1px" />
                                </Header>
                                <TitlePanel BackColor="WhiteSmoke" ForeColor="#717171" HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="12pt" Font-Bold="True">
                                    <Paddings PaddingLeft="10px" PaddingTop="15px" PaddingBottom="10px" />
                                    <BorderBottom BorderColor="#A4A4A4" BorderStyle="Solid" BorderWidth="1px" />
                                </TitlePanel>
                            </Styles>
                            <StylesToolbar>
                                <Item VerticalAlign="Middle">
                                    <Border BorderColor="Transparent" BorderStyle="Solid" BorderWidth="0px" />
                                </Item>
                            </StylesToolbar>
                            <Border BorderColor="#A4A4A4" BorderStyle="Solid" BorderWidth="1px" />
                        </dx:ASPxGridView>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="5px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="5px" />
            </dx:LayoutItem>
            <dx:LayoutGroup BackColor="Transparent" Caption="" ColSpan="1" ShowCaption="False" Name="AttachmentFileControl">
                <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                <Items>
                    <dx:LayoutItem Caption="" ColSpan="1" ShowCaption="False" Name="UploadFilePanel" RequiredMarkDisplayMode="Hidden">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxUploadControl ID="UploadFile" runat="server" Width="100%" OnFileUploadComplete="UploadFileControl_FileUploadComplete" AutoStartUpload="true" UploadMode="Auto" ShowTextBox="True" ShowProgressPanel="True" RightToLeft="True" BrowseButton-Text="Browse File" TextBoxStyle-HorizontalAlign="Left" BrowseButtonStyle-BackColor="#4F81BD" BrowseButtonStyle-ForeColor="White">
                                    <AdvancedModeSettings EnableMultiSelect="False" EnableFileList="False" EnableDragAndDrop="True" />
                                    <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".jpg,.jpeg,.gif,.png,.pdf">
                                    </ValidationSettings>
                                    <ClientSideEvents FileUploadComplete="OnFileUploadComplete" />
                                </dx:ASPxUploadControl>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="1" ShowCaption="False" Name="UploadFileNote" RequiredMarkDisplayMode="Hidden">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblFileNote" runat="server" Text="Allowed file extensions: .jpg, .jpeg, .gif, .png., .pdf. (Maximum file size: 4 MB.)" Font-Size="9pt" Font-Bold="False">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="10px" PaddingLeft="10px" PaddingTop="2px" />
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="1" ShowCaption="False" Name="EditAttachmentDoc" RequiredMarkDisplayMode="Hidden">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxCallbackPanel ID="cbAfterUploadDoc" ClientInstanceName="cbAfterUploadDoc" runat="server">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent1" runat="server">
                                            <dx:ASPxGridView ID="cvAttachmentDocument" runat="server" KeyFieldName="FileName" OnRowDeleting="cvDocument_RowDeleting">
                                                <SettingsPopup>
                                                <FilterControl AutoUpdatePosition="False"></FilterControl>
                                                </SettingsPopup>
                                                <SettingsText EmptyDataRow=" " />
                                                <Columns>
                                                    <dx:GridViewDataTextColumn VisibleIndex="0" Caption="File Name">
                                                        <DataItemTemplate>
                                                            <dx:ASPxButton ID="btnDownload" runat="server" Text='<%# "The " + Eval("FileTypeInText") + " " + Eval("FileName") + " has been uploaded on " + Eval("Created", "{0:dd MMM yyyy}") + " by " + Eval("CreatedBy") %>' RenderMode="Link" OnClick="btnDownload_Click" CausesValidation="False" Font-Size="9" Wrap="True" HorizontalAlign="Left"></dx:ASPxButton>
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="FileType" VisibleIndex="1" Visible="false">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewCommandColumn ShowDeleteButton="True" ShowInCustomizationForm="True" ShowRecoverButton="False" VisibleIndex="2">
                                                    </dx:GridViewCommandColumn>
                                                </Columns>
                                                <ClientSideEvents EndCallback="function(s, e) {
	cbDisplayImage.PerformCallback();
}" />
                                                <Settings ShowColumnHeaders="False" />
                                                <Styles>
                                                    <Row Font-Size="8pt">
                                                    </Row>
                                                    <Cell>
                                                        <Border BorderStyle="None" BorderWidth="0px" />
                                                    </Cell>
                                                </Styles>
                                                <Border BorderColor="Transparent" BorderStyle="None" BorderWidth="0px" />
                                            </dx:ASPxGridView>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="1" ShowCaption="False" Name="DisplayImage" RequiredMarkDisplayMode="Hidden">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxCallbackPanel ID="cbDisplayImage" ClientInstanceName="cbDisplayImage" runat="server" OnCallback="cbDisplayImage_Callback">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent3" runat="server">
                                            <dx:ASPxImageGallery ID="DisplayImageItems" runat="server" EmptyDataText=" " EnableViewState="False" ImageContentBytesField="Document" ItemSpacing="2px" Layout="Breakpoints" ThumbnailWidth="480" ThumbnailHeight="320" Width="100%">
                                                <SettingsFolder ImageCacheFolder="~\Thumb\" />
                                                <SettingsFullscreenViewer NavigationBarVisibility="Always" />
                                                <Styles>
                                                    <Content>
                                                        <Paddings PaddingTop="8px" />
                                                        <Border BorderWidth="0px" />
                                                    </Content>
                                                    <Item>
                                                        <Border BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                    </Item>
                                                </Styles>
                                                <SettingsBreakpointsLayout ItemsPerPage="30" ItemsPerRow="6">
                                                    <Breakpoints>
                                                        <dx:ImageGalleryBreakpoint DeviceSize="Medium" ItemsPerRow="4" />
                                                        <dx:ImageGalleryBreakpoint DeviceSize="Small" ItemsPerRow="3" />
                                                        <dx:ImageGalleryBreakpoint DeviceSize="Custom" MaxWidth="545" ItemsPerRow="2" />
                                                    </Breakpoints>
                                                </SettingsBreakpointsLayout>
                                                <PagerSettings EndlessPagingMode="OnScroll">
                                                </PagerSettings>
                                                <Paddings Padding="0px" />
                                            </dx:ASPxImageGallery>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
                <ParentContainerStyle>
                    <Paddings PaddingBottom="0px" PaddingLeft="15px" PaddingRight="0px" PaddingTop="0px" />
                </ParentContainerStyle>
            </dx:LayoutGroup>

            <dx:LayoutGroup Caption="" ColCount="2" ColSpan="1" ColumnCount="2" ShowCaption="False" Width="100%" Name="SubmitInfo">
                <Border BorderStyle="Solid" BorderWidth="2px" BorderColor="DarkGray" />
                <Items>
                    <dx:LayoutItem Caption="" ColSpan="2" ColumnSpan="2" ShowCaption="False" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblSubmitStatus" runat="server" Text="" Font-Bold="True" Font-Size="12" Font-Underline="False">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="5px" />
                        <ParentContainerStyle>
                            <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                        </ParentContainerStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="2" HorizontalAlign="Left" ShowCaption="False" VerticalAlign="Top" ColumnSpan="2" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblSubmitNote" runat="server" Text="I shall assure that there are no incompatible this PTW and shall ensure that all safety control measures are duly checked by the person responsible until work completion. I am aware that no work shall be carried out before PTW approval.">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Name" ColSpan="1" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblSubmitName" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="10px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ShowCaption="False" ColSpan="1" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblSubmitRole" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="10px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Designation" ColSpan="1" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblSubmitDesignation" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Company Name" ColSpan="1" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblSubmitCompany" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Remarks" ColSpan="2" ColumnSpan="2" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxMemo ID="txtSubmitRemarks" runat="server" Width="100%">
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="2" ColumnSpan="2" ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxButton ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" Width="100px"></dx:ASPxButton>
                                <%--<dx:ASPxButton ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" Width="100px"></dx:ASPxButton>--%>
                                <%--<dx:ASPxButton ID="btnCancel" runat="server" Text="Back" CausesValidation="False" Width="100px" OnClick="btnBack_Click"></dx:ASPxButton>--%>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="10px" PaddingTop="10px" />
                    </dx:LayoutItem>
                </Items>
                <Paddings PaddingBottom="0px" PaddingLeft="10px" PaddingRight="10px" PaddingTop="0px" />
                <ParentContainerStyle>
                    <Paddings PaddingLeft="16px" />
                </ParentContainerStyle>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="" ColCount="2" ColSpan="1" ColumnCount="2" ShowCaption="False" Width="100%" Name="AssessedInfo">
                <Border BorderStyle="Solid" BorderWidth="2px" BorderColor="DarkGray" />
                <Items>
                    <dx:LayoutItem Caption="" ColSpan="2" ColumnSpan="2" ShowCaption="False" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblAssessedStatus" runat="server" Text="Part 2: Endorsement by WAH Assessor" Font-Size="12" Font-Bold="True">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="5px" />
                        <ParentContainerStyle>
                            <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                        </ParentContainerStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="2" HorizontalAlign="Left" ShowCaption="False" VerticalAlign="Top" ColumnSpan="2" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblAssessedNote" runat="server" Text="I Acknowledge the above work activities shall be carried out in accordance with the Risk Assessment and Safe Work Practices.">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Name" ColSpan="1" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblAssessedName" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="10px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="1" VerticalAlign="Top" ShowCaption="False" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblAssessedRole" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="10px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Designation" ColSpan="1" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblAssessedDesignation" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Company" ColSpan="1" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblAssessedCompany" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Remarks" ColSpan="2" ColumnSpan="2" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxMemo ID="txtAssessedRemarks" runat="server" Width="100%">
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="2" ColumnSpan="2" ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxButton ID="btnAssessedApprove" runat="server" Text="Verify" Width="100px" OnClick="btnAssessedApprove_Click" ValidationGroup="Assessor"></dx:ASPxButton>
<%--                                <dx:ASPxButton ID="btnAssessedReject" runat="server" Text="Reject" Width="100px" OnClick="btnAssessedReject_Click"></dx:ASPxButton>
                                <dx:ASPxButton ID="btnAssessedReturn" runat="server" Text="Return" Width="100px" OnClick="btnAssessedReturn_Click"></dx:ASPxButton>
                                <dx:ASPxButton ID="btnAssessedCancel" runat="server" Text="Back" Width="100px" CausesValidation="False" OnClick="btnBack_Click"></dx:ASPxButton>--%>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="10px" />
                    </dx:LayoutItem>
                </Items>
                <Paddings PaddingBottom="0px" PaddingLeft="10px" PaddingRight="10px" PaddingTop="0px" />
                <ParentContainerStyle>
                    <Paddings PaddingLeft="16px" />
                </ParentContainerStyle>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="" ColCount="2" ColSpan="1" ColumnCount="2" ShowCaption="False" Width="100%" Name="VerifiedInfo">
                <Border BorderStyle="Solid" BorderWidth="2px" BorderColor="DarkGray" />
                <Items>
                    <dx:LayoutItem Caption="" ColSpan="2" ColumnSpan="2" ShowCaption="False" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblVerifiedStatus" runat="server" Text="Part 3: HEA Safety" Font-Bold="True" Font-Size="12">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="5px" />
                        <ParentContainerStyle>
                            <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                        </ParentContainerStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="2" HorizontalAlign="Left" ShowCaption="False" VerticalAlign="Top" ColumnSpan="2" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblVerifiedNote" runat="server" Text="Satisfaction of the safety provision taken by applicant.">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Name" ColSpan="1" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblVerifiedName" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="10px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="1" VerticalAlign="Top" ShowCaption="False" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblVerifiedRole" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="10px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Designation" ColSpan="1" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblVerifiedDesignation" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Company" ColSpan="1" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblVerifiedCompany" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Remarks" ColSpan="2" ColumnSpan="2" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxMemo ID="txtVerifiedRemarks" runat="server" Width="100%">
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="2" ColumnSpan="2" ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxButton ID="btnVerifyApprove" runat="server" Text="Verify" Width="100px" OnClick="btnVerifyApprove_Click" ValidationGroup="Assessor"></dx:ASPxButton>
                                <%--<dx:ASPxButton ID="btnVerifyBack" runat="server" Text="Back" Width="100px" CausesValidation="False" OnClick="btnBack_Click"></dx:ASPxButton>--%>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="10px" />
                    </dx:LayoutItem>
                </Items>
                <Paddings PaddingBottom="0px" PaddingLeft="10px" PaddingRight="10px" PaddingTop="0px" />
                <ParentContainerStyle>
                    <Paddings PaddingLeft="16px" />
                </ParentContainerStyle>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="" ColCount="2" ColSpan="1" ColumnCount="2" ShowCaption="False" Width="100%" Name="ApprovalInfo">
                <Border BorderStyle="Solid" BorderWidth="2px" BorderColor="DarkGray" />
                <Items>
                    <dx:LayoutItem Caption="" ColSpan="2" ColumnSpan="2" ShowCaption="False" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblApprovalStatus" runat="server" Text="Part 4: Approval by HEA Project Manager / Authorized Competent Person" Font-Size="12" Font-Bold="True">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="5px" />
                        <ParentContainerStyle>
                            <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                        </ParentContainerStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="2" HorizontalAlign="Left" ShowCaption="False" VerticalAlign="Top" ColumnSpan="2" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblApprovalNote" runat="server" Text="Permit To Work is :">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Name" ColSpan="1" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblApprovalName" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="10px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="PTW Role" ShowCaption="False" ColSpan="1" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblApprovalRole" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="10px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Designation" ColSpan="1" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblApprovalDesignation" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Company" ColSpan="1" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblApprovalCompany" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Remarks" ColSpan="2" ColumnSpan="2" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxMemo ID="txtApprovalReamrks" runat="server" Width="100%">
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="2" ColumnSpan="2" ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxButton ID="btnApprovalApprove" runat="server" Text="Approve" Width="100px" OnClick="btnApprovalApprove_Click" ValidationGroup="Approve"></dx:ASPxButton>
<%--                                <dx:ASPxButton ID="btnApprovalReject" runat="server" Text="Reject" Width="100px" OnClick="btnApprovalReject_Click"></dx:ASPxButton>
                                <dx:ASPxButton ID="btnApprovalReturn" runat="server" Text="Return" Width="100px" OnClick="btnApprovalReturn_Click"></dx:ASPxButton>--%>
                                <%--<dx:ASPxButton ID="btnApprovalCancel" runat="server" Text="Back" Width="100px" CausesValidation="False" OnClick="btnBack_Click"></dx:ASPxButton>--%>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="10px" />
                    </dx:LayoutItem>
                </Items>
                <Paddings PaddingBottom="0px" PaddingLeft="10px" PaddingRight="10px" PaddingTop="0px" />
                <ParentContainerStyle>
                    <Paddings PaddingLeft="16px" />
                </ParentContainerStyle>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="" ColCount="2" ColSpan="1" ColumnCount="2" ShowCaption="False" Width="100%" Name="DailyInfo">
                <Border BorderStyle="Solid" BorderWidth="2px" BorderColor="DarkGray" />
                <Items>
                    <dx:LayoutItem Caption="" ColSpan="2" ColumnSpan="2" ShowCaption="False" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblDailyReviewStatus" runat="server" Text="Part 5: Daily Review by WAH Supervisor or Above" Font-Size="12" Font-Bold="True">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="5px" />
                        <ParentContainerStyle>
                            <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                        </ParentContainerStyle>
                    </dx:LayoutItem>
                    <dx:LayoutGroup Name="Day1" Caption="" ColCount="6" ColSpan="2" ColumnCount="6" ColumnSpan="2" ShowCaption="False" Width="100%">
                        <Items>
                            <dx:LayoutItem ColSpan="1" ShowCaption="False" VerticalAlign="Middle">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <table class="templateTable">
                                            <tr> 
                                                <td class="value">
                                                    <dx:ASPxLabel ID="lblday1" runat="server" Font-Bold="True" Text="Day 1" />
                                                </td>
                                                <td class="value" style="padding-left:5px; padding-right:0px;">
                                                    <dx:ASPxCheckBox ID="Day1Applicant" Text="Applicant" runat="server" Enabled="false"></dx:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day1ApplicationRemarks" Caption="Remarks" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxTextBox ID="txtDay1Applicant" runat="server" Width="95%">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day1ApplicationButton" Caption="" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxButton ID="btnDay1Applicant" runat="server" Text="Verify" Width="100px" CausesValidation="false" GroupName="1" OnClick="btnDayApplicant_Click">
                                        </dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ColSpan="1" ShowCaption="False" VerticalAlign="Middle">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <table class="templateTable">
                                            <tr> 
                                                <td class="value" style="padding-left:0px; padding-right:5px;">
                                                    <dx:ASPxCheckBox ID="Day1Assessor" Text="Assessor" runat="server" Enabled="false"></dx:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day1AssessorRemarks" Caption="Remarks" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxTextBox ID="txtDay1Assesser" runat="server" Width="95%">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day1AssessorButton" Caption="" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxButton ID="btnDay1Assesser" runat="server" Text="Verify" Width="100px" CausesValidation="false" GroupName="1" OnClick="btnDayAssesser_Click">
                                        </dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                    <dx:LayoutGroup Name="Day2" Caption="" ColCount="6" ColSpan="2" ColumnCount="6" ColumnSpan="2" ShowCaption="False" Width="100%">
                        <Items>
                            <dx:LayoutItem ColSpan="1" ShowCaption="False" VerticalAlign="Middle">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <table class="templateTable">
                                            <tr> 
                                                <td class="value">
                                                    <dx:ASPxLabel ID="lblday2" runat="server" Font-Bold="True" Text="Day 2" />
                                                </td>
                                                <td class="value" style="padding-left:5px; padding-right:0px;">
                                                    <dx:ASPxCheckBox ID="Day2Applicant" Text="Applicant" runat="server" Enabled="false"></dx:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day2ApplicationRemarks" Caption="Remarks" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxTextBox ID="txtDay2Applicant" runat="server" Width="95%">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day2ApplicationButton" Caption="" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxButton ID="btnDay2Applicant" runat="server" Text="Verify" Width="100px" CausesValidation="false" GroupName="2" OnClick="btnDayApplicant_Click">
                                        </dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ColSpan="1" ShowCaption="False" VerticalAlign="Middle">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <table class="templateTable">
                                            <tr> 
                                                <td class="value" style="padding-left:0px; padding-right:5px;">
                                                    <dx:ASPxCheckBox ID="Day2Assessor" Text="Assessor" runat="server" Enabled="false"></dx:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day2AssessorRemarks" Caption="Remarks" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxTextBox ID="txtDay2Assesser" runat="server" Width="95%">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day2AssessorButton" Caption="" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxButton ID="btnDay2Assesser" runat="server" Text="Verify" Width="100px" CausesValidation="false" GroupName="2" OnClick="btnDayAssesser_Click">
                                        </dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                    <dx:LayoutGroup Name="Day3" Caption="" ColCount="6" ColSpan="2" ColumnCount="6" ColumnSpan="2" ShowCaption="False" Width="100%">
                        <Items>
                            <dx:LayoutItem ColSpan="1" ShowCaption="False" VerticalAlign="Middle">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <table class="templateTable">
                                            <tr> 
                                                <td class="value">
                                                    <dx:ASPxLabel ID="lblday3" runat="server" Font-Bold="True" Text="Day 3" />
                                                </td>
                                                <td class="value" style="padding-left:5px; padding-right:0px;">
                                                    <dx:ASPxCheckBox ID="Day3Applicant" Text="Applicant" runat="server" Enabled="false"></dx:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day3ApplicationRemarks" Caption="Remarks" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxTextBox ID="txtDay3Applicant" runat="server" Width="95%">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day3ApplicationButton" Caption="" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxButton ID="btnDay3Applicant" runat="server" Text="Verify" Width="100px" CausesValidation="false" GroupName="3" OnClick="btnDayApplicant_Click">
                                        </dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ColSpan="1" ShowCaption="False" VerticalAlign="Middle">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <table class="templateTable">
                                            <tr> 
                                                <td class="value" style="padding-left:0px; padding-right:5px;">
                                                    <dx:ASPxCheckBox ID="Day3Assessor" Text="Assessor" runat="server" Enabled="false"></dx:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day3AssessorRemarks" Caption="Remarks" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxTextBox ID="txtDay3Assesser" runat="server" Width="95%">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day3AssessorButton" Caption="" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxButton ID="btnDay3Assesser" runat="server" Text="Verify" Width="100px" CausesValidation="false" GroupName="3" OnClick="btnDayAssesser_Click">
                                        </dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                    <dx:LayoutGroup Name="Day4" Caption="" ColCount="6" ColSpan="2" ColumnCount="6" ColumnSpan="2" ShowCaption="False" Width="100%">
                        <Items>
                            <dx:LayoutItem ColSpan="1" ShowCaption="False" VerticalAlign="Middle">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <table class="templateTable">
                                            <tr> 
                                                <td class="value">
                                                    <dx:ASPxLabel ID="lblday4" runat="server" Font-Bold="True" Text="Day 4" />
                                                </td>
                                                <td class="value" style="padding-left:5px; padding-right:0px;">
                                                    <dx:ASPxCheckBox ID="Day4Applicant" Text="Applicant" runat="server" Enabled="false"></dx:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day4ApplicationRemarks" Caption="Remarks" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxTextBox ID="txtDay4Applicant" runat="server" Width="95%">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day4ApplicationButton" Caption="" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxButton ID="btnDay4Applicant" runat="server" Text="Verify" Width="100px" CausesValidation="false" GroupName="4" OnClick="btnDayApplicant_Click">
                                        </dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ColSpan="1" ShowCaption="False" VerticalAlign="Middle">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <table class="templateTable">
                                            <tr> 
                                                <td class="value" style="padding-left:0px; padding-right:5px;">
                                                    <dx:ASPxCheckBox ID="Day4Assessor" Text="Assessor" runat="server" Enabled="false"></dx:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day4AssessorRemarks" Caption="Remarks" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxTextBox ID="txtDay4Assesser" runat="server" Width="95%">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day4AssessorButton" Caption="" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxButton ID="btnDay4Assesser" runat="server" Text="Verify" Width="100px" CausesValidation="false" GroupName="4" OnClick="btnDayAssesser_Click">
                                        </dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                    <dx:LayoutGroup Name="Day5" Caption="" ColCount="6" ColSpan="2" ColumnCount="6" ColumnSpan="2" ShowCaption="False" Width="100%">
                        <Items>
                            <dx:LayoutItem ColSpan="1" ShowCaption="False" VerticalAlign="Middle">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <table class="templateTable">
                                            <tr> 
                                                <td class="value">
                                                    <dx:ASPxLabel ID="lblday5" runat="server" Font-Bold="True" Text="Day 5" />
                                                </td>
                                                <td class="value" style="padding-left:5px; padding-right:0px;">
                                                    <dx:ASPxCheckBox ID="Day5Applicant" Text="Applicant" runat="server" Enabled="false"></dx:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day5ApplicationRemarks" Caption="Remarks" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxTextBox ID="txtDay5Applicant" runat="server" Width="95%">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day5ApplicationButton" Caption="" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxButton ID="btnDay5Applicant" runat="server" Text="Verify" Width="100px" CausesValidation="false" GroupName="5" OnClick="btnDayApplicant_Click">
                                        </dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ColSpan="1" ShowCaption="False" VerticalAlign="Middle">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <table class="templateTable">
                                            <tr> 
                                                <td class="value" style="padding-left:0px; padding-right:5px;">
                                                    <dx:ASPxCheckBox ID="Day5Assessor" Text="Assessor" runat="server" Enabled="false"></dx:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day5AssessorRemarks" Caption="Remarks" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxTextBox ID="txtDay5Assesser" runat="server" Width="95%">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day5AssessorButton" Caption="" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxButton ID="btnDay5Assesser" runat="server" Text="Verify" Width="100px" CausesValidation="false" GroupName="5" OnClick="btnDayAssesser_Click">
                                        </dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                    <dx:LayoutGroup Name="Day6" Caption="" ColCount="6" ColSpan="2" ColumnCount="6" ColumnSpan="2" ShowCaption="False" Width="100%">
                        <Items>
                            <dx:LayoutItem ColSpan="1" ShowCaption="False" VerticalAlign="Middle">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <table class="templateTable">
                                            <tr> 
                                                <td class="value">
                                                    <dx:ASPxLabel ID="lblday6" runat="server" Font-Bold="True" Text="Day 6" />
                                                </td>
                                                <td class="value" style="padding-left:5px; padding-right:0px;">
                                                    <dx:ASPxCheckBox ID="Day6Applicant" Text="Applicant" runat="server" Enabled="false"></dx:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day6ApplicationRemarks" Caption="Remarks" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxTextBox ID="txtDay6Applicant" runat="server" Width="95%">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day6ApplicationButton" Caption="" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxButton ID="btnDay6Applicant" runat="server" Text="Verify" Width="100px" CausesValidation="false" GroupName="6" OnClick="btnDayApplicant_Click">
                                        </dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ColSpan="1" ShowCaption="False" VerticalAlign="Middle">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <table class="templateTable">
                                            <tr> 
                                                <td class="value" style="padding-left:0px; padding-right:5px;">
                                                    <dx:ASPxCheckBox ID="Day6Assessor" Text="Assessor" runat="server" Enabled="false"></dx:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day6AssessorRemarks" Caption="Remarks" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxTextBox ID="txtDay6Assesser" runat="server" Width="95%">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day6AssessorButton" Caption="" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxButton ID="btnDay6Assesser" runat="server" Text="Verify" Width="100px" CausesValidation="false" GroupName="6" OnClick="btnDayAssesser_Click">
                                        </dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                    <dx:LayoutGroup Name="Day7" Caption="" ColCount="6" ColSpan="2" ColumnCount="6" ColumnSpan="2" ShowCaption="False" Width="100%">
                        <Items>
                            <dx:LayoutItem ColSpan="1" ShowCaption="False" VerticalAlign="Middle">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <table class="templateTable">
                                            <tr> 
                                                <td class="value">
                                                    <dx:ASPxLabel ID="lblday7" runat="server" Font-Bold="True" Text="Day 6" />
                                                </td>
                                                <td class="value" style="padding-left:5px; padding-right:0px;">
                                                    <dx:ASPxCheckBox ID="Day7Applicant" Text="Applicant" runat="server" Enabled="false"></dx:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day7ApplicationRemarks" Caption="Remarks" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxTextBox ID="txtDay7Applicant" runat="server" Width="95%">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day7ApplicationButton" Caption="" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxButton ID="btnDay7Applicant" runat="server" Text="Verify" Width="100px" CausesValidation="false" GroupName="7" OnClick="btnDayApplicant_Click">
                                        </dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ColSpan="1" ShowCaption="False" VerticalAlign="Middle">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <table class="templateTable">
                                            <tr> 
                                                <td class="value" style="padding-left:0px; padding-right:5px;">
                                                    <dx:ASPxCheckBox ID="Day7Assessor" Text="Assessor" runat="server" Enabled="false"></dx:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day7AssessorRemarks" Caption="Remarks" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxTextBox ID="txtDay7Assesser" runat="server" Width="95%">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="Day7AssessorButton" Caption="" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxButton ID="btnDay7Assesser" runat="server" Text="Verify" Width="100px" CausesValidation="false" GroupName="7" OnClick="btnDayAssesser_Click">
                                        </dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                </Items>
                <Paddings PaddingBottom="0px" PaddingLeft="10px" PaddingRight="10px" PaddingTop="0px" />
                <ParentContainerStyle>
                    <Paddings PaddingLeft="16px" />
                </ParentContainerStyle>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="" ColCount="2" ColSpan="1" ColumnCount="2" ShowCaption="False" Width="100%" Name="ClosedInfo">
                <Border BorderStyle="Solid" BorderWidth="2px" BorderColor="DarkGray" />
                <Items>
                    <dx:LayoutItem Caption="" ColSpan="2" ColumnSpan="2" ShowCaption="False" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblClosedStatus" runat="server" Text="Part 6: Notification of Work Completion (To be fill up by Permit Applicant)" Font-Bold="True" Font-Size="12">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="5px" />
                        <ParentContainerStyle>
                            <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                        </ParentContainerStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="2" HorizontalAlign="Left" ShowCaption="False" VerticalAlign="Top" ColumnSpan="2" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Notification of Work Completion (To be fill up by Permit Applicant)." Width="100%">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Name" ColSpan="1" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblClosedName" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="10px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="1" ShowCaption="False" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblClosedRole" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="10px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Designation" ColSpan="1" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblClosedDesignation" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Company" ColSpan="1" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblClosedCompany" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Remarks" ColSpan="2" ColumnSpan="2" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxMemo ID="txtClosedRemarks" runat="server" Width="100%">
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="2" ColumnSpan="2" ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxButton ID="btnClosedClose" runat="server" Text="Complete the Permit" Width="120px" OnClick="btnClosedClose_Click" ValidationGroup="Close"></dx:ASPxButton>
                                <%--<dx:ASPxButton ID="btnClosedCancel" runat="server" Text="Back" Width="100px" OnClick="btnBack_Click"></dx:ASPxButton>--%>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="10px" />
                    </dx:LayoutItem>
                </Items>
                <Paddings PaddingBottom="0px" PaddingLeft="10px" PaddingRight="10px" PaddingTop="0px" />
                <ParentContainerStyle>
                    <Paddings PaddingLeft="16px" />
                </ParentContainerStyle>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="" ColCount="2" ColSpan="1" ColumnCount="2" ShowCaption="False" Width="100%" Name="ClosureAcceptedInfo">
                <Border BorderStyle="Solid" BorderWidth="2px" BorderColor="DarkGray" />
                <Items>
                    <dx:LayoutItem Caption="" ColSpan="2" ColumnSpan="2" ShowCaption="False" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblAcceptedStatus" runat="server" Text="Part 7: Closure Review" Font-Bold="True" Font-Size="12">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="2" HorizontalAlign="Left" ShowCaption="False" VerticalAlign="Top" ColumnSpan="2" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" Width="100%">
                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Acknowledgement of activity completion by Main Constructor." Width="100%">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Name" ColSpan="1" VerticalAlign="Top" Width="50%"  CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblAcceptedName" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="10px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ShowCaption="False" ColSpan="1" VerticalAlign="Top" Width="50%" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblAcceptedRole" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="10px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Designation" ColSpan="1" VerticalAlign="Top" Width="50%" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblAcceptedDesignation" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Company" ColSpan="1" VerticalAlign="Top" Width="50%" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblAcceptedCompany" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Remarks" ColSpan="2" ColumnSpan="2" VerticalAlign="Top" Width="100%" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" Width="100%">
                                <dx:ASPxMemo ID="txtlblAcceptedRemarks" runat="server" Width="100%">
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />

<CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="2" ColumnSpan="2" ShowCaption="False" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxButton ID="btnAcceptedVerify" runat="server" Text="Review" Width="100px" OnClick="btnAcceptedVerify_Click" ValidationGroup="Review"></dx:ASPxButton>
                                <dx:ASPxButton ID="btnAcceptedCancel" runat="server" Text="Back" Width="100px" OnClick="btnBack_Click"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="10px" />
                    </dx:LayoutItem>
                </Items>
                <Paddings PaddingBottom="0px" PaddingLeft="10px" PaddingRight="10px" PaddingTop="0px" />
                <ParentContainerStyle>
                    <Paddings PaddingLeft="16px" />
                </ParentContainerStyle>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="" ColSpan="1" ShowCaption="False" Name="ReturnRejectGroup">
                <Border BorderStyle="Solid" BorderWidth="2px" BorderColor="DarkGray" />
                <Items>
                    <dx:LayoutItem Caption="" ColSpan="1" ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblReason" runat="server" Text="For Return, Reject & Revoke : Please specify the reason"></dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="10px" />
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="1" ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxTextBox ID="txtReason" runat="server" Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Please enter the Reason" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                        <RequiredField ErrorText="Please enter the Reason" IsRequired="True" />
                                    </ValidationSettings>
                                    <InvalidStyle BackColor="#FFE6EE">
                                    </InvalidStyle>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="1" ColumnSpan="1" ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <%--<dx:ASPxButton ID="ASPxButton13" runat="server" Text="Approve" Width="100px" ValidationGroup="Approve"></dx:ASPxButton>--%>
                                <dx:ASPxButton ID="btnReject" runat="server" Text="Reject" Width="100px" OnClick="btnReject_Click"></dx:ASPxButton>
                                <dx:ASPxButton ID="btnReturn" runat="server" Text="Return" Width="100px" OnClick="btnReturn_Click"></dx:ASPxButton>
                                <dx:ASPxButton ID="btnRevoke" runat="server" Text="Revoke" Width="100px" OnClick="btnRevoke_Click"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="10px" />
                    </dx:LayoutItem>
                </Items>
                <ParentContainerStyle>
                    <Paddings PaddingBottom="5px" PaddingLeft="16px" PaddingRight="0px" PaddingTop="0px" />
                </ParentContainerStyle>
            </dx:LayoutGroup>

            <dx:LayoutItem Caption="Location" ColSpan="1" ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxLabel ID="lblLocation" runat="server" Width="100%" Font-Size="8">
                        </dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
                <CaptionStyle Font-Bold="True">
                </CaptionStyle>
            </dx:LayoutItem>


        </Items>
    </dx:ASPxFormLayout>

    <dx:ASPxCallback ID="ASPxCallbackResult" runat="server" ClientInstanceName="callback" OnCallback="ASPxCallbackResult_Callback">
        <ClientSideEvents CallbackComplete="OnCallbackComplete" />
    </dx:ASPxCallback>
</asp:Content>
