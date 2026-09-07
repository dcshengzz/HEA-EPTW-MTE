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
    public class ProjectStaffViewModel
    {
        public static List<ProjectStaffModel> GetProjectStaffList(string UserID, string ProjectName)
        {
            List<ProjectStaffModel> list = new List<ProjectStaffModel>();
            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@ProjectName",ProjectName)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetProjectStaffList]", Params);
            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    ProjectStaffModel entData = new ProjectStaffModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static ProjectStaffModel ConvertDataRowToEntity(DataRow value)
        {
            ProjectStaffModel result = new ProjectStaffModel();

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
        public static void ProjectStaff_InsertUpdate(ProjectStaffModel data)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@ProjectName",data.ProjectName),
                new SqlParameter("@UserID",data.UserID),
                new SqlParameter("@Status",data.Status),
                new SqlParameter("@Created",data.Created),
                new SqlParameter("@CreatedBy",data.CreatedBy),
                new SqlParameter("@Updated",data.Updated),
                new SqlParameter("@UpdatedBy",data.UpdatedBy)
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_ProjectStaff_InsertUpdate]", Params);
        }
        public static void ProjectStaff_Delete(string UserID, string ProjectName)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@ProjectName",ProjectName),
                new SqlParameter("@UserID",UserID)
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_ProjectStaff_Delete]", Params);
        }
    }
}