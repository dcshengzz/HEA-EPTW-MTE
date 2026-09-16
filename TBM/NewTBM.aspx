<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="NewTBM.aspx.cs" Inherits="HEA.ePTW.TBM.NewTBM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/ePTW_TBM.css") %>' />    
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
            callback.PerformCallback(latitude + "|" + longitude);
        } 
        function onSelectedWorkChanged(s, e) {
            //if (s.GetValue() == "Others.")
            //    gvHazards.GetEditor("Others").SetVisible(true);
            //else
            //    gvHazards.GetEditor("Others").SetVisible(false);
        }
        function OnEquipmentEndCallback(s, e) {
            //cpSafetyCheckList.PerformCallback();
            //alert("A row was successfully updated!");
            //alert(e.CallbackName);
            //if (typeof(s.cpMessage) != 'undefined' && s.cpMessage !== null) {
                //alert(s.cpMessage);
                // Optional: delete the property to prevent it from showing on subsequent unrelated callbacks
                //delete s.cpMessage; 
            //}
        }
        function onToolbarItemClick(s, e) {
            alert("Custom Action Clicked");
            alert(e.item.name);
            if (e.item.name == "MutipleAttendees") {
                alert("Custom Action Clicked");
            }
        }

        function onWorkInit(s, e)
        {
        }
        function OnToolbarItemClick(s, e)
        {
            if (e.item.name == "MutipleAttendee") {
                pcMultiple.Show();
            }
        }
    </script> 
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PageContent" runat="server">
    <asp:HiddenField ID="hfLatitude" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfLongitude" runat="server" ClientIDMode="Static" />
    <dx:ASPxFormLayout runat="server" ID="flToolboxMeeting" CssClass="formLayout" ShowItemCaptionColon="False" RequiredMark="" Width="98%">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="650" />
        <Items>
            <dx:LayoutItem BackColor="#494949" ColSpan="1" ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxLabel ID="lblTitle" runat="server" Font-Bold="True" Font-Size="14pt" ForeColor="White" Text="My Toolbox Meeting">
                        </dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
            </dx:LayoutItem>
            <dx:LayoutItem ShowCaption="False" ColSpan="1">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxLabel ID="lblWorkflowError" runat="server" Visible="False" ForeColor="Red" />
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Team Name" ColSpan="1">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxLabel ID="lblProjectName" runat="server">
                        </dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="12px" />
                <CaptionStyle Font-Bold="True">
                </CaptionStyle>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Toolbox Meeting Date" ColSpan="1">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxDateEdit ID="dtMeetingDate" runat="server" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy" OnValidation="dtMeetingDate_Validation">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Please select the Valid Date" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                <RequiredField ErrorText="Please select the Valid Date" IsRequired="True" />
                            </ValidationSettings>
                            <InvalidStyle BackColor="#FFE6EE">
                            </InvalidStyle>
                        </dx:ASPxDateEdit>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="5px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="5px" />
                <CaptionStyle Font-Bold="True">
                </CaptionStyle>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Supervisor / Manager" ColSpan="1" Visible="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxTextBox ID="txtSupervisor" runat="server" MaxLength="100">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Please enter the Supervisor or Manager" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                <RequiredField ErrorText="Please enter the Supervisor or Manager" IsRequired="True" />
                            </ValidationSettings>
                            <InvalidStyle BackColor="#FFE6EE">
                            </InvalidStyle>
                        </dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="5px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="5px" />
                <CaptionStyle Font-Bold="True">
                </CaptionStyle>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Safety Supervisor" ColSpan="1" Visible="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxTextBox ID="txtSafety" runat="server" MaxLength="100">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Please enter the Safety Supervisor" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                <RequiredField ErrorText="Please enter the Safety Supervisor" IsRequired="True" />
                            </ValidationSettings>
                            <InvalidStyle BackColor="#FFE6EE">
                            </InvalidStyle>
                        </dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="5px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="5px" />
                <CaptionStyle Font-Bold="True">
                </CaptionStyle>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Work Description (AM)" ColSpan="1">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxMemo ID="txtDescription" runat="server" Height="100px" OnValidation="txtDescription_Validation">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Please enter the Description" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                <%--<RequiredField ErrorText="Please enter the Description" IsRequired="True" />--%>
                            </ValidationSettings>
                            <InvalidStyle BackColor="#FFE6EE">
                            </InvalidStyle>
                        </dx:ASPxMemo>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="5px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="5px" />
                <CaptionStyle Font-Bold="True">
                </CaptionStyle>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Work Description (PM)" ColSpan="1">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxMemo ID="txtDescriptionPM" runat="server" Height="100px" OnValidation="txtDescription_Validation">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Please enter the Description" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                <%--<RequiredField ErrorText="Please enter the Description" IsRequired="True" />--%>
                            </ValidationSettings>
                            <InvalidStyle BackColor="#FFE6EE">
                            </InvalidStyle>
                        </dx:ASPxMemo>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="5px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="5px" />
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
                                                    <dx:LayoutItem Caption="Remarks" Width="100%">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxMemo ID="txtRemarks" runat="server" Height="50px" Width="100%">
                                                                </dx:ASPxMemo>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                        <Paddings PaddingBottom="0px" PaddingLeft="25px" PaddingRight="10px" PaddingTop="0px" />
                                                        <CaptionStyle Font-Bold="False">
                                                        </CaptionStyle>
                                                    </dx:LayoutItem>
                                                </Items>
                                            </dx:ASPxFormLayout>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="20px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutItem Caption="" ColSpan="1" ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxPopupControl ID="pcMultiple" runat="server" Width="780" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
                            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcMultiple"
                            HeaderText="Add Multiple Attendee Records" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" AutoUpdatePosition="true" OnLoad="pcMultiple_Load" CloseAnimationType="None">
                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                            <ContentCollection>
                                <dx:PopupControlContentControl runat="server">
                                    <dx:ASPxPanel ID="Panel1" runat="server">
                                        <PanelCollection>
                                            <dx:PanelContent runat="server">
                                                <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout1" Width="100%" Height="100%">
                                                    <Items>
                                                        <dx:LayoutItem ColSpan="1" ShowCaption="False">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server">

                                                                    <dx:ASPxGridView ID="gvMultiple" runat="server" Width="100%">
                                                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="300" ShowHeaderFilterButton="True" />
                                                                        <SettingsBehavior MergeGroupsMode="Always" AutoExpandAllGroups="true" />
                                                                        <Settings ShowGroupPanel="true" ShowFooter="true" ShowGroupFooter="VisibleIfExpanded" />
                                                                        <SettingsPager Mode="ShowAllRecords" />
                                                                        <SettingsPopup>
                                                                            <FilterControl AutoUpdatePosition="False">
                                                                            </FilterControl>
                                                                        </SettingsPopup>
                                                                        <Columns>
                                                                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" />
                                                                            <dx:GridViewDataTextColumn FieldName="UserID" VisibleIndex="1">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn FieldName="FullName" VisibleIndex="1">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn FieldName="Position" VisibleIndex="1">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn FieldName="ConstructorName" VisibleIndex="1">
                                                                            </dx:GridViewDataTextColumn>
                                                                        </Columns>
                                                                    </dx:ASPxGridView>

                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem ShowCaption="False" Paddings-PaddingTop="19">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer>
                                                                    <dx:ASPxButton ID="btOK" runat="server" Text="OK" Width="80px" AutoPostBack="False" Style="float: left; margin-right: 8px">
                                                                        <ClientSideEvents Click="function(s, e) {
	pcMultiple.Hide();
}" />
                                                                    </dx:ASPxButton>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>

                                                            <Paddings PaddingTop="19px"></Paddings>
                                                        </dx:LayoutItem>
                                                    </Items>
                                                </dx:ASPxFormLayout>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxPanel>
                                </dx:PopupControlContentControl>
                            </ContentCollection>
                        </dx:ASPxPopupControl>
                        <dx:ASPxGridView ID="gvStaff" ClientInstanceName="gvStaff" runat="server" Width="100%"
                            KeyFieldName="UserID" EnablePagingGestures="False" AutoGenerateColumns="False"
                            Visible="True"
                            OnCellEditorInitialize="gvStaff_CellEditorInitialize"
                            OnRowValidating="gvStaff_RowValidating"
                            OnRowInserting="gvStaff_RowInserting"
                            OnRowDeleting="gvStaff_RowDeleting" ClientSideEvents-ToolbarItemClick="onToolbarItemClick">
                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                            <ClientSideEvents ToolbarItemClick="function(s, e) {
