using DocProUtil.Attributes;
using System.Web.Mvc;

namespace DocproReport.Controllers
{
    //[AclAuthorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return GetCustResultOrView("Index");
        }
    }
}