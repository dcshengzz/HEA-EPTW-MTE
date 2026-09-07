using HEA.ePTW.Models;
using HEA.ePTW.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HEA.ePTW
{
    public partial class ProjectOverview : System.Web.UI.Page
    {
        ProjectModel project;
        DataSet dsMainpowerDetails;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (UserViewModel.GetSelectedProject() != null)
                {
                    project = ProjectViewModel.GetProjectDetails(UserViewModel.GetSelectedProject());
                    lblTitle.Text = $"Overview ({project.Name})";
                    ImgProject.ContentBytes = project.Photo;
                    lblProject.Text = project.Name;
                    lblLocation.Text = project.Address;
                    lblDescription.Text = project.Description;
                    lblMainCons.Text = project.ConstructorName;

                    ConstructorModel cont = ConstructorViewModel.GetConstructorRecord(project.ConstructorName);
                    if (cont != null)
                    {
                        lblContact.Text = $"{cont.ContactPerson} (Number : {cont.ContactNumber})";
                    }

                    DataSet ds = ProjectViewModel.Project_Summary(project.Name);
                    if (ds.Tables.Count > 0)
                    {
                        DataRow[] ptwresult = ds.Tables[0].Select("Module = 'PTW'");
                        if (ptwresult.Length > 0)
                        {
                            DataTable dt = ptwresult.CopyToDataTable();
                            WebChartPTW.DataSource = dt;
                            WebChartPTW.DataBind();
                        }
                        DataRow[] tbmresult = ds.Tables[0].Select("Module = 'TBM'");
                        if (tbmresult.Length > 0)
                        {
                            DataTable dt = tbmresult.CopyToDataTable();
                            WebChartTBM.DataSource = dt;
                            WebChartTBM.DataBind();
                        }
                        DataRow[] ccpresult = ds.Tables[0].Select("Module = 'CCP'");
                        if (ccpresult.Length > 0)
                        {
                            DataTable dt = ccpresult.CopyToDataTable();
                            WebChartCCP.DataSource = dt;
                            WebChartCCP.DataBind();
                        }
                    }

                    //DataSet dsUsers = ProjectViewModel.Project_Users(project.Name);
                    //if (ds.Tables.Count > 0)
                    //{
                    //    WebChartControlUsers.DataSource = dsUsers.Tables[0];
                    //    WebChartControlUsers.DataBind();
                    //}

                    DataSet dsMainpower = ProjectViewModel.Project_Mainpower(project.Name);
                    if (dsMainpower.Tables.Count > 0)
                    {
                        wcManpower.DataSource = dsMainpower.Tables[0];
                        wcManpower.DataBind();
                    }

                    dsMainpowerDetails = ProjectViewModel.Project_MainpowerDetails(project.Name);
                    Session["Project_MainpowerDetails"] = dsMainpowerDetails;
                }
                else
                {
                    if (Page.IsCallback)
                        DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/Projects.aspx");
                    else
                        Response.Redirect("~/Projects.aspx");
                }
            }
            else
            {
                if (Session["Project_MainpowerDetails"] != null) dsMainpowerDetails = (DataSet)Session["Project_MainpowerDetails"];
            }

            if (dsMainpowerDetails != null && dsMainpowerDetails.Tables.Count > 0)
            {
                gvDetails.DataSource = dsMainpowerDetails.Tables[0];
                gvDetails.DataBind();
            }
        }

        protected void WebChartPTW_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {

        }
    }
}