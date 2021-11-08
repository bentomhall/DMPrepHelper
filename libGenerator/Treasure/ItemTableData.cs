using libGenerator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libGenerator.Treasure
{
    public class ItemTableData
    {
        public string TableLabel { get; set; } = "";
        public List<TreasureItem> Entries { get; set; } = new List<TreasureItem>();

        private WeightedChoiceSet<TreasureItem> choices;
        public TreasureItem GetItem(double x)
        {
            if (choices == null) {
                var dict = new Dictionary<TreasureItem, double>();
                Entries.ForEach(v => dict.Add(v, v.Weight));
                choices = new WeightedChoiceSet<TreasureItem>(dict); 
            }
            return choices.Match(x);
        }
    }

    public class TreasureItem : IEquatable<TreasureItem>, IComparable<TreasureItem>
    {
        public string Rarity { get; set; } = "Uncommon";
        public bool IsMajor { get; set; } = false;
        public string Name { get; set; } = "";
        public double Weight { get; set; } = 1;
        public List<string> SubTypes { get; set; } = new List<string>();
        public string Description
        {
            get
            {
                string subType = null;
                if (SubTypes.Count > 0) { subType = random.Choice(SubTypes); }
                string output = Name;
                if (subType != null) { output += $" ({subType}, {Rarity})"; }
                else { output += $" ({Rarity})"; }
                return output;
            }
        }

        private Random random = new Random();

        public bool Equals(TreasureItem other)
        {
            return this.Name == other.Name;
        }

        public int CompareTo(TreasureItem other)
        {
            if (other == null) { return 1; }
            return Name.CompareTo(other.Name);
        }

        public static bool operator > (TreasureItem lhs, TreasureItem rhs)
        {
            return lhs.CompareTo(rhs) > 0;
        }

        public static bool operator < (TreasureItem lhs, TreasureItem rhs)
        {
            return lhs.CompareTo(rhs) < 0;
        }
    }
}
