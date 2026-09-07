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
    public partial class EquipmentTypeSetting : System.Web.UI.Page
    {
        List<EquipmentTypeModel> eqtypeList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                eqtypeList = EquipmentTypeViewModel.GetEquipmentTypeList();
                Session["ePTW_EqTypeList"] = eqtypeList;
            }
            else
            {
                eqtypeList = (Session["ePTW_EqTypeList"] as List<EquipmentTypeModel>);
            }

            gvEquipmentType.DataSource = eqtypeList;
            gvEquipmentType.DataBind();
            gvEquipmentType.FocusedRowIndex = -1;
        }
        private void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }

        protected void gvEquipmentType_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "Description")
            {
                ((ASPxMemo)e.Editor).Height = 100;
            }
        }
        protected void gvEquipmentType_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            if (e.NewValues["CategoryID"] == null || e.NewValues["CategoryID"].ToString() == "")
                AddError(e.Errors, gvEquipmentType.Columns["CategoryID"], "Please enter the Equipment Category.");
            if (e.NewValues["EquipmentType"] == null || e.NewValues["EquipmentType"].ToString() == "")
                AddError(e.Errors, gvEquipmentType.Columns["EquipmentType"], "Please enter the Equipment Type.");
            if (e.NewValues["Description"] == null || e.NewValues["Description"].ToString() == "")
                AddError(e.Errors, gvEquipmentType.Columns["Description"], "Please enter the Description.");
            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
                e.RowError = "Please, correct all errors.";
        }
        protected void gvEquipmentType_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();
            EquipmentTypeModel ent = new EquipmentTypeModel();
            ent.CategoryID = e.NewValues["CategoryID"] != null ? e.NewValues["CategoryID"].ToString() : "";
            ent.EquipmentType = e.NewValues["EquipmentType"] != null ? e.NewValues["EquipmentType"].ToString() : "";
            ent.Description = e.NewValues["Description"] != null ? e.NewValues["Description"].ToString() : "";
            ent.Status = 1;
            ent.Created = DateTime.Now;
            ent.CreatedBy = user.UserID;
            ent.Updated = DateTime.Now;
            ent.UpdatedBy = user.UserID;

            eqtypeList.Add(ent);
            Session["ePTW_UsersList"] = eqtypeList;

            EquipmentTypeViewModel.EquipmentType_InsertUpdate(ent);

            e.Cancel = true;
            gvEquipmentType.CancelEdit();
            gvEquipmentType.DataSource = eqtypeList;
            gvEquipmentType.DataBind();
        }
        protected void gvEquipmentType_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string type = e.Keys["EquipmentType"].ToString();
            UserModel user = UserViewModel.GetLoggedInUserInfo();
            EquipmentTypeModel ent = eqtypeList.Find(item => item.EquipmentType == type);

            if (ent != null)
            {
                ent.CategoryID = e.NewValues["CategoryID"].ToString();
                ent.EquipmentType = e.NewValues["EquipmentType"].ToString();
                ent.Description = e.NewValues["Description"].ToString();
                ent.Status = 1;
                ent.Created = DateTime.Now;
                ent.CreatedBy = user.UserID;
                ent.Updated = DateTime.Now;
                ent.UpdatedBy = user.UserID;

                EquipmentTypeViewModel.EquipmentType_InsertUpdate(ent);

                Session["ePTW_UsersList"] = eqtypeList;
            }

            e.Cancel = true;
            gvEquipmentType.CancelEdit();
            gvEquipmentType.DataSource = eqtypeList;
            gvEquipmentType.DataBind();
        }

        protected void gvEquipmentType_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "lock")
            {
                string strtype = gvEquipmentType.GetRowValues(gvEquipmentType.FocusedRowIndex, "EquipmentType").ToString();
                if (strtype != "")
                {
                    EquipmentTypeViewModel.Equipment_LockUnlock(strtype, 2);
                    EquipmentTypeModel ent = eqtypeList.Find(item => item.EquipmentType == strtype);

                    if (ent != null)
                    {
                        ent.Status = 2;
                        Session["ePTW_EqTypeList"] = eqtypeList;
                    }

                    gvEquipmentType.CancelEdit();
                    gvEquipmentType.DataSource = eqtypeList;
                    gvEquipmentType.DataBind();
                }
            }
            if (e.Parameters == "unlock")
            {
                string strtype = gvEquipmentType.GetRowValues(gvEquipmentType.FocusedRowIndex, "EquipmentType").ToString();
                if (strtype != "")
                {
                    EquipmentTypeViewModel.Equipment_LockUnlock(strtype, 1);
                    EquipmentTypeModel ent = eqtypeList.Find(item => item.EquipmentType == strtype);

                    if (ent != null)
                    {
                        ent.Status = 1;
                        Session["ePTW_UsersList"] = eqtypeList;
                    }

                    gvEquipmentType.CancelEdit();
                    gvEquipmentType.DataSource = eqtypeList;
                    gvEquipmentType.DataBind();
                }
            }
        }
    }
}