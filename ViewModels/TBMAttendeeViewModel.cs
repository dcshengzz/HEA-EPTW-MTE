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
    public class TBMAttendeeViewModel
    {
        public static List<TBMAttendeeModel> GetTBMAttendeeList(string Key)
        {
            List<TBMAttendeeModel> list = new List<TBMAttendeeModel>();
            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@Key",Key),
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetTBMAttendeeList]", Params);
            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    TBMAttendeeModel entData = new TBMAttendeeModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static TBMAttendeeModel ConvertDataRowToEntity(DataRow value)
        {
            TBMAttendeeModel result = new TBMAttendeeModel();

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
        public static void TBMAttendee_InsertUpdate(TBMAttendeeModel data)
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


            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_TBMAttendee_InsertUpdate]", Params);
        }
        public static void TBMAttendee_Delete(string UserID, string Key)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@Key",Key)
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_TBMAttendee_Delete]", Params);
        }
    }
}