using DevExpress.Web;
using HEA.ePTW.Models;
using HEA.ePTW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HEA.ePTW.Settings
{
    public partial class PlanProjectSetting : System.Web.UI.Page
    {
        List<ProjectModel> list;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                list = ProjectViewModel.GetProjectsList("");
                Session["projectlist"] = list;
                gvProjects.DataSource = list;
                gvProjects.DataBind();
            }
            else
            {
                list = (Session["projectlist"] as List<ProjectModel>);
                gvProjects.DataSource = list;
                gvProjects.DataBind();
            }
        }

        protected void gvConstructors_BeforePerformDataSelect(object sender, EventArgs e)
        {
            //ASPxGridView TempGrid = sender as ASPxGridView;
            //var masterkey = TempGrid.GetMasterRowKeyValue();

            //if (masterkey != null)
            //{
            //    Guid ProjectID = Guid.Parse(masterkey.ToString());
            //    List<ProjectConstructorModel> constructorlist = ProjectConstructorViewModel.GetProjectConstructorsList("", ProjectID);
            //    TempGrid.DataSource = constructorlist;
            //}            
        }
    }
}