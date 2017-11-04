using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC.ZZCommon;
using MVC.ZZAdminService;
using MVC.ZZAdminModels;

namespace MVC.ZZWebSite.Areas.Admin.Controllers
{
    public class IndexController : Controller
    {
        // GET: Admin/Index
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Login(){ ViewBag.Title = "中联管理后台登录"; return View();}

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="用户名"></param>
        /// <param name="密码"></param>
        /// <param name="验证码"></param>
        /// <returns></returns>
        public JsonResult LoginIn(string UserName, string Password, string Code)
        {

            if (Session["ValidateCode"] == null)
            {
                return JsonHander.CreateJsonResultMessage(0, "请刷新验证码！", true);
            }
            if (Session["ValidateCode"].ToString().ToLower() != Code.ToLower())
            {
                return JsonHander.CreateJsonResultMessage(0, "验证码有误！", true);
            }
            //LogOnModel model = new LogOnModel();
            UsersService a = new ZZAdminService.UsersService();
            var result = a.Validate(UserName, Password, true);
            if (result is Guid)
            {
                Session["UGuid"] = result;
                //登录验证
                FormsAuthentication.SetAuthCookie(result.ToString(), true);
                return JsonHander.CreateJsonResultMessage(1, "登录成功！", true);

            }
            else
            {
                return JsonHander.CreateJsonResultMessage(0, result.ToString(), true);
            }


        }
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult ValidateCode()
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(4);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }


        public ActionResult Home()
        {
            return View();
        }
        //public ActionResult Reg()
        //{
        //    UsersService a = new ZZAdminService.UsersService();
        //    Users u = new Users();
        //    a.Register(u);
        //    return View();
        //}
    }
}