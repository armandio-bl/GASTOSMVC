using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GASTOSMVC.Controllers
{
    public class OUTController : Controller
    {
        // GET: OUT
        public ActionResult Index()
        {
            HttpCookie aCookie; string cookieName; int limit = Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                cookieName = Request.Cookies[i].Name; aCookie = new HttpCookie(cookieName); aCookie.Expires = DateTime.Now.AddDays(-1); Response.Cookies.Add(aCookie);
            }
            aCookie = null; Session.Abandon();
            this.Dispose(); GC.SuppressFinalize(this); GC.Collect(); GC.WaitForPendingFinalizers();
            return View();
        }
    }
}