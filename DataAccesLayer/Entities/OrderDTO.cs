using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Entities
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string OrderUniqueId { get; set; }
        public DateTime Date { get; set; }
        //public UserDTO User {get;set;}
        
    }
}
