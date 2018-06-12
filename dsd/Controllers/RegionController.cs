using DocproReport.Customs.Enum;
using DocproReport.Customs.Params;
using DocproReport.Models.UBDT;
using DocproReport.Models.Views;
using DocproReport.Repository;
using DocProUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocproReport.Controllers
{
    public class RegionController : BaseController
    {
        // GET: District
        private string defaultPath = "/district.html";
        private int areaParent = (int)AreaType.Tinh;
        private int areaType = (int)AreaType.Huyen;
        #region Danh sách
        public ActionResult Index()
        {
            var test = @Locate.T("Xóa");
            var searchParam = Utils.Bind<AreaParam>(DATA);
            searchParam.AreaType = areaType;
            var district = AreaRepository.Search(CUser.IDChannel, searchParam, Paging);
            SetTitle(Locate.T("Danh sách Quận/Huyện"));
            return GetCustResultOrView(new DocproReport.Customs.Params.ViewParam
            {
                Data = new DistrictModel
                {
                    Regions = AreaRepository.Instance.GetListByFieldOrDefault(CUser.IDChannel, "AreaType", areaParent),
                    Districts = district,
                    SearchParam = searchParam
                },
                ViewName = "Index",
                ViewNameAjax = "District"
            });
        }

        #endregion

        #region Create
        public ActionResult Create()
        {

            SetTitle(Locate.T("Tạo Quận/Huyện"));
            return GetDialogResultOrView(new DocproReport.Customs.Params.ViewParam
            {
                ViewName = "Create",
                ViewNameAjax = "Create",
                Data = new DistrictModel
                {
                    Regions = AreaRepository.Instance.GetListByFieldOrDefault(CUser.IDChannel, "AreaType", areaParent),
                    District = new Area(),
                    Url = Locate.Url("/district/save.html")
                },
                Width = 800
            }
           );
        }
        public ActionResult Save()
        {
            var ward = Utils.BindCreatedBy<Area>(DATA, CUser.ID);
            if (!IsValidate(ward))
                return GetResultOrRedirectDefault(defaultPath);
            ward.AreaType = areaType;
            ward.IDChannel = CUser.IDChannel;

            if (AreaRepository.Instance.Insert(ward))
                SetSuccess(Locate.T("Thêm quận/huyện thành công"));
            else
                SetError(Locate.T("Thêm quận/huyện không thành công"));
            return GetResultOrRedirectDefault(defaultPath);
        }

        #endregion
        #region Update
        public ActionResult Update(int id)
        {
            SetTitle(Locate.T("Cập nhật quận/huyện"));
            var oWard = AreaRepository.Instance.GetById(id);
            if (Utils.IsEmpty(oWard))
            {
                SetError(Locate.T(" quận/huyện không tồn tại"));
                return GetResultOrRedirectDefault(defaultPath);
            }
            return GetDialogResultOrView(new DocproReport.Customs.Params.ViewParam
            {
                ViewName = "Update",
                ViewNameAjax = "Update",
                Data = new DistrictModel
                {
                    Regions = AreaRepository.Instance.GetListByFieldOrDefault(CUser.IDChannel, "AreaType", areaParent),
                    District = oWard,
                    Url = Locate.Url("/district/change.html")
                },
                Width = 800
            }
           );
        }
        public ActionResult Change()
        {
            var item = AreaRepository.Instance.GetById(Utils.GetInt(DATA, "ID"));
            if (Utils.IsEmpty(item))
            {
                SetError(Locate.T("quận/huyện không tồn tại"));
                return GetResultOrRedirectDefault(defaultPath);
            }
            var ward = Utils.BindUpdatedBy<Area>(item, DATA, CUser.ID);
            if (!IsValidate(ward))
                return GetResultOrRedirectDefault(defaultPath);
            if (AreaRepository.Instance.Update(ward))
                SetSuccess(Locate.T("Cập nhật quận/huyện thành công"));
            else
                SetError(Locate.T("Cập nhật quận/huyện không thành công"));
            return GetResultOrRedirectDefault(defaultPath);
        }
        #endregion

        #region Delete
        public ActionResult IsDelete(DocproReport.Customs.Params.DeleteParam deleteParam)
        {
            var item = AreaRepository.Instance.GetById((int)deleteParam.ID);
            if (Equals(item, null))
            {
                SetWarn(Locate.T("quận/huyện không còn tồn tại"));
                return GetResultOrRedirectDefault(defaultPath);
            }
            return GetDialogResultOrViewDelete(new DocproReport.Customs.Params.DeleteParam
            {
                ID = item.ID,
                RedirectPath = GetRedirectOrDefault(defaultPath),
                Action = Locate.Url("/district/delete.html"),
                BackTitle = Locate.T("Quay lại danh sách quận/huyện"),
                ConfTitle = Locate.T("Xóa  quận/huyện"),
                Title = Locate.T("Xác nhận xóa thông tin  quận/huyện")
            });
        }

        public ActionResult IsDeletes(DocproReport.Customs.Params.DeletesParam deletesParam)
        {
            if (!deletesParam.HasID)
            {
                SetWarn(Locate.T("Bạn chưa chọn  quận/huyện cần xóa"));
                return GetResultOrRedirectDefault(defaultPath);
            }
            var items = AreaRepository.Instance.GetByIds(deletesParam.IDToInts());
            if (Equals(items, null))
            {
                SetWarn(Locate.T(" quận/huyện không còn tồn tại"));
                return GetResultOrRedirectDefault(defaultPath);
            }
            else if (items.Exists(t => t.CreatedBy != CUser.ID))
            {
                SetWarn(Locate.T("Bạn đang xóa  quận/huyện của người khác"));
                return GetResultOrRedirectDefault(defaultPath);
            }

            return GetDialogResultOrViewDeletes(new DocproReport.Customs.Params.DeletesParam
            {
                ID = items.Select(x => (long)x.ID).ToArray(),
                RedirectPath = GetRedirectOrDefault(defaultPath),
                Action = Locate.Url("/district/deletes.html"),
                BackTitle = Locate.T("Quay lại danh sách quận/huyện"),
                ConfTitle = Locate.T("Xóa  quận/huyện đã chọn ?", items.Count),
                Title = Locate.T("Xác nhận xóa quận/huyện")

            });
        }

        public ActionResult Delete(DocproReport.Customs.Params.DeleteParam deleteParam)
        {
            var oWard = AreaRepository.Instance.GetById((int)deleteParam.ID);
            if (Utils.IsEmpty(oWard))
            {
                SetError(Locate.T(" quận/huyện không còn tồn tại"));
            }
            else if (AreaRepository.Instance.Delete(oWard))
                SetSuccess(Locate.T("Xóa  quận/huyện thành công"));
            else
                SetError(Locate.T("Lỗi: xóa  quận/huyện không thành công"));
            return GetResultOrRedirectDefault(defaultPath);
        }

        public ActionResult Deletes(DocproReport.Customs.Params.DeletesParam deletesParam)
        {
            var oWards = AreaRepository.Instance.GetByIds(deletesParam.IDToInts());
            if (!oWards.Any())
                SetError(Locate.T(" quận/huyện không còn tồn tại"));
            else if (AreaRepository.Instance.Deletes(oWards))
                SetSuccess(Locate.T("Xóa {0}  quận/huyện thành công", oWards.Count));
            else
                SetError(Locate.T("Lỗi: xóa  quận/huyện không thành công"));
            return GetResultOrRedirectDefault(defaultPath);
        }
        #endregion

        #region Validate
        private bool IsValidate(Area Ward)
        {
            if (string.IsNullOrEmpty(Ward.Name))
            {
                SetError(Locate.T("Tiêu đề quận/huyện không được để trống"));
            }
            if (AreaRepository.Instance.NameExists(CUser.IDChannel, Ward.Name, Ward.ID))
            {
                SetError(Locate.T("Trùng tên quận/huyện"));
            }
            else if (AreaRepository.Instance.CodeExists(CUser.IDChannel, Ward.Code, Ward.ID))
            {
                SetError(Locate.T("Trùng mã quận/huyện"));
            }
            return !HasError;
        }
        #endregion

    }
}