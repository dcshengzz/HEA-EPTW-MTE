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
                    rolelist = (Session["ePTW_RoleList"] as List<RoleModel>);
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
            if (e.NewValues["UserRoleCategory"] == null || e.NewValues["UserRoleCategory"].ToString() == "")
                AddError(e.Errors, gvUsers.Columns["UserRoleCategory"], "Please select the User Role.");
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
            ent.UserRoleCategory = e.NewValues["UserRoleCategory"] != null ? e.NewValues["UserRoleCategory"].ToString() : "Applicant";
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
            ApplyRoleCategory(ent.UserID, ent.UserRoleCategory, user);

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
                ent.UserRoleCategory = e.NewValues["UserRoleCategory"] != null ? e.NewValues["UserRoleCategory"].ToString() : "Applicant";
                ent.Roles = ent.UserRoleCategory;
                ent.Updated = DateTime.Now;
                ent.UpdatedBy = user.UserID;

                string strPassword = e.NewValues["Password"] != null ? e.NewValues["Password"].ToString() : "";
                if (strPassword != "") ent.Password = strPassword;

                UserViewModel.User_InsertUpdate(ent);
                ApplyRoleCategory(ent.UserID, ent.UserRoleCategory, user);

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
            string value = roles ?? "";
            if (value.IndexOf("ADMIN", StringComparison.OrdinalIgnoreCase) >= 0) return "Admin";
            if (value.IndexOf("APPROVER", StringComparison.OrdinalIgnoreCase) >= 0) return "Approver";
            return "Applicant";
        }

        private void ApplyRoleCategory(string userId, string category, UserModel actor)
        {
            List<UserRoleModel> currentRoles = UserRoleViewModel.GetUserRoleList(userId);
            IEnumerable<RoleModel> targetRoles;
            if (string.Equals(category, "Admin", StringComparison.OrdinalIgnoreCase))
                targetRoles = rolelist;
            else if (string.Equals(category, "Approver", StringComparison.OrdinalIgnoreCase))
                targetRoles = rolelist.Where(item => item.RoleID.IndexOf("APPROVER", StringComparison.OrdinalIgnoreCase) >= 0);
            else
                targetRoles = rolelist.Where(item => item.RoleID.IndexOf("USER", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                                     item.RoleID.IndexOf("APPLICANT", StringComparison.OrdinalIgnoreCase) >= 0);

            List<RoleModel> targets = targetRoles.ToList();
            if (targets.Count == 0 && rolelist.Count > 0) targets.Add(rolelist[0]);

            foreach (UserRoleModel existing in currentRoles)
                UserRoleViewModel.UserRole_Delete(userId, existing.RoleID);

            foreach (RoleModel role in targets)
            {
                UserRoleViewModel.UserRole_InsertUpdate(new UserRoleModel
                {
                    UserID = userId,
                    RoleID = role.RoleID,
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
            if (!e.IsValid)
            {
                issues.Add(new UserImportIssue(1, "File", "Invalid Excel file or file exceeds 10 MB.", "Upload a valid .xlsx or .xlsm workbook no larger than 10 MB."));
                e.CallbackData = SerializeUserImportResult(importedCount, issues);
                return;
            }

            try
            {
                users = Session["ePTW_UsersList"] as List<UserModel> ?? new List<UserModel>();
                rolelist = Session["ePTW_RoleList"] as List<RoleModel> ?? new List<RoleModel>();
                constructorlist = Session["ePTW_ConstructorList"] as List<ConstructorModel> ?? new List<ConstructorModel>();
                UserModel actor = UserViewModel.GetLoggedInUserInfo();
                string[] requiredHeaders = { "Title", "First Name", "Last Name", "Document Type", "Document No", "Contact No", "Email Address", "Contractor Name", "Position" };
                var allowedTitles = new HashSet<string>(new[] { "Mr", "Mrs", "Miss", "Ms" }, StringComparer.OrdinalIgnoreCase);
                var allowedDocumentTypes = new HashSet<string>(new[] { "NRIC", "FIN", "WP" }, StringComparer.OrdinalIgnoreCase);
                var constructors = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (ConstructorModel constructor in constructorlist)
                    if (!constructors.ContainsKey(constructor.Name)) constructors.Add(constructor.Name, constructor.Name);
                var emails = new HashSet<string>(users.Select(item => item.EmailAddress), StringComparer.OrdinalIgnoreCase);
                var documents = new HashSet<string>(users.Select(item => item.DocumentNo), StringComparer.OrdinalIgnoreCase);

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
                            if (!string.IsNullOrWhiteSpace(values["Email Address"]) && !IsValidEmail(values["Email Address"]))
                                issues.Add(new UserImportIssue(row, "Email Address", "Invalid email address.", "Enter a valid email address."));
                            else if (!string.IsNullOrWhiteSpace(values["Email Address"]) && emails.Contains(values["Email Address"]))
                                issues.Add(new UserImportIssue(row, "Email Address", "Duplicate value (" + values["Email Address"] + ").", "Ensure Email Address is unique."));
                            if (!string.IsNullOrWhiteSpace(values["Document No"]) && documents.Contains(values["Document No"]))
                                issues.Add(new UserImportIssue(row, "Document No", "Duplicate value (" + values["Document No"] + ").", "Ensure Document No is unique."));

                            if (issues.Count != issueStart) continue;

                            try
                            {
                                var item = new UserModel
                                {
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
                                    Password = "1111",
                                    Status = 1,
                                    UserRoleCategory = "Applicant",
                                    Roles = "Applicant",
                                    Created = DateTime.Now,
                                    Updated = DateTime.Now,
                                    CreatedBy = actor.UserID,
                                    UpdatedBy = actor.UserID
                                };
                                UserViewModel.User_InsertUpdate(item);
                                users.Add(item);
                                emails.Add(item.EmailAddress);
                                documents.Add(item.DocumentNo);
                                importedCount++;
                                try
                                {
                                    ApplyRoleCategory(item.UserID, "Applicant", actor);
                                }
                                catch (Exception roleException)
                                {
                                    issues.Add(new UserImportIssue(row, "User Roles", "The user was imported but the Applicant role could not be assigned: " + roleException.Message, "Assign the Applicant role from User Management."));
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

            e.CallbackData = SerializeUserImportResult(importedCount, issues);
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

        private static string SerializeUserImportResult(int importedCount, List<UserImportIssue> issues)
        {
            return new JavaScriptSerializer().Serialize(new { ImportedCount = importedCount, Issues = issues });
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
