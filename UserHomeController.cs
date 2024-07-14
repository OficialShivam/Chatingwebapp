using Chatingweb.DBWorkChat;
using Chatingweb.WebChatDataEDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chatingweb.Controllers
{
    public class UserHomeController : Controller
    {
        // GET: UserHome
        public ActionResult Index()
        {
            if (Session["Name"] != null)
            {
                return View();
            }
            else
            {
                return View("~/Views/Home/Login.cshtml");
            }
            
        }
        public ActionResult ChatBoxActon()
        {
            if (Session["Name"] != null)
            {
                var nm = Request.QueryString["mb"];
                var unm = Request.QueryString["nam"];
                //TempData["nm"]=nm;
                //TempData["nm1"]=unm;
                using (var bd = new ChatingwebEntities())
                {
                    var tb = bd.AccountPerstionaluserInfoes.Where(s => s.LoginUserMobileNo == nm).ToList();
                    return View(tb);
                }
                //return View();
            }
            else
            {
                return View("~/Views/Home/Login.cshtml");
            }

            
          
           // return View();
            
        }
        
       
       public ActionResult ViewProfile()
       {
            var em = Session["Email"];
            //var em = ViewBag["email"];
            using (var db= new ChatingwebEntities())
            {
                var tb = db.UserAccountLogins;//.FirstOrDefault(s => s.UserEmail);
                if(tb!=null)
                {
                    return View(tb);
                }
                else
                {
                    Response.Write("<script>alert('Profile not show some technical issue')</script>");
                }
            }
            //if (Session["Name"] != null)
            //{
            //    return View();
            //}
            //else
            //{
            //    return View("~/Views/Home/Login.cshtml");
            //}
            return View("~/Views/Home/Login.cshtml");
        }
        [HttpGet]
        public ActionResult AddNewUserChatCOnntect()
        {
            var nm = Request.QueryString["mb"];
            var unm = Request.QueryString["nam"];
            return View();
        }
        [HttpPost]
        public ActionResult AddNewUserChatCOnntect(AccountPerstionalifouser value)
        {
            var db = new ChatingwebEntities();
            
            if (ModelState.IsValid)
            {
                var tbl = db.AccountPerstionaluserInfoes.Any(s => s.AddNewUserPhone == value.AddNewUserPhone);
                var nm = db.AccountPerstionaluserInfoes.Any(s => s.NewUerName == value.NewUerName);
                if (tbl && nm)
                {
                    ViewBag.Name = "This" + "" + value.NewUerName + "" + "Allready Ragistered";
                    ViewBag.Mobile = "This" + "" + value.AddNewUserPhone + "" + "Allready Ragistered";
                }
                else
                {
                    using (var dbt = new ChatingwebEntities())
                    {
                        //string str = Request.QueryString["nam"].ToString();
                        var nm1 = Request.QueryString["mb"];
                        var unm = Request.QueryString["nam"];
                        AccountPerstionaluserInfo data = new AccountPerstionaluserInfo()
                        {
                            LoginUserMobileNo = TempData["nm"].ToString(),
                            LoginUserName = TempData["nm1"].ToString(),
                            AddNewUserPhone = value.AddNewUserPhone,
                            NewUerName = value.NewUerName,
                            NewuserPictureProfile = value.NewuserPictureProfile,
                            NewUserStatus = 0,
                        };
                        if(data!=null)
                        {
                            db.AccountPerstionaluserInfoes.Add(data);
                            db.SaveChanges();
                            ModelState.Clear();
                            return View("ChatBoxActon");
                        }
                    }
                }
            }
            return View();
        }
    }
}