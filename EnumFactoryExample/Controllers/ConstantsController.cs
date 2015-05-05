using System.Web.Mvc;
using EnumFactoryExample.Models.Factory;

namespace EnumFactoryExample.Controllers
{
    public class ConstantsController : Controller
    {
        [HttpGet]
        public ActionResult Enums()
        {
            return JavaScript(EnumerationSerializer.GetEnumJavaScript("OurGreatApp"));
        }
    }
}
