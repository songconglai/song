using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInterface.Core
{
    public class PagerInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int RecordCount { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPageIndex { get; set; }
        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; }
    }

    public class PagerQuery<TPager, TEntityList> 
    {
        public PagerQuery(TPager pager, TEntityList entityList)
        {
            this.Pager = pager;
            this.EntityList = entityList;
        }
        public TPager Pager { get; set; }
        public TEntityList EntityList { get; set; }
    }
}
