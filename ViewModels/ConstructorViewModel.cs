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
    public class ConstructorViewModel
    {
        public static List<ConstructorModel> GetConstructorsList(string strLoginID)
        {
            List<ConstructorModel> list = new List<ConstructorModel>();
            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@LoginID",strLoginID)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetConstructorList]", Params);
            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    ConstructorModel entData = new ConstructorModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }
        public static ConstructorModel GetConstructorRecord(string strName)
        {
            ConstructorModel data;
            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@Name",strName)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_GetConstructor]", Params);
            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    data = ConvertDataRowToEntity(dsData.Tables[0].Rows[0]);
                    return data;
                }
            }
            return null;
        }
        public static void ConstructorsInsertUpdate(ConstructorModel data)
        {
            SqlParameter[] Params =
            {
                new SqlParameter("@Name",data.Name),
                new SqlParameter("@Description",data.Description),
                new SqlParameter("@Address",data.Address),
                new SqlParameter("@ContactPerson",data.ContactPerson),
                new SqlParameter("@ContactNumber",data.ContactNumber),
                new SqlParameter("@Status",data.Status),
                new SqlParameter("@Created",data.Created),
                new SqlParameter("@CreatedBy",data.CreatedBy),
                new SqlParameter("@Updated",data.Updated),
                new SqlParameter("@UpdatedBy",data.UpdatedBy)
            };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_Constructor_InsertUpdate]", Params);
        }
        public static ConstructorModel ConvertDataRowToEntity(DataRow value)
        {
            ConstructorModel result = new ConstructorModel();

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

        public static List<ConstructorModel> GetConstructor_CodeTable()
        {
            List<ConstructorModel> list = new List<ConstructorModel>();

            DataSet dsData = new DataSet();
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[Procedure_CodeTable_GetConstructorList]", null);
            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    ConstructorModel entData = new ConstructorModel();
                    entData = ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }






        public static List<ProjectModel> GetConstructorProjectsList(ConstructorModel data)
        {
            List<ProjectModel> list = new List<ProjectModel>();
            DataSet dsData = new DataSet();
            SqlParameter[] Params =
            {
                new SqlParameter("@ConstructorID",data.ID)
            };
            dsData = SqlHelper.ExecuteDataset(SqlHelper.ConnStr, System.Data.CommandType.StoredProcedure, "[dbo].[sp_GetConstructorList]", Params);
            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    ProjectModel entData = new ProjectModel();
                    entData = ProjectViewModel.ConvertDataRowToEntity(dr);
                    list.Add(entData);
                }
            }
            return list;
        }





        //static List<ConstructorModel> GenerateConstructors()
        //{
        //    List<ConstructorModel> constructors = new List<ConstructorModel> {

        //         new ConstructorModel {
        //            ID = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"),
        //            Name = "The Turner Corporation",
        //            Description = "Turner Corporation is a global leader in construction services, with its headquarters in New York City. Known for its vast portfolio, Turner Construction handles a remarkable variety of projects, from healthcare facilities to sports arenas. Its commitment to sustainability and innovation has earned it a solid reputation.",
        //            Status = 1
        //         },
        //         new ConstructorModel {
        //            ID = new Guid("21223344-5566-7788-99AA-BBCCDDEEFF00"),
        //            Name = "Bechtel Corporation",
        //            Description = "Founded in 1898, Bechtel Corporation is one of the oldest construction firms in the United States. Based in Reston, Virginia, Bechtel has a diverse portfolio that spans sectors such as infrastructure, energy, and mining. It is renowned for its ability to handle complex and large-scale projects.",
        //            Status = 1
        //         },
        //         new ConstructorModel {
        //            ID = new Guid("31223344-5566-7788-99AA-BBCCDDEEFF00"),
        //            Name = "STO Building Group, Inc.",
        //            Description = "STO Building Group, with its headquarters in New York, is a prominent player in the commercial construction sector. The company has a strong presence in interior construction, building renovations, and ground-up construction. Its dedication to quality has made it a favorite among corporations.",
        //            Status = 2
        //         },
        //         new ConstructorModel {
        //            ID = new Guid("41223344-5566-7788-99AA-BBCCDDEEFF00"),
        //            Name = "DPR Construction",
        //            Description = "DPR Construction is a forward-thinking general contracting company that has made significant strides in the construction industry since its founding in 1990. Headquartered in San Francisco, California, DPR is particularly well-known for its commitment to sustainability and innovative project delivery..",
        //            Status = 2
        //         },
        //         new ConstructorModel {
        //            ID = new Guid("51223344-5566-7788-99AA-BBCCDDEEFF00"),
        //            Name = "Kiewit Corporation",
        //            Description = "Kiewit Corporation, headquartered in Omaha, Nebraska, is a Fortune 500 company known for its construction, engineering, and mining expertise. Their portfolio includes some of North America’s most significant infrastructure projects, such as highways, bridges, and tunnels.",
        //            Status = 2
        //         },
        //         new ConstructorModel {
        //            ID = new Guid("61223344-5566-7788-99AA-BBCCDDEEFF00"),
        //            Name = "Mastec Inc.",
        //            Description = "Based in Coral Gables, Florida, Mastec Inc. specializes in infrastructure construction, particularly in the energy and telecommunications sectors. Its ability to adapt to the rapidly changing technological landscape has positioned it as a leader in its field.",
        //            Status = 1
        //         },
        //         new ConstructorModel {
        //            ID = new Guid("71223344-5566-7788-99AA-BBCCDDEEFF00"),
        //            Name = "Clark Group",
        //            Description = "Clark Group, headquartered in Bethesda, Maryland, is one of the US’s largest privately held construction companies. It is known for its expertise in building large-scale commercial and institutional projects. Its commitment to excellence and innovation has earned it a stellar reputation.",
        //            Status = 1
        //         },
        //         new ConstructorModel {
        //            ID = new Guid("81223344-5566-7788-99AA-BBCCDDEEFF00"),
        //            Name = "Fluor Corporation",
        //            Description = "Fluor Corporation, based in Irving, Texas, is known for its engineering, procurement, construction, and maintenance services. It serves various industries, including oil and gas, chemicals, power, and infrastructure. Its global presence and diversified portfolio make it a dominant player in the construction industry.",
        //            Status = 2
        //         },
        //         new ConstructorModel {
        //            ID = new Guid("91223344-5566-7788-99AA-BBCCDDEEFF00"),
        //            Name = "The Whiting-Turner Contracting Company",
        //            Description = "Established in 1909, The Whiting-Turner Contracting Company is a prominent construction management and general contracting firm based in Baltimore, Maryland. It is known for its work in various sectors, including healthcare, education, and retail.",
        //            Status = 2
        //         },
        //         new ConstructorModel {
        //            ID = new Guid("A1223344-5566-7788-99AA-BBCCDDEEFF00"),
        //            Name = "Skanska USA",
        //            Description = "Skanska USA, a subsidiary of Swedish construction giant Skanska AB, is a leading construction and development firm headquartered in New York City. Established in the United States in the early 1970s, Skanska USA has earned recognition for its commitment to sustainability, safety, and innovation in construction. The company focuses on various sectors, including commercial, residential, infrastructure, and healthcare projects.",
        //            Status = 2
        //         }

        //    };

        //    return constructors;
        //}
    }
}