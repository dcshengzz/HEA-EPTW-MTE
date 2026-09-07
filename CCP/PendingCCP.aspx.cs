using DevExpress.Web;
using HEA.ePTW.Models;
using HEA.ePTW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HEA.ePTW.CCP
{
    public partial class PendingCCP : System.Web.UI.Page
    {
        List<CCPModel> ccplist;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    UserModel user = UserViewModel.GetLoggedInUserInfo();
                    var project = UserViewModel.GetSelectedProject();
                    ccplist = CCPViewModel.GetPendingCCPList(user.UserID, project.ToString());
                    Session["CCP_PendingCCP"] = ccplist;
                }
                else
                {
                    ccplist = (List<CCPModel>)Session["CCP_PendingCCP"];
                }

                gvCCP.DataSource = ccplist;
                gvCCP.DataBind();
            }
            catch
            {
                Response.Redirect("~/Account/SignIn.aspx");
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            ASPxButton btnDownload = sender as ASPxButton;
            GridViewDataItemTemplateContainer container = btnDownload.NamingContainer as GridViewDataItemTemplateContainer;
            string key = container.KeyValue.ToString();

            var result = ccplist.FirstOrDefault(item => item.Key == key);

            if (result != null)
            {
                Session["CCP_Record"] = result;

                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/CCP/NewCCP.aspx?Status=P");
                else
                    Response.Redirect("~/CCP/NewCCP.aspx?Status=P");
            }
        }

    }
}