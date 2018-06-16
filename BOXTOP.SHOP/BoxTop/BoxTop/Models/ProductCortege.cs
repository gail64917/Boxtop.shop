using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxTop.Models
{
    public class ProductCortege
    {
        public string ProductName;
        public Dictionary<string, int> CategoryParameters = new Dictionary<string, int>();
        public float Cost;
        public int Count;

        public ProductCortege() { }
        public ProductCortege(string productName, float cost, int count)
        {
            this.ProductName = productName;
            this.Cost = cost;
            this.Count = count;
        }
    }
}
