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
    public class EquipmentViewModel
    {
        //public static List<EquipmentModel> GetEquipment_CodeTable()
        //{
        //    List<EquipmentModel> list = new List<EquipmentModel>();

        //    DataSet dsData = new DataSet();
        //    dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_CodeTable_GetEquipmentList]", null);
        //    if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dsData.Tables[0].Rows)
        //        {
        //            EquipmentModel entData = new EquipmentModel();
        //            entData = ConvertDataRowToEntity(dr);
        //            list.Add(entData);
        //        }
        //    }
        //    return list;
        //}
        public static List<EquipmentModel> GetEquipment_ByProjectAndType_CodeTable(string Project, string EquipmentType)
        {
            List<EquipmentModel> list = new List<EquipmentModel>();

            DataSet dsData = new DataSet();

            SqlParameter[] Params =
            {
                new SqlParameter("@ProjectName",Project),
                new SqlParameter("@EquipmentType",EquipmentType)
            };

            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_CodeTable_GetEquipmentList_ByProjectAndType]", Params);

            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    EquipmentModel entData = new EquipmentModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static List<EquipmentModel> GetEquipment_ByProject_CodeTable(string Project)
        {
            List<EquipmentModel> list = new List<EquipmentModel>();

            DataSet dsData = new DataSet();

            SqlParameter[] Params =
            {
                new SqlParameter("@ProjectName",Project),
            };

            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_CodeTable_GetEquipmentList_ByProject]", Params);

            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    EquipmentModel entData = new EquipmentModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static List<EquipmentModel> GetEquipmentList(string strProjectName)
        {
            List<EquipmentModel> list = new List<EquipmentModel>();

            SqlParameter[] Params =
            {
                new SqlParameter("@ProjectName",strProjectName),
            };

            DataSet dsData = new DataSet();
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetEquipmentList]", Params);

            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    EquipmentModel entData = new EquipmentModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static void Equipment_InsertUpdate(EquipmentModel data)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@RegistrationNo",data.RegistrationNo),
                new SqlParameter("@EquipmentName",data.EquipmentName),
                new SqlParameter("@EquipmentType",data.EquipmentType),
                new SqlParameter("@ProjectName",data.ProjectName),
                new SqlParameter("@Description",data.Description),
                new SqlParameter("@Status",data.Status),
                new SqlParameter("@Photo",data.Photo),
                new SqlParameter("@Document",data.Document),
                new SqlParameter("@Created",data.Created),
                new SqlParameter("@CreatedBy",data.CreatedBy),
                new SqlParameter("@Updated",data.Updated),
                new SqlParameter("@UpdatedBy",data.UpdatedBy)
            };


            if (data.Photo == null) Params[6].Value = System.Data.SqlTypes.SqlBinary.Null;
            if (data.Document == null) Params[7].Value = System.Data.SqlTypes.SqlBinary.Null;
            
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_Equipment_InsertUpdate]", Params);
        }
        private static EquipmentModel ConvertDataRowToEntity(DataRow value)
        {
            EquipmentModel result = new EquipmentModel();

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