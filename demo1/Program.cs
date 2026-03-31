
using demo1.Uitl;
using Microsoft.EntityFrameworkCore;

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
            builder.Services.AddSwaggerGen();

            #region Database 
            // ? 4. 自动注册数据库（核心！）
            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                // ? 2. 自动获取连接字符串
                var connectionString = builder.Configuration.GetConnectionString("Default");
                options.UseSqlServer(connectionString);
            });

            #endregion


            builder.Services.AddAutoMapper(typeof(Program));
            // 或传入你放置 MappingProfile 的程序集：
            // builder.Services.AddAutoMapper(typeof(MappingProfile));








            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
