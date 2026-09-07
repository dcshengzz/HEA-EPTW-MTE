<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="NewCCP.aspx.cs" Inherits="HEA.ePTW.CCP.NewCCP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/ePTW_CCP.css") %>' />  
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/ePTW_CCP.js") %>'></script>

    <%--<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?sensor=false"></script>--%>
    <%--<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false&libraries=places"></script>--%>
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
    <dx:ASPxFormLayout runat="server" ID="flCCP" CssClass="formLayout" ShowItemCaptionColon="False" RequiredMark="" Width="98%">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="650" />
        <Items>
            <dx:LayoutItem BackColor="#494949" ColSpan="1" ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxLabel ID="lblTitle" runat="server" Font-Bold="True" Font-Size="14pt" ForeColor="White" Text="Compliance Check Points">
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
            <dx:LayoutItem Caption="Key Activities" ColSpan="1">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxComboBox ID="cbKey" runat="server" ValueType="System.String" DropDownStyle="DropDownList" IncrementalFilteringMode="Contains" EnableCallbackMode="true" ValueField="Name" TextFormatString="{0}" Width="100%" OnCustomFiltering="cbKey_CustomFiltering" ItemStyle-Wrap="True" OnSelectedIndexChanged="cbKey_SelectedIndexChanged" AutoPostBack="True">
                            <Columns>
                                <dx:ListBoxColumn FieldName="Name" Width="30px" />
                                <dx:ListBoxColumn FieldName="Description" />
                            </Columns>
                            <ItemStyle Wrap="True"></ItemStyle>
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Please select the Key Activities" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                <RequiredField ErrorText="Please select the Key Activities" IsRequired="True" />
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
            <dx:LayoutItem Caption="Date" ColSpan="1">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxDateEdit ID="dtDate" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" UseMaskBehavior="True">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Please select the Date" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                <RequiredField ErrorText="Please select the Date" IsRequired="True" />
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
            <dx:LayoutItem Caption="Activities Description" ColSpan="1">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxMemo ID="txtDescription" runat="server" Height="100px" Width="100%">
                        </dx:ASPxMemo>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
                <CaptionStyle Font-Bold="True">
                </CaptionStyle>
            </dx:LayoutItem>
            <dx:LayoutGroup Name="QuestionsAndAnswer" Caption="" ColSpan="1" ShowCaption="False" ColCount="2" ColumnCount="2" Width="100%">
                <Items>
                    <dx:LayoutItem Caption="" ColSpan="2" ShowCaption="False" ColumnSpan="2" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxCallbackPanel ID="QuestionsAndAnswerCallbackPanel" ClientInstanceName="QuestionsAndAnswerCallbackPanel" runat="server" Width="100%" OnCallback="QuestionsAndAnswerCallbackPanel_Callback">
                                    <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent3" runat="server">
                                            <dx:ASPxFormLayout ID="FormLayoutQNA" runat="server" ShowItemCaptionColon="False" RequiredMark="" RequiredMarkDisplayMode="None" Width="100%">
                                                <Items>
                                                    <dx:LayoutGroup Name="QuestionsAndAnswer" Caption="" ColSpan="1" ShowCaption="False" ColCount="2" ColumnCount="2" Width="100%">
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
                                        <dx:PanelContent ID="PanelContent2" runat="server">
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
            <dx:LayoutItem Name="ReturnRejectControl" Caption="If want to Return or Reject, please specify the Reason :-" ColSpan="1" ShowCaption="True">
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
                <CaptionSettings Location="Top" />
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
                <CaptionCellStyle>
                    <Paddings PaddingBottom="5px" />
                </CaptionCellStyle>
                <CaptionStyle Font-Bold="True">
                </CaptionStyle>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="" ColSpan="1" ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxButton ID="btnSubmit" runat="server" Text="Submit" Width="100px" OnClick="btnSubmit_Click"></dx:ASPxButton>
                        <dx:ASPxButton ID="btnDelete" runat="server" Text="Delete" Width="100px" OnClick="btnDelete_Click"></dx:ASPxButton>
                        <dx:ASPxButton ID="btnCancel" runat="server" Text="Cancel" Width="100px" CausesValidation="False" OnClick="btnCancel_Click"></dx:ASPxButton>
                        <dx:ASPxButton ID="btnApprove" runat="server" Text="Approve" Width="100px" OnClick="btnApprove_Click" CausesValidation="False"></dx:ASPxButton>
                        <dx:ASPxButton ID="btnReject" runat="server" Text="Reject" Width="100px" OnClick="btnReject_Click"></dx:ASPxButton>
                        <dx:ASPxButton ID="btnReturn" runat="server" Text="Return" Width="100px" OnClick="btnReturn_Click"></dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="0px" />
            </dx:LayoutItem>
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
