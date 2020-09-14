using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataMiningTest
{
    public static class DataAnalyzer
    {
        public static Dictionary<string, HashSet<string>> BasketData;
        public static Dictionary<string, double> GoodsData;
        
        public static void Analyze()
        {
            var filename = Directory.EnumerateFiles(Directory.GetCurrentDirectory())
                .First(x => Path.GetExtension(x) == ".csv");
            var reader = File.OpenText(filename);
            var data = new Dictionary<string, double>();
            BasketData = new Dictionary<string, HashSet<string>>();
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var str = reader.ReadLine().Split(';');
                if (data.ContainsKey(str[0]))
                    data[str[0]]++;
                else
                    data.Add(str[0], 1);
                if (BasketData.ContainsKey(str[1]))
                    BasketData[str[1]].Add(str[0]);
                else
                    BasketData.Add(str[1], new HashSet<string> {str[0]});
            }
            reader.Close();
            GoodsData = data.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        public static Dictionary<string, double> GetCompanionGoods(string good)
        {
            var result = new Dictionary<string, int>();
            foreach (var basket in BasketData.Values)
                if (basket.Contains(good))
                    foreach (var item in basket)
                        if (result.ContainsKey(item))
                            result[item]++;
                        else
                            result.Add(item, 1);
            double goodBought = GoodsData[good];
            return result.Select(x => new KeyValuePair<string, double>(x.Key, x.Value * 100 / goodBought))
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}