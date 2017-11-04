using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC.ZZCommon
{
    public class JsonHander
    {
        /// <summary>
        /// 创建返回格式为JsonResult的提示信息
        /// </summary>
        /// <param name="type">成功=1、失败=2</param>
        /// <param name="Messages">提示信息</param>
        /// <param name="IsAllowGet">是否允许Get [IsAllowGet]=true </param>
        /// <returns>JsonResult</returns>
        public static JsonResult CreateJsonResultMessage(int Sussces, string MessagesBox, bool IsAllowGet)
        {
            var res = new JsonResult();
            var date = new { type = Sussces, Messages = MessagesBox };
            res.Data = date;
            if (IsAllowGet)
            {
                res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            }
            return res;
        }
        /// <summary>
        /// 创建返回格式为JsonResult的提示信息
        /// </summary>
        /// <param name="type">成功=1、失败=2</param>
        /// <param name="Messages">提示信息</param>
        /// <returns>JsonResult</returns>
        public static JsonResult CreateJsonResultMessage(int Sussces, string MessagesBox)
        {
            var res = new JsonResult();
            var date = new { type = Sussces, Messages = MessagesBox };
            res.Data = date;
            return res;
        }
    }
}
