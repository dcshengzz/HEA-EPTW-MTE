using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HEA.ePTW.Models
{
    public class KeyActivitiesModel
    {
        public int ID { get; set; }
        public string Module { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
    }
}