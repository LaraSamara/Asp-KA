using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.Core.Dto_s.Cart
{
    public class CartItemDto
    {
        public int ItemId { get; set; }
        public int Store_id { get; set; }
        public int UnitId { get; set; }
        public double Quantity { get; set; }
    }
}
