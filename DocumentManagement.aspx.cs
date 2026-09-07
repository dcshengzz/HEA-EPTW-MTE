using DevExpress.Web;
using HEA.ePTW.Models;
using HEA.ePTW.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HEA.ePTW
{
    public partial class Documents : System.Web.UI.Page
    {
        List<DocumentModel> doclist;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strProject = UserViewModel.GetSelectedProject();
                doclist = DocumentViewModel.GetDocumentList(strProject);
                Session["ePTW_DocList"] = doclist;
                Session["ePTW_DocumentData"] = null;
                Session["ePTW_DocumentExt"] = null;
                Session["ePTW_DocumentFileName"] = null;
            }
            else
            {
                doclist = (Session["ePTW_DocList"] as List<DocumentModel>);
            }

            gvDocuments.DataSource = doclist;
            gvDocuments.DataBind();
            gvDocuments.FocusedRowIndex = -1;
        }

        protected void gvDocuments_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            ASPxGridView gridView = sender as ASPxGridView;
            if (gridView.IsEditing && e.Column.FieldName == "DocumentType")
            {
                List<DocumentTypeModel> list = DocumentTypeViewModel.GetDocumentTypeList();
                ASPxComboBox cbDocType = e.Editor as ASPxComboBox;

                cbDocType.TextField = "DocumentType";
                cbDocType.ValueField = "DocumentType";
                cbDocType.DataSource = list;
                cbDocType.DataBind();
            }
        }
        private void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
        protected void gvDocuments_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            if (e.NewValues["DocumentType"] == null || e.NewValues["DocumentType"].ToString() == "")
                AddError(e.Errors, gvDocuments.Columns["DocumentType"], "Please select the Document Type.");
            if (e.NewValues["Description"] == null || e.NewValues["Description"].ToString() == "")
                AddError(e.Errors, gvDocuments.Columns["Description"], "Please enter the Equipment Description.");
            if (Session["ePTW_DocumentData"] == null)
                AddError(e.Errors, gvDocuments.Columns["DocumentData"], "Please select and upload the Document.");
            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
                e.RowError = "Please, correct all errors.";
        }
        protected void gvDocuments_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();
            DocumentModel ent = new DocumentModel();

            ent.Description = e.NewValues["Description"] != null ? e.NewValues["Description"].ToString() : "";
            ent.DocumentType = e.NewValues["DocumentType"] != null ? e.NewValues["DocumentType"].ToString() : "";
            ent.DocumentData = Session["ePTW_DocumentData"] != null ? (byte[])Session["ePTW_DocumentData"] : null;
            ent.FileType = Session["ePTW_DocumentExt"] != null ? Session["ePTW_DocumentExt"].ToString() : "";
            ent.FileName = Session["ePTW_DocumentFileName"] != null ? Session["ePTW_DocumentFileName"].ToString() : "";
            ent.ProjectName = UserViewModel.GetSelectedProject();
            ent.Created = DateTime.Now;
            ent.Updated = DateTime.Now;
            ent.CreatedBy = user.UserID;
            ent.UpdatedBy = user.UserID;
            ent.Status = 1;

            doclist.Add(ent);
            Session["ePTW_DocList"] = doclist;

            DocumentViewModel.Document_InsertUpdate(ent);

            e.Cancel = true;
            gvDocuments.CancelEdit();
            gvDocuments.DataSource = doclist;
            gvDocuments.DataBind();
        }
        protected void gvDocuments_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();
            int id = (int)e.Keys["ID"];
            DocumentModel ent = doclist.Find(item => item.ID == id);
            if (ent != null)
            {
                ent.Description = e.NewValues["Description"] != null ? e.NewValues["Description"].ToString() : "";
                ent.DocumentType = e.NewValues["DocumentType"] != null ? e.NewValues["DocumentType"].ToString() : "";
                ent.DocumentData = Session["ePTW_DocumentData"] != null ? (byte[])Session["ePTW_DocumentData"] : null;
                ent.FileType = Session["ePTW_DocumentExt"] != null ? Session["ePTW_DocumentExt"].ToString() : "";
                ent.FileName = Session["ePTW_DocumentFileName"] != null ? Session["ePTW_DocumentFileName"].ToString() : "";
                ent.Updated = DateTime.Now;
                ent.UpdatedBy = user.UserID;

                DocumentViewModel.Document_InsertUpdate(ent);

                Session["ePTW_EQList"] = doclist;
                Session["ePTW_DocumentData"] = null;
                Session["ePTW_DocumentExt"] = null;
                Session["ePTW_DocumentFileName"] = null;
                Session["ePTW_DocumentID"] = null;
            }

            e.Cancel = true;
            gvDocuments.CancelEdit();
            gvDocuments.DataSource = doclist;
            gvDocuments.DataBind();
        }
        protected void ASPxUploadControl1_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            if (e.IsValid)
            {
                Session["ePTW_DocumentData"] = e.UploadedFile.FileBytes;
                string resultExtension = Path.GetExtension(e.UploadedFile.FileName);
                string resultFileName = e.UploadedFile.FileName;
                long sizeInKilobytes = e.UploadedFile.ContentLength / 1024;
                Session["ePTW_DocumentExt"] = resultExtension;
                Session["ePTW_DocumentFileName"] = resultFileName;
                e.CallbackData = e.UploadedFile.FileName;
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
        protected void lblFileName_Init(object sender, EventArgs e)
        {
            if (gvDocuments.FocusedRowIndex >= 0 && !gvDocuments.IsNewRowEditing)
            {
                int docid = (int)gvDocuments.GetRowValues(gvDocuments.FocusedRowIndex, "ID");
                if (docid >= 0 && Session["ePTW_DocumentID"] == null)
                {
                    DocumentModel data = DocumentViewModel.GetDocumentDetail(docid);
                    ASPxLabel lblFileName = sender as ASPxLabel;
                    GridViewDataItemTemplateContainer container = lblFileName.NamingContainer as GridViewDataItemTemplateContainer;
                    lblFileName.Text = data.FileName;
                    if (container.FindControl("btnRemove") != null)
                    {
                        container.FindControl("btnRemove").Visible = true;
                        ((ASPxButton)container.FindControl("btnRemove")).ClientVisible = true;
                    }
                    Session["ePTW_DocumentID"] = docid;
                    Session["ePTW_DocumentData"] = data.DocumentData;
                    Session["ePTW_DocumentExt"] = data.FileType;
                    Session["ePTW_DocumentFileName"] = data.FileName;
                }
            }
        }
        protected void gvDocuments_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            Session["ePTW_DocumentID"] = null;
            Session["ePTW_DocumentData"] = null;
            Session["ePTW_DocumentExt"] = null;
            Session["ePTW_DocumentFileName"] = null;
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            ASPxButton btnDownload = sender as ASPxButton;
            GridViewDataItemTemplateContainer container = btnDownload.NamingContainer as GridViewDataItemTemplateContainer;
            int docid = (int)container.KeyValue;
            DocumentModel data = DocumentViewModel.GetDocumentDetail(docid);

            if (data.DocumentData != null)
            {
                Response.ContentType = "application/octet-stream";
                Response.Clear();
                Response.BufferOutput = true;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + data.FileName);
                Response.BinaryWrite(data.DocumentData);
                Response.Flush();
                // This would be the ideal spot to collect some download statistics and / or tracking  
                // also, you could implement other requests, such as delete the file after download  

            }
        }
    }
}