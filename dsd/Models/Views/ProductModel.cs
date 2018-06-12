using DocProModel.Models;
using DocproReport.Customs.Params;
using DocproReport.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocproReport.Models.Views
{
    public class ProductModel
    {
        public string Url { get; set; }
        public List<Product> Products { get; set; }
        public ProductParam SearchParam { get; set; }
        public List<Category> categories { get; set; }
        public List<Version> versions { get; set; }
    }
 
}