using ClosedXML.Excel;
using DocProLogic.Customs.Builders;
using DocProModel.Models;
using DocProModel.Repository;
using DocproReport.Customs.Entities;
using DocproReport.Customs.Params;
using DocproReport.Customs.Utilities;
using DocProUtil;
using DocProUtil.Attributes;
using DocProUtil.Cf;
using DocProUtil.UControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace DocproReport.Controllers
{
    [AclAccessLog]
    public class BaseController : Controller
    {
        /// <summary>
        ///     Title page
        /// </summary>
        private string _title;
        private readonly List<string> _warns = new List<string>();
        private readonly List<string> _errors = new List<string>();
        private readonly List<string> _success = new List<string>();
        private readonly List<string> _notifies = new List<string>();

        #region Form Data
        private Hashtable _data;
        protected Hashtable DATA
        {
            get
            {
                if (Equals(_data, null))
                    _data = Utils.GetDataPost();
                return _data;
            }
        }

        #endregion

        #region ResResult

        private bool _isLogout;

        private bool _isDL;
        private object _wDL;
        private string _htDL;

        private bool _isCust;
        private string _htCust;

        private bool _resOnlyData;
        private dynamic _dataRes;

        internal void SetIsLogout()
        {
            _isLogout = true;
        }
        internal void SetDataResponse(dynamic data)
        {
            _dataRes = data;
        }
        internal void SetOnlyDataResponse(dynamic data)
        {
            _resOnlyData = true;
            _dataRes = data;
        }
        /// <summary>
        /// Set html of dialog
        /// </summary>
        /// <param name="html"></param>
        internal void SetHtmlDialog(string html, object width)
        {
            _isDL = true;
            _wDL = width;
            _htDL = html;
        }

        /// <summary>
        /// Set html of custom placed
        /// </summary>
        /// <param name="html"></param>
        internal void SetHtmlResponse(string html)
        {
            _isCust = true;
            _htCust = html;
        }

        protected JsonResult GetResult()
        {
            if (_resOnlyData)
                return Json(new Hashtable {
                    {"data", _dataRes ?? string.Empty}
                });

            var res = new Hashtable();
            res.Add("data", _dataRes ?? string.Empty);
            res.Add("title", ViewBag.Title ?? string.Empty);

            if (IsError)
                res.Add("isErr", 1);
            if (_isLogout)
                res.Add("isLogout", 1);

            if (IsMsg)
            {
                res.Add("isMsg", 1);
                res.Add("htMsg", GetMessages());
            }
            if (_isDL)
            {
                res.Add("isDL", 1);
                res.Add("wDL", _wDL);
                res.Add("htDL", _htDL ?? string.Empty);
            }
            if (_isCust)
            {
                res.Add("isCust", 1);
                res.Add("htCust", _htCust ?? string.Empty);
            }
            return Json(res);
        }

        public bool IsMsg
        {
            get
            {
                return IsError || IsSuccess || IsNotify || IsWarn;
            }
        }
        public bool IsError
        {
            get
            {
                return Session["Errors"] != null && ((List<string>)Session["Errors"]).Any();
            }
        }
        public bool IsSuccess
        {
            get
            {
                return Session["Success"] != null && ((List<string>)Session["Success"]).Any();
            }
        }
        public bool IsNotify
        {
            get
            {
                return Session["Notifies"] != null && ((List<string>)Session["Notifies"]).Any();
            }
        }
        public bool IsWarn
        {
            get
            {
                return Session["Warns"] != null && ((List<string>)Session["Warns"]).Any();
            }
        }
        internal bool HasError
        {
            get { return _errors.Any(); }
        }
        #endregion

        #region RenderOptions
        protected static string RenderOptions(dynamic data, bool hasUndefined = true)
        {
            return HtmlOption.RenderOption(Utils.GetOptions(data, hasUndefined));
        }
        protected static string RenderOptions(dynamic data, int selected, bool hasUndefined = true)
        {
            return HtmlOption.RenderOption(Utils.GetOptions(data, selected, hasUndefined));
        }
        protected static string RenderOptions(dynamic data, List<int> selecteds, bool hasUndefined = true)
        {
            return HtmlOption.RenderOption(Utils.GetOptions(data, selecteds, hasUndefined));
        }
        #endregion

        /// <summary>
        ///     Session ID
        /// </summary>
        protected string SessionID
        {
            get
            {
                try
                {
                    return Session.SessionID;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        ///     Current user
        /// </summary>
        protected User CUser;

        /// <summary>
        ///     Current user
        /// </summary>

        /// <summary>
        ///     Pagination
        /// </summary>
        protected Pagination Paging;

        /// <summary>
        ///     Stylesheets
        /// </summary>
        protected List<string> Css;

        /// <summary>
        ///     Javascripts
        /// </summary>
        protected List<string> Scripts;

        protected bool IsAjax
        {
            get
            {
                return Request.IsAjaxRequest();
            }
        }

        /// <summary>
        ///     Set title page
        /// </summary>
        /// <param name="title"></param>
        protected void SetTitle(string title)
        {
            _title = title;
        }

        /// <summary>
        ///     Set success
        /// </summary>
        /// <param name="success"></param>
        internal void SetSuccess(string success)
        {
            _success.Add(success);
            Session["Success"] = _success;
        }
        internal void SetSuccess(List<string> success)
        {
            if (!Equals(success, null) && success.Any())
            {
                _success.AddRange(success);
                Session["Success"] = _success;
            }
        }
        internal void SetNotify(string notify)
        {
            _notifies.Add(notify);
            Session["Notifies"] = _notifies;
        }
        internal void SetNotify(List<string> notifies)
        {
            if (!Equals(notifies, null) && notifies.Any())
            {
                _notifies.AddRange(notifies);
                Session["Notifies"] = _notifies;
            }
        }

        /// <summary>
        ///     Set errors
        /// </summary>
        /// <param name="error"></param>
        internal void SetError(string error)
        {
            _errors.Add(error);
            Session["Errors"] = _errors;
        }
        internal void SetErrors(List<string> errors)
        {
            if (!Equals(errors, null) && errors.Any())
            {
                _errors.AddRange(errors);
                Session["Errors"] = _errors;
            }
        }

        /// <summary>
        ///     Set warnings
        /// </summary>
        /// <param name="warn"></param>
        internal void SetWarn(string warn)
        {
            _warns.Add(warn);
            Session["Warns"] = _warns;
        }
        internal void SetWarns(List<string> warns)
        {
            if (!Equals(warns, null) && warns.Any())
            {
                _warns.AddRange(warns);
                Session["Warns"] = _warns;
            }
        }

        /// <summary>
        ///     Include stylesheet
        /// </summary>
        /// <param name="item"></param>
        protected void IncludeCss(string item)
        {
            if (Equals(Css, null))
                Css = new List<string>();

            Css.Add(item);
        }

        /// <summary>
        ///     Include javascript
        /// </summary>
        /// <param name="item"></param>
        protected void IncludeScript(string item)
        {
            if (Equals(Scripts, null))
                Scripts = new List<string>();

            Scripts.Add(item);
        }

        /// <summary>
        ///     Called before the action method is invoked.
        /// </summary>
        /// <param name="filterContext">
        ///     Information about the current request and action.
        /// </param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            Paging = new Pagination(HttpContext.Request);

            var pathAndQuery = string.Empty;
            if (!Equals(HttpContext.Request.Url, null))
                pathAndQuery = HttpContext.Request.Url.PathAndQuery;

            GetCUser();
            Locate.RealtimeTranslates();

            var channel = ChannelRepository.Admin_GetByIdOrDefault(CUser.IDChannel);
            var banner = GlobalConfig.GetStringSetting("Banner");
            var customerName = GlobalConfig.GetStringSetting("CustomerName");
            var customerSystem = GlobalConfig.GetStringSetting("CustomerSystem");
            if (string.IsNullOrEmpty(customerName))
            {
                customerName = string.IsNullOrEmpty(channel.Describe)
                    ? "Công ty CPĐT thương mại và phát triển công nghệ FSI" : channel.Describe;
            }
            if (string.IsNullOrEmpty(customerSystem))
            {
                customerSystem = string.IsNullOrEmpty(channel.Name)
                    ? "Hệ thống quản trị tài liệu thông minh - DocPro" : channel.Name;
            }

            ViewBag.CUILang = Locate.CurUILang;
            ViewBag.Title = Locate.T(customerSystem);
            ViewBag.CustomerName = Locate.T(customerName);
            ViewBag.CustomerSystem = Locate.T(customerSystem);
            ViewBag.Banner = banner;
            ViewBag.Channel = channel;

            var routeDataValues = Request.RequestContext.RouteData.Values;
            ViewBag.CtrlName = routeDataValues.ContainsKey("controller") ? routeDataValues["controller"].ToString().ToLower() : string.Empty;
            ViewBag.ActionName = routeDataValues.ContainsKey("action") ? routeDataValues["action"].ToString().ToLower() : string.Empty;
            ViewBag.RedirectPath = Utils.GetString(DATA, "RedirectPath");
            ViewBag.DATARequest = DATA;
        }
        protected void GetCUser()
        {
            CUser = AclConfig.CurrentUser;
            ViewBag.CUser = CUser;
            ViewBag.CUserDept = DeptRepository.Admin_GetByIdOrDefault(CUser.IDChannel, CUser.IDDept);
            ViewBag.CUserPosition = PositionRepository.Admin_GetByIdOrDefault(CUser.IDChannel, CUser.IDPosition);
        }

        /// <summary>
        ///     Called after the action method is invoked.
        /// </summary>
        /// <param name="filterContext">
        ///     Information about the current request and action.
        /// </param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (Request.IsAjaxRequest() == false)
            {
                if (CUser.ID > 0)
                {
                    ViewBag.MsgCount = MessageRecentRepository.MessageCount(CUser.ID);
                    ViewBag.NtfiCount = NotificationRepository.NtfCount(CUser.ID);
                    ViewBag.NtfiHtml = NotificationBuilder.NotificationString(CUser);
                    ViewBag.RecentHtml = RecentBuilder.RecentString(CUser);
                }


                #region Css, JS, CDATA

                //Is mobile
                if (Request.Browser.IsMobileDevice)
                {
                    IncludeCss("~/Assets/jquery/css/jquery.mobile.css");
                    IncludeScript("~/Assets/jquery/js/jquery.mobile.js");
                }

                //Set title
                if (!Equals(_title, null))
                    ViewBag.Title = _title;

                //Set stylesheets
                if (!Equals(Css, null))
                    ViewBag.Css = Css.ToArray();

                //Set javascripts
                if (!Equals(Scripts, null))
                    ViewBag.Scripts = Scripts.ToArray();

                //Set pagination
                ViewBag.Pagination = Paging;

                //set dashboard

                //Set CDATA
                ViewBag.CData = Utils.Serialize(new Hashtable
                {
                    {"VirtualPath", GlobalConfig.VirtualPath},
                    {"UILang", Locate.CurUILang},
                    {"Token", AclConfig.GetTKByU()},
                    {"CUser", new Hashtable
                    {
                        {"Uid", CUser.ID},
                        {"Name", string.IsNullOrEmpty(CUser.Name)
                            ? CUser.Username
                            : CUser.Name},
                        {"Avatar", CUser.Avatar},
                        {"Namealias", CUser.ID}
                    }},
                    {"Storage", new Hashtable{
                        {"domain", GlobalConfig.StgUrl},
                        {"urlFile", GlobalConfig.StgUrlFile},
                        {"urlSmile", GlobalConfig.StgUrlSmile}
                    }}
                });
                #endregion
            }
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
            SysConfig.ResetCaches();

        }
        protected ActionResult GetDialogResultOrViewConfirm(ConfirmParam confirmParam, object w = null)
        {
            ViewBag.ConfirmParam = confirmParam;
            if (!string.IsNullOrEmpty(confirmParam.Title))
                SetTitle(confirmParam.Title);

            return GetDialogResultOrView("~/Views/Shared/IsConfirm.cshtml", Equals(w, null) ? 500 : w);
        }
        protected ActionResult GetDialogResultOrViewConfirmWithNote(ConfirmParam confirmParam, object w = null, string note = "")
        {
            ViewBag.ConfirmParam = confirmParam;
            if (!string.IsNullOrEmpty(confirmParam.Title))
                SetTitle(confirmParam.Title);
            ViewBag.Note = note;
            ViewBag.TitleNote = Locate.T("Ghi chú");
            return GetDialogResultOrView("~/Views/Shared/IsConfirmWithNote.cshtml", Equals(w, null) ? 500 : w);
        }
        protected ActionResult GetDialogResultOrViewConfirms(ConfirmsParam confirmsParam, object w = null)
        {
            ViewBag.ConfirmsParam = confirmsParam;
            if (!string.IsNullOrEmpty(confirmsParam.Title))
                SetTitle(confirmsParam.Title);

            return GetDialogResultOrView("~/Views/Shared/IsConfirms.cshtml", Equals(w, null) ? 500 : w);
        }
        protected ActionResult GetDialogResultOrViewDelete(DeleteParam deleteParam, object w = null)
        {
            ViewBag.DeleteParam = deleteParam;
            if (!string.IsNullOrEmpty(deleteParam.Title))
                SetTitle(deleteParam.Title);

            return GetDialogResultOrView("~/Views/Shared/IsDelete.cshtml", Equals(w, null) ? 500 : w);
        }
        protected ActionResult GetDialogResultOrViewDeletes(DeletesParam deletesParam, object w = null)
        {
            ViewBag.DeletesParam = deletesParam;
            if (!string.IsNullOrEmpty(deletesParam.Title))
                SetTitle(deletesParam.Title);

            return GetDialogResultOrView("~/Views/Shared/IsDeletes.cshtml", Equals(w, null) ? 500 : w);
        }

        protected string GetFormSearch(string name, object data = null)
        {
            return string.IsNullOrEmpty(name)
                ? string.Empty
                : GetView(string.Format(
                    "~/Views/Shared/Searchs/{0}.cshtml",
                    name
                ), data);
        }
        protected string GetView(string viewName, object data = null)
        {
            try
            {
                ViewBag.Pagination = Paging;
                if (!Equals(_title, null))
                    ViewBag.Title = _title;

                using (var sw = new StringWriter())
                {
                    ControllerContext.Controller.ViewData.Model = data;
                    var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                    var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    return sw.ToString();
                }
            }
            catch (Exception ex)
            {
                Loger.Log(ex);
                return string.Empty;
            }
        }

        protected string GetMessages()
        {
            return GetView("~/Views/Shared/Message.cshtml");
        }

        protected string GetReferrerOrDefault(string defaultPath)
        {
            if (Equals(Request.UrlReferrer, null))
                return defaultPath;

            return Request.UrlReferrer.ToString();
        }

        protected string GetRedirectOrDefault(string defaultPath)
        {
            var redirectPath = Utils.GetString(DATA, "RedirectPath");
            return string.IsNullOrEmpty(redirectPath)
                ? defaultPath
                : redirectPath;
        }

        protected ActionResult GetResultOrRedirectDefault(string defaultPath)
        {
            if (Request.IsAjaxRequest())
                return GetResult();
            return RedirectToPath(GetRedirectOrDefault(defaultPath));
        }
        protected ActionResult GetResultOrReferrerDefault(string defaultPath)
        {
            if (Request.IsAjaxRequest())
                return GetResult();
            return RedirectToPath(GetReferrerOrDefault(defaultPath));
        }
        protected ActionResult GetResultOrView(string viewName)
        {
            if (Request.IsAjaxRequest())
            {
                return GetResult();
            }
            return View(viewName);
        }
        protected ActionResult GetCustResultOrView(ViewParam viewParam)
        {
            if (Request.IsAjaxRequest())
            {
                //if (!Equals(viewParam.Data, null))
                //SetDataResponse(viewParam.Data);

                SetHtmlResponse(GetView(viewParam.ViewNameAjax ?? viewParam.ViewName, viewParam.Data));
                return GetResult();
            }
            return View(viewParam.ViewName, viewParam.Data);
        }
        protected ActionResult GetDialogResultOrView(ViewParam viewParam)
        {
            if (Request.IsAjaxRequest())
            {
                SetHtmlDialog(GetView(viewParam.ViewNameAjax ?? viewParam.ViewName, viewParam.Data), viewParam.Width ?? 600);
                return GetResult();
            }
            return View(viewParam.ViewName, viewParam.Data);
        }
        protected ActionResult GetCustResultOrView(string viewName, object data = null)
        {
            if (Request.IsAjaxRequest())
            {
                //if (!Equals(data, null))
                //SetDataResponse(data);

                SetHtmlResponse(GetView(viewName, data));
                return GetResult();
            }
            return View(viewName, data);
        }
        protected ActionResult GetDialogResultOrView(string viewName, object w = null, object data = null)
        {
            if (Request.IsAjaxRequest())
            {
                SetHtmlDialog(GetView(viewName, data), w ?? 600);
                return GetResult();
            }
            return View(viewName, data);
        }
        protected ActionResult PageNotFound()
        {
            if (Request.IsAjaxRequest())
            {
                SetError(Locate.T("Nội dung không tồn tại"));
                return GetResult();
            }
            return RedirectToPath("/error/page-not-found.html");
        }

        protected RedirectResult RedirectToPath(string path)
        {
            path = (path ?? "/");
            return Redirect(Locate.Url(path));
        }
        protected RedirectResult RedirectToPath(string path, params object[] param)
        {
            return Redirect(Locate.Url(path, param));
        }
        protected ActionResult ExportExcelCommon<T>(List<T> items, Dictionary<string, string> headers, bool isshowheader, string template, int rowstart, int colstart, string filename, List<ItemExcel> itemexcels)
        {
            template = HttpContext.Server.MapPath(Path.Combine("~/Templates/", template));
            var ftmp = DateTime.Now.ToString("ddMMyyyssmmhh");
            var down = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Downloads", ftmp, filename);
            XLWorkbook MyWorkBook = new XLWorkbook();
            CUtility.ExportExcel<T>(out MyWorkBook, items, headers, isshowheader, template, rowstart, colstart, itemexcels);
            Directory.CreateDirectory(Path.GetDirectoryName(down));
            MyWorkBook.SaveAs(down);
            return File(down, Utils.GetContentType(".xlsx"), filename);
        }
        protected ActionResult ExportPDFCommon(DataTable data, string filename, string title, string template = "")
        {
            var ftmp = DateTime.Now.ToString("ddMMyyyssmmhh");
            var down = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Downloads", ftmp, filename);
            Directory.CreateDirectory(Path.GetDirectoryName(down));
            CUtility.ExportToPdf(data, filename, down, title);
            return File(down, Utils.GetContentType(".pdf"), filename);
        }
        protected ActionResult ExportExcelCommon(string template, List<ExportExcelCommon> exportExcelTest, string filename, string Uname, List<ItemExcel> itemexcels)
        {
            template = HttpContext.Server.MapPath(Path.Combine("~/Templates/", template));
            var ftmp = DateTime.Now.ToString("ddMMyyyssmmhh");
            var down = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Downloads", ftmp, filename);
            XLWorkbook MyWorkBook = new XLWorkbook();
            CUtility.ExportExcelCommon(out MyWorkBook, template, exportExcelTest, Uname, itemexcels);
            Directory.CreateDirectory(Path.GetDirectoryName(down));
            MyWorkBook.SaveAs(down);
            return File(down, Utils.GetContentType(".xlsx"), filename);
        }
    }
}