using DocproReport.Customs.Params;
using DocproReport.Models;
using DocproReport.Models.Views;
using DocProUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocProModel.Repository;
using DocProModel.Models;
using DocProLogic;
using DocProModel.Customs.Params;
using DocproReport.Repository;
using DocproReport.Customs.Entities;
using DocproReport.Customs.Enum;
using PetaPoco;
using System.Data;

namespace DocproReport.Controllers
{
    public class HTReportController : BaseController
    {
        #region Kho Vat Ly
        public ActionResult KhoVatLy()
        {
            SetTitle("Quản lý kho Vật lý");
            var searchParam = Utils.Bind<KhoVatLyParam>(DATA);
            var khoVatLy = KhoVatLyRepository.Search(CUser.IDChannel, searchParam, null);
            return GetCustResultOrView(new DocproReport.Customs.Params.ViewParam()
            {
                ViewName = "KhoVatLy",
                ViewNameAjax = "KhoVatLyTable",
                Data = new KhoVatLyModelView()
                {
                    listKhoVatLy = khoVatLy,
                    SearchParam = searchParam,
                    Url = Locate.Url("/htreport/save.html"),
                },
            });
        }
        public ActionResult Save()
        {
            var IDs = Utils.GetInts(DATA, "ID");
            var Mas = Utils.GetStrings(DATA, "Ma");
            var TenKes = Utils.GetStrings(DATA, "TenKe");
            var TenTus = Utils.GetStrings(DATA, "TenTu");
            var Days = Utils.GetStrings(DATA, "Day");
            var KichThuocs = Utils.GetInts(DATA, "KichThuoc");
            var DaSuDungs = Utils.GetInts(DATA, "DaSuDung");
            var ConTrongs = Utils.GetInts(DATA, "ConTrong");
            var TinhTrangs = Utils.GetInts(DATA, "TinhTrang");
            List<KhoVatLy> ListKhoVatLy = new List<KhoVatLy>();
            for (int i = 0; i < IDs.Length; i++)
            {
                var khoVatLy = new KhoVatLy()
                {
                    Ma = Mas[i],
                    TenKe = TenKes[i],
                    TenTu = TenTus[i],
                    Day = Days[i],
                    KichThuoc = KichThuocs[i],
                    DaSuDung = DaSuDungs[i],
                    ConTrong = ConTrongs[i],
                    TinhTrang = TinhTrangs[i],
                    Created = DateTime.Now,
                    CreatedBy= CUser.ID
                };
                ListKhoVatLy.Add(khoVatLy);
            }
            //delete all of them
            long[] ids = KhoVatLyRepository.Instance.GetListOrDefault().Select(o => (long)o.ID).ToArray<long>();
            KhoVatLyRepository.Instance.Deletes(ids);
            //insert new item
            if (Utils.IsNotEmpty(ListKhoVatLy) && KhoVatLyRepository.Instance.Inserts(ListKhoVatLy))
            {
                SetSuccess(Locate.T("Cập nhật bản ghi thành công"));
            }
            else
            {
                SetError(Locate.T("Lỗi: Cập nhật bản ghi không thành công"));
            }
            return RedirectToPath(Locate.Url("/htreport/khovatly.html"));
        }
        public ActionResult DeleteKhoVatly(Customs.Params.DeleteParam deleteParam)
        {
            var khoVatLy = KhoVatLyRepository.Instance.GetById((int)deleteParam.ID);
            if (Utils.IsEmpty(khoVatLy))
            {
                SetError(Locate.T("Kho vật lý không còn tồn tại"));
            }
            else if (KhoVatLyRepository.Instance.Delete(khoVatLy))
                SetSuccess(Locate.T("Xóa Kho vật lý thành công"));
            else
                SetError(Locate.T("Lỗi: xóa kho vật lý không thành công"));
            return RedirectToPath(Locate.Url("/htreport/khovatly.html"));
        }
        public ActionResult ExportKhoVatLy()
        {
            var khoVatLyEntities = new List<KhoVatLyExcel>();
            var searchParam = Utils.Bind<KhoVatLyParam>(DATA);
            var khoVatLy = KhoVatLyRepository.Search(CUser.IDChannel, searchParam, null);
            var count = 0;
            foreach (var item in khoVatLy)
            {
                var khoVatLyExcel = new KhoVatLyExcel()
                {
                    STT = count++,
                    Ma = item.Ma,
                    TenKe = item.TenKe,
                    TenTu = item.TenTu,
                    Day = item.Day,
                    KichThuoc = item.KichThuoc.ToString(),
                    DaSuDung = item.DaSuDung.ToString(),
                    ConTrong = item.ConTrong.ToString(),
                    TinhTrang = Utils.GetDescribe<EnuTinhTrang>((EnuTinhTrang)item.TinhTrang)
                };
                khoVatLyEntities.Add(khoVatLyExcel);
            }
            var dichearder = new Dictionary<string, string>();
            dichearder.Add("STT", "STT");
            dichearder.Add("Ma", Locate.T("Mã Kệ"));
            dichearder.Add("TenKe", Locate.T("Tên Kệ"));
            dichearder.Add("TenTu", Locate.T("Tên Tủ"));
            dichearder.Add("Day", Locate.T("Dãy"));
            dichearder.Add("KichThuoc", Locate.T("Kích Thước"));
            dichearder.Add("DaSuDung", Locate.T("Đã Sử Dụng"));
            dichearder.Add("ConTrong", Locate.T("Còn Trống"));
            dichearder.Add("TinhTrang", Locate.T("Tình Trạng"));
            return ExportExcelCommon<KhoVatLyExcel>(khoVatLyEntities, dichearder, true, "KhoVatLy.xlsx", 4, 1, string.Format("BCKhoVatLy_{0}.xlsx", "excel"), null);
        }
        #endregion
   
    }
}