if (e.item.name == &quot;MutipleAttendees&quot;) 
     	{
                pcMultiple.Show();
}
}" />
                            <SettingsPager Visible="False">
                            </SettingsPager>
                            <Settings ShowTitlePanel="True" />
                            <SettingsBehavior AllowFocusedRow="true" AllowSelectByRowClick="true" AllowDragDrop="false" AllowSelectSingleRowOnly="True" />
                            <SettingsPopup>
                                <FilterControl AutoUpdatePosition="False"></FilterControl>
                            </SettingsPopup>
                            <SettingsText EmptyDataRow=" " Title="Attendee Records" />
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
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Contractor Name">
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
                                <dx:GridViewDataTextColumn FieldName="ConstructorName" VisibleIndex="3" Caption="Contractor Name">
                                    <PropertiesTextEdit ClientInstanceName="txtConstructorName">
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Toolbars>
                                <dx:GridViewToolbar Position="Bottom">
                                    <Items>
                                        <dx:GridViewToolbarItem Command="New" Text="Add Attendee">
                                        </dx:GridViewToolbarItem>
                                        <dx:GridViewToolbarItem Command="Delete" Text="Remove Attendee">
                                        </dx:GridViewToolbarItem>
                                        <dx:GridViewToolbarItem Text="Add Multiple Attendees" Name="MutipleAttendees" Visible="False" Command="Custom">
                                            <Image IconID="businessobjects_bo_department_svg_16x16">
                                            </Image>
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
            <dx:LayoutItem Caption="" ColSpan="1" ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxGridView ID="gvHazards" ClientInstanceName="gvHazards" runat="server" 
                            KeyFieldName="WorkActivity" EnablePagingGestures="False" AutoGenerateColumns="False"
                            OnCellEditorInitialize="gvHazards_CellEditorInitialize"
                            OnRowValidating="gvHazards_RowValidating"
                            OnRowInserting="gvHazards_RowInserting"
                            OnRowDeleting="gvHazards_RowDeleting" OnRowUpdating="gvHazards_RowUpdating">
                            <SettingsBehavior AllowFocusedRow="true" AllowSelectByRowClick="true" AllowDragDrop="false" AllowSelectSingleRowOnly="True" />
                            <SettingsPager Visible="False">
                            </SettingsPager>
                            <Settings ShowTitlePanel="True" />
                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                            <SettingsPopup>
                                <FilterControl AutoUpdatePosition="False"></FilterControl>
                            </SettingsPopup>
                            <SettingsText EmptyDataRow=" " Title="Hazard / Issue Records" />
                            <EditFormLayoutProperties ShowItemCaptionColon="False" ColCount="2" ColumnCount="2">
                                <Items>
                                    <dx:GridViewColumnLayoutItem ColSpan="2" ColumnName="Work Activity" ColumnSpan="2">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:EmptyLayoutItem ColSpan="2" ColumnSpan="2">
                                    </dx:EmptyLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="2" ColumnName="Others" Caption="Others, Please specify" ColumnSpan="2">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:EmptyLayoutItem ColSpan="2" Height="10px" ColumnSpan="2">
                                    </dx:EmptyLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="2" ColumnName="What cause the hazard?" ColumnSpan="2">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:EmptyLayoutItem ColSpan="2" Height="10px" ColumnSpan="2">
                                    </dx:EmptyLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="2" ColumnName="What happen as a result?" ColumnSpan="2">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:EmptyLayoutItem ColSpan="2" Height="15px" ColumnSpan="2">
                                    </dx:EmptyLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="2" ColumnName="Action To Be Taken" ColumnSpan="2">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:EmptyLayoutItem ColSpan="2" ColumnSpan="2" Height="15px">
                                    </dx:EmptyLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColSpan="2" ColumnName="Remarks for Actions" ColumnSpan="2">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:EmptyLayoutItem ColSpan="2" ColumnSpan="2" Height="15px">
                                    </dx:EmptyLayoutItem>
                                    <dx:EditModeCommandLayoutItem ColSpan="2" HorizontalAlign="Right" ColumnSpan="2">
                                    </dx:EditModeCommandLayoutItem>
                                </Items>
                            </EditFormLayoutProperties>
                            <Columns>
                                <dx:GridViewDataComboBoxColumn FieldName="WorkActivity" ShowInCustomizationForm="True" VisibleIndex="0">
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataMemoColumn FieldName="CauseOfHazard" ShowInCustomizationForm="True" VisibleIndex="2" Caption="What cause the hazard?">
                                </dx:GridViewDataMemoColumn>
                                <dx:GridViewDataMemoColumn Caption="What happen as a result?" FieldName="HappenAsResult" ShowInCustomizationForm="True" VisibleIndex="3">
                                </dx:GridViewDataMemoColumn>
                                <dx:GridViewDataTextColumn FieldName="Others" ShowInCustomizationForm="True" VisibleIndex="1">
                                    <PropertiesTextEdit>
                                        <ValidationSettings CausesValidation="True" Display="Dynamic" SetFocusOnError="True">
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Action To Be Taken" FieldName="ActionToTaken" ShowInCustomizationForm="True" VisibleIndex="4">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Text="Priority 1: Measures with environment and facilities (Elimination, Subsitution, Engineering Control)" Value="Priority 1: Measures with environment and facilities (Elimination, Subsitution, Engineering Control)" />
                                            <dx:ListEditItem Text="Priority 2: Measures by humans and in action (Adminstrative control)" Value="Priority 2: Measures by humans and in action (Adminstrative control)" />
                                            <dx:ListEditItem Text="Priority 3: Measures with protective equipment (Personal Protective Equipment)" Value="Priority 3: Measures with protective equipment (Personal Protective Equipment)" />
                                        </Items>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataMemoColumn Caption="Remarks for Actions" FieldName="ActionRemarks" ShowInCustomizationForm="True" VisibleIndex="5">
                                </dx:GridViewDataMemoColumn>
                            </Columns>
                            <Toolbars>
                                <dx:GridViewToolbar Position="Bottom">
                                    <Items>
                                        <dx:GridViewToolbarItem Command="New" Text="New Record">
                                        </dx:GridViewToolbarItem>
                                        <dx:GridViewToolbarItem Command="Edit" Text="Update Record">
                                        </dx:GridViewToolbarItem>
                                        <dx:GridViewToolbarItem Command="Delete" Text="Remove Record">
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
            <dx:LayoutItem Caption="Today Team Action Goal" ColSpan="1">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxMemo ID="txtTodayTeamActionGoal" runat="server" Height="100px">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Please enter the Today Team Action Goal" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                <RequiredField ErrorText="Please enter the Today Team Action Goal" IsRequired="True" />
                            </ValidationSettings>
                            <InvalidStyle BackColor="#FFE6EE">
                            </InvalidStyle>
                        </dx:ASPxMemo>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="5px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="5px" />
                <CaptionStyle Font-Bold="True">
                </CaptionStyle>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Today Touch and Call" ColSpan="1">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxMemo ID="txtTodayTouchAndCall" runat="server" Height="100px">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Please enter the Today Touch and Call" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                <RequiredField ErrorText="Please enter the Today Touch and Call" IsRequired="True" />
                            </ValidationSettings>
                            <InvalidStyle BackColor="#FFE6EE">
                            </InvalidStyle>
                        </dx:ASPxMemo>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="5px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="5px" />
                <CaptionStyle Font-Bold="True">
                </CaptionStyle>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Feedback from worker at site" ColSpan="1">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxMemo ID="txtFeedback" runat="server" Height="100px">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Please enter the Feedback" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                <RequiredField ErrorText="Please enter the Feedback" IsRequired="True" />
                            </ValidationSettings>
                            <InvalidStyle BackColor="#FFE6EE">
                            </InvalidStyle>
                        </dx:ASPxMemo>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="5px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="5px" />
                <CaptionStyle Font-Bold="True">
                </CaptionStyle>
            </dx:LayoutItem>


