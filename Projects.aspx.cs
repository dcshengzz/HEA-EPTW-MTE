using System.Collections.Generic;
using HEA.ePTW.Model;
using DevExpress.Web;
using HEA.ePTW.Models;
using HEA.ePTW.ViewModels;
using System;
using System.Web;

namespace HEA.ePTW
{
    public partial class Projects : System.Web.UI.Page
    {
        List<ProjectModel> list;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UserModel user = UserViewModel.GetLoggedInUserInfo();
                if (!IsPostBack)
                {
                    list = ProjectViewModel.GetProjectsList(user.UserID);
                    Session["projectlist"] = list;

                }
                else
                {
                    list = (Session["projectlist"] as List<ProjectModel>);
                }

                gvProjects.DataSource = list;
                gvProjects.DataBind();
            }
            catch
            {
                Response.Redirect("~/Account/SignIn.aspx");
            }
        }
        protected void GridView_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["Kind"] = 1;
            e.NewValues["Priority"] = 2;
            e.NewValues["Status"] = 1;
            e.NewValues["IsDraft"] = true;
            e.NewValues["IsArchived"] = false;
        }
        protected void GridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
        }
        protected void gvProjects_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID == "Select")
            {
                var masterkey = gvProjects.GetRowValues(e.VisibleIndex, "Name");
                HttpContext.Current.Session["ePTW_Project"] = masterkey;

                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/ProjectOverview.aspx");
                else
                    Response.Redirect("~/ProjectOverview.aspx");
            }
        }
        
    }
}