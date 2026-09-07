using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HEA.ePTW.Models
{
    public class EquipmentTypeModel
    {
        public int ID { get; set; }
        public string CategoryID { get; set; }
        public string EquipmentType { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime Updated { get; set; }

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
        public EquipmentTypeModel()
        {
            Status = 1;
            Description = "";
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }
    }
}