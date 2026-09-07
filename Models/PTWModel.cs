using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HEA.ePTW.Models
{
    public class PTWModel
    {
        public int ID { get; set; }
        public string Key { get; set; }
        public string ProjectName { get; set; }
        public string RequestBy { get; set; }
        public string RequestName { get; set; }
        public string RequestPosition { get; set; }
        public string RequestCompany { get; set; }
        public DateTime? RequestDate { get; set; }
        public string RequestRemarks { get; set; }
        public string AssessBy { get; set; }
        public string AssessName { get; set; }
        public string AssessPosition { get; set; }
        public string AssessCompany { get; set; }
        public DateTime? AssessDate { get; set; }
        public string AssessRemarks { get; set; }
        public string ApproveBy { get; set; }
        public string ApproveName { get; set; }
        public string ApprovePosition { get; set; }
        public string ApproveCompany { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string ApproveRemarks { get; set; }
        public string CloseBy { get; set; }
        public string CloseName { get; set; }
        public string ClosePosition { get; set; }
        public string CloseCompany { get; set; }
        public DateTime? CloseDate { get; set; }
        public string CloseRemarks { get; set; }
        public string ClosureBy { get; set; }
        public string ClosureName { get; set; }
        public string ClosurePosition { get; set; }
        public string ClosureCompany { get; set; }
        public DateTime? ClosureDate { get; set; }
        public string ClosureRemarks { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string EquipmentName { get; set; }
        public int TotalWorker { get; set; }



        public string SafetyOfficerBy { get; set; }
        public string SafetyOfficerName { get; set; }
        public string SafetyOfficerPosition { get; set; }
        public string SafetyOfficerCompany { get; set; }
        public string SafetyOfficerRemarks { get; set; }
        public DateTime? SafetyOfficerDate { get; set; }
        public DateTime? Day1 { get; set; }
        public string Day1RemarkByApplicant { get; set; }
        public string Day1RemarkByAssesser { get; set; }
        public DateTime? Day1VerifiedByApplicant { get; set; }
        public DateTime? Day1VerifiedByAssesser { get; set; }
        public DateTime? Day2 { get; set; }
        public string Day2RemarkByApplicant { get; set; }
        public string Day2RemarkByAssesser { get; set; }
        public DateTime? Day2VerifiedByApplicant { get; set; }
        public DateTime? Day2VerifiedByAssesser { get; set; }
        public DateTime? Day3 { get; set; }
        public string Day3RemarkByApplicant { get; set; }
        public string Day3RemarkByAssesser { get; set; }
        public DateTime? Day3VerifiedByApplicant { get; set; }
        public DateTime? Day3VerifiedByAssesser { get; set; }
        public DateTime? Day4 { get; set; }
        public string Day4RemarkByApplicant { get; set; }
        public string Day4RemarkByAssesser { get; set; }
        public DateTime? Day4VerifiedByApplicant { get; set; }
        public DateTime? Day4VerifiedByAssesser { get; set; }
        public DateTime? Day5 { get; set; }
        public string Day5RemarkByApplicant { get; set; }
        public string Day5RemarkByAssesser { get; set; }
        public DateTime? Day5VerifiedByApplicant { get; set; }
        public DateTime? Day5VerifiedByAssesser { get; set; }
        public DateTime? Day6 { get; set; }
        public string Day6RemarkByApplicant { get; set; }
        public string Day6RemarkByAssesser { get; set; }
        public DateTime? Day6VerifiedByApplicant { get; set; }
        public DateTime? Day6VerifiedByAssesser { get; set; }
        public DateTime? Day7 { get; set; }
        public string Day7RemarkByApplicant { get; set; }
        public string Day7RemarkByAssesser { get; set; }
        public DateTime? Day7VerifiedByApplicant { get; set; }
        public DateTime? Day7VerifiedByAssesser { get; set; }

        public string WorkType { get; set; }
        public string Description { get; set; }
        public string ReturnRejectReason { get; set; }
        public string ReturnRejectBy { get; set; }
        public DateTime? ReturnRejectDate { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int Status { get; set; }
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
                    case 0:
                        strResult = "NEW";
                        break;
                    case 1:
                        strResult = "SUBMITTED";
                        break;
                    case 2:
                        strResult = "ASSESSED";
                        break;
                    case 3:
                        strResult = "VERIFIED";
                        break;
                    case 4:
                        strResult = "APPROVED";
                        break;
                    case 5:
                        strResult = "CLOSED";
                        break;
                    case 6:
                        strResult = "CLOSURE REVIEW";
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
                    case 100:
                        strResult = "REVOKED";
                        break;
                }
                return strResult;
            }
        }
        public PTWModel()
        {
            Status = 0;
            WorkType = "";
            DateFrom = DateTime.Now;
            DateTo = DateTime.Now;
            RequestBy = "";
            RequestName = "";
            RequestPosition = "";
            AssessBy = "";
            AssessName = "";
            AssessPosition = "";
            AssessCompany = "";
            AssessDate = null;
            ApproveBy = "";
            ApproveName = "";
            ApprovePosition = "";
            ApproveCompany = "";
            ApproveDate = null;
            CloseBy = "";
            CloseName = "";
            ClosePosition = "";
            CloseCompany = "";
            CloseDate = null;
            ClosureBy = "";
            ClosureName = "";
            ClosurePosition = "";
            ClosureCompany = "";
            ClosureDate = null;
            Description = "";
            ReturnRejectReason = "";
            ReturnRejectBy = "";
            ReturnRejectDate = null;

            SafetyOfficerBy = "";
            SafetyOfficerName = "";
            SafetyOfficerPosition = "";
            SafetyOfficerCompany = "";
            SafetyOfficerRemarks = "";
            SafetyOfficerDate = null;
            Day1 = null;
            Day1RemarkByApplicant = "";
            Day1RemarkByAssesser = "";
            Day1VerifiedByApplicant = null;
            Day1VerifiedByAssesser = null;
            Day2 = null;
            Day2RemarkByApplicant = "";
            Day2RemarkByAssesser = "";
            Day2VerifiedByApplicant = null;
            Day2VerifiedByAssesser = null;
            Day3 = null;
            Day3RemarkByApplicant = "";
            Day3RemarkByAssesser = "";
            Day3VerifiedByApplicant = null;
            Day3VerifiedByAssesser = null;
            Day4 = null;
            Day4RemarkByApplicant = "";
            Day4RemarkByAssesser = "";
            Day4VerifiedByApplicant = null;
            Day4VerifiedByAssesser = null;
            Day5 = null;
            Day5RemarkByApplicant = "";
            Day5RemarkByAssesser = "";
            Day5VerifiedByApplicant = null;
            Day5VerifiedByAssesser = null;
            Day6 = null;
            Day6RemarkByApplicant = "";
            Day6RemarkByAssesser = "";
            Day6VerifiedByApplicant = null;
            Day6VerifiedByAssesser = null;
            Day7 = null;
            Day7RemarkByApplicant = "";
            Day7RemarkByAssesser = "";
            Day7VerifiedByApplicant = null;
            Day7VerifiedByAssesser = null;

        }
    }
}