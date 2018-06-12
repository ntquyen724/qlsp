using DocproReport.Customs.Enum;
using DocproReport.Customs.Utilities;
using DocProUtil;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace DocproReport.Customs.Helper
{
    public class CustomSelectItem : SelectListItem
    {
        public string Class { get; set; }
        public string SelectedValue { get; set; }
    }
    public static class HelperHtml
    {
        #region---------------Control---------------------------
        public static MvcHtmlString CusTextBoxDate(this HtmlHelper html, string id, string name, object value, string displayname, string placeholder = "", bool isNotEmpty = false, object htmlAttributes = null, int maxlen = 0)
        {
            TagBuilder tag = new TagBuilder("input");
            if (maxlen > 0)
            {
                tag.MergeAttribute("data-bv-stringlength-message", displayname + Locate.T(" không được vượt quá {0} ký tự", maxlen));
                tag.MergeAttribute("data-bv-stringlength-max", maxlen.ToString());
                tag.MergeAttribute("minlength", "0");
            }
            tag.MergeAttribute("class", "form-control date");
            tag.setCommonTextBox(id, name, value, displayname, placeholder, isNotEmpty, htmlAttributes);
            return new MvcHtmlString(tag.ToString());
        }

        /// <summary>
        /// Tự sinh text box Datetime
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">Tên</param>
        /// <param name="displayname">Tên hiển thị</param>
        /// <param name="value">Giá trị</param>
        /// <param name="placeholder">placeholder</param>
        /// <param name="isNotEmpty">Trạng thái bắt buộc</param>
        /// <param name="msgNotEmpty">Thông báo lỗi bắt buộc</param>
        /// <param name="msgdigits">Thông báo lỗi định dạng số</param>
        /// <param name="htmlAttributes">Các thuộc tính khác</param>
        /// <returns></returns>
        public static MvcHtmlString CusTextBoxDateTime(this HtmlHelper html, string id, string name, object value, string displayname, string placeholder = "", bool isNotEmpty = false, object htmlAttributes = null, int maxlen = 0)
        {
            TagBuilder tag = new TagBuilder("input");
            if (maxlen > 0)
            {
                tag.MergeAttribute("data-bv-stringlength-message", displayname + Locate.T(" không được vượt quá {0} ký tự", maxlen));
                tag.MergeAttribute("data-bv-stringlength-max", maxlen.ToString());
                tag.MergeAttribute("minlength", "0");
            }
            tag.MergeAttribute("class", "form-control datetime");
            tag.setCommonTextBox(id, name, value, displayname, placeholder, isNotEmpty, htmlAttributes);
            return new MvcHtmlString(tag.ToString());
        }


        /// <summary>
        /// Tự sinh text box
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">Tên</param>
        /// <param name="displayname">Tên hiển thị</param>
        /// <param name="value">Giá trị</param>
        /// <param name="placeholder">placeholder</param>
        /// <param name="isNotEmpty">Trạng thái bắt buộc</param>
        /// <param name="msgNotEmpty">Thông báo lỗi bắt buộc</param>
        /// <param name="msgdigits">Thông báo lỗi định dạng số</param>
        /// <param name="htmlAttributes">Các thuộc tính khác</param>
        /// <returns></returns>
        public static MvcHtmlString CusTextBox(this HtmlHelper html, string id, string name, object value, string displayname, string placeholder = "", bool isNotEmpty = false, object htmlAttributes = null, int maxlen = 0)
        {
            TagBuilder tag = new TagBuilder("input");
            if (maxlen > 0)
            {
                tag.MergeAttribute("data-bv-stringlength-message", displayname + Locate.T(" không được vượt quá {0} ký tự", maxlen));
                tag.MergeAttribute("data-bv-stringlength-max", maxlen.ToString());
                tag.MergeAttribute("minlength", "0");
            }
            tag.setCommonTextBox(id, name, value, displayname, placeholder, isNotEmpty, htmlAttributes);
            return new MvcHtmlString(tag.ToString());
        }

        /// <summary>
        /// Tự sinh text box số điện thoại
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">Tên</param>
        /// <param name="displayname">Tên hiển thị</param>
        /// <param name="value">Giá trị</param>
        /// <param name="placeholder">placeholder</param>
        /// <param name="isNotEmpty">Trạng thái bắt buộc</param>
        /// <param name="msgNotEmpty">Thông báo lỗi bắt buộc</param>
        /// <param name="msgdigits">Thông báo lỗi định dạng số</param>
        /// <param name="htmlAttributes">Các thuộc tính khác</param>
        /// <param name="country">chuẩn quốc gia áp dụng, mặc định = VI</param>
        /// <returns></returns>
        public static MvcHtmlString CusTextBoxPhone(this HtmlHelper html, string id, string name, object value, string displayname, string placeholder = "", bool isNotEmpty = false, object htmlAttributes = null, string country = "VI")
        {
            TagBuilder tag = new TagBuilder("input");
            tag.MergeAttribute("data-bv-phone-country", country);
            tag.MergeAttribute("data-bv-phone-message", Locate.T("Số điện thoại sai định dạng"));
            tag.MergeAttribute("data-bv-phone", "true");
            tag.MergeAttribute("data-title-show", Locate.T("Số điện thoại không đúng định dạng"));
            tag.setCommonTextBox(id, name, value, displayname, placeholder, isNotEmpty, htmlAttributes);
            return new MvcHtmlString(tag.ToString());
        }


        /// <summary>
        /// Tự sinh text box kiểu số
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">Tên</param>
        /// <param name="displayname">Tên hiển thị</param>
        /// <param name="value">Giá trị</param>
        /// <param name="placeholder">placeholder</param>
        /// <param name="isNotEmpty">Trạng thái bắt buộc</param>
        /// <param name="msgNotEmpty">Thông báo lỗi bắt buộc</param>
        /// <param name="isdigits">Trạng thái text box là số</param>
        /// <param name="msgdigits">Thông báo lỗi định dạng số</param>
        /// <param name="htmlAttributes">Các thuộc tính khác</param>
        /// <returns></returns>
        public static MvcHtmlString CusTextBoxDigit(this HtmlHelper html, string id, string name, object value, string displayname, string placeholder = "", bool isNotEmpty = false, object htmlAttributes = null, int maxlen = 0)
        {
            TagBuilder tag = new TagBuilder("input");
            tag.setCommonTextBox(id, name, value, displayname, placeholder, isNotEmpty, htmlAttributes);
            tag.MergeAttribute("data-bv-digits-message", displayname + Locate.T(" phải là kiểu số"));
            tag.MergeAttribute("data-bv-digits", "true");
            if (maxlen > 0)
            {
                tag.MergeAttribute("data-bv-between-message", displayname + Locate.T(" không được vượt quá {0}", maxlen));
                tag.MergeAttribute("data-bv-between-max", maxlen.ToString());
                tag.MergeAttribute("data-bv-between-min", "0");
                tag.MergeAttribute("data-bv-between", "true");
            }
            return new MvcHtmlString(tag.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="displayname"></param>
        /// <param name="placeholder"></param>
        /// <param name="isNotEmpty"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="maxlen"></param>
        /// <returns></returns> 
        public static MvcHtmlString CusTextBoxNumberic(this HtmlHelper html, string id, string name, object value, string displayname, string placeholder = "", bool isNotEmpty = false, object htmlAttributes = null, int maxlen = 0)
        {
            TagBuilder tag = new TagBuilder("input");
            //tag.MergeAttribute("type", "number");
            tag.setCommonTextBox(id, name, value, displayname, placeholder, isNotEmpty, htmlAttributes);
            tag.MergeAttribute("data-fv-numeric-message", displayname + Locate.T(" phải là kiểu số"));
            tag.MergeAttribute("data-fv-numeric", "true");
            if (maxlen > 0)
            {
                tag.MergeAttribute("data-bv-between-message", displayname + Locate.T(" không được vượt quá {0}", maxlen));
                tag.MergeAttribute("data-bv-between-max", maxlen.ToString());
                tag.MergeAttribute("data-bv-between-min", "0");
                tag.MergeAttribute("data-bv-between", "true");
            }
            return new MvcHtmlString(tag.ToString());
        }
        /// <summary>
        /// Tự sinh text box kiểu bắt buộc, hoặc số
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">Tên</param>
        /// <param name="displayname">Tên hiển thị</param>
        /// <param name="value">Giá trị</param>
        /// <param name="placeholder">placeholder</param>
        /// <param name="htmlAttributes">Các thuộc tính khác</param>
        /// <returns></returns>
        public static MvcHtmlString CusTextBoxNomal(this HtmlHelper html, string id, string name, object value = null, string placeholder = "", object htmlAttributes = null)
        {
            TagBuilder tag = new TagBuilder("input");
            tag.setCommonTextBox(id, name, value, placeholder, placeholder, false, htmlAttributes);
            return new MvcHtmlString(tag.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="placeholder"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString CusTextArea(this HtmlHelper html, string id, string name, object value = null, string placeholder = "", object htmlAttributes = null, int rows = 0)
        {
            TagBuilder tag = new TagBuilder("textarea");
            tag.setCommonArea(id, name, value, placeholder, rows, placeholder, false, htmlAttributes);
            return new MvcHtmlString(tag.ToString());
        }
        /// <summary>
        /// Tự sinh Dropdowlist
        /// </summary>
        /// <param name="name">Tên</param>
        /// <param name="id">ID</param>
        /// <param name="optionLabel">Hiển thị tìm kiếm tất cả</param>
        /// <param name="list">Dach sách phần tử của select</param>
        /// <param name="htmlAttributes">các thuộc tính thêm</param>
        /// <returns></returns>
        public static MvcHtmlString CusDropdownList(this HtmlHelper htmlHelper, string id, string name, string optionLabel, IEnumerable<SelectListItem> list, object htmlAttributes = null, string displayname = "", bool isNotEmpty = false)
        {
            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("name");
            }
            TagBuilder dropdown = new TagBuilder("select");
            dropdown.setCommonDropdowList(name, id, optionLabel, list, htmlAttributes, displayname, isNotEmpty);
            return MvcHtmlString.Create(dropdown.ToString(TagRenderMode.Normal));
        }
        /// <summary>
        /// Tự sinh Dropdowlist dạng select2
        /// </summary>
        /// <param name="name">Tên</param>
        /// <param name="id">ID</param>
        /// <param name="optionLabel">Hiển thị tìm kiếm tất cả</param>
        /// <param name="list">Dach sách phần tử của select</param>
        /// <param name="htmlAttributes">các thuộc tính thêm</param>
        /// <param name="isSelectChange">Thay đổi phần tử khác khi select</param>
        ///  <param name="targetChange">Nơi phần tử được thay đổi</param>
        /// <returns></returns>
        public static MvcHtmlString CusDropdownListSelect2(this HtmlHelper htmlHelper, string id, string name, string optionLabel, IEnumerable<SelectListItem> list, object htmlAttributes = null, bool isSelectChange = false, string targetChange = "", string urlchange = "", string displayname = "", bool isEmpty = false)
        {
            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("name");
            }
            TagBuilder dropdown = new TagBuilder("select");
            if (isSelectChange)
            {
                dropdown.Attributes.Add("class", "form-control autoSelect2 select_change");
                dropdown.Attributes.Add("data-target", targetChange);
                dropdown.Attributes.Add("data-url", urlchange);
            }
            else
                dropdown.Attributes.Add("class", "form-control autoSelect2");
            dropdown.setCommonDropdowList(name, id, optionLabel, list, htmlAttributes, displayname, isEmpty);
            return MvcHtmlString.Create(dropdown.ToString(TagRenderMode.Normal));
        }
        /// <summary>
        /// Tự sinh Dropdowlist dạng chọn nhiều
        /// </summary>
        /// <param name="name">Tên</param>
        /// <param name="id">ID</param>
        /// <param name="optionLabel">Hiển thị tìm kiếm tất cả</param>
        /// <param name="list">Dach sách phần tử của select</param>
        /// <param name="htmlAttributes">các thuộc tính thêm</param>
        /// <returns></returns>
        public static MvcHtmlString CusDropdownListPicker(this HtmlHelper htmlHelper, string id, string name, string optionLabel, IEnumerable<SelectListItem> list, string placeholder, object htmlAttributes = null, string displayname = "", bool isEmpty = false)
        {
            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("name");
            }
            TagBuilder dropdown = new TagBuilder("select");
            dropdown.Attributes.Add("class", "selectpicker form-control");
            dropdown.Attributes.Add("multiple", "true");
            dropdown.Attributes.Add("data-live-search", "true");
            //dropdown.Attributes.Add("data-size", "5");
            dropdown.Attributes.Add("data-live-search-placeholder", placeholder);
            // dropdown.Attributes.Add("data-actions-box", "true");
            //dropdown.Attributes.Add("data-deselect-all-text", Locate.T("Bỏ chọn"));
            //dropdown.Attributes.Add("data-select-all-text", Locate.T("Chọn tất cả"));
            dropdown.Attributes.Add("data-none-selected-text", displayname);
            dropdown.setCommonDropdowList(name, id, optionLabel, list, htmlAttributes, displayname, isEmpty);
            return MvcHtmlString.Create(dropdown.ToString(TagRenderMode.Normal));
        }
        private static void setCommonDropdowList(this TagBuilder dropdown, string name, string id, string optionLabel, IEnumerable<SelectListItem> list, object htmlAttributes, string displayname = "", bool isNotEmpty = false)
        {
            dropdown.Attributes.Add("name", name);
            dropdown.Attributes.Add("id", id);
            dropdown.MergeAttribute("data-bv-field", name);
            if (htmlAttributes != null)
            {
                IDictionary<string, object> attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                foreach (var item in attributes)
                {
                    dropdown.MergeAttribute(item.Key, item.Value.ToString());
                }
            }
            if (isNotEmpty)
            {
                dropdown.MergeAttribute("data-bv-notempty-message", Locate.T("{0} không được để trống", displayname));
                dropdown.MergeAttribute("data-bv-notempty", "true");
            }
            StringBuilder options = new StringBuilder();
            if (!string.IsNullOrEmpty(optionLabel))
                options.Append("<option value=''>" + optionLabel + "</option>");
            foreach (var item in list)
            {
                if (item.Selected)
                    options.Append("<option selected=true value='" + item.Value + "'>" + item.Text + "</option>");
                else
                    options.Append("<option value='" + item.Value + "'>" + item.Text + "</option>");
            }
            dropdown.InnerHtml = options.ToString();
        }
        private static void setCommonTextBox(this TagBuilder tag, string id, string name, object value, string displayname, string placeholder = "", bool isNotEmpty = false, object htmlAttributes = null)
        {
            tag.MergeAttribute("type", "text");
            tag.MergeAttribute("id", id);
            tag.MergeAttribute("name", name);
            tag.MergeAttribute("class", "form-control");
            tag.MergeAttribute("placeholder", placeholder);
            tag.MergeAttribute("title", placeholder);
            tag.MergeAttribute("data-bv-field", name);
            if (isNotEmpty)
            {
                tag.MergeAttribute("data-bv-notempty-message", displayname + Locate.T(" không được để trống"));
                tag.MergeAttribute("data-bv-notempty", "true");
            }
            if (Utils.IsNotEmpty(value))
                tag.MergeAttribute("value", value.ToString());
            if (htmlAttributes != null)
            {
                IDictionary<string, object> attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                foreach (var item in attributes)
                {
                    tag.MergeAttribute(item.Key, item.Value.ToString());
                }
            }
        }
        private static void setCommonArea(this TagBuilder tag, string id, string name, object value, string displayname, int irows, string placeholder = "", bool isNotEmpty = false, object htmlAttributes = null)
        {
            tag.MergeAttribute("id", id);
            tag.MergeAttribute("name", name);
            tag.MergeAttribute("class", "form-control");
            tag.MergeAttribute("placeholder", placeholder);
            tag.MergeAttribute("title", placeholder);
            tag.MergeAttribute("data-bv-field", name);
            tag.MergeAttribute("rows", irows.ToString());
            if (isNotEmpty)
            {
                tag.MergeAttribute("data-bv-notempty-message", displayname + Locate.T(" không được để trống"));
                tag.MergeAttribute("data-bv-notempty", "true");
            }
            if (Utils.IsNotEmpty(value))
            {
                tag.MergeAttribute("value", value.ToString());
                tag.InnerHtml = value.ToString();
            }
            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                tag.MergeAttributes(attributes);
            }
        }
        #endregion----------------------------------------------

        #region--------Status-------
        /// <summary>
        /// Sịnh động Trạng thái
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static MvcHtmlString RenderStatus(this HtmlHelper html, int status)
        {
            //DacPV TODO
            TagBuilder tag = new TagBuilder("lable");
            tag.MergeAttribute("value", status.ToString());
            return new MvcHtmlString("Trạng thái " + tag.ToString());
        }
        #endregion------------------
    }
}