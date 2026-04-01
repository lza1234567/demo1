
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using demo1.Mode;
using demo1.Uitl;
using DTO.UserControllerDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace demo1.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext DbContext; 
        private readonly IMapper _mapper;
        public UserController(DatabaseContext dbContext,IMapper mapper) 
        { 
            this.DbContext = dbContext;
            this._mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Add(long id,long userid,[FromBody] UserDtoAdd userDtoAdd)
        {
            bool iExsitic = DbContext.Set<Users>().Any(a => a.Id == id);
            if (iExsitic)
            {
                return BadRequest("该ID已存在");
            }
            iExsitic = DbContext.Set<Users>().Any(a => a.Name == userDtoAdd.Name);
            Users user = new Users();
            user = _mapper.Map<Users>(userDtoAdd); 
            user.CreateId = userid;
            user.Id = id;
            DbContext.Set<Users>().Add(user);
            await DbContext.SaveChangesAsync();
            return Ok("完成输入！");
        }
        [HttpPost]
        public async Task<IActionResult> Del(long id)
        {
            var users = await DbContext.Set<Users>().SingleOrDefaultAsync(a => a.Id == id);
            if(users == null)
            {
                return BadRequest("找不到");
            } 
            DbContext.Set<Users>().Remove(users);
            await DbContext.SaveChangesAsync(); 
            return Ok("完成输入！");
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] UsersEditDto usersDto)
        {
            if(usersDto.Id <= 0 )
            {
                return BadRequest("错误操作，请重新刷新");
            }
            var users = await DbContext.Set<Users>().SingleOrDefaultAsync(a => a.Id == usersDto.Id);
            if (users == null)
            {
                return NotFound("找不到");
            }
            _mapper.Map(usersDto, users);
            await DbContext.SaveChangesAsync();
            return Ok("完成修改！");
        }
        [HttpPost]
        public async Task<IActionResult> Find([FromBody] UsersFindDto dto)
        {
            // 1. 构建基础查询（动态添加条件）
            var query = DbContext.Set<Users>().AsNoTracking(); // 只读查询，不跟踪

            if (!string.IsNullOrWhiteSpace(dto.Phone))
                query = query.Where(a => a.Phone == dto.Phone);

            if (!string.IsNullOrWhiteSpace(dto.Name))
                query = query.Where(a => a.Name == dto.Name);

            if (  (dto.Gender == 0 || dto.Gender == 1))
                query = query.Where(a => a.Gender == dto.Gender);

            // 2. 获取总数
            int totalCount = await query.CountAsync();

            // 3. 应用分页（必须排序，假设按 Id 排序）
            var items = await query
                .OrderBy(a => a.Id)
                .Skip((dto.PageIndex - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToListAsync();
            string sql = query.ToQueryString();
            Console.WriteLine(sql);
            // 4. 返回结果
            return Ok(new
            {
                totalCount = totalCount,
                pageIndex = dto.PageIndex,
                pageSize = dto.PageSize,
                items = items
            });
        }
    }
}
