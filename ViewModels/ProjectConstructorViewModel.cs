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
    public class ProjectConstructorViewModel
    {
        public static List<ProjectConstructorModel> GetProjectConstructorsList(string ProjectName)
        {
            List<ProjectConstructorModel> list = new List<ProjectConstructorModel>();
            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@ProjectName",ProjectName)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetProjectConstructorsList]", Params);
            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    ProjectConstructorModel entData = new ProjectConstructorModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static ProjectConstructorModel ConvertDataRowToEntity(DataRow value)
        {
            ProjectConstructorModel result = new ProjectConstructorModel();

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
        public static void ProjectConstructor_InsertUpdate(ProjectConstructorModel data)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@ProjectName",data.ProjectName),
                new SqlParameter("@ConstructorName",data.ConstructorName),
                new SqlParameter("@Status",data.Status),
                new SqlParameter("@Created",data.Created),
                new SqlParameter("@CreatedBy",data.CreatedBy),
                new SqlParameter("@Updated",data.Updated),
                new SqlParameter("@UpdatedBy",data.UpdatedBy)
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_ProjectConstructor_InsertUpdate]", Params);
        }
        public static void ProjectConstructor_Delete(string ProjectName, string ConstructorName)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@ProjectName",ProjectName),
                new SqlParameter("@ConstructorName",ConstructorName)
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_ProjectConstructor_Delete]", Params);
        }
    }
}