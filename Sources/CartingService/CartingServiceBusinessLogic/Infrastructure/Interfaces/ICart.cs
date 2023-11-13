using CartingServiceBusinessLogic.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingServiceBusinessLogic.Infrastructure.Interfaces
{
    public interface ICart
    {
        public string CartName { get; set; }

        public List<CartEntity> Items { get; }

        public int AddToItems(CartEntity entity);

        public bool RemoveItem(CartEntity entity);
    }
}
