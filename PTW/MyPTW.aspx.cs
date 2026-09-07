using CrystalDecisions.CrystalReports.Engine;
using DevExpress.Web;
using HEA.ePTW.Models;
using HEA.ePTW.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
//using System.Web.UI.WebControls;

namespace HEA.ePTW.PTW
{
    public partial class MyPTW : System.Web.UI.Page
    {
        List<PTWModel> ptwlist;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    UserModel user = UserViewModel.GetLoggedInUserInfo();
                    var project = UserViewModel.GetSelectedProject();
                    ptwlist = PTWViewModel.GetPTWList(user.UserID, project.ToString());
                    Session["PTW_MyPTW"] = ptwlist;
                }
                else
                {
                    ptwlist = (List<PTWModel>)Session["PTW_MyPTW"];
                }

                gvPTW.DataSource = ptwlist;
                gvPTW.DataBind();
            }
            catch
            {
                Response.Redirect("~/Account/SignIn.aspx");
            }
        }

        protected void btnLink_Click(object sender, EventArgs e)
        {
            ASPxButton btnDownload = sender as ASPxButton;
            GridViewDataItemTemplateContainer container = btnDownload.NamingContainer as GridViewDataItemTemplateContainer;
            string key = container.KeyValue.ToString();

            var result = ptwlist.FirstOrDefault(item => item.Key == key);

            if (result != null)
            {
                Session["PTW_Record"] = result;

                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/PTW/NewPTW.aspx?Status=D");
                else
                    Response.Redirect("~/PTW/NewPTW.aspx?Status=D");
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            ASPxButton btnDownload = sender as ASPxButton;
            GridViewDataItemTemplateContainer container = btnDownload.NamingContainer as GridViewDataItemTemplateContainer;
            string key = container.KeyValue.ToString();

            DataSet ds = PTWViewModel.GetPTWReport(key);

            if (ds != null)
            {
                try
                {
                    ds.Tables[0].TableName = "tbl_PTWRecord";
                    ds.Tables[1].TableName = "tbl_PTWStaffs";
                    ds.Tables[2].TableName = "tbl_PTWEquipments";
                    ds.Tables[3].TableName = "tbl_PTWDetails";
                    ds.Tables[4].TableName = "tbl_Attachment";



                    foreach (DataRow dr in ds.Tables[4].Rows)
                    {
                        dr["Document"] = imageToByteArray(byteArrayToImage((Byte[])dr["Document"]));
                    }

                    ReportDocument repdoc = new ReportDocument();
                    repdoc = new HEA.ePTW.Reports.PTWReport();
                    repdoc.SetDataSource(ds);
                    string strFileName = "PTW_" + key;   // + ".PDF";
                    repdoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, strFileName);
                    Response.Flush();
                    repdoc.Dispose();
                }
                catch (Exception ex)
                {
                }
            }
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            return ms.ToArray();
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
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
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/PTW/NewPTW.aspx?Status=D");
                else
                    Response.Redirect("~/PTW/NewPTW.aspx?Status=D");
            }
        }
    }
}