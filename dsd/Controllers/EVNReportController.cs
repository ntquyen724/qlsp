using DocProLogic;
using DocProLogic.Models;
using DocProModel.Customs.Params;
using DocProModel.Models;
using DocProModel.Repository;
using DocproReport.Controllers;
using DocproReport.Customs.Entities;
using DocproReport.Customs.Params;
using DocproReport.Customs.Utilities;
using DocProUtil;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocproReport.Controllers
{
    
    public class EVNReportController : BaseController
    {

        // private string defaultPath = "/Job.html";
        private long count;
        #region index
        public ActionResult Index(int id=0)
        {
            var param = Utils.Bind<EVNReportParam>(DATA);
            ViewBag.SearchParam = param;
            var stgfileModel = new StgfileLogic(DATA, Paging, CUser, true).Index(id);
            SetTitle(Locate.T("Báo cáo"));
            return GetCustResultOrView(new DocproReport.Customs.Params.ViewParam {
                Data=stgfileModel,
                ViewName="Index",
                ViewNameAjax="StgDocs"
            });
        }
        public ActionResult ExportExcel(int id = 0)
        {
            var stgfileModel = new StgfileLogic(DATA, null, CUser, true).Index(id);
            var param = Utils.Bind<EVNReportParam>(DATA);
            SetTitle(Locate.T("Báo cáo chi tiết"));
            var dichearder = new Dictionary<string, string>();
            dichearder.Add("STT", "STT");
            dichearder.Add("Name", Locate.T("Tên tài liệu"));
            dichearder.Add("Field2", Locate.T("Mã tài liệu"));
            dichearder.Add("Describe", Locate.T("Mô tả"));
            dichearder.Add("Field3", Locate.T("Trạng thái"));
            var list = new List<ExportExcelCommon>();
            ExportExcelCommon exportExcel = new ExportExcelCommon();
            exportExcel.isshowheader = true;
            exportExcel.headers = dichearder;
            exportExcel.rowstart = 2;
            exportExcel.colstart = 1;
            exportExcel.sheetname = "báo cáo";
            exportExcel.itemSheets = AddStatus(stgfileModel.StgDocs);
            list.Add(exportExcel);
            List<ItemExcel> itemexcels = new List<ItemExcel>();
            itemexcels.Add(
              new ItemExcel { Row=1,
                  Col =1,
                  Value =String.Format("Báo cáo : {0}",(stgfileModel.StgDocTypes.FirstOrDefault(t=> t.ID==param.IDDocType)?? new StgDocType()).Name) }  
              );
            #region thống kê itemexcels
            #endregion
            return ExportExcelTest("evnbaocao.xlsx", list, string.Format("baocao_{0}.xlsx", "excel"), CUser.Name, itemexcels);
        }
        private List<StgDoc> AddStatus(List<StgDoc> stgdocs)
        {
            if(Utils.IsNotEmpty(stgdocs))
            {
                foreach (var item in stgdocs)
                {
                    item.Field3 = (item.IsReplaced ? Locate.T("Thay thế") : CUtility.IsDue(item.Saved) ? Locate.T("Hết hiệu lực") : Locate.T("Còn hiệu lực"));
                }
            }
            else 
                return new List<StgDoc>();
            return stgdocs;
        }
        #endregion
    }
}