using MVC.ZZAdminModels;
using MVC.ZZAdminService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.ZZCommon.MVCPager;

namespace MVC.ZZWebSite.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index(int page=1 )
        {

            ViewBag.TiTle = "产品管理";


            using (ProductService service=new ProductService ())
            {
                var model = service.ManageList().ToPagedList(page,5);
                return View("Index", model);
            }
           
        }
        #region ajaxPost搜索

        [HttpPost]
        public ActionResult AjaxSearchPost(string title, string bdate, string edate)
        {
            return ajaxSearchPostResult(title, bdate, edate);
        }
        private ActionResult ajaxSearchPostResult(string title, string bdate, string edate)
        {
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
                    DateTime Bda = Convert.ToDateTime(edate);
                    qry = qry.Where<product>(u => u.ConfimTime >= Bda);
                }
                if (Request.IsAjaxRequest())
                    return RedirectToAction("index",qry);
                return RedirectToAction("index", qry);

            }
          
          
        } 
        #endregion

        #region 获取产品信息
        /// <summary>
        /// 获取所有的产品信息
        /// </summary>
        /// <returns>返回产品详细信息的Json对象</returns>
        public ActionResult GetAllProductInfos()
                {

                    //Json格式的要求{total:22,rows:{}}

                    //实现对用户分页的查询，rows：一共多少条，page：请求的当前第几页

                    int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);

                    int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);

                    //得到多条件查询的参数

                    string Title = Request["WHC_Title"];

                    string BDate = Request["WHC_BDate"];

                    string EDate = Request["WHC_EDate"];

                    //调用分页的方法，传递参数,拿到分页之后的数据

                    //var data = _userInfoService.LoadPageEntities(pageIndex, pageSize, out total,

                    //    u => true && u.DeletionStateCode == 0, true, u => u.SortCode);

                    //封装一个业务逻辑层的方法，来处理分页过滤事件

                    ProductQuery productQuery = new ProductQuery()
                    {

                        PageSize = pageSize,

                        PageIndex = pageIndex,

                        Title = Title,

                        Bdate = BDate,

                        Edate = EDate,

                        Total = 0

                    };
                    ProductService wxu = new ProductService();
                    var data = wxu.LoadSearchData(productQuery).OrderBy(p => p.ConfimTime);

                    //构造成Json的格式传递

                    var result = new { total = productQuery.Total, rows = data };

                    return Json(result, JsonRequestBehavior.AllowGet);

                }
                #endregion


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
                model.City = post.City;
                model.Price = post.Price;
                model.ProName = post.ProName;
                using (ProductService ps = new ProductService()){
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


    }
}