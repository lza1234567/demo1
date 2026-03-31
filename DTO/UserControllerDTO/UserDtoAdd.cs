using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserControllerDTO
{
    public  class UserDtoAdd
    {   
        public string Name { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public int Gender { get; set; }

        public long DepartmentId { get; set; }
    }
}
