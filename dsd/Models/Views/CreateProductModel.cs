using DocProModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocproReport.Models.Views
{
    public class CreateProductModel
    {
        public Product Product { get; set; }
        public List<Category> categories { get; set; }
        public Version version { get; set; }
        public List<User> users { get; set; }
        public string Url { get; set; }
        public Dictionary<int,string> VersionFileTypes { get; set; }

    }
}