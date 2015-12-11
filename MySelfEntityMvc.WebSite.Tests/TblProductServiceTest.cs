using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySelfEntityMvc.Services;
using MySelfEntityMvc.WebSite.Controllers;

namespace MySelfEntityMvc.WebSite.Tests
{
    [TestClass]
    public class TblProductServiceTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            TblProductBusiness service = new TblProductBusiness();
            service.TestJoin();
        }

        [TestMethod]
        public void TestMethod2()
        {
            TblProductBusiness service = new TblProductBusiness();
            service.TestSql();
        }
        [TestMethod]
        public void TestHome()
        {
            HomeController home = new HomeController();
            ActionResult res = home.Index();
        }
    }
}
