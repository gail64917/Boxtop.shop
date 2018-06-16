using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxTop.Models
{
    public class OrderItems
    {
        public int ID { get; set; }
        public string productsInBox { get; set; }
        public string Adress { get; set; }
        public float Cost { get; set; }
        public string UserInfo { get; set; }
        public string DeliveryInfo { get; set; }

        public OrderItems(string adress, string userInfo, float cost, string deliveryInfo)
        {
            this.Adress = adress;
            this.UserInfo = userInfo;
            this.Cost = cost;
            this.DeliveryInfo = deliveryInfo;
        }

        public OrderItems() { }


        public static string ItemsStringView(Dictionary<Product, int> dict)
        {
            string secondString = string.Join(";", dict.Select(x => x.Key.ProductName + "=" + x.Value).ToArray());
            return secondString;
        }
    }
}
