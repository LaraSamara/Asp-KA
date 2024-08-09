using EFTaskTwo_CRUD.Data;
using EFTaskTwo_CRUD.Models;

namespace EFTaskTwo_CRUD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext context = new ApplicationDbContext();
           
            // add data to product table
            List<Product> products = new List<Product>() {
            new Product() { Name = "Lenovo-Labtop", Price = 5000},
            new Product() { Name = "Victus-Labtop", Price = 6000 },
            new Product() { Name = "Apple-Ipad", Price = 4500 },
            new Product() { Name = "Iphone 13", Price = 4000 },
            new Product() { Name = "Iphone 15", Price = 6000 }};
            context.Products.AddRange(products);
            context.SaveChanges();
            
            // add data to order table 
            List<Order> orders = new List<Order>()
            {
                new Order(){Address = "Jenin"},
                new Order(){Address = "Ramallah"},
                new Order(){Address = "Nablus"},
                new Order(){Address = "Qalqilia"},
                new Order(){Address = "Nablus"},
                new Order(){Address = "Jenin"},
            };
            context.Orders.AddRange(orders);
            context.SaveChanges();
            
         
            // get all products
            var productsList = context.Products.ToList();
            foreach (var product in productsList)
            {
                Console.WriteLine($"Product Id is: {product.Id}, Name is: {product.Name}, Price is: {product.Price}");
            }
            
            // get all orders
            var ordersList = context.Orders.ToList();
            foreach(var order in ordersList)
            {
                Console.WriteLine($"Order Id is: {order.Id}, Address is: {order.Address}, Created AT: {order.CreatedAt}");
            }
            
            // update product name
            var productUpdates =context.Products.FirstOrDefault(prod => prod.Name == "Victus-Labtop");
            if( productUpdates != null)
            {
                productUpdates.Name = "Victus";
                context.SaveChanges();
            }
            
            // update order created at
            var orderUpdated = context.Orders.FirstOrDefault(ord => ord.Address == "Jenin");
            if (orderUpdated != null)
            {
                orderUpdated.CreatedAt = DateTime.Now;
                context.SaveChanges();
            }
            
            // remove product with id 2
            var removedProduct = context.Products.First(prod => prod.Id == 2);
            context.Products.Remove(removedProduct);
            context.SaveChanges();
            
            // remove order with id 3
           var RemovedOrder = context.Orders.First(ord => ord.Id == 3);
            context.Orders.Remove(RemovedOrder);
            context.SaveChanges();
           
            
        }
    }
}
