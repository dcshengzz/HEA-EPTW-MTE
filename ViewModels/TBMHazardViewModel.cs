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
    public class TBMHazardViewModel
    {
        public static List<TBMHazardModel> GetTBMHazardList(string Key)
        {
            List<TBMHazardModel> list = new List<TBMHazardModel>();
            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@Key",Key),
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetTBMHazardList]", Params);
            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    TBMHazardModel entData = new TBMHazardModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static TBMHazardModel ConvertDataRowToEntity(DataRow value)
        {
            TBMHazardModel result = new TBMHazardModel();

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
        public static void TBMHazard_InsertUpdate(TBMHazardModel data)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@ID",data.ID),
                new SqlParameter("@Key",data.Key),
                new SqlParameter("@WorkActivity",data.WorkActivity),
                new SqlParameter("@Others",data.Others),
                new SqlParameter("@CauseOfHazard",data.CauseOfHazard),
                new SqlParameter("@HappenAsResult",data.HappenAsResult),
                new SqlParameter("@ActionToTaken",data.ActionToTaken),
                new SqlParameter("@ActionRemarks",data.ActionRemarks),
                new SqlParameter("@Created",data.Created),
                new SqlParameter("@CreatedBy",data.CreatedBy),
                new SqlParameter("@Updated",data.Updated),
                new SqlParameter("@UpdatedBy",data.UpdatedBy)
            };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_TBMHazard_InsertUpdate]", Params);
        }
        public static void TBMHazard_Delete(int ID, string Key)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@ID",ID),
                new SqlParameter("@Key",Key)
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_TBMHazard_Delete]", Params);
        }
    }
}