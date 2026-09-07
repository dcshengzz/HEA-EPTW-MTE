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
    public class TBMEquipmentViewModel
    {
        public static List<TBMEquipmentModel> GetTBMEquipmentList(string Key)
        {
            List<TBMEquipmentModel> list = new List<TBMEquipmentModel>();
            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@Key",Key),
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetTBMEquipmentList]", Params);
            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    TBMEquipmentModel entData = new TBMEquipmentModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static TBMEquipmentModel ConvertDataRowToEntity(DataRow value)
        {
            TBMEquipmentModel result = new TBMEquipmentModel();

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
        public static void TBMEquipment_InsertUpdate(TBMEquipmentModel data)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@ID",data.ID),
                new SqlParameter("@Key",data.Key),
                new SqlParameter("@EquipmentName",data.EquipmentName),
                new SqlParameter("@RegistrationNo",data.RegistrationNo),
                new SqlParameter("@EquipmentType",data.EquipmentType),
                new SqlParameter("@MachineType",data.MachineType),
                new SqlParameter("@Created",data.Created),
                new SqlParameter("@CreatedBy",data.CreatedBy),
                new SqlParameter("@Updated",data.Updated),
                new SqlParameter("@UpdatedBy",data.UpdatedBy)
            };


            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_TBMEquipment_InsertUpdate]" +
                "", Params);
        }
        public static void TBMEquipment_Delete(string EquipmentName, string Key)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@EquipmentName",EquipmentName),
                new SqlParameter("@Key",Key)
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_TBMEquipment_Delete]", Params);
        }
    }
}