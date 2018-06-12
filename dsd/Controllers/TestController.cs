using DocproReport.Models;
using DocproReport.Customs.Params;
using DocproReport.Models.Views;
using DocproReport.Repository;
using DocProUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocProModel.Repository;

namespace DocproReport.Controllers
{
    public class TestController : BaseController
    {
        // GET: Test
        private string defaultPath = "/test.html";
        #region Danh sách User
        public ActionResult Index()
        {
            #region Test Instance
            var t1 = UserName.Instance.Delete(2); //delete by Id

            var t2data = new UserName { ID = 6, Address = "ha noi" }; //delete by obj
            var t2 = UserName.Instance.Delete(t2data);
            
            var t3sql = UserName.Instance.SqlBuilder(0).Where("UserType =@0 and CreateBy=@1",1,0); 
            //var t3 = UserName.Instance.GetList(t3sql); //get list 
            //var t4 = UserName.Instance.GetListOrDefault(t3sql); //get list 

            //var sql = UserName.Instance.SqlBuilder().Where("ID>@0", 18); //get 1 row by 1 result by sql  
            //var t5 = UserName.Instance.GetSingleOrDefault(sql);

            var t6 = UserName.Instance.GetSelectColumns();//get all columns 

            var t7 = UserName.Instance.GetTableName();

            long[] ids = {13,19,15,16 };
            var t8 = UserName.Instance.IncrementField(ids,"ID",1); //gia tri tang dan

            //var t9data = new UserName { Address = "ha noi", Name = "nam", DateCreate = DateTime.Now, IDChannel = 0, UserType = 1 };
            //var t9 = UserName.Instance.Insert(t9data);  //insert to table 1 row

            //List<UserName> userNames = new List<UserName>();  
            //userNames.Add(t9data);
            //userNames.Add(t9data);
            //var t10 = UserName.Instance.Inserts(userNames); //insert list row

            


            #endregion
            var searchParam = Utils.Bind<TestParam>(DATA); //get param to search
            SetTitle(Locate.T("Danh sách Tài khoản")); // set Title 
            var data = TestRepository.Search(CUser.IDChannel, searchParam, Paging);  //get data,search data
            var datatest = UserName.Instance.GetList();
           
            return GetCustResultOrView(new DocproReport.Customs.Params.ViewParam //return view
            {
                Data = new UserModel
                {
                   SearchParam=searchParam,
                   ListUser= TestRepository.Instance.GetListByFieldOrDefault(CUser.IDChannel, "UserType", 1),
                   Users =data,
                },
                ViewName = "Index",
                ViewNameAjax = "Test"
            });
        }
        #endregion

        public ActionResult CreateView()
        {
            SetTitle(Locate.T("Tạo Tài khoản"));
            List<Mail> listmail = new List<Mail>();
            listmail.Add(new Mail { MailString = "gmail.com", MailValue = 1 });
            listmail.Add(new Mail { MailString = "yahoo.com", MailValue = 2 });
            return GetDialogResultOrView(new DocproReport.Customs.Params.ViewParam
            {
                ViewName = "CreateView",
                ViewNameAjax = "CreateView",
                Data = new UserModel
                {
                    Mail = listmail,
                    User = new UserName(),
                    Url = Locate.Url("/test/save.html")
                },
                Width = 800
            });
        }


        #region Tạo mới danh sách User
        public ActionResult Create()
        {
            SetTitle(Locate.T("Tạo Tài khoản"));
            List<Mail> listmail = new List<Mail>();
            listmail.Add(new Mail { MailString = "gmail.com", MailValue = 1 });
            listmail.Add(new Mail { MailString = "yahoo.com", MailValue = 2 });
            return GetDialogResultOrView(new DocproReport.Customs.Params.ViewParam
            {
                ViewName = "Create",
                ViewNameAjax = "Create",
                Data = new UserModel
                {
                    Mail=listmail,
                    User = new UserName(),
                    Url = Locate.Url("/test/save.html")
                },
                Width = 800
            });
        }
        public ActionResult Save()
        {
            var user = Utils.BindCreatedBy<UserName>(DATA, CUser.ID);
            if (!IsValidate(user))
                return GetResultOrRedirectDefault(defaultPath);
            user.IDChannel = CUser.IDChannel;
            user.DateCreate = DateTime.Now;
            user.UserType = 1;
            if (TestRepository.Instance.Insert(user))
                SetSuccess(Locate.T("Thêm tài khoản thành công"));
            else
                SetError(Locate.T("Thêm tài khoản không thành công"));
            return GetResultOrRedirectDefault(defaultPath);
        }
        #endregion

