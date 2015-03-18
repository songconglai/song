using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using GameInterface.Common.Utils;
using GameInterface.JsonModel;
using GameInterface.WebAPI.Models;
using GameInterface.Core;
using System.Text.RegularExpressions;
using GameInterface.Domain.Enum;
using GameInterface.Domain.Model;


namespace GameInterface.WebAPI.WebControllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        /// <summary>
        /// 登录页面
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <returns></returns>
        public ActionResult Index(string message)
        {
            VOUser user = new VOUser();
            ViewBag.Message = message;

            HttpCookie cookie = CookieUtils.GetCookie(System.Web.HttpContext.Current, "LogonID");
            if (cookie != null)
            {
                user.UserName = cookie.Value;
                user.RememberMe = true;
            }
            return View(user);
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            CookieUtils.RemoveCookie("LogonID", System.Web.HttpContext.Current);
            GlobalVariables.CurrentUser = new JsonUser();

            return RedirectToAction("Index", "Login", new { message = "您已成功注销！" });
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SumbitLogon(VOUser user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                return RedirectToAction("Index", "Login", new { message = "登录名，密码不能为空！" });
            }

            JsonUser jsonUser = UserFo.UserLogin(user.UserName, user.Password);

            CookieUtils.AddCookie("LogonID", user.UserName, System.Web.HttpContext.Current);

            if (user.RememberMe)
            {
                HttpCookie cookie = CookieUtils.GetCookie(System.Web.HttpContext.Current, "LogonID");
                cookie.Expires = DateTime.Now.AddDays(7);
            }
            GlobalVariables.CurrentUser = jsonUser;

            if (jsonUser == null)
            {
                return RedirectToAction("Index", "Login", new { message = "用户名，密码验证失败！" });
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 注册页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Register() 
        {
            return View(new VOUser());
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(VOUser user, string message)
        {

            ViewBag.Message = message;
            bool isCanRegist = true;
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Email))
            {
                ViewBag.Message += "用户名或者密码不能为空;";
                isCanRegist = false;
            }
            if (user.Password != user.RePassword) 
            {
                ViewBag.Message += "输入的密码不一样;";
                isCanRegist = false;
            }
            
            string expression = @"([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,5})+";
            if (isCanRegist == true && !Regex.IsMatch(user.Email, expression, RegexOptions.Compiled))
            {
                ViewBag.Message += "邮箱格式不正确;";
                isCanRegist = false;
            }
            if(user.UserName !=null && user.Email !=null){
            NameOrEmailIsExist isexist = UserFo.UserNameOrEmailIsExist(user.UserName, user.Email);
            if (isexist != NameOrEmailIsExist.不存在) {
                ViewBag.Message += isexist.ToString();
                isCanRegist = false;
            }
            }
            if (isCanRegist)
            {
                LoginUser loginuser =new LoginUser();
                ObjectHelper.CopyToObject<LoginUser>(user,ref loginuser);
                
                DbStatus dbStatus = UserFo.UserRegister(loginuser);
                if (DbStatus.正常 == dbStatus)
                {
                    return RedirectToAction("Index", "Login");
                    //return RedirectToAction("Index", "Home");
                }
            }
            user.Password = "";
            user.RePassword = "";
            return View(user);

        }

        /// <summary>
        /// 用户管理列表
        /// </summary>
        /// <returns></returns>
        public ActionResult UserList( int? pageIndex,int? pageSize) 
        {
            int pageSizeI = pageSize ?? 5;
            int pageIndexI = pageIndex ?? 1;
            PagerQuery<PagerInfo, List<LoginUser>> userList = UserFo.UserList(pageSizeI, pageIndexI);
            return View(userList);
        }

        public ActionResult EditeUser(string id) 
        {
            LoginUser loginUser = UserFo.GetUser(id);
            
            return View(loginUser);
        }

        [HttpPost]
        public ActionResult EditeUser(LoginUser user)
        {
            ViewBag.Message =  UserFo.ModifyUser(user);;
            return View(user);
        }
    }
}
