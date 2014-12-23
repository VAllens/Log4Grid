using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using Log4Grid.Models;

namespace Log4Grid.Management.Web.Codes
{
    public class Utils
    {
        private static readonly Config.InterfaceFactory MFactory = new Config.InterfaceFactory();

        public static User User
        {
            get
            {
                System.Collections.IDictionary dict = HttpContext.Current.Items;
                if (dict["_ISLOAD_USER"] == null)
                {
                    HttpCookie loginer = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                    if (loginer != null && loginer.Value != null)
                    {
                        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(loginer.Value);
                        string userid = ticket.Name;
                        User user = MFactory.User.Exists(userid);
                        dict["_LOGINER"] = user;
                    }
                    dict["_ISLOAD_USER"] = true;
                }
                return (User) dict["_LOGINER"];
            }
        }

        public static void SetLogin(string name)
        {
            FormsAuthentication.SetAuthCookie(name, true);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(name, false, 120);
            string encTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void Signout()
        {
            if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                HttpCookie myCookie = new HttpCookie(FormsAuthentication.FormsCookieName);
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }
        }

        private static Dictionary<LogType, string> _mClass;

        private static readonly object MLockClass = new object();

        public static Dictionary<LogType, string> Class
        {
            get
            {
                lock (MLockClass)
                {
                    if (_mClass == null)
                    {
                        _mClass = new Dictionary<LogType, string>();
                        _mClass.Add(LogType.Error, "danger");
                        _mClass.Add(LogType.Warn, "warning");
                        _mClass.Add(LogType.Info, "info");
                        _mClass.Add(LogType.Debug, "debug");
                        _mClass.Add(LogType.Fatal, "fatal");
                    }
                    return _mClass;
                }
            }
        }

        public static string GetRowClass(LogType type)
        {
            string value = null;
            if (!Class.TryGetValue(type, out value))
            {
                value = "active";
            }
            return value;
        }
    }
}