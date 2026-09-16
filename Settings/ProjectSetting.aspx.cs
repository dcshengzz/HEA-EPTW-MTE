using DevExpress.Web;
using HEA.ePTW.Class;
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
    public partial class ProjectSetting : System.Web.UI.Page
    {
        List<ProjectModel> list;
        List<ConstructorModel> constructorlist;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    UserModel user = UserViewModel.GetLoggedInUserInfo();
                    constructorlist = ConstructorViewModel.GetConstructorsList(user.UserID);
                    Session["ePTW_ConstructorList"] = constructorlist;
                    list = ProjectViewModel.GetProjectSettingList(user.UserID);
                    Session["ePTW_Projectlist"] = list;
                }
                else
                {
                    constructorlist = (Session["ePTW_ConstructorList"] as List<ConstructorModel>);
                    list = (Session["ePTW_Projectlist"] as List<ProjectModel>);
                }

                gvProjects.DataSource = list;
                gvProjects.DataBind();
            }
            catch
            {
                Response.Redirect("~/Account/SignIn.aspx");
            }
        }

        protected void gvProjects_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            ASPxGridView gridView = sender as ASPxGridView;
            if (gridView.IsEditing && e.Column.FieldName == "ConstructorName")
            {
                ASPxComboBox cbConstructor = e.Editor as ASPxComboBox;
                cbConstructor.TextField = "Name";
                cbConstructor.ValueField = "Name";
                cbConstructor.DataSource = constructorlist;
                cbConstructor.DataBind();
            }
            if (gridView.IsEditing && e.Column.FieldName == "ApproverUserID")
            {
                ASPxComboBox editor = (ASPxComboBox)e.Editor;
                editor.DataSource = ProjectViewModel.GetTBMApproverCandidates();
                editor.TextField = "FullName";
                editor.ValueField = "UserID";
                editor.DataBind();
            }
            if (!gridView.IsNewRowEditing)
            {
                if (e.Column.FieldName == "Name")
                {
                    ((ASPxTextBox)e.Editor).Enabled = false;
                }
            }
            if (e.Column.FieldName == "Photo")
            {
                ((ASPxBinaryImage)e.Editor).Width = 250;
                ((ASPxBinaryImage)e.Editor).Height = 250;
            }
            if (e.Column.FieldName == "Address")
            {
                ((ASPxMemo)e.Editor).Height = 100;
            }
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
        protected void gvProjects_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            string approverID = e.NewValues["ApproverUserID"] == null ? "" : e.NewValues["ApproverUserID"].ToString();
            if (string.IsNullOrWhiteSpace(approverID) ||
                !ProjectViewModel.GetTBMApproverCandidates().Any(item => item.UserID == approverID))
                AddError(e.Errors, gvProjects.Columns["ApproverUserID"], "Select an active user with the PTW APPROVER role.");
            if (e.IsNewRow)
            {
                if (e.NewValues["Name"] == null || e.NewValues["Name"].ToString() == "")
                    AddError(e.Errors, gvProjects.Columns["Name"], "Please enter the Team name.");
                else
                {
                    var result = ProjectViewModel.GetProjectDetails(e.NewValues["Name"].ToString());
                    if (result != null) AddError(e.Errors, gvProjects.Columns["Name"], "This Team already exists in the system.");
                }
            }
            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
                e.RowError = "Please, correct all errors.";
        }
        protected void gvProjects_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            ProjectModel ent = new ProjectModel();

            ent.Name = e.NewValues["Name"].ToString().Trim();
            ent.ApproverUserID = e.NewValues["ApproverUserID"].ToString();
            ent.Address = "";
            ent.Description = "";
            ent.ConstructorName = user.ConstructorName ?? "";
            ent.StartDate = DateTime.Now.Date;
            ent.EndDate = DateTime.Now.Date.AddYears(1);
            ent.Created = DateTime.Now;
            ent.Updated = DateTime.Now;
            ent.CreatedBy = user.UserID;
            ent.UpdatedBy = user.UserID;
            ent.Status = 1;
            list.Add(ent);

            Session["ePTW_Projectlist"] = list;

            ProjectViewModel.Project_InsertUpdate(ent);

            e.Cancel = true;
            gvProjects.CancelEdit();
            gvProjects.DataSource = list;
            gvProjects.DataBind();
        }
        protected void gvProjects_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            string strName = e.Keys["Name"].ToString();
            ProjectModel ent = list.FirstOrDefault(item => item.Name == strName);

            if (ent != null)
            {
                ent.ApproverUserID = e.NewValues["ApproverUserID"].ToString();
                ent.Updated = DateTime.Now;
                ent.UpdatedBy = user.UserID;

                Session["ePTW_Projectlist"] = list;

                ProjectViewModel.Project_InsertUpdate(ent);

                e.Cancel = true;
                gvProjects.CancelEdit();
                gvProjects.DataSource = list;
                gvProjects.DataBind();
            }
        }
        protected void gvProjects_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            if (e.Parameters == "lock")
            {
                string strName = gvProjects.GetRowValues(gvProjects.FocusedRowIndex, "Name").ToString();
                if (strName != "")
                {
                    ProjectModel ent = list.FirstOrDefault(item => item.Name == strName);
                    if (ent != null)
                    {
                        ent.Status = 2;
                        ent.Updated = DateTime.Now;
                        ent.UpdatedBy = user.UserID;
                        ProjectViewModel.Project_InsertUpdate(ent);
                        Session["ePTW_Projectlist"] = list;
                        gvProjects.CancelEdit();
                        gvProjects.DataSource = list;
                        gvProjects.DataBind();
                    }
                }
            }
            if (e.Parameters == "unlock")
            {
                string strName = gvProjects.GetRowValues(gvProjects.FocusedRowIndex, "Name").ToString();
                if (strName != "")
                {
                    ProjectModel ent = list.FirstOrDefault(item => item.Name == strName);
                    if (ent != null)
                    {
                        ent.Status = 1;
                        ent.Updated = DateTime.Now;
                        ent.UpdatedBy = user.UserID;
                        ProjectViewModel.Project_InsertUpdate(ent);
                        Session["ePTW_Projectlist"] = list;
                        gvProjects.CancelEdit();
                        gvProjects.DataSource = list;
                        gvProjects.DataBind();
                    }
                }
            }
        }
        protected void gvProjects_BeforePerformDataSelect(object sender, EventArgs e)
        {
            var masterKey = (sender as ASPxGridView).GetMasterRowKeyValue();
            if (masterKey != null)
            {
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ASPxButton button = sender as ASPxButton;
            GridViewPreviewRowTemplateContainer container = button.NamingContainer as GridViewPreviewRowTemplateContainer;
            if (container != null)
            {
                int visibleIndex = container.VisibleIndex;
                object keyValue = gvProjects.GetRowValues(visibleIndex, gvProjects.KeyFieldName);

                gvProjects.FocusedRowIndex = visibleIndex;
                gvProjects.StartEdit(visibleIndex);
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ASPxButton button = sender as ASPxButton;
            GridViewPreviewRowTemplateContainer container = button.NamingContainer as GridViewPreviewRowTemplateContainer;
            if (container == null) return;

            object keyValue = gvProjects.GetRowValues(container.VisibleIndex, gvProjects.KeyFieldName);
            ProjectModel ent = keyValue == null ? null : list.FirstOrDefault(item => item.Name == keyValue.ToString());
            if (ent == null) return;

            UserModel user = UserViewModel.GetLoggedInUserInfo();
            ent.Status = 97;
            ent.Updated = DateTime.Now;
            ent.UpdatedBy = user.UserID;
            ProjectViewModel.Project_InsertUpdate(ent);
            list.Remove(ent);
            Session["ePTW_Projectlist"] = list;
            gvProjects.CancelEdit();
            gvProjects.DataSource = list;
            gvProjects.DataBind();
        }
        protected void btnLock_Click(object sender, EventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            ASPxButton button = sender as ASPxButton;
            GridViewPreviewRowTemplateContainer container = button.NamingContainer as GridViewPreviewRowTemplateContainer;
            if (container != null)
            {
                int visibleIndex = container.VisibleIndex;
                object keyValue = gvProjects.GetRowValues(visibleIndex, gvProjects.KeyFieldName);

                if (keyValue.ToString() != "")
                {
                    ProjectModel ent = list.FirstOrDefault(item => item.Name == keyValue.ToString());
                    if (ent != null)
                    {
                        ent.Status = 2;
                        ent.Updated = DateTime.Now;
                        ent.UpdatedBy = user.UserID;
                        ProjectViewModel.Project_InsertUpdate(ent);
                        Session["ePTW_Projectlist"] = list;
                        gvProjects.CancelEdit();
                        gvProjects.DataSource = list;
                        gvProjects.DataBind();
                    }
                }
            }
        }
        protected void btnUnlock_Click(object sender, EventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            ASPxButton button = sender as ASPxButton;
            GridViewPreviewRowTemplateContainer container = button.NamingContainer as GridViewPreviewRowTemplateContainer;
            if (container != null)
            {
                int visibleIndex = container.VisibleIndex;
                object keyValue = gvProjects.GetRowValues(visibleIndex, gvProjects.KeyFieldName);

                if (keyValue.ToString() != "")
                {
                    ProjectModel ent = list.FirstOrDefault(item => item.Name == keyValue.ToString());
                    if (ent != null)
                    {
                        ent.Status = 1;
                        ent.Updated = DateTime.Now;
                        ent.UpdatedBy = user.UserID;
                        ProjectViewModel.Project_InsertUpdate(ent);
                        Session["ePTW_Projectlist"] = list;
                        gvProjects.CancelEdit();
                        gvProjects.DataSource = list;
                        gvProjects.DataBind();
                    }
                }
            }
        }
    }
}
