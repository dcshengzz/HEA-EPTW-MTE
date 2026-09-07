using DevExpress.Web;
using HEA.ePTW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HEA.ePTW.Account
{
    public partial class SignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SignInButton_Click(object sender, EventArgs e)
        {
            FormLayout.FindItemOrGroupByName("GeneralError").Visible = false;
            if (ASPxEdit.ValidateEditorsInContainer(this))
            {
                string strUserName = UserNameTextBox.Text;
                //if (!strUserName.Contains("@")) strUserName = strUserName + "@hitachi.com";
                if (!UserViewModel.SignIn(strUserName, PasswordButtonEdit.Text))
                {
                    GeneralErrorDiv.InnerText = "Invalid login attempt.";
                    FormLayout.FindItemOrGroupByName("GeneralError").Visible = true;
                }
                else
                    Response.Redirect("~/");
            }
        }
    }
}