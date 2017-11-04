using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC.ZZAdminModels;
using MVC.ZZCommon;
using System.Security.Cryptography;

namespace MVC.ZZAdminService
{
    public class UsersService : BaseService<Users>
    {
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>错误信息 或者 Guid</returns>
        public object Validate(string username, string password, bool toMD5 = true)
        {
            var model = Table.FirstOrDefault(p => p.Mobile == username);
            if (model == null) return "用户名不存在";//用户名不存在
            if (toMD5)
                password = ToMD5(password);
            if (model.Pass != password) return "密码输入错误";//密码错误
           // model.Guid = Guid.NewGuid();
            Scope.SubmitChanges();
            return Guid.NewGuid();
          
        }

        public string ToMD5(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(result).Replace("-", "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns>返回 错误信息 或者 Success</returns>
        public string Register(Users account)
        {
           // if (Table.Any(p => p.Mobile =="admin")) return "用户名已存在";
            account.Pass = ToMD5("123456");
            account.Mobile = "admin";
            account.CreateTime = DateTime.Now;
            account.Status = 0;
            Table.InsertOnSubmit(account);
            Scope.SubmitChanges();
            return "Success";
        }
    }
}
