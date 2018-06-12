using System;
using System.Collections.Generic;
using DocProUtil;
using DocproReport.Models;
using DocproReport.Models;
using DocproReport.Customs.Params;
using DocproReport.Customs.Params;

namespace DocproReport.Repository
{
    public class WardRepository : Ward
    {
        public static List<Ward> Search(int idchannel, WardParam param, Pagination paging)
        {
            return Instance.GetListOrDefault(
                    Instance.SqlBuilder(idchannel)
                    .WhereSearchMeta(param.Term)
                    , paging, "ISNULL(Updated,Created) DESC");
        }
    }
}