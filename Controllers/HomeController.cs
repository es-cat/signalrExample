using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Chat(string groupid)
        {
            if (string.IsNullOrEmpty(groupid))
            {
                ViewBag.groupid = Guid.NewGuid().ToString();
            }
            else {
                ViewBag.groupid = groupid;
            }
            return View();
        }
    }
}
