using DevExpress.Web;
using HEA.ePTW.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HEA.ePTW.CCP
{
    public partial class CCPReport : System.Web.UI.Page
    {
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            gvCCP.SettingsResizing.ColumnResizeMode = (ColumnResizeMode)Enum.Parse(typeof(ColumnResizeMode), "Control", true);
            //gvCCP.SettingsResizing.Visualization = (ResizingMode)Enum.Parse(typeof(ResizingMode), ddlResizingVisualization.Text, true);

            if (!IsPostBack)
            {
                dtDateTo.Date = DateTime.Now;
                dtDateFrom.Date = DateTime.Now.AddMonths(-1);
                ds = CCPViewModel.GetCCPReport(dtDateFrom.Date, dtDateTo.Date);
            }
            else
            {
                if (Session["CCP_Report"] != null)
                {
                    ds = (DataSet)Session["CCP_Report"];

                }
            }
            if (ds != null && ds.Tables.Count > 0)
            {
                gvCCP.DataSource = ds.Tables[0];
                gvCCP.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ds = CCPViewModel.GetCCPReport(dtDateFrom.Date, dtDateTo.Date);
            if (ds != null && ds.Tables.Count > 0)
            {
                Session["CCP_Report"] = ds;
                gvCCP.DataSource = ds.Tables[0];
                gvCCP.DataBind();
            }
        }
    }
}