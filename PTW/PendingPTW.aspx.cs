using DevExpress.Web;
using HEA.ePTW.Models;
using HEA.ePTW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HEA.ePTW.PTW
{
    public partial class PendingPTW : System.Web.UI.Page
    {
        List<PTWModel> ptwlist;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                UserModel user = UserViewModel.GetLoggedInUserInfo();
                var project = UserViewModel.GetSelectedProject();
                ptwlist = PTWViewModel.GetPendingPTWList(user.UserID, project.ToString());
                Session["PTW_PendingPTW"] = ptwlist;
            }
            else
            {
                ptwlist = (List<PTWModel>)Session["PTW_PendingPTW"];
            }

            gvPTW.DataSource = ptwlist;
            gvPTW.DataBind();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            ASPxButton btnDownload = sender as ASPxButton;
            GridViewDataItemTemplateContainer container = btnDownload.NamingContainer as GridViewDataItemTemplateContainer;
            string key = container.KeyValue.ToString();

            var result = ptwlist.FirstOrDefault(item => item.Key == key);

            if (result != null)
            {
                Session["PTW_Record"] = result;

                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/PTW/NewPTW.aspx?Status=P");
                else
                    Response.Redirect("~/PTW/NewPTW.aspx?Status=P");
            }
        }
    }
}