<%--            <dx:LayoutItem Name="ReturnRejectControl" Caption="If want to Return or Reject, please specify the Reason :-" ColSpan="1" ShowCaption="True">
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
            </dx:LayoutItem>--%>

            <dx:LayoutItem ColSpan="1" ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxGridView ID="gvEquipment" ClientInstanceName="gvEquipment" runat="server" Width="100%" Visible="True"
                            EnablePagingGestures="False" KeyFieldName="RegistrationNo"
                            OnCellEditorInitialize="gvEquipment_CellEditorInitialize"
                            OnRowValidating="gvEquipment_RowValidating"
                            OnRowInserting="gvEquipment_RowInserting" 
                            OnRowDeleting="gvEquipment_RowDeleting"
                            AutoGenerateColumns="False">
                            <SettingsBehavior AllowFocusedRow="true" AllowSelectByRowClick="true" AllowDragDrop="false" AllowSelectSingleRowOnly="True" />
                            <ClientSideEvents EndCallback="function(s, e) {
OnEquipmentEndCallback();
}" />
                            <SettingsPager Visible="False">
                            </SettingsPager>
                            <Settings ShowTitlePanel="True" />
                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                            <SettingsPopup>
                                <FilterControl AutoUpdatePosition="False"></FilterControl>
                            </SettingsPopup>
                            <SettingsText EmptyDataRow=" " Title="Unit No / Manufacturing Number" />
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
                                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="Machine Type">
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
                                <dx:GridViewDataComboBoxColumn FieldName="MachineType" ShowInCustomizationForm="True" VisibleIndex="3">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Text="Elevator" Value="E" />
                                            <dx:ListEditItem Text="Escalator" Value="P" />
                                        </Items>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
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
                <Paddings PaddingBottom="0px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="5px" />
            </dx:LayoutItem>
            <dx:LayoutGroup Name="SafetyCheckList" Caption="" ColSpan="1" ShowCaption="False" ColCount="2" ColumnCount="2">
                <Items>
                    <dx:LayoutItem Caption="" ColSpan="2" ShowCaption="False" ColumnSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxCallbackPanel ID="cpSafetyCheckList" ClientInstanceName="cpSafetyCheckList" runat="server" Width="98%" OnCallback="cpSafetyCheckList_Callback">
                                    <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent4" runat="server">
                                            <dx:ASPxFormLayout ID="flCheckListGroup" runat="server" ShowItemCaptionColon="False" RequiredMark="" RequiredMarkDisplayMode="None" Width="100%">
                                                <Items>
                                                    <dx:LayoutGroup Name="SafetyCheckListQA" Caption="" ColSpan="1" ShowCaption="False" ColCount="1" ColumnCount="1" Width="100%">
                                                        <Items>
                                                        </Items>
                                                    </dx:LayoutGroup>
                                                </Items>
                                            </dx:ASPxFormLayout>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                                <dx:ASPxCallbackPanel ID="cpSafetyCheckListSelect" ClientInstanceName="cpSafetyCheckListSelect" runat="server" Width="98%" OnCallback="cpSafetyCheckListSelect_Callback">
                                    <PanelCollection>
