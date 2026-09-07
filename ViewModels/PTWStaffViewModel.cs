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
    public class PTWStaffViewModel
    {
        public static List<PTWStaffModel> GetPTWStaffList(string Key)
        {
            List<PTWStaffModel> list = new List<PTWStaffModel>();
            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@Key",Key),
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetPTWStaffList]", Params);
            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    PTWStaffModel entData = new PTWStaffModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static PTWStaffModel ConvertDataRowToEntity(DataRow value)
        {
            PTWStaffModel result = new PTWStaffModel();

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
        public static void PTWStaff_InsertUpdate(PTWStaffModel data)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@ID",data.ID),
                new SqlParameter("@Key",data.Key),
                new SqlParameter("@UserID",data.UserID),
                new SqlParameter("@FullName",data.FullName),
                new SqlParameter("@Position",data.Position),
                new SqlParameter("@ConstructorName",data.ConstructorName),
                new SqlParameter("@Created",data.Created),
                new SqlParameter("@CreatedBy",data.CreatedBy),
                new SqlParameter("@Updated",data.Updated),
                new SqlParameter("@UpdatedBy",data.UpdatedBy)
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_PTWStaff_InsertUpdate]", Params);
        }
        public static void PTWStaff_Delete(string UserID, string Key)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@Key",Key)
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_PTWStaff_Delete]", Params);
        }
    }
}