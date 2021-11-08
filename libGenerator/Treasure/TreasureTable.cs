using libGenerator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace libGenerator.Treasure
{
    public class TreasureTable
    {
        public int CopperDice { get; set; } = 0;
        public int CopperMultiplier { get; set; } = 100;
        public int SilverDice { get; set; } = 0;
        public int SilverMultiplier { get; set; } = 100;
        public int GoldDice { get; set; } = 0;
        public int GoldMultiplier { get; set; } = 10;
        public int PlatinumDice { get; set; } = 0;
        public int PlatinumMultiplier { get; set; } = 10;
        public List<TreasureTableItem> Items { get; set; } = new List<TreasureTableItem>();
        public int Tier { get; set; } = 0;

        public Tuple<CashReward, List<string>> GenerateTreasure(List<ItemTableData> tables, bool includeItems)
        {
            if (items == null) { ConstructItemSet(); }
            var random = new Random();
            var copper = MathUtil.Roll(CopperDice, 6, random)*CopperMultiplier;
            var silver = MathUtil.Roll(SilverDice, 6, random)*SilverMultiplier;
            var gold = MathUtil.Roll(GoldDice, 6, random)*GoldMultiplier;
            var platinum = MathUtil.Roll(PlatinumDice, 6, random)*PlatinumMultiplier;
            var itemEntry = items.Match(random.NextDouble());
            var gems = $"{MathUtil.Roll(itemEntry.GemDice, itemEntry.GemDiceSize, random)}x{itemEntry.GemValue} gems";
            var cash = new CashReward { Copper = copper, Silver = silver, Gold = gold, Platinum = platinum, Gems = gems };
            var rolledItems = new List<string>();
            if (includeItems)
            {
                var itemRolls = MathUtil.Roll(itemEntry.ItemDiceCount, itemEntry.ItemDiceSize, random);
                for (int i = 0; i < itemRolls; i++ )
                {
                    var item = tables[i].GetItem(random.NextDouble());
                    rolledItems.Add(item.Description);
                }
            }
            return new Tuple<CashReward, List<string>>(cash, rolledItems);
        }

        public struct CashReward
        {
            public int Copper { get; set; }
            public int Silver { get; set; }
            public int Gold { get; set; }
            public int Platinum { get; set; }
            public string Gems { get; set; }

            public string Description { 
                get
                {
                    var output = new List<string>();
                    if (Copper > 0) { output.Add($"{Copper} cp"); }
                    if (Silver > 0) { output.Add($"{Silver} sp"); }
                    if (Gold > 0) { output.Add($"{Gold} gp"); }
                    if (Platinum > 0) { output.Add($"{Platinum} pp"); }
                    if (Gems != "0x0 gems") { output.Add(Gems); }
                    return string.Join(", ", output);
                }
            }
        }

        private WeightedChoiceSet<TreasureTableItem> items;

        private void ConstructItemSet()
        {
            var dict = new Dictionary<TreasureTableItem, double>();
            foreach (var item in Items)
            {
                dict.Add(item, item.Weight);
            }
            items = new WeightedChoiceSet<TreasureTableItem>(dict);
        }
    }

    public class TreasureTableItem
    {
        public int Weight { get; set; } = 1;
        public int GemDice { get; set; } = 0;
        public int GemDiceSize { get; set; } = 6;
        public int GemValue { get; set; } = 0;
        public int ItemDiceCount { get; set; } = 0;
        public int ItemDiceSize { get; set; } = 6;
        public int ItemTableIndex { get; set; } = 0;
    }
}
