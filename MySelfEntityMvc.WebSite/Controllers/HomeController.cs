using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using MySelfEntityMvc.Models.Entity;
using MySelfEntityMvc.Services;

namespace MySelfEntityMvc.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private static ILog logger = LogManager.GetLogger(typeof(HomeController));
        //
        // GET: /Home/

        public ActionResult Index()
        {
            TblProductBusiness service = new TblProductBusiness();
            List<TblProduct> aa = service.GetAllTest();
            logger.Info("我正在访问Home/Index");            
            return View(aa);
        }

        public ActionResult PartnerView()
        {
            return View();
        }

    }
}
