using Chatingweb.DBWorkChat;
using Chatingweb.WebChatDataEDMX;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Chatingweb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(UserAccountRagister value)
        {
            var db = new ChatingwebEntities();

            if (ModelState.IsValid)
            {
                var Email = db.UserAccounts.Any(c => c.UserEmail == value.UserEmail);
                var Mobile = db.UserAccounts.Any(v => v.UserMobile == value.UserMobile);
                if (Email && Mobile)
                {
                    ViewBag.Email = "This" + "" + Email + "" + "Allready Ragistered";
                    ViewBag.Mobile = "This" + "" + Mobile + "" + "Allready Ragistered";
                    return View("~/Views/Home/Login.cshtml");
                }
                else
                {
                    using (db = new ChatingwebEntities())
                    {
                        UserAccount ragister = new UserAccount()
                        {
                            UserName = value.UserName,
                            UserEmail = value.UserEmail,
                            UserPassword = value.UserPassword,
                            UserMobile = value.UserMobile,
                            Status = 0,
                            UserRagisterDate = Convert.ToString(DateTime.Now)
                           
                        };
                        Response.Cookies["Ragiser"]["Name"]=value.UserName;
                        Response.Cookies["Ragiser"]["Email"] = value.UserEmail;
                        Response.Cookies["Ragiser"]["Mobile"]=value.UserMobile;
                        Response.Cookies["Ragiser"]["password"]=value.UserPassword;
                        Response.Cookies["Ragiser"].Expires= DateTime.Now.AddDays(30);
                    if (ragister != null)
                        {

                            db.UserAccounts.Add(ragister);
                            db.SaveChanges();

                            ModelState.Clear();
                            return View("~/Views/Home/Login.cshtml");
                        }
                    }
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult ReadCookie()
        {
            //Fetch the Cookie using its Key.
            HttpCookie nameCookie = Request.Cookies["Name"];

            //If Cookie exists fetch its value.
            string name = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";

            TempData["Message"] = name;

            return RedirectToAction("Index");
        }
        [HttpPost]
        public void WriteCookie( string name ,string mobile , string email, string pass)
        {
            //Create a Cookie with a suitable Key.
            HttpCookie nameCookie = new HttpCookie("Name");
            HttpCookie mobileCookie = new HttpCookie("Mobile");
            HttpCookie EmailCookie = new HttpCookie("Email");
            HttpCookie PasswordCookie = new HttpCookie("Password");

            //Set the Cookie value.
            nameCookie.Values["Name"] = Request.Form["name"];
            mobileCookie.Values["Mobile"] = Request.Form["mobile"];
            EmailCookie.Values["Email"] = Request.Form["email"];
            PasswordCookie.Values["Password"] = Request.Form["pass"];

            //Set the Expiry date.
            nameCookie.Expires = DateTime.Now.AddDays(30);

            //Add the Cookie to Browser.
            Response.Cookies.Add(nameCookie);
            Response.Cookies.Add(mobileCookie);
            Response.Cookies.Add(EmailCookie); 
            Response.Cookies.Add(PasswordCookie);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(UserAccountLoginstust value)
        {   
            var db = new ChatingwebEntities();
          var status = db.UserAccounts.Any(s => s.UserEmail == value.UserEmail && s.Status == 0);
          if (status)
          {
                if (ModelState.IsValid)
                {
                    var loginEmail = db.UserAccountLogins.Any(c => c.UserEmail == value.UserEmail);
                    var loginusermobile = db.UserAccountLogins.Any(v => v.UserPassword == value.UserPassword);
                
                    if (loginEmail && loginusermobile)
                    {
                        ViewBag.Email = "This" + "" + loginEmail + "" + "Allready Ragistered";
                        ViewBag.Mobile = "This" + "" + loginusermobile + "" + "Allready Ragistered";

                        var s = db.UserAccountLogins.FirstOrDefault(x => x.UserEmail == value.UserEmail && x.UserPassword == value.UserPassword);
                        {
                            s.UserLastLoginDate = Convert.ToString(DateTime.Now);
                        };
                        if (s != null)
                        {
                            db.SaveChanges();
                            ModelState.Clear();
                            var name = db.UserAccounts.Where(c => c.UserEmail == value.UserEmail).Select(c => c.UserName).ToList();
                            foreach (var nm in name)
                            {
                                Session["Name"] = nm;
                                TempData["Message"] = nm;
                            }
                            var mobile = db.UserAccounts.Where(c => c.UserEmail == value.UserEmail).Select(c => c.UserMobile).ToList();
                            foreach (var ph in mobile)
                            {
                                Session["Phone"] = ph;
                                //TempData["Phone"] = ph;
                            }
                            Session["Email"] = loginEmail;
                            //ViewBag["email"]=loginEmail;
                            return RedirectToAction("ChatBoxActon", "UserHome", new { nam = name[0], mb = mobile[0] });

                        }

                    }
                }
                else
                {
                    var Email = db.UserAccounts.Any(c => c.UserEmail == value.UserEmail);
                    var Password = db.UserAccounts.Any(v => v.UserPassword == value.UserPassword);
                    if (Email)
                    {
                        if (Password)
                        {
                            var data = db.UserAccounts.Where(c => c.UserPassword == value.UserPassword).ToList();
                            if (ModelState.IsValid)
                            {
                                using (db = new ChatingwebEntities())
                                {
                                    foreach (var item in data)
                                    {
                                        if (item.UserPassword == value.UserPassword)
                                        {
                                            UserAccountLogin Logi = new UserAccountLogin()
                                            {
                                                UserName = item.UserName,
                                                UserEmail = value.UserEmail,
                                                UserPassword = value.UserPassword,
                                                UserMobile = item.UserMobile,
                                                UserRagisterDate = item.UserRagisterDate,
                                                RagisterStatus = item.Status,
                                                Loginstatus = 1,
                                                UserLoginDate = Convert.ToString(DateTime.Now),
                                            };
                                                WriteCookie(Logi.UserName,Logi.UserMobile,Logi.UserEmail,Logi.UserPassword);
                                            if (Logi != null)
                                            {
                                                db.UserAccountLogins.Add(Logi);
                                                db.SaveChanges();
                                                ModelState.Clear();
                                                var name = db.UserAccounts.Where(c => c.UserEmail == value.UserEmail).Select(c => c.UserName).ToList();
                                                foreach (var nm in name)
                                                {
                                                    Session["Name"] = nm;
                                                }
                                                var mobile = db.UserAccounts.Where(c => c.UserEmail == value.UserEmail).Select(c => c.UserMobile).ToList();
                                                foreach (var ph in name)
                                                {
                                                    Session["Phone"] = ph;
                                                }
                                                return RedirectToAction("ChatBoxActon", "UserHome");

                                            }

                                        }
                                    }

                                };
                            }
                        }
                    }
                }

          }
          else 
          {
                Response.Write("<script>alert('you are bloked by admin please contect Suport number')</script>");
          }
            
            return View();
        }

        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpGet]
        public ActionResult NewPassword()
        {
            return View();  
        }
        public ActionResult About()
        {
            return View();
        }
    }
}



                        




//if (ModelState.IsValid)
//{
//    using (db = new ChatingwebEntities())
//    {
//        UserAccountLogin Login = new UserAccountLogin()
//        {
//            UserName = value.UserName,
//            UserEmail = value.UserEmail,
//            UserPassword = value.UserPassword,
//            UserMobile = value.UserMobile,
//            RagisterStatus = 1,
//            UserRagisterDate = Convert.ToString(DateTime.Now),

//        };
//        if (Login != null)
//        {
//            db.UserAccountLogins.Add(Login);
//            db.SaveChanges();
//            ModelState.Clear();
//        }
//    };

//}

//return View();
//        }
//    }