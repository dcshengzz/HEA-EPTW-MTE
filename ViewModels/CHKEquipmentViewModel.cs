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
    public class CHKEquipmentViewModel
    {
        public static List<CHKEquipmentModel> GetCHKEquipmentList(string Key)
        {
            List<CHKEquipmentModel> list = new List<CHKEquipmentModel>();
            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@Key",Key),
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetCHKEquipmentList]", Params);
            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    CHKEquipmentModel entData = new CHKEquipmentModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static CHKEquipmentModel ConvertDataRowToEntity(DataRow value)
        {
            CHKEquipmentModel result = new CHKEquipmentModel();

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
        public static void CHKEquipment_InsertUpdate(CHKEquipmentModel data)
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


            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_CHKEquipment_InsertUpdate]" +
                "", Params);
        }
        public static void CHKEquipment_Delete(string EquipmentName, string Key)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@EquipmentName",EquipmentName),
                new SqlParameter("@Key",Key)
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_CHKEquipment_Delete]", Params);
        }
    }
}