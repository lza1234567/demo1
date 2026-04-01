using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserControllerDTO
{ 

    public class UsersFindDto
    {
        #region 固定
         
        #endregion 
        /// <summary>
        /// 获取或设置用户的姓名。
        /// </summary> 
        public string Name { get; set; }
          
        /// <summary>
        /// 获取或设置用户的电话。
        /// </summary> 
        public string Phone { get; set; }

        /// <summary>
        /// 性别 1男2女
        /// </summary> 
        public int Gender { get; set; }


        /// <summary>
        /// 偏移量 当前页数 Skip
        /// </summary>
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 一页的行数  Take
        /// </summary>
        public int PageSize { get; set; } = 50;
    }
}
