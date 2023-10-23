using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingServiceDAL.Infrastructure.Interfaces
{
    internal interface ICart
    {
        public string CartName { get; set; }

        public List<IEntity> Entities { get; set; }
    }
}
