using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqMethods
{
    internal class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public String Email { get; set; }
        public string Phone { get; set; }
        public String City { get; set; }
        public List<Product> PurchasedProducts { get; set; }
        public String PrintProducts()
        {
            String products = "";
            foreach (var product in PurchasedProducts)
            {
                products += product.ToString();
                products += "\n";
            }
            return products;
        }
        public override String ToString()
        {
            return $"Id {Id}, Name {Name}, Email {Email}, Phone {Phone}, City{City}\nProducts:\n{PrintProducts()}";
        }
    }
}
