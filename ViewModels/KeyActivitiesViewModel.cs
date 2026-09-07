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
    public class KeyActivitiesViewModel
    {
        public static List<KeyActivitiesModel> GetKeyActivitiesList(string ModuleName)
        {
            List<KeyActivitiesModel> list = new List<KeyActivitiesModel>();
            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@ModuleName",ModuleName)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_KeyActivitiesList]", Params);
            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    KeyActivitiesModel entData = new KeyActivitiesModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }

        public static KeyActivitiesModel ConvertDataRowToEntity(DataRow value)
        {
            KeyActivitiesModel result = new KeyActivitiesModel();

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