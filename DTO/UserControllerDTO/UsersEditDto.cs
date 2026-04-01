using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserControllerDTO
{

    public class UsersEditDto
    {
        #region 固定

        /// <summary>
        /// 主键 ID
        /// </summary>
        public long Id { get; set; }
         
        #endregion

        /// <summary>
        /// 获取或设置用户的姓名。
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置用户的密码。
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置用户的电话。
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string Phone { get; set; }

        /// <summary>
        /// 性别 1男2女
        /// </summary>
        [Column(TypeName = "int")]

        public int Gender { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [Column(TypeName = "bigint")]
        public long DepartmentId { get; set; }
    }
}
