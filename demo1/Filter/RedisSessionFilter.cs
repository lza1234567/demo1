using System.IdentityModel.Tokens.Jwt; 
using System.Security.Claims; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
 
using demo1.Uitl.JWT; 
using StackExchange.Redis;

namespace demo1.Filter
{
    public class RedisSessionFilter : IAsyncAuthorizationFilter
    {

        private readonly IDatabase _redis;
        private readonly IConnectionMultiplexer _redis1;
        public RedisSessionFilter(  IConnectionMultiplexer redis)
        { 
            this._redis = redis.GetDatabase(); 
        }


        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // 1. 获取当前用户
            var user = context.HttpContext.User; 
            // 1. 先判断 user 对象本身是否存在
            if (user == null)
            {
                return;
            }

            // 2. 再判断 Identity 属性是否存在
            if (user.Identity == null)
            {
                return;
            }

            // 3. 最后判断是否已认证
            if (!user.Identity.IsAuthenticated)
            {
                return;
            }

            // 2. 从 JWT 中提取 jti
            var jti = user.FindFirstValue(JwtRegisteredClaimNames.Jti);
            if (string.IsNullOrEmpty(jti))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
             
            // 3. 检查 Redis 中是否存在该会话
            var sessionKey =  $"token:session:{jti}" ;
            if (!await _redis.KeyExistsAsync(sessionKey))
            { 
                context.Result = new UnauthorizedObjectResult($@"{user.GetUserId()}+{user.GetUserName()}+会话已过期，请重新登录");
                return;
            }
            // 如果存在，不做任何操作，继续执行
        }
    }
}
