using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace caTest
{
    public class PromotionEngin
    {
        public void start()
        {
            try
            {
                var promoteList = new List<Promotion>();
                var productList = new List<Product>();
                decimal result = 0;

                productList = getInput();
                promoteList = getPromotionList();
                result = calculateResult(productList, promoteList);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        public decimal calculateResult(List<Product> productList, List<Promotion> promotionRule)
        {
            decimal result = 0;
            foreach (var r in promotionRule)
            {
                var itemlist = r.item.Split("|");
                Array.Sort(itemlist);
                result = result + calculate(r, productList);
                //while (FitPromote(itemlist, productList))
                //{
                //    for (int i = 0; i < r.item.Length; i++)
                //    {
                //        var item = productList.Find(n => n.name == r.item[i].ToString());
                //        productList.Remove(item);
                //    }

                //    result += r.FinalPrice;
                //}
            }

            if (productList.Count > 0)
            {
                result = result + productList.Sum(n => n.price);
            }

            return result;
        }

        public void getJson<T>(List<T> result,string filePath)
        {
 

            using (StreamReader file = new StreamReader(filePath))
            {
                try
                {
                    string json = file.ReadToEnd();

                    var serializerSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };

                    var list = JsonConvert.DeserializeObject<IEnumerable<T>>(json, serializerSettings).ToList();

                    foreach (var i in list)
                    {
                        result.Add(i);
                    }
    



                }
                catch (Exception)
                {
                    Console.WriteLine("Problem reading file");
                }

                
            }

        
            
        }

        public List<Promotion> getPromotionList()
        {
            List<Promotion> resultList = new List<Promotion>();

            getJson(resultList,"PromoteList.json");
 
            return resultList;
        }

        public void InsertItemToList<T>(List<T> List, int num, T item)
        {
            for (int i = 0; i < num; i++)
            {

                List.Add(item);
            }
        }


        public List<Product> getInput()
        {
            List<Product> resultList = new List<Product>();

            List<Product> productCategory = new List<Product>();
            getJson(productCategory,"ProductList.json");

            foreach (var t in productCategory)
            {
                string no = "";
                int number = 0;
                while (!int.TryParse(no, out number))
                {
                    Console.WriteLine($"Enter no. of product {t.name}:");
                    no = Console.ReadLine();
                }

                InsertItemToList(resultList, number, new Product(t.name, t.price));
 
            }
             
            return resultList;

        }

        public decimal calculate(Promotion rule, List<Product> productlist)
        {
            var itemlist = rule.item.Split("|");
            var dict = new Dictionary<string, int>();
            var ToBeRemovedList = new Dictionary<string, int>();

            Array.Sort(itemlist);

            foreach (var a in itemlist)
            {
                if (dict.ContainsKey(a))
                {
                     dict[a] = dict[a] + 1;
                }
                else
                {
                    dict.Add(a, 1);
                }
            }

            foreach (var a in dict)
            {
                var item = productlist.FindAll(n => n.name == a.Key);

                if (item.Count == 0)
                {
                    return 0;
                }

                ToBeRemovedList.Add(a.Key, item.Count/a.Value);
            }

            var amount = ToBeRemovedList.OrderBy(n => n.Value).FirstOrDefault().Value;

            

            foreach (var a in ToBeRemovedList)
            {
                for (int k = 0; k < amount * dict[a.Key]; k++)
                {
                    var item = productlist.Find(n => n.name == a.Key);
                    productlist.Remove(item);
                }
            }

            return rule.FinalPrice * amount;

        }

        public bool FitPromote(string[] itemlist, List<Product> productlist)
        {
            bool result = false;
            int cnt = 0;

            for (int k = 0; k < productlist.Count; k++)
            {
                if (productlist[k].name == itemlist[cnt].ToString())
                {
                    cnt++;
                }

                if (cnt == itemlist.Length)
                {
                     return true;
                }
            }
                
 
            return result;
        }

    }
}
