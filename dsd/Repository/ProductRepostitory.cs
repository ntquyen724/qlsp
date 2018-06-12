using DocproReport.Customs.Params;
using DocproReport.Models;
using DocProUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocproReport.Repository
{
    public class ProductRepostitory:Product
    {
        public static List<Product> Search(ProductParam param, Pagination paging)
        {
            return Instance.GetListOrDefault(
                    Instance.SqlBuilder()
                    .WhereSearchMeta(param.Term, "Name")
                    , paging, "");
        }


    }
}