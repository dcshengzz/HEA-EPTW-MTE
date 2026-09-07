using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HEA.ePTW.Models
{
    public class ProjectStaffModel
    {
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public string UserID { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string ConstructorName { get; set; }
        public int Status { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }

        public ProjectStaffModel()
        {
        }
    }
}