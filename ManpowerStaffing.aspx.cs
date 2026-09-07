using HEA.ePTW.Models;
using HEA.ePTW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HEA.ePTW
{
    public partial class ManpowerStaffing : System.Web.UI.Page
    {
        List<UserModel> staffs;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (HttpContext.Current.Session["ePTW_User"] != null && HttpContext.Current.Session["ePTW_Project"] != null)
                {
                    string strUserID = UserViewModel.GetLoggedInUserInfo().UserID;
                    string ProjectID = UserViewModel.GetSelectedProject();
                    staffs = UserViewModel.GetPersonnelList(strUserID, ProjectID);
                    Session["ePTW_StaffList"] = staffs;
                }

            }
            else
            {
                staffs = (Session["ePTW_StaffList"] as List<UserModel>);
            }

            gvStaffs.DataSource = staffs;
            gvStaffs.DataBind();
            gvStaffs.FocusedRowIndex = -1;
        }
    }
}