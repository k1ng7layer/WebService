using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Entities
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public RoleDTO Role { get; set; }
    }
}
        
