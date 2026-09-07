using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HEA.ePTW.Models
{
    public class ProjectModel
    {
        public int ID { get; set; }
        public byte[] Photo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        public string ConstructorName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
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
        public ProjectModel()
        {
            Status = 1;
            Name = "";
            Description = "";
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }
    }
}