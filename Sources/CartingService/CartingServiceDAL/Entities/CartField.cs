using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingServiceDAL.Entities
{
    public class CartField
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }
    }
}
