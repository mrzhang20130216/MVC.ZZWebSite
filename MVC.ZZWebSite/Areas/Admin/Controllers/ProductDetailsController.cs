using MVC.ZZAdminService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.ZZCommon.MVCPager;
using MVC.ZZAdminModels;

namespace MVC.ZZWebSite.Areas.Admin.Controllers
{
    /// <summary>
    /// 产品行程信息
    /// </summary>
    public class ProductDetailsController : Controller
    {
        // GET: Admin/ProductDetails
        public ActionResult Index(int page = 1, int ProductID = 0)
        {
            using (ProductDetailsService wxu = new ProductDetailsService())
            {
                var qry = wxu.ManageList().AsQueryable();
                if (ProductID != 0) qry = qry.Where(p => p.ProductId== ProductID);
                var model = qry.OrderByDescending(a => a.CreateTime).ToPagedList(page,5);

                ProductService ps = new ProductService();
                product s= ps.GetById(ProductID);
                if (s != null) {
                    ViewBag.productTitle = s.Title; ViewBag.ProductID = ProductID;
                }
                else
                {
                    ViewBag.productTitle = "";
                    ViewBag.ProductID = 0;
                }
                return View(model);
            }

        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            using (ProductDetailsService service = new ProductDetailsService())
            {
                if (service.Delete(id))
                    return Json(true, JsonRequestBehavior.AllowGet);
                else
                    return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Edit(int ProId)
        {
            ViewBag.TiTle = "产品详细";
            ProductDetailsService ps = new ProductDetailsService();
            productDetails model = ps.GetById(ProId);
            return View(model);
        }
        [HttpPost]
        public JsonResult Edit(productDetails post)
        {
            try
            {
                ProductDetailsService service = new ProductDetailsService();
                service.Edit(post);
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Create(productDetails post)
        {
            try
            {
                ProductDetailsService service = new ProductDetailsService();
                service.Create(post);
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

    }
}