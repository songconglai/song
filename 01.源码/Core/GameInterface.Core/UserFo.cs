
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GameInterface.Domain.Enum;
using GameInterface.Domain.Model;
using GameInterface.Domain.Repositories.Repositories;
using GameInterface.Common.Utils;
using GameInterface.JsonModel;

namespace GameInterface.Core
{
    /// <summary>
    /// 用户操作类
    /// </summary>
    public class UserFo
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static JsonUser UserLogin(string name, string password)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            password = MD5PasswordUtils.MD5Password(password);

            using (GameInterfaceDbContext context = new GameInterfaceDbContext())
            {
                var user = context.LoginUsers.FirstOrDefault(p => (p.UserName == name || p.Email == name) && p.Password == password);
                if (user == null)
                {
                    return null;
                }
                JsonUser jsonUser = new JsonUser();
                ObjectHelper.CopyToObject<JsonUser>(user, ref jsonUser);
                return jsonUser;
            }
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static DbStatus UserRegister(LoginUser loginUser)
        {
            using (GameInterfaceDbContext context = new GameInterfaceDbContext())
            {
                loginUser.CreateTime = DateTime.Now;
                loginUser.Password = MD5PasswordUtils.MD5Password(loginUser.Password);
                var user = context.LoginUsers.Add(loginUser);

                int i = context.SaveChanges();
                if (i > 0)
                {
                    return DbStatus.正常;
                }
                return DbStatus.异常;
            }
        }

        /// <summary>
        /// 用户名和邮箱是否已经存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static NameOrEmailIsExist UserNameOrEmailIsExist(string name, string email)
        {
            using (GameInterfaceDbContext context = new GameInterfaceDbContext())
            {
                var user = context.LoginUsers.FirstOrDefault(p => p.UserName == name || p.Email == email);
                if (user != null)
                {
                    if (user.UserName == name)
                    {
                        return NameOrEmailIsExist.用户名已存在;
                    }
                    else
                    {
                        return NameOrEmailIsExist.邮箱已存在;
                    }
                }
                return NameOrEmailIsExist.不存在;
            }
        }

        public static List<LoginUser> UserList()
        {

            using (GameInterfaceDbContext context = new GameInterfaceDbContext())
            {
                List<LoginUser> users = new List<LoginUser>();
                var list = from o in context.LoginUsers
                           where o.Status == UserStatus.正常.ToString() || o.Status == UserStatus.禁用.ToString()
                           orderby o.CreateTime descending
                           select o;
                if (list.Count() > 0)
                {
                    users.AddRange(list);
                }
                return users;
            }
        }


        public static LoginUser GetUser(string id)
        {
            if (string.IsNullOrEmpty(id)) { 
                return null;
            }
            using (GameInterfaceDbContext context = new GameInterfaceDbContext())
            {
                LoginUser user = context.LoginUsers.SingleOrDefault(m => m.ID == new Guid(id));
                return user;
            }
        }

        public static string ModifyUser(LoginUser user)
        {
            using (GameInterfaceDbContext context = new GameInterfaceDbContext())
            {
                LoginUser userL = context.LoginUsers.SingleOrDefault(m => m.ID == user.ID);
                userL.Type = user.Type;
                userL.Status = user.Status;
                int i = context.SaveChanges();
                if (i > 0)
                {
                    return "更新成功";
                }
                return "更新失败";
            }
        }
    }
}
