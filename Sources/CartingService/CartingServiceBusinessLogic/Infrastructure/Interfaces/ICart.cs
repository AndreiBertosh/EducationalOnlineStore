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

        public List<CartItem> Items { get; }

        public int AddToItems(CartItem entity);

        public bool RemoveItem(CartItem entity);
    }
}
