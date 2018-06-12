using System;
using System.Collections.Generic;
using DocProUtil;
using DocproReport.Models;
using DocproReport.Models;
using DocproReport.Customs.Params;
using DocproReport.Models.UBDT;

namespace DocproReport.Repository
{
    public class AreaRepository : Area
    {
        public static List<Area> Search(int idchannel, AreaParam param, Pagination paging)
        {
            return Instance.GetListOrDefault(
                    Instance.SqlBuilder(idchannel)
                    .WhereSearchMeta(param.Term)
                    .WhereIsTrue(param.AreaType > 0, "AreaType =@0", param.AreaType)
                    , paging, "ISNULL(Updated,Created) DESC");
        }
        public static List<Area> SearchByCoupon(int idchannel, int idCoupon)
        {
            return Instance.GetListOrDefault(
                    Instance.SqlBuilder()
                    .Where("ID in (SELECT IDArea FROM CouponAreaLimit WHERE IDCoupon=@0)", idCoupon));
        }
       
    }
}