using System;
using System.Collections.Generic;
using System.Linq;

namespace LibGenerator.Core
{
    public class WeightedChoiceSet
    {
        public WeightedChoiceSet(Dictionary<string, double> input)
        {
            var currentSum = 0.0;
            foreach (var kvp in input)
            {
                currentSum += kvp.Value;
                table[kvp.Key] = currentSum;
            }
            Normalize();
        }

        public int Count => table.Count();

        public string Match(double r)
        {
            
            return table.First(x => x.Value >= r).Key;
        }

        public void AddOrModify(string key, double value)
        {
            table[key] = value;
            Normalize();
        }

        public void Remove(string key)
        {
            table.Remove(key);
            Normalize();
        }

        private Dictionary<string, double> table = new Dictionary<string, double>();

        private void Normalize()
        {
            if (table.Count == 0) { return; }
            var sum = table.Values.Max();
            
            foreach (var key in table.Keys.ToList())
            {
                table[key] = table[key] / sum;
            }
        }

        public Dictionary<string, double> ToDictionary() => table;
    }

    public static class Extensions
    {
        public static string Choice(this Random r, List<string> lst)
        {
            if (lst.Count == 0)
            {
                return "";
            }
            var index = r.Next(0, lst.Count);
            return lst[index];
        }
    }
}
