using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Log4Grid.Management.Web.Controllers.Filters;
using Log4Grid.Models;

namespace Log4Grid.Management.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        private readonly Config.InterfaceFactory _mFactory = new Config.InterfaceFactory();

        [Login]
        public ActionResult Index()
        {
            Models.IndexView iv = new Models.IndexView { Apps = _mFactory.Management.ListApp() };
            return View(iv);
        }

        [Login]
        public ActionResult GetAppStatus()
        {
            ContentResult cr = new ContentResult();
            IList<ApplicationData> result = _mFactory.Management.ListApp();
            cr.Content = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return cr;
        }

        [Login]
        public ActionResult LogList(string app, string host, LogType? type, DateTime? from, DateTime? to, int? pageIndex)
        {
            Models.HomeLogList hl = new Models.HomeLogList();
            int pages = 0;
            if (!string.IsNullOrEmpty(app))
            {
                hl.Logs = _mFactory.Search.List(app, host, type, from, to, pageIndex ?? 0, out pages);
            }
            else
            {
                hl.Logs = new List<LogModel>();
            }
            hl.PageIndex = pageIndex ?? 0;
            hl.Pages = pages;
            return View(hl);
        }

        [AjaxSuccess]
        public ActionResult CleanApp()
        {
            _mFactory.Management.CleanApp();
            return View();
        }

        public ActionResult Logout()
        {
            Codes.Utils.Signout();
            return new RedirectResult("/Login");
        }

        public ActionResult Login(bool? post, string name, string pwd)
        {
            string result = "";
            if (post != null && post.Value)
            {
                User user = _mFactory.User.Login(name, pwd);
                if (user != null)
                {
                    Codes.Utils.SetLogin(user.Name);
                    return new RedirectResult("/");
                }
                result = "用户名或密码不可用";
            }
            return View((object)result);
        }

        [AjaxSuccess]
        public ActionResult DeleteUser(string name)
        {
            if (!HttpContext.Request.IsLocal)
            {
                throw new Exception("必须在局域网内访问!");
            }
            _mFactory.User.Delete(name);
            return View();
        }

        public ActionResult Users()
        {
            Models.HomeUsers user = new Models.HomeUsers();
            if (HttpContext.Request.IsLocal)
            {
                user.Users = _mFactory.User.List();
            }
            else
            {
                ViewBag.Message = "读取用户列表失败,必须在局域网内访问!";
            }
            return View(user);
        }

        public ActionResult CreateUser(bool? post, string name, string pwd, string rpwd)
        {
            string result = "";
            if (!HttpContext.Request.IsLocal && Request.HttpMethod.ToLower() == "post")
            {
                result = "创建用户失败,必须在局域网内访问!";
                return View((object)result);
            }
            if (!HttpContext.Request.IsLocal)
            {
                result = "必须在局域网内访问!";
                return View((object)result);
            }
            if (post != null && post.Value)
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pwd))
                {
                    result = "用户名或密码不能为空";
                }
                else if (pwd != rpwd)
                {
                    result = "确认密码和密码不一致";
                }
                else
                {
                    User user = _mFactory.User.Create(name, pwd, "", true);
                    if (user == null)
                    {
                        result = "用户已存在";
                    }
                    else
                    {
                        result = "创建成功";
                    }
                }
            }
            return View((object)result);
        }

        [AjaxSuccess]
        [Login]
        public ActionResult Clean(string app, string host)
        {
            _mFactory.Store.Clean(app, host);
            return View();
        }

        [AjaxSuccess]
        [Login]
        public ActionResult Backup(string app, string host)
        {
            _mFactory.Store.Backup(app, host);
            return View();
        }
    }
}