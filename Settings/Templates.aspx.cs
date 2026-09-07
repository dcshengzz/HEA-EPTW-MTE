using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Data;
using DevExpress.Web.ASPxTreeList;
using HEA.ePTW.Class;
using HEA.ePTW.Models;
using HEA.ePTW.ViewModels;

namespace HEA.ePTW.Settings
{
    public partial class Templates : System.Web.UI.Page
    {
        List<TemplateModel> templateList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                templateList = TemplateViewModel.GetTemplateList("CCP");
                Session["ePTW_TemplateList"] = templateList;
            }
            else
            {
                templateList = (Session["ePTW_TemplateList"] as List<TemplateModel>);
            }
            gvTemplates.DataSource = templateList;
            gvTemplates.DataBind();
            gvTemplates.FocusedRowIndex = -1;


            DataTable table1 = GetData();
            //ASPxGridView1.DataSource = table1;
            //(ASPxGridView1.Columns["Question"] as GridViewDataColumn).DataItemTemplate = new EditorTemplate();
            //(ASPxGridView1.Columns["Answer"] as GridViewDataColumn).DataItemTemplate = new EditorTemplate();
            //if (!IsPostBack && !IsCallback) ASPxGridView1.DataBind();
        }
        private void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
        protected void gvTemplates_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            ASPxGridView gridView = sender as ASPxGridView;
            if (gridView.IsEditing && e.Column.FieldName == "Module")
            {
                List<ModuleModel> list = ModuleViewModel.GetModueList();
                ASPxComboBox cbModule = e.Editor as ASPxComboBox;

                cbModule.TextField = "Module";
                cbModule.ValueField = "Module";
                cbModule.DataSource = list;
                cbModule.DataBind();
            }
        }
        protected void gvTemplates_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            if (e.NewValues["Module"] == null || e.NewValues["Module"].ToString() == "")
                AddError(e.Errors, gvTemplates.Columns["Module"], "Please select the Module Name.");
            if (e.NewValues["TemplateName"] == null || e.NewValues["TemplateName"].ToString() == "")
                AddError(e.Errors, gvTemplates.Columns["TemplateName"], "Please enter the Template Name.");
            if (e.NewValues["Description"] == null || e.NewValues["Description"].ToString() == "")
                AddError(e.Errors, gvTemplates.Columns["Description"], "Please enter the Description.");
            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
                e.RowError = "Please, correct all errors.";
        }
        protected void gvTemplates_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            //Guid id = (Guid)e.Keys["ID"];
            //TemplateModel ent = templateList.Find(item => item.ID == id);

            //if (ent != null)
            //{
            //    ent.Module = e.NewValues["Module"] != null ? e.NewValues["Module"].ToString() : "";
            //    ent.Description = e.NewValues["Description"] != null ? e.NewValues["Description"].ToString() : "";
            //    ent.TemplateName = e.NewValues["TemplateName"] != null ? e.NewValues["TemplateName"].ToString() : "";
            //    ent.Updated = DateTime.Now;
            //    ent.UpdatedBy = "1111";

            //    TemplateViewModel.Template_InsertUpdate(ent);

            //    Session["ePTW_TemplateList"] = templateList;
            //}

            //e.Cancel = true;
            //gvTemplates.CancelEdit();
            //gvTemplates.DataSource = templateList;
            //gvTemplates.DataBind();
        }
        protected void gvTemplates_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //TemplateModel ent = new TemplateModel();

            //ent.ID = Guid.NewGuid();
            //ent.Module = e.NewValues["Module"] != null ? e.NewValues["Module"].ToString() : "";
            //ent.Description = e.NewValues["Description"] != null ? e.NewValues["Description"].ToString() : "";
            //ent.TemplateName = e.NewValues["TemplateName"] != null ? e.NewValues["TemplateName"].ToString() : "";
            //ent.Status = 1;
            //ent.Created = DateTime.Now;
            //ent.CreatedBy = "1111";
            //ent.Updated = DateTime.Now;
            //ent.UpdatedBy = "1111";

            //templateList.Add(ent);
            //Session["ePTW_TemplateList"] = templateList;

            //TemplateViewModel.Template_InsertUpdate(ent);

            //e.Cancel = true;
            //gvTemplates.CancelEdit();
            //gvTemplates.DataSource = templateList;
            //gvTemplates.DataBind();
        }
        protected void gvTemplates_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }


        private DataTable GetData()
        {
            //if (Session["Data"] == null)
            //{
                DataTable table = new DataTable();
                table.Columns.Add("Question");
                table.Columns.Add("Answer");
                table.Columns.Add("Editor");
                table.Columns.Add("Editor1");

                
                //table.Rows.Add(new object[] { "Hot Work Permit|B|T|U", "", "TextLabel", "" });
                //table.Rows.Add(new object[] { "Part 1 - To be completed by the Requestor|U", "", "TextLabel|U", "" });
               // table.Rows.Add(new object[] { "Date From", "", "DateEdit", "" });
               // table.Rows.Add(new object[] { "Date To", "", "DateEdit", "" });
               // table.Rows.Add(new object[] { "Work to be carried out", "", "TextEdit", "" });
               // table.Rows.Add(new object[] { "Equipment to be used", "", "ComboBox", "" });
               // table.Rows.Add(new object[] { "No. of Workers", "", "SpinEdit" });
               // table.Rows.Add(new object[] { "Part 2 - To be completed by the Permit Approver (Responsible Person)|U", "", "TextLabel|U", "" });
               // table.Rows.Add(new object[] { "1. Area free from combustible or flammable materials|I", "", "RadioButtonList|Yes|No" });
                //table.Rows.Add(new object[] { "2. Equipment use for hot work check in good condition with safety devices in place, hoses and fittings tested without leak, etc|I", "", "RadioButtonList|Yes|No" });
               // table.Rows.Add(new object[] { "3. Gas cylinders properly secured and upright, oil/grease free on oxygen regulators.|I", "", "RadioButtonList|Yes|No" });
                //table.Rows.Add(new object[] { "4. Flashback arrester fitted on fuel and oxygen/air sources and check valve/non-return valve fitted between each gas torch inlet and gas hose|I", "", "RadioButtonList|Yes|No" });
                //table.Rows.Add(new object[] { "5. Ventilation for the area provided|I", "", "RadioButtonList|Yes|No" });
                //table.Rows.Add(new object[] { "6. Use of fire retardant sheets to shield or contain falling sparks|I", "", "RadioButtonList|Yes|No" });
               // table.Rows.Add(new object[] { "7. Arc welding set has low voltage shock preventer, good insulation and approved electric plug/connectors used.|I", "", "RadioButtonList|Yes|No" });
               // table.Rows.Add(new object[] { "8. Contractor or staff has done and submitted the risk assessment|I", "", "RadioButtonList|Yes|No" });
               // table.Rows.Add(new object[] { "9. Warning signs and barricades put up at the work area|I", "", "RadioButtonList|Yes|No" });
               // table.Rows.Add(new object[] { "10. Fire extinguishers available beside the hot work operation area|I", "", "RadioButtonList|Yes|No" });
               // table.Rows.Add(new object[] { "11. PPE for hot work available|I", "", "RadioButtonList|Yes|No" });
              //  table.Rows.Add(new object[] { "12. Fire watch duties identified and person available on site|I", "", "RadioButtonList|Yes|No" });


            //table.Rows.Add(new object[] { "OID No. / MFG No.", "", "TextEdit", "" });
            //table.Rows.Add(new object[] { "Lift No.", "", "TextEdit", "" });
            //table.Rows.Add(new object[] { "Lift Type", "", "TextEdit", "" });
            //table.Rows.Add(new object[] { "From (Date)",  "", "DateEdit" });
            //table.Rows.Add(new object[] { "To (Date)", "", "DateEdit" });
            //table.Rows.Add(new object[] { "No. of Workers", "", "SpinEdit" });




            //table.Rows.Add(new object[] { "What is your name?", "", "TextEdit", "SpinEdit" });
            //table.Rows.Add(new object[] { "How old are you?", "", "SpinEdit" });
            //table.Rows.Add(new object[] { "What is your gender?", "", "ComboBox" });
            //table.Rows.Add(new object[] { "What is your birth date?", "123", "DateEdit" });
            //table.Rows.Add(new object[] { "What is your birth date?", "", "RadioButtonList" });
            //table.Rows.Add(new object[] { "What is your birth date   ?", "", "grid" });
            //Session["Data"] = table;
            //}
            return table;
        }
        private DataTable GetData1()
        {
            //if (Session["Data"] == null)
            //{
                DataTable table = new DataTable();
                table.Columns.Add("Question");
                table.Columns.Add("Answer");
                table.Columns.Add("Editor");
                table.Columns.Add("Editor1");
                //table.Rows.Add(new object[] { "Cold Work Permit|B|T|U", "", "TextLabel", "" });
                //table.Rows.Add(new object[] { "Permit valid from ", "", "DateEdit", "" });
                //table.Rows.Add(new object[] { "Permit valid to", "", "DateEdit", "" });
                //table.Rows.Add(new object[] { "Scope of works", "", "TextEdit", "" });
                //table.Rows.Add(new object[] { "Equipment to be used", "", "ComboBox", "" });
                //table.Rows.Add(new object[] { "No. of Workers", "", "SpinEdit" });
                //table.Rows.Add(new object[] { "Potential Hazards and Controls|U", "", "TextLabel|U", "" });
                //table.Rows.Add(new object[] { "1. Depressurizing|I", "", "RadioButtonList|Yes|No" });
                //table.Rows.Add(new object[] { "2. Draining|I", "", "RadioButtonList|Yes|No" });
                //table.Rows.Add(new object[] { "3. Adequate lighting|I", "", "RadioButtonList|Yes|No" });
                //table.Rows.Add(new object[] { "4. Safety tags and locks|I", "", "RadioButtonList|Yes|No" });
                //table.Rows.Add(new object[] { "5. Electrical isolation|I", "", "RadioButtonList|Yes|No" });
                //table.Rows.Add(new object[] { "6. Mechanical isolation|I", "", "RadioButtonList|Yes|No" });
                //table.Rows.Add(new object[] { "7. Exposure to moving/rotating machinery|I", "", "RadioButtonList|Yes|No" });
                //table.Rows.Add(new object[] { "8. Confined space|I", "", "RadioButtonList|Yes|No" });
                //table.Rows.Add(new object[] { "9. Fall protection|I", "", "RadioButtonList|Yes|No" });
                //table.Rows.Add(new object[] { "10. Standby man|I", "", "RadioButtonList|Yes|No" });
                //table.Rows.Add(new object[] { "11. Ventilate properly|I", "", "RadioButtonList|Yes|No" });
                //table.Rows.Add(new object[] { "12. Warning notice|I", "", "RadioButtonList|Yes|No" });
                //table.Rows.Add(new object[] { "13. Potential flammable/explosive atmosphere|I", "", "RadioButtonList|Yes|No" });
                //table.Rows.Add(new object[] { "14. Potential high temperature|I", "", "RadioButtonList|Yes|No" });
                //table.Rows.Add(new object[] { "15. Potential high pressure|I", "", "RadioButtonList|Yes|No" });
                //table.Rows.Add(new object[] { "16. Potential exposure to hazardous materials|I", "", "RadioButtonList|Yes|No" });
                //Session["Data"] = table;
            //}
            return table;
        }
        protected void ASPxGridView1_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView gvques = sender as ASPxGridView;
            string templatename = gvques.GetMasterRowFieldValues("TemplateName").ToString();

            DataTable table;
            switch (templatename)
            {
                case "Hot Work Permit":
                    table = GetData();
                    break;
                case "Cold Work Permit":
                    table = GetData1();
                    break;
                case "Electrical Work Permit":
                    table = GetDataElectrical();
                    break;
                default:
                    table = GetData1();
                    break;
            }
            (sender as ASPxGridView).DataSource = table;
            ((sender as ASPxGridView).Columns["Question"] as GridViewDataColumn).DataItemTemplate = new EditorTemplate();
            ((sender as ASPxGridView).Columns["Answer"] as GridViewDataColumn).DataItemTemplate = new EditorTemplate();
            if (!IsPostBack && !IsCallback) (sender as ASPxGridView).DataBind();

            //ASPxGridView1.DataSource = table;
            //(ASPxGridView1.Columns["Question"] as GridViewDataColumn).DataItemTemplate = new EditorTemplate();
            //(ASPxGridView1.Columns["Answer"] as GridViewDataColumn).DataItemTemplate = new EditorTemplate();
            //if (!IsPostBack && !IsCallback) ASPxGridView1.DataBind();
        }
        private DataTable GetDataElectrical()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Question");
            table.Columns.Add("Answer");
            table.Columns.Add("Editor");
            table.Columns.Add("Editor1");
            //table.Rows.Add(new object[] { "Electrical Work Permit|B|T|U", "", "TextLabel", "" });
            //table.Rows.Add(new object[] { "Permit valid from ", "", "DateEdit", "" });
            //table.Rows.Add(new object[] { "Permit valid to", "", "DateEdit", "" });
            //table.Rows.Add(new object[] { "Scope of Work", "", "TextEdit", "" });
            //table.Rows.Add(new object[] { "Equipment to be used", "", "ComboBox", "" });
            //table.Rows.Add(new object[] { "No. of Workers", "", "SpinEdit" });
           // table.Rows.Add(new object[] { "Section 1 - Electrical Work Details|U", "", "TextLabel|U", "" });
            table.Rows.Add(new object[] { "1. Is this low voltage work?|I", "", "RadioButtonList|Yes|No" });
            table.Rows.Add(new object[] { "2. Is this high voltage work?|I", "", "RadioButtonList|Yes|No" });
            table.Rows.Add(new object[] { "3. Will work require equipment isolation?|I", "", "RadioButtonList|Yes|No" });
            table.Rows.Add(new object[] { "Section 2 - High Voltage Work|U", "", "TextLabel|U", "" });
            table.Rows.Add(new object[] { "Is HV Switching to be performed?|I", "", "RadioButtonList|Yes|No" });
            table.Rows.Add(new object[] { "Will HV equipment be isolated?|I", "", "RadioButtonList|Yes|No" });
            table.Rows.Add(new object[] { "Will HV equipment be live?|I", "", "RadioButtonList|Yes|No" });
            table.Rows.Add(new object[] { "Is the safety observer suitably trained?(Completed High Voltage Operator Training)|I", "", "RadioButtonList|Yes|No" });
            table.Rows.Add(new object[] { "Section 3|U", "", "TextLabel|U", "" });
            table.Rows.Add(new object[] { "Is the installation to be carried out in a hazardous area? If yes, a Certificate of Electrical Compliance is required.|I", "", "RadioButtonList|Yes|No" });
            table.Rows.Add(new object[] { "Update of the hazardous area equipment dossier on completion of works, with submission of Certificate of Electrical Compliance (CEC)?|I", "", "RadioButtonList|Yes|No" });
            table.Rows.Add(new object[] { "Hazardous Area Qualification Training noted?|I", "", "RadioButtonList|Yes|No" });
            table.Rows.Add(new object[] { "Does the work area require barricading?|I", "", "RadioButtonList|Yes|No" });
            table.Rows.Add(new object[] { "Are warning signs and bunting in place at the work area?|I", "", "RadioButtonList|Yes|No" });
            table.Rows.Add(new object[] { "Are weather conditions suitable for this work to be completed?|I", "", "RadioButtonList|Yes|No" });
            table.Rows.Add(new object[] { "Has applicant completed a JSEA or Procedure for the task/s? (A JSEA or Procedure must be completed, attached to this permit prior to task commencing)|I", "", "RadioButtonList|Yes|No" });
            table.Rows.Add(new object[] { "NB: Details of any alterations or additions made to fixed wiring on Tasports’ facilities must be supplied to TasPorts’ Electrical Contract Licence Holder or his site designee for inclusion on relevant drawings.This permit is valid only for the ’Expected work timing’ period stated above.No work is to be undertaken outside of that covered in Scope of work to be performed.All persons engaged in the work must be suitably trained and licensed.|I", "", "RadioButtonList|Yes|No" });
            return table;
        }
    }
}