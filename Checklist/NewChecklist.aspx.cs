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

namespace HEA.ePTW.Checklist
{
    public partial class NewChecklist : System.Web.UI.Page
    {
        string strKey = Guid.NewGuid().ToString();




        List<AttachmentModel> attachlist;
        //List<KeyActivitiesModel> keyactivitieslist;
        //List<TBMAttendeeModel> tbmattendeelist;
        //List<TBMHazardModel> hazardlist;
        List<UserModel> alluserlist;
        //List<QuestionAndAnswerModel> templatedetaillist;
        List<CHKEquipmentModel> chkequipmentlist;
        //List<EquipmentModel> equipmentlist;
        List<QuestionAndAnswerModel> safetydetaillist;
        private bool currentSafetySectionNotApplicable;

        ChecklistModel master;

        private void RetrieveFromQueryString()
        {
            string strTask = (Request.QueryString["Status"] == null) ? "" : Request.QueryString["Status"].ToString();
            if (strTask == "N")
            {
                Session["CHK_Record_IMG"] = null;
                Session["CHK_KEYACTLIST"] = null;
                Session["CHK_Record"] = null;
                Session["CHK_Hazard"] = null;
                Session["CHK_Attendee"] = null;
            }
            Session["CHK_Task"] = strTask;
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

                    //keyactivitieslist = KeyActivitiesViewModel.GetKeyActivitiesList("CHK");

                    if (Session["CHK_Record"] != null)
                    {
                        master = (ChecklistModel)Session["CHK_Record"];

                        attachlist = AttachmentViewModel.GetAttachmentList(master.Key);
                        //tbmattendeelist = TBMAttendeeViewModel.GetTBMAttendeeList(master.Key);
                        //hazardlist = TBMHazardViewModel.GetTBMHazardList(master.Key);
                        //templatedetaillist = TBMViewModel.GetTBMDetails(master.Key);
                        chkequipmentlist = CHKEquipmentViewModel.GetCHKEquipmentList(master.Key);
                        safetydetaillist = ChecklistViewModel.GetCHKChecklist(master.Key);
                    }
                    else
                    {
                        master = new ChecklistModel();
                        master.Key = strKey;
                        master.ProjectName = project;
                        master.ConductedBy = user.UserID;
                        master.ConductedByName = user.FullName;
                        master.ConductedCompany = user.ConstructorName;
                        master.ConductedPosition = user.Position;
                        master.MeetingDate = DateTime.Now;
                        Session["CHK_Record"] = master;
                        attachlist = new List<AttachmentModel>();
                        //tbmattendeelist = new List<TBMAttendeeModel>();
                        //hazardlist = new List<TBMHazardModel>();
                        chkequipmentlist = new List<CHKEquipmentModel>();
                        //templatedetaillist = TemplateViewModel.GetTemplateDetails("TBM");
                        safetydetaillist = new List<QuestionAndAnswerModel>();
                    }

                    //alluserlist = UserViewModel.GetUser_CodeForModule(project.ToString());
                    //if (alluserlist != null) alluserlist = alluserlist.Where(item => item.ConstructorName == user.ConstructorName).ToList();

                    //Session["TBM_TemplateDetails"] = templatedetaillist;
                    Session["CHK_Record_IMG"] = attachlist;
                    //Session["TBM_KEYACTLIST"] = keyactivitieslist;
                    //Session["TBM_Attendee"] = tbmattendeelist;
                    //Session["TBM_Hazard"] = hazardlist;
                    Session["CHK_UserList"] = alluserlist;
                    Session["CHK_Equipment"] = chkequipmentlist;
                    Session["CHK_SafetyDetails"] = safetydetaillist;

                    BindMaster();
                }
                else
                {
                    attachlist = (List<AttachmentModel>)Session["CHK_Record_IMG"];
                    //keyactivitieslist = (List<KeyActivitiesModel>)Session["TBM_KEYACTLIST"];
                    //tbmattendeelist = (List<TBMAttendeeModel>)Session["TBM_Attendee"];
                    //hazardlist = (List<TBMHazardModel>)Session["TBM_Hazard"];
                    alluserlist = (List<UserModel>)Session["CHK_UserList"];
                    //templatedetaillist = (List<QuestionAndAnswerModel>)Session["TBM_TemplateDetails"];
                    safetydetaillist = (List<QuestionAndAnswerModel>)Session["CHK_SafetyDetails"];
                    chkequipmentlist = (List<CHKEquipmentModel>)Session["CHK_Equipment"];
                    master = (ChecklistModel)Session["CHK_Record"];
                }

                //Show Title & Location
                string strLatitude = hfLatitude.Value;
                string strLongitude = hfLongitude.Value;
                string strLocation = "";
                switch (master.Status)
                {
                    case 0:
                        lblTitle.Text = "Safety Action Key Points Checklist - New";
                        if (strLatitude != "") strLocation = $"Location : Latitude {strLatitude}, Longitude {strLongitude}";
                        break;
                    case 1:
                        lblTitle.Text = "Safety Action Key Points Checklist - Submitted";
                        if (master.Latitude != "") strLocation = $"Location : Latitude {master.Latitude}, Longitude {master.Longitude}";
                        break;
                    case 2:
                        lblTitle.Text = "Safety Action Key Points Checklist - Approved";
                        if (master.Latitude != "") strLocation = $"Location : Latitude {master.Latitude}, Longitude {master.Longitude}";
                        break;
                    case 98:
                        lblTitle.Text = "Safety Action Key Points Checklist - Returned";
                        if (strLatitude != "") strLocation = $"Location : Latitude {strLatitude}, Longitude {strLongitude}";
                        break;
                    case 99:
                        lblTitle.Text = "Safety Action Key Points Checklist - Rejected";
                        if (master.Latitude != "") strLocation = $"Location : Latitude {master.Latitude}, Longitude {master.Longitude}";
                        break;
                }
                lblLocation.Text = strLocation;