<dx:PanelContent runat="server"></dx:PanelContent>
</PanelCollection>
                                </dx:ASPxCallbackPanel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup BackColor="Transparent" Caption="" ColSpan="1" ShowCaption="False" Name="AttachmentFileControl">
                <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                <Items>
                    <dx:LayoutItem Caption="" ColSpan="1" ShowCaption="False" Name="UploadFilePanel" RequiredMarkDisplayMode="Hidden">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxUploadControl ID="UploadFile" ClientInstanceName="UploadFile" runat="server" Width="100%" OnFileUploadComplete="UploadFileControl_FileUploadComplete" AutoStartUpload="true" UploadMode="Auto" ShowTextBox="True" ShowProgressPanel="True" RightToLeft="True" BrowseButton-Text="Browse File" TextBoxStyle-HorizontalAlign="Left" BrowseButtonStyle-BackColor="#4F81BD" BrowseButtonStyle-ForeColor="White">

<BrowseButton Text="Browse File"></BrowseButton>

                                    <AdvancedModeSettings EnableMultiSelect="False" EnableFileList="False" EnableDragAndDrop="True" />
                                    <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".jpg,.jpeg,.gif,.png,.pdf">
                                    </ValidationSettings>
                                    <ClientSideEvents FileUploadComplete="OnFileUploadComplete" />

