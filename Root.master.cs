using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using HEA.ePTW.Model;
using DevExpress.Web;
using HEA.ePTW.ViewModels;
using System.IO;
using System.Drawing;
using HEA.ePTW.Class;
using HEA.ePTW.Models;
using System.Data;

namespace HEA.ePTW {
    public partial class Root : MasterPage {
        public bool EnableBackButton { get; set; }
        protected void Page_Load(object sender, EventArgs e) {
            //if(!string.IsNullOrEmpty(Page.Header.Title))
            //    Page.Header.Title += " - ";
            //Page.Header.Title = Page.Header.Title + "HEA - ePTW";

            Page.Header.DataBind();


            if (Page.AppRelativeVirtualPath.ToLower().Contains("setting"))
            {
                //EnableBackButton = false;
            //    LeftPanel.Visible = false;
            //    //LeftPanel.Visible = false;
            //    //LeftAreaMenu.Visible = false;
            }
            //else
            //{
            //    HideUnusedContent();
            //}
            //tvTableOfContents.DataBind();
            //tvTableOfContents.ExpandAll();
            UpdateUserMenuItemsVisible();
            HideUnusedContent();
            UpdateUserInfo();


            tvRightMenu.Nodes.Clear();
            tvTableOfContents.Nodes.Clear();

            if (!UserViewModel.IsAuthenticated())
            {
                if (!Request.Url.ToString().ToUpper().Contains("SIGNIN"))
                    Response.Redirect("~/Account/SignIn.aspx");
            }
            else
            {
                UserModel ent = UserViewModel.GetLoggedInUserInfo();

                if (UserViewModel.GetSelectedProject() == null)
                {
                    //lblProject.Text = "No selected Project.";
                    if (!Request.Url.ToString().ToUpper().Contains("SETTINGS"))
                    {
                        if (!Request.Url.ToString().ToUpper().Contains("PROJECTS.ASPX"))
                            Response.Redirect("~/Projects.aspx");
                    }

                    TreeViewNode rootn = new TreeViewNode();
                    rootn.Text = "Select Team";
                    rootn.NavigateUrl = "~\\Projects.aspx";
                    tvTableOfContents.Nodes.Add(rootn);
                }
                else
                {
                    string strProjectName = UserViewModel.GetSelectedProject();

                    DataSet dsLeft = UserViewModel.GetLeftMenu(ent.UserID);
                    UpdateLeftMenu(dsLeft);

                    TreeViewNode chtn = new TreeViewNode();
                    chtn.Text = "Change Team";
                    chtn.NavigateUrl = "~\\Projects.aspx";
                    tvTableOfContents.Nodes.Add(chtn);
                }

                //DataSet dsLeft = UserViewModel.GetLeftMenu(ent.UserID);
                //UpdateLeftMenu(dsLeft);
                DataSet dsRight = UserViewModel.GetRightMenu(ent.UserID);
                UpdateRightMenu(dsRight);

                tvTableOfContents.ExpandAll();
            }
        }
        public void UpdateLeftMenu(DataSet value)
        {
            //tvTableOfContents.Nodes.Clear();
            foreach (DataRow dr in value.Tables[0].Rows)
            {
                TreeViewNode tvn = new TreeViewNode();
                tvn.Text = dr["MenuText"].ToString();
                tvn.NavigateUrl = dr["NavigationUrl"].ToString();

                if (dr["Parent"].ToString() == "")
                {
                    tvTableOfContents.Nodes.Add(tvn);
                }
                else
                {
                    tvTableOfContents.Nodes.FindByText(dr["Parent"].ToString()).Nodes.Add(tvn);
                }
            }
            //tvTableOfContents.Nodes.fin.FindByText()
        }
        public void UpdateRightMenu(DataSet value)
        {
            tvRightMenu.Nodes.Clear();
            foreach(DataRow dr in value.Tables[0].Rows)
            {
                TreeViewNode tvn = new TreeViewNode();
                tvn.Text = dr["MenuText"].ToString();
                tvn.NavigateUrl = dr["NavigationUrl"].ToString();
                tvRightMenu.Nodes.Add(tvn);
            }
        }





        protected void HideUnusedContent() {
            //LeftAreaMenu.Items[1].Visible = EnableBackButton;

            //bool hasLeftPanelContent = HasContent(LeftPanelContent);
            //LeftAreaMenu.Items.FindByName("ToggleLeftPanel").Visible = hasLeftPanelContent;
            //LeftPanel.Visible = hasLeftPanelContent;

            //bool hasRightPanelContent = HasContent(RightPanelContent);
            //RightAreaMenu.Items.FindByName("ToggleRightPanel").Visible = hasRightPanelContent;
            //RightPanel.Visible = hasRightPanelContent;

            bool hasPageToolbar = HasContent(PageToolbar);
            PageToolbarPanel.Visible = hasPageToolbar;
        }

