using DevExpress.Web;
using HEA.ePTW.Class;
using HEA.ePTW.Models;
using HEA.ePTW.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using System.Net.Mail;
using System.Web.Script.Serialization;

namespace HEA.ePTW.Settings
{
    public partial class UserSetting : System.Web.UI.Page
    {
        private static readonly string[] RoleCategoryNames = { "Applicant", "Approver", "Admin" };
        private static readonly string[] ApplicantRoleIds = { "PTW USER", "TBM USER", "CCP USER", "CKL USER" };
        private static readonly string[] ApproverRoleIds =
        {
            "PTW ASSESSOR", "PTW SAFETY", "PTW APPROVER", "PTW CLOSURE", "CCP APPROVER"
        };
        private static readonly string[] AdministrativeRoleIds = { "APPLICATION ADMIN", "COMPANY ADMIN" };
        private static readonly string[] AdminRoleIds =
        {
            "CCP APPROVER", "PTW USER", "PTW ASSESSOR", "PTW APPROVER",
            "APPLICATION ADMIN", "COMPANY ADMIN", "TBM USER", "CCP USER",
            "PTW SAFETY", "CKL USER", "PTW CLOSURE"
        };

        List<UserModel> users;
        List<RoleModel> rolelist;
        List<UserRoleModel> userrolelist;
        List<ConstructorModel> constructorlist;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    UserModel ent = UserViewModel.GetLoggedInUserInfo();
                    users = UserViewModel.GetUserList(ent.UserID);
                    Session["ePTW_UsersList"] = users;
                    rolelist = RoleViewModel.GetRoleList(ent.UserID);
                    Session["ePTW_RoleList"] = rolelist;
                    constructorlist = ConstructorViewModel.GetConstructorsList(ent.UserID);
                    Session["ePTW_ConstructorList"] = constructorlist;
                    PopulateRoleCategories();
                }
                else
                {
                    users = (Session["ePTW_UsersList"] as List<UserModel>);
                    rolelist = Session["ePTW_RoleList"] as List<RoleModel>;
                    constructorlist = (Session["ePTW_ConstructorList"] as List<ConstructorModel>);
                }

