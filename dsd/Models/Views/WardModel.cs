using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocproReport.Models;
using DocproReport.Customs.Params;
using DocproReport.Models;
using DocproReport.Models.UBDT;

namespace DocproReport.Models.Views
{
    public class WardModel
    {
        public string Url { get; set; }
        public List<Area> Districts { get; set; }
        public List<Area> Wards { get; set; }
        public Area Ward { get; set; }
        public AreaParam SearchParam { get; set; }
    }
}