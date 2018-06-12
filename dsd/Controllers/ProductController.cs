using DocProModel.Models;
using DocProModel.Repository;
using DocproReport.Customs.Enum;
using DocproReport.Customs.Params;
using DocproReport.Models;
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
    public class ProductController : BaseController
    {
        // GET: Product
        private string defaultPath = "/product.html";
        public ActionResult Index()
        {
            var searchParam = Utils.Bind<ProductParam>(DATA);
            var product = ProductRepostitory.Search(searchParam, Paging);
            SetTitle(Locate.T("Danh sách Sản phẩm"));
            CUser.IDChannel = 7;
            var roomType = CategoryTypeRepository.Instance.GetFirstByFieldOrDefault(CUser.IDChannel, "Code", DMType.DM_KSP.ToString());
            var Categories = CategoryRepository.Instance.GetListByFieldOrDefault(CUser.IDChannel, "Type", roomType.ID);

            var versions1 = VersionRepostitory.Instance.GetList();
            return GetCustResultOrView(new DocproReport.Customs.Params.ViewParam
            {
                Data = new ProductModel
                {
                    SearchParam = searchParam,
                    Products = product,
                    categories = Categories,
                    versions = VersionRepostitory.Instance.GetList(),
                },
                ViewName = "Index",
                ViewNameAjax = "Product"

            });
        }

        public ActionResult Create()
        {
            var Member = ""; //lay danh sach thanh vien tham gia da co
            #region Lấy danh sách loại sản phẩm
            CUser.IDChannel = 7;
            var roomType = CategoryTypeRepository.Instance.GetFirstByFieldOrDefault(CUser.IDChannel, "Code", DMType.DM_KSP.ToString());
            var Categories = CategoryRepository.Instance.GetListByFieldOrDefault(CUser.IDChannel, "Type", roomType.ID); //lay danh sach loai san pham 
            #endregion
         
            SetTitle(Locate.T("Tạo Sản phẩm"));
            return GetDialogResultOrView(new DocproReport.Customs.Params.ViewParam
            {
                ViewName = "Create",
                ViewNameAjax = "Create",
                Data = new CreateProductModel
                {
                    users = UserRepository.Instance.GetListOrDefault(),
                    Url = Locate.Url("/product/save.html"),
                    categories = Categories,
                    Product = new Models.Product(),
                    version=new Models.Version(),
                    VersionFileTypes= Utils.GetDescribes<VersionFileType>()
        },
                Width = 800
            });

        }
        public ActionResult Save()
        {

            var ward = Utils.BindCreatedBy<Product>(DATA, CUser.ID);

            return GetResultOrRedirectDefault(defaultPath);
        }
    }
}