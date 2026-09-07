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
    public class TBMViewModel
    {
        private static TBMModel ConvertDataRowToEntity(DataRow value)
        {
            TBMModel result = new TBMModel();

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
        public static List<TBMModel> GetMyTBMList(string UserID, string ProjectName)
        {
            List<TBMModel> list = new List<TBMModel>();

            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@ProjectName",ProjectName)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetMyTBMList]", Params);

            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    TBMModel entData = new TBMModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static DataSet GetTBMReport(string Key)
        {
            List<TBMModel> list = new List<TBMModel>();

            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@Key",Key)
            };

            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_TBMReport]", Params);

            return dsData;
        }
        public static List<QuestionAndAnswerModel> GetTBMDetails(string Key)
        {
            List<QuestionAndAnswerModel> list = new List<QuestionAndAnswerModel>();

            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@Key",Key)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetTBMDetails]", Params);

            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    QuestionAndAnswerModel entData = new QuestionAndAnswerModel();
                    entData = ConvertDataRowToDetails(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static List<QuestionAndAnswerModel> GetTBMChecklist(string Key)
        {
            List<QuestionAndAnswerModel> list = new List<QuestionAndAnswerModel>();

            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@Key",Key)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetTBMCheckList]", Params);

            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    QuestionAndAnswerModel entData = new QuestionAndAnswerModel();
                    entData = ConvertDataRowToDetails(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        private static QuestionAndAnswerModel ConvertDataRowToDetails(DataRow value)
        {
            QuestionAndAnswerModel result = new QuestionAndAnswerModel();

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
        public static List<TBMModel> GetPendingTBMList(string UserID, string ProjectName)
        {
            List<TBMModel> list = new List<TBMModel>();

            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@ProjectName",ProjectName)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_TBM_GetPendingList]", Params);

            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    TBMModel entData = new TBMModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static void TBM_InsertUpdate(TBMModel data)
        {

            SqlParameter[] Params =
            {
                new SqlParameter("@ID",data.ID),
                new SqlParameter("@Key",data.Key),
                new SqlParameter("@ProjectName",data.ProjectName),
                new SqlParameter("@MeetingDate",data.MeetingDate),
                new SqlParameter("@Supervisor",data.Supervisor),
                new SqlParameter("@ConductedBy",data.ConductedBy),
                new SqlParameter("@ActionsPreviousCompleted",data.ActionsPreviousCompleted),
                new SqlParameter("@Description",data.Description),
                new SqlParameter("@DescriptionPM",data.DescriptionPM),
                new SqlParameter("@Remarks",data.Remarks),
                new SqlParameter("@TodayTeamActionGoal",data.TodayTeamActionGoal),
                new SqlParameter("@TodayTouchAndCall",data.TodayTouchAndCall),
                new SqlParameter("@Feedback",data.Feedback),
                new SqlParameter("@ReturnRejectReason",data.ReturnRejectReason),
                new SqlParameter("@ReturnRejectDate",data.ReturnRejectDate),
                new SqlParameter("@ApprovedBy",data.ApprovedBy),
                new SqlParameter("@ApprovedDate",data.ApprovedDate),
                new SqlParameter("@Latitude",data.Latitude),
                new SqlParameter("@Longitude",data.Longitude),
                new SqlParameter("@Status",data.Status),
                new SqlParameter("@Created",data.Created),
                new SqlParameter("@CreatedBy",data.CreatedBy),
                new SqlParameter("@Updated",data.Updated),
                new SqlParameter("@UpdatedBy",data.UpdatedBy),
                new SqlParameter("@SupervisorName",data.SupervisorName),
                new SqlParameter("@ConductedByName",data.ConductedByName),
                new SqlParameter("@Safety",data.Safety),
                new SqlParameter("@SafetyName",data.SafetyName),
                new SqlParameter("@ConductedPosition",data.ConductedPosition),
                new SqlParameter("@ConductedCompany",data.ConductedCompany),
                new SqlParameter("@ApproveName",data.ApproveName),
                new SqlParameter("@ApprovePosition",data.ApprovePosition),
                new SqlParameter("@ApproveCompany",data.ApproveCompany),
                new SqlParameter("@ApproveRemarks",data.ApproveRemarks),
                new SqlParameter("@SafetyDeclaration1",data.SafetyDeclaration1),
                new SqlParameter("@SafetyDeclaration2",data.SafetyDeclaration2),
                new SqlParameter("@SafetyDeclaration3",data.SafetyDeclaration3),
                new SqlParameter("@SafetyDeclaration4",data.SafetyDeclaration4)
            };

            try
            {
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_TBMRecord_InsertUpdate]", Params);
            }
            catch (Exception ex)
            {

            }
        }
        public static void TBMDetail_InsertUpdate(QuestionAndAnswerModel value)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@ID",value.ID),
                new SqlParameter("@Key",value.Key),
                new SqlParameter("@TemplateName",value.TemplateName),
                new SqlParameter("@Question",value.Question),
                new SqlParameter("@Answer",value.Answer),
                new SqlParameter("@Selection",value.Selection),
                new SqlParameter("@Status",value.Status),
                new SqlParameter("@DocumentStatus",value.DocumentStatus),
                new SqlParameter("@Sort",value.Sort),
                new SqlParameter("@Created",value.Created),
                new SqlParameter("@CreatedBy",value.CreatedBy),
                new SqlParameter("@Updated",value.Updated),
                new SqlParameter("@UpdatedBy",value.UpdatedBy)
            };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_TBMDetail_InsertUpdate]", Params);
        }
        public static void TBMChecklist_InsertUpdate(QuestionAndAnswerModel value)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@ID",value.ID),
                new SqlParameter("@Key",value.Key),
                new SqlParameter("@TemplateName",value.TemplateName),
                new SqlParameter("@Question",value.Question),
                new SqlParameter("@Answer",value.Answer),
                new SqlParameter("@Selection",value.Selection),
                new SqlParameter("@Status",value.Status),
                new SqlParameter("@DocumentStatus",value.DocumentStatus),
                new SqlParameter("@Sort",value.Sort),
                new SqlParameter("@Created",value.Created),
                new SqlParameter("@CreatedBy",value.CreatedBy),
                new SqlParameter("@Updated",value.Updated),
                new SqlParameter("@UpdatedBy",value.UpdatedBy)
            };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_TBMChecklist_InsertUpdate]", Params);
        }
    }
}