using System;
using System.Data.Linq;
using System.Linq.Expressions;
using MVC.ZZAdminModels;

namespace MVC.ZZCommon
{
    public class BaseService : IDisposable
    {
        protected DBScope Scope;
        protected ZZDataContext DB { get { return Scope.DB; } }
        /// <summary>
        /// 自动提交
        /// </summary>
        public bool AutoSubmit { get; set; }
        public BaseService()
        {
            Scope = new DBScope();
            AutoSubmit = true;
        }
        public void SubmitChanges()
        {
            Scope.SubmitChanges();
        }
        protected void AutoSubmitChanges()
        {
            if (AutoSubmit) SubmitChanges();
        }
        public void Dispose()
        {
            Scope.Dispose();
        }
    }
    public class BaseService<T> : BaseService where T : class
    {
        public virtual Table<T> Table { get { return DB.GetTable<T>(); } }
        public virtual void LoadWithFor(params Expression<Func<T, object>>[] expression)
        {
            if (expression == null) return;
            DataLoadOptions option = DB.LoadOptions;
            if (option == null) option = new DataLoadOptions();
            foreach (var p in expression)
                option.LoadWith<T>(p);
            if (DB.LoadOptions == null) DB.LoadOptions = option;
        }
        public virtual void LoadWith<Y>(params Expression<Func<Y, object>>[] expression)
        {
            if (expression == null) return;
            DataLoadOptions option = DB.LoadOptions;
            if (option == null) option = new DataLoadOptions();
            foreach (var p in expression)
                option.LoadWith<Y>(p);
            if (DB.LoadOptions == null) DB.LoadOptions = option;
        }
    }
}
