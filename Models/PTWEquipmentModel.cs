using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HEA.ePTW.Models
{
    public class PTWEquipmentModel
    {
        public int ID { get; set; }
        public string Key { get; set; }
        public string EquipmentName { get; set; }
        public string RegistrationNo { get; set; }
        public string EquipmentType { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
    }
}