        protected bool HasContent(Control contentPlaceHolder) {
            if(contentPlaceHolder == null) return false;

            ControlCollection childControls = contentPlaceHolder.Controls;
            if(childControls.Count == 0) return false;

            return true;
        }

        // SignIn/Register

        protected void UpdateUserMenuItemsVisible() {
            var isAuthenticated = UserViewModel.IsAuthenticated();
            //RightAreaMenu.Items.FindByName("SignInItem").Visible = !isAuthenticated;
            //RightAreaMenu.Items.FindByName("RegisterItem").Visible = !isAuthenticated;
            RightAreaMenu.Items.FindByName("MyAccountItem").Visible = isAuthenticated;
            RightAreaMenu.Items.FindByName("ProfileItem").Visible = isAuthenticated;
            RightAreaMenu.Items.FindByName("SignOutItem").Visible = isAuthenticated;
        }

        protected void UpdateUserInfo() {
            if(UserViewModel.IsAuthenticated()) {
                var user = UserViewModel.GetLoggedInUserInfo();
                var myAccountItem = RightAreaMenu.Items.FindByName("MyAccountItem");
                var myQRCodeItem = RightAreaMenu.Items.FindByName("ProfileItem");
                var userName = (ASPxLabel)myAccountItem.FindControl("UserNameLabel");
                var email = (ASPxLabel)myAccountItem.FindControl("EmailLabel");
                var emptyImage = (HtmlGenericControl)RightAreaMenu.Items[0].FindControl("EmptyImg");
                ASPxBinaryImage accountImage = (ASPxBinaryImage)RightAreaMenu.Items[0].FindControl("AccountImage");
                userName.Text = string.Format("{0}", user.FullName);
                email.Text = user.EmailAddress;
                accountImage.Attributes["class"] = "account-image";

                if (user.Photo == null) // || user.Photo.Length == 0)
                {
                    //emptyImage.Visible = true;
                    //emptyImage.InnerHtml = string.Format("{0}", user.UserName).ToUpper();

                    //accountImage.Visible = false;
                    //accountImage.InnerHtml = string.Format("{0}", user.UserName).ToUpper();
                }
                else
                {
                    ASPxBinaryImage temp = (ASPxBinaryImage)myAccountItem.FindControl("AvatarUrl");
                    temp.ContentBytes = user.Photo;
                    accountImage.ContentBytes = user.Photo;
                }
                //if (string.IsNullOrEmpty(user.Photo))
                //{
                //    accountImage.InnerHtml = string.Format("{0}{1}", user.FirstName[0], user.LastName[0]).ToUpper();
                //}
                //else
                //{
                //    var avatarUrl = (HtmlImage)myAccountItem.FindControl("AvatarUrl");
                //    avatarUrl.Attributes["src"] = ResolveUrl(user.AvatarUrl);
                //    accountImage.Style["background-image"] = ResolveUrl(user.AvatarUrl);
                //}

                ASPxBinaryImage qrImage = (ASPxBinaryImage)myQRCodeItem.FindControl("qrCodeImage");
                Byte[] byteArray;
                var width = 250; // width of the Qr Code   
                var height = 250; // height of the Qr Code   
                var margin = 0;
                var qrCodeWriter = new ZXing.BarcodeWriterPixelData
                {
                    Format = ZXing.BarcodeFormat.QR_CODE,
                    Options = new ZXing.Common.EncodingOptions
                    {
                        Height = height,
                        Width = width,
                        Margin = margin
                    }
                };

                string strData = $"EPTW|https://203.127.105.98/|{user.UserID}";
                string strEncrytedData = SecurityService.Encrypt(strData, "b14ca5898a4e4133bbce2ea2315a1916");
                var pixelData = qrCodeWriter.Write(strEncrytedData);

                using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
                {
                    using (var ms = new MemoryStream())
                    {
                        var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                        try
                        {
                            // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image   
                            System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                        }
                        finally
                        {
                            bitmap.UnlockBits(bitmapData);
                        }
                        // save to stream as PNG   
                        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byteArray = ms.ToArray();

                        qrImage.ContentBytes = byteArray;
                    }
                }
            }
        }

        protected void RightAreaMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e) {
            if (e.Item.Name == "ProfileItem")
            {
                Response.Redirect("~/Account/Profile.aspx");
            }
            if (e.Item.Name == "SignOutItem")
            {
                UserViewModel.SignOut(); // DXCOMMENT: Your Signing out logic
                Response.Redirect("~/");
            }
        }

        protected void ApplicationMenu_ItemDataBound(object source, MenuItemEventArgs e) {
            e.Item.Image.Url = string.Format("Content/Images/{0}.svg", e.Item.Text);
            e.Item.Image.UrlSelected = string.Format("Content/Images/{0}-white.svg", e.Item.Text);
        }
    }
}