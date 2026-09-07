using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HEA.ePTW.Models
{
    public class RoleModel
    {
        public string RoleID { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }

        public RoleModel()
        {
            InitializeProperties();
        }
        private void InitializeProperties()
        {
            RoleID = "";
            Description = "";
            Status = 1;
            Created = DateTime.Now;
            CreatedBy = "";
            Updated = DateTime.Now;
            UpdatedBy = "";
        }
    }
}