using BoxTop.Functions;
using BoxTop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxTop.Data
{
    public class DbInitializer
    {
        public static void Initialize(DatabaseContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            ///Юзеры
            var users = new User[]
            {
                new User{ Login = "Vova", Password = Hasher.GetHashString("sad"), Role = "Admin"},
                new User{ Login = "Georg", Password = Hasher.GetHashString("happy"), Role = "User"}
            };
            foreach (User s in users)
            {
                context.Users.Add(s);
            }
            context.SaveChanges();


            ///Продукты, категории
            var products = new Product[]
            {
            new Product{ ProductCategory = "Book", ProductName = "Harry Potter", Cost = 2000, CountInBase = 1},
            new Product{ ProductCategory = "ComputerItem" , ProductName = "The Witcher 3", Cost = 2500, CountInBase = 2}
            };
            foreach (Product p in products)
            {
                context.Products.Add(p);
            }
            context.SaveChanges();


            var categories = new Category[]
            {
                new Category{ CategoryName = "For Erudite"},
                new Category{ CategoryName = "For Men"},
                new Category{ CategoryName = "For Woman" }
            };
            foreach (Category c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();

            var product_categories = new Product_Category[]
            {
                new Product_Category() { CategoryID = 1, ProductID = 1, Strength = 10 },
                new Product_Category() { CategoryID = 2, ProductID = 1, Strength = 7 },
                new Product_Category() { CategoryID = 3, ProductID = 1, Strength = 7 },
                new Product_Category() { CategoryID = 1, ProductID = 2, Strength = 3 },
                new Product_Category() { CategoryID = 2, ProductID = 2, Strength = 5 },
                new Product_Category() { CategoryID = 3, ProductID = 2, Strength = 3 }
            };

            foreach (Product_Category pc in product_categories)
            {
                context.Product_Categories.Add(pc);
            }
            context.SaveChanges();

            ///Заказы (пример)
            var orders = new OrderItems[]
            {

                new OrderItems("Geroyev Panfilovcev, 14/53", "Vladimir", 3000, "Во второй половине дня, номер для связи +7925ххххххх") { productsInBox = "продукты1" },
                new OrderItems("Фрязино, пр. Мира, 10/40", "Наталия", 2300, "Перед доставкой позвонить, номер для связи +7926ххххххх") { productsInBox = "продукты2" }
            };

            foreach (OrderItems p in orders)
            {
                context.OrderItems.Add(p);
            }
            context.SaveChanges();
        }
    }
}
