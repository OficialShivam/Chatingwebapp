using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chatingweb.WebChatDataEDMX;
using System.Web.Mvc;

namespace Chatingweb.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Admintbl value)
        {
            var db = new ChatingwebEntities();
            var status = db.Admintbls.Any(s => s.Email == value.Email && s.AdminStatus == 5);
            if (status) 
            {
                if (db.Admintbls.Any(d => d.Mobile == value.Mobile))
                {
                    using(var data = new ChatingwebEntities())
                    {
                        var s = db.Admintbls.FirstOrDefault(x => x.Email == value.Email && x.Mobile == value.Mobile);
                        {
                            s.AdminLoginDate = Convert.ToString(DateTime.Now);
                        };
                        if(s!=null)
                        {   
                            data.SaveChanges();
                            ModelState.Clear();
                            var name = db.Admintbls.Where(c => c.Email == value.Email).Select(c => c.AdminName).ToList();
                            foreach (var nm in name)
                            {
                                Session["Name"] = nm;
                            }
                            var mobile = db.Admintbls.Where(c => c.Email == value.Email).Select(c => c.AdminProfile).ToList();
                            foreach (var pf in mobile)
                            {
                                Session["Profile"] = pf;
                            }
                            var mobil = db.Admintbls.Where(c => c.Email == value.Email).Select(c => c.Mobile).ToList();
                            foreach (var pf in mobile)
                            {
                                Session["mn"] = pf;
                            }
                            return View("~/Areas/Admin/Views/Admin/Index.cshtml");//RedirectToAction("/Admin/Index");
                        }
                    }
                }
            }
            return View();
        }
        public ActionResult AllRagisterUser()
        {
            using (var db = new ChatingwebEntities())
            {
                var s = db.UserAccounts.ToList();

                return View(s);
            }   
        }
        public ActionResult DeleteAccount(int id)
        {
            using (var db = new ChatingwebEntities())
            {
                var s = db.UserAccounts.FirstOrDefault(a=>a.UserId==id);
                if(s!=null)
                {db.UserAccounts.Remove(s);
                   int p= db.SaveChanges();
                    if(p>0)
                    {
                        return RedirectToAction("AllRagisterUser");
                    }
                }

                return View("<script>alert('Some technical Error'<script>)");
            }
        }
        public ActionResult AllLoginuserAccount()
        {
            using (var db = new ChatingwebEntities())
            {
                var s = db.UserAccountLogins.ToList();

                return View(s);
            }
        }
        public ActionResult DetailLoginAccount(int id)
        {
            using (var db = new ChatingwebEntities())
            {
                var s = db.UserAccountLogins.FirstOrDefault(a => a.LoginId == id);
                if (s != null)
                {
                   return View(s);  
                }

                //Response.Write("<script>alert('Some technical Error'<script>)");
                return View();
            }
            
        }
    }
}