                //if (templatedetaillist != null)
                //{
                //    ClearQuestionAndAnswer();
                //    foreach (QuestionAndAnswerModel ent in templatedetaillist)
                //        AddEnableQuestionAnswer(ent, master.Status);
                //}

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
                if (returnrejectgroup != null) (returnrejectgroup as LayoutGroup).Visible = false;
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
                //cvAttachmentDocument.SettingsDataSecurity.AllowDelete = false;
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
                    cbDeLine1.Enabled = true;
                    cbDeLine2.Enabled = true;
                    cbDeLine3.Enabled = true;
                    cbDeLine4.Enabled = true;
                    btnSubmit.Visible = true;
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
                    //cvAttachmentDocument.SettingsDataSecurity.AllowDelete = true;
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
                    if (approverRole != null || UserViewModel.IsAdmin(user.UserID))
                    {
                        if (returnrejectgroup != null) (returnrejectgroup as LayoutGroup).Visible = true;
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
                    lblSubmitStatus.Text = "Applicant Details - Submitted";
                    //cvAttachmentDocument.SettingsDataSecurity.AllowDelete = false;
                    //if (uploadfilegroup != null)
                    //{
                    //    var EditDocument = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("EditAttachmentDoc");
                    //    if (EditDocument != null) (EditDocument as LayoutItem).Visible = true;
                    //    var DisplayImage = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("DisplayImage");
                    //    if (DisplayImage != null) (DisplayImage as LayoutItem).Visible = true;
                    //}
                }
                if (master.Status == 2)
                {
                    lblLocation.Text = strLocation;
                    //cvAttachmentDocument.SettingsDataSecurity.AllowDelete = false;
                    if (uploadfilegroup != null)
                    {
                        var EditDocument = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("EditAttachmentDoc");
                        if (EditDocument != null) (EditDocument as LayoutItem).Visible = true;
                        var DisplayImage = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("DisplayImage");
                        if (DisplayImage != null) (DisplayImage as LayoutItem).Visible = true;
                    }
                    lblSubmitStatus.Text = "Applicant Details - Submitted";
                    if (approvalgroup != null) (approvalgroup as LayoutGroup).Visible = true;
                    lblApprovalCompany.Text = master.ApproveCompany;
                    lblApprovalDesignation.Text = master.ApprovePosition;
                    lblApprovalName.Text = master.ApproveName;
                    txtApprovalRemarks.Enabled = false;
                    lblApprovalStatus.Text = "Part 2: Approval by HEA Project Manager / Authorized Competent Person";
                }
                if (master.Status == 99)
                {
                    if (returnrejectgroup != null) (returnrejectgroup as LayoutGroup).Visible = true;
                    txtReason.Visible = true;
                    txtReason.Enabled = false;
                    //cvAttachmentDocument.SettingsDataSecurity.AllowDelete = false;
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
                    ////cvAttachmentDocument.SettingsDataSecurity.AllowDelete = true;
                    //if (uploadfilegroup != null)
                    //{
                    //    var UploadFilePanel = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("UploadFilePanel");
                    //    if (UploadFilePanel != null) (UploadFilePanel as LayoutItem).Visible = true;
                    //    var UploadFileNote = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("UploadFileNote");
                    //    if (UploadFileNote != null) (UploadFileNote as LayoutItem).Visible = true;
                    //    var EditDocument = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("EditAttachmentDoc");
                    //    if (EditDocument != null) (EditDocument as LayoutItem).Visible = true;
                    //    var DisplayImage = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("DisplayImage");
                    //    if (DisplayImage != null) (DisplayImage as LayoutItem).Visible = true;
                    //}
                    if (returnrejectgroup != null) (returnrejectgroup as LayoutGroup).Visible = true;

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
                    currentSafetySectionNotApplicable = false;
                    foreach (QuestionAndAnswerModel ent in safetydetaillist)
                        AddEnableCheckList(ent, master.Status);
                }

