using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxTop.Models
{
    public class Product_Category
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public int Strength { get; set; }

        public Product Product { get; set; }
        public Category Category { get; set; }

        public Product_Category(Product product, Category category, int strength)
        {
            this.Product = product;
            this.Category = category;
            this.ProductID = product.ID;
            this.CategoryID = category.ID;
            this.Strength = strength;
        }

        public Product_Category() { }
    }
}
