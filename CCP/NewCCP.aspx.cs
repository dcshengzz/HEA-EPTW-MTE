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

namespace HEA.ePTW.CCP
{
    public partial class NewCCP : System.Web.UI.Page
    {
        string strKey = Guid.NewGuid().ToString();
        //List<AttachmentModel> attachdoclist;
        List<AttachmentModel> attachlist;
        List<EquipmentModel> equipmentlist;
        List<KeyActivitiesModel> keyactivitieslist;
        List<QuestionAndAnswerModel> templatedetaillist;
        CCPModel master;

        private void RetrieveFromQueryString()
        {
            string strTask = (Request.QueryString["Status"] == null) ? "" : Request.QueryString["Status"].ToString();
            if (strTask == "N")
            {
                Session["CCP_Record_IMG"] = null;
                Session["CCP_EQUIPMENTLIST"] = null;
                Session["CCP_KEYACTLIST"] = null;
                Session["CCP_Record"] = null;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var project = UserViewModel.GetSelectedProject();
                lblProjectName.Text = project != null ? project.ToString() : "";
                UserModel user = UserViewModel.GetLoggedInUserInfo();
                List<UserRoleModel> userroles = UserRoleViewModel.GetUserRoleList(user.UserID);

                if (!Page.IsPostBack)
                {
                    RetrieveFromQueryString();
                    equipmentlist = EquipmentViewModel.GetEquipment_ByProjectAndType_CodeTable(project.ToString(), "MFG NO");
                    keyactivitieslist = KeyActivitiesViewModel.GetKeyActivitiesList("CCP");
                    Session["CCP_EQUIPMENTLIST"] = equipmentlist;
                    Session["CCP_KEYACTLIST"] = keyactivitieslist;

                    if (Session["CCP_Record"] != null)
                    {
                        master = (CCPModel)Session["CCP_Record"];
                        attachlist = AttachmentViewModel.GetAttachmentList(master.Key);
                        templatedetaillist = CCPViewModel.GetCCPDetails(master.Key);
                    }
                    else
                    {
                        master = new CCPModel();
                        master.Key = strKey;
                        master.ProjectName = project != null ? project.ToString() : "";
                        attachlist = new List<AttachmentModel>();
                        Session["CCP_Record"] = master;
                    }

                    Session["CCP_Record_IMG"] = attachlist;
                    Session["CCP_TemplateDetails"] = templatedetaillist;

                    cbMFG.DataSource = equipmentlist;
                    cbMFG.DataBind();
                    cbKey.DataSource = keyactivitieslist;
                    cbKey.DataBind();
                    BindMaster();
                }
                else
                {
                    attachlist = (List<AttachmentModel>)Session["CCP_Record_IMG"];
                    equipmentlist = (List<EquipmentModel>)Session["CCP_EQUIPMENTLIST"];
                    keyactivitieslist = (List<KeyActivitiesModel>)Session["CCP_KEYACTLIST"];
                    master = (CCPModel)Session["CCP_Record"];
                    templatedetaillist = (List<QuestionAndAnswerModel>)Session["CCP_TemplateDetails"];

                    cbMFG.DataSource = equipmentlist;
                    cbMFG.DataBind();
                    cbKey.DataSource = keyactivitieslist;
                    cbKey.DataBind();
                }

                //Show Title & Location
                string strLatitude = hfLatitude.Value;
                string strLongitude = hfLongitude.Value;
                string strLocation = "";
                switch (master.Status)
                {
                    case 0:
                        lblTitle.Text = "Compliance Check Point - New";
                        if (strLatitude != "") strLocation = $"Location : Latitude {strLatitude}, Longitude {strLongitude}";
                        break;
                    case 1:
                        lblTitle.Text = "Compliance Check Point - Submitted";
                        if (master.Latitude != "") strLocation = $"Location : Latitude {master.Latitude}, Longitude {master.Longitude}";
                        break;
                    case 2:
                        lblTitle.Text = "Compliance Check Point - Completed";
                        if (master.Latitude != "") strLocation = $"Location : Latitude {master.Latitude}, Longitude {master.Longitude}";
                        break;
                    case 98:
                        lblTitle.Text = "Compliance Check Point - Returned";
                        if (strLatitude != "") strLocation = $"Location : Latitude {strLatitude}, Longitude {strLongitude}";
                        break;
                    case 99:
                        lblTitle.Text = "Compliance Check Point - Rejected";
                        if (master.Latitude != "") strLocation = $"Location : Latitude {master.Latitude}, Longitude {master.Longitude}";
                        break;
                }
                lblLocation.Text = strLocation;

                if (templatedetaillist != null && templatedetaillist.Count > 0)
                {
                    ClearQuestionAndAnswer();
                    foreach (QuestionAndAnswerModel ent in templatedetaillist)
                        AddEnableQuestionAnswer(ent, master.Status);
                }
                var uploadfilegroup = flCCP.FindItemOrGroupByName("AttachmentFileControl");
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
                var returnrejectgroup = flCCP.FindItemOrGroupByName("ReturnRejectControl");
                if (returnrejectgroup != null) (returnrejectgroup as LayoutItem).Visible = false;

                btnSubmit.Visible = false;
                btnCancel.Visible = false;
                btnDelete.Visible = false;
                btnApprove.Visible = false;
                btnReturn.Visible = false;
                btnReject.Visible = false;
                cvAttachmentDocument.SettingsDataSecurity.AllowDelete = false;
                cbMFG.Enabled = false;
                cbKey.Enabled = false;
                txtDescription.Enabled = false;
                dtDate.Enabled = false;
                txtReason.Enabled = false;

                if (master.Status == 0)
                {
                    cbMFG.Enabled = true;
                    cbKey.Enabled = true;
                    txtDescription.Enabled = true;
                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;
                    dtDate.Enabled = true;
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
                }
                if (master.Status == 1)
                {
                    var approverRole = userroles.FirstOrDefault(item => item.RoleID == "CCP APPROVER");
                    if (approverRole != null && master.CreatedBy != user.UserID)
                    {
                        if (returnrejectgroup != null) (returnrejectgroup as LayoutItem).Visible = true;
                        btnApprove.Visible = true;
                        btnReturn.Visible = true;
                        btnReject.Visible = true;
                        txtReason.Enabled = true;
                    }
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
                    cvAttachmentDocument.SettingsDataSecurity.AllowDelete = false;
                    if (uploadfilegroup != null)
                    {
                        var EditDocument = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("EditAttachmentDoc");
                        if (EditDocument != null) (EditDocument as LayoutItem).Visible = true;
                        var DisplayImage = (uploadfilegroup as LayoutGroup).FindItemOrGroupByName("DisplayImage");
                        if (DisplayImage != null) (DisplayImage as LayoutItem).Visible = true;
                    }
                }
                if (master.Status == 99)
                {
                    if (returnrejectgroup != null) (returnrejectgroup as LayoutItem).Visible = true;
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
                    txtReason.Enabled = false;

                    cbMFG.Enabled = true;
                    cbKey.Enabled = true;
                    txtDescription.Enabled = true;
                    dtDate.Enabled = true;
                    btnSubmit.Visible = true;
                    btnDelete.Visible = true;
                    btnCancel.Visible = true;
                }

                cvAttachmentDocument.DataSource = attachlist;
                cvAttachmentDocument.DataBind();
                DisplayImageItems.DataSource = attachlist.Where(item => item.FileType == "IMG");
                DisplayImageItems.DataBind();
            }
            catch
            {
                Response.Redirect("~/Account/SignIn.aspx");
            }

        }

