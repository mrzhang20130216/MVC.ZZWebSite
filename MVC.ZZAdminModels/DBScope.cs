using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.Linq;

namespace MVC.ZZAdminModels
{
    public class DBScope : IDisposable
    {
        /// <summary>
        /// 创建者
        /// </summary>
        private bool Creater { get; set; }

        public ZZDataContext DB
        {
            get
            {
                if (HttpContext.Current != null)
                {//Web 程序
                    if (HttpContext.Current.Items["CurrentTransactionContext"] is ZZDataContext)
                        return HttpContext.Current.Items["CurrentTransactionContext"] as ZZDataContext;
                    else
                    {
                        var db = CreateDataContext();
                        DB = db;
                        return db;
                    }
                }
                else
                {//Win 程序
                    return CreateDataContext();
                }
            }
            private set
            {
                if (HttpContext.Current != null)
                {//Web 程序
                    HttpContext.Current.Items["CurrentTransactionContext"] = value;
                }
            }
        }

        /// <summary>
        /// 创建数据上下文
        /// </summary>
        /// <returns></returns>
        private ZZDataContext CreateDataContext()
        {
            Creater = true;
            return new ZZDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["database"].ConnectionString);
        }

        public void Dispose()
        {
            if (Creater)
            {
                this.DB.Dispose();
                this.DB = null;
            }
        }
        public void SubmitChanges()
        {
            try
            {
                DB.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
            }
            catch (System.Data.Linq.ChangeConflictException ex)
            {
                //DB.ChangeConflicts.ResolveAll(RefreshMode.KeepCurrentValues);  //保持当前的值
                DB.ChangeConflicts.ResolveAll(RefreshMode.OverwriteCurrentValues);//保持原来的更新,放弃了当前的值.
                //DB.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);//保存原来的值 有冲突的话保存当前版本
                // 注意：解决完冲突后还得 SubmitChanges() 一次，不然一样是没有更新到数据库的
                DB.SubmitChanges();
            }
        }
    }
}
