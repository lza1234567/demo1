using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace demo1.Mode
{

    public class Users
    {
        #region 固定

        /// <summary>
        /// 主键 ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Column(TypeName = "bigint")]
        public long CreateId { get; set; }
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
    public class UserConfig : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("Sys_User");
            builder.Property(e => e.Id)
                .HasColumnType("bigint")
                .ValueGeneratedNever(); ///这个 Id 字段的值永远不会由数据库自动生成（例如自增 ID）
        }
    }

}
