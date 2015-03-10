using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GameInterface.Domain.Enum;

namespace GameInterface.WebAPI.Models
{
    public class VOUser
    {
        public VOUser() 
        {
            this.ID = Guid.NewGuid();
            this.Status = UserStatus.正常.ToString();
            this.Type = UserType.普通用户.ToString();
        }
        /// <summary>
        /// 用户唯一标识
        /// </summary>

        public Guid ID { get; set; }

        /// <summary>
        /// 用户登录名
        /// </summary>

        public string UserName { get; set; }

        /// <summary>
        /// 用户密码 MD5加密
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 当前密码
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// 再次输入密码
        /// </summary>
        public string RePassword { get; set; }
        
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 用户类型（普通用户、管理员）
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }



        [Display(Name = "记住我")]
        public bool RememberMe { get; set; }


    }
}