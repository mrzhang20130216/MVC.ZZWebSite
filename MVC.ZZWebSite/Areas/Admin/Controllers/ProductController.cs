using MVC.ZZAdminModels;
using MVC.ZZAdminService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.ZZCommon.MVCPager;
using System.EnterpriseServices;

namespace MVC.ZZWebSite.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index(int page=1 , string title=null, string bdate=null, string edate=null)
        {
            

            ViewBag.TiTle = "产品管理";
            using (ProductService wxu = new ProductService())
            {
                var qry = wxu.ManageList().AsQueryable();
                if (!string.IsNullOrWhiteSpace(title)) qry = qry.Where(p => p.Title.Contains(title));
                if (!string.IsNullOrWhiteSpace(bdate))
                {
                    DateTime Bda = Convert.ToDateTime(bdate);
                    qry = qry.Where<product>(u => u.ConfimTime >= Bda);
                }
                if (!string.IsNullOrWhiteSpace(edate))
                {
                    DateTime Eda = Convert.ToDateTime(edate);
                    qry = qry.Where<product>(u => u.ConfimTime >= Eda);
                }
                var model = qry.OrderByDescending(a => a.CreateTime).ToPagedList(page, 5);
                return View(model);
            }
        }
        /// <summary>
        /// 产品详细
        /// </summary>
        /// <param name="ProId"></param>
        /// <returns></returns>
        public ActionResult Edit(int ProId)
        {
            ViewBag.TiTle = "产品详细";
            ProductService ps = new ProductService();
            product model = ps.GetById(ProId);
            return View(model);
        }

        [HttpPost]
        public JsonResult Edit(product post)
        {
            try
            {
                ProductService service = new ProductService();
                service.Edit(post);
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            
        }

        #region 添加产品
        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public ActionResult Create(product post)
        {
            try
            {
                product model = new product();
                model.Title = post.Title;
                model.Price = post.Price;
                model.ProName = post.ProName;
                using (ProductService ps = new ProductService())
                {
                    ps.Create(model);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                //ErrorMessage = "程序添加出错,请保存当前资料 - " + ex.Message;
                return View("Edit", post);
            }

        }
        #endregion
        [HttpPost]
        public JsonResult Delete(int id)
        {
            using (ProductService service = new ProductService())
            {
                if (service.Delete(id))
                    return Json(true, JsonRequestBehavior.AllowGet);
                else
                    return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
      
       
    }
}