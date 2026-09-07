using DevExpress.Web;
using HEA.ePTW.Models;
using HEA.ePTW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HEA.ePTW.Settings
{
    public partial class DocumentTypeSetting : System.Web.UI.Page
    {
        List<DocumentTypeModel> doctypeList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                doctypeList = DocumentTypeViewModel.GetDocumentTypeList();
                Session["ePTW_DocTypeList"] = doctypeList;
            }
            else
            {
                doctypeList = (Session["ePTW_DocTypeList"] as List<DocumentTypeModel>);
            }

            gvDocumentType.DataSource = doctypeList;
            gvDocumentType.DataBind();
            gvDocumentType.FocusedRowIndex = -1;
        }

        protected void gvDocumentType_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "Description")
            {
                ((ASPxMemo)e.Editor).Height = 100;
            }
        }
        private void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
        protected void gvDocumentType_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            if (e.NewValues["DocumentType"] == null || e.NewValues["DocumentType"].ToString() == "")
                AddError(e.Errors, gvDocumentType.Columns["DocumentType"], "Document Type cannot be null.");
            //else
            //{
            //    if (e.IsNewRow)
            //    {
            //        var result = ConstructorViewModel.GetConstructorRecord(e.NewValues["Name"].ToString());
            //        if (result != null) AddError(e.Errors, gvConstructors.Columns["Name"], "The Contructor is exist.");
            //    }
            //}
            if (e.NewValues["Description"] == null || e.NewValues["Description"].ToString() == "")
                AddError(e.Errors, gvDocumentType.Columns["Description"], "Description cannot be null.");
        }
        protected void gvDocumentType_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            DocumentTypeModel ent = new DocumentTypeModel();
            ent.DocumentType = e.NewValues["DocumentType"] != null ? e.NewValues["DocumentType"].ToString() : "";
            ent.Description = e.NewValues["Description"] != null ? e.NewValues["Description"].ToString() : "";
            ent.Created = DateTime.Now;
            ent.Updated = DateTime.Now;
            ent.CreatedBy = user.UserID;
            ent.UpdatedBy = user.UserID;
            ent.Status = 1;


            doctypeList.Add(ent);
            Session["ePTW_DocTypeList"] = doctypeList;

            DocumentTypeViewModel.DocumentTypeUpdate(ent);

            e.Cancel = true;
            gvDocumentType.CancelEdit();

            gvDocumentType.DataSource = doctypeList;
            gvDocumentType.DataBind();
        }
        protected void gvDocumentType_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            string type = e.Keys["DocumentType"].ToString();
            DocumentTypeModel result = doctypeList.First(item => item.DocumentType == type);
            if (result != null)
            {
                result.DocumentType = e.NewValues["DocumentType"].ToString();
                result.Description = e.NewValues["Description"].ToString();
                result.UpdatedBy = user.UserID;
                result.Updated = DateTime.Now;

                DocumentTypeViewModel.DocumentTypeUpdate(result);
                Session["ePTW_DocTypeList"] = doctypeList;
            }
            e.Cancel = true;

            gvDocumentType.CancelEdit();
            gvDocumentType.DataSource = doctypeList;
            gvDocumentType.DataBind();
        }

        protected void gvDocumentType_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "lock")
            {
                string type = gvDocumentType.GetRowValues(gvDocumentType.FocusedRowIndex, "DocumentType").ToString();
                DocumentTypeModel result = doctypeList.Find(item => item.DocumentType == type);
                if (result != null)
                {
                    result.Status = 2;
                    DocumentTypeViewModel.DocumentTypeUpdate(result);
                    Session["ePTW_DocTypeList"] = doctypeList;
                }
                gvDocumentType.DataSource = doctypeList;
                gvDocumentType.DataBind();
            }
            if (e.Parameters == "unlock")
            {
                string type = gvDocumentType.GetRowValues(gvDocumentType.FocusedRowIndex, "DocumentType").ToString();
                DocumentTypeModel result = doctypeList.Find(item => item.DocumentType == type);
                if (result != null)
                {
                    result.Status = 1;
                    DocumentTypeViewModel.DocumentTypeUpdate(result);
                    Session["ePTW_DocTypeList"] = doctypeList;
                }
                gvDocumentType.DataSource = doctypeList;
                gvDocumentType.DataBind();
            }
        }
    }
}