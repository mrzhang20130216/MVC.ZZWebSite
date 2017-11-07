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


        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(FormCollection form)
        {
            string message = string.Empty, path = string.Empty;
            // if (!IsAuthenticated) message = "需上传文件,请先登录";

            if (Request.Files.Count == 0) message = "未检测到提交文件";

            Upfile upfile = new Upfile();
            upfile.Key = ParamHelper.GetInt(Request["key"]);
            //upfile.Location = ParamHelper.GetInt(Request["location"]);
            upfile.Module = (short)ParamHelper.GetInt(Request["module"]);
            // if (!string.IsNullOrEmpty(Request["guid"])) upfile.Guid = Guid.Parse(Request["guid"]);
            //upfile.AccountID = AccountInfo.ID;
            //存储目录
            var Prefix = "~/upfile/";
            var Path = Prefix;
            var SavePath = Server.MapPath(Path);
            if (!System.IO.Directory.Exists(SavePath))
                System.IO.Directory.CreateDirectory(SavePath);

            HttpPostedFileBase post = Request.Files[0];
            if (post.ContentLength == 0)
                message = "无效文件";
            if (post.ContentLength > 0x100000)
                message = "图片大小超过限制 1MB";
            var FileName = "";
            //判断文件类型
            string extension = System.IO.Path.GetExtension(post.FileName).ToLower();
            if (string.IsNullOrEmpty(message) && Regex.IsMatch(extension, @".(jpg)", RegexOptions.IgnoreCase))//限制上传类型
            {//图片文件
                FileName = SavePath + DateTime.Now.ToString("yyMMddHHmmssffff") + extension;
                try
                {
                    post.SaveAs(FileName);
                }
                catch
                {
                    System.IO.File.Delete(FileName);
                    message = "文件上传出错";
                }
                var width = ParamHelper.GetInt(Request["width"]);//图片宽度
                var height = ParamHelper.GetInt(Request["height"]);//图片高度
                if (width > 0 && height > 0)//重置图片
                    ImageHelper.Thumbnails(FileName, FileName, width, height, "#ffffff", "#ffffff");
                else
                {
                    //压缩图片
                    var maxWidth = ParamHelper.GetInt(Request["maxwidth"]);//最大宽度
                    var maxHeight = ParamHelper.GetInt(Request["maxheight"]);//最大高度
                    if (maxWidth > 0 || maxHeight > 0)
                        ImageHelper.Compress(FileName, maxWidth, maxHeight);//压缩图片
                    else
                    {

                        maxHeight = 1200; maxWidth = 1200;
                        ImageHelper.Compress(FileName, maxWidth, maxHeight);//压缩图片
                    }
                }
                var watermark = Request["watermark"];//是否水印
                upfile.Path = FileName.Replace(Server.MapPath(Prefix), "").Replace("\\", "/");
                //using (UpfileService service = new UpfileService())
                //{
                //    service.Create(upfile);
                //}
                path = "/upfile/" + upfile.Path;
            }
            else if (string.IsNullOrEmpty(message))
            {
                message = "不支持 " + extension + "格式文件上传";
            }

            if (!string.IsNullOrEmpty(Request["CKEditor"]))//如果是CKEditor 提交
                return Content(string.Format("<script type=\"text/javascript\">window.parent.CKEDITOR.tools.callFunction({0},'{1}','{2}') ;</script>",
                                                    Request["CKEditorFuncNum"], path, string.Empty));
            return Json(new { message = string.IsNullOrEmpty(message) ? "success" : message, path = path }, "text/html", JsonRequestBehavior.AllowGet);
        }
    }
}