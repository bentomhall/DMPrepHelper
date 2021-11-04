using libGenerator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace libGenerator.DMGDungeonBuilder
{
    public class DMGDoor : IContinuation, IDMGLocation
    {
        public DMGDoor(bool isSecret, bool isFromChamber)
        {
            IsSecret = isSecret;
            IsFromChamber = isFromChamber;
            var generator = new Random();
            var roll = generator.Next(1, 21); //d20
            IsLocked = roll > 10 && roll % 2 == 0; //even rolls above 10 are locked.
            Description = generateDescription(roll);

            BeyondDoor = getBeyondDoor(generator.NextDouble());
            FurtherContinuation = BeyondDoor.FurtherContinuation;
        }

        public IContinuation BeyondDoor { get; private set; }
        public ContinuationType FurtherContinuation { get; private set; }
        public string Description { get; private set; }
        public bool IsSecret { get; private set; }
        public bool IsLocked { get; private set; }

        public bool IsFromChamber { get; private set; }
        public bool IsDoor { get => true; }
        public bool IsChamber { get => false; }
        public bool IsPassage { get => false; }
        
        private string generateDescription(int roll)
        {
            double d = roll / 20;
            var descriptions = new WeightedChoiceSet<string>(new Dictionary<string, double>
            {
                {"Wooden door.", 0.6 },
                {"Stone door.", 0.1 },
                {"Iron door.", 0.1 },
                {"Portcullis.", 0.1 },
                {"Secret door.", 0.1 }
            });
            return descriptions.Match(d);
        }

        private IContinuation getBeyondDoor(double roll)
        {
            var choices = new WeightedChoiceSet<IContinuation>(new Dictionary<IContinuation, double>
            {
                {new DMGPassage("Passage extending 10 ft, then T intersection extending 10 ft to the right and left"), 0.1 },
                {new DMGPassage("Passage extending 20 ft straight ahead"), 0.3 },
                {new Terminus("Chamber"), 0.5 },
                {new DMGStair("Stairs", false), 0.05 },
                {new Terminus("False door with trap"), 0.05 }
            });
            return choices.Match(roll);

        }
    }
}
