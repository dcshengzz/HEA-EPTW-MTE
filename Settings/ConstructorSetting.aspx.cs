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
    public partial class ContructorSetting : System.Web.UI.Page
    {
        List<ConstructorModel> list;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserModel user = UserViewModel.GetLoggedInUserInfo();
                list = ConstructorViewModel.GetConstructorsList(user.UserID);
                Session["list"] = list;
                gvConstructors.DataSource = list;
                gvConstructors.DataBind();
            }
            else
            {
                list = (Session["list"] as List<ConstructorModel>);
                gvConstructors.DataSource = list;
                gvConstructors.DataBind();
            }
        }
        protected void gvConstructors_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (!gvConstructors.IsNewRowEditing)
            {
                if (e.Column.FieldName == "Name")
                {
                    ((ASPxTextBox)e.Editor).Enabled = false;
                }
            }
            if (e.Column.FieldName == "Description")
            {
                ((ASPxMemo)e.Editor).Height = 100;
            }
            if (e.Column.FieldName == "Address")
            {
                ((ASPxMemo)e.Editor).Height = 100;
            }
        }
        protected void gvConstructors_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            if (e.IsNewRow)
            {
                var result = ConstructorViewModel.GetConstructorRecord(e.NewValues["Name"].ToString());
                if (result != null) AddError(e.Errors, gvConstructors.Columns["Name"], "The Contructor is exist.");
                if (e.NewValues["Name"] == null || e.NewValues["Name"].ToString() == "")
                    AddError(e.Errors, gvConstructors.Columns["Name"], "Constructor Name cannot be null.");
            }
            if (e.NewValues["Description"] == null || e.NewValues["Description"].ToString() == "")
                AddError(e.Errors, gvConstructors.Columns["Description"], "Description cannot be null.");
            if (e.NewValues["Address"] == null || e.NewValues["Address"].ToString() == "")
                AddError(e.Errors, gvConstructors.Columns["Address"], "Address cannot be null.");
            if (e.NewValues["ContactPerson"] == null || e.NewValues["ContactPerson"].ToString() == "")
                AddError(e.Errors, gvConstructors.Columns["ContactPerson"], "Contact Person cannot be null.");
            if (e.NewValues["ContactNumber"] == null || e.NewValues["ContactNumber"].ToString() == "")
                AddError(e.Errors, gvConstructors.Columns["ContactNumber"], "Contact tNumber cannot be null.");            
            //foreach (GridViewColumn column in gvConstructors.Columns)
            //{
            //    GridViewDataColumn dataColumn = column as GridViewDataColumn;
            //    if (dataColumn == null) continue;
            //    if (e.NewValues[dataColumn.FieldName] == null)
            //        e.Errors[dataColumn] = "Value cannot be null.";
            //}
            //e.RowError = "";
            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
                e.RowError = "Please, correct all errors.";
        }
        private void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
        protected void gvConstructors_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            ConstructorModel ent = new ConstructorModel();
            ent.Name = e.NewValues["Name"] != null ? e.NewValues["Name"].ToString() : "";
            ent.Description = e.NewValues["Description"] != null ? e.NewValues["Description"].ToString() : "";
            ent.Address = e.NewValues["Address"] != null ? e.NewValues["Address"].ToString() : "";
            ent.ContactPerson = e.NewValues["ContactPerson"] != null ? e.NewValues["ContactPerson"].ToString() : "";
            ent.ContactNumber = e.NewValues["ContactNumber"] != null ? e.NewValues["ContactNumber"].ToString() : "";
            ent.Created = DateTime.Now;
            ent.Updated = DateTime.Now;
            ent.CreatedBy = user.UserID;
            ent.UpdatedBy = user.UserID;
            ent.Status = 1;
            list.Add(ent);
            Session["list"] = list;

            ConstructorViewModel.ConstructorsInsertUpdate(ent);

            e.Cancel = true;
            gvConstructors.CancelEdit();

            gvConstructors.DataSource = list;
            gvConstructors.DataBind();
        }
        protected void gvConstructors_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            string name = e.Keys["Name"].ToString();
            ConstructorModel result = list.Find(item => item.Name == name);
            if (result != null)
            {
                result.Name = name.ToString();
                result.Description = e.NewValues["Description"].ToString();
                result.Address = e.NewValues["Address"].ToString();
                result.ContactPerson = e.NewValues["ContactPerson"].ToString();
                result.ContactNumber = e.NewValues["ContactNumber"].ToString();
                result.UpdatedBy = user.UserID;
                result.Updated = DateTime.Now;
                ConstructorViewModel.ConstructorsInsertUpdate(result);
                Session["list"] = list;
            }
            e.Cancel = true;
            gvConstructors.CancelEdit();
            gvConstructors.DataSource = list;
            gvConstructors.DataBind();
        }
        protected void gvConstructors_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "lock")
            {
                string name = gvConstructors.GetRowValues(gvConstructors.FocusedRowIndex, "Name").ToString();
                ConstructorModel result = list.Find(item => item.Name == name);
                if (result != null)
                {
                    result.Status = 2;
                    ConstructorViewModel.ConstructorsInsertUpdate(result);
                    Session["list"] = list;
                }
                gvConstructors.DataSource = list;
                gvConstructors.DataBind();
            }
            if (e.Parameters == "unlock")
            {
                string name = gvConstructors.GetRowValues(gvConstructors.FocusedRowIndex, "Name").ToString();
                ConstructorModel result = list.Find(item => item.Name == name);
                if (result != null)
                {
                    result.Status = 1;
                    ConstructorViewModel.ConstructorsInsertUpdate(result);
                    Session["list"] = list;
                }
                gvConstructors.DataSource = list;
                gvConstructors.DataBind();
            }
        }

        //protected void gvProjects_BeforePerformDataSelect(object sender, EventArgs e)
        //{
        //    var masterKey = (sender as ASPxGridView).GetMasterRowKeyValue();
        //    //Session["TravelPlanNumber"] = masterKey;
        //    //if (masterKey != null)
        //    //{
        //    //    ObservableCollection<GeneralClaimMasterENT> list = new ObservableCollection<GeneralClaimMasterENT>();
        //    //    ASPxGridView gvDetails = (sender as ASPxGridView);
        //    //    list = TravelPlanDAL.GetGeneralClaimList(masterKey.ToString());
        //    //    gvDetails.DataSource = list;
        //    //}
        //}
    }
}