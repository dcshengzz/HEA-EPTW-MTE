using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HEA.ePTW.Models
{
    public class AttachmentModel
    {
        public int ID { get; set; }
        public string Key { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] Document { get; set; }
        public int Status { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }

        public string FileTypeInText
        {
            get
            {
                string strResult = "";

                switch (FileType)
                {
                    case "IMG":
                        strResult = "Image File";
                        break;
                    case "PDF":
                        strResult = "PDF File";
                        break;
                }
                return strResult;
            }
        }
    }
}