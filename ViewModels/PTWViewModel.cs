using HEA.ePTW.Class;
using HEA.ePTW.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace HEA.ePTW.ViewModels
{
    public class PTWViewModel
    {
        private static PTWModel ConvertDataRowToEntity(DataRow value)
        {
            PTWModel 
result = new PTWModel();

            foreach (DataColumn col in value.Table.Columns)
            {
                PropertyInfo prop = result.GetType().GetProperty(col.ColumnName);
                if (prop != null && value[col] != DBNull.Value)
                {
                    string test = prop.PropertyType.ToString();
                    if (prop.PropertyType.ToString() == "System.Boolean")
                        prop.SetValue(result, ((bool)value[col]), null);
                    else
                        prop.SetValue(result, value[col], null);
                }
            }
            return result;
        }
        public static List<PTWModel> GetPTWList(string UserID, string ProjectName)
        {
            List<PTWModel> list = new List<PTWModel>();

            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@ProjectName",ProjectName)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetPTWList]", Params);

            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    PTWModel entData = new PTWModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static PTWModel GetPTWRecord(string key)
        {
            PTWModel ptw = new PTWModel();

            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@Key",key)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetPTW]", Params);

            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                ptw = ConvertDataRowToEntity(dsData.Tables[0].Rows[0]);
            }
            return ptw;
        }
        public static DataSet GetPTWReport(string Key)
        {
            List<TBMModel> list = new List<TBMModel>();

            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@Key",Key)
            };

            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_PTWReport]", Params);

            return dsData;
        }
        public static List<PTWModel> GetPendingPTWList(string UserID, string ProjectName)
        {
            List<PTWModel> list = new List<PTWModel>();

            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@ProjectName",ProjectName)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_PTW_GetPendingList]", Params);

            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    PTWModel entData = new PTWModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static void PTW_InsertUpdate(PTWModel data)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@ID",data.ID),
                new SqlParameter("@Key",data.Key),
                new SqlParameter("@ProjectName",data.ProjectName),
                new SqlParameter("@RequestBy",data.RequestBy),
                new SqlParameter("@RequestName",data.RequestName),
                new SqlParameter("@RequestPosition",data.RequestPosition),
                new SqlParameter("@RequestCompany",data.RequestCompany),
                new SqlParameter("@RequestDate",data.RequestDate),
                new SqlParameter("@RequestRemarks",data.RequestRemarks),
                new SqlParameter("@AssessBy",data.AssessBy),
                new SqlParameter("@AssessName",data.AssessName),
                new SqlParameter("@AssessPosition",data.AssessPosition),
                new SqlParameter("@AssessCompany",data.AssessCompany),
                new SqlParameter("@AssessDate",data.AssessDate),
                new SqlParameter("@AssessRemarks",data.AssessRemarks),
                new SqlParameter("@ApproveBy",data.ApproveBy),
                new SqlParameter("@ApproveName",data.ApproveName),
                new SqlParameter("@ApprovePosition",data.ApprovePosition),
                new SqlParameter("@ApproveCompany",data.ApproveCompany),
                new SqlParameter("@ApproveDate",data.ApproveDate),
                new SqlParameter("@ApproveRemarks",data.ApproveRemarks),
                new SqlParameter("@CloseBy",data.CloseBy),
                new SqlParameter("@CloseName",data.CloseName),
                new SqlParameter("@ClosePosition",data.ClosePosition),
                new SqlParameter("@CloseCompany",data.CloseCompany),
                new SqlParameter("@CloseDate",data.CloseDate),
                new SqlParameter("@CloseRemarks",data.CloseRemarks),
                new SqlParameter("@ClosureBy",data.ClosureBy),
                new SqlParameter("@ClosureName",data.ClosureName),
                new SqlParameter("@ClosurePosition",data.ClosurePosition),
                new SqlParameter("@ClosureCompany",data.ClosureCompany),
                new SqlParameter("@ClosureDate",data.ClosureDate),
                new SqlParameter("@ClosureRemarks",data.ClosureRemarks),
                new SqlParameter("@DateFrom",data.DateFrom),
                new SqlParameter("@DateTo",data.DateTo),
                new SqlParameter("@WorkType",data.WorkType),
                new SqlParameter("@Description",data.Description),
                new SqlParameter("@ReturnRejectReason",data.ReturnRejectReason),
                new SqlParameter("@ReturnRejectBy",data.ReturnRejectBy),
                new SqlParameter("@ReturnRejectDate",data.ReturnRejectDate),
                new SqlParameter("@Latitude",data.Latitude),
                new SqlParameter("@Longitude",data.Longitude),
                new SqlParameter("@Status",data.Status),
                new SqlParameter("@TotalWorker",data.TotalWorker),
                new SqlParameter("@EquipmentName",data.EquipmentName),
                new SqlParameter("@SafetyOfficerBy",data.SafetyOfficerBy),
                new SqlParameter("@SafetyOfficerName",data.SafetyOfficerName),
                new SqlParameter("@SafetyOfficerPosition",data.SafetyOfficerPosition),
                new SqlParameter("@SafetyOfficerCompany",data.SafetyOfficerCompany),
                new SqlParameter("@SafetyOfficerDate",data.SafetyOfficerDate),
                new SqlParameter("@SafetyOfficerRemarks",data.SafetyOfficerRemarks),
                new SqlParameter("@Day1",data.Day1),
                new SqlParameter("@Day1RemarkByApplicant",data.Day1RemarkByApplicant),
                new SqlParameter("@Day1RemarkByAssesser",data.Day1RemarkByAssesser),
                new SqlParameter("@Day1VerifiedByApplicant",data.Day1VerifiedByApplicant),
                new SqlParameter("@Day1VerifiedByAssesser",data.Day1VerifiedByAssesser),
                new SqlParameter("@Day2",data.Day2),
                new SqlParameter("@Day2RemarkByApplicant",data.Day2RemarkByApplicant),
                new SqlParameter("@Day2RemarkByAssesser",data.Day2RemarkByAssesser),
                new SqlParameter("@Day2VerifiedByApplicant",data.Day2VerifiedByApplicant),
                new SqlParameter("@Day2VerifiedByAssesser",data.Day2VerifiedByAssesser),
                new SqlParameter("@Day3",data.Day3),
                new SqlParameter("@Day3RemarkByApplicant",data.Day3RemarkByApplicant),
                new SqlParameter("@Day3RemarkByAssesser",data.Day3RemarkByAssesser),
                new SqlParameter("@Day3VerifiedByApplicant",data.Day3VerifiedByApplicant),
                new SqlParameter("@Day3VerifiedByAssesser",data.Day3VerifiedByAssesser),
                new SqlParameter("@Day4",data.Day4),
                new SqlParameter("@Day4RemarkByApplicant",data.Day4RemarkByApplicant),
                new SqlParameter("@Day4RemarkByAssesser",data.Day4RemarkByAssesser),
                new SqlParameter("@Day4VerifiedByApplicant",data.Day4VerifiedByApplicant),
                new SqlParameter("@Day4VerifiedByAssesser",data.Day4VerifiedByAssesser),
                new SqlParameter("@Day5",data.Day5),
                new SqlParameter("@Day5RemarkByApplicant",data.Day5RemarkByApplicant),
                new SqlParameter("@Day5RemarkByAssesser",data.Day5RemarkByAssesser),
                new SqlParameter("@Day5VerifiedByApplicant",data.Day5VerifiedByApplicant),
                new SqlParameter("@Day5VerifiedByAssesser",data.Day5VerifiedByAssesser),
                new SqlParameter("@Day6",data.Day6),
                new SqlParameter("@Day6RemarkByApplicant",data.Day6RemarkByApplicant),
                new SqlParameter("@Day6RemarkByAssesser",data.Day6RemarkByAssesser),
                new SqlParameter("@Day6VerifiedByApplicant",data.Day6VerifiedByApplicant),
                new SqlParameter("@Day6VerifiedByAssesser",data.Day6VerifiedByAssesser),
                new SqlParameter("@Day7",data.Day7),
                new SqlParameter("@Day7RemarkByApplicant",data.Day7RemarkByApplicant),
                new SqlParameter("@Day7RemarkByAssesser",data.Day7RemarkByAssesser),
                new SqlParameter("@Day7VerifiedByApplicant",data.Day7VerifiedByApplicant),
                new SqlParameter("@Day7VerifiedByAssesser",data.Day7VerifiedByAssesser),
                new SqlParameter("@Created",data.Created),
                new SqlParameter("@CreatedBy",data.CreatedBy),
                new SqlParameter("@Updated",data.Updated),
                new SqlParameter("@UpdatedBy",data.UpdatedBy)
            };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_PTWRecord_InsertUpdate]", Params);
        }
    }
}