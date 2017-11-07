using MVC.ZZAdminModels;
using MVC.ZZCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.ZZAdminService
{
   public  class ProductDetailsService : BaseService<productDetails>
    {
        public IQueryable<productDetails> ManageList()
        {
            return Table;
        }

        public bool Delete(int id)
        {
            var news = Table.FirstOrDefault(p => p.Id == id);
            if (news == null)
                return false;
            return Delete(news);
        }
        public virtual bool Delete(productDetails model)
        {
            Table.DeleteOnSubmit(model);
            DB.SubmitChanges();
            return true;
        }
        /// <summary>
        /// 根据id查询产品日程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public productDetails GetById(int id)
        {
            return Table.SingleOrDefault(a => a.Id == id);
        }
        public void Edit(productDetails model)
        {
            var pro = Table.FirstOrDefault(p => p.Id == model.Id);
           // pro.City = model.City ;
          //  pro.CityId = 7;//model.CityId;
          //  pro.CityName = model.CityName;
            pro.Content = model.Content;
            pro.CreateTime = DateTime.Now;
            pro.DaysType = model.DaysType;
            pro.Hotel = model.Hotel;
            pro.ParkInto = model.ParkInto;
            pro.ParkName = model.ParkName;
            pro.Pic = model.Pic;
            pro.Remark = model.Remark;
            pro.Rice = model.Rice;
            pro.Shopping = model.Shopping;
            pro.ShoppingContent = model.ShoppingContent;
            pro.VisitTime = model.VisitTime;
            Scope.SubmitChanges();

        }

        public void Create(productDetails model)
        {
            var pro=new productDetails();
            pro.Content = model.Content??string.Empty;
            pro.CreateTime = DateTime.Now;
            pro.DaysType = model.DaysType ?? string.Empty;
            pro.Hotel = model.Hotel ?? string.Empty;
            pro.ParkInto = model.ParkInto ?? string.Empty;
            pro.ParkName = model.ParkName ?? string.Empty;
            pro.Pic = model.Pic ?? string.Empty;
            pro.Remark = model.Remark ?? string.Empty;
            pro.Rice = model.Rice ?? string.Empty;
            pro.Shopping = model.Shopping ?? string.Empty;
            pro.ShoppingContent = model.ShoppingContent ?? string.Empty;
            pro.VisitTime = model.VisitTime ;
            Table.InsertOnSubmit(model);
            Scope.SubmitChanges();
        }
        }
}
