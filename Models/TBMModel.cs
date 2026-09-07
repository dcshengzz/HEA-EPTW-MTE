using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HEA.ePTW.Models
{
    public class TBMModel
    {
        public int ID { get; set; }
        public string Key { get; set; }
        public string ProjectName { get; set; }
        public DateTime MeetingDate { get; set; }
        public string Supervisor { get; set; }
        public string ConductedBy { get; set; }
        public string Safety { get; set; }

        public string TodayTeamActionGoal { get; set; }
        public string TodayTouchAndCall { get; set; }
        public string Feedback { get; set; }

        public string ActionsPreviousCompleted { get; set; }
        public string Description { get; set; }
        public string DescriptionPM { get; set; }
        public string Remarks { get; set; }

        public int Status { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ReturnRejectReason { get; set; }
        public DateTime? ReturnRejectDate { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
        public string SupervisorName { get; set; }
        public string ConductedByName { get; set; }
        public string ConductedPosition { get; set; }
        public string ConductedCompany { get; set; }

        public string SafetyDeclaration1 { get; set; }
        public string SafetyDeclaration2 { get; set; }
        public string SafetyDeclaration3 { get; set; }
        public string SafetyDeclaration4 { get; set; }

        public string ApproveName { get; set; }
        public string ApprovePosition { get; set; }
        public string ApproveCompany { get; set; }
        public string ApproveRemarks { get; set; }
        public string SafetyName { get; set; }
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
                        strResult = "APPROVED";
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

        public TBMModel()
        {
            Status = 0;
            MeetingDate = DateTime.Now;
            SafetyDeclaration1 = "N";
            SafetyDeclaration2 = "N";
            SafetyDeclaration3 = "N";
            SafetyDeclaration4 = "N";
        }
    }
}