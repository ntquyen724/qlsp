using DocProModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocproReport.Models
{
    public class DocproQLKDataContext<T> : DataContext<T> where T : class, new()
    {
        public DocproQLKDataContext()
            : base("LocalConnectionString")
        {
        }
    }
}