using DevExpress.Web;
using HEA.ePTW.Models;
using HEA.ePTW.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HEA.ePTW.PTW
{
    public partial class NewPTW : System.Web.UI.Page
    {
        string strKey = Guid.NewGuid().ToString();
        List<AttachmentModel> attachlist;
        //List<AttachmentModel> attachdoclist;
        List<TemplateModel> templatelist;
        List<QuestionAndAnswerModel> templatedetaillist;
        List<PTWEquipmentModel> ptwequipmentlist;
        List<EquipmentModel> equipmentlist;
        List<PTWStaffModel> ptwstafflist;
        PTWModel master;

        private void RetrieveFromQueryString()
        {
            string strTask = (Request.QueryString["Status"] == null) ? "" : Request.QueryString["Status"].ToString();
            if (strTask == "N")
            {
                Session["PTW_Record_IMG"] = null;
                //Session["PTW_Record_DOC"] = null;
                Session["PTW_EQUIPMENTLIST"] = null;
                Session["PTW_Record"] = null;
                Session["PTW_TemplateDetails"] = null;
                Session["PTW_Equipment"] = null;
                Session["PTW_Staff"] = null;
            }
            Session["PTW_Task"] = strTask;
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
                    if (Session["PTW_Record"] != null)
                    {
                        master = (PTWModel)Session["PTW_Record"];
                        attachlist = AttachmentViewModel.GetAttachmentList(master.Key).ToList();
                        
                        //attachlist = AttachmentViewModel.GetAttachmentList(master.Key).Where(item => item.FileType == "IMG").ToList();
                        //attachdoclist = AttachmentViewModel.GetAttachmentList(master.Key).Where(item => item.FileType == "PDF").ToList();
                        templatedetaillist = PTWDetailViewModel.GetPTWeDetails(master.Key);
                        //ptwequipmentlist = PTWEquipmentViewModel.GetPTWEquipmentList(master.Key);
                        //ptwstafflist = PTWStaffViewModel.GetPTWStaffList(master.Key);
                    }
                    else
                    {
                        master = new PTWModel();
                        master.Key = strKey;
                        master.ProjectName = project != null ? project.ToString() : "";
                        master.TotalWorker = 1;
                        //master.RequestBy = user.UserID;
                        //master.RequestCompany = user.ConstructorName;
                        //master.RequestDate = DateTime.Now;
                        //master.RequestPosition = user.Position;
                        //master.RequestName = user.FullName;
                        master.DateFrom = DateTime.Now;
                        master.DateTo = DateTime.Now;
                        Session["PTW_Record"] = master;
                        attachlist = new List<AttachmentModel>();
                        //attachdoclist = new List<AttachmentModel>();
                        //ptwequipmentlist = new List<PTWEquipmentModel>();
                        //ptwstafflist = new List<PTWStaffModel>();
                    }

                    equipmentlist = EquipmentViewModel.GetEquipment_ByProject_CodeTable(project.ToString());
                    templatelist = TemplateViewModel.GetTemplateList("PTW");

                    Session["PTW_Template"] = templatelist;
                    Session["PTW_Record_IMG"] = attachlist;
                    //Session["PTW_Record_DOC"] = attachdoclist;
                    Session["PTW_EQUIPMENTLIST"] = equipmentlist;
                    Session["PTW_TemplateDetails"] = templatedetaillist;
                    Session["PTW_Equipment"] = ptwequipmentlist;
                    Session["PTW_Staff"] = ptwstafflist;
                    BindMaster();
                }
                else
                {
                    attachlist = (List<AttachmentModel>)Session["PTW_Record_IMG"];
                    //attachdoclist = (List<AttachmentModel>)Session["PTW_Record_DOC"];
                    equipmentlist = (List<EquipmentModel>)Session["PTW_EQUIPMENTLIST"];
                    templatelist = (List<TemplateModel>)Session["PTW_Template"];
                    templatedetaillist = (List<QuestionAndAnswerModel>)Session["PTW_TemplateDetails"];
                    //ptwequipmentlist = (List<PTWEquipmentModel>)Session["PTW_Equipment"];
                    //ptwstafflist = (List<PTWStaffModel>)Session["PTW_Staff"];
                    master = (PTWModel)Session["PTW_Record"];
                }


                //Show Title & Location
                string strLatitude = hfLatitude.Value;
                string strLongitude = hfLongitude.Value;
                string strLocation = "";
                switch (master.Status)
                {
                    case 0:
                        lblTitle.Text = "Permit To Work - New";
                        if (strLatitude != "") strLocation = $"Location : Latitude {strLatitude}, Longitude {strLongitude}";
                        break;
                    case 1:
                        lblTitle.Text = "Permit To Work - Submitted";
                        if (master.Latitude != "") strLocation = $"Location : Latitude {master.Latitude}, Longitude {master.Longitude}";
                        break;
                    case 2:
                        lblTitle.Text = "Permit To Work - Assessed";
                        if (master.Latitude != "") strLocation = $"Location : Latitude {master.Latitude}, Longitude {master.Longitude}";
                        break;
                    case 3:
                        lblTitle.Text = "Permit To Work - Safety Verified";
                        if (master.Latitude != "") strLocation = $"Location : Latitude {master.Latitude}, Longitude {master.Longitude}";
                        break;
                    case 4:
                        lblTitle.Text = "Permit To Work - Approved";
                        if (master.Latitude != "") strLocation = $"Location : Latitude {master.Latitude}, Longitude {master.Longitude}";
                        break;
                    case 5:
                        lblTitle.Text = "Permit To Work - Closed";
                        if (master.Latitude != "") strLocation = $"Location : Latitude {master.Latitude}, Longitude {master.Longitude}";
                        break;
                    case 6:
                        lblTitle.Text = "Permit To Work - Completed";
                        if (master.Latitude != "") strLocation = $"Location : Latitude {master.Latitude}, Longitude {master.Longitude}";
                        break;
                    case 98:
                        lblTitle.Text = "Permit To Work - Returned";
                        if (strLatitude != "") strLocation = $"Location : Latitude {strLatitude}, Longitude {strLongitude}";
                        break;
                    case 99:
                        lblTitle.Text = "Permit To Work - Rejected";
                        if (master.Latitude != "") strLocation = $"Location : Latitude {master.Latitude}, Longitude {master.Longitude}";
                        break;
                    case 100:
                        lblTitle.Text = "Permit To Work - Revoked";
                        if (master.Latitude != "") strLocation = $"Location : Latitude {master.Latitude}, Longitude {master.Longitude}";
                        break;
                }
                lblLocation.Text = strLocation;

                if (templatedetaillist != null)
                {
                    List<UserRoleModel> roles = UserRoleViewModel.GetUserRoleList(user.UserID);
                    ClearQuestionAndAnswer();
                    foreach (QuestionAndAnswerModel ent in templatedetaillist)
                        AddEnableQuestionAnswer(ent, master.Status, roles);
                }
                SetVisibleControls(master);

                cbType.DataSource = templatelist;
                cbType.DataBind();
                cvAttachmentDocument.DataSource = attachlist;
                cvAttachmentDocument.DataBind();
                DisplayImageItems.DataSource = attachlist.Where(item => item.FileType == "IMG");
                DisplayImageItems.DataBind();
                cbMFG.DataSource = equipmentlist;
                cbMFG.DataBind();
                //cvImages.DataSource = attachlist;
                //cvImages.DataBind();
                //cvDocument.DataSource = attachdoclist;
                //cvDocument.DataBind();
                //gvEquipment.DataSource = ptwequipmentlist;
                //gvEquipment.DataBind();
                //gvStaff.DataSource = ptwstafflist;
                //gvStaff.DataBind();
            }
            catch
            {
                Response.Redirect("~/Account/SignIn.aspx");
            }
        }



        private void SetVisibleControls(PTWModel data)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            btnSubmit.Visible = false;
            btnReject.Visible = false;
            btnReturn.Visible = false;
            btnRevoke.Visible = false;
            //btnDelete.Visible = false;
            //btnCancel.Visible = false;
            //////////btnAssessedApprove.Visible = false;
            //////////btnAssessedReject.Visible = false;
            //////////btnAssessedReturn.Visible = false;
            //////////btnAssessedCancel.Visible = false;
            ////////btnApprovalApprove.Visible = false;
            ////////btnApprovalReject.Visible = false;
            ////////btnApprovalReturn.Visible = false;
            ////////btnApprovalCancel.Visible = false;
            //////////btnClosedClose.Visible = false;
            //////////btnClosedCancel.Visible = false;

            //var layoutgroup = flPermitToWork.FindItemOrGroupByName("UploadFiles");
            var uploadfilegroup = flPermitToWork.FindItemOrGroupByName("AttachmentFileControl");
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
            var returnrejectgrp = flPermitToWork.FindItemOrGroupByName("ReturnRejectGroup");
            var submitgrp = flPermitToWork.FindItemOrGroupByName("SubmitInfo");
            var assessedgrp = flPermitToWork.FindItemOrGroupByName("AssessedInfo");
            var safetygrp = flPermitToWork.FindItemOrGroupByName("VerifiedInfo");
            var approvalgrp = flPermitToWork.FindItemOrGroupByName("ApprovalInfo");
            var closedgrp = flPermitToWork.FindItemOrGroupByName("ClosedInfo");
            var closuregrp = flPermitToWork.FindItemOrGroupByName("ClosureAcceptedInfo");
            var dailygrp = flPermitToWork.FindItemOrGroupByName("DailyInfo");

            if (returnrejectgrp != null) ((LayoutGroup)returnrejectgrp).Visible = false;
            if (assessedgrp != null) ((LayoutGroup)assessedgrp).Visible = false;
            if (safetygrp != null) ((LayoutGroup)safetygrp).Visible = false;
            if (approvalgrp != null) ((LayoutGroup)approvalgrp).Visible = false;
            if (closedgrp != null) ((LayoutGroup)closedgrp).Visible = false;
            if (closuregrp != null) ((LayoutGroup)closuregrp).Visible = false;
            if (dailygrp != null) ((LayoutGroup)dailygrp).Visible = false;

            if (data.Status == 0 || data.Status == 98)
            {
                dtFrom.Enabled = true;
                dtTo.Enabled = true;
                txtJobDescription.Enabled = true;
                cbMFG.Enabled = true;
                seTotalWorker.Enabled = true;
                cbType.Enabled = true;
                //EnableQNA(true);
                //cvImages.SettingsDataSecurity.AllowDelete = true;
                //cvImages.SettingsDataSecurity.AllowInsert = true;
                //cvDocument.SettingsDataSecurity.AllowDelete = true;
                //cvDocument.SettingsDataSecurity.AllowInsert = true;
                //gvEquipment.SettingsDataSecurity.AllowDelete = true;
                //gvEquipment.SettingsDataSecurity.AllowInsert = true;
                //gvStaff.SettingsDataSecurity.AllowDelete = true;
                //gvStaff.SettingsDataSecurity.AllowInsert = true;
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
                if (master.RequestBy == "") btnSubmit.Visible = true;
                else
                {
                    if (user.UserID == master.RequestBy) btnSubmit.Visible = true;
                    else btnSubmit.Visible = false;
                }
                //if (data.Status == 98 && user.UserID == master.RequestBy)
                //{
                //    btnDelete.Visible = true;
                //}
                //else btnDelete.Visible = false;
                if (master.Status == 98 || master.Status == 99)
                {
                    txtReason.Text = master.ReturnRejectReason;
                    if (returnrejectgrp != null) ((LayoutGroup)returnrejectgrp).Visible = true;
                    txtReason.Enabled = false;
                }  
                //btnCancel.Visible = true;
                //lblSubmitStatus.Text = "";
                if (master.RequestBy == "")
                {
                    lblSubmitName.Text = user.FullName;
                    lblSubmitDesignation.Text = user.Position;
                    lblSubmitCompany.Text = user.ConstructorName;
                }
                else
                {
                    lblSubmitName.Text = master.RequestName;
                    lblSubmitDesignation.Text = master.RequestPosition;
                    lblSubmitCompany.Text = master.RequestCompany;
                    //txtSubmitRemarks.Text = master.RequestRemarks;
                }
                txtSubmitRemarks.Enabled = true;
            }
            else
            {
                dtFrom.Enabled = false;
                dtTo.Enabled = false;
                txtJobDescription.Enabled = false;
                cbMFG.Enabled = false;
                seTotalWorker.Enabled = false;
                cbType.Enabled = false;
                //EnableQNA(false);
                //cvImages.SettingsDataSecurity.AllowDelete = false;
                //cvImages.SettingsDataSecurity.AllowInsert = false;
                //cvDocument.SettingsDataSecurity.AllowDelete = false;
                //cvDocument.SettingsDataSecurity.AllowInsert = false;
                //gvEquipment.SettingsDataSecurity.AllowDelete = false;
                //gvEquipment.SettingsDataSecurity.AllowInsert = false;
                //gvStaff.SettingsDataSecurity.AllowDelete = false;
                //gvStaff.SettingsDataSecurity.AllowInsert = false;
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
                cvAttachmentDocument.SettingsDataSecurity.AllowDelete = false;
                if (uploadfilegroup != null)
                {
                    var EditDocument = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("EditAttachmentDoc");
                    if (EditDocument != null) (EditDocument as LayoutItem).Visible = true;
                    var DisplayImage = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("DisplayImage");
                    if (DisplayImage != null) (DisplayImage as LayoutItem).Visible = true;
                }

                List<UserRoleModel> roles = UserRoleViewModel.GetUserRoleList(user.UserID);

                var assessRole = roles.FirstOrDefault(item => item.RoleID == "PTW ASSESSOR");
                bool isAdmin = UserViewModel.IsAdmin(user.UserID);

                if ((assessRole != null || isAdmin) && master.Status == 1)
                {
                    if (assessedgrp != null) ((LayoutGroup)assessedgrp).Visible = true;
                    lblAssessedName.Text = user.FullName;
                    lblAssessedDesignation.Text = user.Position;
                    lblAssessedCompany.Text = user.ConstructorName;
                    txtAssessedRemarks.Enabled = true;
                    ////////////btnAssessedApprove.Visible = true;
                    ////////////btnAssessedReject.Visible = true;
                    ////////////btnAssessedReturn.Visible = true;
                    ////////////btnAssessedCancel.Visible = true;
                    btnReject.Visible = true;
                    btnReturn.Visible = true;
                    if (returnrejectgrp != null) ((LayoutGroup)returnrejectgrp).Visible = true;
                    txtReason.Enabled = true;
                }

                var safetyRole = roles.FirstOrDefault(item => item.RoleID == "PTW SAFETY");

                if ((safetyRole != null || isAdmin) && master.Status == 2)
                {
                    if (safetygrp != null) ((LayoutGroup)safetygrp).Visible = true;
                    lblVerifiedName.Text = user.FullName;
                    lblVerifiedDesignation.Text = user.Position;
                    lblVerifiedCompany.Text = user.ConstructorName;
                    txtVerifiedRemarks.Enabled = true;
                    btnReject.Visible = true;
                    btnReturn.Visible = true;
                    if (returnrejectgrp != null) ((LayoutGroup)returnrejectgrp).Visible = true;
                    txtReason.Enabled = true;
                }

                var approvalRole = roles.FirstOrDefault(item => item.RoleID == "PTW APPROVER");

                if ((approvalRole != null || isAdmin) && master.Status == 3)
                {
                    if (approvalgrp != null) ((LayoutGroup)approvalgrp).Visible = true;

                    lblApprovalName.Text = user.FullName;
                    lblApprovalDesignation.Text = user.Position;
                    lblApprovalCompany.Text = user.ConstructorName;
                    txtApprovalReamrks.Enabled = true;
                    ////////btnApprovalApprove.Visible = true;
                    ////////btnApprovalReject.Visible = true;
                    ////////btnApprovalReturn.Visible = true;
                    ////////btnApprovalCancel.Visible = true;
                    if (returnrejectgrp != null) ((LayoutGroup)returnrejectgrp).Visible = true;
                    txtReason.Enabled = true;
                    btnReject.Visible = true;
                    btnReturn.Visible = true;
                }

                var DailyRole = roles.FirstOrDefault(item => item.RoleID == "PTW ASSESSOR");
                if ((DailyRole != null || isAdmin || user.UserID == master.RequestBy) && master.Status == 4)
                {
                    if (dailygrp != null)
                    {
                        ((LayoutGroup)dailygrp).Visible = true;
                        var day1 = ((LayoutGroup)dailygrp).FindItemOrGroupByName("Day1");
                        var day2 = ((LayoutGroup)dailygrp).FindItemOrGroupByName("Day2");
                        var day3 = ((LayoutGroup)dailygrp).FindItemOrGroupByName("Day3");
                        var day4 = ((LayoutGroup)dailygrp).FindItemOrGroupByName("Day4");
                        var day5 = ((LayoutGroup)dailygrp).FindItemOrGroupByName("Day5");
                        var day6 = ((LayoutGroup)dailygrp).FindItemOrGroupByName("Day6");
                        var day7 = ((LayoutGroup)dailygrp).FindItemOrGroupByName("Day7");

                        if (master.Day1 != null)
                        {
                            //txtDay1Applicant.Text = master.Day1RemarkByApplicant;
                            //txtDay1Assesser.Text = master.Day1RemarkByAssesser;
                            lblday1.Text = "Day 1 (" + Convert.ToDateTime(master.Day1).ToString("dd/MM/yy") + ")";
                            if (master.Day1VerifiedByApplicant != null) Day1Applicant.Checked = true;
                            if (master.Day1VerifiedByAssesser != null) Day1Assessor.Checked = true;
                            btnDay1Applicant.Enabled = false;
                            txtDay1Applicant.Enabled = false;
                            btnDay1Assesser.Enabled = false;
                            txtDay1Assesser.Enabled = false;

                            if (user.UserID == master.RequestBy)
                            {
                                if (master.Day1VerifiedByApplicant == null && (Convert.ToDateTime(master.Day1).Date == DateTime.Now.Date || Convert.ToDateTime(master.Day1).Date.AddDays(1) == DateTime.Now.Date))
                                {
                                    btnDay1Applicant.Enabled = true;
                                    txtDay1Applicant.Enabled = true;
                                }
                            }
                            if (DailyRole != null || isAdmin)
                            {
                                if (master.Day1VerifiedByAssesser == null && (Convert.ToDateTime(master.Day1).Date == DateTime.Now.Date || Convert.ToDateTime(master.Day1).Date.AddDays(1) == DateTime.Now.Date))
                                {
                                    btnDay1Assesser.Enabled = true;
                                    txtDay1Assesser.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            if (day1 != null) ((LayoutGroup)day1).Visible = false;
                        }
                        if (master.Day2 != null)
                        {
                            //txtDay2Applicant.Text = master.Day2RemarkByApplicant;
                            //txtDay2Assesser.Text = master.Day2RemarkByAssesser;
                            lblday2.Text = "Day 2 (" + Convert.ToDateTime(master.Day2).ToString("dd/MM/yy") + ")";
                            if (master.Day2VerifiedByApplicant != null) Day2Applicant.Checked = true;
                            if (master.Day2VerifiedByAssesser != null) Day2Assessor.Checked = true;
                            btnDay2Applicant.Enabled = false;
                            txtDay2Applicant.Enabled = false;
                            btnDay2Assesser.Enabled = false;
                            txtDay2Assesser.Enabled = false;

                            if (user.UserID == master.RequestBy)
                            {
                                if (master.Day2VerifiedByApplicant == null && (Convert.ToDateTime(master.Day2).Date == DateTime.Now.Date || Convert.ToDateTime(master.Day2).Date.AddDays(1) == DateTime.Now.Date))
                                {
                                    btnDay2Applicant.Enabled = true;
                                    txtDay2Applicant.Enabled = true;
                                }
                            }
                            if (DailyRole != null || isAdmin)
                            {
                                if (master.Day2VerifiedByAssesser == null && (Convert.ToDateTime(master.Day2).Date == DateTime.Now.Date || Convert.ToDateTime(master.Day2).Date.AddDays(1) == DateTime.Now.Date))
                                {
                                    btnDay2Assesser.Enabled = true;
                                    txtDay2Assesser.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            if (day2 != null) ((LayoutGroup)day2).Visible = false;
                        }
                        if (master.Day3 != null)
                        {
                            //txtDay3Applicant.Text = master.Day3RemarkByApplicant;
                            //txtDay3Assesser.Text = master.Day3RemarkByAssesser;
                            lblday3.Text = "Day 3 (" + Convert.ToDateTime(master.Day3).ToString("dd/MM/yy") + ")";
                            if (master.Day3VerifiedByApplicant != null) Day3Applicant.Checked = true;
                            if (master.Day3VerifiedByAssesser != null) Day3Assessor.Checked = true;
                            btnDay3Applicant.Enabled = false;
                            txtDay3Applicant.Enabled = false;
                            btnDay3Assesser.Enabled = false;
                            txtDay3Assesser.Enabled = false;

                            if (user.UserID == master.RequestBy)
                            {
                                if (master.Day3VerifiedByApplicant == null && (Convert.ToDateTime(master.Day3).Date == DateTime.Now.Date || Convert.ToDateTime(master.Day3).Date.AddDays(1) == DateTime.Now.Date))
                                {
                                    btnDay3Applicant.Enabled = true;
                                    txtDay3Applicant.Enabled = true;
                                }
                            }
                            if (DailyRole != null || isAdmin)
                            {
                                if (master.Day3VerifiedByAssesser == null && (Convert.ToDateTime(master.Day3).Date == DateTime.Now.Date || Convert.ToDateTime(master.Day3).Date.AddDays(1) == DateTime.Now.Date))
                                {
                                    btnDay3Assesser.Enabled = true;
                                    txtDay3Assesser.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            if (day3 != null) ((LayoutGroup)day3).Visible = false;
                        }
                        if (master.Day4 != null)
                        {
                            //txtDay4Applicant.Text = master.Day4RemarkByApplicant;
                            //txtDay4Assesser.Text = master.Day4RemarkByAssesser;
                            lblday4.Text = "Day 4 (" + Convert.ToDateTime(master.Day4).ToString("dd/MM/yy") + ")";
                            if (master.Day4VerifiedByApplicant != null) Day4Applicant.Checked = true;
                            if (master.Day4VerifiedByAssesser != null) Day4Assessor.Checked = true;
                            btnDay4Applicant.Enabled = false;
                            txtDay4Applicant.Enabled = false;
                            btnDay4Assesser.Enabled = false;
                            txtDay4Assesser.Enabled = false;

                            if (user.UserID == master.RequestBy)
                            {
                                if (master.Day4VerifiedByApplicant == null && (Convert.ToDateTime(master.Day4).Date == DateTime.Now.Date || Convert.ToDateTime(master.Day4).Date.AddDays(1) == DateTime.Now.Date))
                                {
                                    btnDay4Applicant.Enabled = true;
                                    txtDay4Applicant.Enabled = true;
                                }
                            }
                            if (DailyRole != null || isAdmin)
                            {
                                if (master.Day4VerifiedByAssesser == null && (Convert.ToDateTime(master.Day4).Date == DateTime.Now.Date || Convert.ToDateTime(master.Day4).Date.AddDays(1) == DateTime.Now.Date))
                                {
                                    btnDay4Assesser.Enabled = true;
                                    txtDay4Assesser.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            if (day4 != null) ((LayoutGroup)day4).Visible = false;
                        }
                        if (master.Day5 != null)
                        {
                            //txtDay5Applicant.Text = master.Day5RemarkByApplicant;
                            //txtDay5Assesser.Text = master.Day5RemarkByAssesser;
                            lblday5.Text = "Day 5 (" + Convert.ToDateTime(master.Day5).ToString("dd/MM/yy") + ")";
                            if (master.Day5VerifiedByApplicant != null) Day5Applicant.Checked = true;
                            if (master.Day5VerifiedByAssesser != null) Day5Assessor.Checked = true;
                            btnDay5Applicant.Enabled = false;
                            txtDay5Applicant.Enabled = false;
                            btnDay5Assesser.Enabled = false;
                            txtDay5Assesser.Enabled = false;

                            if (user.UserID == master.RequestBy)
                            {
                                if (master.Day5VerifiedByApplicant == null && (Convert.ToDateTime(master.Day5).Date == DateTime.Now.Date || Convert.ToDateTime(master.Day5).Date.AddDays(1) == DateTime.Now.Date))
                                {
                                    btnDay5Applicant.Enabled = true;
                                    txtDay5Applicant.Enabled = true;
                                }
                            }
                            if (DailyRole != null || isAdmin)
                            {
                                if (master.Day5VerifiedByAssesser == null && (Convert.ToDateTime(master.Day5).Date == DateTime.Now.Date || Convert.ToDateTime(master.Day5).Date.AddDays(1) == DateTime.Now.Date))
                                {
                                    btnDay5Assesser.Enabled = true;
                                    txtDay5Assesser.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            if (day5 != null) ((LayoutGroup)day5).Visible = false;
                        }
                        if (master.Day6 != null)
                        {
                            //txtDay6Applicant.Text = master.Day6RemarkByApplicant;
                            //txtDay6Assesser.Text = master.Day6RemarkByAssesser;
                            lblday6.Text = "Day 6 (" + Convert.ToDateTime(master.Day6).ToString("dd/MM/yy") + ")";
                            if (master.Day6VerifiedByApplicant != null) Day6Applicant.Checked = true;
                            if (master.Day6VerifiedByAssesser != null) Day6Assessor.Checked = true;
                            btnDay6Applicant.Enabled = false;
                            txtDay6Applicant.Enabled = false;
                            btnDay6Assesser.Enabled = false;
                            txtDay6Assesser.Enabled = false;

                            if (user.UserID == master.RequestBy)
                            {
                                if (master.Day6VerifiedByApplicant == null && (Convert.ToDateTime(master.Day6).Date == DateTime.Now.Date || Convert.ToDateTime(master.Day6).Date.AddDays(1) == DateTime.Now.Date))
                                {
                                    btnDay6Applicant.Enabled = true;
                                    txtDay6Applicant.Enabled = true;
                                }
                            }
                            if (DailyRole != null || isAdmin)
                            {
                                if (master.Day6VerifiedByAssesser == null && (Convert.ToDateTime(master.Day6).Date == DateTime.Now.Date || Convert.ToDateTime(master.Day6).Date.AddDays(1) == DateTime.Now.Date))
                                {
                                    btnDay6Assesser.Enabled = true;
                                    txtDay6Assesser.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            if (day6 != null) ((LayoutGroup)day6).Visible = false;
                        }
                        if (master.Day7 != null)
                        {
                            //txtDay7Applicant.Text = master.Day7RemarkByApplicant;
                            //txtDay7Assesser.Text = master.Day7RemarkByAssesser;
                            lblday7.Text = "Day 7 (" + Convert.ToDateTime(master.Day7).ToString("dd/MM/yy") + ")";
                            if (master.Day7VerifiedByApplicant != null) Day7Applicant.Checked = true;
                            if (master.Day7VerifiedByAssesser != null) Day7Assessor.Checked = true;
                            btnDay7Applicant.Enabled = false;
                            txtDay7Applicant.Enabled = false;
                            btnDay7Assesser.Enabled = false;
                            txtDay7Assesser.Enabled = false;

                            if (user.UserID == master.RequestBy)
                            {
                                if (master.Day7VerifiedByApplicant == null && (Convert.ToDateTime(master.Day7).Date == DateTime.Now.Date || Convert.ToDateTime(master.Day7).Date.AddDays(1) == DateTime.Now.Date))
                                {
                                    btnDay7Applicant.Enabled = true;
                                    txtDay7Applicant.Enabled = true;
                                }
                            }
                            if (DailyRole != null || isAdmin)
                            {
                                if (master.Day7VerifiedByAssesser == null && (Convert.ToDateTime(master.Day7).Date == DateTime.Now.Date || Convert.ToDateTime(master.Day7).Date.AddDays(1) == DateTime.Now.Date))
                                {
                                    btnDay7Assesser.Enabled = true;
                                    txtDay7Assesser.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            if (day7 != null) ((LayoutGroup)day7).Visible = false;
                        }
                    }
                }

                var RevokeRole = roles.FirstOrDefault(item => item.RoleID == "PTW ASSESSOR" || item.RoleID == "PTW SAFETY" || item.RoleID == "PTW APPROVER");
                if ((RevokeRole != null || isAdmin) && master.Status == 4)
                {
                    if (returnrejectgrp != null) ((LayoutGroup)returnrejectgrp).Visible = true;
                    txtReason.Enabled = true;
                    btnRevoke.Visible = true;
                }

                if (master.Status == 4 && user.UserID == master.RequestBy)
                {
                    if (closedgrp != null) ((LayoutGroup)closedgrp).Visible = true;
                    lblClosedName.Text = user.FullName;
                    lblClosedDesignation.Text = user.Position;
                    lblClosedCompany.Text = user.ConstructorName;
                    txtClosedRemarks.Enabled = true;
                    ////////btnClosedClose.Visible = true;
                    ////////btnClosedCancel.Visible = true;
                }

                var closureRole = roles.FirstOrDefault(item => item.RoleID == "PTW CLOSURE");

                if ((closureRole != null || isAdmin) && master.Status == 5)
                {
                    if (closuregrp != null) ((LayoutGroup)closuregrp).Visible = true;
                    lblAcceptedName.Text = user.FullName;
                    lblAcceptedDesignation.Text = user.Position;
                    lblAcceptedCompany.Text = user.ConstructorName;
                    txtClosedRemarks.Enabled = true;
                    btnAcceptedVerify.Visible = true;
                    btnAcceptedCancel.Visible = true;
                }
            }

            if ((master.Status > 0 && master.Status < 20) || master.Status == 99 || master.Status == 97 || master.Status == 100)
            {
                if (master.RequestName != "")
                {
                    if (submitgrp != null) ((LayoutGroup)submitgrp).Visible = true;
                    lblSubmitStatus.Text = "PTW Applicant";
                    lblSubmitName.Text = master.RequestName;
                    lblSubmitDesignation.Text = master.RequestPosition;
                    lblSubmitCompany.Text = master.RequestCompany;
                    txtSubmitRemarks.Text = master.RequestRemarks;
                    txtSubmitRemarks.Enabled = false;
                    //if (master.Status == 1 && master.RequestBy == user.UserID) btnCancel.Visible = true;
                    if (master.Status == 97)
                    {
                        //btnCancel.Visible = true;
                        lblSubmitStatus.Text = "Deleted on " + Convert.ToDateTime(master.Updated).ToString("dd MMM yyyy hh:mm");
                        txtReason.Text = master.ReturnRejectReason + " - " + master.ReturnRejectBy;
                        if (returnrejectgrp != null) ((LayoutGroup)returnrejectgrp).Visible = true;
                        txtReason.Enabled = false;
                    }
                }
                if (master.AssessBy != "")
                {
                    if (assessedgrp != null) ((LayoutGroup)assessedgrp).Visible = true;
                    lblAssessedStatus.Text = "Part 2: Endorsement by WAH Assessor";
                    lblAssessedName.Text = master.AssessName;
                    lblAssessedDesignation.Text = master.AssessPosition;
                    lblAssessedCompany.Text = master.AssessCompany;
                    txtAssessedRemarks.Text = master.AssessRemarks;
                    txtAssessedRemarks.Enabled = false;
                    btnAssessedApprove.Visible = false;
                    //////////btnAssessedReject.Visible = false;
                    //////////btnAssessedReturn.Visible = false;
                    //////////if (master.Status == 2 && master.RequestBy == user.UserID) btnAssessedCancel.Visible = true;
                }
                if (master.SafetyOfficerBy != "")
                {
                    if (safetygrp != null) ((LayoutGroup)safetygrp).Visible = true;
                    lblVerifiedStatus.Text = "Part 3: HEA Safety";
                    lblVerifiedName.Text = master.SafetyOfficerName;
                    lblVerifiedCompany.Text = master.SafetyOfficerCompany;
                    lblVerifiedDesignation.Text = master.SafetyOfficerPosition;
                    txtVerifiedRemarks.Text = master.SafetyOfficerRemarks;
                    txtVerifiedRemarks.Enabled = false;
                    btnVerifyApprove.Visible = false;
                }

                if (master.ApproveBy != "")
                {
                    if (approvalgrp != null) ((LayoutGroup)approvalgrp).Visible = true;
                    lblApprovalStatus.Text = "Part 4: Approval by HEA Project Manager / Authorized Competent Person";
                    lblApprovalNote.Text = "Permit To Work is :";
                    if (master.Status == 4)
                        lblApprovalNote.Text = "Permit To Work is : Approved";
                    else
                        lblApprovalNote.Text = "Permit To Work is : Not Approved";
                    lblApprovalName.Text = master.ApproveName;
                    lblApprovalDesignation.Text = master.ApprovePosition;
                    lblApprovalCompany.Text = master.ApproveCompany;
                    txtApprovalReamrks.Text = master.ApproveRemarks;
                    txtApprovalReamrks.Enabled = false;
                    btnApprovalApprove.Visible = false;
                    //////////btnApprovalReject.Visible = false;
                    //////////btnApprovalReturn.Visible = false;
                    //////////if (master.Status == 3 && master.RequestBy == user.UserID) btnApprovalCancel.Visible = true;
                }

                if (master.Status > 4 && master.Status != 98 && master.Status != 99)
                {
                    var day1 = ((LayoutGroup)dailygrp).FindItemOrGroupByName("Day1");
                    var day2 = ((LayoutGroup)dailygrp).FindItemOrGroupByName("Day2");
                    var day3 = ((LayoutGroup)dailygrp).FindItemOrGroupByName("Day3");
                    var day4 = ((LayoutGroup)dailygrp).FindItemOrGroupByName("Day4");
                    var day5 = ((LayoutGroup)dailygrp).FindItemOrGroupByName("Day5");
                    var day6 = ((LayoutGroup)dailygrp).FindItemOrGroupByName("Day6");
                    var day7 = ((LayoutGroup)dailygrp).FindItemOrGroupByName("Day7");
                    ((LayoutGroup)day1).Visible = false;
                    ((LayoutGroup)day2).Visible = false;
                    ((LayoutGroup)day3).Visible = false;
                    ((LayoutGroup)day4).Visible = false;
                    ((LayoutGroup)day5).Visible = false;
                    ((LayoutGroup)day6).Visible = false;
                    ((LayoutGroup)day7).Visible = false;
                    if (dailygrp != null) ((LayoutGroup)dailygrp).Visible = true;
                    if (master.Day1 != null)
                    {
                        lblday1.Text = "Day 1 (" + Convert.ToDateTime(master.Day1).ToString("dd/MM/yy") + ")";
                        if (master.Day1VerifiedByApplicant != null) Day1Applicant.Checked = true;
                        if (master.Day1VerifiedByAssesser != null) Day1Assessor.Checked = true;
                        btnDay1Applicant.Enabled = false;
                        txtDay1Applicant.Enabled = false;
                        btnDay1Assesser.Enabled = false;
                        txtDay1Assesser.Enabled = false;
                        ((LayoutGroup)day1).Visible = true;
                    }
                    if (master.Day2 != null)
                    {
                        lblday2.Text = "Day 2 (" + Convert.ToDateTime(master.Day2).ToString("dd/MM/yy") + ")";
                        if (master.Day2VerifiedByApplicant != null) Day2Applicant.Checked = true;
                        if (master.Day2VerifiedByAssesser != null) Day2Assessor.Checked = true;
                        btnDay2Applicant.Enabled = false;
                        txtDay2Applicant.Enabled = false;
                        btnDay2Assesser.Enabled = false;
                        txtDay2Assesser.Enabled = false;
                        ((LayoutGroup)day2).Visible = true;
                    }
                    if (master.Day3 != null)
                    {
                        lblday3.Text = "Day 3 (" + Convert.ToDateTime(master.Day3).ToString("dd/MM/yy") + ")";
                        if (master.Day3VerifiedByApplicant != null) Day3Applicant.Checked = true;
                        if (master.Day3VerifiedByAssesser != null) Day3Assessor.Checked = true;
                        btnDay3Applicant.Enabled = false;
                        txtDay3Applicant.Enabled = false;
                        btnDay3Assesser.Enabled = false;
                        txtDay3Assesser.Enabled = false;
                        ((LayoutGroup)day3).Visible = true;
                    }
                    if (master.Day4 != null)
                    {
                        lblday4.Text = "Day 4 (" + Convert.ToDateTime(master.Day4).ToString("dd/MM/yy") + ")";
                        if (master.Day4VerifiedByApplicant != null) Day4Applicant.Checked = true;
                        if (master.Day4VerifiedByAssesser != null) Day4Assessor.Checked = true;
                        btnDay4Applicant.Enabled = false;
                        txtDay4Applicant.Enabled = false;
                        btnDay4Assesser.Enabled = false;
                        txtDay4Assesser.Enabled = false;
                        ((LayoutGroup)day4).Visible = true;
                    }
                    if (master.Day5 != null)
                    {
                        lblday5.Text = "Day 5 (" + Convert.ToDateTime(master.Day5).ToString("dd/MM/yy") + ")";
                        if (master.Day5VerifiedByApplicant != null) Day5Applicant.Checked = true;
                        if (master.Day5VerifiedByAssesser != null) Day5Assessor.Checked = true;
                        btnDay5Applicant.Enabled = false;
                        txtDay5Applicant.Enabled = false;
                        btnDay5Assesser.Enabled = false;
                        txtDay5Assesser.Enabled = false;
                        ((LayoutGroup)day5).Visible = true;
                    }
                    if (master.Day6 != null)
                    {
                        lblday6.Text = "Day 6 (" + Convert.ToDateTime(master.Day6).ToString("dd/MM/yy") + ")";
                        if (master.Day6VerifiedByApplicant != null) Day6Applicant.Checked = true;
                        if (master.Day6VerifiedByAssesser != null) Day6Assessor.Checked = true;
                        btnDay6Applicant.Enabled = false;
                        txtDay6Applicant.Enabled = false;
                        btnDay6Assesser.Enabled = false;
                        txtDay6Assesser.Enabled = false;
                        ((LayoutGroup)day6).Visible = true;
                    }
                    if (master.Day7 != null)
                    {
                        lblday7.Text = "Day 7 (" + Convert.ToDateTime(master.Day7).ToString("dd/MM/yy") + ")";
                        if (master.Day7VerifiedByApplicant != null) Day7Applicant.Checked = true;
                        if (master.Day7VerifiedByAssesser != null) Day7Assessor.Checked = true;
                        btnDay7Applicant.Enabled = false;
                        txtDay7Applicant.Enabled = false;
                        btnDay7Assesser.Enabled = false;
                        txtDay7Assesser.Enabled = false;
                        ((LayoutGroup)day7).Visible = true;
                    }
                }

                if (master.CloseBy != "")
                {
                    if (closedgrp != null) ((LayoutGroup)closedgrp).Visible = true;
                    lblClosedStatus.Text = "Part 6: Notification of Work Completion, To be fill up by Permit Applicant (Closed on " + Convert.ToDateTime(master.CloseDate).ToString("dd MMM yyyy hh:mm") + ")";
                    lblClosedName.Text = master.CloseName;
                    lblClosedDesignation.Text = master.ClosePosition;
                    lblClosedCompany.Text = master.CloseCompany;
                    txtClosedRemarks.Text = master.CloseRemarks;
                    txtClosedRemarks.Enabled = false;
                    btnClosedClose.Visible = false;
                    ////////btnClosedCancel.Visible = false;
                    ////////if (master.Status == 4 && master.RequestBy == user.UserID) btnClosedCancel.Visible = true;
                }

                if (master.ClosureBy != "")
                {
                    if (closuregrp != null) ((LayoutGroup)closuregrp).Visible = true;
                    lblAcceptedStatus.Text = "Closure Review on " + Convert.ToDateTime(master.ClosureDate).ToString("dd MMM yyyy hh:mm");
                    lblAcceptedName.Text = master.ClosureBy;
                    lblAcceptedDesignation.Text = master.ClosurePosition;
                    lblAcceptedCompany.Text = master.ClosureCompany;
                    txtlblAcceptedRemarks.Text = master.ClosureRemarks;
                    txtlblAcceptedRemarks.Enabled = false;
                    btnAcceptedVerify.Visible = false;
                    btnAcceptedCancel.Visible = false;
                    if (master.Status == 5 && master.RequestBy == user.UserID) btnAcceptedCancel.Visible = true;
                }

                if (master.Status == 100 || master.Status == 98 || master.Status == 99)
                {
                    if (returnrejectgrp != null) ((LayoutGroup)returnrejectgrp).Visible = true;
                    txtReason.Enabled = false;
                    txtReason.Text = master.ReturnRejectReason;
                    txtReason.Border.BorderWidth = 0;
                }
            }
        }

        private void BindMaster()
        {
            lblProjectName.Text = master.ProjectName;
            dtFrom.Value = master.DateFrom;
            dtTo.Value = master.DateTo;
            cbType.Text = master.WorkType;
            txtJobDescription.Text = master.Description;
            cbMFG.Text = master.EquipmentName;
            seTotalWorker.Value = master.TotalWorker;
            txtDay1Applicant.Text = master.Day1RemarkByApplicant;
            txtDay1Assesser.Text = master.Day1RemarkByAssesser;
            txtDay2Applicant.Text = master.Day2RemarkByApplicant;
            txtDay2Assesser.Text = master.Day2RemarkByAssesser;
            txtDay3Applicant.Text = master.Day3RemarkByApplicant;
            txtDay3Assesser.Text = master.Day3RemarkByAssesser;
            txtDay4Applicant.Text = master.Day4RemarkByApplicant;
            txtDay4Assesser.Text = master.Day4RemarkByAssesser;
            txtDay5Applicant.Text = master.Day5RemarkByApplicant;
            txtDay5Assesser.Text = master.Day5RemarkByAssesser;
            txtDay6Applicant.Text = master.Day6RemarkByApplicant;
            txtDay6Assesser.Text = master.Day6RemarkByAssesser;
            txtDay7Applicant.Text = master.Day7RemarkByApplicant;
            txtDay7Assesser.Text = master.Day7RemarkByAssesser;
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
        //            attachdoclist = (List<AttachmentModel>)Session["PTW_Record_DOC"];
        //            attachdoclist.Add(ent);
        //            Session["PTW_Record_DOC"] = attachdoclist;
        //        }
        //        else
        //        {
        //            ent.FileType = "IMG";
        //            attachlist = (List<AttachmentModel>)Session["PTW_Record_IMG"];
        //            attachlist.Add(ent);
        //            Session["PTW_Record_IMG"] = attachlist;
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

        //            Session["PTW_Record_IMG"] = attachlist;
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

        //            Session["PTW_Record_DOC"] = attachdoclist;
        //        }
        //    }
        //}



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
                attachlist = (List<AttachmentModel>)Session["PTW_Record_IMG"];
                attachlist.Add(ent);
                Session["PTW_Record_IMG"] = attachlist;
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

                    Session["PTW_Record_IMG"] = attachlist;
                }
            }
        }
        protected void cvDocument_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var filename = e.Keys["FileName"];
            if (filename != null)
            {
                ASPxGridView tempGrid = (ASPxGridView)sender;
                attachlist = (List<AttachmentModel>)Session["PTW_Record_IMG"];
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

                    Session["PTW_Record_IMG"] = attachlist;
                }
            }
        }
        protected void cbDisplayImage_Callback(object sender, CallbackEventArgsBase e)
        {
            attachlist = (List<AttachmentModel>)Session["PTW_Record_IMG"];
            DisplayImageItems.DataSource = attachlist.Where(item => item.FileType == "IMG");
            DisplayImageItems.DataBind();
        }



        private void ClearQuestionAndAnswer()
        {
            var group = FormLayoutQNA.FindItemOrGroupByName("QuestionsAndAnswer");
            if (group != null)
            {
                ((LayoutGroup)group).Items.Clear();

                //LayoutItem itemCheckList = new LayoutItem();
                //itemCheckList.Width = Unit.Percentage(80);
                //itemCheckList.VerticalAlign = FormLayoutVerticalAlign.Top;
                //itemCheckList.Paddings.PaddingTop = Unit.Pixel(10);
                //itemCheckList.Paddings.PaddingBottom = Unit.Pixel(10);
                //itemCheckList.Caption = "";
                //itemCheckList.ShowCaption = DevExpress.Utils.DefaultBoolean.False;

                //ASPxLabel lbCheckList = new ASPxLabel();
                //lbCheckList.Width = Unit.Percentage(100);
                //lbCheckList.Text = "Description of Control Measures Implemented ";
                //lbCheckList.Font.Size = FontUnit.Point(12);
                //lbCheckList.Font.Underline = true;
                //lbCheckList.Font.Bold = true;
                //itemCheckList.Controls.Add(lbCheckList);

                //LayoutItem itemResult = new LayoutItem();
                //itemResult.Width = Unit.Percentage(20);
                //itemResult.VerticalAlign = FormLayoutVerticalAlign.Top;
                //itemResult.Paddings.PaddingTop = Unit.Pixel(10);
                //itemResult.Paddings.PaddingBottom = Unit.Pixel(10);
                //itemResult.Caption = "";
                //itemResult.ShowCaption = DevExpress.Utils.DefaultBoolean.False;

                //ASPxLabel lbResult = new ASPxLabel();
                //lbResult.Width = Unit.Percentage(100);
                //lbResult.Font.Size = FontUnit.Point(12);
                //lbResult.Font.Underline = true;
                //lbResult.Font.Bold = true;
                //lbResult.Text = "Result";
                //itemResult.Controls.Add(lbResult);

                //((LayoutGroup)group).Items.Add(itemCheckList);
                //((LayoutGroup)group).Items.Add(itemResult);
            }
        }
        private void AddEnableQuestionAnswer(QuestionAndAnswerModel value, int StatusCode, List<UserRoleModel> rolelist)
        {
            var group = FormLayoutQNA.FindItemOrGroupByName("QuestionsAndAnswer");
            if (group != null)
            {
                string[] strsplit = value.Selection.Split('|');
                switch (strsplit[0])
                {
                    case "N":
                        LayoutItem itemQuestion1 = new LayoutItem();
                        itemQuestion1.Width = Unit.Percentage(100);
                        itemQuestion1.Paddings.PaddingLeft = Unit.Pixel(20);
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
                        itemQuestion2.Controls.Add(lbQuestion2);
                        ((LayoutGroup)group).Items.Add(itemQuestion2);
                        break;
                    case "B2":
                        LayoutItem itemQuestion4 = new LayoutItem();
                        itemQuestion4.Paddings.PaddingTop = Unit.Pixel(10);
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
                        txtBox.ClientSideEvents.ValueChanged = "function(s, e) { QuestionsAndAnswerCallbackPanel.PerformCallback(''); }";
                        txtBox.ID = "Answer_" + value.ID.ToString();
                        txtBox.Width = Unit.Percentage(100);
                        txtBox.MaxLength = 450;
                        if (value.Answer != null && value.Answer != "") txtBox.Text = value.Answer;
                        if (StatusCode == 0 || StatusCode == 98) txtBox.Enabled = true;
                        else txtBox.Enabled = false;
                        itemQuestionT.Controls.Add(txtBox);
                        ((LayoutGroup)group).Items.Add(itemQuestionT);
                        break;
                    case "C":
                        LayoutItem itemQuestion3 = new LayoutItem();
                        itemQuestion3.Width = Unit.Percentage(100);
                        itemQuestion3.Paddings.PaddingLeft = Unit.Pixel(20);
                        itemQuestion3.VerticalAlign = FormLayoutVerticalAlign.Middle;
                        itemQuestion3.Caption = "";
                        itemQuestion3.ShowCaption = DevExpress.Utils.DefaultBoolean.False;
                        itemQuestion3.Paddings.PaddingTop = Unit.Pixel(20);
                        itemQuestion3.Paddings.PaddingBottom = Unit.Pixel(0);
                        ASPxCheckBox chkBox = new ASPxCheckBox();
                        chkBox.ID = "Answer_" + value.ID.ToString();
                        chkBox.ClientSideEvents.CheckedChanged = "function(s, e) { QuestionsAndAnswerCallbackPanel.PerformCallback(''); }";
                        chkBox.Width = Unit.Percentage(100);
                        chkBox.Text = value.Question;
                        chkBox.Font.Bold = true;
                        if (value.Answer != null && value.Answer != "") chkBox.Checked = (value.Answer == "T");
                        if (StatusCode == 0 || StatusCode == 98) chkBox.Enabled = true;
                        else chkBox.Enabled = false;
                        itemQuestion3.Controls.Add(chkBox);
                        ((LayoutGroup)group).Items.Add(itemQuestion3);
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
                        lstBox.ClientSideEvents.SelectedIndexChanged = "function(s, e) { QuestionsAndAnswerCallbackPanel.PerformCallback(''); }";
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
                        cbAnswer.ClientSideEvents.SelectedIndexChanged = "function OnListBoxIndexChanged(s, e) { QuestionsAndAnswerCallbackPanel.PerformCallback(''); }";
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



                //
                //cbAnswer.ValidationSettings.Display = Display.Dynamic;
                //cbAnswer.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.Text;
                //cbAnswer.ValidationSettings.ErrorText = "Please select the Answer.";
                //cbAnswer.ValidationSettings.RequiredField.ErrorText = "Please select the Answer.";
                //cbAnswer.ValidationSettings.RequiredField.IsRequired = true;
                //cbAnswer.ValidationSettings.ErrorTextPosition = ErrorTextPosition.Bottom;
                //cbAnswer.ValidationSettings.SetFocusOnError = true;
                //cbAnswer.InvalidStyle.BackColor = ColorTranslator.FromHtml("#FFE6EE");
                //cbAnswer.ClientSideEvents.SelectedIndexChanged = "function OnListBoxIndexChanged(s, e) { QuestionsAndAnswerCallbackPanel.PerformCallback(''); }";
                //cbAnswer.Width = Unit.Percentage(100);
                //string[] strplit = value.Selection.Split('|');
                //if (strplit.Length > 1)
                //{
                //    for (int i = 0; i < strplit.Length; i++)
                //        cbAnswer.Items.Add(strplit[i]);
                //}
                //else
                //{
                //    cbAnswer.Items.Add(value.Selection);
                //}
                //cbAnswer.ID = "Answer_" + value.ID.ToString();
                //cbAnswer.Text = value.Answer;
                //cbAnswer.Enabled = false;
                //if (value.Selection == "") cbAnswer.Visible = false;
                //itemAnswer.Controls.Add(cbAnswer);
                //switch (value.DocumentStatus)
                //{
                //    case 0:
                //        if (StatusCode == 0 || StatusCode == 98) cbAnswer.Enabled = true;
                //        else cbAnswer.Enabled = false;
                //        break;
                //    case 1:
                //        var assessor = rolelist.FirstOrDefault(item => item.RoleID == "PTW ASSESSOR");
                //        if (StatusCode == 1 && assessor != null)
                //        {
                //            cbAnswer.Enabled = true;
                //            cbAnswer.ValidationSettings.ValidationGroup = "Assessor";
                //        }
                //        else cbAnswer.Enabled = false;
                //        break;
                //    case 2:
                //        var approver = rolelist.FirstOrDefault(item => item.RoleID == "PTW APPROVER");
                //        if (StatusCode == 2 && approver != null)
                //        {
                //            cbAnswer.Enabled = true;
                //            cbAnswer.ValidationSettings.ValidationGroup = "Approve";
                //        }
                //        else cbAnswer.Enabled = false;
                //        break;
                //    case 3:
                //        var close = rolelist.FirstOrDefault(item => item.RoleID == "PTW USER");
                //        if (StatusCode == 3 && close != null)
                //        {
                //            cbAnswer.Enabled = true;
                //            cbAnswer.ValidationSettings.ValidationGroup = "Close";
                //        }
                //        else cbAnswer.Enabled = false;
                //        break;
                //    case 4:
                //        var review = rolelist.FirstOrDefault(item => item.RoleID == "PTW CLOSURE");
                //        if (StatusCode == 3 && review != null)
                //        {
                //            cbAnswer.Enabled = true;
                //            cbAnswer.ValidationSettings.ValidationGroup = "Review";
                //        }
                //        else cbAnswer.Enabled = false;
                //        break;
                //}




            }
        }
        //private void EnableQNA(bool Enable)
        //{
        //    var group = FormLayoutQNA.FindItemOrGroupByName("QuestionsAndAnswer");
        //    if (group != null)
        //    {
        //        templatedetaillist = (List<QuestionAndAnswerModel>)Session["PTW_TemplateDetails"];
        //        foreach (var item in ((LayoutGroup)group).Items)
        //        {
        //            if (item is LayoutItem)
        //            {
        //                foreach (var control in ((LayoutItem)item).Controls)
        //                {
        //                    if (control is ASPxComboBox)
        //                    {
        //                        if (((ASPxComboBox)control).ID.Contains("Answer_"))
        //                        {
        //                            ((ASPxComboBox)control).Enabled = Enable;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        Session["PTW_TemplateDetails"] = templatedetaillist;
        //    }
        //}
        protected void QuestionsAndAnswerCallbackPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            var group = FormLayoutQNA.FindItemOrGroupByName("QuestionsAndAnswer");
            if (group != null)
            {
                templatedetaillist = (List<QuestionAndAnswerModel>)Session["PTW_TemplateDetails"];
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
                            if (control is ASPxTextBox)
                            {
                                if (((ASPxTextBox)control).ID.Contains("Answer_"))
                                {
                                    string[] strplit = ((ASPxTextBox)control).ID.Split('_');
                                    int id = Convert.ToInt32(strplit[1]);
                                    string answer = ((ASPxTextBox)control).Text;

                                    var detail = templatedetaillist.FirstOrDefault(itm => itm.ID == id);
                                    if (detail != null) detail.Answer = answer;
                                }
                            }
                            if (control is ASPxCheckBox)
                            {
                                if (((ASPxCheckBox)control).ID.Contains("Answer_"))
                                {
                                    string[] strplit = ((ASPxCheckBox)control).ID.Split('_');
                                    int id = Convert.ToInt32(strplit[1]);
                                    string answer = "F";
                                    if (((ASPxCheckBox)control).Checked == true) answer = "T";
                                    var detail = templatedetaillist.FirstOrDefault(itm => itm.ID == id);
                                    if (detail != null) detail.Answer = answer;
                                }
                            }
                            if (control is ASPxListBox)
                            {
                                if (((ASPxListBox)control).ID.Contains("Answer_"))
                                {
                                    string[] strplit = ((ASPxListBox)control).ID.Split('_');
                                    int id = Convert.ToInt32(strplit[1]);

                                    string answer = "";
                                    foreach (ListEditItem li in ((ASPxListBox)control).SelectedItems)
                                    {
                                        if (answer == "")
                                            answer = (string)li.Value;
                                        else
                                            answer += "|" + (string)li.Value;
                                    }
                                    var detail = templatedetaillist.FirstOrDefault(itm => itm.ID == id);
                                    if (detail != null) detail.Answer = answer;
                                }
                            }
                        }
                    }
                }
                Session["PTW_TemplateDetails"] = templatedetaillist;
            }
        }





        protected void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();
            List<UserRoleModel> roles = UserRoleViewModel.GetUserRoleList(user.UserID);
            ClearQuestionAndAnswer();
            templatedetaillist = TemplateViewModel.GetTemplateDetails(cbType.Text);
            foreach(QuestionAndAnswerModel ent in templatedetaillist)
                AddEnableQuestionAnswer(ent, master.Status, roles);
            Session["PTW_TemplateDetails"] = templatedetaillist;
        }
        protected void cbAnswer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = ((ASPxComboBox)sender).ID;
            string[] strplit = name.Split('_');

            templatedetaillist = (List<QuestionAndAnswerModel>)Session["PTW_TemplateDetails"];
            var result = templatedetaillist.FirstOrDefault(item => item.ID == Convert.ToInt32(strplit[1]));
            if (result != null)
            {
                result.Answer = ((ASPxComboBox)sender).Text;
            }
            Session["PTW_TemplateDetails"] = templatedetaillist;
        }
        protected void dtFrom_Validation(object sender, ValidationEventArgs e)
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            bool result = date.Date <= dtTo.Date;
            TimeSpan difference = dtTo.Date - date.Date;
            TimeSpan diffwToday = date.Date - DateTime.Now.Date;
            result = result && (difference.Days >= 0 && difference.Days <= 6) && (diffwToday.Days >= 0);
            e.IsValid = result;
        }
        protected void dtTo_Validation(object sender, ValidationEventArgs e)
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            bool result = date.Date >= dtFrom.Date;
            TimeSpan difference = date.Date - dtFrom.Date;
            TimeSpan diffwToday = dtFrom.Date - DateTime.Now.Date;
            result = result && (difference.Days >= 0 && difference.Days <= 6) && (diffwToday.Days >= 0);
            e.IsValid = result;
        }





        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Page.IsCallback)
                DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/PTW/MyPTW.aspx");
            else
                Response.Redirect("~/PTW/MyPTW.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            try
            {
                attachlist = (List<AttachmentModel>)Session["PTW_Record_IMG"];
                //attachdoclist = (List<AttachmentModel>)Session["PTW_Record_DOC"];
                templatedetaillist = (List<QuestionAndAnswerModel>)Session["PTW_TemplateDetails"];
                master = (PTWModel)Session["PTW_Record"];
                master.Status = 97;
                master.Updated = DateTime.Now;
                master.UpdatedBy = user.UserID;
                master.RequestRemarks = txtSubmitRemarks.Text;
                PTWViewModel.PTW_InsertUpdate(master);
                foreach (QuestionAndAnswerModel qa in templatedetaillist)
                {
                    qa.Key = master.Key;
                    PTWDetailViewModel.PTWDetail_InsertUpdate(qa);
                }
                foreach (AttachmentModel img in attachlist)
                {
                    img.Key = master.Key;
                    AttachmentViewModel.Attachment_InsertUpdate(img);
                }
                //foreach (AttachmentModel doc in attachdoclist)
                //{
                //    doc.Key = master.Key;
                //    AttachmentViewModel.Attachment_InsertUpdate(doc);
                //}

                Session["PTWP_Record_IMG"] = null;
                //Session["PTW_Record_DOC"] = null;
                Session["PTW_Template"] = null;
                Session["PTW_TemplateDetails"] = null;
                Session["PTW_Record"] = null;

                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/PTW/MyPTW.aspx");
                else
                    Response.Redirect("~/PTW/MyPTW.aspx");
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnAssessedReturn_Click(object sender, EventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            try
            {
                attachlist = (List<AttachmentModel>)Session["PTW_Record_IMG"];
                //attachdoclist = (List<AttachmentModel>)Session["PTW_Record_DOC"];
                templatedetaillist = (List<QuestionAndAnswerModel>)Session["PTW_TemplateDetails"];
                master = (PTWModel)Session["PTW_Record"];

                master.Status = 98;
                master.Updated = DateTime.Now;
                master.UpdatedBy = user.UserID;
                master.ReturnRejectBy = user.UserID;
                master.ReturnRejectDate = DateTime.Now;
                master.ReturnRejectReason = txtReason.Text;

                PTWViewModel.PTW_InsertUpdate(master);
                Session["PTWP_Record_IMG"] = null;
                //Session["PTW_Record_DOC"] = null;
                Session["PTW_Template"] = null;
                Session["PTW_TemplateDetails"] = null;
                Session["PTW_Record"] = null;

                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/PTW/PendingPTW.aspx");
                else
                    Response.Redirect("~/PTW/PendingPTW.aspx");
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnAssessedReject_Click(object sender, EventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            try
            {
                attachlist = (List<AttachmentModel>)Session["PTW_Record_IMG"];
                //attachdoclist = (List<AttachmentModel>)Session["PTW_Record_DOC"];
                templatedetaillist = (List<QuestionAndAnswerModel>)Session["PTW_TemplateDetails"];
                master = (PTWModel)Session["PTW_Record"];

                master.Status = 99;
                master.Updated = DateTime.Now;
                master.UpdatedBy = user.UserID;
                master.ReturnRejectBy = user.UserID;
                master.ReturnRejectDate = DateTime.Now;
                master.ReturnRejectReason = txtReason.Text;

                PTWViewModel.PTW_InsertUpdate(master);
                Session["PTWP_Record_IMG"] = null;
                //Session["PTW_Record_DOC"] = null;
                Session["PTW_Template"] = null;
                Session["PTW_TemplateDetails"] = null;
                Session["PTW_Record"] = null;

                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/PTW/PendingPTW.aspx");
                else
                    Response.Redirect("~/PTW/PendingPTW.aspx");
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnApprovalReject_Click(object sender, EventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            try
            {
                attachlist = (List<AttachmentModel>)Session["PTW_Record_IMG"];
                //attachdoclist = (List<AttachmentModel>)Session["PTW_Record_DOC"];
                templatedetaillist = (List<QuestionAndAnswerModel>)Session["PTW_TemplateDetails"];
                master = (PTWModel)Session["PTW_Record"];

                master.Status = 99;
                master.Updated = DateTime.Now;
                master.UpdatedBy = user.UserID;
                master.ReturnRejectBy = user.UserID;
                master.ReturnRejectDate = DateTime.Now;
                master.ReturnRejectReason = txtReason.Text;

                PTWViewModel.PTW_InsertUpdate(master);
                Session["PTWP_Record_IMG"] = null;
                //Session["PTW_Record_DOC"] = null;
                Session["PTW_Template"] = null;
                Session["PTW_TemplateDetails"] = null;
                Session["PTW_Record"] = null;

                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/PTW/PendingPTW.aspx");
                else
                    Response.Redirect("~/PTW/PendingPTW.aspx");
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnApprovalReturn_Click(object sender, EventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            try
            {
                attachlist = (List<AttachmentModel>)Session["PTW_Record_IMG"];
                //attachdoclist = (List<AttachmentModel>)Session["PTW_Record_DOC"];
                templatedetaillist = (List<QuestionAndAnswerModel>)Session["PTW_TemplateDetails"];
                master = (PTWModel)Session["PTW_Record"];

                master.Status = 98;
                master.Updated = DateTime.Now;
                master.UpdatedBy = user.UserID;
                master.ReturnRejectBy = user.UserID;
                master.ReturnRejectDate = DateTime.Now;
                master.ReturnRejectReason = txtReason.Text;

                PTWViewModel.PTW_InsertUpdate(master);
                Session["PTWP_Record_IMG"] = null;
                //Session["PTW_Record_DOC"] = null;
                Session["PTW_Template"] = null;
                Session["PTW_TemplateDetails"] = null;
                Session["PTW_Record"] = null;

                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/PTW/PendingPTW.aspx");
                else
                    Response.Redirect("~/PTW/PendingPTW.aspx");
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnAcceptedVerify_Click(object sender, EventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            try
            {
                attachlist = (List<AttachmentModel>)Session["PTW_Record_IMG"];
                //attachdoclist = (List<AttachmentModel>)Session["PTW_Record_DOC"];
                templatedetaillist = (List<QuestionAndAnswerModel>)Session["PTW_TemplateDetails"];
                master = (PTWModel)Session["PTW_Record"];

                master.Status = 5;
                master.Updated = DateTime.Now;
                master.UpdatedBy = user.UserID;
                master.ClosureRemarks = txtlblAcceptedRemarks.Text;
                master.ClosureBy = user.UserID;
                master.ClosureCompany = user.ConstructorName;
                master.ClosureDate = DateTime.Now;
                master.ClosureName = user.FullName;
                master.ClosurePosition = user.Position;

                foreach (QuestionAndAnswerModel qa in templatedetaillist)
                {
                    qa.Key = master.Key;
                    PTWDetailViewModel.PTWDetail_InsertUpdate(qa);
                }

                PTWViewModel.PTW_InsertUpdate(master);

                Session["PTWP_Record_IMG"] = null;
                //Session["PTW_Record_DOC"] = null;
                Session["PTW_Template"] = null;
                Session["PTW_TemplateDetails"] = null;
                Session["PTW_Record"] = null;

                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/PTW/PendingPTW.aspx");
                else
                    Response.Redirect("~/PTW/PendingPTW.aspx");
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            string strTask = (Session["PTW_Task"] == null) ? "" : Session["PTW_Task"].ToString();
            string strLink = "~/PTW/MyPTW.aspx";
            if (strTask == "P") strLink = "~/PTW/PendingPTW.aspx";
            if (Page.IsCallback)
                DevExpress.Web.ASPxWebControl.RedirectOnCallback(strLink);
            else
                Response.Redirect(strLink);
        }



        protected void gvEquipment_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            ASPxGridView gridView = sender as ASPxGridView;
            if (gridView.IsEditing && e.Column.FieldName == "RegistrationNo")
            {
                ASPxComboBox cbEquipment = e.Editor as ASPxComboBox;
                var ProjectName = UserViewModel.GetSelectedProject();
                List<EquipmentModel> eqlist = EquipmentViewModel.GetEquipment_ByProject_CodeTable(ProjectName.ToString());
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
        private void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
        protected void gvEquipment_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ASPxGridView tempGrid = (ASPxGridView)sender;
            if (e.NewValues["RegistrationNo"] == null || e.NewValues["RegistrationNo"].ToString() == "")
                AddError(e.Errors, tempGrid.Columns["RegistrationNo"], "Please select the Equipment Registration Number.");

            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
                e.RowError = "Please, correct all errors.";
        }
        protected void gvEquipment_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView tempGrid = (ASPxGridView)sender;
            UserModel user = UserViewModel.GetLoggedInUserInfo();
            master = (PTWModel)Session["PTW_Record"];
            ptwequipmentlist = (List<PTWEquipmentModel>)Session["PTW_Equipment"];

            PTWEquipmentModel ent = new PTWEquipmentModel();
            ent.ID = 0;
            ent.Key = master.Key;
            ent.EquipmentName = e.NewValues["EquipmentName"] != null ? e.NewValues["EquipmentName"].ToString() : "";
            ent.RegistrationNo = e.NewValues["RegistrationNo"] != null ? e.NewValues["RegistrationNo"].ToString() : "";
            ent.EquipmentType = e.NewValues["EquipmentType"] != null ? e.NewValues["EquipmentType"].ToString() : "";
            ent.Created = DateTime.Now;
            ent.Updated = DateTime.Now;
            ent.CreatedBy = user.UserID;
            ent.UpdatedBy = user.UserID;

            ptwequipmentlist.Add(ent);
            Session["PTW_Equipment"] = ptwequipmentlist;

            e.Cancel = true;
            gvEquipment.CancelEdit();
            gvEquipment.DataSource = ptwequipmentlist;
        }
        protected void gvEquipment_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ASPxGridView tempGrid = (ASPxGridView)sender;
            string strName = tempGrid.GetRowValues(tempGrid.FocusedRowIndex, "RegistrationNo").ToString();
            if (strName != "")
            {
                ptwequipmentlist = (List<PTWEquipmentModel>)Session["PTW_Equipment"];

                PTWEquipmentModel ent = ptwequipmentlist.FirstOrDefault(item => item.RegistrationNo == strName);
                PTWEquipmentViewModel.ProjectEquipment_Delete(ent.EquipmentName, ent.Key);
                ptwequipmentlist.Remove(ent);
                Session["PTW_Equipment"] = ptwequipmentlist;

                e.Cancel = true;
                tempGrid.CancelEdit();
                tempGrid.DataSource = ptwequipmentlist;
            }
        }


        protected void gvStaff_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();
            var project = UserViewModel.GetSelectedProject();
            ASPxGridView gridView = sender as ASPxGridView;
            if (gridView.IsEditing && e.Column.FieldName == "UserID")
            {
                ASPxComboBox cbStaff = e.Editor as ASPxComboBox;
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
            master = (PTWModel)Session["PTW_Record"];
            ptwstafflist = (List<PTWStaffModel>)Session["PTW_Staff"]; 

            PTWStaffModel ent = new PTWStaffModel();
            ent.Key = master.Key;
            ent.UserID = e.NewValues["UserID"] != null ? e.NewValues["UserID"].ToString() : "";
            ent.FullName = e.NewValues["FullName"] != null ? e.NewValues["FullName"].ToString() : "";
            ent.Position = e.NewValues["Position"] != null ? e.NewValues["Position"].ToString() : "";
            ent.ConstructorName = e.NewValues["ConstructorName"] != null ? e.NewValues["ConstructorName"].ToString() : "";
            ent.Created = DateTime.Now;
            ent.Updated = DateTime.Now;
            ent.CreatedBy = user.UserID;
            ent.UpdatedBy = user.UserID;

            ptwstafflist.Add(ent);
            Session["PTW_Staff"] = ptwstafflist;

            e.Cancel = true;
            tempGrid.CancelEdit();
            tempGrid.DataSource = ptwstafflist;
        }
        protected void gvStaff_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ASPxGridView tempGrid = (ASPxGridView)sender;
            string strUserID = tempGrid.GetRowValues(tempGrid.FocusedRowIndex, "UserID").ToString();
            if (strUserID != "")
            {
                ptwstafflist = (List<PTWStaffModel>)Session["PTW_Staff"];

                PTWStaffModel ent = ptwstafflist.FirstOrDefault(item => item.UserID == strUserID);
                PTWStaffViewModel.PTWStaff_Delete(ent.UserID, ent.Key);
                ptwstafflist.Remove(ent);
                Session["PTW_Staff"] = ptwstafflist;

                e.Cancel = true;
                tempGrid.CancelEdit();
                tempGrid.DataSource = ptwstafflist;
            }
        }

        protected void cbMFG_CustomFiltering(object sender, ListEditCustomFilteringEventArgs e)
        {

        }






        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ASPxEdit.ValidateEditorsInContainer(this))
            {

                UserModel user = UserViewModel.GetLoggedInUserInfo();

                try
                {
                    attachlist = (List<AttachmentModel>)Session["PTW_Record_IMG"];
                    //attachdoclist = (List<AttachmentModel>)Session["PTW_Record_DOC"];
                    templatedetaillist = (List<QuestionAndAnswerModel>)Session["PTW_TemplateDetails"];
                    ptwequipmentlist = (List<PTWEquipmentModel>)Session["PTW_Equipment"];
                    master = (PTWModel)Session["PTW_Record"];
                    master.Status = 1;
                    master.RequestBy = user.UserID;
                    master.RequestCompany = user.ConstructorName;
                    master.RequestDate = DateTime.Now;
                    master.RequestPosition = user.Position;
                    master.RequestName = user.FullName;
                    master.ReturnRejectBy = "";
                    master.ReturnRejectDate = null;
                    master.ReturnRejectReason = "";
                    master.AssessBy = "";
                    master.AssessCompany = "";
                    master.AssessDate = null;
                    master.AssessName = "";
                    master.AssessPosition = "";
                    master.AssessRemarks = "";
                    master.ApproveBy = "";
                    master.ApproveCompany = "";
                    master.ApproveDate = null;
                    master.ApproveName = "";
                    master.ApprovePosition = "";
                    master.ApproveRemarks = "";
                    master.CloseBy = "";
                    master.CloseCompany = "";
                    master.CloseDate = null;
                    master.CloseName = "";
                    master.ClosePosition = "";
                    master.CloseRemarks = "";
                    master.ClosureBy = "";
                    master.ClosureCompany = "";
                    master.ClosureDate = null;
                    master.ClosureName = "";
                    master.ClosePosition = "";
                    master.CloseRemarks = "";
                    master.EquipmentName = cbMFG.Text;
                    master.TotalWorker = Convert.ToInt32(seTotalWorker.Value);
                    master.Created = DateTime.Now;
                    master.Updated = DateTime.Now;
                    master.CreatedBy = user.UserID;
                    master.UpdatedBy = user.UserID;
                    master.DateFrom = dtFrom.Date;
                    master.DateTo = dtTo.Date;
                    master.Description = txtJobDescription.Text;
                    master.WorkType = cbType.Text;
                    master.RequestRemarks = txtSubmitRemarks.Text;
                    master.Latitude = hfLatitude.Value;
                    master.Longitude = hfLongitude.Value;
                    master.SafetyOfficerBy = "";
                    master.SafetyOfficerName = "";
                    master.SafetyOfficerPosition = "";
                    master.SafetyOfficerCompany = "";
                    master.SafetyOfficerRemarks = "";
                    master.SafetyOfficerDate = null;
                    master.Day1 = null;
                    master.Day2 = null;
                    master.Day3 = null;
                    master.Day4 = null;
                    master.Day5 = null;
                    master.Day6 = null;
                    master.Day7 = null;
                    master.Day1RemarkByApplicant = "";
                    master.Day1RemarkByAssesser = "";
                    master.Day1VerifiedByApplicant = null;
                    master.Day1VerifiedByAssesser = null;
                    master.Day2RemarkByApplicant = "";
                    master.Day2RemarkByAssesser = "";
                    master.Day2VerifiedByApplicant = null;
                    master.Day2VerifiedByAssesser = null;
                    master.Day3RemarkByApplicant = "";
                    master.Day3RemarkByAssesser = "";
                    master.Day3VerifiedByApplicant = null;
                    master.Day3VerifiedByAssesser = null;
                    master.Day4RemarkByApplicant = "";
                    master.Day4RemarkByAssesser = "";
                    master.Day4VerifiedByApplicant = null;
                    master.Day4VerifiedByAssesser = null;
                    master.Day5RemarkByApplicant = "";
                    master.Day5RemarkByAssesser = "";
                    master.Day5VerifiedByApplicant = null;
                    master.Day5VerifiedByAssesser = null;
                    master.Day6RemarkByApplicant = "";
                    master.Day6RemarkByAssesser = "";
                    master.Day6VerifiedByApplicant = null;
                    master.Day6VerifiedByAssesser = null;
                    master.Day7RemarkByApplicant = "";
                    master.Day7RemarkByAssesser = "";
                    master.Day7VerifiedByApplicant = null;
                    master.Day7VerifiedByAssesser = null;

                    TimeSpan difference = dtTo.Date - dtFrom.Date;
                    for(int i = 0; i <= difference.Days; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                master.Day1 = dtFrom.Date;
                                break;
                            case 1:
                                master.Day2 = dtFrom.Date.AddDays(i);
                                break;
                            case 2:
                                master.Day3 = dtFrom.Date.AddDays(i);
                                break;
                            case 3:
                                master.Day4 = dtFrom.Date.AddDays(i);
                                break;
                            case 4:
                                master.Day5 = dtFrom.Date.AddDays(i);
                                break;
                            case 5:
                                master.Day6 = dtFrom.Date.AddDays(i);
                                break;
                            case 6:
                                master.Day7 = dtFrom.Date.AddDays(i);
                                break;
                        }
                    }


                    PTWViewModel.PTW_InsertUpdate(master);

                    foreach (QuestionAndAnswerModel qa in templatedetaillist)
                    {
                        qa.Key = master.Key;
                        PTWDetailViewModel.PTWDetail_InsertUpdate(qa);
                    }

                    foreach (AttachmentModel img in attachlist)
                    {
                        img.Key = master.Key;
                        AttachmentViewModel.Attachment_InsertUpdate(img);
                    }
                    //foreach (AttachmentModel doc in attachdoclist)
                    //{
                    //    doc.Key = master.Key;
                    //    AttachmentViewModel.Attachment_InsertUpdate(doc);
                    //}
                    //foreach (PTWEquipmentModel eq in ptwequipmentlist)
                    //{
                    //    eq.Key = master.Key;
                    //    PTWEquipmentViewModel.PTWEquipment_InsertUpdate(eq);
                    //}
                    //foreach (PTWStaffModel eq in ptwstafflist)
                    //{
                    //    eq.Key = master.Key;
                    //    PTWStaffViewModel.PTWStaff_InsertUpdate(eq);
                    //}

                    Session["PTWP_Record_IMG"] = null;
                    //Session["PTW_Record_DOC"] = null;
                    Session["PTW_Template"] = null;
                    Session["PTW_TemplateDetails"] = null;
                    Session["PTW_Record"] = null;
                    Session["PTW_Equipment"] = null;
                    Session["PTW_Staff"] = null;
                    //Session["PTW_EQUIPMENTLIST"] = null;
                    //Session["CCP_KEYACTLIST"] = null;

                    if (Page.IsCallback)
                        DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/PTW/MyPTW.aspx");
                    else
                        Response.Redirect("~/PTW/MyPTW.aspx");
                }
                catch (Exception ex)
                {
                }
            }
        }
        protected void btnAssessedApprove_Click(object sender, EventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();
            try
            {
                //attachlist = (List<AttachmentModel>)Session["PTW_Record_IMG"];
                ////attachdoclist = (List<AttachmentModel>)Session["PTW_Record_DOC"];
                //templatedetaillist = (List<QuestionAndAnswerModel>)Session["PTW_TemplateDetails"];
                master = (PTWModel)Session["PTW_Record"];
                master.Status = 2;
                master.Updated = DateTime.Now;
                master.UpdatedBy = user.UserID;
                master.AssessRemarks = txtAssessedRemarks.Text;
                master.AssessBy = user.UserID;
                master.AssessCompany = user.ConstructorName;
                master.AssessDate = DateTime.Now;
                master.AssessName = user.FullName;
                master.AssessPosition = user.Position;

                PTWViewModel.PTW_InsertUpdate(master);

                //foreach (QuestionAndAnswerModel qa in templatedetaillist)
                //{
                //    qa.Key = master.Key;
                //    PTWDetailViewModel.PTWDetail_InsertUpdate(qa);
                //}

                Session["PTWP_Record_IMG"] = null;
                //Session["PTW_Record_DOC"] = null;
                Session["PTW_Template"] = null;
                Session["PTW_TemplateDetails"] = null;
                Session["PTW_Record"] = null;

                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/PTW/PendingPTW.aspx");
                else
                    Response.Redirect("~/PTW/PendingPTW.aspx");
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnVerifyApprove_Click(object sender, EventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            try
            {
                master = (PTWModel)Session["PTW_Record"];
                master.Status = 3;
                master.Updated = DateTime.Now;
                master.UpdatedBy = user.UserID;
                master.SafetyOfficerRemarks = txtVerifiedRemarks.Text;
                master.SafetyOfficerBy = user.UserID;
                master.SafetyOfficerCompany = user.ConstructorName;
                master.SafetyOfficerDate = DateTime.Now;
                master.SafetyOfficerName = user.FullName;
                master.SafetyOfficerPosition = user.Position;
                PTWViewModel.PTW_InsertUpdate(master);

                Session["PTWP_Record_IMG"] = null;
                Session["PTW_Template"] = null;
                Session["PTW_TemplateDetails"] = null;
                Session["PTW_Record"] = null;

                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/PTW/PendingPTW.aspx");
                else
                    Response.Redirect("~/PTW/PendingPTW.aspx");
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnApprovalApprove_Click(object sender, EventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();
            try
            {
                //attachlist = (List<AttachmentModel>)Session["PTW_Record_IMG"];
                ////attachdoclist = (List<AttachmentModel>)Session["PTW_Record_DOC"];
                //templatedetaillist = (List<QuestionAndAnswerModel>)Session["PTW_TemplateDetails"];
                master = (PTWModel)Session["PTW_Record"];
                master.Status = 4;
                master.Updated = DateTime.Now;
                master.UpdatedBy = user.UserID;
                master.ApproveRemarks = txtApprovalReamrks.Text;
                master.ApproveBy = user.UserID;
                master.ApproveCompany = user.ConstructorName;
                master.ApproveDate = DateTime.Now;
                master.ApproveName = user.FullName;
                master.ApprovePosition = user.Position;

                PTWViewModel.PTW_InsertUpdate(master);

                //foreach (QuestionAndAnswerModel qa in templatedetaillist)
                //{
                //    qa.Key = master.Key;
                //    PTWDetailViewModel.PTWDetail_InsertUpdate(qa);
                //}

                Session["PTWP_Record_IMG"] = null;
                //Session["PTW_Record_DOC"] = null;
                Session["PTW_Template"] = null;
                Session["PTW_TemplateDetails"] = null;
                Session["PTW_Record"] = null;

                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/PTW/PendingPTW.aspx");
                else
                    Response.Redirect("~/PTW/PendingPTW.aspx");
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnClosedClose_Click(object sender, EventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();
            try
            {
                //attachlist = (List<AttachmentModel>)Session["PTW_Record_IMG"];
                //attachdoclist = (List<AttachmentModel>)Session["PTW_Record_DOC"];
                //templatedetaillist = (List<QuestionAndAnswerModel>)Session["PTW_TemplateDetails"];
                master = (PTWModel)Session["PTW_Record"];

                master.Status = 5;
                master.Updated = DateTime.Now;
                master.UpdatedBy = user.UserID;
                master.CloseRemarks = txtClosedRemarks.Text;
                master.CloseBy = user.UserID;
                master.CloseCompany = user.ConstructorName;
                master.CloseDate = DateTime.Now;
                master.CloseName = user.FullName;
                master.ClosePosition = user.Position;

                //foreach (QuestionAndAnswerModel qa in templatedetaillist)
                //{
                //    qa.Key = master.Key;
                //    PTWDetailViewModel.PTWDetail_InsertUpdate(qa);
                //}

                PTWViewModel.PTW_InsertUpdate(master);


                Session["PTWP_Record_IMG"] = null;
                //Session["PTW_Record_DOC"] = null;
                Session["PTW_Template"] = null;
                Session["PTW_TemplateDetails"] = null;
                Session["PTW_Record"] = null;

                string strTask = (Session["PTW_Task"] == null) ? "" : Session["PTW_Task"].ToString();
                string strLink = "~/PTW/MyPTW.aspx";
                if (strTask == "P") strLink = "~/PTW/PendingPTW.aspx";
                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(strLink);
                else
                    Response.Redirect(strLink);
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
                master = (PTWModel)Session["PTW_Record"];
                if (master.Status == 3)
                {
                    master.ApproveRemarks = txtApprovalReamrks.Text;
                    master.ApproveBy = user.UserID;
                    master.ApproveCompany = user.ConstructorName;
                    master.ApproveDate = DateTime.Now;
                    master.ApproveName = user.FullName;
                    master.ApprovePosition = user.Position;
                }
                master.Status = 99;
                master.Updated = DateTime.Now;
                master.UpdatedBy = user.UserID;
                master.ReturnRejectBy = user.UserID;
                master.ReturnRejectDate = DateTime.Now;
                master.ReturnRejectReason = txtReason.Text;
                PTWViewModel.PTW_InsertUpdate(master);
                Session["PTWP_Record_IMG"] = null;
                Session["PTW_Template"] = null;
                Session["PTW_TemplateDetails"] = null;
                Session["PTW_Record"] = null;
                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/PTW/PendingPTW.aspx");
                else
                    Response.Redirect("~/PTW/PendingPTW.aspx");
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
                master = (PTWModel)Session["PTW_Record"];
                if (master.Status == 3)
                {
                    master.ApproveRemarks = txtApprovalReamrks.Text;
                    master.ApproveBy = user.UserID;
                    master.ApproveCompany = user.ConstructorName;
                    master.ApproveDate = DateTime.Now;
                    master.ApproveName = user.FullName;
                    master.ApprovePosition = user.Position;
                }
                master.Status = 98;
                master.Updated = DateTime.Now;
                master.UpdatedBy = user.UserID;
                master.ReturnRejectBy = user.UserID;
                master.ReturnRejectDate = DateTime.Now;
                master.ReturnRejectReason = txtReason.Text;
                PTWViewModel.PTW_InsertUpdate(master);
                Session["PTWP_Record_IMG"] = null;
                Session["PTW_Template"] = null;
                Session["PTW_TemplateDetails"] = null;
                Session["PTW_Record"] = null;
                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/PTW/PendingPTW.aspx");
                else
                    Response.Redirect("~/PTW/PendingPTW.aspx");
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnRevoke_Click(object sender, EventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();
            try
            {
                master = (PTWModel)Session["PTW_Record"];
                master.Status = 100;
                master.Updated = DateTime.Now;
                master.UpdatedBy = user.UserID;
                master.ReturnRejectBy = user.UserID;
                master.ReturnRejectDate = DateTime.Now;
                master.ReturnRejectReason = txtReason.Text;
                PTWViewModel.PTW_InsertUpdate(master);
                Session["PTWP_Record_IMG"] = null;
                Session["PTW_Template"] = null;
                Session["PTW_TemplateDetails"] = null;
                Session["PTW_Record"] = null;
                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/PTW/PendingPTW.aspx");
                else
                    Response.Redirect("~/PTW/PendingPTW.aspx");
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnDayApplicant_Click(object sender, EventArgs e)
        {
            ASPxButton btn = (sender as ASPxButton);
            master = (PTWModel)Session["PTW_Record"];
            PTWModel ent = PTWViewModel.GetPTWRecord(master.Key);
            switch (btn.GroupName)
            {
                case "1":
                    ent.Day1RemarkByApplicant = txtDay1Applicant.Text;
                    ent.Day1VerifiedByApplicant = DateTime.Now;
                    PTWViewModel.PTW_InsertUpdate(ent);
                    break;
                case "2":
                    ent.Day2RemarkByApplicant = txtDay2Applicant.Text;
                    ent.Day2VerifiedByApplicant = DateTime.Now;
                    PTWViewModel.PTW_InsertUpdate(ent);
                    break;
                case "3":
                    ent.Day3RemarkByApplicant = txtDay3Applicant.Text;
                    ent.Day3VerifiedByApplicant = DateTime.Now;
                    PTWViewModel.PTW_InsertUpdate(ent);
                    break;
                case "4":
                    ent.Day4RemarkByApplicant = txtDay4Applicant.Text;
                    ent.Day4VerifiedByApplicant = DateTime.Now;
                    PTWViewModel.PTW_InsertUpdate(ent);
                    break;
                case "5":
                    ent.Day5RemarkByApplicant = txtDay5Applicant.Text;
                    ent.Day5VerifiedByApplicant = DateTime.Now;
                    PTWViewModel.PTW_InsertUpdate(ent);
                    break;
                case "6":
                    ent.Day6RemarkByApplicant = txtDay6Applicant.Text;
                    ent.Day6VerifiedByApplicant = DateTime.Now;
                    PTWViewModel.PTW_InsertUpdate(ent);
                    break;
                case "7":
                    ent.Day7RemarkByApplicant = txtDay7Applicant.Text;
                    ent.Day7VerifiedByApplicant = DateTime.Now;
                    PTWViewModel.PTW_InsertUpdate(ent);
                    break;
            }

            if (Page.IsCallback)
                DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/PTW/MyPTW.aspx");
            else
                Response.Redirect("~/PTW/MyPTW.aspx");
        }
        protected void btnDayAssesser_Click(object sender, EventArgs e)
        {
            ASPxButton btn = (sender as ASPxButton);
            master = (PTWModel)Session["PTW_Record"];
            PTWModel ent = PTWViewModel.GetPTWRecord(master.Key);
            switch (btn.GroupName)
            {
                case "1":
                    ent.Day1RemarkByAssesser = txtDay1Assesser.Text;
                    ent.Day1VerifiedByAssesser = DateTime.Now;
                    if (ent.Day1VerifiedByApplicant == null)
                    {
                        ent.Day1RemarkByApplicant = "";
                        ent.Day1VerifiedByApplicant = DateTime.Now;
                    }
                    PTWViewModel.PTW_InsertUpdate(ent);
                    break;
                case "2":
                    ent.Day2RemarkByAssesser = txtDay2Assesser.Text;
                    ent.Day2VerifiedByAssesser = DateTime.Now;
                    if (ent.Day2VerifiedByApplicant == null)
                    {
                        ent.Day2RemarkByApplicant = "";
                        ent.Day2VerifiedByApplicant = DateTime.Now;
                    }
                    PTWViewModel.PTW_InsertUpdate(ent);
                    break;
                case "3":
                    ent.Day3RemarkByAssesser = txtDay3Assesser.Text;
                    ent.Day3VerifiedByAssesser = DateTime.Now;
                    if (ent.Day3VerifiedByApplicant == null)
                    {
                        ent.Day3RemarkByApplicant = "";
                        ent.Day3VerifiedByApplicant = DateTime.Now;
                    }
                    PTWViewModel.PTW_InsertUpdate(ent);
                    break;
                case "4":
                    ent.Day4RemarkByAssesser = txtDay4Assesser.Text;
                    ent.Day4VerifiedByAssesser = DateTime.Now;
                    if (ent.Day4VerifiedByApplicant == null)
                    {
                        ent.Day4RemarkByApplicant = "";
                        ent.Day4VerifiedByApplicant = DateTime.Now;
                    }
                    PTWViewModel.PTW_InsertUpdate(ent);
                    break;
                case "5":
                    ent.Day5RemarkByAssesser = txtDay5Assesser.Text;
                    ent.Day5VerifiedByAssesser = DateTime.Now;
                    if (ent.Day5VerifiedByApplicant == null)
                    {
                        ent.Day5RemarkByApplicant = "";
                        ent.Day5VerifiedByApplicant = DateTime.Now;
                    }
                    PTWViewModel.PTW_InsertUpdate(ent);
                    break;
                case "6":
                    ent.Day6RemarkByAssesser = txtDay6Assesser.Text;
                    ent.Day6VerifiedByAssesser = DateTime.Now;
                    if (ent.Day6VerifiedByApplicant == null)
                    {
                        ent.Day6RemarkByApplicant = "";
                        ent.Day6VerifiedByApplicant = DateTime.Now;
                    }
                    PTWViewModel.PTW_InsertUpdate(ent);
                    break;
                case "7":
                    ent.Day7RemarkByAssesser = txtDay7Assesser.Text;
                    ent.Day7VerifiedByAssesser = DateTime.Now;
                    if (ent.Day7VerifiedByApplicant == null)
                    {
                        ent.Day7RemarkByApplicant = "";
                        ent.Day7VerifiedByApplicant = DateTime.Now;
                    }
                    PTWViewModel.PTW_InsertUpdate(ent);
                    break;
            }

            if (Page.IsCallback)
                DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/PTW/PendingPTW.aspx");
            else
                Response.Redirect("~/PTW/PendingPTW.aspx");
        }
    }
}
