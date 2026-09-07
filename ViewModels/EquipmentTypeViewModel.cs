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
    public class EquipmentTypeViewModel
    {
        public static List<EquipmentTypeModel> GetEquipmentTypeList()
        {
            List<EquipmentTypeModel> list = new List<EquipmentTypeModel>();

            DataSet dsData = new DataSet();
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetEquipmentTypeList]", null);

            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    EquipmentTypeModel entData = new EquipmentTypeModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        //public static List<EquipmentTypeModel> GetEquipmentTypeList(string ProjectID)
        //{
        //    List<EquipmentTypeModel> list = new List<EquipmentTypeModel>();

        //    DataSet dsData = new DataSet();
        //    dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[sp_GetEquipmentTypeList]", null);

        //    if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dsData.Tables[0].Rows)
        //        {
        //            EquipmentTypeModel entData = new EquipmentTypeModel();
        //            entData = ConvertDataRowToEntity(dr);
        //            list.Add(entData);
        //        }
        //    }
        //    return list;
        //}
        public static void EquipmentType_InsertUpdate(EquipmentTypeModel data)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@CategoryID",data.CategoryID),
                new SqlParameter("@EquipmentType",data.EquipmentType),
                new SqlParameter("@Description",data.Description),
                new SqlParameter("@Status",data.Status),
                new SqlParameter("@Created",data.Created),
                new SqlParameter("@CreatedBy",data.CreatedBy),
                new SqlParameter("@Updated",data.Updated),
                new SqlParameter("@UpdatedBy",data.UpdatedBy)
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_EquipmentType_InsertUpdate]", Params);
        }
        public static void Equipment_LockUnlock(string EquipmentType, int Status)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@EquipmentType",EquipmentType),
                new SqlParameter("@Status",Status)
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[sp_EquipmentType_LockUnlock]", Params);
        }
        private static EquipmentTypeModel ConvertDataRowToEntity(DataRow value)
        {
            EquipmentTypeModel result = new EquipmentTypeModel();

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