using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.ZZCommon
{
    public class ParamHelper
    {
        public static int GetInt(object obj)
        {
            string val = Convert.ToString(obj);
            if (string.IsNullOrEmpty(val)) return 0;
            try
            {
                return int.Parse(val);
            }
            catch
            {
                return 0;
            }
        }
        public static string GetString(object obj)
        {
            string val = Convert.ToString(obj);
            if (val == null) return string.Empty;
            return val;
        }
    }
}
