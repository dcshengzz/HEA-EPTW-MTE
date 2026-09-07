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
    public class ProjectViewModel
    {
        //public static List<ProjectModel> GetProjects()
        //{
        //    if (HttpContext.Current.Session["Projects"] == null)
        //        HttpContext.Current.Session["Projects"] = GenerateProjects();
        //    return HttpContext.Current.Session["Projects"] as List<ProjectModel>;
        //}
        public static ProjectModel GetProjectDetails(string ProjectName)
        {
            ProjectModel ent = new ProjectModel();
            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@Name",ProjectName)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetProjectDetails]", Params);
            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                ent = ConvertDataRowToEntity(dsData.Tables[0].Rows[0]);
                return ent;
            }
            else
                return null;
        }
        public static List<ProjectModel> GetProjectsList(string strUser)
        {
            List<ProjectModel> list = new List<ProjectModel>();
            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@UserID", strUser)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetProjectList]", Params);
            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    ProjectModel entData = new ProjectModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static List<ProjectModel> GetProjectSettingList(string strUser)
        {
            List<ProjectModel> list = new List<ProjectModel>();
            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@UserID", strUser)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetProjectSettingList]", Params);
            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    ProjectModel entData = new ProjectModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }

        public static ProjectModel ConvertDataRowToEntity(DataRow value)
        {
            ProjectModel result = new ProjectModel();

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


        public static void Project_InsertUpdate(ProjectModel data)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@Photo",data.Photo),
                new SqlParameter("@Name",data.Name),
                new SqlParameter("@Address",data.Address),
                new SqlParameter("@Description",data.Description),
                new SqlParameter("@ConstructorName",data.ConstructorName),
                new SqlParameter("@StartDate",data.StartDate),
                new SqlParameter("@EndDate",data.EndDate),
                new SqlParameter("@Status",data.Status),
                new SqlParameter("@Created",data.Created),
                new SqlParameter("@CreatedBy",data.CreatedBy),
                new SqlParameter("@Updated",data.Updated),
                new SqlParameter("@UpdatedBy",data.UpdatedBy)
            };

            Params[0].SqlDbType = SqlDbType.VarBinary;
            if (data.Photo == null)
            {
                Params[0].Value = DBNull.Value;
            }

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_Project_InsertUpdate]", Params);
        }



        public static DataSet Project_Summary(string ProjectName)
        {
            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@ProjectName", ProjectName)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetProjectSummary]", Params);
            return dsData;
        }
        public static DataSet Project_Users(string ProjectName)
        {
            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@ProjectName", ProjectName)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetProjectUser]", Params);
            return dsData;
        }
        public static DataSet Project_Mainpower(string ProjectName)
        {
            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@ProjectName", ProjectName)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetProjectManpower]", Params);
            return dsData;
        }
        public static DataSet Project_MainpowerDetails(string ProjectName)
        {
            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@ProjectName", ProjectName)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetProjectManpowerDetails]", Params);
            return dsData;
        }
        public static DataSet Project_MainpowerDetails()
        {
            DataSet dsData = new DataSet();
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetProjectAllManpowerDetails]", null);
            return dsData;
        }
    }
}