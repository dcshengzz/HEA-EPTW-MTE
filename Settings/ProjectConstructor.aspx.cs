using DevExpress.Data.Filtering;
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
    public partial class ProjectConstructor : System.Web.UI.Page
    {
        List<ProjectModel> list;
        List<ProjectConstructorModel> projectconstructorlist;
        List<ProjectStaffModel> projectstafflist;
        List<ProjectEquipmentModel> projectequipmentlist;
        List<AttachmentModel> attachmentlist;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    UserModel user = UserViewModel.GetLoggedInUserInfo();
                    list = ProjectViewModel.GetProjectSettingList(user.UserID);
                    Session["ePTW_Projectlist"] = list;
                }
                else
                {
                    list = (Session["ePTW_Projectlist"] as List<ProjectModel>);
                }

                gvProjects.DataSource = list;
                gvProjects.DataBind();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Account/SignIn.aspx");
            }
        }




        







        private void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }


        protected void gvConstructor_BeforePerformDataSelect(object sender, EventArgs e)
        {
            var strProjectName = (sender as ASPxGridView).GetMasterRowKeyValue();
            Session["ProjectConstructor"] = strProjectName;
            UserModel ent = UserViewModel.GetLoggedInUserInfo();
            if (strProjectName != null)
            {
                projectconstructorlist = ProjectConstructorViewModel.GetProjectConstructorsList(strProjectName.ToString());
                ((ASPxGridView)sender).DataSource = projectconstructorlist;
            }
        }
        protected void gvConstructor_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            ASPxGridView gridView = sender as ASPxGridView;
            if (gridView.IsEditing && e.Column.FieldName == "ConstructorName")
            {
                ASPxComboBox cbConstructor = e.Editor as ASPxComboBox;
                List<ConstructorModel> constlist = ConstructorViewModel.GetConstructor_CodeTable();
                cbConstructor.DataSource = constlist;
                cbConstructor.TextField = "Name";
                cbConstructor.ValueField = "Name";
                cbConstructor.DataBind();
            }
            if (gridView.IsEditing && e.Column.FieldName == "Description")
            {
                ASPxTextBox txtTemp = e.Editor as ASPxTextBox;
                txtTemp.ReadOnly = true;
            }
        }
        protected void gvConstructor_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ASPxGridView tempGrid = (ASPxGridView)sender;
            if (e.NewValues["ConstructorName"] == null || e.NewValues["ConstructorName"].ToString() == "")
                AddError(e.Errors, tempGrid.Columns["ConstructorName"], "Please select the Constructor.");

            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
                e.RowError = "Please, correct all errors.";
        }
        protected void gvConstructor_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView tempGrid = (ASPxGridView)sender;
            UserModel user = UserViewModel.GetLoggedInUserInfo();
            ProjectConstructorModel ent = new ProjectConstructorModel();
            ent.ProjectName = Session["ProjectConstructor"].ToString();
            ent.ConstructorName = e.NewValues["ConstructorName"] != null ? e.NewValues["ConstructorName"].ToString() : "";
            ent.Description = e.NewValues["Description"] != null ? e.NewValues["Description"].ToString() : "";
            ent.Created = DateTime.Now;
            ent.Updated = DateTime.Now;
            ent.CreatedBy = user.UserID;
            ent.UpdatedBy = user.UserID;
            ent.Status = 1;
            ProjectConstructorViewModel.ProjectConstructor_InsertUpdate(ent);
            projectconstructorlist.Add(ent);

            e.Cancel = true;
            tempGrid.CancelEdit();
            tempGrid.DataSource = projectconstructorlist;
        }
        protected void gvConstructor_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "delete")
            {
                ASPxGridView tempGrid = (ASPxGridView)sender;
                string strConstructorName = tempGrid.GetRowValues(tempGrid.FocusedRowIndex, "ConstructorName").ToString();
                string strProject = Session["ProjectConstructor"].ToString();
                if (strConstructorName != "")
                {
                    ProjectConstructorViewModel.ProjectConstructor_Delete(strProject, strConstructorName);
                    ProjectConstructorModel ent = projectconstructorlist.FirstOrDefault(item => item.ConstructorName == strConstructorName && item.ProjectName == strProject);
                    projectconstructorlist.Remove(ent);

                    tempGrid.DataSource = projectconstructorlist;
                    tempGrid.DataBind();
                }
            }
        }



        protected void gvStaff_BeforePerformDataSelect(object sender, EventArgs e)
        {
            var strProjectName = (sender as ASPxGridView).GetMasterRowKeyValue();
            Session["ProjectConstructor"] = strProjectName;
            UserModel ent = UserViewModel.GetLoggedInUserInfo();
            if (strProjectName != null)
            {
                projectstafflist = ProjectStaffViewModel.GetProjectStaffList(ent.UserID, strProjectName.ToString());
                ((ASPxGridView)sender).DataSource = projectstafflist;
            }
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
        protected void gvStaff_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            ASPxGridView gridView = sender as ASPxGridView;
            if (gridView.IsEditing && e.Column.FieldName == "UserID")
            {
                ASPxComboBox cbStaff = e.Editor as ASPxComboBox;
                cbStaff.DropDownStyle = DropDownStyle.DropDownList;
                cbStaff.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                cbStaff.EnableCallbackMode = true;
                cbStaff.CustomFiltering += cbAttendees_CustomFiltering;

                List<UserModel> eqlist = UserViewModel.GetUser_UserListForMainCons();
                cbStaff.DataSource = eqlist;
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
            ProjectStaffModel ent = new ProjectStaffModel();
            ent.ProjectName = Session["ProjectConstructor"].ToString();
            ent.UserID = e.NewValues["UserID"] != null ? e.NewValues["UserID"].ToString() : "";
            ent.FullName = e.NewValues["FullName"] != null ? e.NewValues["FullName"].ToString() : "";
            ent.Position = e.NewValues["Position"] != null ? e.NewValues["Position"].ToString() : "";
            ent.Created = DateTime.Now;
            ent.Updated = DateTime.Now;
            ent.CreatedBy = user.UserID;
            ent.UpdatedBy = user.UserID;
            ent.Status = 1;
            ProjectStaffViewModel.ProjectStaff_InsertUpdate(ent);
            projectstafflist.Add(ent);

            e.Cancel = true;
            tempGrid.CancelEdit();
            tempGrid.DataSource = projectstafflist;
        }
        protected void gvStaff_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "delete")
            {
                ASPxGridView tempGrid = (ASPxGridView)sender;
                string strUser = tempGrid.GetRowValues(tempGrid.FocusedRowIndex, "UserID").ToString();
                string strProject = Session["ProjectConstructor"].ToString();
                if (strUser != "")
                {
                    ProjectStaffViewModel.ProjectStaff_Delete(strUser, strProject);
                    ProjectStaffModel ent = projectstafflist.FirstOrDefault(item => item.UserID == strUser && item.ProjectName == strProject);
                    projectstafflist.Remove(ent);

                    tempGrid.DataSource = projectstafflist;
                    tempGrid.DataBind();
                }
            }
        }


        protected void gvEquipment_BeforePerformDataSelect(object sender, EventArgs e)
        {
            var strProjectName = (sender as ASPxGridView).GetMasterRowKeyValue();
            Session["ProjectConstructor"] = strProjectName;
            UserModel ent = UserViewModel.GetLoggedInUserInfo();
            if (strProjectName != null)
            {
                projectequipmentlist = ProjectEquipmentViewModel.GetProjectEquipmentList(ent.UserID, strProjectName.ToString());
                ((ASPxGridView)sender).DataSource = projectequipmentlist;
            }
        }
        protected void gvEquipment_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            ASPxGridView gridView = sender as ASPxGridView;
            if (gridView.IsEditing && e.Column.FieldName == "RegistrationNo")
            {
                //ASPxComboBox cbEquipment = e.Editor as ASPxComboBox;
                ////List<EquipmentModel> eqlist = EquipmentViewModel.GetEquipment_CodeTable();
                //cbEquipment.DataSource = eqlist;
                //cbEquipment.TextField = "RegistrationNo";
                //cbEquipment.ValueField = "RegistrationNo";
                //cbEquipment.DataBind();
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

            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
                e.RowError = "Please, correct all errors.";
        }
        protected void gvEquipment_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView tempGrid = (ASPxGridView)sender;
            UserModel user = UserViewModel.GetLoggedInUserInfo();
            ProjectEquipmentModel ent = new ProjectEquipmentModel();
            ent.ProjectName = Session["ProjectConstructor"].ToString();
            ent.EquipmentName = e.NewValues["EquipmentName"] != null ? e.NewValues["EquipmentName"].ToString() : "";
            ent.RegistrationNo = e.NewValues["RegistrationNo"] != null ? e.NewValues["RegistrationNo"].ToString() : "";
            ent.EquipmentType = e.NewValues["EquipmentType"] != null ? e.NewValues["EquipmentType"].ToString() : "";
            ent.Created = DateTime.Now;
            ent.Updated = DateTime.Now;
            ent.CreatedBy = user.UserID;
            ent.UpdatedBy = user.UserID;
            ent.Status = 1;
            ProjectEquipmentViewModel.ProjectEquipment_InsertUpdate(ent);
            projectequipmentlist.Add(ent);

            e.Cancel = true;
            tempGrid.CancelEdit();
            tempGrid.DataSource = projectequipmentlist;
        }
        protected void gvEquipment_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "delete")
            {
                ASPxGridView tempGrid = (ASPxGridView)sender;
                string strName = tempGrid.GetRowValues(tempGrid.FocusedRowIndex, "EquipmentName").ToString();
                string strProject = Session["ProjectConstructor"].ToString();
                if (strName != "")
                {
                    ProjectEquipmentViewModel.ProjectEquipment_Delete(strName, strProject);
                    ProjectEquipmentModel ent = projectequipmentlist.FirstOrDefault(item => item.EquipmentName == strName && item.ProjectName == strProject);
                    projectequipmentlist.Remove(ent);

                    tempGrid.DataSource = projectequipmentlist;
                    tempGrid.DataBind();
                }
            }
        }


        protected void gvPhoto_BeforePerformDataSelect(object sender, EventArgs e)
        {
            var ProjectName = Session["ProjectConstructor"];
            if (ProjectName != null)
            {
                UserModel ent = UserViewModel.GetLoggedInUserInfo();
                attachmentlist = AttachmentViewModel.GetAttachmentList(ProjectName.ToString());
                ((ASPxCardView)sender).DataSource = attachmentlist;
            }
        }
        protected void gvImage_CardValidating(object sender, ASPxCardViewDataValidationEventArgs e)
        {
            ASPxCardView tempGrid = (ASPxCardView)sender;
            if (e.NewValues["Document"] == null)
            {
                e.Errors[(CardViewColumn)tempGrid.Columns["Document"]] = "Please upload the Image.";
            }
            // Specifies an error message for a card.
            if (e.Errors.Count > 0) e.CardError = "Please, fill all fields.";
        }
        protected void gvImage_CardInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxCardView tempGrid = (ASPxCardView)sender;
            UserModel user = UserViewModel.GetLoggedInUserInfo();
            AttachmentModel ent = new AttachmentModel();
            ent.Document = e.NewValues["Document"] != null ? ImageResize.ReduceImageSize((byte[])e.NewValues["Document"], 90) : null;
            ent.Status = 1;
            ent.FileType = "IMG";
            ent.FileName = Guid.NewGuid().ToString() + ".jpg";
            ent.Key = Session["ProjectConstructor"].ToString();
            ent.Created = DateTime.Now;
            ent.Updated = DateTime.Now;
            ent.CreatedBy = user.UserID;
            ent.UpdatedBy = user.UserID;
            AttachmentViewModel.Attachment_InsertUpdate(ent);
            attachmentlist.Add(ent);

            e.Cancel = true;
            tempGrid.CancelEdit();
            tempGrid.DataSource = attachmentlist;
        }
        protected void gvImage_CardUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            var id = e.Keys["ID"];
            if (id != null)
            {
                int intID = Convert.ToInt32(id);
                ASPxCardView tempGrid = (ASPxCardView)sender;
                UserModel user = UserViewModel.GetLoggedInUserInfo();
                var attach = attachmentlist.FirstOrDefault(item => item.ID == intID);
                if (attach != null)
                {
                    attach.Document = e.NewValues["Document"] != null ? ImageResize.ReduceImageSize((byte[])e.NewValues["Document"], 90) : null;
                    attach.Created = DateTime.Now;
                    attach.Updated = DateTime.Now;
                    attach.CreatedBy = user.UserID;
                    attach.UpdatedBy = user.UserID;
                    AttachmentViewModel.Attachment_InsertUpdate(attach);

                    e.Cancel = true;
                    tempGrid.CancelEdit();
                    tempGrid.DataSource = attachmentlist;
                }
            }
        }
        protected void gvImage_CardDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var id = e.Keys["ID"];
            if (id != null)
            {
                int intID = Convert.ToInt32(id);
                ASPxCardView tempGrid = (ASPxCardView)sender;
                var attach = attachmentlist.FirstOrDefault(item => item.ID == intID);
                if (attach != null)
                {
                    AttachmentViewModel.Attachment_Delete(intID);
                    e.Cancel = true;
                    tempGrid.CancelEdit();

                    attachmentlist.Remove(attach);
                    tempGrid.DataSource = attachmentlist;
                }
            }
        }


    }
}