using DocproReport.Customs.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocproReport.Models.Views
{
    public class KhoVatLyModelView
    {
        public KhoVatLyParam SearchParam { get; set; }
        public KhoVatLy khoVatLy { get; set; }
        public List<KhoVatLy> listKhoVatLy { get; set; }
        public string Url { get; set; }
    }
}