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

                getObjectListFromJson(promoteList, "PromoteList.json");
                getObjectListFromJson(productList, "ProductList.json");
                getInput(productList);
                
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
 
                result = result + calculate(r, productList);

            }

            foreach (var a in productList)
            {
                result = result + (a.price * a.amount);
            }

         
            return result;
        }

        public void getObjectListFromJson<T>(List<T> result,string filePath)
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

        //public List<T> getPromotionList()
        //{
        //    List<T> resultList = new List<T>();

        //    getJson(resultList,"PromoteList.json");
 
        //    return resultList;
        //}

        public void InsertItemToList<T>(List<T> List, int num, T item)
        {
            for (int i = 0; i < num; i++)
            {

                List.Add(item);
            }
        }


        public void getInput(List<Product> productCategory)
        {
            List<Product> resultList = new List<Product>();



            foreach (var t in productCategory)
            {
                string no = "";
                int number = 0;
                while (!int.TryParse(no, out number))
                {
                    Console.WriteLine($"Enter no. of product {t.name}:");
                    no = Console.ReadLine();
                }
                t.amount = number;
                //productCategory.Add(new Product(t.name, t.price, number));

                //InsertItemToList(resultList, number, new Product(t.name, t.price));
 
            }
 

        }

        public decimal calculate(Promotion rule, List<Product> productlist)
        {
            var itemlist = rule.item.Split("|");
            var dict = new Dictionary<string, int>();
            var ToBeRemovedList = new Dictionary<string, int>();

            Array.Sort(itemlist);
            //get the promotion rule item list, e.g: 'a',3
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



            //loop the rule item list, calculate the list of item to be removed in product list"
            foreach (var a in dict)
            {
                var item = productlist.Find(n => n.name == a.Key);

                if (item.amount == 0)
                {
                    return 0;
                }

                ToBeRemovedList.Add(a.Key, item.amount/a.Value);
            }
            //we have to choose the minimum value of item because the number of the promotion price can be applied will depend on it
            //for example: a = 5, b = 2 ,rule = ab,
            //the number of the promotion can be applied = number of b in product list (2) / the number of b in the rule (1) == 2
            var RemovedKeyValue = ToBeRemovedList.OrderBy(n => n.Value).FirstOrDefault();

            foreach (var a in productlist)
            {
                if (a.amount >0 && ToBeRemovedList.ContainsKey(a.name))
                {
                    a.amount = a.amount - (RemovedKeyValue.Value * dict[RemovedKeyValue.Key]);  
                }
                
            }

         

            return rule.FinalPrice * RemovedKeyValue.Value;

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
