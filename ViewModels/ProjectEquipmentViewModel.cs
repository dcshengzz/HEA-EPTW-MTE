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
    public class ProjectEquipmentViewModel
    {
        public static List<ProjectEquipmentModel> GetProjectEquipmentList(string UserID, string ProjectName)
        {
            List<ProjectEquipmentModel> list = new List<ProjectEquipmentModel>();
            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@ProjectName",ProjectName)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetProjectEquipmentList]", Params);
            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    ProjectEquipmentModel entData = new ProjectEquipmentModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static ProjectEquipmentModel ConvertDataRowToEntity(DataRow value)
        {
            ProjectEquipmentModel result = new ProjectEquipmentModel();

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
        public static void ProjectEquipment_InsertUpdate(ProjectEquipmentModel data)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@EquipmentName",data.EquipmentName),
                new SqlParameter("@ProjectName",data.ProjectName),
                new SqlParameter("@Status",data.Status),
                new SqlParameter("@Created",data.Created),
                new SqlParameter("@CreatedBy",data.CreatedBy),
                new SqlParameter("@Updated",data.Updated),
                new SqlParameter("@UpdatedBy",data.UpdatedBy)
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_ProjectEquipment_InsertUpdate]", Params);
        }
        public static void ProjectEquipment_Delete(string EquipmentName, string ProjectName)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@EquipmentName",EquipmentName),
                new SqlParameter("@ProjectName",ProjectName)
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_ProjectEquipment_Delete]", Params);
        }
    }
}