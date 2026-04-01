using System.Security.Claims;

namespace demo1.Uitl.JWT
{
    public static class NowLoginUserInfo
    {
        /// <summary>
        /// 获取当前登录用户id
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static long GetUserId(this ClaimsPrincipal user)
        {
            var id = user.FindFirstValue(ClaimTypes.NameIdentifier);
            return long.TryParse(id, out long result) ? result : 0;
        }
        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.Identity?.Name ?? user.FindFirstValue(ClaimTypes.Name);
        }
        /// <summary>
        /// 判断用户是否权限
        /// var claims = new List<Claim>
        //{
        //    // ... 其他声明
        //    new Claim("permission", "article:delete"),
        //    new Claim("permission", "article:edit")
        //};
        /// </summary>
        /// <param name="user"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        public static bool HasPermission(this ClaimsPrincipal user, string permission)
        {
            return user.HasClaim(c => c.Type == "permission" && c.Value == permission);
        }
         
        public static string GetRedisTokenUserIDKey(this ClaimsPrincipal user )
        {
            return $"token:userID:{user.GetUserId()}";
        }
        public static string GetRedisTokenSessionKey(this ClaimsPrincipal user ,string Id)
        {
            return $"token:session:{Id}";
        }
    }
}