                //cvAttachmentDocument.DataSource = attachlist;
                //cvAttachmentDocument.DataBind();
                //DisplayImageItems.DataSource = attachlist.Where(item => item.FileType == "IMG");
                //DisplayImageItems.DataBind();
                //gvStaff.DataSource = tbmattendeelist;
                //gvStaff.DataBind();
                //gvHazards.DataSource = hazardlist;
                //gvHazards.DataBind();
                gvEquipment.DataSource = chkequipmentlist;
                gvEquipment.DataBind();
            }
            catch (Exception ex)
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
            cbDeLine1.Checked = master.SafetyDeclaration1 == "Y";
            cbDeLine2.Checked = master.SafetyDeclaration2 == "Y";
            cbDeLine3.Checked = master.SafetyDeclaration3 == "Y";
            cbDeLine4.Checked = master.SafetyDeclaration4 == "Y";
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
            //ASPxGridView tempGrid = (ASPxGridView)sender;
            //UserModel user = UserViewModel.GetLoggedInUserInfo();
            //master = (ChecklistModel)Session["CHK_Record"];
            //TBMAttendeeModel ent = new TBMAttendeeModel();
            //ent.Key = master.Key;
            //ent.UserID = e.NewValues["UserID"] != null ? e.NewValues["UserID"].ToString() : "";
            //ent.FullName = e.NewValues["FullName"] != null ? e.NewValues["FullName"].ToString() : "";
            //ent.Position = e.NewValues["Position"] != null ? e.NewValues["Position"].ToString() : "";
            //ent.ConstructorName = e.NewValues["ConstructorName"] != null ? e.NewValues["ConstructorName"].ToString() : "";
            //ent.Created = DateTime.Now;
            //ent.Updated = DateTime.Now;
            //ent.CreatedBy = user.UserID;
            //ent.UpdatedBy = user.UserID;
            //tbmattendeelist.Add(ent);
            //Session["CHK_Attendee"] = tbmattendeelist;
            //e.Cancel = true;
            //tempGrid.CancelEdit();
            //tempGrid.DataSource = tbmattendeelist;
        }
        protected void gvStaff_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //ASPxGridView tempGrid = (ASPxGridView)sender;
            //string strUserID = tempGrid.GetRowValues(tempGrid.FocusedRowIndex, "UserID").ToString();
            //if (strUserID != "")
            //{
            //    tbmattendeelist = (List<TBMAttendeeModel>)Session["TBM_Attendee"];

            //    TBMAttendeeModel ent = tbmattendeelist.FirstOrDefault(item => item.UserID == strUserID);
            //    TBMAttendeeViewModel.TBMAttendee_Delete(ent.UserID, ent.Key);
            //    tbmattendeelist.Remove(ent);
            //    Session["PTW_Attendee"] = tbmattendeelist;

            //    e.Cancel = true;
            //    tempGrid.CancelEdit();
            //    tempGrid.DataSource = tbmattendeelist;
            //}
        }

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
            master = (ChecklistModel)Session["CHK_Record"];
            chkequipmentlist = (List<CHKEquipmentModel>)Session["CHK_Equipment"];

            CHKEquipmentModel ent = new CHKEquipmentModel();
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

            chkequipmentlist.Add(ent);
            Session["CHK_Equipment"] = chkequipmentlist;

            e.Cancel = true;
            gvEquipment.CancelEdit();
            gvEquipment.DataSource = chkequipmentlist;
        }
        protected void gvEquipment_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ASPxGridView tempGrid = (ASPxGridView)sender;
            string strName = tempGrid.GetRowValues(tempGrid.FocusedRowIndex, "RegistrationNo").ToString();
            if (strName != "")
            {
                chkequipmentlist = (List<CHKEquipmentModel>)Session["CHK_Equipment"];

                CHKEquipmentModel ent = chkequipmentlist.FirstOrDefault(item => item.RegistrationNo == strName);
                CHKEquipmentViewModel.CHKEquipment_Delete(ent.EquipmentName, ent.Key);
                chkequipmentlist.Remove(ent);
                Session["CHK_Equipment"] = chkequipmentlist;

                e.Cancel = true;
                tempGrid.CancelEdit();
                tempGrid.DataSource = chkequipmentlist;
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
            //ASPxGridView gridView = sender as ASPxGridView;
            //if (gridView.IsEditing && e.Column.FieldName == "WorkActivity")
            //{
            //    ASPxComboBox cbWorkActivity = e.Editor as ASPxComboBox;
            //    keyactivitieslist = (List<KeyActivitiesModel>)Session["TBM_KEYACTLIST"];
            //    cbWorkActivity.DataSource = keyactivitieslist;
            //    cbWorkActivity.TextField = "Name";
            //    cbWorkActivity.ValueField = "Name";
            //    cbWorkActivity.DataBind();
            //    cbWorkActivity.ClientSideEvents.SelectedIndexChanged = "onSelectedWorkChanged";
            //    cbWorkActivity.ClientSideEvents.Init = "onWorkInit";
            //}
            //if (gridView.IsEditing && e.Column.FieldName == "CauseOfHazard")
            //{
            //    ASPxMemo txtTemp = e.Editor as ASPxMemo;
            //    txtTemp.Height = 150;
            //}

            //if (gridView.IsEditing && e.Column.FieldName == "HappenAsResult")
            //{
            //    ASPxMemo txtTemp = e.Editor as ASPxMemo;
            //    txtTemp.Height = 150;
            //}

            //if (gridView.IsEditing && e.Column.FieldName == "ActionRemarks")
            //{
            //    ASPxMemo txtTemp = e.Editor as ASPxMemo;
            //    txtTemp.Height = 150;
            //}
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
            //ASPxGridView tempGrid = (ASPxGridView)sender;
            //UserModel user = UserViewModel.GetLoggedInUserInfo();
            //master = (ChecklistModel)Session["CHK_Record"];
            //hazardlist = (List<TBMHazardModel>)Session["CHK_Hazard"];

            //TBMHazardModel ent = new TBMHazardModel();
            //ent.ID = 0;
            //ent.Key = master.Key;
            //ent.WorkActivity = e.NewValues["WorkActivity"] != null ? e.NewValues["WorkActivity"].ToString() : "";
            //ent.Others = e.NewValues["Others"] != null ? e.NewValues["Others"].ToString() : "";
            //ent.CauseOfHazard = e.NewValues["CauseOfHazard"] != null ? e.NewValues["CauseOfHazard"].ToString() : "";
            //ent.HappenAsResult = e.NewValues["HappenAsResult"] != null ? e.NewValues["HappenAsResult"].ToString() : "";

            //ent.ActionToTaken = e.NewValues["ActionToTaken"] != null ? e.NewValues["ActionToTaken"].ToString() : "";
            //ent.ActionRemarks = e.NewValues["ActionRemarks"] != null ? e.NewValues["ActionRemarks"].ToString() : "";

            //ent.Created = DateTime.Now;
            //ent.Updated = DateTime.Now;
            //ent.CreatedBy = user.UserID;
            //ent.UpdatedBy = user.UserID;

            //hazardlist.Add(ent);
            //Session["TBM_Hazard"] = hazardlist;

            //e.Cancel = true;
            //tempGrid.CancelEdit();
            //tempGrid.DataSource = hazardlist;
        }
        protected void gvHazards_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //ASPxGridView tempGrid = (ASPxGridView)sender;
            //string strWork = tempGrid.GetRowValues(tempGrid.FocusedRowIndex, "WorkActivity").ToString();
            //if (strWork != "")
            //{
            //    hazardlist = (List<TBMHazardModel>)Session["TBM_Hazard"];
            //    TBMHazardModel ent = hazardlist.FirstOrDefault(item => item.WorkActivity == strWork);
            //    if (ent.ID > 0) TBMHazardViewModel.TBMHazard_Delete(ent.ID, ent.Key);
            //    hazardlist.Remove(ent);
            //    Session["PTW_Hazard"] = hazardlist;

            //    e.Cancel = true;
            //    tempGrid.CancelEdit();
            //    tempGrid.DataSource = hazardlist;
            //}
        }
        protected void gvHazards_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            //ASPxGridView tempGrid = (ASPxGridView)sender;
            //string strWork = tempGrid.GetRowValues(tempGrid.FocusedRowIndex, "WorkActivity").ToString();
            //UserModel user = UserViewModel.GetLoggedInUserInfo();
            ////master = (TBMModel)Session["TBM_Record"];
            //hazardlist = (List<TBMHazardModel>)Session["TBM_Hazard"];
            //TBMHazardModel ent = hazardlist.FirstOrDefault(item => item.WorkActivity == strWork);
            //if (ent != null)
            //{
            //    ent.WorkActivity = e.NewValues["WorkActivity"] != null ? e.NewValues["WorkActivity"].ToString() : "";
            //    ent.Others = e.NewValues["Others"] != null ? e.NewValues["Others"].ToString() : "";
            //    ent.CauseOfHazard = e.NewValues["CauseOfHazard"] != null ? e.NewValues["CauseOfHazard"].ToString() : "";
            //    ent.HappenAsResult = e.NewValues["HappenAsResult"] != null ? e.NewValues["HappenAsResult"].ToString() : "";

            //    ent.ActionToTaken = e.NewValues["ActionToTaken"] != null ? e.NewValues["ActionToTaken"].ToString() : "";
            //    ent.ActionRemarks = e.NewValues["ActionRemarks"] != null ? e.NewValues["ActionRemarks"].ToString() : "";

            //    ent.Updated = DateTime.Now;
            //    ent.UpdatedBy = user.UserID;
            //}
            //Session["TBM_Hazard"] = hazardlist;

            //e.Cancel = true;
            //tempGrid.CancelEdit();
            //tempGrid.DataSource = hazardlist;
        }








        // Document Workflow
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();
            // Dynamic checklist answers are stored in Session by callbacks.  Capture the
            // values posted with this submit as well, otherwise validation can use the
            // previous callback state and silently reject a valid checklist.
            CaptureSafetyCheckBoxAnswers();
            safetydetaillist = Session["CHK_SafetyDetails"] as List<QuestionAndAnswerModel> ?? safetydetaillist;
            lblChecklistError.Visible = false;

            Page.Validate();
            if (!Page.IsValid ||
                !ValidateCommonComplianceRequirements() ||
                !ValidateMandatorySafetySections())
            {
                lblChecklistError.Text = "Please check the box for confirmation.";
                lblChecklistError.Visible = true;
                return;
            }
            if (!cbDeLine1.Checked || !cbDeLine2.Checked || !cbDeLine3.Checked || !cbDeLine4.Checked)
            {
                lblChecklistError.Text = "All four Application Details confirmation boxes must be checked before submission.";
                lblChecklistError.Visible = true;
                return;
            }
            try
            {
                attachlist = (List<AttachmentModel>)Session["CHK_Record_IMG"];
                //templatedetaillist = (List<QuestionAndAnswerModel>)Session["TBM_TemplateDetails"];
                master = (ChecklistModel)Session["CHK_Record"];
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
                master.SafetyDeclaration1 = cbDeLine1.Checked ? "Y" : "N";
                master.SafetyDeclaration2 = cbDeLine2.Checked ? "Y" : "N";
                master.SafetyDeclaration3 = cbDeLine3.Checked ? "Y" : "N";
                master.SafetyDeclaration4 = cbDeLine4.Checked ? "Y" : "N";

                ChecklistViewModel.CHK_InsertUpdate(master);

                foreach (AttachmentModel img in attachlist)
                {
                    img.Key = master.Key;
                    AttachmentViewModel.Attachment_InsertUpdate(img);
                }
                //foreach (TBMAttendeeModel att in tbmattendeelist)
                //{
                //    att.Key = master.Key;
                //    TBMAttendeeViewModel.TBMAttendee_InsertUpdate(att);
                //}
                //foreach (TBMHazardModel hzd in hazardlist)
                //{
                //    hzd.Key = master.Key;
                //    TBMHazardViewModel.TBMHazard_InsertUpdate(hzd);
                //}
                //foreach (QuestionAndAnswerModel qa in templatedetaillist)
                //{
                //    qa.Key = master.Key;
                //    TBMViewModel.TBMDetail_InsertUpdate(qa);
                //}
                foreach (QuestionAndAnswerModel sc in safetydetaillist)
                {
                    sc.Key = master.Key;
                    ChecklistViewModel.CHKChecklist_InsertUpdate(sc);
                }
                foreach (CHKEquipmentModel eq in chkequipmentlist)
                {
                    eq.Key = master.Key;
                    CHKEquipmentViewModel.CHKEquipment_InsertUpdate(eq);
                }

                Session["CHK_Record"] = null;
                Session["CHK_Record_IMG"] = null;
                Session["CHK_TemplateDetails"] = null;
                Session["CHK_Attendee"] = null;
                Session["CHK_Hazard"] = null;
                Session["CHK_UserList"] = null;
                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/Checklist/MyChecklist.aspx");
                else
                    Response.Redirect("~/Checklist/MyChecklist.aspx");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Checklist submission failed: {0}", ex);
                lblChecklistError.Text = "The checklist could not be submitted. Please try again or contact the system administrator.";
                lblChecklistError.Visible = true;
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            try
            {
                master = (ChecklistModel)Session["CHK_Record"];
                master.Status = 97;
                master.Updated = DateTime.Now;
                master.UpdatedBy = user.UserID;
                master.ReturnRejectDate = null;
                master.ReturnRejectReason = "";
                master.ApprovedBy = "";
                master.ApprovedDate = null;

                ChecklistViewModel.CHK_InsertUpdate(master);

                Session["CHK_Record_IMG"] = null;
                Session["CHK_Record_DOC"] = null;
                Session["CHK_Attendee"] = null;
                Session["CHK_Hazard"] = null;
                Session["CHK_UserList"] = null;
                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/Checklist/PendingChecklist.aspx");
                else
                    Response.Redirect("~/Checklist/PendingChecklist.aspx");
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string strTask = (Session["CHK_Task"] == null) ? "" : Session["CHK_Task"].ToString();
            string strLink = "~/Checklist/MyChecklist.aspx";
            if (strTask == "P") strLink = "~/Checklist/PendingChecklist.aspx";
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
                master = (ChecklistModel)Session["CHK_Record"];
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
                ChecklistViewModel.CHK_InsertUpdate(master);

                Session["CHK_Record_IMG"] = null;
                Session["CHK_Record_DOC"] = null;
                Session["CHK_Attendee"] = null;
                Session["CHK_Hazard"] = null;
                Session["CHK_UserList"] = null;
                Session["CHK_Checklist"] = null;
                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/Checklist/PendingChecklist.aspx");
                else
                    Response.Redirect("~/Checklist/PendingChecklist.aspx");
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
                master = (ChecklistModel)Session["CHK_Record"];
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

                ChecklistViewModel.CHK_InsertUpdate(master);

                Session["CHK_Record_IMG"] = null;
                Session["CHK_Record_DOC"] = null;
                Session["CHK_Attendee"] = null;
                Session["CHK_Hazard"] = null;
                Session["CHK_UserList"] = null;
                Session["CHK_Checklist"] = null;
                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/Checklist/PendingChecklist.aspx");
                else
                    Response.Redirect("~/Checklist/PendingChecklist.aspx");
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
                master = (ChecklistModel)Session["CHK_Record"];
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

                ChecklistViewModel.CHK_InsertUpdate(master);

                Session["CHK_Record_IMG"] = null;
                Session["CHK_Record_DOC"] = null;
                Session["CHK_Attendee"] = null;
                Session["CHK_Hazard"] = null;
                Session["CHK_UserList"] = null;
                Session["CHK_Checklist"] = null;
                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/Checklist/PendingChecklist.aspx");
                else
                    Response.Redirect("~/Checklist/PendingChecklist.aspx");
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
            }
        }
        protected void ASPxCallbackResult_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string strLocation = e.Parameter;
            if (strLocation != "|")
            {
                string[] strLocationDTL = strLocation.Split('|');
                master = (ChecklistModel)Session["CHK_Record"];
                master.Latitude = strLocationDTL[0];
                master.Longitude = strLocationDTL[1];
                Session["CHK_Record"] = master;
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

                    Session["CHK_Record_IMG"] = attachlist;
                }
            }
        }
        protected void cvDocument_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var filename = e.Keys["FileName"];
            if (filename != null)
            {
                ASPxGridView tempGrid = (ASPxGridView)sender;
                attachlist = (List<AttachmentModel>)Session["CHK_Record_IMG"];
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
            //attachlist = (List<AttachmentModel>)Session["CHK_Record_IMG"];
            //DisplayImageItems.DataSource = attachlist.Where(item => item.FileType == "IMG");
            //DisplayImageItems.DataBind();
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
            //var group = FormLayoutQNA.FindItemOrGroupByName("QuestionsAndAnswer");
            //if (group != null)
            //{
            //    templatedetaillist = (List<QuestionAndAnswerModel>)Session["TBM_TemplateDetails"];
            //    foreach (var item in ((LayoutGroup)group).Items)
            //    {
            //        if (item is LayoutItem)
            //        {
            //            foreach (var control in ((LayoutItem)item).Controls)
            //            {
            //                if (control is ASPxComboBox)
            //                {
            //                    if (((ASPxComboBox)control).ID.Contains("Answer_"))
            //                    {
            //                        string[] strplit = ((ASPxComboBox)control).ID.Split('_');
            //                        int id = Convert.ToInt32(strplit[1]);
            //                        string answer = ((ASPxComboBox)control).Text;

            //                        var detail = templatedetaillist.FirstOrDefault(itm => itm.ID == id);
            //                        if (detail != null) detail.Answer = answer;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    Session["TBM_TemplateDetails"] = templatedetaillist;
            //}
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
                        bool canMarkLabelNotApplicable = CanMarkNotApplicable(value);
                        if (canMarkLabelNotApplicable)
                            currentSafetySectionNotApplicable = string.Equals(value.Answer, "NA", StringComparison.OrdinalIgnoreCase);
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
                        if (canMarkLabelNotApplicable)
                            AddSectionHeading(itemQuestion1, lbQuestion1, value, StatusCode);
                        else
                            itemQuestion1.Controls.Add(lbQuestion1);
                        ((LayoutGroup)group).Items.Add(itemQuestion1);
                        break;
                    case "B1":
                        currentSafetySectionNotApplicable = CanMarkNotApplicable(value) && string.Equals(value.Answer, "NA", StringComparison.OrdinalIgnoreCase);
                        LayoutItem itemQuestion2 = new LayoutItem();
                        itemQuestion2.Paddings.PaddingTop = Unit.Pixel(10);
                        itemQuestion2.Width = Unit.Percentage(100);
                        itemQuestion2.VerticalAlign = FormLayoutVerticalAlign.Middle;
                        itemQuestion2.Caption = "";
                        itemQuestion2.ShowCaption = DevExpress.Utils.DefaultBoolean.False;
                        ASPxLabel lbQuestion2 = new ASPxLabel();
                        lbQuestion2.ID = "lbQuestion" + value.ID.ToString();
                        lbQuestion2.Text = value.Question;
                        lbQuestion2.Font.Bold = true;
                        lbQuestion2.Font.Size = 12;
                        lbQuestion2.Font.Underline = true;
                        AddSectionHeading(itemQuestion2, lbQuestion2, value, StatusCode);
                        ((LayoutGroup)group).Items.Add(itemQuestion2);
                        break;
                    case "B2":
                        currentSafetySectionNotApplicable = CanMarkNotApplicable(value) && string.Equals(value.Answer, "NA", StringComparison.OrdinalIgnoreCase);
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
                        lbQuestion4.Text = value.Question;
                        lbQuestion4.Font.Bold = true;
                        //lbQuestion4.Font.Size = 12;
                        AddSectionHeading(itemQuestion4, lbQuestion4, value, StatusCode);
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
                        txtBox.Enabled = (StatusCode == 0 || StatusCode == 98) && !currentSafetySectionNotApplicable;
                        itemQuestionT.Controls.Add(txtBox);
                        ((LayoutGroup)group).Items.Add(itemQuestionT);
                        break;
                    case "CL":
                        bool canMarkLeftNotApplicable = CanMarkNotApplicable(value);
                        if (canMarkLeftNotApplicable)
                            currentSafetySectionNotApplicable = string.Equals(value.Answer, "NA", StringComparison.OrdinalIgnoreCase);
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
                        chkBox.ClientSideEvents.CheckedChanged = canMarkLeftNotApplicable
                            ? "function(s, e) { cpSafetyCheckList.PerformCallback('toggle-heading|" + value.ID + "|' + (s.GetChecked() ? '1' : '0')); }"
                            : "function(s, e) { cpSafetyCheckListSelect.PerformCallback(''); }";
                        if (!canMarkLeftNotApplicable) chkBox.Width = Unit.Percentage(95);
                        chkBox.Text = value.Question;
                        chkBox.TextAlign = TextAlign.Right;
                        chkBox.Font.Bold = true;
                        ConfigureMandatoryConfirmation(chkBox, value);
                        if (value.Answer != null && value.Answer != "") chkBox.Checked = (value.Answer == "T");
                        chkBox.Enabled = StatusCode == 0 || StatusCode == 98;
                        ASPxPanel leftHeadingLine = new ASPxPanel();
                        leftHeadingLine.CssClass = "checklist-section-heading";
                        chkBox.CssClass = canMarkLeftNotApplicable
                            ? "checklist-section-title mandatory-checklist-confirmation"
                            : "checklist-section-title";
                        leftHeadingLine.Controls.Add(chkBox);
                        if (canMarkLeftNotApplicable)
                            leftHeadingLine.Controls.Add(CreateNotApplicableCheckBox(value, StatusCode));
                        itemQuestion3L.Controls.Add(leftHeadingLine);
                        ((LayoutGroup)group).Items.Add(itemQuestion3L);
                        break;
                    case "CR":
                        bool canMarkRightNotApplicable = CanMarkNotApplicable(value);
                        if (canMarkRightNotApplicable)
                            currentSafetySectionNotApplicable = string.Equals(value.Answer, "NA", StringComparison.OrdinalIgnoreCase);
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
                        if (!canMarkRightNotApplicable) chkBoxR.Width = Unit.Percentage(95);
                        chkBoxR.Text = value.Question;
                        chkBoxR.TextAlign = TextAlign.Right;
                        chkBoxR.Font.Bold = false;
                        ConfigureMandatoryConfirmation(chkBoxR, value);
                        if (value.Answer != null && value.Answer != "") chkBoxR.Checked = (value.Answer == "T");
                        chkBoxR.Enabled = (StatusCode == 0 || StatusCode == 98) && !currentSafetySectionNotApplicable;
                        itemQuestion3R.Controls.Add(chkBoxR);
                        if (canMarkRightNotApplicable)
                            itemQuestion3R.Controls.Add(CreateNotApplicableCheckBox(value, StatusCode));
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
                        lstBox.Enabled = (StatusCode == 0 || StatusCode == 98) && !currentSafetySectionNotApplicable;

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
                        cbAnswer.ID = "Answer_" + value.ID.ToString();
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
                        cbAnswer.Text = value.Answer;
                        cbAnswer.Enabled = (StatusCode == 0 || StatusCode == 98) && !currentSafetySectionNotApplicable;
                        ((LayoutGroup)group).Items.Add(itemQuestionM);
                        break;
                }
            }
        }
        protected void cpSafetyCheckList_Callback(object sender, CallbackEventArgsBase e)
        {
            bool isNotApplicableToggle = !string.IsNullOrWhiteSpace(e.Parameter) &&
                e.Parameter.StartsWith("toggle-na|", StringComparison.OrdinalIgnoreCase);
            bool isHeadingToggle = !string.IsNullOrWhiteSpace(e.Parameter) &&
                e.Parameter.StartsWith("toggle-heading|", StringComparison.OrdinalIgnoreCase);
            if (isNotApplicableToggle || isHeadingToggle)
            {
                string[] parts = e.Parameter.Split('|');
                int id;
                safetydetaillist = Session["CHK_SafetyDetails"] as List<QuestionAndAnswerModel>;
                if (parts.Length == 3 && int.TryParse(parts[1], out id) && safetydetaillist != null)
                {
                    CaptureSafetyCheckBoxAnswers();
                    QuestionAndAnswerModel heading = safetydetaillist.FirstOrDefault(item => item.ID == id);
                    if (heading != null)
                    {
                        bool isChecked = parts[2] == "1";
                        heading.Answer = isChecked ? (isNotApplicableToggle ? "NA" : "T") : "F";
                        if (isNotApplicableToggle && isChecked)
                            ClearSafetySectionChildCheckBoxes(heading);
                    }
                    Session["CHK_SafetyDetails"] = safetydetaillist;
                    ClearSafetyList();
                    currentSafetySectionNotApplicable = false;
                    foreach (QuestionAndAnswerModel item in safetydetaillist)
                        AddEnableCheckList(item, 0);
                }
                return;
            }

            ClearSafetyList();
            if (Session["CHK_Equipment"] != null)
            {
                chkequipmentlist = (List<CHKEquipmentModel>)Session["CHK_Equipment"];
                if (chkequipmentlist.Count > 0)
                {
                    safetydetaillist = new List<QuestionAndAnswerModel>();
                    var distinctmactype = chkequipmentlist.Select(Item => Item.MachineType).Distinct().ToList();
                    foreach (var machinetype in distinctmactype)
                    {
                        List<QuestionAndAnswerModel> tempCheckList;
                        tempCheckList = TemplateViewModel.GetTemplateDetails("SAFETYCHECKLIST-" + machinetype.ToString());
                        safetydetaillist.AddRange(tempCheckList);
                    }
                    currentSafetySectionNotApplicable = false;
                    foreach (QuestionAndAnswerModel tmp in safetydetaillist)
                        AddEnableCheckList(tmp, 0);
                    Session["CHK_SafetyDetails"] = safetydetaillist;
                }
            }
        }
        protected void cpSafetyCheckListSelect_Callback(object sender, CallbackEventArgsBase e)
        {
            CaptureSafetyCheckBoxAnswers();
        }

        private void CaptureSafetyCheckBoxAnswers()
        {
            var group = flCheckListGroup.FindItemOrGroupByName("SafetyCheckListQA");
            if (group != null)
            {
                safetydetaillist = Session["CHK_SafetyDetails"] as List<QuestionAndAnswerModel>;
                if (safetydetaillist == null) return;
                foreach (var item in ((LayoutGroup)group).Items)
                {
                    if (item is LayoutItem)
                    {
                        foreach (Control control in GetControlsRecursive((LayoutItem)item))
                        {
                            ASPxCheckBox checkBox = control as ASPxCheckBox;
                            if (checkBox != null && checkBox.ID.StartsWith("Answer_", StringComparison.Ordinal))
                            {
                                int id;
                                if (!int.TryParse(checkBox.ID.Substring("Answer_".Length), out id)) continue;
                                QuestionAndAnswerModel detail = safetydetaillist.FirstOrDefault(itemDetail => itemDetail.ID == id);
                                if (detail == null) continue;
                                if (CanMarkNotApplicable(detail) &&
                                    string.Equals(detail.Answer, "NA", StringComparison.OrdinalIgnoreCase) &&
                                    !checkBox.Checked)
                                    continue;
                                detail.Answer = checkBox.Checked ? "T" : "F";
                            }
                        }
                    }
                }
                Session["CHK_SafetyDetails"] = safetydetaillist;
            }
        }

        private static IEnumerable<Control> GetControlsRecursive(LayoutItem item)
        {
            foreach (Control control in item.Controls)
            {
                yield return control;
                foreach (Control child in GetControlsRecursive(control))
                    yield return child;
            }
        }

        private static IEnumerable<Control> GetControlsRecursive(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                yield return child;
                foreach (Control descendant in GetControlsRecursive(child))
                    yield return descendant;
            }
        }

        private void ClearSafetySectionChildCheckBoxes(QuestionAndAnswerModel heading)
        {
            int headingIndex = safetydetaillist.IndexOf(heading);
            for (int index = headingIndex + 1; index < safetydetaillist.Count; index++)
            {
                QuestionAndAnswerModel item = safetydetaillist[index];
                string selectionType = ((item.Selection ?? "").Split('|')[0]).Trim();
                if (string.Equals(selectionType, "CL", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(selectionType, "B1", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(selectionType, "B2", StringComparison.OrdinalIgnoreCase)) break;
                if (string.Equals(selectionType, "CR", StringComparison.OrdinalIgnoreCase))
                    item.Answer = "";
            }
        }

        private static bool CanMarkNotApplicable(QuestionAndAnswerModel value)
        {
            string selectionType = ((value.Selection ?? "").Split('|')[0]).Trim();
            if (!string.Equals(selectionType, "CL", StringComparison.OrdinalIgnoreCase)) return false;
            string heading = (value.Question ?? "").Trim();
            if (heading.Length == 0) return false;
            return ContainsAllWords(heading, "working", "height")
                || ContainsAllWords(heading, "landing", "door", "hall")
                || ContainsAllWords(heading, "car", "operation")
                || ContainsAllWords(heading, "driving", "operation")
                || ContainsAllWords(heading, "moving", "platform")
                || ContainsAllWords(heading, "multiple", "workers")
                || ContainsAllWords(heading, "hoisting", "machines")
                || ContainsAllWords(heading, "horizontal", "pulling", "heavy", "loads")
                || ContainsAllWords(heading, "control", "panels", "live", "parts")
                || ContainsAllWords(heading, "moving", "rotating", "parts")
                || ContainsAllWords(heading, "electric", "tools")
                || ContainsAllWords(heading, "confined", "spaces")
                || ContainsAllWords(heading, "organic", "solvents");
        }

        private static bool ContainsAllWords(string text, params string[] words)
        {
            return words.All(word => text.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void AddSectionHeading(LayoutItem item, ASPxLabel label,
            QuestionAndAnswerModel value, int statusCode)
        {
            ASPxPanel headingLine = new ASPxPanel();
            headingLine.CssClass = "checklist-section-heading";
            label.CssClass = "checklist-section-title";
            headingLine.Controls.Add(label);
            if (CanMarkNotApplicable(value))
                headingLine.Controls.Add(CreateNotApplicableCheckBox(value, statusCode));
            item.Controls.Add(headingLine);
        }

        private void ConfigureMandatoryConfirmation(ASPxCheckBox checkBox, QuestionAndAnswerModel value)
        {
            if (CanMarkNotApplicable(value))
            {
                checkBox.CssClass = "mandatory-checklist-confirmation";
                ConfigureConfirmationValidation(checkBox);
                checkBox.Validation += MandatorySafetySection_Validation;
                return;
            }
            if (!IsMandatoryChecklistQuestion(value.Question)) return;
            checkBox.CssClass = "mandatory-checklist-confirmation";
            ConfigureConfirmationValidation(checkBox);
            checkBox.Validation += MandatoryChecklistConfirmation_Validation;
        }

        private static void ConfigureConfirmationValidation(ASPxCheckBox checkBox)
        {
            checkBox.ValidationSettings.Display = Display.Dynamic;
            checkBox.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.Text;
            checkBox.ValidationSettings.ErrorTextPosition = ErrorTextPosition.Right;
            checkBox.ValidationSettings.SetFocusOnError = true;
        }

        protected void MandatoryChecklistConfirmation_Validation(object sender, ValidationEventArgs e)
        {
            e.IsValid = (sender as ASPxCheckBox).Checked;
            e.ErrorText = e.IsValid ? "" : "Please check the box for confirmation.";
        }

        protected void MandatorySafetySection_Validation(object sender, ValidationEventArgs e)
        {
            ASPxCheckBox sectionCheckBox = sender as ASPxCheckBox;
            int questionId;
            bool isNotApplicable = false;
            if (sectionCheckBox != null && int.TryParse(sectionCheckBox.ID.Replace("Answer_", ""), out questionId))
            {
                ASPxCheckBox notApplicable = sectionCheckBox.Parent == null ? null :
                    sectionCheckBox.Parent.FindControl("NotApplicable_" + questionId) as ASPxCheckBox;
                isNotApplicable = notApplicable != null && notApplicable.Checked;
                if (!isNotApplicable)
                {
                    List<QuestionAndAnswerModel> details = Session["CHK_SafetyDetails"] as List<QuestionAndAnswerModel>;
                    QuestionAndAnswerModel detail = details == null ? null : details.FirstOrDefault(item => item.ID == questionId);
                    isNotApplicable = detail != null && string.Equals(detail.Answer, "NA", StringComparison.OrdinalIgnoreCase);
                }
            }
            e.IsValid = sectionCheckBox != null && (sectionCheckBox.Checked || isNotApplicable);
            e.ErrorText = e.IsValid ? "" : "Please check the box for confirmation.";
        }

        private static bool IsMandatoryChecklistQuestion(string question)
        {
            if (string.IsNullOrWhiteSpace(question)) return false;
            string[] mandatorySentences =
            {
                "less-experienced worker with under one year",
                "safety check of on-site movement routes",
                "If a worker feels unwell",
                "unplanned work"
            };
            return mandatorySentences.Any(sentence => question.IndexOf(sentence, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private ASPxCheckBox CreateNotApplicableCheckBox(QuestionAndAnswerModel value, int statusCode)
        {
            var result = new ASPxCheckBox();
            result.ID = "NotApplicable_" + value.ID.ToString();
            result.Text = "Not applicable";
            result.CssClass = "section-not-applicable";
            result.Checked = string.Equals(value.Answer, "NA", StringComparison.OrdinalIgnoreCase);
            result.Enabled = statusCode == 0 || statusCode == 98;
            result.ClientSideEvents.CheckedChanged = "function(s, e) { cpSafetyCheckList.PerformCallback('toggle-na|" + value.ID + "|' + (s.GetChecked() ? '1' : '0')); }";
            return result;
        }

        private bool ValidateCommonComplianceRequirements()
        {
            if (safetydetaillist == null) return true;
            var mandatory = safetydetaillist.Where(item => IsMandatoryChecklistQuestion(item.Question)).ToList();
            if (mandatory.Count > 0)
                return mandatory.All(item => string.Equals(item.Answer, "T", StringComparison.OrdinalIgnoreCase));
            bool inCommonSection = false;
            int checkboxCount = 0;
            foreach (QuestionAndAnswerModel item in safetydetaillist.OrderBy(value => value.Sort))
            {
                string type = (item.Selection ?? "").Split('|')[0];
                if (type == "B1" || type == "B2")
                {
                    if (inCommonSection && checkboxCount > 0) break;
                    inCommonSection = (item.Question ?? "").IndexOf("Common Compliance Requirements", StringComparison.OrdinalIgnoreCase) >= 0;
                    continue;
                }
                if (!inCommonSection || (type != "CL" && type != "CR")) continue;
                checkboxCount++;
                if (!string.Equals(item.Answer, "T", StringComparison.OrdinalIgnoreCase)) return false;
                if (checkboxCount == 4) return true;
            }
            return checkboxCount == 0 || checkboxCount == 4;
        }

        private bool ValidateMandatorySafetySections()
        {
            if (safetydetaillist == null) return true;
            return safetydetaillist.Where(CanMarkNotApplicable).All(item =>
                string.Equals(item.Answer, "T", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(item.Answer, "NA", StringComparison.OrdinalIgnoreCase));
        }

        protected void cbDeLine_Validation(object sender, ValidationEventArgs e)
        {
            e.IsValid = (sender as ASPxCheckBox).Checked;
            e.ErrorText = e.IsValid ? "" : "Please check the box for confirmation.";
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

    }
}
