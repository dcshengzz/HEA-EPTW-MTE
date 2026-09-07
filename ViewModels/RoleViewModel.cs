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
    public class RoleViewModel
    {
        public static List<RoleModel> GetRoleList(string strLoginID)
        {
            List<RoleModel> list = new List<RoleModel>();

            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@LoginID",strLoginID)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetRoleList]", Params);

            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    RoleModel entData = new RoleModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }

        private static RoleModel ConvertDataRowToEntity(DataRow value)
        {
            RoleModel result = new RoleModel();

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