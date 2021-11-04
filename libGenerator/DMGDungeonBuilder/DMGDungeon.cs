using System;
using System.Collections.Generic;
using System.Text;

namespace libGenerator.DMGDungeonBuilder
{
    public class DMGDungeon
    {
        /*
         * 1 Generate a chamber (starting or not)
         * For each passage or door off that chamber, keep generating passages and chambers until another chamber results.
         * Generate chamber purpose (given type or general)
         * Generate chamber contents/hazards/traps/monster purposes
         */

        public DMGDungeon(int maxChambers, PurposeType type)
        {
            this.maxChambers = maxChambers;
            adjacencyMatrix = new int[maxChambers, maxChambers];
            Purpose = type;
            CreateChambers();
        }

        private void CreateChambers()
        {
            var r = new Random();
            int i = 0;
            while (i < maxChambers)
            {
                var chamber = new DMGChamber(i, Purpose);
                var adjacents = new int[maxChambers];
                foreach (var exit in chamber. Exits)
                {
                    if (exit.FurtherContinuation != ContinuationType.Terminus)
                    {
                        var x = r.Next(0, maxChambers);
                        adjacents[x] = 1;
                    }
                }

                if (chamberHasConnections(adjacents, chamber.Id))
                {
                    chambers.Add(chamber);
                    for (int j=0; j < maxChambers; j++)
                    {
                        var isAdjacent = adjacents[j];
                        if (isAdjacent == 1)
                        {
                            adjacencyMatrix[i, j] = isAdjacent;
                            adjacencyMatrix[j, i] = isAdjacent;
                        }
                    }
                    i += 1;
                }
            }
        }

        public IEnumerable<DMGChamber> Chambers { get => chambers; }
        public PurposeType Purpose { get; }
        public HashSet<int> AdjacencyFor(int id)
        {
            var output = new HashSet<int>();
            for (int j=0; j < maxChambers; j++)
            {
                if (adjacencyMatrix[id, j] == 1) { output.Add(j); }
            }
            return output;
        }

        private bool chamberHasConnections(int[] adjacency, int id)
        {
            for (int i=0; i < adjacency.Length; i++)
            {
                if (adjacency[i] == 1 && i != id)
                {
                    return true;
                }
            }
            return false;
        }

        private readonly int maxChambers;
        private readonly List<DMGChamber> chambers = new List<DMGChamber>();
        private readonly int[,] adjacencyMatrix;
    }
}
