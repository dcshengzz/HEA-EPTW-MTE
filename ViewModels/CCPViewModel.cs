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
    public class CCPViewModel
    {
        public static void CCP_InsertUpdate(CCPModel data)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@ID",data.ID),
                new SqlParameter("@Key",data.Key),
                new SqlParameter("@ProjectName",data.ProjectName),
                new SqlParameter("@EquipmentName",data.EquipmentName),
                new SqlParameter("@KeyActivitiesName",data.KeyActivitiesName),
                new SqlParameter("@ActivitiesDate",data.ActivitiesDate),
                new SqlParameter("@Description",data.Description),
                new SqlParameter("@Latitude",data.Latitude),
                new SqlParameter("@Longitude",data.Longitude),
                new SqlParameter("@Status",data.Status),
                new SqlParameter("@Created",data.Created),
                new SqlParameter("@CreatedBy",data.CreatedBy),
                new SqlParameter("@Updated",data.Updated),
                new SqlParameter("@UpdatedBy",data.UpdatedBy),
                new SqlParameter("@ReturnRejectReason",data.ReturnRejectReason),
                new SqlParameter("@ApprovedBy",data.ApprovedBy),
                new SqlParameter("@ApprovedDate",data.ApprovedDate),
                new SqlParameter("@ReturnRejectDate",data.ReturnRejectDate)
            };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_CCPRecord_InsertUpdate]", Params);
        }
        public static List<QuestionAndAnswerModel> GetCCPDetails(string Key)
        {
            List<QuestionAndAnswerModel> list = new List<QuestionAndAnswerModel>();

            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@Key",Key)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetCCPDetails]", Params);

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
        public static void CCPDetail_InsertUpdate(QuestionAndAnswerModel value)
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
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_CCPDetail_InsertUpdate]", Params);
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




        public static DataSet GetCCPReport(DateTime from, DateTime to)
        {
            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@DateFrom",from),
                new SqlParameter("@DateTo",to)
            };

            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_CCPReport]", Params);

            return dsData;
        }

        public static List<CCPModel> GetMyCCPList(string UserID, string ProjectName)
        {
            List<CCPModel> list = new List<CCPModel>();

            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@ProjectName",ProjectName)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetMyCCPList]", Params);

            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    CCPModel entData = new CCPModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static List<CCPModel> GetPendingCCPList(string UserID, string ProjectName)
        {
            List<CCPModel> list = new List<CCPModel>();

            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@ProjectName",ProjectName)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_CCP_GetPendingList]", Params);

            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    CCPModel entData = new CCPModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        private static CCPModel ConvertDataRowToEntity(DataRow value)
        {
            CCPModel result = new CCPModel();

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
    }
}