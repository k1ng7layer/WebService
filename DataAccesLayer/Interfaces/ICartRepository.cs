using DataAccesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Interfaces
{
    public interface ICartRepository: IRepository<Cart>
    {
        public void RemoveRangeByProductId(int id);
    }
}
