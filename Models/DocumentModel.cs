using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HEA.ePTW.Models
{
    public class DocumentModel
    {
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public string DocumentType { get; set; }
        public string Description { get; set; }
        public byte[] DocumentData { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public int Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime Updated { get; set; }
    }
}