<TextBoxStyle HorizontalAlign="Left"></TextBoxStyle>

<BrowseButtonStyle BackColor="#4F81BD" ForeColor="White"></BrowseButtonStyle>
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
            <dx:LayoutGroup Caption="" ColCount="2" ColSpan="1" ColumnCount="2" ShowCaption="False" Width="100%" Name="SubmitInfo">
                <Border BorderStyle="None" BorderWidth="0px" />
                <Items>
                    <dx:LayoutItem Caption="" ColSpan="2" ColumnSpan="2" ShowCaption="False" Width="100%" Visible="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblSubmitStatus" runat="server" Text="Applicant Details" Font-Bold="True" Font-Size="12" Font-Underline="False">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="5px" />
                        <ParentContainerStyle>
                            <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                        </ParentContainerStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="2" HorizontalAlign="Left" ShowCaption="False" VerticalAlign="Top" ColumnSpan="2" Width="100%" Visible="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblSubmitNote" runat="server" Text="I will ensure that the check items specified in the Key Points Confirmation Sheet are thoroughly implemented, and I will fulfill my responsibilities as the person in-charge of the work as outlined below." Visible="True">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="2" HorizontalAlign="Left" ShowCaption="False" VerticalAlign="Top" ColumnSpan="2" Width="100%" Visible="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Check the each box below for confirmation :-" Visible="True">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="2" HorizontalAlign="Left" ShowCaption="False" VerticalAlign="Top" ColumnSpan="2" Width="100%" Visible="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxCheckBox ID="cbDeLine1" runat="server" Text="Before start of work, I have explained and provided instructions on the specific details of the day’s work location, work content and procedures, worker assignments and roles, as well as potential
 hazards and countermeasures. I will also confirm that all team members have understood these points before starting the work." OnValidation="cbDeLine_Validation">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Please enter the Description" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                    </ValidationSettings>
                                    <InvalidStyle BackColor="#FFE6EE">
                                    </InvalidStyle>
                                </dx:ASPxCheckBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="2" HorizontalAlign="Left" ShowCaption="False" VerticalAlign="Top" ColumnSpan="2" Width="100%" Visible="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxCheckBox ID="cbDeLine2" runat="server" Text="During the work, I will supervise and ensure that all personnel confirm their safety at all times before proceeding to the next action." OnValidation="cbDeLine_Validation">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Please enter the Description" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                    </ValidationSettings>
                                    <InvalidStyle BackColor="#FFE6EE">
                                    </InvalidStyle>
                                </dx:ASPxCheckBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="2" HorizontalAlign="Left" ShowCaption="False" VerticalAlign="Top" ColumnSpan="2" Width="100%" Visible="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxCheckBox ID="cbDeLine3" runat="server" Text="If the work cannot be carried out safely, I will suspend the work and ensure that it is reported to my supervisor (line manager)." OnValidation="cbDeLine_Validation">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Please enter the Description" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                    </ValidationSettings>
                                    <InvalidStyle BackColor="#FFE6EE">
                                    </InvalidStyle>
                                </dx:ASPxCheckBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="2" HorizontalAlign="Left" ShowCaption="False" VerticalAlign="Top" ColumnSpan="2" Width="100%" Visible="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxCheckBox ID="cbDeLine4" runat="server" Text="I will ensure that no one is exposed to danger and that the work is completed safely from start to finish." OnValidation="cbDeLine_Validation">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Please enter the Description" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                    </ValidationSettings>
                                    <InvalidStyle BackColor="#FFE6EE">
                                    </InvalidStyle>
                                </dx:ASPxCheckBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Name" ColSpan="1" VerticalAlign="Top" CaptionStyle-Font-Bold="true" Visible="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblSubmitName" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="10px" />
                        <CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ShowCaption="False" ColSpan="1" VerticalAlign="Top" CaptionStyle-Font-Bold="true" Visible="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblSubmitRole" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="10px" />
                        <CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Designation" ColSpan="1" VerticalAlign="Top" CaptionStyle-Font-Bold="true" Visible="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblSubmitDesignation" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />
                        <CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Company Name" ColSpan="1" VerticalAlign="Top" CaptionStyle-Font-Bold="true" Visible="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblSubmitCompany" runat="server">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />
                        <CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="2" ColumnSpan="2" ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxButton ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" Width="100px"></dx:ASPxButton>
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
                <Paddings PaddingBottom="10px" PaddingLeft="10px" PaddingRight="10px" PaddingTop="0px" />
                <ParentContainerStyle>
                    <Paddings PaddingLeft="16px" PaddingRight="16px" />
                </ParentContainerStyle>
                <CaptionCellStyle>
                    <Paddings PaddingBottom="5px" />
                </CaptionCellStyle>
                <CaptionStyle Font-Bold="True">
                </CaptionStyle>
            </dx:LayoutItem>
            <dx:LayoutGroup Caption="" ColCount="2" ColSpan="1" ColumnCount="2" ShowCaption="False" Width="100%" Name="ApprovalInfo">
                <Border BorderStyle="Solid" BorderWidth="2px" BorderColor="DarkGray" />
                <Items>
                    <dx:LayoutItem Caption="" ColSpan="2" ColumnSpan="2" ShowCaption="False" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxLabel ID="lblApprovalStatus" runat="server" Text="Part 2: Approval by HEA Project Manager / Authorized Competent Person" Font-Size="12" Font-Bold="True">
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
                                <dx:ASPxLabel ID="lblApprovalNote" runat="server" Text="">
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
                    <dx:LayoutItem Caption="Company Name" ColSpan="1" VerticalAlign="Top" CaptionStyle-Font-Bold="true">
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
                                <dx:ASPxMemo ID="txtApprovalRemarks" runat="server" Width="100%">
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <Paddings PaddingBottom="5px" PaddingTop="5px" />
                        <CaptionStyle Font-Bold="True"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="2" ColumnSpan="2" ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxButton ID="btnApprove" runat="server" Width="120px" Text="Approve" OnClick="btnApprove_Click" CausesValidation="False"></dx:ASPxButton>
                                <dx:ASPxButton ID="btnReject" runat="server" Width="120px" Text="Reject" OnClick="btnReject_Click"></dx:ASPxButton>
                                <dx:ASPxButton ID="btnReturn" runat="server" Width="120px" Text="Return" OnClick="btnReturn_Click"></dx:ASPxButton>
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
            <dx:LayoutItem Caption="Location" ColSpan="1" ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxLabel ID="lblLocation" runat="server" Width="100%" Font-Size="8">
                        </dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="5px" PaddingLeft="16px" PaddingRight="8px" PaddingTop="5px" />
                <CaptionStyle Font-Bold="True">
                </CaptionStyle>
            </dx:LayoutItem>

            <dx:LayoutItem Caption="" ColSpan="1" ShowCaption="False" Visible="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <div>
                            <h2>Consent Form for Video Surveillance</h2>
                            <p>This consent form is intended to inform individuals entering the construction site of Hitachi Elevator Asia Pte Ltd project that video surveillance is in operation. The purpose of surveillance is to ensure workplace safety and health, monitor site activities, investigate incidents, training, processes review and future AI Analytics. This follows the Personal Data Protection Act (PDPA) of Singapore.</p>
                            <h3>Consent Statement</h3> 
                            <p>I hereby acknowledge and consent to the collection of my personal data through video surveillance while I am present at the construction site. I understand that the footage may be used for the purposes stated above and will be handled in accordance with the PDPA.</p>
                            <h3>Purpose of Surveillance</h3> 
                            <ul>
                                <li style="margin-top: 2px; margin-bottom: 2px;">Ensuring workplace safety and health (WSH)</li>
                                <li style="margin-top: 2px; margin-bottom: 2px;">Monitoring site activities</li>
                                <li style="margin-top: 2px; margin-bottom: 2px;">Investigating incidents</li>
                                <li style="margin-top: 2px; margin-bottom: 2px;">Training</li>
                                <li style="margin-top: 2px; margin-bottom: 2px;">Review of processes</li>
                                <li style="margin-top: 2px; margin-bottom: 2px;">Future AI analytics</li>
                            </ul>
                            <h3>Data Protection Officer Contact</h3> 
                            Name: Ng Jim Wei<br>
                            Email: jimwei.ng.hu@hitachi.com<br>
                            Phone: +65 6416 1711<br>
                        </div>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <Paddings PaddingBottom="10px" PaddingLeft="10px" PaddingTop="2px" />
            </dx:LayoutItem>


        </Items>
    </dx:ASPxFormLayout>
    <dx:ASPxCallback ID="ASPxCallbackResult" runat="server" ClientInstanceName="callback" OnCallback="ASPxCallbackResult_Callback">
        <ClientSideEvents CallbackComplete="OnCallbackComplete" />
    </dx:ASPxCallback>
</asp:Content>
