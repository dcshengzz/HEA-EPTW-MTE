using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HEA.ePTW.Models
{
    public class TBMHazardModel
    {
        public int ID { get; set; }
        public string Key { get; set; }
        public string WorkActivity { get; set; }
        public string Others { get; set; }
        public string CauseOfHazard { get; set; }
        public string HappenAsResult { get; set; }

        public string ActionToTaken { get; set; }
        public string ActionRemarks { get; set; }

        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
    }
}