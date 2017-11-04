using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.ZZAdminModels
{
    /// <summary>
    /// 产品查询
    /// </summary>
    public class ProductQuery: ParamterQuery
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string Bdate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string Edate { get; set; }
    }
}
