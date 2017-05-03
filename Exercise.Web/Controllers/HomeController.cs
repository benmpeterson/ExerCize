using Exercise.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exercise.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";

                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
                return View();
            }
            else
            {
                ViewBag.Name = "Not Logged IN";
            }
            return View();

        }

        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var p = usermanager.GetEmail(user.GetUserId());
                if (p != "admin@admin.com")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        

        //public boolean isadminuser()
        //{
        //    if (user.identity.isauthenticated)
        //    {
        //        var user = user.identity;
        //        applicationdbcontext context = new applicationdbcontext();
        //        var usermanager = new usermanager<applicationuser>(new userstore<applicationuser>(context));
        //        var s = usermanager.getroles(user.getuserid());
        //        if (s[0].tostring() == "admin")
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    return false;
        //}


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}