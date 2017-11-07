using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace MVC.ZZCommon
{
    public abstract class BaseViewPage<T> : System.Web.Mvc.WebViewPage<T>
    {
        #region ImprotScript
        public Dictionary<string, int> Script
        {
            get
            {
                if (ViewContext.TempData["ScriptList"] == null)
                    ViewContext.TempData["ScriptList"] = new Dictionary<string, int>();
                return ViewContext.TempData["ScriptList"] as Dictionary<string, int>;
            }
        }
        public void ImportScript(string url)
        {
            ImportScript(url, 0);
        }
        public void ImportScript(string url, int order)
        {
            if (Script.ContainsKey(url))
                return;
            Script.Add(url, order);
        }
        public MvcHtmlString RenderScritp()
        {
            StringBuilder script = new StringBuilder();
            foreach (var item in Script.OrderByDescending(p => p.Value))
            {
                script.AppendFormat("<script src=\"{0}\" type=\"text/javascript\"></script>\r\n", Url.Content(item.Key));
            }
            return new MvcHtmlString(script.ToString());
        }
        #endregion
    }
    public abstract class BaseViewPage : BaseViewPage<dynamic> { }
    public abstract class AdminViewPage : AdminViewPage<dynamic> { }
    public abstract class AdminViewPage<T> : BaseViewPage<T>
    {

    }
}
