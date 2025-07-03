using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Innocalc.Utility
{
    class FileSearcher
    {
        public static float Search(int val, string file)
        {
            var Dict = JsonSerializer.Deserialize<Dictionary<int,float>>(File.ReadAllText("Data\\"+file));
            var keys = Dict.Keys.ToList();
            keys.Sort();
            if (val < keys[0])
                return Dict[keys[0]];
            else if (val > keys[^1])
                return Dict[keys[^1]];
            else if (Dict.ContainsKey(val))
                return Dict[val];
            else
            {
                int lowerKey = keys.Where(k => k < val).Max();
                int upperKey = keys.Where(k => k > val).Min();

                float lowerValue = Dict[lowerKey];
                float upperValue = Dict[upperKey];

                float interpolatedValue = lowerValue + (upperValue - lowerValue) * (val - lowerKey) / (upperKey - lowerKey);
                return interpolatedValue;
            }
        }
    }
}
