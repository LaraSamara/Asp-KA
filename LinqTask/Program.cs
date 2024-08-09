<<<<<<< HEAD
﻿namespace LinqTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Product Labtop = new Product()
            {
                Id = 1,
                Name = "Labtop",
                Price = 3000,
                Stock = 100
            };
            Product Ipad = new Product()
            {
                Id = 2,
                Name = "Ipad",
                Price = 2500,
                Stock = 200
            };
            Product Phone = new Product()
            {
                Id = 3,
                Name = "Phone",
                Price = 6000,
                Stock = 20
            };
            Product Watch = new Product()
            {
                Id = 4,
                Name = "Watch",
                Price = 1500,
                Stock = 60
            };
            List<Customer> Customers = new List<Customer>() {
                new Customer()
                {
                    Id = 1,
                    Name = "Lara",
                    Email = "larasamara2002@gmai.com",
                    Phone ="059550615",
                    City = "Jenin",
                    PurchasedProducts = new List<Product> { Labtop, Phone }
                },
                new Customer()
                {
                    Id = 2,
                    Name = "Sara",
                    Email = "sarasamara@gmai.com",
                    Phone ="059550616",
                    City = "Jenin",
                    PurchasedProducts = new List<Product>{ Phone, Watch }
                },
                new Customer()
                {
                    Id = 3,
                    Name = "Sali",
                    Email = "salisamara@gmai.com",
                    Phone ="059550617",
                    City = "Nablus",
                    PurchasedProducts = new List < Product > { Ipad, Watch }
                },
                new Customer()
                {
                    Id = 4,
                    Name = "Eva",
                    Email = "Evasamara@gmai.com",
                    Phone ="059550618",
                    City = "Nablus",
                    PurchasedProducts = new List < Product > { Ipad, Watch, Labtop }
                },
                new Customer()
                {
                    Id = 5,
                    Name = "Khali",
                    Email = "Khalilsamara@gmai.com",
                    Phone ="059550634",
                    City = "Ramallah",
                    PurchasedProducts = new List < Product > { Ipad, Watch, Labtop, Phone }
                }
            };
            //"Nablus" كيف يمكنك الحصول على العملاء الذين يعيشون في مدينة 
            var One = Customers.Where(customer => customer.City.Equals("Nablus")).ToList();
            // كيف يمكنك استخراج أسماء جميع العملاء من قائمة العملاء؟
            var Two = Customers.Select(customer => customer.Name).ToList();
            // كيف يمكنك ترتيب العملاء بحسب أسمائهم بترتيب تصاعدي؟
            var Three = Customers.OrderBy(customer => customer.Name).ToList();
            // كيف يمكنك الحصول على أول عميل في القائمة؟
            var Four = Customers.Take(1).ToList();
            // ؟id = 1 كيف يمكنك الحصول على العميل الذي يحمل
            var Five = Customers.Where(customer => customer.Id == 1).ToList();
            // كيف يمكنك حساب مجموع أسعار المنتجات التي اشتراها جميع العملاء؟
            var Sex = Customers.SelectMany(customer => customer.PurchasedProducts).Sum(product => product.Price);
            // ؟"Qalqilia" كيف يمكنك التحقق مما إذا كان هناك أي عميل يعيش في مدينة
            var Seven = Customers.Any(customer => customer.City.Equals("Qalqilia"));
            // كيف يمكنك تجميع العملاء حسب مدينتهم؟
            var Eight = Customers.GroupBy(customer => customer.City).ToList();
            // كيف يمكنك الحصول على أول ثلاثة عملاء من قائمة العملاء؟
            var Nine = Customers.Take(3).ToList();
        }

     }
}
=======
﻿namespace LinqTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Product Labtop = new Product()
            {
                Id = 1,
                Name = "Labtop",
                Price = 3000,
                Stock = 100
            };
            Product Ipad = new Product()
            {
                Id = 2,
                Name = "Ipad",
                Price = 2500,
                Stock = 200
            };
            Product Phone = new Product()
            {
                Id = 3,
                Name = "Phone",
                Price = 6000,
                Stock = 20
            };
            Product Watch = new Product()
            {
                Id = 4,
                Name = "Watch",
                Price = 1500,
                Stock = 60
            };
            List<Customer> Customers = new List<Customer>() {
                new Customer()
                {
                    Id = 1,
                    Name = "Lara",
                    Email = "larasamara2002@gmai.com",
                    Phone ="059550615",
                    City = "Jenin",
                    PurchasedProducts = new List<Product> { Labtop, Phone }
                },
                new Customer()
                {
                    Id = 2,
                    Name = "Sara",
                    Email = "sarasamara@gmai.com",
                    Phone ="059550616",
                    City = "Jenin",
                    PurchasedProducts = new List<Product>{ Phone, Watch }
                },
                new Customer()
                {
                    Id = 3,
                    Name = "Sali",
                    Email = "salisamara@gmai.com",
                    Phone ="059550617",
                    City = "Nablus",
                    PurchasedProducts = new List < Product > { Ipad, Watch }
                },
                new Customer()
                {
                    Id = 4,
                    Name = "Eva",
                    Email = "Evasamara@gmai.com",
                    Phone ="059550618",
                    City = "Nablus",
                    PurchasedProducts = new List < Product > { Ipad, Watch, Labtop }
                },
                new Customer()
                {
                    Id = 5,
                    Name = "Khali",
                    Email = "Khalilsamara@gmai.com",
                    Phone ="059550634",
                    City = "Ramallah",
                    PurchasedProducts = new List < Product > { Ipad, Watch, Labtop, Phone }
                }
            };
            //"Nablus" كيف يمكنك الحصول على العملاء الذين يعيشون في مدينة 
            var One = Customers.Where(customer => customer.City.Equals("Nablus")).ToList();
            // كيف يمكنك استخراج أسماء جميع العملاء من قائمة العملاء؟
            var Two = Customers.Select(customer => customer.Name).ToList();
            // كيف يمكنك ترتيب العملاء بحسب أسمائهم بترتيب تصاعدي؟
            var Three = Customers.OrderBy(customer => customer.Name).ToList();
            // كيف يمكنك الحصول على أول عميل في القائمة؟
            var Four = Customers.Take(1).ToList();
            // ؟id = 1 كيف يمكنك الحصول على العميل الذي يحمل
            var Five = Customers.Where(customer => customer.Id == 1).ToList();
            // كيف يمكنك حساب مجموع أسعار المنتجات التي اشتراها جميع العملاء؟
            var Sex = Customers.SelectMany(customer => customer.PurchasedProducts).Sum(product => product.Price);
            // ؟"Qalqilia" كيف يمكنك التحقق مما إذا كان هناك أي عميل يعيش في مدينة
            var Seven = Customers.Any(customer => customer.City.Equals("Qalqilia"));
            // كيف يمكنك تجميع العملاء حسب مدينتهم؟
            var Eight = Customers.GroupBy(customer => customer.City).ToList();
            // كيف يمكنك الحصول على أول ثلاثة عملاء من قائمة العملاء؟
            var Nine = Customers.Take(3).ToList();
        }

     }
}
>>>>>>> 70d0ec8 (Upload EF Tasks)
