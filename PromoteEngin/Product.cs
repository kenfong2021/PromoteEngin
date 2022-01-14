using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caTest
{

    [AttributeUsage(AttributeTargets.All)]
    public class HelpAttribute : System.Attribute
    {
        public readonly string Url;

        public string Topic  // Topic 是一个命名（named）参数
        {
            get
            {
                return topic;
            }
            set
            {

                topic = value;
            }
        }

        public HelpAttribute(string url)  // url 是一个定位（positional）参数
        {
            this.Url = url;
        }

        private string topic;
    }

    [HelpAttribute("Information on the class MyClass")]
    public class Product
    {
        public string name { get; set; }
        public decimal price { get; set; }

        public Product(string Name, decimal Price)
        {
            name = Name;
            price = Price;
        }
    }
}
