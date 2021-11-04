using libGenerator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace libGenerator.DMGDungeonBuilder
{
    public class DMGChamber
    {
        public int Id { get; private set; }
        public DMGChamber(int id, PurposeType type)
        {
            generator = new Random();
            Exits = new List<IContinuation>();
            Id = id;
            IsStarting = id == 0;
            if (IsStarting)
            {
                var roll = generator.Next(1, 11); //d10
                var size = startingSizeData[roll];
                Description = size.Item1;
                Exits.Add(new Terminus("Dungeon Entrance"));
                for (int i=0; i < size.Item2 + size.Item3; i++)
                {
                    var location = exitLocations.Match(generator.NextDouble());
                    Exits.Add(new DMGPassage(location));
                }
            }
            else
            {
                Tuple<string, bool> size = getStandardDescription(generator.NextDouble());
                Description = size.Item1;
                MakeExits(size.Item2);
            }
            SetPurpose(type);
        }

        public void SetPurpose(PurposeType type)
        {
            ChamberPurpose = purpose.GetChamberPurpose(type);
            ChamberCondition = purpose.GetChamberState();
        }

        public string ChamberPurpose { get; private set; }
        public string ChamberCondition { get; private set; }
        public string ChamberContents { get => contentGenerator.Contents; }
        public string Obstacle { get => contentGenerator.Obstacle; }
        public string Trap { get => contentGenerator.Trap; }
        public string Hazard { get => contentGenerator.Hazard; }

        private DMGContents contentGenerator = new DMGContents();
        private DMGDungeonPurpose purpose = new DMGDungeonPurpose();

        private void MakeExits(bool isLarge)
        {
            int exits;
            if (isLarge)
            {
                exits = largeExitCounts.Match(generator.NextDouble());
            }
            else
            {
                exits = normalExitCounts.Match(generator.NextDouble());
            }
            for (int i = 0; i < exits; i++)
            {
                var location = exitLocations.Match(generator.NextDouble());
                Exits.Add(new DMGPassage(location));
            }
        }

   
        private Tuple<string, bool> getStandardDescription(double roll)
        {
            var size = chamberSizes.Match(roll);
            var description = chamberDescriptions.Match(roll);

            return new Tuple<string, bool>(description, size == "large" || size == "large_2"); //total hack
        }

        public string Description { get; set; }
        public List<IContinuation> Exits { get; set; }
        public bool IsStarting { get; private set; }

        private static Dictionary<int, Tuple<string, int, int>> startingSizeData = new Dictionary<int, Tuple<string, int, int>> {
            {1, new Tuple<string, int, int>("Square, 20 x 20 ft, passage on each wall.", 3, 0) },
            {2, new Tuple<string, int, int>("Square, 20 x 20 ft, door on two walls, passage in 3rd wall.", 0, 2) },
            {3, new Tuple<string, int, int>("Sqaure, 40 x 40 ft, doors on three walls.", 0, 2) },
            {4, new Tuple<string, int, int>("Rectangle, 80 x 20 ft, with a row of pillars down the middle, two passages leading from each long wall, doors on each short wall.", 3, 2) },
            {5, new Tuple<string, int, int>("Rectangle, 20 x 40 ft, passage on each wall.", 3, 0) },
            {6, new Tuple<string, int, int>("Circle, 40ft diameter, one passage at each cardinal direction.", 3, 0) },
            {7, new Tuple<string, int, int>("Circle, 40ft diameter, one passage in each cardinal direction, well in center which may lead to lower level.", 3, 0) },
            {8, new Tuple<string, int, int>("Sqauare, 20 x 20 ft, door on two walls, passage on 3rd wall, secret door on fourth wall.", 0, 3) },
            {9, new Tuple<string, int, int>("Passage, 10 ft wide, T intersection.", 2, 0) },
            {10, new Tuple<string, int, int>("Passage, 10 ft wide, four-way intersection.", 3, 0) }
        };

        private Random generator;

        private static WeightedChoiceSet<int> normalExitCounts = new WeightedChoiceSet<int>(new Dictionary<int, double>
        {
            {0, 0.25 },
            {1, 0.3 },
            {2, 0.2 },
            {3, 0.15 },
            {4, 0.1 }
        });

        private static WeightedChoiceSet<int> largeExitCounts = new WeightedChoiceSet<int>(new Dictionary<int, double>
        {
            {0, 0.15 },
            {1, 0.25 },
            {2, 0.25 },
            {3, 0.2 },
            {4, 0.05 },
            {5, 0.05 },
            {6, 0.05 }
        });

        private readonly WeightedChoiceSet<string> chamberDescriptions = new WeightedChoiceSet<string>(new Dictionary<string, double>
        {
            {"Square, 20 x 20 ft.", 0.1 },
            {"Square, 30 x 30 ft.", 0.1 },
            {"Square, 40 x 40 ft.", 0.1 },
            {"Rectangle, 20 x 30 ft.", 0.15 },
            {"Rectangle, 30 x 40 ft.", 0.15 },
            {"Rectangle, 40 x 50 ft.", 0.1 },
            {"Rectangle, 50 x 80 ft.", 0.05 },
            {"Circle, 30 ft diameter.", 0.05 },
            {"Circle, 50 ft diameter.", 0.05 },
            {"Octagon, 40 x 40 ft.", 0.05 },
            {"Octagon, 60 x 60 ft.", 0.05 },
            {"Trapezoid, roughly 40 x 60 ft.", 0.05 }
        });

        private readonly WeightedChoiceSet<string> chamberSizes = new WeightedChoiceSet<string>(new Dictionary<string, double>
        {
            {"small", 0.6 },
            {"large", 0.15 },
            {"small_2", 0.05 },
            {"large_2", 0.2 }
        });

        private readonly WeightedChoiceSet<string> exitLocations = new WeightedChoiceSet<string>(new Dictionary<string, double>
        {
            {"on wall opposite entrance", 0.35 },
            {"on wall left of entrance", 0.25 },
            {"on wall right of entrance", 0.25 },
            {"on same wall as entrance", 0.15 }
        });

        private readonly WeightedChoiceSet<string> exitTypes = new WeightedChoiceSet<string>(new Dictionary<string, double>
        {
            {"Passage leading to dead end", 1 },
            {"Passage leading to another chamber", 5 }
        });
    }
}
