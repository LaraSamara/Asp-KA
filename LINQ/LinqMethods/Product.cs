
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqMethods
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        public override String ToString()
        {
            return $"Id {Id}, Name {Name}, Price {Price}, Stock {Stock}";
        }
    }
}
