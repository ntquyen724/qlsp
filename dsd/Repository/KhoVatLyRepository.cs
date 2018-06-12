using DocproReport.Customs.Params;
using DocproReport.Models;
using DocProUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocproReport.Repository
{
    public class KhoVatLyRepository : KhoVatLy
    {
        public static List<KhoVatLy> Search(int idchannel, KhoVatLyParam param, Pagination paging)
        {
            var sqlPage = Instance.SqlBuilder(idchannel)
                    .WhereIsTrue(Utils.IsNotEmpty(param.Term), String.Format("TenKe Like N'%{0}%' ", param.Term));
            return Instance.GetListOrDefault(sqlPage, paging, "");
        }
    }
}