using DocproReport.Customs.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocproReport.Customs.Entities
{
    public class ExportExcelCommon
    {
        public dynamic itemSheets { get; set; }
        public string sheetname { get; set; }
        public  Dictionary<string, string> headers { get; set; }
        public bool isshowheader { get; set; }
        public int rowstart { get; set; }
        public int colstart { get; set; }
        public List<ItemExcel> itemexcels { get; set; }


    }
}