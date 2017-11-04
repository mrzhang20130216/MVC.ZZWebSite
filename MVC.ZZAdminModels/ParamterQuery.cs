using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.ZZAdminModels
{
    /// <summary>
    /// 分页查询 基础类
    /// </summary>
    public class ParamterQuery
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Total { get; set; }
    }
}
