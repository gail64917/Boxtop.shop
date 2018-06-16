using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxTop.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Product_Category> BoundedWith { get; set; }

        public Category(string categoryName)
        {
            this.CategoryName = categoryName;
        }

        public Category() { }
    }
}
