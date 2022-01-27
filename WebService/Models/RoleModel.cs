using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserModel> Users { get; set; }
        public RoleModel()
        {
            Users = new List<UserModel>();
        }
    }
}