        private void BindMaster()
        {
            lblProjectName.Text = master.ProjectName;
            dtDate.Value = master.ActivitiesDate;
            cbMFG.Text = master.EquipmentName;
            cbKey.Text = master.KeyActivitiesName;
            txtDescription.Text = master.Description;
            txtReason.Text = master.ReturnRejectReason;
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
                attachlist = (List<AttachmentModel>)Session["CCP_Record_IMG"];
                attachlist.Add(ent);
                Session["CCP_Record_IMG"] = attachlist;
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

                    Session["CCP_Record_IMG"] = attachlist;
                }
            }
        }
        protected void cvDocument_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var filename = e.Keys["FileName"];
            if (filename != null)
            {
                ASPxGridView tempGrid = (ASPxGridView)sender;
                attachlist = (List<AttachmentModel>)Session["CCP_Record_IMG"];
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

                    Session["CCP_Record_IMG"] = attachlist;
                }
            }
        }
        protected void cbDisplayImage_Callback(object sender, CallbackEventArgsBase e)
        {
            attachlist = (List<AttachmentModel>)Session["CCP_Record_IMG"];
            DisplayImageItems.DataSource = attachlist.Where(item => item.FileType == "IMG");
            DisplayImageItems.DataBind();
        }


        // Document Workflow
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            try
            {
                attachlist = (List<AttachmentModel>)Session["CCP_Record_IMG"];
                templatedetaillist = (List<QuestionAndAnswerModel>)Session["CCP_TemplateDetails"];
                //attachdoclist = (List<AttachmentModel>)Session["CCP_Record_DOC"];
                master = (CCPModel)Session["CCP_Record"];
                master.KeyActivitiesName = cbKey.Text;
                master.EquipmentName = cbMFG.Text;
                master.Description = txtDescription.Text;
                master.Status = 1;
                master.ActivitiesDate = (DateTime)dtDate.Value;
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

                CCPViewModel.CCP_InsertUpdate(master);
                foreach (AttachmentModel img in attachlist)
                {
                    img.Key = master.Key;
                    AttachmentViewModel.Attachment_InsertUpdate(img);
                }

                foreach (QuestionAndAnswerModel qa in templatedetaillist)
                {
                    qa.Key = master.Key;
                    CCPViewModel.CCPDetail_InsertUpdate(qa);
                }
                ////////    foreach (AttachmentModel doc in attachdoclist)
                ////////    {
                ////////        doc.Key = master.Key;
                ////////        AttachmentViewModel.Attachment_InsertUpdate(doc);
                ////////    }

                Session["CCP_Record_IMG"] = null;
                //Session["CCP_Record_DOC"] = null;
                Session["CCP_TemplateDetails"] = null;
                Session["CCP_EQUIPMENTLIST"] = null;
                Session["CCP_KEYACTLIST"] = null;

                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/CCP/MyCCP.aspx");
                else
                    Response.Redirect("~/CCP/MyCCP.aspx");
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
                master = (CCPModel)Session["CCP_Record"];
                master.KeyActivitiesName = cbKey.Text;
                master.EquipmentName = cbMFG.Text;
                master.Description = txtDescription.Text;
                master.Status = 97;
                master.ActivitiesDate = (DateTime)dtDate.Value;
                master.Updated = DateTime.Now;
                master.UpdatedBy = user.UserID;
                master.ReturnRejectDate = null;
                master.ReturnRejectReason = "";
                master.ApprovedBy = "";
                master.ApprovedDate = null;

                CCPViewModel.CCP_InsertUpdate(master);

                Session["CCP_Record_IMG"] = null;
                Session["CCP_Record_DOC"] = null;
                Session["CCP_EQUIPMENTLIST"] = null;
                Session["CCP_KEYACTLIST"] = null;

                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/CCP/MyCCP.aspx");
                else
                    Response.Redirect("~/CCP/MyCCP.aspx");
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Page.IsCallback)
                DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/CCP/MyCCP.aspx");
            else
                Response.Redirect("~/CCP/MyCCP.aspx");
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            try
            {
                master = (CCPModel)Session["CCP_Record"];
                master.KeyActivitiesName = cbKey.Text;
                master.EquipmentName = cbMFG.Text;
                master.Description = txtDescription.Text;
                master.Status = 2;
                master.ActivitiesDate = (DateTime)dtDate.Value;
                master.Updated = DateTime.Now;
                master.UpdatedBy = user.UserID;
                master.ReturnRejectDate = null;
                master.ReturnRejectReason = "";
                master.ApprovedBy = user.UserID;
                master.ApprovedDate = DateTime.Now;

                CCPViewModel.CCP_InsertUpdate(master);

                Session["CCP_Record_IMG"] = null;
                //Session["CCP_Record_DOC"] = null;
                Session["CCP_EQUIPMENTLIST"] = null;
                Session["CCP_KEYACTLIST"] = null;

                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/CCP/PendingCCP.aspx");
                else
                    Response.Redirect("~/CCP/PendingCCP.aspx");
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
                master = (CCPModel)Session["CCP_Record"];
                master.KeyActivitiesName = cbKey.Text;
                master.EquipmentName = cbMFG.Text;
                master.Description = txtDescription.Text;
                master.Status = 99;
                master.ActivitiesDate = (DateTime)dtDate.Value;
                master.Updated = DateTime.Now;
                master.UpdatedBy = user.UserID;
                master.ReturnRejectDate = DateTime.Now;
                master.ReturnRejectReason = txtReason.Text;
                master.ApprovedBy = "";
                master.ApprovedDate = null;

                CCPViewModel.CCP_InsertUpdate(master);

                Session["CCP_Record_IMG"] = null;
                //Session["CCP_Record_DOC"] = null;
                Session["CCP_EQUIPMENTLIST"] = null;
                Session["CCP_KEYACTLIST"] = null;

                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/CCP/PendingCCP.aspx");
                else
                    Response.Redirect("~/CCP/PendingCCP.aspx");
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
                master = (CCPModel)Session["CCP_Record"];
                master.KeyActivitiesName = cbKey.Text;
                master.EquipmentName = cbMFG.Text;
                master.Description = txtDescription.Text;
                master.Status = 98;
                master.ActivitiesDate = (DateTime)dtDate.Value;
                master.Updated = DateTime.Now;
                master.UpdatedBy = user.UserID;
                master.ReturnRejectDate = DateTime.Now;
                master.ReturnRejectReason = txtReason.Text;
                master.ApprovedBy = "";
                master.ApprovedDate = null;

                CCPViewModel.CCP_InsertUpdate(master);

                Session["CCP_Record_IMG"] = null;
                //Session["CCP_Record_DOC"] = null;
                Session["CCP_EQUIPMENTLIST"] = null;
                Session["CCP_KEYACTLIST"] = null;

                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/CCP/PendingCCP.aspx");
                else
                    Response.Redirect("~/CCP/PendingCCP.aspx");
            }
            catch (Exception ex)
            {
            }
        }


        // Filtering Function
        protected void cbMFG_CustomFiltering(object sender, ListEditCustomFilteringEventArgs e)
        {
            string[] words = e.Filter.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] columns = new string[] { "EquipmentName", "RegistrationNo", "EquipmentType" };
            e.FilterExpression = GroupOperator.And(words.Select(w =>
                GroupOperator.Or(
                    columns.Select(c =>
                        new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(c), w)
                    )
                )
            )).ToString();
            e.CustomHighlighting = columns.ToDictionary(c => c, c => words);
        }
        protected void cbKey_CustomFiltering(object sender, ListEditCustomFilteringEventArgs e)
        {
            string[] words = e.Filter.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] columns = new string[] { "Name", "Description" };
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
                templatedetaillist = (List<QuestionAndAnswerModel>)Session["CCP_TemplateDetails"];
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
                Session["CCP_TemplateDetails"] = templatedetaillist;
            }
        }
        protected void cbKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();
            List<UserRoleModel> roles = UserRoleViewModel.GetUserRoleList(user.UserID);
            templatedetaillist = TemplateViewModel.GetTemplateDetails(cbKey.Text);

            if (templatedetaillist != null && templatedetaillist.Count > 0)
            {
                ClearQuestionAndAnswer();
                foreach (QuestionAndAnswerModel ent in templatedetaillist)
                    AddEnableQuestionAnswer(ent, master.Status);
            }
            Session["CCP_TemplateDetails"] = templatedetaillist;
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
                lbCheckList.Text = "Control Measures Implemented :-";
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
    }
}