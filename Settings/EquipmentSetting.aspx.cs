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
using System.Web.Script.Serialization;

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
            if (!IsPostBack)
                gvEquipment.FocusedRowIndex = -1;
        }
        private void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }

        protected void gvEquipment_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
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
                AddError(e.Errors, gvEquipment.Columns["RegistrationNo"], "Please enter the Building Name.");
            else
            {
                string original = e.Keys["RegistrationNo"] == null ? "" : e.Keys["RegistrationNo"].ToString();
                string proposed = e.NewValues["RegistrationNo"].ToString().Trim();
                if (equipmentlist.Any(item => !string.Equals(item.RegistrationNo, original, StringComparison.OrdinalIgnoreCase) &&
                                              string.Equals(item.RegistrationNo, proposed, StringComparison.OrdinalIgnoreCase)))
                    AddError(e.Errors, gvEquipment.Columns["RegistrationNo"], "This Building Name already exists.");
            }
            if (e.NewValues["EquipmentName"] == null || e.NewValues["EquipmentName"].ToString() == "")
                AddError(e.Errors, gvEquipment.Columns["EquipmentName"], "Please enter the EL/ES Number.");
            if (e.NewValues["EquipmentType"] == null || e.NewValues["EquipmentType"].ToString() == "")
                AddError(e.Errors, gvEquipment.Columns["EquipmentType"], "Please enter the MFG NO.");
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
            ent.Description = "";
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
                ent.RegistrationNo = e.NewValues["RegistrationNo"] != null ? e.NewValues["RegistrationNo"].ToString().Trim() : ent.RegistrationNo;
                ent.EquipmentName = e.NewValues["EquipmentName"] != null ? e.NewValues["EquipmentName"].ToString() : "";
                ent.EquipmentType = e.NewValues["EquipmentType"] != null ? e.NewValues["EquipmentType"].ToString() : "";
                ent.Description = "";
                if (e.NewValues["Photo"] != null) ent.Photo = (byte[])e.NewValues["Photo"];
                if (e.NewValues["Document"] != null) ent.Document = (byte[])e.NewValues["Document"];
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

        protected void gvEquipment_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "refresh")
            {
                equipmentlist = EquipmentViewModel.GetEquipmentList(UserViewModel.GetSelectedProject());
                Session["ePTW_EQList"] = equipmentlist;
            }
            else if (e.Parameters == "lock" || e.Parameters == "unlock")
            {
                if (gvEquipment.FocusedRowIndex >= 0)
                {
                    object key = gvEquipment.GetRowValues(gvEquipment.FocusedRowIndex, "RegistrationNo");
                    EquipmentModel ent = key == null ? null : equipmentlist.FirstOrDefault(item => item.RegistrationNo == key.ToString());
                    if (ent != null)
                    {
                        UserModel user = UserViewModel.GetLoggedInUserInfo();
                        ent.Status = e.Parameters == "lock" ? 2 : 1;
                        ent.Updated = DateTime.Now;
                        ent.UpdatedBy = user.UserID;
                        EquipmentViewModel.Equipment_InsertUpdate(ent);
                        Session["ePTW_EQList"] = equipmentlist;
                    }
                }
            }

            gvEquipment.DataSource = equipmentlist;
            gvEquipment.DataBind();
        }

        protected void ucEquipmentExcel_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            var issues = new List<ImportIssue>();
            int importedCount = 0;

            if (!e.IsValid)
            {
                issues.Add(new ImportIssue(1, "File", "Invalid Excel file or file exceeds 10 MB.", "Upload a valid .xlsx or .xlsm workbook no larger than 10 MB."));
                e.CallbackData = SerializeImportResult(importedCount, issues);
                return;
            }

            try
            {
                equipmentlist = Session["ePTW_EQList"] as List<EquipmentModel> ?? EquipmentViewModel.GetEquipmentList(UserViewModel.GetSelectedProject());
                UserModel user = UserViewModel.GetLoggedInUserInfo();
                string project = UserViewModel.GetSelectedProject();

                using (var stream = new MemoryStream(e.UploadedFile.FileBytes))
                using (var workbook = new XLWorkbook(stream))
                {
                    var sheet = workbook.Worksheets.First();
                    var headers = BuildHeaderMap(sheet);
                    string[] requiredHeaders = { "Building Name", "MFG NO", "EL/ES Number" };
                    foreach (string header in requiredHeaders)
                    {
                        if (!headers.ContainsKey(header))
                            issues.Add(new ImportIssue(1, header, "Required column is missing.", "Add the \"" + header + "\" header to Row 1."));
                    }

                    if (issues.Count == 0)
                    {
                        int lastRow = sheet.LastRowUsed() == null ? 1 : sheet.LastRowUsed().RowNumber();
                        var seen = new HashSet<string>(equipmentlist.Select(item => item.RegistrationNo), StringComparer.OrdinalIgnoreCase);
                        for (int row = 2; row <= lastRow; row++)
                        {
                            string building = CellText(sheet, row, headers["Building Name"]);
                            string mfg = CellText(sheet, row, headers["MFG NO"]);
                            string eles = CellText(sheet, row, headers["EL/ES Number"]);
                            if (string.IsNullOrWhiteSpace(building) && string.IsNullOrWhiteSpace(mfg) && string.IsNullOrWhiteSpace(eles))
                                continue;

                            int issueStart = issues.Count;
                            if (string.IsNullOrWhiteSpace(building))
                                issues.Add(new ImportIssue(row, "Building Name", "Empty field.", "Enter a valid Building Name."));
                            else if (seen.Contains(building))
                                issues.Add(new ImportIssue(row, "Building Name", "Duplicate value (" + building + ").", "Ensure Building Name is unique."));
                            if (string.IsNullOrWhiteSpace(mfg))
                                issues.Add(new ImportIssue(row, "MFG NO", "Empty field.", "Enter a valid MFG NO."));
                            if (string.IsNullOrWhiteSpace(eles))
                                issues.Add(new ImportIssue(row, "EL/ES Number", "Empty field.", "Enter a valid EL/ES Number."));

                            if (issues.Count != issueStart) continue;

                            try
                            {
                                var item = new EquipmentModel
                                {
                                    RegistrationNo = building,
                                    EquipmentType = mfg,
                                    EquipmentName = eles,
                                    Description = "",
                                    ProjectName = project,
                                    Status = 1,
                                    Created = DateTime.Now,
                                    Updated = DateTime.Now,
                                    CreatedBy = user.UserID,
                                    UpdatedBy = user.UserID
                                };
                                EquipmentViewModel.Equipment_InsertUpdate(item);
                                equipmentlist.Add(item);
                                seen.Add(building);
                                importedCount++;
                            }
                            catch (Exception ex)
                            {
                                issues.Add(new ImportIssue(row, "Record", "The row could not be saved: " + ex.Message, "Review the row values and try again."));
                            }
                        }
                    }
                }

                Session["ePTW_EQList"] = equipmentlist;
            }
            catch (Exception ex)
            {
                issues.Add(new ImportIssue(1, "File", "The workbook could not be read: " + ex.Message, "Use an unprotected .xlsx or .xlsm workbook with headers in Row 1."));
            }

            e.CallbackData = SerializeImportResult(importedCount, issues);
        }

        private static Dictionary<string, int> BuildHeaderMap(IXLWorksheet sheet)
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

        private static string CellText(IXLWorksheet sheet, int row, int column)
        {
            return sheet.Cell(row, column).GetFormattedString().Trim();
        }

        private static string SerializeImportResult(int importedCount, List<ImportIssue> issues)
        {
            return new JavaScriptSerializer().Serialize(new { ImportedCount = importedCount, Issues = issues });
        }

        public class ImportIssue
        {
            public int Row { get; set; }
            public string Field { get; set; }
            public string Description { get; set; }
            public string SuggestedFix { get; set; }

            public ImportIssue(int row, string field, string description, string suggestedFix)
            {
                Row = row;
                Field = field;
                Description = description;
                SuggestedFix = suggestedFix;
            }
        }

    }
}
