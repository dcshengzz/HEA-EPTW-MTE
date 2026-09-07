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
    public class TemplateViewModel
    {
        public static List<TemplateModel> GetTemplateList(string Module)
        {
            List<TemplateModel> list = new List<TemplateModel>();

            DataSet dsData = new DataSet();

            SqlParameter[] Params =
            {
                new SqlParameter("@Module", Module)
            };

            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetTemplateList]", Params);

            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    TemplateModel entData = new TemplateModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static List<QuestionAndAnswerModel> GetTemplateDetails(string TemplateName)
        {
            List<QuestionAndAnswerModel> list = new List<QuestionAndAnswerModel>();

            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@TemplateName",TemplateName)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetTemplateDetails]", Params);

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
        public static void Template_InsertUpdate(TemplateModel value)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@ID",value.ID),
                new SqlParameter("@Module",value.Module),
                new SqlParameter("@TemplateName",value.TemplateName),
                new SqlParameter("@Description",value.Description),
                new SqlParameter("@Status",value.Status),
                new SqlParameter("@Created",value.Created),
                new SqlParameter("@CreatedBy",value.CreatedBy),
                new SqlParameter("@Updated",value.Updated),
                new SqlParameter("@UpdatedBy",value.UpdatedBy)
            };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[sp_Template_InsertUpdate]", Params);
        }
        private static TemplateModel ConvertDataRowToEntity(DataRow value)
        {
            TemplateModel result = new TemplateModel();

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