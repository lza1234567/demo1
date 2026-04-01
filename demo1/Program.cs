
using System.Text;
using demo1.Filter;
using demo1.Uitl; 
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

namespace demo1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                // 1. 定义安全方案（Bearer Token）
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "请输入 JWT Token，格式：Bearer {your_token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                // 2. 添加安全要求（让所有接口都带上锁图标，并可在全局应用）
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            #region Database 
            // ? 4. 自动注册数据库（核心！）
            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                // ? 2. 自动获取连接字符串
                var connectionString = builder.Configuration.GetConnectionString("Default");
                options.UseSqlServer(connectionString);
            });

            #endregion

            #region redis

            // Redis 注册
            var redisConn = ConnectionMultiplexer.Connect(builder.Configuration["Redis:ConnectionString"]);
            builder.Services.AddSingleton<IConnectionMultiplexer>(redisConn); 
            #endregion

            #region jwt
            // 读取 JWT 配置
            var jwtKey = builder.Configuration["Jwt:Key"];
            var jwtIssuer = builder.Configuration["Jwt:Issuer"];
            var jwtAudience = builder.Configuration["Jwt:Audience"];

            // 添加认证服务
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };
                });

            builder.Services.AddAuthorization(); // 添加授权服务
            #endregion
            builder.Services.AddAutoMapper(typeof(Program));
            // 或传入你放置 MappingProfile 的程序集：
            // builder.Services.AddAutoMapper(typeof(MappingProfile));
            #region 过滤器
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<RedisSessionFilter>(); // 全局注册
            });
            #endregion
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            // 添加中间件（顺序重要）
            app.UseAuthentication(); // 认证中间件 
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
