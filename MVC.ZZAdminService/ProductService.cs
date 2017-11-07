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

        public void Edit(product model)
        {
            var pro = Table.FirstOrDefault(p => p.Id == model.Id);
            pro.Title = model.Title??string.Empty;
            pro.Number = model.Number ?? string.Empty;
            pro.Price = model.Price;
            pro.Knows = model.Knows ?? string.Empty;
            pro.Include = model.Include ?? string.Empty;
            pro.ChildrenComment = model.ChildrenComment ?? string.Empty;
            pro.Days = model.Days ?? string.Empty;
            pro.GroupDays = model.GroupDays ?? string.Empty;
            pro.FavorablePrice = model.FavorablePrice;
            pro.Join = model.Join ?? string.Empty;
            pro.Knows = model.Knows ?? string.Empty;
            pro.Reason = model.Reason ?? string.Empty;
            pro.Special = model.Special ?? string.Empty;
            Scope.SubmitChanges();
           
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

        public bool Delete(int id)
        {
            var news = Table.FirstOrDefault(p => p.Id == id);
            if (news == null)
                return false;
            return Delete(news);
        }
        public virtual bool Delete(product model)
        {
            Table.DeleteOnSubmit(model);DB.SubmitChanges();
            return true;
        }


    }
}
