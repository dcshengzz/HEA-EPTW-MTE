using HEA.ePTW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HEA.ePTW.ViewModels
{
    public class ReportViewModel
    {
        public static List<ReportHeadCountModel> GetHeadCount()
        {
            HttpContext.Current.Session["rptHeadCount"] = GenerateHeadCount();
            return HttpContext.Current.Session["rptHeadCount"] as List<ReportHeadCountModel>;
        }

        static List<ReportHeadCountModel> GenerateHeadCount()
        {
            List<ReportHeadCountModel> data = new List<ReportHeadCountModel> {

                 new ReportHeadCountModel {
                     Constructor = "HEA",
                     Year = 2024,
                     HeadCount = 54
                 },
                 new ReportHeadCountModel {
                     Constructor = "JD & Partners",
                     Year = 2024,
                     HeadCount = 122
                 },
                 new ReportHeadCountModel {
                     Constructor = "Kimly Construction",
                     Year = 2024,
                     HeadCount = 84
                 },
                 new ReportHeadCountModel {
                     Constructor = "CPC Construction",
                     Year = 2024,
                     HeadCount = 152
                 }
            };

            return data;
        }
    }
}