using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DevExpress.Web;
using HEA.ePTW.Models;
using HEA.ePTW.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
//using System.Web.UI.WebControls;

namespace HEA.ePTW.TBM
{
    public partial class MyTBM : System.Web.UI.Page
    {
        List<TBMModel> tbmlist;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    UserModel user = UserViewModel.GetLoggedInUserInfo();
                    var project = UserViewModel.GetSelectedProject();
                    tbmlist = TBMViewModel.GetMyTBMList(user.UserID, project.ToString());
                    Session["TBM_MyTBM"] = tbmlist;
                }
                else
                {
                    tbmlist = (List<TBMModel>)Session["TBM_MyTBM"];
                }

                gvTBM.DataSource = tbmlist;
                gvTBM.DataBind();
            }
            catch (Exception ex)
            { 
                Response.Redirect("~/Account/SignIn.aspx");
            }
        }
        protected void btnReport_Click(object sender, EventArgs e)
        {
            ASPxButton btnDownload = sender as ASPxButton;
            GridViewDataItemTemplateContainer container = btnDownload.NamingContainer as GridViewDataItemTemplateContainer;
            string key = container.KeyValue.ToString();

            DataSet ds = TBMViewModel.GetTBMReport(key);

            if (ds != null)
            {
                try
                {
                    ds.Tables[0].TableName = "tbl_TBMRecord";
                    ds.Tables[1].TableName = "tbl_TBMAttendees";
                    ds.Tables[2].TableName = "tbl_TBMHazard";
                    ds.Tables[3].TableName = "tbl_Attachment";

                    foreach (DataRow dr in ds.Tables[3].Rows)
                    {
                        dr["Document"] = imageToByteArray(byteArrayToImage((Byte[])dr["Document"]));
                    }
                    ReportDocument repdoc = new ReportDocument();
                    repdoc = new HEA.ePTW.Reports.TBMReport();
                    repdoc.SetDataSource(ds);

                    string strFileName = "TBM_" + key;   // + ".PDF";
                    repdoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, strFileName);
                    Response.Flush();
                    repdoc.Dispose();
                    //string filename = "TBM_" + key + ".pdf";
                    //string pdfFilePath = Server.MapPath("~/Outputs/") + filename;
                    //WriteLog(pdfFilePath);
                    //repdoc.ExportToDisk(ExportFormatType.PortableDocFormat, pdfFilePath);
                    //Response.Clear();
                    //Response.ContentType = "application/pdf";
                    //Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                    //Response.TransmitFile(pdfFilePath);
                    //Response.End();
                }
                catch(Exception ex)
                {
                    //WriteLog(ex.Message.ToString());
                }
            }
        }
        //private void WriteLog(string text)
        //{
        //    // Set a variable to the Documents path.
        //    string docPath = "c:\\temp\\log\\WriteFile.txt";

        //    using (StreamWriter sw = new StreamWriter(docPath))
        //    {
        //        sw.WriteLine(DateTime.Now.ToString("dd/MMM/yyyy hh:mm") + " : " +  text);
        //    }
        //}
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
        protected void gvTBM_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (e.ButtonID == "Select")
            {
                var status = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "StatusText");
                if (status.ToString() == "APPROVED")
                    e.Visible = DevExpress.Utils.DefaultBoolean.True;
                else
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
            }
        }
        protected void gvTBM_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.FieldName == "")
            {
                //ASPxButton result = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "btnReport") as ASPxButton;
                //var status = e.GetValue("StatusText");

                //if (status.ToString().Equals("APPROVED"))
                //    result.Visible = true;
                //else
                //    result.Visible = false;
            }
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            ASPxButton btnDownload = sender as ASPxButton;
            GridViewDataItemTemplateContainer container = btnDownload.NamingContainer as GridViewDataItemTemplateContainer;
            string key = container.KeyValue.ToString();

            var result = tbmlist.FirstOrDefault(item => item.Key == key);

            if (result != null)
            {
                Session["TBM_Record"] = result;

                if (Page.IsCallback)
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/TBM/NewTBM.aspx");
                else
                    Response.Redirect("~/TBM/NewTBM.aspx");
            }
        }
    }
}