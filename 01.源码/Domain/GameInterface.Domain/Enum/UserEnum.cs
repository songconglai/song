using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInterface.Domain.Enum
{
    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserType
    {
        普通用户,
        管理员
    }

    public enum UserStatus
    {
        删除 = -2,
        禁用 = -1,
        正常 = 0,

    }

    public enum NameOrEmailIsExist 
    {
        用户名已存在,
        邮箱已存在,
        不存在
    }

}
