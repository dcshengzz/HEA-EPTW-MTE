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
    public class DocumentViewModel
    {
        public static List<DocumentModel> GetDocumentList(string ProjectName)
        {
            List<DocumentModel> list = new List<DocumentModel>();

            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@ProjectName",ProjectName)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetDocumentList]", Params);

            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    DocumentModel entData = new DocumentModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static DocumentModel GetDocumentDetail(int id)
        {
            DocumentModel entData = new DocumentModel();

            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@ID",id)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetDocumentData]", Params);

            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
                entData = ConvertDataRowToEntity(dsData.Tables[0].Rows[0]);

            return entData;
        }
        private static DocumentModel ConvertDataRowToEntity(DataRow value)
        {
            DocumentModel result = new DocumentModel();

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
        public static void Document_InsertUpdate(DocumentModel data)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@ID",data.ID),
                new SqlParameter("@ProjectName",data.ProjectName),
                new SqlParameter("@DocumentType",data.DocumentType),
                new SqlParameter("@Description",data.Description),
                new SqlParameter("@DocumentData",data.DocumentData),
                new SqlParameter("@FileName",data.FileName),
                new SqlParameter("@FileType",data.FileType),
                new SqlParameter("@Status",data.Status),
                new SqlParameter("@Created",data.Created),
                new SqlParameter("@CreatedBy",data.CreatedBy),
                new SqlParameter("@Updated",data.Updated),
                new SqlParameter("@UpdatedBy",data.UpdatedBy)
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_Document_InsertUpdate]", Params);
        }
    }
}