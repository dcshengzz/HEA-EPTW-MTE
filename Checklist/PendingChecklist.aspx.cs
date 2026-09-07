using DevExpress.Web;
using HEA.ePTW.Models;
using HEA.ePTW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HEA.ePTW.Checklist
{
    public partial class PendingChecklist : System.Web.UI.Page
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
                    chklist = ChecklistViewModel.GetPendingCHKList(user.UserID, project.ToString());
                    Session["CHK_PendingCHK"] = chklist;
                }
                else
                {
                    chklist = (List<ChecklistModel>)Session["CHK_PendingCHK"];
                }

                gvTBM.DataSource = chklist;
                gvTBM.DataBind();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Account/SignIn.aspx");
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
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/Checklist/NewChecklist.aspx?Status=P");
                else
                    Response.Redirect("~/Checklist/NewChecklist.aspx?Status=P");
            }
        }
        protected void gvTBM_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            //if (e.DataColumn.FieldName == "")
            //{
            //    ASPxButton result = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "btnReport") as ASPxButton;
            //    var status = e.GetValue("StatusText");

            //    if (status.ToString().Equals("APPROVED"))
            //        result.Visible = true;
            //    else
            //        result.Visible = false;
            //}
        }
        //protected void btnDownload_Click(object sender, EventArgs e)
        //{
        //    ASPxButton btnDownload = sender as ASPxButton;
        //    GridViewDataItemTemplateContainer container = btnDownload.NamingContainer as GridViewDataItemTemplateContainer;
        //    string key = container.KeyValue.ToString();

        //    var result = tbmlist.FirstOrDefault(item => item.Key == key);

        //    if (result != null)
        //    {
        //        Session["TBM_Record"] = result;

        //        if (Page.IsCallback)
        //            DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/TBM/NewTBM.aspx?Status=P");
        //        else
        //            Response.Redirect("~/TBM/NewTBM.aspx?Status=P");
        //    }
        //}
    }
}