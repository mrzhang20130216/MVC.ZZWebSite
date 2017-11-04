using MVC.ZZAdminModels;
using MVC.ZZCommon;
using MVC.ZZCommon.MVCPager;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.ZZAdminService
{
   public  class ProductService : BaseService<product>
    {
        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<product> ManageList()
        {
            return Table;
        }

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<product> LoadSearchData(ProductQuery query)
        {

            var temp = (
                  from m in Table
                  select m
                        );
            //_DbSession.BaseUserRepository.LoadEntities(u => true);

            //首先过滤产品名称

            if (!string.IsNullOrEmpty(query.Title))
            {
                temp = temp.Where<product>(u => u.Title.Contains(query.Title));  //like '%mmm%'
            }
            if (!string.IsNullOrEmpty(query.Edate))
            {
                DateTime Eda = Convert.ToDateTime(query.Edate);
                temp = temp.Where<product>(u => u.ConfimTime <= Eda);
            }
            if (!string.IsNullOrEmpty(query.Bdate))
            {
                DateTime Bda = Convert.ToDateTime(query.Bdate);
                temp = temp.Where<product>(u => u.ConfimTime >= Bda);
            }
            
            query.Total = temp.Count();

            return temp.OrderBy(u => u.Id).Skip(query.PageSize * (query.PageIndex - 1)).Take(query.PageSize);

        }


        /// <summary>
        /// 新增产品
        /// </summary>
        /// <param name="model"></param>
        public void Create(product model)
        {
            model.CreateTime = DateTime.Now;
            if (string.IsNullOrEmpty(model.Title)) model.Title =String.Empty;
            if (string.IsNullOrEmpty(model.Number)) model.Title = String.Empty;
            if (string.IsNullOrEmpty(model.Intro)) model.Title = String.Empty;
            if (string.IsNullOrEmpty(model.PriceComment)) model.Title = String.Empty;
            if (string.IsNullOrEmpty(model.Reason)) model.Title = String.Empty;
            if (string.IsNullOrEmpty(model.Special)) model.Title = String.Empty;
            if (string.IsNullOrEmpty(model.Days)) model.Title = String.Empty;
            if (string.IsNullOrEmpty(model.GroupDays)) model.Title = String.Empty;
            if (string.IsNullOrEmpty(model.Include)) model.Title = String.Empty;
            if (string.IsNullOrEmpty(model.Knows)) model.Title = String.Empty;
            if (string.IsNullOrEmpty(model.Point)) model.Title = String.Empty;
            if (string.IsNullOrEmpty(model.Join)) model.Title = String.Empty;
            if (string.IsNullOrEmpty(model.ChildrenComment)) model.Title = String.Empty;
            if (string.IsNullOrEmpty(model.CityName)) model.Title = String.Empty;
            if (string.IsNullOrEmpty(model.CountryName)) model.Title = String.Empty;
            if (string.IsNullOrEmpty(model.ProName)) model.Title = String.Empty;
            if (string.IsNullOrEmpty(model.Pcover)) model.Title = String.Empty;
            Table.InsertOnSubmit(model);
            DB.SubmitChanges();
        }

        /// <summary>
        /// 根据id查询产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public product GetById(int id)
        {
            return Table.SingleOrDefault(a => a.Id == id);
        }


       
    }
}
