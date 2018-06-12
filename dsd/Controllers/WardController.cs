using DocProModel.Models;
using DocProModel.Repository;
using DocproReport.Controllers;
using DocproReport.Customs.Enum;
using DocproReport.Customs.Params;
using DocproReport.Models;
using DocproReport.Models.Views;
using DocproReport.Repository;
using DocproReport.Models.UBDT;
using DocproReport.Repository;
using DocProUtil;
using DocProUtil.Attributes;
using DocProUtil.Customs.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DocproReport.Controllers
{
    //[AclAuthorize]
    public class WardController : BaseController
    {
        private string defaultPath = "/ward.html";
        private int areaParent = (int)AreaType.Huyen;
        private int areaType = (int)AreaType.Xa;
        #region Danh sách
        public ActionResult Index()
        {
           
            var test = @Locate.T("Xóa");
            var searchParam = Utils.Bind<AreaParam>(DATA);
            searchParam.AreaType = areaType;
            var wards = AreaRepository.Search(CUser.IDChannel, searchParam, Paging);
            SetTitle(Locate.T("Danh sách phường/xã"));
            return GetCustResultOrView(new DocproReport.Customs.Params.ViewParam
            {
                Data = new WardModel
                {
                    Districts=AreaRepository.Instance.GetListByFieldOrDefault(CUser.IDChannel, "AreaType", areaParent),
                    Wards = wards,
                    SearchParam = searchParam
                },
                ViewName = "Index",
                ViewNameAjax = "Wards"
            });
        }
        #endregion
        #region Create
        public ActionResult Create()
        {
            var DistrictsTest = AreaRepository.Instance.GetListByFieldOrDefault(CUser.IDChannel, "AreaType", areaParent);
           
            SetTitle(Locate.T("Tạo phường/xã"));
            return GetDialogResultOrView(new DocproReport.Customs.Params.ViewParam
            {
                ViewName = "Create",
                ViewNameAjax = "Create",
                Data = new WardModel
                {
                    Districts = AreaRepository.Instance.GetListByFieldOrDefault(CUser.IDChannel, "AreaType", areaParent),
                    Ward = new Area(),
                    Url = Locate.Url("/ward/save.html")
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
                SetSuccess(Locate.T("Thêm phường/xã thành công"));
            else
                SetError(Locate.T("Thêm phường/xã không thành công"));
            return GetResultOrRedirectDefault(defaultPath);
        }

        #endregion

        #region Update
        public ActionResult Update(int id)
        {
            SetTitle(Locate.T("Cập nhật phường/xã"));
            var oWard = AreaRepository.Instance.GetById(id);
            if (Utils.IsEmpty(oWard))
            {
                SetError(Locate.T(" phường/xã không tồn tại"));
                return GetResultOrRedirectDefault(defaultPath);
            }
            return GetDialogResultOrView(new DocproReport.Customs.Params.ViewParam
            {
                ViewName = "Update",
                ViewNameAjax = "Update",
                Data = new WardModel
                {
                    Districts = AreaRepository.Instance.GetListByFieldOrDefault(CUser.IDChannel, "AreaType", areaParent),
                    Ward = oWard,
                    Url = Locate.Url("/ward/change.html")
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
                SetError(Locate.T("Phường/xã không tồn tại"));
                return GetResultOrRedirectDefault(defaultPath);
            }
            var ward = Utils.BindUpdatedBy<Area>(item, DATA, CUser.ID);
            if (!IsValidate(ward))
                return GetResultOrRedirectDefault(defaultPath);
            if (AreaRepository.Instance.Update(ward))
                SetSuccess(Locate.T("Cập nhật phường/xã thành công"));
            else
                SetError(Locate.T("Cập nhật phường/xã không thành công"));
            return GetResultOrRedirectDefault(defaultPath);
        }
        #endregion

        #region Delete
        public ActionResult IsDelete(DocproReport.Customs.Params.DeleteParam deleteParam)
        {
            var item = AreaRepository.Instance.GetById((int)deleteParam.ID);
            if (Equals(item, null))
            {
                SetWarn(Locate.T("phường/xã không còn tồn tại"));
                return GetResultOrRedirectDefault(defaultPath);
            }
            return GetDialogResultOrViewDelete(new DocproReport.Customs.Params.DeleteParam
            {
                ID = item.ID,
                RedirectPath = GetRedirectOrDefault(defaultPath),
                Action = Locate.Url("/ward/delete.html"),
                BackTitle = Locate.T("Quay lại danh sách phường/xã"),
                ConfTitle = Locate.T("Xóa  phường/xã"),
                Title = Locate.T("Xác nhận xóa thông tin  phường/xã")
            });
        }

        public ActionResult IsDeletes(DocproReport.Customs.Params.DeletesParam deletesParam)
        {
            if (!deletesParam.HasID)
            {
                SetWarn(Locate.T("Bạn chưa chọn  phường/xã cần xóa"));
                return GetResultOrRedirectDefault(defaultPath);
            }
            var items = AreaRepository.Instance.GetByIds(deletesParam.IDToInts());
            if (Equals(items, null))
            {
                SetWarn(Locate.T(" phường/xã không còn tồn tại"));
                return GetResultOrRedirectDefault(defaultPath);
            }
            else if (items.Exists(t => t.CreatedBy != CUser.ID))
            {
                SetWarn(Locate.T("Bạn đang xóa  phường/xã của người khác"));
                return GetResultOrRedirectDefault(defaultPath);
            }

            return GetDialogResultOrViewDeletes(new DocproReport.Customs.Params.DeletesParam
            {
                ID = items.Select(x => (long)x.ID).ToArray(),
                RedirectPath = GetRedirectOrDefault(defaultPath),
                Action = Locate.Url("/ward/deletes.html"),
                BackTitle = Locate.T("Quay lại danh sách phường/xã"),
                ConfTitle = Locate.T("Xóa  phường/xã đã chọn ?", items.Count),
                Title = Locate.T("Xác nhận xóa phường/xã")

            });
        }

        public ActionResult Delete(DocproReport.Customs.Params.DeleteParam deleteParam)
        {
            var oWard = AreaRepository.Instance.GetById((int)deleteParam.ID);
            if (Utils.IsEmpty(oWard))
            {
                SetError(Locate.T(" phường/xã không còn tồn tại"));
            }
            else if (AreaRepository.Instance.Delete(oWard))
                SetSuccess(Locate.T("Xóa  phường/xã thành công"));
            else
                SetError(Locate.T("Lỗi: xóa  phường/xã không thành công"));
            return GetResultOrRedirectDefault(defaultPath);
        }

        public ActionResult Deletes(DocproReport.Customs.Params.DeletesParam deletesParam)
        {
            var oWards = AreaRepository.Instance.GetByIds(deletesParam.IDToInts());
            if (!oWards.Any())
                SetError(Locate.T(" phường/xã không còn tồn tại"));
            else if (AreaRepository.Instance.Deletes(oWards))
                SetSuccess(Locate.T("Xóa {0}  phường/xã thành công", oWards.Count));
            else
                SetError(Locate.T("Lỗi: xóa  phường/xã không thành công"));
            return GetResultOrRedirectDefault(defaultPath);
        }
        #endregion

        #region Validate
        private bool IsValidate(Area Ward)
        {
            if (string.IsNullOrEmpty(Ward.Name))
            {
                SetError(Locate.T("Tiêu đề phường/xã không được để trống"));
            }
            if (AreaRepository.Instance.NameExists(CUser.IDChannel,Ward.Name ,Ward.ID))
            {
                SetError(Locate.T("Trùng tên phường/xã"));
            }
            else if (AreaRepository.Instance.CodeExists(CUser.IDChannel, Ward.Code, Ward.ID))
            {
                SetError(Locate.T("Trùng mã phường/xã"));
            }
            return !HasError;
        }
        #endregion
    }
}