        #region
        public ActionResult Update(int id)
        {
            SetTitle(Locate.T("Cập nhật tài khoản"));
            var oUser = TestRepository.Instance.GetById(id);
            if (Utils.IsEmpty(oUser))
            {
                SetError(Locate.T("Tài khoản không tồn tại"));
                return GetResultOrRedirectDefault(defaultPath);
            }
            return GetDialogResultOrView(new DocproReport.Customs.Params.ViewParam
            {
                ViewName = "Update",
                ViewNameAjax = "Update",
                Data = new UserModel
                {
                    User= oUser,
                    Url = Locate.Url("/test/change.html")
                },
                Width = 800
            }
           );
        }
        public ActionResult Change()
        {
            var item = TestRepository.Instance.GetById(Utils.GetInt(DATA, "ID"));
            if (Utils.IsEmpty(item))
            {
                SetError(Locate.T("Tài khoản không tồn tại"));
                return GetResultOrRedirectDefault(defaultPath);
            }
            var user = Utils.BindUpdatedBy<UserName>(item, DATA, CUser.ID);

            if (!IsValidate(user))
                return GetResultOrRedirectDefault(defaultPath);
            if (TestRepository.Instance.Update(user))
                SetSuccess(Locate.T("Cập nhật Tài khoản thành công"));
            else
                SetError(Locate.T("Cập nhật Tài khoản không thành công"));
            return GetResultOrRedirectDefault(defaultPath);
        }
        #endregion
        #region Delete
        public ActionResult IsDelete(DocproReport.Customs.Params.DeleteParam deleteParam)
        {
            var item = TestRepository.Instance.GetById((int)deleteParam.ID);
            if (Equals(item, null))
            {
                SetWarn(Locate.T("Tài khoản không còn tồn tại"));
                return GetResultOrRedirectDefault(defaultPath);
            }
            return GetDialogResultOrViewDelete(new DocproReport.Customs.Params.DeleteParam
            {
                ID = item.ID,
                RedirectPath = GetRedirectOrDefault(defaultPath),
                Action = Locate.Url("/test/delete.html"),
                BackTitle = Locate.T("Quay lại danh sách tài khoản"),
                ConfTitle = Locate.T("Xóa  tài khoản"),
                Title = Locate.T("Xác nhận xóa thông tin  tài khoản")
            });
        }

        public ActionResult IsDeletes(DocproReport.Customs.Params.DeletesParam deletesParam)
        {
            if (!deletesParam.HasID)
            {
                SetWarn(Locate.T("Bạn chưa chọn  tài khoản cần xóa"));
                return GetResultOrRedirectDefault(defaultPath);
            }
            var items = TestRepository.Instance.GetByIds(deletesParam.IDToInts());
            if (Equals(items, null))
            {
                SetWarn(Locate.T(" tài khoản không còn tồn tại"));
                return GetResultOrRedirectDefault(defaultPath);
            }
            else if (items.Exists(t => t.CreateBy != CUser.ID))
            {
                SetWarn(Locate.T("Bạn đang xóa  tài khoản của người khác"));
                return GetResultOrRedirectDefault(defaultPath);
            }

            return GetDialogResultOrViewDeletes(new DocproReport.Customs.Params.DeletesParam
            {
                ID = items.Select(x => (long)x.ID).ToArray(),
                RedirectPath = GetRedirectOrDefault(defaultPath),
                Action = Locate.Url("/test/deletes.html"),
                BackTitle = Locate.T("Quay lại danh sách tài khoản"),
                ConfTitle = Locate.T("Xóa  tài khoản đã chọn ?", items.Count),
                Title = Locate.T("Xác nhận xóa tài khoản")

            });
        }

        public ActionResult Delete(DocproReport.Customs.Params.DeleteParam deleteParam)
        {
            var oWard = TestRepository.Instance.GetById((int)deleteParam.ID);
            if (Utils.IsEmpty(oWard))
            {
                SetError(Locate.T(" tài khoản không còn tồn tại"));
            }
            else if (TestRepository.Instance.Delete(oWard))
                SetSuccess(Locate.T("Xóa  tài khoản thành công"));
            else
                SetError(Locate.T("Lỗi: xóa  tài khoản không thành công"));
            return GetResultOrRedirectDefault(defaultPath);
        }

        public ActionResult Deletes(DocproReport.Customs.Params.DeletesParam deletesParam)
        {
            var oWards = TestRepository.Instance.GetByIds(deletesParam.IDToInts());
            if (!oWards.Any())
                SetError(Locate.T(" Tài khoản không còn tồn tại"));
            else if (TestRepository.Instance.Deletes(oWards))
                SetSuccess(Locate.T("Xóa {0}  Tài khoản thành công", oWards.Count));
            else
                SetError(Locate.T("Lỗi: xóa  Tài khoản không thành công"));
            return GetResultOrRedirectDefault(defaultPath);
        }
        #endregion
        #region Validate
        private bool IsValidate(UserName user)
        {
            if (string.IsNullOrEmpty(user.Name))
            {
                SetError(Locate.T("Tên không được để trống"));
            }
            if (TestRepository.Instance.NameExists(CUser.IDChannel, user.Name, user.ID))
            {
                SetError(Locate.T("Trùng tên tài khoản"));
            }          
            return !HasError;
        }
        #endregion
    }
}