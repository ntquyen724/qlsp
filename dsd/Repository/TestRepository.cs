
using DocproReport.Customs.Params;
using DocproReport.Models;
using DocProUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocproReport.Repository
{
    public class TestRepository:UserName
    {
        public static List<UserName> Search(int idchannel, TestParam param, Pagination paging)
        {
            var i = Instance.SqlBuilder(idchannel)
                    .WhereSearchMeta(param.Term)
                    .WhereIsTrue(1 > 0, "UserType =@0", 1);
            return Instance.GetListOrDefault(i, paging, "");
           
        }
    }
}