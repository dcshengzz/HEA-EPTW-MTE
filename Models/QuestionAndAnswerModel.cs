using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HEA.ePTW.Models
{
    public class QuestionAndAnswerModel
    {
        public int ID { get; set; }
        public string Key { get; set; }
        public string TemplateName { get; set; }
        public string Question { get; set; }
        public string Selection { get; set; }
        public string Answer { get; set; }
        public int Sort { get; set; }
        public int Status { get; set; }
        public int DocumentStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime Updated { get; set; }
    }
}