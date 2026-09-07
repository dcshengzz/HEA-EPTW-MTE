using HEA.ePTW.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HEA.ePTW.Settings
{
    public partial class TBMReportAll : System.Web.UI.Page
    {
        DataSet dsMainpowerDetails;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dsMainpowerDetails = ProjectViewModel.Project_MainpowerDetails();
                Session["Project_MainpowerDetails"] = dsMainpowerDetails;

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
    }
}