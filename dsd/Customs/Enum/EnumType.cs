using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DocproReport.Customs.Enum
{
    public enum EnuTinhTrang
    {
        [Description("Đang sử dụng")]
        Process = 1,
        [Description("Chưa sử dụng")]
        Todo = 2,
        [Description("Kệ đầy")]
        Full = 3,
    }
    public enum ContentTargetType
    {
        [Description("Danh mục tự nhập")]
        Tunhap = 1,
        [Description("Danh mục quận huyện")]
        QuanHuyen = 2,
        [Description("Danh mục xã phường")]
        Xaphuong = 3,
    }

    /// <summary>
    /// Cấp dữ liệu
    /// </summary>
    public enum DataLevel
    {
        [Description("Tỉnh/TP")]
        Tinhthanh = 1,
        [Description("Quận huyện")]
        Quanhuyen = 2,
        [Description("Phường xã")]
        Phuongxa = 3,
    }
    /// <summary>
    /// Cấp độ báo báo
    /// </summary>
    public enum GrantReport
    {
        [Description("Trung ương")]
        TrungUong = 1,
        [Description("Địa phương")]
        DiaPhuong = 2,
    }
    /// <summary>
    /// Loại đơn vị
    /// </summary>
    public enum UnitType
    {
        [Description("Bộ")]
        Bo = 1,
        [Description("Sở ban ngành")]
        SoNganh = 2,
        [Description("Tỉnh")]
        Tinh = 3,
        [Description("Huyện")]
        Huyen = 4,
        [Description("Xã")]
        Xa = 5,
    }
    /// <summary>
    /// Loại vùng
    /// </summary>
    public enum AreaType
    {
        [Description("Vùng")]
        Vung = 1,
        [Description("Tỉnh")]
        Tinh = 2,
        [Description("Huyện")]
        Huyen = 3,
        [Description("Xã")]
        Xa = 4,
        [Description("Thôn/bản")]
        Thon = 5,
    }
    /// <summary>
    /// Loại đơn vị
    /// </summary>
    public enum FormStatus
    {
        [Description("Nháp")]
        Nhap = 1,
        [Description("Chờ duyệt")]
        ChoDuyet = 4,
        [Description("Duyệt")]
        Duyet = 2,
        [Description("Từ chối duyệt")]
        TuChoiDuyet = 3,
    }
    /// <summary>
    /// Loại đơn vị
    /// </summary>
    public enum CouponStatus
    {
        [Description("Nháp")]
        Nhap = 1,
        [Description("Chờ duyệt")]
        ChoDuyet = 4,
        [Description("Duyệt")]
        Duyet = 2,
        [Description("Từ chối duyệt")]
        TuChoiDuyet = 3,
    }
    /// <summary>
    /// Loại đơn vị
    /// </summary>
    public enum UnitInput
    {
        [Description("Ủy ban dân tộc")]
        UyBanDanToc = 1,
        [Description("Bộ")]
        Bo = 2,
        [Description("Sở ban ngành")]
        So = 3,
        [Description("Tỉnh")]
        Tinh = 4,
    }
    /// <summary>
    /// Loại đơn vị
    /// </summary>
    public enum InputStatus
    {
        [Description("Đang nhập")]
        DangNhap = 1,
        [Description("Đã nhập")]
        DaNhap = 2,
        [Description("Duyệt")]
        Duyet = 3
    }
    /// <summary>
    /// Duyệt các phiếu nhập
    /// </summary>
    public enum ApproveCouponStatus
    {
        [Description("Đang nhập")]
        DangNhap = 1,
        [Description("Chờ duyệt")]
        ChoDuyet = 2,
        [Description("Đã duyệt")]
        DaDuyet = 3,
        [Description("Từ chối")]
        TuChoi = 4,
    }

    /// <summary>
    /// Loại thuộc tính
    /// </summary>
    public enum PropertiesIDType
    {
        [Description("Danh mục tự nhập")]
        Tunhap = 1,
        [Description("Danh mục quận huyện")]
        QuanHuyen = 2,
        [Description("Danh mục xã phường")]
        Xaphuong = 3
    }
    /// <summary>
    /// Tiesn độ nhập phiếu
    /// </summary>
    public enum InputProgressStatus
    {
        [Description("Chưa nhập")]
        ChuaNhap = 1,
        [Description("Đang nhập")]
        DangNhap = 2,
        [Description("Đã nhập")]
        DaNhap = 3
    }

    public enum VersionStatus
    {
        [Description("Chờ duyệt")]
        ChoDuyet=1,
        [Description("Đã duyệt")]
        DaDuyet = 2,
        [Description("Từ chối duyệt")]
        Tuchoiduyt = 3
    }
    public enum VersionFileType
    {
        [Description("Tài liệu yêu cầu khách hàng")]
        TLYCKH = 1,
        [Description("Tài liệu khảo sát")]
        TLKS = 2,
        [Description("Tài liệu phân tích thiết kế hệ thống")]
        TLPTTKHT = 3,
        [Description("Tài liệu đặc tả")]
        TLDacTa = 4,
        [Description("Testcase")]
        TLTestCase = 5,
        [Description("UAT")]
        UAT = 6,
        [Description("Tài liệu đào tạo")]
        TLDaoTao = 7,
        [Description("Video hướng dẫn sử dụng")]
        TLVideo = 8,
        [Description("Source Release")]
        TLSR = 9,
        [Description("Tài liệu khác")]
        TLK = 10
    }
}