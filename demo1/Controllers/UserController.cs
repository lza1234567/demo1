
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using demo1.Mode;
using demo1.Uitl;
using DTO.UserControllerDTO;
using Microsoft.AspNetCore.Mvc;

namespace demo1.Controllers
{ 

    [Route("api/[controller]")]
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
            Users user = new Users();
            user = _mapper.Map<Users>(userDtoAdd); 
            user.CreateId = id;
            user.Id = userid;
            DbContext.Set<Users>().Add(user);
            await DbContext.SaveChangesAsync();
            return Ok("完成输入！");
        } 
    }
}
