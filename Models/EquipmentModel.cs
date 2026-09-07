using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HEA.ePTW.Models
{
    public class EquipmentModel
    {
        public string RegistrationNo { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentType { get; set; }
        public string Description { get; set; }
        public string ProjectName { get; set; }
        public int Status { get; set; }
        public byte[] Photo { get; set; }
        public byte[] Document { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
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
        public EquipmentModel()
        {
            InitializeProperties();
        }
        private void InitializeProperties()
        {
            RegistrationNo = "";
            Description = "";
            Status = 1;
            Photo = null;
            Document = null;
            Created = DateTime.Now;
            CreatedBy = "";
            Updated = DateTime.Now;
            UpdatedBy = "";
        }
    }
}