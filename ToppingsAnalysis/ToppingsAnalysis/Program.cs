using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToppingsAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            var combinationTable = new Dictionary<string, int>();
          
            using (StreamReader r = new StreamReader(@"..\..\pizzas.json"))
            {
                string json = r.ReadToEnd();
                List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(json);
                foreach (var order in orders)
                {
                    if (!combinationTable.ContainsKey(order.key))
                        combinationTable.Add(order.key, 0);
                    combinationTable[order.key]++;
                }
                var combinationTableList = combinationTable.ToList();
                combinationTableList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
                for(int i =0;i<20;i++)
                {
                    Console.WriteLine(combinationTableList[i].Key + "\t" + combinationTableList[i].Value);
                }
                Console.ReadLine();
            }
            


        }
    }

    class Order
    {
        
        public string[] toppings;
        public string key {
            get {
                if (!_KeyCreated && toppings != null)
                {
                    Array.Sort(toppings);
                    _key = string.Join(" + ", toppings);
                    _KeyCreated = true;
                }
            
                return _key;
            }
        }

        private bool _KeyCreated;
        private string _key;

    }
}
