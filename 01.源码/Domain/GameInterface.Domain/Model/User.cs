using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GameInterface.Domain.Model
{
    public class User : AggregateRoot
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        [Key]
        public Guid ID { get; set; }

        /// <summary>
        /// 用户登录名
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 用户密码 MD5加密
        /// </summary>
        [StringLength(65)]
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        [StringLength(50)]
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// 用户类型（普通用户、管理员）
        /// </summary>
        [Required]
        [StringLength(10)]
        public string Type { get; set; }

        [Required]
        [StringLength(6)]
        public string Status { get; set; }
    }
}
