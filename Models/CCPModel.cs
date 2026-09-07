using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HEA.ePTW.Models
{
    public class CCPModel
    {
        public int ID { get; set; }
        public string Key { get; set; }
        public string ProjectName { get; set; }
        public string EquipmentName { get; set; }
        public string KeyActivitiesName { get; set; }
        public DateTime ActivitiesDate { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int Status { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
        public string ReturnRejectReason { get; set; }
        public DateTime? ReturnRejectDate { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public string StatusText
        {
            get
            {
                string strResult = "";

                switch (Status)
                {
                    case 0:
                        strResult = "NEW";
                        break;
                    case 1:
                        strResult = "SUBMITTED";
                        break;
                    case 2:
                        strResult = "COMPLETED";
                        break;
                    case 97:
                        strResult = "DELETED";
                        break;
                    case 98:
                        strResult = "RETURNED";
                        break;
                    case 99:
                        strResult = "REJECTED";
                        break;
                }
                return strResult;
            }
        }
        public CCPModel()
        {
            Status = 0;
            ActivitiesDate = DateTime.Now;
        }
    }
}