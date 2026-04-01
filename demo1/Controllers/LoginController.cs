using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Azure.Core;
using demo1.Mode;
using demo1.Uitl;
using demo1.Uitl.JWT; 
using DTO.LoginControllerDTO;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace demo1.Controllers
{
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {

        private readonly DatabaseContext DbContext;
        private readonly IMapper _mapper; 
        private readonly IDatabase _redis; 
        public LoginController(DatabaseContext dbContext, IMapper mapper, IConnectionMultiplexer redis)
        {
            this.DbContext = dbContext;
            this._mapper = mapper;
            this._redis = redis.GetDatabase();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto,[FromServices] IConfiguration config)
        {
            var users =  DbContext.Set<Users>().SingleOrDefault(a => a.Name == dto.name);
            if(users == null)
            {
                return NotFound("用户不存在");
            }
            if(users.Password != dto.password)
            {
                return Unauthorized("用户名或密码错误");
            }
            #region 生成 JWT  求时间长度ttl，JWTID 
            // 1. 生成 JWT 
            var token =  JwtHelper.GenerateToken(config, users.Id, dto.name );
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);///ReadJwtToken 将token解析一下成为jwt对象
            var JWTID = jwtToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value;///JWT ID（jti） 之前生成jwt 存储的数据
            var ttl = jwtToken.ValidTo - DateTime.UtcNow;///距离过期还有多长时间  过期时间，时间长度（ValidTo） 
            var sessionData = new { users.Id, dto.name };//, roles };
            var jsonData = System.Text.Json.JsonSerializer.Serialize(sessionData);
            #endregion  

            #region 存储会话到 Redis，key = session:{JWTID} 
            var oldJwtID = await _redis.StringGetAsync($"token:userID:{users.Id}");
            var oldJwt = await _redis.StringGetAsync($"token:session:{oldJwtID}");
            if (!string.IsNullOrEmpty(oldJwt))
            {
                await _redis.KeyDeleteAsync($"token:session:{oldJwtID}");
            }
            await _redis.StringSetAsync($"token:userID:{users.Id}", JWTID, ttl);
            await _redis.StringSetAsync($"token:session:{JWTID}", jsonData, ttl);

            #endregion 
            return Ok(new { token=token, users });  
        }
        [HttpGet]
        public async Task<IActionResult> Showtoken()
        {
            long userid = User.GetUserId();
            if (userid == 0) 
            {
                return Unauthorized("无法获取用户信息");
            }
            Users users =await  DbContext.Set<Users>().FindAsync(userid);
            if(users == null)
            {
                return NotFound("未找到");
            }
            return Ok(users);
        }
        [HttpGet]
        public async Task<IActionResult> ShowRedis()
        {
            long userid = User.GetUserId();
            if (userid == 0)
            {
                return Unauthorized("无法获取用户信息");
            }
            Users users = await DbContext.Set<Users>().FindAsync(userid);
            var oldRedisId = await _redis.StringGetAsync($"user:{users.Id}:NowJWTID");
            if (!string.IsNullOrEmpty(oldRedisId)) 
            {
                return NotFound("未找到");
            }
            return Ok($"key：user:{users.Id}:NowJWTID            value：{oldRedisId}" );
        }
    }
}
