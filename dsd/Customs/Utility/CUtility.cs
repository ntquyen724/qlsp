using ClosedXML.Excel;
using DocproReport.Customs.Entities;
using DocproReport.Customs.Enum;
using DocproReport.Models.Views;
using DocProUtil;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;


namespace DocproReport.Customs.Utilities
{
    public class ItemExcel
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public string Value { get; set; }
        public string Title { get; set; }
    }
    public static class CUtility
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            var date = dt.AddDays(-1 * diff).Date;
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }
        public static DateTime EndOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            var date = dt.AddDays(-1 * diff).Date;
            date = date.AddDays(7);
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }
        public static bool ExportToPdf(DataTable data, string fileName, string down, string title)
        {
            //Create a document object
            Document document = new Document(PageSize.A4);
            try
            {
                //get a PDFWriter object 
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(down, FileMode.Create));

                // open the document for writting
                document.Open();
                // add header
                Font georgia = FontFactory.GetFont("georgia", 18f);
                georgia.Color = BaseColor.RED;
                Chunk pdfData = new Chunk(title+"\n\n", georgia);
                Paragraph element = new Paragraph(pdfData);
                element.Alignment = Element.ALIGN_CENTER;
                document.Add(element);

                document.AddTitle(title);
                PdfPTable table = new PdfPTable(data.Columns.Count);
                table.WidthPercentage = 100;
                string fonts = Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + @"\Assets\fonts\arialbd.ttf";
                BaseFont bf = BaseFont.CreateFont(fonts, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font font = new Font(bf, 12);
                for (int k = 0; k < data.Columns.Count; k++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(data.Columns[k].ColumnName));//, font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.BackgroundColor = new BaseColor(51, 102, 102);
                    table.AddCell(cell);
                }
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    for (int j = 0; j < data.Columns.Count; j++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(data.Rows[i][j].ToString()));//, font));
                        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(cell);
                    }
                }
                document.Add(table);
                if (document.IsOpen())
                {
                    document.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                if (document.IsOpen())
                {
                    document.Close();
                }
                Loger.Log(ex, "log");
                return false;
            }
        }
        public static MvcHtmlString CustomDropdownList(string name, string id, string optionLabel, IEnumerable<SelectListItem> list, string selectedValue, object htmlAttributes = null)
        {
            string fullName = name;
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("name");
            }
            TagBuilder dropdown = new TagBuilder("select");
            dropdown.Attributes.Add("name", fullName);
            dropdown.Attributes.Add("id", id);
            //if (htmlAttributes != null)
            //{
            //    IDictionary attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            //    foreach (var item in attributes)
            //    {
            //        dropdown.MergeAttribute(item.Key, item.ToString());
            //    }
            //}
            StringBuilder options = new StringBuilder();            // Make optionLabel the first item that gets rendered.            
            if (optionLabel != null) options.Append("" + optionLabel + "");
            foreach (var item in list)
            {
                if (item.Selected) options.Append("" + item.Text + "");
                else options.Append("" + item.Text + "");
            }
            dropdown.InnerHtml = options.ToString();
            return MvcHtmlString.Create(dropdown.ToString(TagRenderMode.Normal));
        }
        public static bool ExportExcel<T>(out XLWorkbook MyWorkBook, List<T> itemSheets, Dictionary<string, string> headers, bool isshowheader, string template, int rowstart, int colstart, List<ItemExcel> itemexcels)
        {
            if (File.Exists(template))
                MyWorkBook = new XLWorkbook(template);
            else
                MyWorkBook = new XLWorkbook();
            var MyWorkSheet = MyWorkBook.Worksheet(1);
            try
            {
                //add item single

                // Tạo header 
                int index = 0;
                int sheet = 1;
                //if (MyWorkBook.Worksheet(sheet) == null)
                //    MyWorkBook.AddWorksheet(sheet.ToString());
                //var MyWorkSheet = MyWorkBook.Worksheet(sheet);
                if (isshowheader)
                {
                    foreach (var item in headers)
                    {
                        MyWorkSheet.Cell(rowstart, colstart + index).Style.Font.Bold = true;
                        MyWorkSheet.Cell(rowstart, colstart + index).Value = item.Value;
                        MyWorkSheet.Cell(rowstart, colstart + index).Style.Alignment.WrapText = true;
                        index++;
                    }
                }

                index = 1;
                var row = MyWorkSheet.Row(rowstart + 1);
                var totalItems = itemSheets.Count;
                row.InsertRowsBelow(itemSheets.Count);
                foreach (var item in itemSheets)
                {

                    var values = item.KeyValue();
                    int col = 0;
                    foreach (var it in headers)
                    {
                        var value = values.Contains(it.Key) ? values[it.Key] : "";
                        if (col == 0)
                        {
                            MyWorkSheet.Cell(rowstart + index, colstart + col).Value = index.ToString();
                            col++;
                            continue;
                        }
                        if (value is DateTime || value is DateTime?)
                        {
                            MyWorkSheet.Cell(rowstart + index, colstart + col).DataType = XLCellValues.Text;
                            MyWorkSheet.Cell(rowstart + index, colstart + col).SetValue<string>(Utils.DateToString((DateTime)value, "dd-MM-yyyy"));
                        }
                        else
                        {
                            MyWorkSheet.Cell(rowstart + index, colstart + col).Value = Convert.ToString(value);
                        }
                        col++;
                    }
                    index++;
                }
                if (Utils.IsNotEmpty<ItemExcel>(itemexcels))
                {
                    foreach (var itexcel in itemexcels)
                    {
                        MyWorkSheet.Cell(itexcel.Row, itexcel.Col).DataType = XLCellValues.Text;
                        MyWorkSheet.Cell(itexcel.Row, itexcel.Col).Value = itexcel.Value;
                    }
                }
                sheet++;
                return true;
            }
            catch
            {
                return false;
            }
        }
        //static List<T> CreateListByExample<T>(T obj)
        //{
        //    return new List<T>();
        //}
        public static bool ExportExcelCommon(out XLWorkbook MyWorkBook, string template, List<ExportExcelCommon> exportExcel, string Uname, List<ItemExcel> itemexcels)
        {
            // file template đã tồn tại
            if (File.Exists(template))
                MyWorkBook = new XLWorkbook(template);
            else
                MyWorkBook = new XLWorkbook();
            // lưu các tên sheet vào DIC
            Dictionary<string, string> SheetNames = new Dictionary<string, string>();
            var xlWorkSheets = MyWorkBook.Worksheets;
            for (int x = 1; x <= xlWorkSheets.Count; x++)
            {
                SheetNames.Add(MyWorkBook.Worksheet(x).Name, x.ToString());
            }
            // set Authour cho Excel
            MyWorkBook.Properties.Author = Uname;
            //index cho sheet sẽ là sheet cuối cùng
            int sheet = xlWorkSheets.Count + 1;
            int sheetDefault = 0;
            try
            {
                foreach (var excelItem in exportExcel)
                {
                    int sheetTemp = 0;
                    int index = 0;
                    // tạo sheet theo tên truyền vào hoặc index
                    if (Utils.IsNotEmpty(excelItem.sheetname))
                    {
                        // nếu teenn sheet đã có thì insert dữ liệu vào sheet đó
                        string value;
                        SheetNames.TryGetValue(excelItem.sheetname, out value);
                        if (Utils.IsEmpty(value))
                        {
                            MyWorkBook.AddWorksheet(excelItem.sheetname);
                        }
                        else
                        {
                            sheetTemp = int.Parse(value);
                        }
                    }
                    else
                    {
                        // tạo sheet mặc định chứa các dữ liệu k có tên sheet
                        if (sheetDefault == 0)
                        {
                            sheetDefault = sheet;
                            sheet++;
                            MyWorkBook.AddWorksheet("SheetDefault");
                        }
                    }
                    int x = (Utils.IsEmpty(excelItem.sheetname)) ? sheetDefault : ((sheetTemp == 0) ? sheet++ : sheetTemp);
                    try
                    {
                        SheetNames.Add(MyWorkBook.Worksheet(x).Name, x.ToString());
                    }
                    catch
                    {
                    }
                    var MyWorkSheet = MyWorkBook.Worksheet(x);
                    // nếu có dữ liệu cố định, in dữ liệu
                    if (Utils.IsNotEmpty<ItemExcel>(itemexcels))
                    {
                        foreach (var itexcel in itemexcels)
                        {
                            MyWorkSheet.Cell(itexcel.Row, itexcel.Col).DataType = XLCellValues.Text;
                            MyWorkSheet.Cell(itexcel.Row, itexcel.Col).Value = itexcel.Value;
                        }
                    }
                    // Tạo header 
                    if (excelItem.isshowheader)
                    {
                        foreach (var item in excelItem.headers)
                        {
                            MyWorkSheet.Cell(excelItem.rowstart, excelItem.colstart + index).Style.Font.Bold = true;
                            MyWorkSheet.Cell(excelItem.rowstart, excelItem.colstart + index).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                            MyWorkSheet.Cell(excelItem.rowstart, excelItem.colstart + index).Value = item.Value;
                            MyWorkSheet.Cell(excelItem.rowstart, excelItem.colstart + index).Style.Alignment.WrapText = true;
                            index++;
                        }
                    }
                    index = 1;
                    var row = MyWorkSheet.Row(excelItem.rowstart + 1);
                    var totalItems = excelItem.itemSheets.Count;
                    row.InsertRowsBelow(excelItem.itemSheets.Count);
                    // chuyển dynamic thành Ilist
                    IList list = excelItem.itemSheets;

                    foreach (var item in list)
                    {
                        var values = item.KeyValue();
                        int col = 0;
                        foreach (var it in excelItem.headers)
                        {
                            var value = values.Contains(it.Key) ? values[it.Key] : "";
                            if (col == 0)
                            {
                                MyWorkSheet.Cell(excelItem.rowstart + index, excelItem.colstart + col).Value = index.ToString();
                                col++;
                                continue;
                            }
                            if (value is DateTime || value is DateTime?)
                            {
                                MyWorkSheet.Cell(excelItem.rowstart + index, excelItem.colstart + col).DataType = XLCellValues.Text;
                                MyWorkSheet.Cell(excelItem.rowstart + index, excelItem.colstart + col).SetValue<string>(Utils.DateToString((DateTime)value, "dd-MM-yyyy"));
                            }
                            else
                            {
                                MyWorkSheet.Cell(excelItem.rowstart + index, excelItem.colstart + col).Value = Convert.ToString(value);
                            }
                            col++;
                        }
                        index++;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        private static void AddValueToCell(IXLWorksheet sheet, int row, int col, string value)
        {
            sheet.Cell(row, col).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            sheet.Cell(row, col).Style.Border.TopBorder = XLBorderStyleValues.Thin;
            sheet.Cell(row, col).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            sheet.Cell(row, col).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            sheet.Cell(row, col).DataType = XLCellValues.Text;
            sheet.Cell(row, col).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            sheet.Cell(row, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            sheet.Cell(row, col).Style.Alignment.SetWrapText(true);
            sheet.Cell(row, col).SetValue<string>(value);
        }
        public static string GenLinkExport(string url, dynamic objFillter)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(url);
                var types = objFillter.GetType();
                var props = new List<PropertyInfo>(types.GetProperties());
                foreach (var prop in props)
                {
                    try
                    {
                        var propValue = prop.GetValue(objFillter, null);
                        var propType = prop.PropertyType;
                        if (propValue != null)
                        {
                            if (propType == typeof(int))
                                if (propValue > 0 || prop.Name == "IDChannel")
                                    sb.AppendFormat(prop.Name + "={0}&", propValue);
                            if (propType == typeof(DateTime) ? true : propType == typeof(DateTime?))
                            {
                                var valueDate = Utils.DateToString(propValue, "dd-MM-yyyy");
                                if (Utils.IsEmpty(valueDate))
                                    sb.AppendFormat(prop.Name + "={0}&", valueDate);
                            }
                            if (propType == typeof(string))
                                sb.AppendFormat(prop.Name + "={0}&", propValue);
                            if (propType == typeof(bool))
                                sb.AppendFormat(prop.Name + "={0}&", propValue);
                        }
                        if (propType.IsArray)
                        {
                            var arrays = (Array)prop.GetValue(objFillter, null);
                            for (int i = 0; i < arrays.Length; i++)
                            {
                                sb.AppendFormat(prop.Name + "={0}&", arrays.GetValue(i));
                            }
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                url = sb.ToString().TrimEnd("&");
            }
            catch (Exception)
            {
                url = "#";
            }
            return url;
        }

        public static string GetValue(this Dictionary<int, string> dic, int key)
        {
            if (dic.ContainsKey(key))
                return dic[key];
            return string.Empty;
        }
        public static float GetValue(this Dictionary<int, float> dic, int key)
        {
            if (dic.ContainsKey(key))
                return dic[key];
            return 0;
        }

        public static string AddString(int level, string str)
        {
            var newStr = string.Empty;
            if (level > 0)
            {
                for (int i = 0; i < level; i++)
                {
                    newStr += str;
                }
            }
            return newStr;
        }
    }
}