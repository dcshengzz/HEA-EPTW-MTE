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
    public class PTWDetailViewModel
    {
        public static List<QuestionAndAnswerModel> GetPTWeDetails(string Key)
        {
            List<QuestionAndAnswerModel> list = new List<QuestionAndAnswerModel>();

            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@Key",Key)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetPTWDetails]", Params);

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
        public static void PTWDetail_InsertUpdate(QuestionAndAnswerModel value)
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
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_PTWDetail_InsertUpdate]", Params);
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
    }
}