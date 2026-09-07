using DevExpress.Data.Filtering;
using DevExpress.Web;
using HEA.ePTW.Models;
using HEA.ePTW.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HEA.ePTW.TBM
{
    public partial class NewTBM : System.Web.UI.Page
    {
        string strKey = Guid.NewGuid().ToString();
        List<AttachmentModel> attachlist;
        List<KeyActivitiesModel> keyactivitieslist;
        List<TBMAttendeeModel> tbmattendeelist;
        List<TBMHazardModel> hazardlist;
        List<UserModel> alluserlist;
        List<QuestionAndAnswerModel> templatedetaillist;
        List<TBMEquipmentModel> tbmequipmentlist;
        //List<EquipmentModel> equipmentlist;
        List<QuestionAndAnswerModel> safetydetaillist;

        TBMModel master;

        private void RetrieveFromQueryString()
        {
            string strTask = (Request.QueryString["Status"] == null) ? "" : Request.QueryString["Status"].ToString();
            if (strTask == "N")
            {
                Session["TBM_Record_IMG"] = null;
                Session["TBM_KEYACTLIST"] = null;
                //Session["TBM_Record_DOC"] = null;
                Session["TBM_Record"] = null;
                Session["TBM_Hazard"] = null;
                Session["TBM_Attendee"] = null;
            }
            Session["TBM_Task"] = strTask;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UserModel user = UserViewModel.GetLoggedInUserInfo();
                var project = UserViewModel.GetSelectedProject();
                lblProjectName.Text = project != null ? project.ToString() : "";

                if (!Page.IsPostBack)
                {
                    RetrieveFromQueryString();

                    keyactivitieslist = KeyActivitiesViewModel.GetKeyActivitiesList("TBM");

                    if (Session["TBM_Record"] != null)
                    {
                        master = (TBMModel)Session["TBM_Record"];
                        attachlist = AttachmentViewModel.GetAttachmentList(master.Key); 
                        tbmattendeelist = TBMAttendeeViewModel.GetTBMAttendeeList(master.Key);
                        hazardlist = TBMHazardViewModel.GetTBMHazardList(master.Key);
                        templatedetaillist = TBMViewModel.GetTBMDetails(master.Key);
                        tbmequipmentlist = TBMEquipmentViewModel.GetTBMEquipmentList(master.Key);
                        safetydetaillist = TBMViewModel.GetTBMChecklist(master.Key);
                    }
                    else
                    {
                        master = new TBMModel();
                        master.Key = strKey;
                        master.ProjectName = project != null ? project.ToString() : "";
                        master.ConductedBy = user.UserID;
                        master.ConductedByName = user.FullName;
                        master.ConductedCompany = user.ConstructorName;
                        master.ConductedPosition = user.Position;
                        master.MeetingDate = DateTime.Now;
                        Session["TBM_Record"] = master;
                        attachlist = new List<AttachmentModel>();
                        tbmattendeelist = new List<TBMAttendeeModel>();
                        hazardlist = new List<TBMHazardModel>();
                        tbmequipmentlist = new List<TBMEquipmentModel>();
                        templatedetaillist = TemplateViewModel.GetTemplateDetails("TBM");
                        safetydetaillist = new List<QuestionAndAnswerModel>();
                    }

                    alluserlist = UserViewModel.GetUser_CodeForModule(project.ToString());
                    if (alluserlist != null) alluserlist = alluserlist.Where(item => item.ConstructorName == user.ConstructorName).ToList();

                    Session["TBM_TemplateDetails"] = templatedetaillist;
                    Session["TBM_Record_IMG"] = attachlist;
                    Session["TBM_KEYACTLIST"] = keyactivitieslist;
                    Session["TBM_Attendee"] = tbmattendeelist;
                    Session["TBM_Hazard"] = hazardlist;
                    Session["TBM_UserList"] = alluserlist;
                    Session["TBM_Equipment"] = tbmequipmentlist;
                    Session["TBM_SafetyDetails"] = safetydetaillist;

                    BindMaster();
                }
                else
                {
                    attachlist = (List<AttachmentModel>)Session["TBM_Record_IMG"];
                    keyactivitieslist = (List<KeyActivitiesModel>)Session["TBM_KEYACTLIST"];
                    tbmattendeelist = (List<TBMAttendeeModel>)Session["TBM_Attendee"];
                    hazardlist = (List<TBMHazardModel>)Session["TBM_Hazard"];
                    alluserlist = (List<UserModel>)Session["TBM_UserList"];
                    templatedetaillist = (List<QuestionAndAnswerModel>)Session["TBM_TemplateDetails"];
                    safetydetaillist = (List<QuestionAndAnswerModel>)Session["TBM_SafetyDetails"];
                    tbmequipmentlist = (List<TBMEquipmentModel>)Session["TBM_Equipment"];
                    master = (TBMModel)Session["TBM_Record"];
                }

                //Show Title & Location
                string strLatitude = hfLatitude.Value;
                string strLongitude = hfLongitude.Value;
                string strLocation = "";
                switch (master.Status)
                {
                    case 0:
                        lblTitle.Text = "Toolbox Meeting - New";
                        if (strLatitude != "") strLocation = $"Location : Latitude {strLatitude}, Longitude {strLongitude}";
                        break;
                    case 1:
                        lblTitle.Text = "Toolbox Meeting - Submitted";
                        if (master.Latitude != "") strLocation = $"Location : Latitude {master.Latitude}, Longitude {master.Longitude}";
                        break;
                    case 2:
                        lblTitle.Text = "Toolbox Meeting - Approved";
                        if (master.Latitude != "") strLocation = $"Location : Latitude {master.Latitude}, Longitude {master.Longitude}";
                        break;
                    case 98:
                        lblTitle.Text = "Toolbox Meeting - Returned";
                        if (strLatitude != "") strLocation = $"Location : Latitude {strLatitude}, Longitude {strLongitude}";
                        break;
                    case 99:
                        lblTitle.Text = "Toolbox Meeting - Rejected";
                        if (master.Latitude != "") strLocation = $"Location : Latitude {master.Latitude}, Longitude {master.Longitude}";
                        break;
                }
                lblLocation.Text = strLocation;


                if (templatedetaillist != null)
                {
                    ClearQuestionAndAnswer();
                    foreach (QuestionAndAnswerModel ent in templatedetaillist)
                        AddEnableQuestionAnswer(ent, master.Status);
                }

                var uploadfilegroup = flToolboxMeeting.FindItemOrGroupByName("AttachmentFileControl");
                if (uploadfilegroup != null)
                {
                    var UploadFilePanel = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("UploadFilePanel");
                    if (UploadFilePanel != null) (UploadFilePanel as LayoutItem).Visible = false;
                    var UploadFileNote = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("UploadFileNote");
                    if (UploadFileNote != null) (UploadFileNote as LayoutItem).Visible = false;
                    var EditDocument = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("EditAttachmentDoc");
                    if (EditDocument != null) (EditDocument as LayoutItem).Visible = false;
                    var DisplayImage = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("DisplayImage");
                    if (DisplayImage != null) (DisplayImage as LayoutItem).Visible = false;
                }
                var returnrejectgroup = flToolboxMeeting.FindItemOrGroupByName("ReturnRejectControl");
                if (returnrejectgroup != null) (returnrejectgroup as LayoutItem).Visible = false;
                var approvalgroup = flToolboxMeeting.FindItemOrGroupByName("ApprovalInfo");
                if (approvalgroup != null) (approvalgroup as LayoutGroup).Visible = false;

                btnSubmit.Visible = false;
                //btnCancel.Visible = false;
                //btnDelete.Visible = false;
                btnApprove.Visible = false;
                btnReturn.Visible = false;
                btnReject.Visible = false;
                txtReason.Enabled = false;
                txtReason.Visible = false;
                cvAttachmentDocument.SettingsDataSecurity.AllowDelete = false;
                //cvImages.SettingsDataSecurity.AllowDelete = false;
                //cvDocument.SettingsDataSecurity.AllowDelete = false;
                gvStaff.SettingsDataSecurity.AllowDelete = false;
                gvHazards.SettingsDataSecurity.AllowDelete = false;
                gvStaff.SettingsDataSecurity.AllowInsert = false;
                gvHazards.SettingsDataSecurity.AllowInsert = false;
                gvStaff.SettingsDataSecurity.AllowDelete = false;
                gvHazards.SettingsDataSecurity.AllowDelete = false;
                gvStaff.SettingsDataSecurity.AllowInsert = false;
                gvHazards.SettingsDataSecurity.AllowInsert = false;
                gvHazards.SettingsDataSecurity.AllowEdit = false;
                gvEquipment.SettingsDataSecurity.AllowInsert = false;
                gvEquipment.SettingsDataSecurity.AllowDelete = false;
                //var layoutgroup = flToolboxMeeting.FindItemOrGroupByName("UploadFiles");
                //if (layoutgroup != null)
                //{
                //    var item1 = ((LayoutGroup)layoutgroup).FindItemOrGroupByName("Part1");
                //    var item2 = ((LayoutGroup)layoutgroup).FindItemOrGroupByName("Part2");
                //    if (item1 != null && item2 != null)
                //    {
                //        ((LayoutItem)item1).Visible = false;
                //        ((LayoutItem)item2).Visible = false;
                //    }
                //}
                dtMeetingDate.Enabled = false;
                txtSupervisor.Enabled = false;
                txtSafety.Enabled = false;
                //cbSupervisor.Enabled = false;
                //cbSafety.Enabled = false;
                txtDescription.Enabled = false;
                txtDescriptionPM.Enabled = false;
                txtRemarks.Enabled = false;
                txtReason.Enabled = false;
                txtTodayTeamActionGoal.Enabled = false;
                txtTodayTouchAndCall.Enabled = false;
                txtTodayTeamActionGoal.Enabled = false;
                txtFeedback.Enabled = false;
                cbDeLine1.Enabled = false;
                cbDeLine2.Enabled = false;
                cbDeLine3.Enabled = false;
                cbDeLine4.Enabled = false;

                if (master.Status == 0)
                {
                    dtMeetingDate.Enabled = true;
                    txtSupervisor.Enabled = true;
                    txtSafety.Enabled = true;
                    //cbSupervisor.Enabled = true;
                    //cbSafety.Enabled = true;
                    txtDescription.Enabled = true;
                    txtDescriptionPM.Enabled = true;
                    txtRemarks.Enabled = true;
                    txtTodayTeamActionGoal.Enabled = true;
                    txtTodayTouchAndCall.Enabled = true;
                    txtTodayTeamActionGoal.Enabled = true;
                    txtFeedback.Enabled = true;
                    btnSubmit.Visible = true;

                    cbDeLine1.Enabled = true;
                    cbDeLine2.Enabled = true;
                    cbDeLine3.Enabled = true;
                    cbDeLine4.Enabled = true;
                    //btnCancel.Visible = true;
                    //cvImages.SettingsDataSecurity.AllowDelete = true;
                    //cvDocument.SettingsDataSecurity.AllowDelete = true;
                    gvStaff.SettingsDataSecurity.AllowDelete = true;
                    gvHazards.SettingsDataSecurity.AllowDelete = true;
                    gvStaff.SettingsDataSecurity.AllowInsert = true;
                    gvHazards.SettingsDataSecurity.AllowInsert = true;
                    gvStaff.SettingsDataSecurity.AllowDelete = true;
                    gvHazards.SettingsDataSecurity.AllowDelete = true;
                    gvStaff.SettingsDataSecurity.AllowInsert = true;
                    gvHazards.SettingsDataSecurity.AllowInsert = true;
                    gvHazards.SettingsDataSecurity.AllowEdit = true;
                    gvEquipment.SettingsDataSecurity.AllowInsert = true;
                    gvEquipment.SettingsDataSecurity.AllowDelete = true;
                    if (uploadfilegroup != null)
                    {
                        var UploadFilePanel = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("UploadFilePanel");
                        if (UploadFilePanel != null) (UploadFilePanel as LayoutItem).Visible = true;
                        var UploadFileNote = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("UploadFileNote");
                        if (UploadFileNote != null) (UploadFileNote as LayoutItem).Visible = true;
                        var EditDocument = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("EditAttachmentDoc");
                        if (EditDocument != null) (EditDocument as LayoutItem).Visible = true;
                        var DisplayImage = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("DisplayImage");
                        if (DisplayImage != null) (DisplayImage as LayoutItem).Visible = true;
                    }
                    cvAttachmentDocument.SettingsDataSecurity.AllowDelete = true;
                    //if (layoutgroup != null)
                    //{
                    //    var item1 = ((LayoutGroup)layoutgroup).FindItemOrGroupByName("Part1");
                    //    var item2 = ((LayoutGroup)layoutgroup).FindItemOrGroupByName("Part2");
                    //    if (item1 != null && item2 != null)
                    //    {
                    //        ((LayoutItem)item1).Visible = true;
                    //        ((LayoutItem)item2).Visible = true;
                    //    }
                    //}
                    //if (master.ConductedByName == "")
                    //{
                    //    lblSubmitName.Text = CurrentUser.FullName;
                    //    lblSubmitDesignation.Text = CurrentUser.Position;
                    //    lblSubmitCompany.Text = CurrentUser.ConstructorName;
                    //}
                    //else
                    //{
                    //    //lblSubmitName.Text = master.ConductedByName;
                    //    //lblSubmitDesignation.Text = master.RequestPosition;
                    //    //lblSubmitCompany.Text = master.RequestCompany;
                    //    //txtSubmitRemarks.Text = master.RequestRemarks;
                    //}

                }
                if (master.Status == 1)
                {
                    List<UserRoleModel> roles = UserRoleViewModel.GetUserRoleList(user.UserID);
                    var approverRole = roles.FirstOrDefault(item => item.RoleID == "PTW APPROVER");
                    if (approverRole != null && master.ConductedBy != user.UserID)
                    {
                        if (returnrejectgroup != null) (returnrejectgroup as LayoutItem).Visible = true;
                        if (approvalgroup != null) (approvalgroup as LayoutGroup).Visible = true;
                        btnApprove.Visible = true;
                        btnReturn.Visible = true;
                        btnReject.Visible = true;
                        txtReason.Visible = true;
                        txtReason.Enabled = true;

                        UserModel PMUser = UserViewModel.GetLoggedInUserInfo();
                        lblApprovalName.Text = PMUser.FullName;
                        lblApprovalDesignation.Text = PMUser.Position;
                        lblApprovalCompany.Text = PMUser.ConstructorName;
                    }
                    //btnCancel.Visible = true;
                    lblSubmitStatus.Text = "Submitted on " + Convert.ToDateTime(master.MeetingDate).ToString("dd MMM yyyy HH:mm") + "";
                    cvAttachmentDocument.SettingsDataSecurity.AllowDelete = false;
                    if (uploadfilegroup != null)
                    {
                        var EditDocument = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("EditAttachmentDoc");
                        if (EditDocument != null) (EditDocument as LayoutItem).Visible = true;
                        var DisplayImage = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("DisplayImage");
                        if (DisplayImage != null) (DisplayImage as LayoutItem).Visible = true;
                    }
                }
                if (master.Status == 2)
                {
                    lblLocation.Text = strLocation;
                    cvAttachmentDocument.SettingsDataSecurity.AllowDelete = false;
                    if (uploadfilegroup != null)
                    {
                        var EditDocument = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("EditAttachmentDoc");
                        if (EditDocument != null) (EditDocument as LayoutItem).Visible = true;
                        var DisplayImage = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("DisplayImage");
                        if (DisplayImage != null) (DisplayImage as LayoutItem).Visible = true;
                    }
                    lblSubmitStatus.Text = "Submitted on " + Convert.ToDateTime(master.MeetingDate).ToString("dd MMM yyyy HH:mm") + "";
                    if (approvalgroup != null) (approvalgroup as LayoutGroup).Visible = true;
                    lblApprovalCompany.Text = master.ApproveCompany;
                    lblApprovalDesignation.Text = master.ApprovePosition;
                    lblApprovalName.Text = master.ApproveName;
                    txtApprovalRemarks.Enabled = false;
                    lblApprovalStatus.Text = "Part 2: Approval by HEA Project Manager / Authorized Competent Person (Approved on " + Convert.ToDateTime(master.ApprovedDate).ToString("dd MMM yyyy HH:mm") + ")";
                }
                if (master.Status == 99)
                {
                    if (returnrejectgroup != null) (returnrejectgroup as LayoutItem).Visible = true;
                    txtReason.Visible = true;
                    txtReason.Enabled = false;
                    cvAttachmentDocument.SettingsDataSecurity.AllowDelete = false;
                    if (uploadfilegroup != null)
                    {
                        var EditDocument = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("EditAttachmentDoc");
                        if (EditDocument != null) (EditDocument as LayoutItem).Visible = true;
                        var DisplayImage = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("DisplayImage");
                        if (DisplayImage != null) (DisplayImage as LayoutItem).Visible = true;
                    }
                }
                if (master.Status == 98 && master.CreatedBy == user.UserID)
                {
                    cvAttachmentDocument.SettingsDataSecurity.AllowDelete = true;
                    if (uploadfilegroup != null)
                    {
                        var UploadFilePanel = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("UploadFilePanel");
                        if (UploadFilePanel != null) (UploadFilePanel as LayoutItem).Visible = true;
                        var UploadFileNote = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("UploadFileNote");
                        if (UploadFileNote != null) (UploadFileNote as LayoutItem).Visible = true;
                        var EditDocument = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("EditAttachmentDoc");
                        if (EditDocument != null) (EditDocument as LayoutItem).Visible = true;
                        var DisplayImage = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("DisplayImage");
                        if (DisplayImage != null) (DisplayImage as LayoutItem).Visible = true;
                    }
                    if (returnrejectgroup != null) (returnrejectgroup as LayoutItem).Visible = true;

                    lblLocation.Text = strLocation;
                    txtReason.Enabled = false;
                    txtReason.Visible = true;
                    btnSubmit.Visible = true;
                    //btnDelete.Visible = true;
                    //btnCancel.Visible = true;
                    dtMeetingDate.Enabled = true;
                    txtSupervisor.Enabled = true;
                    txtSafety.Enabled = true;
                    //cbSupervisor.Enabled = true;
                    //cbSafety.Enabled = true;
                    txtDescription.Enabled = true;
                    txtDescriptionPM.Enabled = true;
                    txtRemarks.Enabled = true;
                    txtTodayTeamActionGoal.Enabled = true;
                    txtTodayTouchAndCall.Enabled = true;
                    txtTodayTeamActionGoal.Enabled = true;

                    cbDeLine1.Enabled = true;
                    cbDeLine2.Enabled = true;
                    cbDeLine3.Enabled = true;
                    cbDeLine4.Enabled = true;
                    //cvImages.SettingsDataSecurity.AllowDelete = true;
                    //cvDocument.SettingsDataSecurity.AllowDelete = true;
                    gvStaff.SettingsDataSecurity.AllowDelete = true;
                    gvHazards.SettingsDataSecurity.AllowDelete = true;
                    gvStaff.SettingsDataSecurity.AllowInsert = true;
                    gvHazards.SettingsDataSecurity.AllowInsert = true;
                    gvStaff.SettingsDataSecurity.AllowDelete = true;
                    gvHazards.SettingsDataSecurity.AllowDelete = true;
                    gvStaff.SettingsDataSecurity.AllowInsert = true;
                    gvHazards.SettingsDataSecurity.AllowInsert = true;
                    gvEquipment.SettingsDataSecurity.AllowInsert = true;
                    gvEquipment.SettingsDataSecurity.AllowDelete = true;


                }

                if (safetydetaillist != null)
                {
                    ClearSafetyList();
                    foreach (QuestionAndAnswerModel ent in safetydetaillist)
                        AddEnableCheckList(ent, master.Status);
                }

                cvAttachmentDocument.DataSource = attachlist;
                cvAttachmentDocument.DataBind();
                DisplayImageItems.DataSource = attachlist.Where(item => item.FileType == "IMG");
                DisplayImageItems.DataBind();
                gvStaff.DataSource = tbmattendeelist;
                gvStaff.DataBind();
                gvHazards.DataSource = hazardlist;
                gvHazards.DataBind();
                gvEquipment.DataSource = tbmequipmentlist;
                gvEquipment.DataBind();
            }
            catch(Exception ex)
            {
                Response.Redirect("~/Account/SignIn.aspx");
            }
        }

        private void BindMaster()
        {
            lblProjectName.Text = master.ProjectName;
            dtMeetingDate.Date = master.MeetingDate;
            txtSupervisor.Text = master.SupervisorName;
            txtSafety.Text = master.SafetyName;
            //cbSupervisor.Value = master.SupervisorName;
            //cbSafety.Value = master.SafetyName;
            txtDescription.Text = master.Description;
            txtDescriptionPM.Text = master.DescriptionPM;
            txtRemarks.Text = master.Remarks;
            txtApprovalRemarks.Text = master.ApproveRemarks;
            txtReason.Text = master.ReturnRejectReason;
            txtFeedback.Text = master.Feedback;
            txtTodayTeamActionGoal.Text = master.TodayTeamActionGoal;
            txtTodayTouchAndCall.Text = master.TodayTouchAndCall;
            lblSubmitName.Text = master.ConductedByName;
            lblSubmitDesignation.Text = master.ConductedPosition;
            lblSubmitCompany.Text = master.ConductedCompany;
            cbDeLine1.Checked = (master.SafetyDeclaration1 == "Y");
            cbDeLine2.Checked = (master.SafetyDeclaration2 == "Y");
            cbDeLine3.Checked = (master.SafetyDeclaration3 == "Y");
            cbDeLine4.Checked = (master.SafetyDeclaration4 == "Y");
        }
        protected void dtMeetingDate_Validation(object sender, DevExpress.Web.ValidationEventArgs e)
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            bool result = date.Date <= DateTime.Now.Date.AddDays(1);
            e.IsValid = result;
            e.ErrorText = "Please enter the valid Date";
        }



        private void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
        protected void gvStaff_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();
            var project = UserViewModel.GetSelectedProject();
            ASPxGridView gridView = sender as ASPxGridView;

            if (gridView.IsEditing && e.Column.FieldName == "UserID")
            {
                ASPxComboBox cbStaff = e.Editor as ASPxComboBox;
                cbStaff.DropDownStyle = DropDownStyle.DropDownList;
                cbStaff.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                cbStaff.EnableCallbackMode = true;
                cbStaff.CustomFiltering += cbAttendees_CustomFiltering;

                List<UserModel> userlist = UserViewModel.GetUser_CodeForModule(project.ToString());
                if (userlist != null) userlist = userlist.Where(item => item.ConstructorName == user.ConstructorName).ToList();
                cbStaff.DataSource = userlist;
                cbStaff.TextField = "UserID";
                cbStaff.ValueField = "UserID";
                cbStaff.DataBind();
            }
            if (gridView.IsEditing && e.Column.FieldName == "FullName")
            {
                ASPxTextBox txtTemp = e.Editor as ASPxTextBox;
                txtTemp.ReadOnly = true;
            }
            if (gridView.IsEditing && e.Column.FieldName == "Position")
            {
                ASPxTextBox txtTemp = e.Editor as ASPxTextBox;
                txtTemp.ReadOnly = true;
            }
            if (gridView.IsEditing && e.Column.FieldName == "ConstructorName")
            {
                ASPxTextBox txtTemp = e.Editor as ASPxTextBox;
                txtTemp.ReadOnly = true;
            }
        }
        protected void gvStaff_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ASPxGridView tempGrid = (ASPxGridView)sender;
            if (e.NewValues["UserID"] == null || e.NewValues["UserID"].ToString() == "")
                AddError(e.Errors, tempGrid.Columns["UserID"], "Please select the User.");

            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
                e.RowError = "Please, correct all errors.";
        }
        protected void gvStaff_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView tempGrid = (ASPxGridView)sender;
            UserModel user = UserViewModel.GetLoggedInUserInfo();
            master = (TBMModel)Session["TBM_Record"];
            tbmattendeelist = (List<TBMAttendeeModel>)Session["TBM_Attendee"];

            TBMAttendeeModel ent = new TBMAttendeeModel();
            ent.Key = master.Key;
            ent.UserID = e.NewValues["UserID"] != null ? e.NewValues["UserID"].ToString() : "";
            ent.FullName = e.NewValues["FullName"] != null ? e.NewValues["FullName"].ToString() : "";
            ent.Position = e.NewValues["Position"] != null ? e.NewValues["Position"].ToString() : "";
            ent.ConstructorName = e.NewValues["ConstructorName"] != null ? e.NewValues["ConstructorName"].ToString() : "";
            ent.Created = DateTime.Now;
            ent.Updated = DateTime.Now;
            ent.CreatedBy = user.UserID;
            ent.UpdatedBy = user.UserID;

            tbmattendeelist.Add(ent);
            Session["TBM_Attendee"] = tbmattendeelist;

            e.Cancel = true;
            tempGrid.CancelEdit();
            tempGrid.DataSource = tbmattendeelist;
        }
        protected void gvStaff_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ASPxGridView tempGrid = (ASPxGridView)sender;
            string strUserID = tempGrid.GetRowValues(tempGrid.FocusedRowIndex, "UserID").ToString();
            if (strUserID != "")
            {
                tbmattendeelist = (List<TBMAttendeeModel>)Session["TBM_Attendee"];

                TBMAttendeeModel ent = tbmattendeelist.FirstOrDefault(item => item.UserID == strUserID);
                TBMAttendeeViewModel.TBMAttendee_Delete(ent.UserID, ent.Key);
                tbmattendeelist.Remove(ent);
                Session["PTW_Attendee"] = tbmattendeelist;

                e.Cancel = true;
                tempGrid.CancelEdit();
                tempGrid.DataSource = tbmattendeelist;
            }
        }


        //protected void UploadFileControl_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        //{
        //    if (e.IsValid)
        //    {
        //        UserModel user = UserViewModel.GetLoggedInUserInfo();
        //        string resultExtension = Path.GetExtension(e.UploadedFile.FileName);
        //        string resultFileName = e.UploadedFile.FileName;
        //        long sizeInKilobytes = e.UploadedFile.ContentLength / 1024;

        //        AttachmentModel ent = new AttachmentModel();
        //        ent.ID = 0;
        //        ent.Key = strKey;
        //        ent.FileName = resultFileName;
        //        ent.Document = e.UploadedFile.FileBytes;
        //        ent.Status = 1;
        //        ent.Created = DateTime.Now;
        //        ent.CreatedBy = user.UserID;
        //        ent.Updated = DateTime.Now;
        //        ent.UpdatedBy = user.UserID;
        //        if (resultExtension.ToUpper() == ".PDF")
        //        {
        //            ent.FileType = "PDF";
        //            attachdoclist = (List<AttachmentModel>)Session["TBM_Record_DOC"];
        //            attachdoclist.Add(ent);
        //            Session["TBM_Record_DOC"] = attachdoclist;
        //        }
        //        else
        //        {
        //            ent.FileType = "IMG";
        //            attachlist = (List<AttachmentModel>)Session["TBM_Record_IMG"];
        //            attachlist.Add(ent);
        //            Session["TBM_Record_IMG"] = attachlist;
        //        }
        //    }
        //}
        //protected void btnDownload_Click(object sender, EventArgs e)
        //{
        //    ASPxButton btnDownload = sender as ASPxButton;
        //    GridViewDataItemTemplateContainer container = btnDownload.NamingContainer as GridViewDataItemTemplateContainer;
        //    var filename = container.KeyValue;
        //    if (filename != null)
        //    {
        //        var attach = attachdoclist.FirstOrDefault(item => item.FileName == filename.ToString());
        //        if (attach != null)
        //        {
        //            Response.ContentType = "application/octet-stream";
        //            Response.Clear();
        //            Response.BufferOutput = true;
        //            Response.AppendHeader("Content-Disposition", "attachment; filename=" + attach.FileName);
        //            Response.BinaryWrite(attach.Document);
        //            Response.Flush();
        //        }
        //    }
        //}
        //protected void ASPxCallbackResult_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        //{
        //    Session["ePTW_DocumentID"] = null;
        //    Session["ePTW_DocumentData"] = null;
        //    Session["ePTW_DocumentExt"] = null;
        //    Session["ePTW_DocumentFileName"] = null;
        //    e.Result = "ok";
        //}
        //protected void cvImages_CardDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        //{
        //    var filename = e.Keys["FileName"];
        //    if (filename != null)
        //    {
        //        ASPxCardView tempGrid = (ASPxCardView)sender;
        //        var attach = attachlist.FirstOrDefault(item => item.FileName == filename.ToString());
        //        if (attach != null)
        //        {
        //            AttachmentModel ent = (AttachmentModel)attach;
        //            if (ent.ID > 0)
        //            {
        //                attachlist.Remove(ent);
        //            }
        //            else
        //            {
        //                AttachmentViewModel.Attachment_Delete(ent.ID);
        //                attachlist.Remove(ent);
        //            }
        //            e.Cancel = true;
        //            tempGrid.CancelEdit();

        //            Session["TBM_Record_IMG"] = attachlist;
        //        }
        //    }
        //}
        //protected void cvDocument_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        //{
        //    var filename = e.Keys["FileName"];
        //    if (filename != null)
        //    {
        //        ASPxGridView tempGrid = (ASPxGridView)sender;
        //        var attach = attachdoclist.FirstOrDefault(item => item.FileName == filename.ToString());
        //        if (attach != null)
        //        {
        //            AttachmentModel ent = (AttachmentModel)attach;
        //            if (ent.ID > 0)
        //            {
        //                attachdoclist.Remove(ent);
        //            }
        //            else
        //            {
        //                AttachmentViewModel.Attachment_Delete(ent.ID);
        //                attachdoclist.Remove(ent);
        //            }
        //            e.Cancel = true;
        //            tempGrid.CancelEdit();

        //            Session["TBM_Record_DOC"] = attachdoclist;
        //        }
        //    }
        //}



        protected void gvEquipment_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            ASPxGridView gridView = sender as ASPxGridView;
            if (gridView.IsEditing && e.Column.FieldName == "RegistrationNo")
            {
                ASPxComboBox cbEquipment = e.Editor as ASPxComboBox;
                var ProjectName = UserViewModel.GetSelectedProject();
                List<EquipmentModel> eqlist = EquipmentViewModel.GetEquipment_ByProject_CodeTable(ProjectName.ToString());

                cbEquipment.DropDownStyle = DropDownStyle.DropDownList;
                cbEquipment.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                cbEquipment.EnableCallbackMode = true;
                cbEquipment.CustomFiltering += cbEquipment_CustomFiltering;

                cbEquipment.DataSource = eqlist;
                cbEquipment.TextField = "RegistrationNo";
                cbEquipment.ValueField = "RegistrationNo";
                cbEquipment.DataBind();
            }
            if (gridView.IsEditing && e.Column.FieldName == "EquipmentType")
            {
                ASPxTextBox txtTemp = e.Editor as ASPxTextBox;
                txtTemp.ReadOnly = true;
            }
            if (gridView.IsEditing && e.Column.FieldName == "EquipmentName")
            {
                ASPxTextBox txtTemp = e.Editor as ASPxTextBox;
                txtTemp.ReadOnly = true;
            }
        }
        protected void gvEquipment_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ASPxGridView tempGrid = (ASPxGridView)sender;
            if (e.NewValues["RegistrationNo"] == null || e.NewValues["RegistrationNo"].ToString() == "")
                AddError(e.Errors, tempGrid.Columns["RegistrationNo"], "Please select the Equipment Registration Number.");

            if (e.NewValues["MachineType"] == null || e.NewValues["MachineType"].ToString() == "")
                AddError(e.Errors, tempGrid.Columns["MachineType"], "Please select the Machine Type.");

            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
                e.RowError = "Please, correct all errors.";
        }
        protected void gvEquipment_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView tempGrid = (ASPxGridView)sender;
            UserModel user = UserViewModel.GetLoggedInUserInfo();
            master = (TBMModel)Session["TBM_Record"];
            tbmequipmentlist = (List<TBMEquipmentModel>)Session["TBM_Equipment"];

            TBMEquipmentModel ent = new TBMEquipmentModel();
            ent.ID = 0;
            ent.Key = master.Key;
            ent.EquipmentName = e.NewValues["EquipmentName"] != null ? e.NewValues["EquipmentName"].ToString() : "";
            ent.RegistrationNo = e.NewValues["RegistrationNo"] != null ? e.NewValues["RegistrationNo"].ToString() : "";
            ent.EquipmentType = e.NewValues["EquipmentType"] != null ? e.NewValues["EquipmentType"].ToString() : "";
            ent.MachineType = e.NewValues["MachineType"] != null ? e.NewValues["MachineType"].ToString() : "";
            ent.Created = DateTime.Now;
            ent.Updated = DateTime.Now;
            ent.CreatedBy = user.UserID;
            ent.UpdatedBy = user.UserID;

            tbmequipmentlist.Add(ent);
            Session["TBM_Equipment"] = tbmequipmentlist;

            e.Cancel = true;
            gvEquipment.CancelEdit();
            gvEquipment.DataSource = tbmequipmentlist;
        }
        protected void gvEquipment_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ASPxGridView tempGrid = (ASPxGridView)sender;
            string strName = tempGrid.GetRowValues(tempGrid.FocusedRowIndex, "RegistrationNo").ToString();
            if (strName != "")
            {
                tbmequipmentlist = (List<TBMEquipmentModel>)Session["TBM_Equipment"];

                TBMEquipmentModel ent = tbmequipmentlist.FirstOrDefault(item => item.RegistrationNo == strName);
                TBMEquipmentViewModel.TBMEquipment_Delete(ent.EquipmentName, ent.Key);
                tbmequipmentlist.Remove(ent);
                Session["TBM_Equipment"] = tbmequipmentlist;

                e.Cancel = true;
                tempGrid.CancelEdit();
                tempGrid.DataSource = tbmequipmentlist;
            }
        }
        protected void cbEquipment_CustomFiltering(object sender, ListEditCustomFilteringEventArgs e)
        {
            string[] words = e.Filter.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] columns = new string[] { "RegistrationNo", "EquipmentType", "EquipmentName" };
            e.FilterExpression = GroupOperator.And(words.Select(w =>
                GroupOperator.Or(
                    columns.Select(c =>
                        new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(c), w)
                    )
                )
            )).ToString();
            e.CustomHighlighting = columns.ToDictionary(c => c, c => words);
        }



        protected void gvHazards_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            ASPxGridView gridView = sender as ASPxGridView;
            if (gridView.IsEditing && e.Column.FieldName == "WorkActivity")
            {
                ASPxComboBox cbWorkActivity = e.Editor as ASPxComboBox;
                keyactivitieslist = (List<KeyActivitiesModel>)Session["TBM_KEYACTLIST"];
                cbWorkActivity.DataSource = keyactivitieslist;
                cbWorkActivity.TextField = "Name";
                cbWorkActivity.ValueField = "Name";
                cbWorkActivity.DataBind();
                cbWorkActivity.ClientSideEvents.SelectedIndexChanged = "onSelectedWorkChanged";
                cbWorkActivity.ClientSideEvents.Init = "onWorkInit";
            }
            if (gridView.IsEditing && e.Column.FieldName == "CauseOfHazard")
            {
                ASPxMemo txtTemp = e.Editor as ASPxMemo;
                txtTemp.Height = 150;
            }

            if (gridView.IsEditing && e.Column.FieldName == "HappenAsResult")
            {
                ASPxMemo txtTemp = e.Editor as ASPxMemo;
                txtTemp.Height = 150;
            }

            if (gridView.IsEditing && e.Column.FieldName == "ActionRemarks")
            {
                ASPxMemo txtTemp = e.Editor as ASPxMemo;
                txtTemp.Height = 150;
            }
        }
        protected void gvHazards_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

            ASPxGridView tempGrid = (ASPxGridView)sender;
            if (e.NewValues["WorkActivity"] == null || e.NewValues["WorkActivity"].ToString() == "")
                AddError(e.Errors, tempGrid.Columns["WorkActivity"], "Please select the Work Activity.");
            else
            {
                if (e.NewValues["WorkActivity"].ToString() == "Others.")
                {
                    if (e.NewValues["Others"] == null || e.NewValues["Others"].ToString() == "")
                        AddError(e.Errors, tempGrid.Columns["Others"], "Please enter the Others value.");
                }
            }
            if (e.NewValues["CauseOfHazard"] == null || e.NewValues["CauseOfHazard"].ToString() == "")
                AddError(e.Errors, tempGrid.Columns["CauseOfHazard"], "Please enter 'What cause the hazard?'.");
            if (e.NewValues["HappenAsResult"] == null || e.NewValues["HappenAsResult"].ToString() == "")
                AddError(e.Errors, tempGrid.Columns["HappenAsResult"], "Please enter 'What happen as a result?'.");
            if (e.NewValues["ActionToTaken"] == null || e.NewValues["ActionToTaken"].ToString() == "")
                AddError(e.Errors, tempGrid.Columns["ActionToTaken"], "Please select the Action to be taken.");
            if (e.NewValues["ActionRemarks"] == null || e.NewValues["ActionRemarks"].ToString() == "")
                AddError(e.Errors, tempGrid.Columns["ActionRemarks"], "Please select the Remarks of action taken.");
            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
                e.RowError = "Please, correct all errors.";
        }
        protected void gvHazards_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView tempGrid = (ASPxGridView)sender;
            UserModel user = UserViewModel.GetLoggedInUserInfo();
            master = (TBMModel)Session["TBM_Record"];
            hazardlist = (List<TBMHazardModel>)Session["TBM_Hazard"];

            TBMHazardModel ent = new TBMHazardModel();
            ent.ID = 0;
            ent.Key = master.Key;
            ent.WorkActivity = e.NewValues["WorkActivity"] != null ? e.NewValues["WorkActivity"].ToString() : "";
            ent.Others = e.NewValues["Others"] != null ? e.NewValues["Others"].ToString() : "";
            ent.CauseOfHazard = e.NewValues["CauseOfHazard"] != null ? e.NewValues["CauseOfHazard"].ToString() : "";
            ent.HappenAsResult = e.NewValues["HappenAsResult"] != null ? e.NewValues["HappenAsResult"].ToString() : "";

            ent.ActionToTaken = e.NewValues["ActionToTaken"] != null ? e.NewValues["ActionToTaken"].ToString() : "";
            ent.ActionRemarks = e.NewValues["ActionRemarks"] != null ? e.NewValues["ActionRemarks"].ToString() : "";

            ent.Created = DateTime.Now;
            ent.Updated = DateTime.Now;
            ent.CreatedBy = user.UserID;
            ent.UpdatedBy = user.UserID;

            hazardlist.Add(ent);
            Session["TBM_Hazard"] = hazardlist;

            e.Cancel = true;
            tempGrid.CancelEdit();
            tempGrid.DataSource = hazardlist;
        }
        protected void gvHazards_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ASPxGridView tempGrid = (ASPxGridView)sender;
            string strWork = tempGrid.GetRowValues(tempGrid.FocusedRowIndex, "WorkActivity").ToString();
            if (strWork != "")
            {
                hazardlist = (List<TBMHazardModel>)Session["TBM_Hazard"];
                TBMHazardModel ent = hazardlist.FirstOrDefault(item => item.WorkActivity == strWork);
                if (ent.ID > 0) TBMHazardViewModel.TBMHazard_Delete(ent.ID, ent.Key);
                hazardlist.Remove(ent);
                Session["PTW_Hazard"] = hazardlist;

                e.Cancel = true;
                tempGrid.CancelEdit();
                tempGrid.DataSource = hazardlist;
            }
        }
        protected void gvHazards_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView tempGrid = (ASPxGridView)sender;
            string strWork = tempGrid.GetRowValues(tempGrid.FocusedRowIndex, "WorkActivity").ToString();
            UserModel user = UserViewModel.GetLoggedInUserInfo();
            //master = (TBMModel)Session["TBM_Record"];
            hazardlist = (List<TBMHazardModel>)Session["TBM_Hazard"];
            TBMHazardModel ent = hazardlist.FirstOrDefault(item => item.WorkActivity == strWork);
            if (ent != null)
            {
                ent.WorkActivity = e.NewValues["WorkActivity"] != null ? e.NewValues["WorkActivity"].ToString() : "";
                ent.Others = e.NewValues["Others"] != null ? e.NewValues["Others"].ToString() : "";
                ent.CauseOfHazard = e.NewValues["CauseOfHazard"] != null ? e.NewValues["CauseOfHazard"].ToString() : "";
                ent.HappenAsResult = e.NewValues["HappenAsResult"] != null ? e.NewValues["HappenAsResult"].ToString() : "";

                ent.ActionToTaken = e.NewValues["ActionToTaken"] != null ? e.NewValues["ActionToTaken"].ToString() : "";
                ent.ActionRemarks = e.NewValues["ActionRemarks"] != null ? e.NewValues["ActionRemarks"].ToString() : "";

                ent.Updated = DateTime.Now;
                ent.UpdatedBy = user.UserID;
            }
            Session["TBM_Hazard"] = hazardlist;

            e.Cancel = true;
            tempGrid.CancelEdit();
            tempGrid.DataSource = hazardlist;
        }








        // Document Workflow
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtDescription.Text == "" && txtDescriptionPM.Text == "")
            {
                return;
            }
            if (txtDescription.Text != "" && txtDescriptionPM.Text != "")
            {
                return;
            }
            if (!cbDeLine1.Checked || !cbDeLine2.Checked || !cbDeLine3.Checked || !cbDeLine4.Checked)
            {
                return;
            }

            UserModel user = UserViewModel.GetLoggedInUserInfo();
            try
            {
                attachlist = (List<AttachmentModel>)Session["TBM_Record_IMG"];
                templatedetaillist = (List<QuestionAndAnswerModel>)Session["TBM_TemplateDetails"];
                master = (TBMModel)Session["TBM_Record"];
                master.MeetingDate = dtMeetingDate.Date;

                master.Supervisor = txtSupervisor.Text;
                master.SupervisorName = txtSupervisor.Text;
                master.Safety = txtSafety.Text;
                master.SafetyName = txtSafety.Text;
                master.Description = txtDescription.Text;
                master.DescriptionPM = txtDescriptionPM.Text;
                master.Remarks = txtRemarks.Text;
                master.TodayTeamActionGoal = txtTodayTeamActionGoal.Text;
                master.TodayTouchAndCall = txtTodayTouchAndCall.Text;
                master.Feedback = txtFeedback.Text;
                master.Status = 1;
                master.ActionsPreviousCompleted = "N";
                master.Created = DateTime.Now;
                master.Updated = DateTime.Now;
                master.CreatedBy = user.UserID;
                master.UpdatedBy = user.UserID;
                master.ReturnRejectDate = null;
                master.ReturnRejectReason = "";
                master.ApprovedBy = "";
                master.ApprovedDate = null;
                master.Latitude = hfLatitude.Value;
                master.Longitude = hfLongitude.Value;

                if (cbDeLine1.Checked) master.SafetyDeclaration1 = "Y";
                if (cbDeLine2.Checked) master.SafetyDeclaration2 = "Y";
                if (cbDeLine3.Checked) master.SafetyDeclaration3 = "Y";
                if (cbDeLine4.Checked) master.SafetyDeclaration4 = "Y";

                TBMViewModel.TBM_InsertUpdate(master);

                foreach (AttachmentModel img in attachlist)
                {
                    img.Key = master.Key;
                    AttachmentViewModel.Attachment_InsertUpdate(img);
                }
                foreach (TBMAttendeeModel att in tbmattendeelist)
                {
                    att.Key = master.Key;
                    TBMAttendeeViewModel.TBMAttendee_InsertUpdate(att);
                }
                foreach (TBMHazardModel hzd in hazardlist)
                {
                    hzd.Key = master.Key;
                    TBMHazardViewModel.TBMHazard_InsertUpdate(hzd);
                }
                foreach (QuestionAndAnswerModel qa in templatedetaillist)
                {
                    qa.Key = master.Key;
                    TBMViewModel.TBMDetail_InsertUpdate(qa);
                }
                foreach (QuestionAndAnswerModel sc in safetydetaillist)
                {
                    sc.Key = master.Key;
                    TBMViewModel.TBMChecklist_InsertUpdate(sc);
                }
                foreach (TBMEquipmentModel eq in tbmequipmentlist)
                {
                    eq.Key = master.Key;
                    TBMEquipmentViewModel.TBMEquipment_InsertUpdate(eq);
                }

                Session["TBM_Record"] = null;
                Session["TBM_Record_IMG"] = null;
                Session["TBM_TemplateDetails"] = null;
                Session["TBM_Attendee"] = null;
                Session["TBM_Hazard"] = null;
                Session["TBM_UserList"] = null;
                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/TBM/MyTBM.aspx");
                else
                    Response.Redirect("~/TBM/MyTBM.aspx");
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            try
            {
                master = (TBMModel)Session["TBM_Record"];
                master.Status = 97;
                master.Updated = DateTime.Now;
                master.UpdatedBy = user.UserID;
                master.ReturnRejectDate = null;
                master.ReturnRejectReason = "";
                master.ApprovedBy = "";
                master.ApprovedDate = null;

                TBMViewModel.TBM_InsertUpdate(master);

                Session["TBM_Record_IMG"] = null;
                Session["TBM_Record_DOC"] = null;
                Session["TBM_Attendee"] = null;
                Session["TBM_Hazard"] = null;
                Session["TBM_UserList"] = null;
                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/TBM/PendingTBM.aspx");
                else
                    Response.Redirect("~/TBM/PendingTBM.aspx");
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string strTask = (Session["TBM_Task"] == null) ? "" : Session["TBM_Task"].ToString();
            string strLink = "~/TBM/MyTBM.aspx";
            if (strTask == "P") strLink = "~/TBM/PendingTBM.aspx";
            if (Page.IsCallback)
                DevExpress.Web.ASPxWebControl.RedirectOnCallback(strLink);
            else
                Response.Redirect(strLink);
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            try
            {
                master = (TBMModel)Session["TBM_Record"];
                master.Status = 2;
                master.Updated = DateTime.Now;
                master.UpdatedBy = user.UserID;
                master.ReturnRejectDate = null;
                master.ReturnRejectReason = "";
                master.ApprovedBy = user.UserID;
                master.ApprovedDate = DateTime.Now;
                master.ApproveCompany = user.ConstructorName;
                master.ApproveName = user.FullName;
                master.ApprovePosition = user.Position;
                master.ApproveRemarks = txtApprovalRemarks.Text;
                TBMViewModel.TBM_InsertUpdate(master);

                Session["TBM_Record_IMG"] = null;
                Session["TBM_Record_DOC"] = null;
                Session["TBM_Attendee"] = null;
                Session["TBM_Hazard"] = null;
                Session["TBM_UserList"] = null;
                Session["TBM_Checklist"] = null;
                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/TBM/PendingTBM.aspx");
                else
                    Response.Redirect("~/TBM/PendingTBM.aspx");
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnReject_Click(object sender, EventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            try
            {
                master = (TBMModel)Session["TBM_Record"];
                master.Status = 99;
                master.Updated = DateTime.Now;
                master.UpdatedBy = user.UserID;
                master.ReturnRejectDate = DateTime.Now;
                master.ReturnRejectReason = txtReason.Text;
                master.ApprovedBy = "";
                master.ApprovedDate = null;
                master.ApproveCompany = "";
                master.ApproveName = "";
                master.ApprovePosition = "";

                TBMViewModel.TBM_InsertUpdate(master);

                Session["TBM_Record_IMG"] = null;
                Session["TBM_Record_DOC"] = null;
                Session["TBM_Attendee"] = null;
                Session["TBM_Hazard"] = null;
                Session["TBM_UserList"] = null;
                Session["TBM_Checklist"] = null;
                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/TBM/PendingTBM.aspx");
                else
                    Response.Redirect("~/TBM/PendingTBM.aspx");
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            try
            {
                master = (TBMModel)Session["TBM_Record"];
                master.Status = 98;
                master.Updated = DateTime.Now;
                master.UpdatedBy = user.UserID;
                master.ReturnRejectDate = DateTime.Now;
                master.ReturnRejectReason = txtReason.Text;
                master.ApprovedBy = "";
                master.ApprovedDate = null;
                master.ApproveCompany = "";
                master.ApproveName = "";
                master.ApprovePosition = "";

                TBMViewModel.TBM_InsertUpdate(master);

                Session["TBM_Record_IMG"] = null;
                Session["TBM_Record_DOC"] = null;
                Session["TBM_Attendee"] = null;
                Session["TBM_Hazard"] = null;
                Session["TBM_UserList"] = null;
                Session["TBM_Checklist"] = null;
                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/TBM/PendingTBM.aspx");
                else
                    Response.Redirect("~/TBM/PendingTBM.aspx");
            }
            catch (Exception ex)
            {
            }
        }


        // Upload File and Image Function
        protected void UploadFileControl_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            if (e.IsValid)
            {
                UserModel user = UserViewModel.GetLoggedInUserInfo();
                string resultExtension = Path.GetExtension(e.UploadedFile.FileName);
                string resultFileName = e.UploadedFile.FileName;
                long sizeInKilobytes = e.UploadedFile.ContentLength / 1024;

                AttachmentModel ent = new AttachmentModel();
                ent.ID = 0;
                ent.Key = strKey;
                ent.FileName = resultFileName;
                ent.Document = e.UploadedFile.FileBytes;
                ent.Status = 1;
                ent.Created = DateTime.Now;
                ent.CreatedBy = user.UserID;
                ent.Updated = DateTime.Now;
                ent.UpdatedBy = user.UserID;
                if (resultExtension.ToUpper() == ".PDF")
                {
                    ent.FileType = "PDF";
                }
                else
                {
                    ent.FileType = "IMG";
                }
                attachlist = (List<AttachmentModel>)Session["TBM_Record_IMG"];
                attachlist.Add(ent);
                Session["TBM_Record_IMG"] = attachlist;
                ///
                /// 
                //////////if (resultExtension.ToUpper() == ".PDF")
                //////////{
                //////////    ent.FileType = "PDF";
                //////////    attachdoclist = (List<AttachmentModel>)Session["CCP_Record_DOC"];
                //////////    attachdoclist.Add(ent);
                //////////    Session["CCP_Record_DOC"] = attachdoclist;
                //////////}
                //////////else
                //////////{
                //////////    ent.FileType = "IMG";
                //////////    attachlist = (List<AttachmentModel>)Session["CCP_Record_IMG"];
                //////////    attachlist.Add(ent);
                //////////    Session["CCP_Record_IMG"] = attachlist;
                //////////}
            }
        }
        protected void ASPxCallbackResult_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string strLocation = e.Parameter;
            if (strLocation != "|")
            {
                string[] strLocationDTL = strLocation.Split('|');
                master = (TBMModel)Session["TBM_Record"];
                master.Latitude = strLocationDTL[0];
                master.Longitude = strLocationDTL[1];
                Session["TBM_Record"] = master;
            }
            Session["ePTW_DocumentID"] = null;
            Session["ePTW_DocumentData"] = null;
            Session["ePTW_DocumentExt"] = null;
            Session["ePTW_DocumentFileName"] = null;
            e.Result = "ok";
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            ASPxButton btnDownload = sender as ASPxButton;
            GridViewDataItemTemplateContainer container = btnDownload.NamingContainer as GridViewDataItemTemplateContainer;
            var filename = container.KeyValue;
            if (filename != null)
            {
                //var attach = attachdoclist.FirstOrDefault(item => item.FileName == filename.ToString());
                var attach = attachlist.FirstOrDefault(item => item.FileName == filename.ToString());
                if (attach != null)
                {
                    Response.ContentType = "application/octet-stream";
                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + attach.FileName);
                    Response.BinaryWrite(attach.Document);
                    Response.Flush();
                }
            }
        }
        protected void cvImages_CardDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var filename = e.Keys["FileName"];
            if (filename != null)
            {
                ASPxCardView tempGrid = (ASPxCardView)sender;
                var attach = attachlist.FirstOrDefault(item => item.FileName == filename.ToString());
                if (attach != null)
                {
                    AttachmentModel ent = (AttachmentModel)attach;
                    if (ent.ID > 0)
                    {
                        attachlist.Remove(ent);
                    }
                    else
                    {
                        AttachmentViewModel.Attachment_Delete(ent.ID);
                        attachlist.Remove(ent);
                    }
                    e.Cancel = true;
                    tempGrid.CancelEdit();

                    Session["TBM_Record_IMG"] = attachlist;
                }
            }
        }
        protected void cvDocument_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var filename = e.Keys["FileName"];
            if (filename != null)
            {
                ASPxGridView tempGrid = (ASPxGridView)sender;
                attachlist = (List<AttachmentModel>)Session["TBM_Record_IMG"];
                var attach = attachlist.FirstOrDefault(item => item.FileName == filename.ToString());
                if (attach != null)
                {
                    AttachmentModel ent = (AttachmentModel)attach;
                    if (ent.ID == 0)
                    {
                        attachlist.Remove(ent);
                    }
                    else
                    {
                        AttachmentViewModel.Attachment_Delete(ent.ID);
                        attachlist.Remove(ent);
                    }
                    e.Cancel = true;
                    tempGrid.CancelEdit();

                    Session["TBM_Record_IMG"] = attachlist;
                }
            }
        }
        protected void cbDisplayImage_Callback(object sender, CallbackEventArgsBase e)
        {
            attachlist = (List<AttachmentModel>)Session["TBM_Record_IMG"];
            DisplayImageItems.DataSource = attachlist.Where(item => item.FileType == "IMG");
            DisplayImageItems.DataBind();
        }

        // Filtering Function
        protected void cbSupervisor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void cbSupervisor_CustomFiltering(object sender, ListEditCustomFilteringEventArgs e)
        {
            string[] words = e.Filter.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] columns = new string[] { "UserID", "FullName", "Position", "ConstructorName" };
            e.FilterExpression = GroupOperator.And(words.Select(w =>
                GroupOperator.Or(
                    columns.Select(c =>
                        new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(c), w)
                    )
                )
            )).ToString();
            e.CustomHighlighting = columns.ToDictionary(c => c, c => words);
        }
        protected void cbSafety_CustomFiltering(object sender, ListEditCustomFilteringEventArgs e)
        {
            string[] words = e.Filter.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] columns = new string[] { "UserID", "FullName", "Position", "ConstructorName" };
            e.FilterExpression = GroupOperator.And(words.Select(w =>
                GroupOperator.Or(
                    columns.Select(c =>
                        new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(c), w)
                    )
                )
            )).ToString();
            e.CustomHighlighting = columns.ToDictionary(c => c, c => words);
        }
        protected void cbAttendees_CustomFiltering(object sender, ListEditCustomFilteringEventArgs e)
        {
            string[] words = e.Filter.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] columns = new string[] { "UserID", "FullName", "Position", "ConstructorName" };
            e.FilterExpression = GroupOperator.And(words.Select(w =>
                GroupOperator.Or(
                    columns.Select(c =>
                        new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(c), w)
                    )
                )
            )).ToString();
            e.CustomHighlighting = columns.ToDictionary(c => c, c => words);
        }


        // Questionaires Module
        protected void QuestionsAndAnswerCallbackPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            var group = FormLayoutQNA.FindItemOrGroupByName("QuestionsAndAnswer");
            if (group != null)
            {
                templatedetaillist = (List<QuestionAndAnswerModel>)Session["TBM_TemplateDetails"];
                foreach (var item in ((LayoutGroup)group).Items)
                {
                    if (item is LayoutItem)
                    {
                        foreach (var control in ((LayoutItem)item).Controls)
                        {
                            if (control is ASPxComboBox)
                            {
                                if (((ASPxComboBox)control).ID.Contains("Answer_"))
                                {
                                    string[] strplit = ((ASPxComboBox)control).ID.Split('_');
                                    int id = Convert.ToInt32(strplit[1]);
                                    string answer = ((ASPxComboBox)control).Text;

                                    var detail = templatedetaillist.FirstOrDefault(itm => itm.ID == id);
                                    if (detail != null) detail.Answer = answer;
                                }
                            }
                        }
                    }
                }
                Session["TBM_TemplateDetails"] = templatedetaillist;
            }
        }
        private void ClearQuestionAndAnswer()
        {
            var group = FormLayoutQNA.FindItemOrGroupByName("QuestionsAndAnswer");
            if (group != null)
            {
                ((LayoutGroup)group).Items.Clear();

                LayoutItem itemCheckList = new LayoutItem();
                itemCheckList.Width = Unit.Percentage(80);
                itemCheckList.VerticalAlign = FormLayoutVerticalAlign.Top;
                itemCheckList.Paddings.PaddingTop = Unit.Pixel(10);
                itemCheckList.Paddings.PaddingBottom = Unit.Pixel(10);
                itemCheckList.Caption = "";
                itemCheckList.ShowCaption = DevExpress.Utils.DefaultBoolean.False;

                ASPxLabel lbCheckList = new ASPxLabel();
                lbCheckList.Width = Unit.Percentage(100);
                lbCheckList.Text = "Please carry out items briefing and select \"Yes\" after it is done. :-";
                lbCheckList.Font.Size = FontUnit.Point(12);
                lbCheckList.Font.Underline = false;
                lbCheckList.Font.Bold = true;
                itemCheckList.Controls.Add(lbCheckList);

                LayoutItem itemResult = new LayoutItem();
                itemResult.Width = Unit.Percentage(20);
                itemResult.VerticalAlign = FormLayoutVerticalAlign.Top;
                itemResult.Paddings.PaddingTop = Unit.Pixel(10);
                itemResult.Paddings.PaddingBottom = Unit.Pixel(10);
                itemResult.Caption = "";
                itemResult.ShowCaption = DevExpress.Utils.DefaultBoolean.False;

                ASPxLabel lbResult = new ASPxLabel();
                lbResult.Width = Unit.Percentage(100);
                //lbResult.Font.Size = FontUnit.Point(12);
                lbResult.Font.Underline = true;
                lbResult.Font.Bold = true;
                lbResult.Text = "";
                itemResult.Controls.Add(lbResult);

                ((LayoutGroup)group).Items.Add(itemCheckList);
                ((LayoutGroup)group).Items.Add(itemResult);
            }
        }
        private void AddEnableQuestionAnswer(QuestionAndAnswerModel value, int StatusCode)
        {
            var group = FormLayoutQNA.FindItemOrGroupByName("QuestionsAndAnswer");
            if (group != null)
            {
                LayoutItem itemQuestion = new LayoutItem();
                itemQuestion.Width = Unit.Percentage(80);
                itemQuestion.VerticalAlign = FormLayoutVerticalAlign.Top;
                itemQuestion.Paddings.PaddingTop = Unit.Pixel(10);
                itemQuestion.Paddings.PaddingBottom = Unit.Pixel(10);
                itemQuestion.Caption = "";
                itemQuestion.ShowCaption = DevExpress.Utils.DefaultBoolean.False;

                ASPxLabel lbQuestion = new ASPxLabel();
                lbQuestion.ID = "lbQuestion" + value.ID.ToString();
                lbQuestion.Width = Unit.Percentage(100);
                lbQuestion.Text = value.Question;
                if (value.Selection == "") lbQuestion.Font.Bold = true;
                itemQuestion.Controls.Add(lbQuestion);

                LayoutItem itemAnswer = new LayoutItem();
                itemAnswer.Width = Unit.Percentage(20);
                itemAnswer.VerticalAlign = FormLayoutVerticalAlign.Top;
                itemAnswer.Paddings.PaddingTop = Unit.Pixel(10);
                itemAnswer.Paddings.PaddingBottom = Unit.Pixel(10);
                itemAnswer.Caption = "";
                itemAnswer.ShowCaption = DevExpress.Utils.DefaultBoolean.False;

                ASPxComboBox cbAnswer = new ASPxComboBox();
                cbAnswer.ValidationSettings.Display = Display.Dynamic;
                cbAnswer.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.Text;
                cbAnswer.ValidationSettings.ErrorText = "Please select the Answer.";
                cbAnswer.ValidationSettings.RequiredField.ErrorText = "Please select the Answer.";
                cbAnswer.ValidationSettings.RequiredField.IsRequired = true;
                cbAnswer.ValidationSettings.ErrorTextPosition = ErrorTextPosition.Bottom;
                cbAnswer.ValidationSettings.SetFocusOnError = true;
                cbAnswer.InvalidStyle.BackColor = ColorTranslator.FromHtml("#FFE6EE");
                cbAnswer.ClientSideEvents.SelectedIndexChanged = "function OnListBoxIndexChanged(s, e) { QuestionsAndAnswerCallbackPanel.PerformCallback(''); }";
                cbAnswer.Width = Unit.Percentage(100);

                string[] strplit = value.Selection.Split('|');
                if (strplit.Length > 1)
                {
                    for (int i = 0; i < strplit.Length; i++)
                        cbAnswer.Items.Add(strplit[i]);
                }
                else
                {
                    cbAnswer.Items.Add(value.Selection);
                }
                cbAnswer.ID = "Answer_" + value.ID.ToString();
                cbAnswer.Text = value.Answer;
                if (value.Selection == "") cbAnswer.Visible = false;
                itemAnswer.Controls.Add(cbAnswer);
                if (StatusCode == 0 || StatusCode == 98)
                    cbAnswer.Enabled = true;
                else
                    cbAnswer.Enabled = false;

                ((LayoutGroup)group).Items.Add(itemQuestion);
                ((LayoutGroup)group).Items.Add(itemAnswer);
            }
        }





        private void ClearSafetyList()
        {
            var group = flCheckListGroup.FindItemOrGroupByName("SafetyCheckListQA");
            if (group != null)
                ((LayoutGroup)group).Items.Clear();
        }
        private void AddEnableCheckList(QuestionAndAnswerModel value, int StatusCode)
        {
            var group = flCheckListGroup.FindItemOrGroupByName("SafetyCheckListQA");
            if (group != null)
            {
                string[] strsplit = value.Selection.Split('|');
                switch (strsplit[0])
                {
                    case "N":
                        LayoutItem itemQuestion1 = new LayoutItem();
                        itemQuestion1.Width = Unit.Percentage(100);
                        itemQuestion1.Paddings.PaddingLeft = Unit.Pixel(25);
                        itemQuestion1.VerticalAlign = FormLayoutVerticalAlign.Middle;
                        itemQuestion1.Caption = "";
                        itemQuestion1.ShowCaption = DevExpress.Utils.DefaultBoolean.False;
                        ASPxLabel lbQuestion1 = new ASPxLabel();
                        lbQuestion1.ID = "lbQuestion" + value.ID.ToString();
                        lbQuestion1.Width = Unit.Percentage(100);
                        lbQuestion1.Text = value.Question;
                        lbQuestion1.Font.Bold = false;
                        itemQuestion1.Controls.Add(lbQuestion1);
                        ((LayoutGroup)group).Items.Add(itemQuestion1);
                        break;
                    case "B1":
                        LayoutItem itemQuestion2 = new LayoutItem();
                        itemQuestion2.Paddings.PaddingTop = Unit.Pixel(10);
                        itemQuestion2.Width = Unit.Percentage(100);
                        itemQuestion2.VerticalAlign = FormLayoutVerticalAlign.Middle;
                        itemQuestion2.Caption = "";
                        itemQuestion2.ShowCaption = DevExpress.Utils.DefaultBoolean.False;
                        ASPxLabel lbQuestion2 = new ASPxLabel();
                        lbQuestion2.ID = "lbQuestion" + value.ID.ToString();
                        lbQuestion2.Width = Unit.Percentage(100);
                        lbQuestion2.Text = value.Question;
                        lbQuestion2.Font.Bold = true;
                        lbQuestion2.Font.Size = 12;
                        lbQuestion2.Font.Underline = true;
                        itemQuestion2.Controls.Add(lbQuestion2);
                        ((LayoutGroup)group).Items.Add(itemQuestion2);
                        break;
                    case "B2":
                        LayoutItem itemQuestion4 = new LayoutItem();
                        itemQuestion4.Paddings.PaddingTop = Unit.Pixel(10);
                        itemQuestion4.Paddings.PaddingBottom = Unit.Pixel(10);
                        itemQuestion4.Paddings.PaddingLeft = Unit.Pixel(10);
                        itemQuestion4.Width = Unit.Percentage(100);
                        itemQuestion4.VerticalAlign = FormLayoutVerticalAlign.Middle;
                        itemQuestion4.Caption = "";
                        itemQuestion4.ShowCaption = DevExpress.Utils.DefaultBoolean.False;
                        ASPxLabel lbQuestion4 = new ASPxLabel();
                        lbQuestion4.ID = "lbQuestion" + value.ID.ToString();
                        lbQuestion4.Width = Unit.Percentage(100);
                        lbQuestion4.Text = value.Question;
                        lbQuestion4.Font.Bold = true;
                        //lbQuestion4.Font.Size = 12;
                        itemQuestion4.Controls.Add(lbQuestion4);
                        ((LayoutGroup)group).Items.Add(itemQuestion4);
                        break;
                    case "T":
                        LayoutItem itemQuestionT = new LayoutItem();
                        itemQuestionT.Width = Unit.Percentage(100);
                        itemQuestionT.Paddings.PaddingLeft = Unit.Pixel(20);
                        itemQuestionT.VerticalAlign = FormLayoutVerticalAlign.Middle;
                        itemQuestionT.Caption = value.Question;
                        itemQuestionT.ShowCaption = DevExpress.Utils.DefaultBoolean.True;
                        ASPxTextBox txtBox = new ASPxTextBox();
                        txtBox.ClientSideEvents.ValueChanged = "function(s, e) { cpSafetyCheckListSelect.PerformCallback(''); }";
                        txtBox.ID = "Answer_" + value.ID.ToString();
                        txtBox.Width = Unit.Percentage(100);
                        txtBox.MaxLength = 450;
                        if (value.Answer != null && value.Answer != "") txtBox.Text = value.Answer;
                        if (StatusCode == 0 || StatusCode == 98) txtBox.Enabled = true;
                        else txtBox.Enabled = false;
                        itemQuestionT.Controls.Add(txtBox);
                        ((LayoutGroup)group).Items.Add(itemQuestionT);
                        break;
                    case "CL":
                        LayoutItem itemQuestion3L = new LayoutItem();
                        itemQuestion3L.Width = Unit.Percentage(100);
                        itemQuestion3L.Paddings.PaddingLeft = Unit.Pixel(45);
                        itemQuestion3L.VerticalAlign = FormLayoutVerticalAlign.Middle;
                        itemQuestion3L.Caption = "";
                        itemQuestion3L.ShowCaption = DevExpress.Utils.DefaultBoolean.False;
                        //itemQuestion3L.Paddings.PaddingTop = Unit.Pixel(0);
                        //itemQuestion3L.Paddings.PaddingBottom = Unit.Pixel(0);
                        ASPxCheckBox chkBox = new ASPxCheckBox();
                        chkBox.ID = "Answer_" + value.ID.ToString();
                        chkBox.ClientSideEvents.CheckedChanged = "function(s, e) { cpSafetyCheckListSelect.PerformCallback(''); }";
                        chkBox.Width = Unit.Percentage(95);
                        chkBox.Text = value.Question;
                        chkBox.TextAlign = TextAlign.Right;
                        chkBox.Font.Bold = true;
                        if (value.Answer != null && value.Answer != "") chkBox.Checked = (value.Answer == "T");
                        if (StatusCode == 0 || StatusCode == 98) chkBox.Enabled = true;
                        else chkBox.Enabled = false;
                        itemQuestion3L.Controls.Add(chkBox);
                        ((LayoutGroup)group).Items.Add(itemQuestion3L);
                        break;
                    case "CR":
                        LayoutItem itemQuestion3R = new LayoutItem();
                        itemQuestion3R.Width = Unit.Percentage(100);
                        itemQuestion3R.Paddings.PaddingLeft = Unit.Pixel(80);
                        itemQuestion3R.VerticalAlign = FormLayoutVerticalAlign.Middle;
                        itemQuestion3R.Caption = "";
                        itemQuestion3R.ShowCaption = DevExpress.Utils.DefaultBoolean.False;
                        //itemQuestion3R.Paddings.PaddingTop = Unit.Pixel(20);
                        //itemQuestion3R.Paddings.PaddingBottom = Unit.Pixel(0);
                        ASPxCheckBox chkBoxR = new ASPxCheckBox();
                        chkBoxR.ID = "Answer_" + value.ID.ToString();
                        chkBoxR.ClientSideEvents.CheckedChanged = "function(s, e) { cpSafetyCheckListSelect.PerformCallback(''); }";
                        chkBoxR.Width = Unit.Percentage(95);
                        chkBoxR.Text = value.Question;
                        chkBoxR.TextAlign = TextAlign.Right;
                        chkBoxR.Font.Bold = false;
                        if (value.Answer != null && value.Answer != "") chkBoxR.Checked = (value.Answer == "T");
                        if (StatusCode == 0 || StatusCode == 98) chkBoxR.Enabled = true;
                        else chkBoxR.Enabled = false;
                        itemQuestion3R.Controls.Add(chkBoxR);
                        ((LayoutGroup)group).Items.Add(itemQuestion3R);
                        break;
                    case "L":
                        LayoutItem itemQuestionL = new LayoutItem();
                        itemQuestionL.Width = Unit.Percentage(100);
                        itemQuestionL.Paddings.PaddingLeft = Unit.Pixel(20);
                        itemQuestionL.VerticalAlign = FormLayoutVerticalAlign.Middle;
                        itemQuestionL.Caption = value.Question;
                        itemQuestionL.ShowCaption = DevExpress.Utils.DefaultBoolean.True;
                        ASPxListBox lstBox = new ASPxListBox();
                        lstBox.ID = "Answer_" + value.ID.ToString();
                        lstBox.ClientSideEvents.SelectedIndexChanged = "function(s, e) { cpSafetyCheckListSelect.PerformCallback(''); }";
                        lstBox.Width = Unit.Percentage(100);
                        lstBox.SelectionMode = ListEditSelectionMode.CheckColumn;
                        for (int i = 1; i < strsplit.Length; i++)
                            lstBox.Items.Add(strsplit[i]);
                        itemQuestionL.Controls.Add(lstBox);

                        if (value.Answer != null && value.Answer != "")
                        {
                            string[] strAns = value.Answer.Split('|');
                            for (int i = 0; i < strAns.Length; i++)
                            {
                                ListEditItem itm = lstBox.Items.FindByText(strAns[i]);
                                if (itm != null) itm.Selected = true;
                            }
                        }

                        ((LayoutGroup)group).Items.Add(itemQuestionL);
                        break;
                    case "M":
                        LayoutItem itemQuestionM = new LayoutItem();
                        itemQuestionM.Width = Unit.Percentage(100);
                        itemQuestionM.Paddings.PaddingLeft = Unit.Pixel(20);
                        itemQuestionM.VerticalAlign = FormLayoutVerticalAlign.Middle;
                        itemQuestionM.Caption = value.Question;
                        itemQuestionM.ShowCaption = DevExpress.Utils.DefaultBoolean.False;
                        ASPxComboBox cbAnswer = new ASPxComboBox();
                        cbAnswer.ValidationSettings.Display = Display.Dynamic;
                        cbAnswer.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.Text;
                        cbAnswer.ValidationSettings.ErrorText = "Please select the Answer.";
                        cbAnswer.ValidationSettings.RequiredField.ErrorText = "Please select the Answer.";
                        cbAnswer.ValidationSettings.RequiredField.IsRequired = true;
                        cbAnswer.ValidationSettings.ErrorTextPosition = ErrorTextPosition.Bottom;
                        cbAnswer.ValidationSettings.SetFocusOnError = true;
                        //cbAnswer.InvalidStyle.BackColor = ColorTranslator.FromHtml("#FFE6EE");
                        cbAnswer.ClientSideEvents.SelectedIndexChanged = "function OnListBoxIndexChanged(s, e) { cpSafetyCheckListSelect.PerformCallback(''); }";
                        cbAnswer.Width = Unit.Percentage(100);
                        if (strsplit.Length > 1)
                        {
                            for (int i = 1; i < strsplit.Length; i++)
                                cbAnswer.Items.Add(strsplit[i]);
                        }
                        else
                        {
                            cbAnswer.Items.Add(value.Selection);
                        }
                        itemQuestionM.Controls.Add(cbAnswer);
                        ((LayoutGroup)group).Items.Add(itemQuestionM);
                        break;
                }
            }
        }
        //private void test()
        //{
        //    if (Session["TBM_Equipment"] != null)
        //    {
        //        tbmequipmentlist = (List<TBMEquipmentModel>)Session["TBM_Equipment"];
        //        if (tbmequipmentlist.Count > 0)
        //        {
        //            safetydetaillist = new List<QuestionAndAnswerModel>();
        //            var distinctmactype = tbmequipmentlist.Select(Item => Item.MachineType).Distinct().ToList();
        //            foreach (var machinetype in distinctmactype)
        //            {
        //                List<QuestionAndAnswerModel> tempCheckList;
        //                tempCheckList = TemplateViewModel.GetTemplateDetails("SAFETYCHECKLIST-" + machinetype.ToString());
        //                safetydetaillist.AddRange(tempCheckList);
        //            }
        //            foreach (QuestionAndAnswerModel tmp in safetydetaillist)
        //                AddEnableCheckList(tmp, 0);
        //            Session["TBM_SafetyDetails"] = safetydetaillist;
        //        }
        //    }
        //}
        protected void cpSafetyCheckList_Callback(object sender, CallbackEventArgsBase e)
        {
            ClearSafetyList();
            if (Session["TBM_Equipment"] != null)
            {
                tbmequipmentlist = (List<TBMEquipmentModel>)Session["TBM_Equipment"];
                if (tbmequipmentlist.Count > 0)
                {
                    safetydetaillist = new List<QuestionAndAnswerModel>();
                    var distinctmactype = tbmequipmentlist.Select(Item => Item.MachineType).Distinct().ToList();
                    foreach (var machinetype in distinctmactype)
                    {
                        List<QuestionAndAnswerModel> tempCheckList;
                        tempCheckList = TemplateViewModel.GetTemplateDetails("SAFETYCHECKLIST-" + machinetype.ToString());
                        safetydetaillist.AddRange(tempCheckList);
                    }
                    foreach (QuestionAndAnswerModel tmp in safetydetaillist)
                        AddEnableCheckList(tmp, 0);
                    Session["TBM_SafetyDetails"] = safetydetaillist;
                }
            }
        }
        protected void cpSafetyCheckListSelect_Callback(object sender, CallbackEventArgsBase e)
        {
            var group = flCheckListGroup.FindItemOrGroupByName("SafetyCheckListQA");
            if (group != null)
            {
                safetydetaillist = (List<QuestionAndAnswerModel>)Session["TBM_SafetyDetails"];
                foreach (var item in ((LayoutGroup)group).Items)
                {
                    if (item is LayoutItem)
                    {
                        foreach (var control in ((LayoutItem)item).Controls)
                        {
                            if (control is ASPxCheckBox)
                            {
                                if (((ASPxCheckBox)control).ID.Contains("Answer_"))
                                {
                                    string[] strplit = ((ASPxCheckBox)control).ID.Split('_');
                                    int id = Convert.ToInt32(strplit[1]);
                                    string answer = "F";
                                    if (((ASPxCheckBox)control).Checked == true) answer = "T";
                                    var detail = safetydetaillist.FirstOrDefault(itm => itm.ID == id);
                                    if (detail != null) detail.Answer = answer;
                                }
                            }
                        }
                    }
                }
                Session["TBM_SafetyDetails"] = safetydetaillist;
            }
        }

        protected void pcMultiple_Load(object sender, EventArgs e)
        {
            var project = UserViewModel.GetSelectedProject();

            if (project != null)
            {
                List<UserModel> userlist = UserViewModel.GetUser_CodeForModule(project.ToString());
                gvMultiple.DataSource = userlist;
                gvMultiple.DataBind();
            }
        }

        protected void btOK_Click(object sender, EventArgs e)
        {
            pcMultiple.ShowOnPageLoad = false;
        }



        protected void txtDescription_Validation(object sender, ValidationEventArgs e)
        {
            if (txtDescription.Text == "" && txtDescriptionPM.Text == "")
            {
                e.IsValid = false;
                e.ErrorText = "Please fill in the Work Description field. Please note: Only one Work Description (AM or PM) will be accepted.";
            }
            else
            {
                if (txtDescription.Text != "" && txtDescriptionPM.Text != "")
                {
                    e.IsValid = false;
                    e.ErrorText = "Please note: Only one Work Description (AM or PM) will be accepted.";
                }
                else
                {
                    e.IsValid = true;
                    e.ErrorText = "";
                }
            }
        }

        protected void cbDeLine_Validation(object sender, ValidationEventArgs e)
        {
            if ((sender as ASPxCheckBox).Checked)
            {
                e.IsValid = true;
                e.ErrorText = "";
            }
            else
            {
                e.IsValid = false;
                e.ErrorText = "Please check the box for confirmation.";
            }
        }
    }
}