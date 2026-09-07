using HEA.ePTW.Class;
using HEA.ePTW.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace HEA.ePTW.ViewModels
{
    public class UserViewModel
    {
        public static List<UserModel> GetUserList(string strLoginID)
        {
            List<UserModel> list = new List<UserModel>();

            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@UserID",strLoginID)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetUserList]", Params);

            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    UserModel entData = new UserModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static UserModel GetUser(string strUserID)
        {
            UserModel result = new UserModel();

            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@UserID",strUserID)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetUser]", Params);

            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                result = ConvertDataRowToEntity(dsData.Tables[0].Rows[0]);
                return result;
            }
            else
                return null;
        }
        public static List<UserModel> GetUser_CodeForModule(string strProject)
        {
            List<UserModel> list = new List<UserModel>();

            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@ProjectName",strProject)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_CodeTable_GetUserListForModule]", Params);

            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    UserModel entData = new UserModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static List<UserModel> GetUser_UserListForMainCons()
        {
            List<UserModel> list = new List<UserModel>();

            DataSet dsData = new DataSet();
            //SqlParameter[] Params =
            //{
            //    new SqlParameter("@ProjectName",strProject)
            //};
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_CodeTable_GetUserListForMainCons]", null);

            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    UserModel entData = new UserModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static void ResetPassword(string UserID)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@UserID",UserID)
            };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_User_ResetPassword]", Params);
        }

        //public static List<UserModel> GetPersonnelList(string UserID, string ProjectID)
        //{
        //    List<UserModel> list = new List<UserModel>();
        //    DataSet dsData = new DataSet();
        //    SqlParameter[] Params =
        //    {
        //        new SqlParameter("@UserID",UserID),
        //        new SqlParameter("@ProjectID",ProjectID)
        //    };
        //    dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[sp_GetPersonnelList]", Params);

        //    if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dsData.Tables[0].Rows)
        //        {
        //            UserModel entData = new UserModel();
        //            entData = ConvertDataRowToEntity(dr);
        //            list.Add(entData);
        //        }
        //    }
        //    return list;
        //}

        public static void User_InsertUpdate(UserModel data)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@Photo",data.Photo),
                new SqlParameter("@Title",data.Title),
                new SqlParameter("@FirstName",data.FirstName),
                new SqlParameter("@LastName",data.LastName),
                new SqlParameter("@DocumentType",data.DocumentType),
                new SqlParameter("@DocumentNo",data.DocumentNo),
                new SqlParameter("@ContactNo",data.ContactNo),
                new SqlParameter("@EmailAddress",data.EmailAddress),
                new SqlParameter("@ConstructorName",data.ConstructorName),
                new SqlParameter("@Position",data.Position),
                new SqlParameter("@UserID",data.UserID),
                new SqlParameter("@Password",data.Password),
                new SqlParameter("@Status",data.Status),
                new SqlParameter("@Created",data.Created),
                new SqlParameter("@CreatedBy",data.CreatedBy),
                new SqlParameter("@Updated",data.Updated),
                new SqlParameter("@UpdatedBy",data.UpdatedBy)
            };

            Params[0].SqlDbType = SqlDbType.VarBinary;
            if (data.Photo == null)
            {
                Params[0].Value = DBNull.Value;
            }

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_User_InsertUpdate]", Params);
        }
        public static void User_LockUnlock(string UserID, int Status)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@Status",Status)
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_User_LockUnlock]", Params);
        }


        private static UserModel ConvertDataRowToEntity(DataRow value)
        {
            UserModel result = new UserModel();

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



        public static bool SignIn(string strUserID, string strPassword)
        {
            List<UserModel> list = new List<UserModel>();

            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@UserID",strUserID),
                new SqlParameter("@Password",strPassword)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetUserSignIn]", Params);

            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    UserModel entData = new UserModel();
                    entData = ConvertDataRowToEntity(dsData.Tables[0].Rows[0]);
                    HttpContext.Current.Session["ePTW_User"] = entData;
                    HttpContext.Current.Session["ePTW_Project"] = null;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void SignOut()
        {
            HttpContext.Current.Session["ePTW_User"] = null;
            HttpContext.Current.Session["ePTW_Project"] = null;
        }
        public static bool IsAuthenticated()
        {
            return GetLoggedInUserInfo() != null;
        }
        public static UserModel GetLoggedInUserInfo()
        {
            return HttpContext.Current.Session["ePTW_User"] as UserModel;
        }
        public static string GetSelectedProject()
        {
            return HttpContext.Current.Session["ePTW_Project"] == null ? null : HttpContext.Current.Session["ePTW_Project"].ToString();
        }




        public static DataSet GetRightMenu(string strUserID)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@UserID",strUserID)
            };

            return SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[procedure_GetUserRightMenu]", Params);
        }
        public static DataSet GetLeftMenu(string strUserID)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@UserID",strUserID)
            };

            return SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[procedure_GetUserLeftMenu]", Params);
        }
    }
}