using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web;

namespace HEA.ePTW.Class
{
    public class EditorTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            templateContainer = container as GridViewDataItemTemplateContainer;
            if (templateContainer.Column.FieldName == "Question") container.Controls.Add(CreateQuestion());
            if (templateContainer.Column.FieldName == "Answer") container.Controls.Add(CreateEditor());
        }

        const string EditorTypeField = "Editor";
        GridViewDataItemTemplateContainer templateContainer;

        GridViewDataItemTemplateContainer TemplateContainer { get { return templateContainer; } }

        protected string GetEditorType()
        {
            return Grid.GetRowValues(templateContainer.VisibleIndex, new string[] { EditorTypeField }).ToString();
        }

        private System.Web.UI.WebControls.WebControl CreateQuestion()
        {
            string strQuestion = Grid.GetRowValues(templateContainer.VisibleIndex, new string[] { "Question" }).ToString();
            System.Web.UI.WebControls.WebControl edit = new ASPxLabel();
            edit.ID = "label_" + TemplateContainer.VisibleIndex.ToString();
            (edit as ASPxLabel).Font.Size = 10;
            string[] tmp = strQuestion.Split('|');
            foreach (string opt in tmp)
            {
                switch (opt)
                {
                    case "U":
                        (edit as ASPxLabel).Font.Underline = true;
                        break;
                    case "B":
                        (edit as ASPxLabel).Font.Bold = true;
                        break;
                    case "I":
                        (edit as ASPxLabel).Font.Italic = true;
                        break;
                    case "T":
                        (edit as ASPxLabel).Font.Size = 14;
                        break;
                }
            }
            (edit as ASPxLabel).Text = tmp[0];
            return edit;
        }
        private System.Web.UI.WebControls.WebControl CreateEditor()
        {
            string editorType = GetEditorType();
            System.Web.UI.WebControls.WebControl edit = CreateEditorCore(editorType);
            edit.ID = "editor_" + TemplateContainer.VisibleIndex.ToString();
            edit.Width = Unit.Percentage(100);
            return edit;
        }

        private System.Web.UI.WebControls.WebControl CreateEditorCore(string editorType)
        {
            System.Web.UI.WebControls.WebControl edit = null;
            string[] value = editorType.Split(new[] { '|' }, 2);
            switch (value[0].ToLower())
            {
                case "dateedit":
                    edit = CreateDateEdit();
                    break;
                case "textlabel":
                    edit = CreateLabel();
                    break;

                case "textedit":
                    edit = CreateTextEdit();
                    break;
                case "combobox":
                    edit = CreateComboBox();
                    break;
                case "spinedit":
                    edit = CreateSpinEdit();
                    break;
                //case "radiobutton":
                //    edit = CreateRadioButton();
                //    break;


                case "radiobuttonlist":
                    edit = CreateRadioBttonlist(value[1]);
                    break;


                //case "grid":
                //    edit = CreateASPxGridView();
                //    break;
                //case "radiogrid":
                //    edit = CreateGVRadioButton();
                //    break;


                default:
                    throw new NotSupportedException("");
            }
            return edit;
        }

        #region Editors
        private System.Web.UI.WebControls.WebControl CreateLabel()
        {
            return new ASPxLabel();
        }
        private System.Web.UI.WebControls.WebControl CreateDateEdit()
        {
            return new ASPxDateEdit();
        }



        private System.Web.UI.WebControls.WebControl CreateTextEdit()
        {
            return new ASPxTextBox();
        }
        private System.Web.UI.WebControls.WebControl CreateSpinEdit()
        {
            return new ASPxSpinEdit();
        }
        private System.Web.UI.WebControls.WebControl CreateComboBox()
        {
            ASPxComboBox combo = new ASPxComboBox();
            combo.Items.Add("Male", "0");
            combo.Items.Add("Famale", "1");
            return combo;
        }
        ASPxGridView ASPxGridView_rb;
        private System.Web.UI.WebControls.WebControl CreateGVRadioButton()
        {

            ASPxGridView_rb = new ASPxGridView();
            ASPxGridView_rb.HtmlRowCreated += radioButtonGrid_HtmlRowCreated;
            DataTable table = CreateGVRadioButton_GetData();
            ASPxGridView_rb.DataSource = table;
            GridViewDataColumn tempGVC;
            tempGVC = new GridViewDataColumn();
            tempGVC.FieldName = "Question";

            for (int i = 0; i < 10; i++)
            {
                tempGVC = new GridViewDataColumn();
                tempGVC.FieldName = "A" + Convert.ToString(i);
                ASPxGridView_rb.Columns.Add(tempGVC);
                tempGVC.DataItemTemplate = new EditorTemplate();
            }


            //     (ASPxGridView1.Columns["Answer"] as GridViewDataColumn).DataItemTemplate = new EditorTemplate();
            //     (ASPxGridView1.Columns["aa"] as GridViewDataColumn).DataItemTemplate = new EditorTemplate();





            ASPxGridView_rb.DataBind();

            return ASPxGridView_rb;


        }
        private DataTable CreateGVRadioButton_GetData()
        {
            //if (Session["Data"] == null) {
            DataTable table1 = new DataTable();
            table1.Columns.Add("Question");
            table1.Columns.Add("Answer");
            table1.Columns.Add("Editor");

            table1.Rows.Add(new object[] { "What is your name?", "", "RadioButton" });
            table1.Rows.Add(new object[] { "How old are you?", "", "RadioButton" });
            table1.Rows.Add(new object[] { "What is your gender?", "", "RadioButton" });
            table1.Rows.Add(new object[] { "What is your birth date ?", "123", "RadioButton" });
            table1.Rows.Add(new object[] { "What is your birth date  ?", "", "RadioButton" });
            table1.Rows.Add(new object[] { "What is your birth date   ?", "", "RadioButton" });
            table1.Rows.Add(new object[] { "What is your birth date    ?", "", "RadioButton" });

            //     table.Rows.Add(new object[] { "What is your birth date   ?", "", "radiogrid" });

            //  Session["Data"] = table1;
            //}
            return table1;
        }
        private System.Web.UI.WebControls.WebControl CreateASPxGridView()
        {
            return new ASPxGridView();
        }



        protected void radioButtonGrid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.GridViewRowType.Data) return;
            string ctrlID = "GR" + e.VisibleIndex + "_A0";
            GridViewDataColumn tempGridViewDataColumn = ASPxGridView_rb.Columns["A1"] as GridViewDataColumn;
            ASPxRadioButton rb = ASPxGridView_rb.FindEditRowCellTemplateControl(tempGridViewDataColumn, ctrlID) as ASPxRadioButton;
            // rb = ALWAYS null                                      
        }
        private ASPxRadioButton CreateRadioButton()
        {

            ASPxRadioButton temp = new ASPxRadioButton();
            temp.ID = "GR" + Convert.ToString(templateContainer.VisibleIndex) + "_" + templateContainer.Column;
            // temp.GroupName = "GR" + Convert.ToString(templateContainer.VisibleIndex);


            return temp;
        }






        private ASPxRadioButtonList CreateRadioBttonlist(string value)
        {
            ASPxRadioButtonList tt = new ASPxRadioButtonList();
            string[] tmp = value.Split('|');
            foreach(string opt in tmp)
            {
                tt.Items.Add(opt);
            }
            tt.RepeatLayout = RepeatLayout.Flow;
            tt.RepeatColumns = 5;
            tt.Border.BorderWidth = 0;
            tt.Paddings.Padding = 0;
            return tt;
        }
        #endregion


        ASPxGridView Grid { get { return templateContainer.Grid; } }
    }
}