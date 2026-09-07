using ClosedXML.Excel;
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
    public partial class EquipmentSetting : System.Web.UI.Page
    {
        
        List<EquipmentModel> equipmentlist;
        protected void Page_Load(object sender, EventArgs e)
        {
            string strProject = UserViewModel.GetSelectedProject();
            lblTitle.Text = "Equipment List - " + strProject;
            if (!IsPostBack)
            {
                //string strProject = UserViewModel.GetSelectedProject();
                equipmentlist = EquipmentViewModel.GetEquipmentList(strProject);
                Session["ePTW_EQList"] = equipmentlist;
            }
            else
            {
                equipmentlist = (Session["ePTW_EQList"] as List<EquipmentModel>);
            }

            gvEquipment.DataSource = equipmentlist;
            gvEquipment.DataBind();
            gvEquipment.FocusedRowIndex = -1;
        }
        private void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }

        protected void gvEquipment_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            ASPxGridView gridView = sender as ASPxGridView;
            if (gridView.IsEditing && e.Column.FieldName == "EquipmentType")
            {
                List<EquipmentTypeModel> list = EquipmentTypeViewModel.GetEquipmentTypeList();
                ASPxComboBox cbConstructor = e.Editor as ASPxComboBox;

                cbConstructor.TextField = "EquipmentType";
                cbConstructor.ValueField = "EquipmentType";
                cbConstructor.DataSource = list;
                cbConstructor.DataBind();
            }

            if (e.Column.FieldName == "Photo")
            {
                ((ASPxBinaryImage)e.Editor).Width = 250;
                ((ASPxBinaryImage)e.Editor).Height = 250;
            }
            if (e.Column.FieldName == "Document")
            {
                ((ASPxBinaryImage)e.Editor).Width = 250;
                ((ASPxBinaryImage)e.Editor).Height = 250;
            }

        }
        protected void gvEquipment_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            if (e.NewValues["RegistrationNo"] == null || e.NewValues["RegistrationNo"].ToString() == "")
                AddError(e.Errors, gvEquipment.Columns["RegistrationNo"], "Please enter the Equipment Registration Number.");
            if (e.NewValues["EquipmentName"] == null || e.NewValues["EquipmentName"].ToString() == "")
                AddError(e.Errors, gvEquipment.Columns["EquipmentName"], "Please enter the Equipment Name.");
            if (e.NewValues["EquipmentType"] == null || e.NewValues["EquipmentType"].ToString() == "")
                AddError(e.Errors, gvEquipment.Columns["EquipmentType"], "Please select the Equipment Type.");
            if (e.NewValues["Description"] == null || e.NewValues["Description"].ToString() == "")
                AddError(e.Errors, gvEquipment.Columns["Description"], "Please enter the Equipment Description.");
            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
                e.RowError = "Please, correct all errors.";
        }
        protected void gvEquipment_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string strProject = UserViewModel.GetSelectedProject();
            UserModel user = UserViewModel.GetLoggedInUserInfo();
            EquipmentModel ent = new EquipmentModel();

            ent.RegistrationNo = e.NewValues["RegistrationNo"] != null ? e.NewValues["RegistrationNo"].ToString() : "";
            ent.EquipmentName = e.NewValues["EquipmentName"] != null ? e.NewValues["EquipmentName"].ToString() : "";
            ent.EquipmentType = e.NewValues["EquipmentType"] != null ? e.NewValues["EquipmentType"].ToString() : "";
            ent.Description = e.NewValues["Description"] != null ? e.NewValues["Description"].ToString() : "";
            ent.ProjectName = strProject;
            //ent.ProjectID = UserViewModel.GetSelectedProject();
            ent.Photo = e.NewValues["Photo"] != null ? (byte[])e.NewValues["Photo"] : null;
            ent.Document = e.NewValues["Document"] != null ? (byte[])e.NewValues["Document"] : null;
            ent.Created = DateTime.Now;
            ent.Updated = DateTime.Now;
            ent.CreatedBy = user.UserID;
            ent.UpdatedBy = user.UserID;
            ent.Status = 1;

            equipmentlist.Add(ent);
            Session["ePTW_EQList"] = equipmentlist;

            EquipmentViewModel.Equipment_InsertUpdate(ent);

            e.Cancel = true;
            gvEquipment.CancelEdit();
            gvEquipment.DataSource = equipmentlist;
            gvEquipment.DataBind();
        }
        protected void gvEquipment_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string regno = e.Keys["RegistrationNo"].ToString();
            UserModel user = UserViewModel.GetLoggedInUserInfo();
            EquipmentModel ent = equipmentlist.Find(item => item.RegistrationNo == regno);

            if (ent != null)
            {
                ent.EquipmentName = e.NewValues["EquipmentName"] != null ? e.NewValues["EquipmentName"].ToString() : "";
                ent.EquipmentType = e.NewValues["EquipmentType"] != null ? e.NewValues["EquipmentType"].ToString() : "";
                ent.Description = e.NewValues["Description"] != null ? e.NewValues["Description"].ToString() : "";
                ent.Photo = e.NewValues["Photo"] != null ? (byte[])e.NewValues["Photo"] : null;
                ent.Document = e.NewValues["Document"] != null ? (byte[])e.NewValues["Document"] : null;
                ent.Updated = DateTime.Now;
                ent.UpdatedBy = user.UserID;

                EquipmentViewModel.Equipment_InsertUpdate(ent);

                Session["ePTW_EQList"] = equipmentlist;
            }

            e.Cancel = true;
            gvEquipment.CancelEdit();
            gvEquipment.DataSource = equipmentlist;
            gvEquipment.DataBind();
        }

    }
}