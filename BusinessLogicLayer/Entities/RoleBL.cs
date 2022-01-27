using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Entities
{
    public class RoleBL
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserBL> Users { get; set; }
        public RoleBL()
        {
            Users = new List<UserBL>();
        }
    }
}
