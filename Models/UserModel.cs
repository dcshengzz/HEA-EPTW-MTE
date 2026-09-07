using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HEA.ePTW.Models
{
    public class UserModel
    {
        public byte[] Photo { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName {
            get {
                return String.Format("{0}. {1} {2}", Title, LastName, FirstName);
            }
        }
        public string DocumentType { get; set; }
        public string DocumentNo { get; set; }
        public string ContactNo { get; set; }
        public string EmailAddress { get; set; }
        public string ConstructorName { get; set; }
        public string Position { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string Confirm { get; set; }
        public int Status { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
        public string Roles { get; set; }
        public string StatusText
        {
            get
            {
                string strResult = "";

                switch (Status)
                {
                    case 1:
                        strResult = "ACTIVE";
                        break;
                    case 2:
                        strResult = "LOCKED";
                        break;
                }
                return strResult;
            }
        }
        public UserModel()
        {
            InitializeProperties();
        }
        private void InitializeProperties()
        {
            UserID = "";
            FirstName = "";
            LastName = "";
            DocumentType = "";
            DocumentNo = "";
            Position = "";
            ContactNo = "";
            EmailAddress = "";
            Password = "";
            Confirm = "";
            Photo = null;
            Created = DateTime.Now;
            CreatedBy = "";
            Updated = DateTime.Now;
            UpdatedBy = "";
        }
    }
}