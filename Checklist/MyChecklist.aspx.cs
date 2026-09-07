using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DevExpress.Web;
using HEA.ePTW.Models;
using HEA.ePTW.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace HEA.ePTW.Checklist
{
    public partial class MyChecklist : System.Web.UI.Page
    {
        List<ChecklistModel> chklist;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    UserModel user = UserViewModel.GetLoggedInUserInfo();
                    var project = UserViewModel.GetSelectedProject();
                    chklist = ChecklistViewModel.GetMyChecklistList(user.UserID, project.ToString());
                    Session["Checklist_MyChecklist"] = chklist;
                }
                else
                {
                    chklist = (List<ChecklistModel>)Session["Checklist_MyChecklist"];
                }

                gvTBM.DataSource = chklist;
                gvTBM.DataBind();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Account/SignIn.aspx");
            }
        }
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            return ms.ToArray();
        }
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        protected void gvTBM_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (e.ButtonID == "Select")
            {
                var status = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "StatusText");
                if (status.ToString() == "APPROVED")
                    e.Visible = DevExpress.Utils.DefaultBoolean.True;
                else
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
            }
        }
        protected void gvTBM_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.FieldName == "")
            {
            }
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            ASPxButton btnDownload = sender as ASPxButton;
            GridViewDataItemTemplateContainer container = btnDownload.NamingContainer as GridViewDataItemTemplateContainer;
            string key = container.KeyValue.ToString();

            var result = chklist.FirstOrDefault(item => item.Key == key);

            if (result != null)
            {
                Session["CHK_Record"] = result;

                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/Checklist/NewChecklist.aspx");
                else
                    Response.Redirect("~/Checklist/NewChecklist.aspx");
            }
        }
    }
}