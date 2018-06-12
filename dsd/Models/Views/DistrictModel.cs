using DocproReport.Customs.Params;
using DocproReport.Models.UBDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocproReport.Models.Views
{
    public class DistrictModel
    {
        public string Url { get; set; }
        public List<Area> Regions { get; set; }
        public List<Area> Districts { get; set; }
        public Area District { get; set; }
        public AreaParam SearchParam { get; set; }
    }
}