                if (rolelist.Count <= 1)
                {
                    var itmNew = ASPxMenu1.Items.FindByName("New");
                    if (itmNew != null) itmNew.Visible = false;
                    var itmUpload = ASPxMenu1.Items.FindByName("UploadExcel");
                    if (itmUpload != null) itmUpload.Visible = false;
                    gvUsers.SettingsDataSecurity.AllowInsert = false;
                }
                gvUsers.DataSource = users;
                gvUsers.DataBind();
                //gvUsers.FocusedRowIndex = -1;
            }
            catch
            {
                Response.Redirect("~/Account/SignIn.aspx");
            }
        }
        private void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
        protected void gvUsers_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
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
            if (e.Column.FieldName == "Photo")
            {
                ((ASPxBinaryImage)e.Editor).Width = 250;
                ((ASPxBinaryImage)e.Editor).Height = 250;
            }

            UserModel user = UserViewModel.GetLoggedInUserInfo();
            object userIdValue = gridView.FocusedRowIndex < 0 ? null : gridView.GetRowValues(gridView.FocusedRowIndex, "UserID");
            string UserID = userIdValue == null ? "" : userIdValue.ToString();
            if (gridView.IsNewRowEditing || user.UserID != UserID)
            {
                if (e.Column.FieldName == "Password")
                {
                    e.Editor.ReadOnly = false;
                    e.Editor.Enabled = false;
                    e.Editor.BackColor = System.Drawing.Color.Gray;
                    e.Editor.Visible = false;
                }
                if (e.Column.FieldName == "Confirm")
                {
                    e.Editor.ReadOnly = false;
                    e.Editor.Enabled = false;
                    e.Editor.BackColor = System.Drawing.Color.Gray;
                    e.Editor.Visible = false;
                }
            }
            else
            {
            }
        }
        protected void gvUsers_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            if (e.NewValues["Title"] == null || e.NewValues["Title"].ToString() == "")
                AddError(e.Errors, gvUsers.Columns["Title"], "Please select the Title.");
            if (e.NewValues["FirstName"] == null || e.NewValues["FirstName"].ToString() == "")
                AddError(e.Errors, gvUsers.Columns["FirstName"], "Please enter the First Name.");
            if (e.NewValues["LastName"] == null || e.NewValues["LastName"].ToString() == "")
                AddError(e.Errors, gvUsers.Columns["LastName"], "Please enter the Last Name.");
            if (e.NewValues["DocumentType"] == null || e.NewValues["DocumentType"].ToString() == "")
                AddError(e.Errors, gvUsers.Columns["DocumentType"], "Please select the Document Type.");
            if (e.NewValues["DocumentNo"] == null || e.NewValues["DocumentNo"].ToString() == "")
                AddError(e.Errors, gvUsers.Columns["DocumentNo"], "Please enter the Document Number.");
            if (e.NewValues["ContactNo"] == null || e.NewValues["ContactNo"].ToString() == "")
                AddError(e.Errors, gvUsers.Columns["ContactNo"], "Please enter the Contact Number.");
            if (e.NewValues["EmailAddress"] == null || e.NewValues["EmailAddress"].ToString() == "")
                AddError(e.Errors, gvUsers.Columns["EmailAddress"], "Please enter the Email Address.");
            else
            {
                if (e.IsNewRow)
                {
                    var result = UserViewModel.GetUser(e.NewValues["EmailAddress"].ToString());
                    if (result != null) AddError(e.Errors, gvUsers.Columns["EmailAddress"], "The Email has been registered.");
                }
            }

            if (e.NewValues["ConstructorName"] == null || e.NewValues["ConstructorName"].ToString() == "")
                AddError(e.Errors, gvUsers.Columns["ConstructorName"], "Please select the Constructor.");
            if (e.NewValues["ContactNo"] == null || e.NewValues["ContactNo"].ToString() == "")
                AddError(e.Errors, gvUsers.Columns["ContactNo"], "Please enter the Contact Number.");
            if (e.NewValues["Position"] == null || e.NewValues["Position"].ToString() == "")
                AddError(e.Errors, gvUsers.Columns["Position"], "Please enter the Position.");
            List<string> selectedRoleCategories = ParseRoleCategories(e.NewValues["UserRoleCategory"]);
            if (selectedRoleCategories.Count == 0)
                AddError(e.Errors, gvUsers.Columns["UserRoleCategory"], "Please select the User Role.");
            else if (selectedRoleCategories.Any(value => !RoleCategoryNames.Contains(value, StringComparer.OrdinalIgnoreCase)))
                AddError(e.Errors, gvUsers.Columns["UserRoleCategory"], "Only Applicant, Approver, and Admin roles are allowed.");
            if (!e.IsNewRow)
            {
                string strPassword = e.NewValues["Password"] != null ? e.NewValues["Password"].ToString() : "";
                string strConfirm = e.NewValues["Confirm"] != null ? e.NewValues["Confirm"].ToString() : "";
                if (strPassword != strConfirm)
                    AddError(e.Errors, gvUsers.Columns["Password"], "Please enter the valid password.");
                //else
                //    if (strPassword.Length < 8)
                //        AddError(e.Errors, gvUsers.Columns["Password"], "Please the length of password must more then 8 chars.");
            }
            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
                e.RowError = "Please, correct all errors.";
        }
        protected void gvUsers_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            UserModel ent = new UserModel();
            ent.Title = e.NewValues["Title"] != null ? e.NewValues["Title"].ToString() : "";
            ent.FirstName = e.NewValues["FirstName"] != null ? e.NewValues["FirstName"].ToString() : "";
            ent.LastName = e.NewValues["LastName"] != null ? e.NewValues["LastName"].ToString() : "";

            ent.Photo = e.NewValues["Photo"] != null ? ImageResize.ReduceImageSize((byte[])e.NewValues["Photo"],20) : null;

            ent.DocumentType = e.NewValues["DocumentType"] != null ? e.NewValues["DocumentType"].ToString() : "";
            ent.DocumentNo = e.NewValues["DocumentNo"] != null ? e.NewValues["DocumentNo"].ToString() : "";
            ent.ContactNo = e.NewValues["ContactNo"] != null ? e.NewValues["ContactNo"].ToString() : "";
            ent.EmailAddress = e.NewValues["EmailAddress"] != null ? e.NewValues["EmailAddress"].ToString() : "";
            ent.ConstructorName = e.NewValues["ConstructorName"] != null ? e.NewValues["ConstructorName"].ToString() : "";
            ent.Position = e.NewValues["Position"] != null ? e.NewValues["Position"].ToString() : "";
            List<string> selectedRoleCategories = NormalizeRoleCategories(ParseRoleCategories(e.NewValues["UserRoleCategory"]));
            ent.UserRoleCategory = string.Join(", ", selectedRoleCategories);
            ent.Roles = ent.UserRoleCategory;
            //ent.UserID = e.NewValues["UserID"] != null ? e.NewValues["UserID"].ToString() : "";
            ent.UserID = ent.EmailAddress;
            ent.Password = e.NewValues["Password"] != null ? e.NewValues["Password"].ToString() : "1111";
            ent.Created = DateTime.Now;
            ent.Updated = DateTime.Now;
            ent.CreatedBy = user.UserID;
            ent.UpdatedBy = user.UserID;
            ent.Status = 1;
            users.Add(ent);
            Session["ePTW_UsersList"] = users;

            UserViewModel.User_InsertUpdate(ent);
            ApplyRoleCategories(ent.UserID, selectedRoleCategories, user);

            e.Cancel = true;
            gvUsers.CancelEdit();
            gvUsers.DataSource = users;
            gvUsers.DataBind();
        }
        protected void gvUsers_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            string userid = e.Keys["UserID"].ToString();
            UserModel ent = users.Find(item => item.UserID == userid);

            if (ent != null)
            {
                ent.Title = e.NewValues["Title"].ToString();
                ent.FirstName = e.NewValues["FirstName"].ToString();
                ent.LastName = e.NewValues["LastName"].ToString();
                ent.Photo = e.NewValues["Photo"] != null ? ImageResize.ReduceImageSize((byte[])e.NewValues["Photo"], 20) : null;
                ent.DocumentType = e.NewValues["DocumentType"].ToString();
                ent.DocumentNo = e.NewValues["DocumentNo"].ToString();
                ent.ContactNo = e.NewValues["ContactNo"].ToString();
                ent.EmailAddress = e.NewValues["EmailAddress"].ToString();
                ent.UserID = ent.EmailAddress;
                ent.ConstructorName = e.NewValues["ConstructorName"].ToString();
                ent.Position = e.NewValues["Position"].ToString();
                List<string> selectedRoleCategories = NormalizeRoleCategories(ParseRoleCategories(e.NewValues["UserRoleCategory"]));
                ent.UserRoleCategory = string.Join(", ", selectedRoleCategories);
                ent.Roles = ent.UserRoleCategory;
                ent.Updated = DateTime.Now;
                ent.UpdatedBy = user.UserID;

                string strPassword = e.NewValues["Password"] != null ? e.NewValues["Password"].ToString() : "";
                if (strPassword != "") ent.Password = strPassword;

                UserViewModel.User_InsertUpdate(ent);
                ApplyRoleCategories(ent.UserID, selectedRoleCategories, user);

                Session["ePTW_UsersList"] = users;
            }

            e.Cancel = true;
            gvUsers.CancelEdit();
            gvUsers.DataSource = users;
            gvUsers.DataBind();
        }
        protected void gvUsers_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "refresh")
            {
                UserModel current = UserViewModel.GetLoggedInUserInfo();
                users = UserViewModel.GetUserList(current.UserID);
                PopulateRoleCategories();
                Session["ePTW_UsersList"] = users;
                gvUsers.DataSource = users;
                gvUsers.DataBind();
                return;
            }
            if (e.Parameters == "lock")
            {
                string strid = gvUsers.GetRowValues(gvUsers.FocusedRowIndex, "UserID").ToString();
                if (strid != "")
                {
                    UserViewModel.User_LockUnlock(strid, 2);
                    UserModel ent = users.Find(item => item.UserID == strid);

                    if (ent != null)
                    {
                        ent.Status = 2;
                        Session["ePTW_UsersList"] = users;
                    }

                    gvUsers.CancelEdit();
                    gvUsers.DataSource = users;
                    gvUsers.DataBind();
                }
            }
            if (e.Parameters == "unlock")
            {
                string strid = gvUsers.GetRowValues(gvUsers.FocusedRowIndex, "UserID").ToString();
                if (strid != "")
                {
                    UserViewModel.User_LockUnlock(strid, 1);
                    UserModel ent = users.Find(item => item.UserID == strid);

                    if (ent != null)
                    {
                        ent.Status = 1;
                        Session["ePTW_UsersList"] = users;
                    }

                    gvUsers.CancelEdit();
                    gvUsers.DataSource = users;
                    gvUsers.DataBind();
                }
            }

            if (e.Parameters == "ResetPassword")
            {
                string strid = gvUsers.GetRowValues(gvUsers.FocusedRowIndex, "UserID").ToString();
                if (strid != "")
                {
                    UserViewModel.ResetPassword(strid);
                }
            }
        }

        protected void gvRoles_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            ASPxGridView gridView = sender as ASPxGridView;
            if (gridView.IsEditing && e.Column.FieldName == "RoleID")
            {
                ASPxComboBox cbRole = e.Editor as ASPxComboBox;
                cbRole.DataSource = rolelist;
                cbRole.DataBind();
            }
        }
        protected void gvRoles_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView userrolegrid = sender as ASPxGridView;
            var masterKey = userrolegrid.GetMasterRowKeyValue();
            if (masterKey != null)
            {
                userrolelist = UserRoleViewModel.GetUserRoleList(masterKey.ToString());
                userrolegrid.DataSource = userrolelist;
                Session["ePTW_SelectedUserID"] = masterKey.ToString();
                Session["ePTW_UserRoleList"] = userrolelist;
            }
        }
        protected void gvRoles_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            if (e.NewValues["RoleID"] == null || e.NewValues["RoleID"].ToString() == "")
                AddError(e.Errors, (sender as ASPxGridView).Columns["RoleID"], "Please select the User Role.");
            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
                e.RowError = "Please, correct all errors.";
        }
        protected void gvRoles_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            UserModel user = UserViewModel.GetLoggedInUserInfo();

            UserRoleModel ent = new UserRoleModel();
            ent.UserID = Session["ePTW_SelectedUserID"] != null ? Session["ePTW_SelectedUserID"].ToString() : "";
            ent.RoleID = e.NewValues["RoleID"] != null ? e.NewValues["RoleID"].ToString() : "";
            ent.Created = DateTime.Now;
            ent.Updated = DateTime.Now;
            ent.CreatedBy = user.UserID;
            ent.UpdatedBy = user.UserID;

            var result = userrolelist.Find(item => item.RoleID == ent.RoleID);
            if (result == null)
            {
                userrolelist.Add(ent);
                Session["ePTW_UserRoleList"] = userrolelist;
                UserRoleViewModel.UserRole_InsertUpdate(ent);
            }

            e.Cancel = true;

            ASPxGridView tmpGV = (sender as ASPxGridView);
            tmpGV.CancelEdit();
            tmpGV.DataSource = userrolelist;
            tmpGV.DataBind();
            (sender as ASPxGridView).FocusedRowIndex = -1;
        }
        protected void gvRoles_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "delete")
            {
                ASPxGridView tmpGV = (sender as ASPxGridView);
                string[] fields = { "UserID", "RoleID" };
                object[] values = (object[])tmpGV.GetRowValues((sender as ASPxGridView).FocusedRowIndex,fields);
                string strUserID = values[0] != null ? values[0].ToString() : "";
                string strRoleID = values[1] != null ? values[1].ToString() : "";
                //string strProjectID = values[2] != null ? values[2].ToString() : "";

                UserRoleModel result = userrolelist.FindLast(item => item.UserID == strUserID && item.RoleID == strRoleID);
                userrolelist.Remove(result);

                Session["ePTW_UserRoleList"] = userrolelist;

                UserRoleViewModel.UserRole_Delete(strUserID,strRoleID);

                tmpGV.DataSource = userrolelist;
                tmpGV.DataBind();
                tmpGV.FocusedRowIndex = -1;
            }
        }

        protected void gvUsers_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Preview)
            {

                ASPxButton btnedit = (ASPxButton)gvUsers.FindPreviewRowTemplateControl(e.VisibleIndex, "btnEdit");
                ASPxButton btnlock = (ASPxButton)gvUsers.FindPreviewRowTemplateControl(e.VisibleIndex, "btnLock");
                //ASPxButton btnunloack = (ASPxButton)gvUsers.FindPreviewRowTemplateControl(e.VisibleIndex, "btnUnlock");
                ASPxButton btnreset = (ASPxButton)gvUsers.FindPreviewRowTemplateControl(e.VisibleIndex, "btnReset");

                btnlock.Visible = false;
                //btnunloack.Visible = false;
                btnedit.Visible = false;
                btnreset.Visible = false;

                if (rolelist.Count > 1)
                {
                    btnedit.Visible = true;
                    btnlock.Visible = true;
                    //btnunloack.Visible = true;
                    btnreset.Visible = true;
                }
                if (rolelist.Count <= 1)
                {
                    btnedit.Visible = true;
                }
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ASPxButton button = sender as ASPxButton;
            GridViewPreviewRowTemplateContainer container = button.NamingContainer as GridViewPreviewRowTemplateContainer;
            if (container != null)
            {
                int visibleIndex = container.VisibleIndex;
                object keyValue = gvUsers.GetRowValues(visibleIndex, gvUsers.KeyFieldName);
                gvUsers.FocusedRowIndex = visibleIndex;
                gvUsers.StartEdit(visibleIndex);
            }
        }
        protected void btnLock_Click(object sender, EventArgs e)
        {
            ASPxButton button = sender as ASPxButton;
            GridViewPreviewRowTemplateContainer container = button.NamingContainer as GridViewPreviewRowTemplateContainer;
            if (container != null)
            {
                int visibleIndex = container.VisibleIndex;
                object keyValue = gvUsers.GetRowValues(visibleIndex, gvUsers.KeyFieldName);
                if (keyValue.ToString() != "")
                {
                    UserViewModel.User_LockUnlock(keyValue.ToString(), 2);
                    UserModel ent = users.Find(item => item.UserID == keyValue.ToString());

                    if (ent != null)
                    {
                        //ent.Status = 2;
                        users.Remove(ent);
                        Session["ePTW_UsersList"] = users;
                    }

                    gvUsers.CancelEdit();
                    gvUsers.DataSource = users;
                    gvUsers.DataBind();
                }
            }
        }
        protected void btnUnlock_Click(object sender, EventArgs e)
        {
            ASPxButton button = sender as ASPxButton;
            GridViewPreviewRowTemplateContainer container = button.NamingContainer as GridViewPreviewRowTemplateContainer;
            if (container != null)
            {
                int visibleIndex = container.VisibleIndex;
                object keyValue = gvUsers.GetRowValues(visibleIndex, gvUsers.KeyFieldName);
                if (keyValue.ToString() != "")
                {
                    UserViewModel.User_LockUnlock(keyValue.ToString(), 1);
                    UserModel ent = users.Find(item => item.UserID == keyValue.ToString());

                    if (ent != null)
                    {
                        ent.Status = 1;
                        Session["ePTW_UsersList"] = users;
                    }

                    gvUsers.CancelEdit();
                    gvUsers.DataSource = users;
                    gvUsers.DataBind();
                }
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ASPxButton button = sender as ASPxButton;
            GridViewPreviewRowTemplateContainer container = button.NamingContainer as GridViewPreviewRowTemplateContainer;
            if (container != null)
            {
                int visibleIndex = container.VisibleIndex;
                object keyValue = gvUsers.GetRowValues(visibleIndex, gvUsers.KeyFieldName);
                if (keyValue.ToString() != "")
                {
                    UserViewModel.ResetPassword(keyValue.ToString());
                }
            }
        }

        private void PopulateRoleCategories()
        {
            if (users == null) return;
            foreach (UserModel item in users)
                item.UserRoleCategory = InferRoleCategory(item.Roles);
        }

        private static string InferRoleCategory(string roles)
        {
            HashSet<string> assignedRoles = ParseDatabaseRoles(roles);
            if (AdministrativeRoleIds.Any(assignedRoles.Contains)) return "Admin";

            var categories = new List<string>();
            if (ApplicantRoleIds.Any(assignedRoles.Contains))
                categories.Add("Applicant");
            if (ApproverRoleIds.Any(assignedRoles.Contains))
                categories.Add("Approver");

            return categories.Count == 0 ? "Applicant" : string.Join(", ", categories);
        }

        private static HashSet<string> ParseDatabaseRoles(string roles)
        {
            return new HashSet<string>((roles ?? "")
                .Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(value => value.Trim())
                .Where(value => value.Length > 0), StringComparer.OrdinalIgnoreCase);
        }

        private static List<string> ParseRoleCategories(object value)
        {
            return Convert.ToString(value)
                .Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(item => item.Trim())
                .Where(item => item.Length > 0)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static List<string> NormalizeRoleCategories(IEnumerable<string> categories)
        {
            var requested = new HashSet<string>(categories ?? Enumerable.Empty<string>(), StringComparer.OrdinalIgnoreCase);

            // Admin already expands to every application role. Keeping the redundant
            // Applicant/Approver labels would be impossible to reconstruct from the
            // existing database role assignments after the page is refreshed.
            if (requested.Contains("Admin")) return new List<string> { "Admin" };

            return RoleCategoryNames
                .Where(category => requested.Contains(category))
                .ToList();
        }

        private void ApplyRoleCategories(string userId, IEnumerable<string> categories, UserModel actor)
        {
            List<UserRoleModel> currentRoles = UserRoleViewModel.GetUserRoleList(userId);
            List<string> normalizedCategories = NormalizeRoleCategories(categories);
            var targetRoleIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (string category in normalizedCategories)
            {
                if (string.Equals(category, "Admin", StringComparison.OrdinalIgnoreCase))
                    targetRoleIds.UnionWith(AdminRoleIds);
                else if (string.Equals(category, "Approver", StringComparison.OrdinalIgnoreCase))
                    targetRoleIds.UnionWith(ApproverRoleIds);
                else if (string.Equals(category, "Applicant", StringComparison.OrdinalIgnoreCase))
                    targetRoleIds.UnionWith(ApplicantRoleIds);
            }

            if (targetRoleIds.Count == 0)
                throw new InvalidOperationException("Select at least one of Applicant, Approver, or Admin.");

            foreach (UserRoleModel existing in currentRoles)
                UserRoleViewModel.UserRole_Delete(userId, existing.RoleID);

            foreach (string roleId in targetRoleIds)
            {
                UserRoleViewModel.UserRole_InsertUpdate(new UserRoleModel
                {
                    UserID = userId,
                    RoleID = roleId,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    CreatedBy = actor.UserID,
                    UpdatedBy = actor.UserID
                });
            }
        }

        protected void ucUserExcel_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            var issues = new List<UserImportIssue>();
            int importedCount = 0;
            int updatedCount = 0;
            if (!e.IsValid)
            {
                issues.Add(new UserImportIssue(1, "File", "Invalid Excel file or file exceeds 10 MB.", "Upload a valid .xlsx or .xlsm workbook no larger than 10 MB."));
                e.CallbackData = SerializeUserImportResult(importedCount, updatedCount, issues);
                return;
            }

            try
            {
                users = Session["ePTW_UsersList"] as List<UserModel> ?? new List<UserModel>();
                rolelist = Session["ePTW_RoleList"] as List<RoleModel> ?? new List<RoleModel>();
                constructorlist = Session["ePTW_ConstructorList"] as List<ConstructorModel> ?? new List<ConstructorModel>();
                UserModel actor = UserViewModel.GetLoggedInUserInfo();
                string[] requiredHeaders = { "Title", "First Name", "Last Name", "Document Type", "Document No", "Contact No", "Email Address", "Contractor Name", "Position", "User Roles" };
                var allowedTitles = new HashSet<string>(new[] { "Mr", "Mrs", "Miss", "Ms" }, StringComparer.OrdinalIgnoreCase);
                var allowedDocumentTypes = new HashSet<string>(new[] { "NRIC", "FIN", "WP" }, StringComparer.OrdinalIgnoreCase);
                var allowedRoleCategories = new HashSet<string>(new[] { "Applicant", "Approver", "Admin" }, StringComparer.OrdinalIgnoreCase);
                var constructors = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (ConstructorModel constructor in constructorlist)
                    if (!constructors.ContainsKey(constructor.Name)) constructors.Add(constructor.Name, constructor.Name);

                using (var stream = new MemoryStream(e.UploadedFile.FileBytes))
                using (var workbook = new XLWorkbook(stream))
                {
                    var sheet = workbook.Worksheets.First();
                    var headers = BuildUserHeaderMap(sheet);
                    foreach (string header in requiredHeaders)
                        if (!headers.ContainsKey(header))
                            issues.Add(new UserImportIssue(1, header, "Required column is missing.", "Add the \"" + header + "\" header to Row 1."));

                    if (issues.Count == 0)
                    {
                        int lastRow = sheet.LastRowUsed() == null ? 1 : sheet.LastRowUsed().RowNumber();
                        for (int row = 2; row <= lastRow; row++)
                        {
                            var values = requiredHeaders.ToDictionary(header => header, header => UserCellText(sheet, row, headers[header]), StringComparer.OrdinalIgnoreCase);
                            if (values.Values.All(string.IsNullOrWhiteSpace)) continue;
                            int issueStart = issues.Count;

                            foreach (string header in requiredHeaders)
                                if (string.IsNullOrWhiteSpace(values[header]))
                                    issues.Add(new UserImportIssue(row, header, "Empty field.", "Enter a valid " + header + "."));

                            if (!string.IsNullOrWhiteSpace(values["Title"]) && !allowedTitles.Contains(values["Title"]))
                                issues.Add(new UserImportIssue(row, "Title", "Invalid Title (" + values["Title"] + ").", "Use Mr, Mrs, Miss, or Ms."));
                            if (!string.IsNullOrWhiteSpace(values["Document Type"]) && !allowedDocumentTypes.Contains(values["Document Type"]))
                                issues.Add(new UserImportIssue(row, "Document Type", "Invalid Document Type (" + values["Document Type"] + ").", "Use NRIC, FIN, or WP."));
                            if (!string.IsNullOrWhiteSpace(values["Contractor Name"]) && !constructors.ContainsKey(values["Contractor Name"]))
                                issues.Add(new UserImportIssue(row, "Contractor Name", "Unknown Contractor Name (" + values["Contractor Name"] + ").", "Use a contractor available in User Management."));
                            List<string> requestedRoleCategories = values["User Roles"]
                                .Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(value => value.Trim())
                                .Where(value => value.Length > 0)
                                .Distinct(StringComparer.OrdinalIgnoreCase)
                                .ToList();
                            List<string> unknownRoleCategories = requestedRoleCategories
                                .Where(value => !allowedRoleCategories.Contains(value))
                                .ToList();
                            if (!string.IsNullOrWhiteSpace(values["User Roles"]) && requestedRoleCategories.Count == 0)
                                issues.Add(new UserImportIssue(row, "User Roles", "No User Role was provided.", "Use Applicant, Approver, or Admin."));
                            if (unknownRoleCategories.Count > 0)
                                issues.Add(new UserImportIssue(row, "User Roles", "Unknown User Role(s): " + string.Join(", ", unknownRoleCategories) + ".", "Use Applicant, Approver, or Admin, separated by commas."));
                            if (!string.IsNullOrWhiteSpace(values["Email Address"]) && !IsValidEmail(values["Email Address"]))
                                issues.Add(new UserImportIssue(row, "Email Address", "Invalid email address.", "Enter a valid email address."));

                            UserModel existingUser = users.FirstOrDefault(item =>
                                string.Equals(item.EmailAddress, values["Email Address"], StringComparison.OrdinalIgnoreCase) ||
                                string.Equals(item.UserID, values["Email Address"], StringComparison.OrdinalIgnoreCase));
                            if (existingUser == null && IsValidEmail(values["Email Address"]))
                                existingUser = UserViewModel.GetUser(values["Email Address"]);
                            UserModel documentOwner = users.FirstOrDefault(item =>
                                string.Equals(item.DocumentNo, values["Document No"], StringComparison.OrdinalIgnoreCase));
                            if (documentOwner != null && (existingUser == null ||
                                !string.Equals(documentOwner.UserID, existingUser.UserID, StringComparison.OrdinalIgnoreCase)))
                                issues.Add(new UserImportIssue(row, "Document No", "Document No is already assigned to another user (" + values["Document No"] + ").", "Use the existing user's Email Address to update that row, or enter a unique Document No."));

                            if (issues.Count != issueStart) continue;

                            try
                            {
                                List<string> roleCategories = NormalizeRoleCategories(requestedRoleCategories.Select(value =>
                                    allowedRoleCategories.First(role => string.Equals(role, value, StringComparison.OrdinalIgnoreCase))));
                                string roleCategoryText = string.Join(", ", roleCategories);
                                var item = new UserModel
                                {
                                    Photo = existingUser == null ? null : existingUser.Photo,
                                    Title = allowedTitles.First(title => string.Equals(title, values["Title"], StringComparison.OrdinalIgnoreCase)),
                                    FirstName = values["First Name"],
                                    LastName = values["Last Name"],
                                    DocumentType = allowedDocumentTypes.First(type => string.Equals(type, values["Document Type"], StringComparison.OrdinalIgnoreCase)),
                                    DocumentNo = values["Document No"],
                                    ContactNo = values["Contact No"],
                                    EmailAddress = values["Email Address"],
                                    UserID = values["Email Address"],
                                    ConstructorName = constructors[values["Contractor Name"]],
                                    Position = values["Position"],
                                    Password = existingUser == null ? "1111" : existingUser.Password,
                                    Status = existingUser == null ? 1 : existingUser.Status,
                                    UserRoleCategory = roleCategoryText,
                                    Roles = roleCategoryText,
                                    Created = existingUser == null ? DateTime.Now : existingUser.Created,
                                    Updated = DateTime.Now,
                                    CreatedBy = existingUser == null ? actor.UserID : existingUser.CreatedBy,
                                    UpdatedBy = actor.UserID
                                };
                                UserViewModel.User_InsertUpdate(item);
                                if (existingUser == null)
                                {
                                    users.Add(item);
                                    importedCount++;
                                }
                                else
                                {
                                    int existingIndex = users.FindIndex(value =>
                                        string.Equals(value.UserID, existingUser.UserID, StringComparison.OrdinalIgnoreCase));
                                    if (existingIndex >= 0)
                                        users[existingIndex] = item;
                                    else
                                        users.Add(item);
                                    updatedCount++;
                                }
                                try
                                {
                                    ApplyRoleCategories(item.UserID, roleCategories, actor);
                                }
                                catch (Exception roleException)
                                {
                                    issues.Add(new UserImportIssue(row, "User Roles", "The user was saved but the selected roles could not be assigned: " + roleException.Message, "Assign the roles from User Management."));
                                }
                            }
                            catch (Exception ex)
                            {
                                issues.Add(new UserImportIssue(row, "Record", "The row could not be saved: " + ex.Message, "Review the row values and try again."));
                            }
                        }
                    }
                }

                Session["ePTW_UsersList"] = users;
            }
            catch (Exception ex)
            {
                issues.Add(new UserImportIssue(1, "File", "The workbook could not be read: " + ex.Message, "Use an unprotected .xlsx or .xlsm workbook with headers in Row 1."));
            }

            e.CallbackData = SerializeUserImportResult(importedCount, updatedCount, issues);
        }

        private static Dictionary<string, int> BuildUserHeaderMap(IXLWorksheet sheet)
        {
            var result = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var lastCell = sheet.Row(1).LastCellUsed();
            if (lastCell == null) return result;
            for (int column = 1; column <= lastCell.Address.ColumnNumber; column++)
            {
                string header = sheet.Cell(1, column).GetString().Trim();
                if (!string.IsNullOrWhiteSpace(header) && !result.ContainsKey(header)) result.Add(header, column);
            }
            return result;
        }

        private static string UserCellText(IXLWorksheet sheet, int row, int column)
        {
            return sheet.Cell(row, column).GetFormattedString().Trim();
        }

        private static bool IsValidEmail(string value)
        {
            try { return new MailAddress(value).Address == value; }
            catch { return false; }
        }

        private static string SerializeUserImportResult(int importedCount, int updatedCount, List<UserImportIssue> issues)
        {
            return new JavaScriptSerializer().Serialize(new { ImportedCount = importedCount, UpdatedCount = updatedCount, Issues = issues });
        }

        public class UserImportIssue
        {
            public int Row { get; set; }
            public string Field { get; set; }
            public string Description { get; set; }
            public string SuggestedFix { get; set; }

            public UserImportIssue(int row, string field, string description, string suggestedFix)
            {
                Row = row;
                Field = field;
                Description = description;
                SuggestedFix = suggestedFix;
            }
        }
    }
}
