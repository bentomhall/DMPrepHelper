using libGenerator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace libGenerator.DMGDungeonBuilder
{
    class DMGDungeonPurpose
    {

        public string GetChamberPurpose(PurposeType dungeonType)
        {
            return tables[dungeonType].Match(random.NextDouble());
        }

        public string GetChamberState()
        {
            return chamberState.Match(random.NextDouble());
        }

        private Random random = new Random();

        private readonly Dictionary<PurposeType, WeightedChoiceSet<string>> tables = new Dictionary<PurposeType, WeightedChoiceSet<string>>
        {
            {PurposeType.DeathTrap, new WeightedChoiceSet<string>(new Dictionary<string, double>
            {
                { "Antechamber or waiting room for spectators", 1 },
                { "Guardroom fortified against intruders", 7 },
                { "Vault for holding important treasures, accessible only by locked or secret door (75% chance of being trapped", 3 },
                { "Room containing a puzzle that must be solved to bypass a trap or monster", 3 },
                { "Trap designed to kill or capture creatures", 5 },
                { "Observation room, allowing guards or spectators to observe creatures moving through the dungeon", 1 }
            })},
            {PurposeType.Lair, new WeightedChoiceSet<string>(new Dictionary<string, double>
            {
                { "Armory stocked with weapons and armor", 1 },
                { "Audience chamber, used to receive guests", 1 },
                { "Banquet room for important celebrations", 1 },
                { "Barracks where the lair's defenders are quartered", 1},
                { "Bedroom, for use by leaders", 1 },
                { "Chapel where the lair's inhabitants worship", 1 },
                { "Cistern or well for drinking water", 1 },
                { "Guardroom for the defense of the lair", 2 },
                { "Kennel for pets or guard beasts", 1 },
                { "Kitchen for food storage and preparation", 1 },
                { "Pen or prison where captives are held", 1 },
                { "Storage, mostly nonperishable goods", 2 },
                { "Throne room where the lair's leaders hold court", 1 },
                { "Torture chamber", 1 },
                { "Training and exercise room", 1 },
                { "Trophy room or museum", 1 },
                { "Latrine or bath", 1 },
                { "Workshop for the construction of weapons, armor, tools, and other goods", 1 }
            }) },
            {PurposeType.Maze, new WeightedChoiceSet<string>(new Dictionary<string, double>
            {
                { "Conjuring room, used to summon creatures that guard the maze", 1 },
                { "Guardroom for sentinels that patrol the maze", 4 },
                { "Lair for guard beasts that patrol the maze", 5 },
                { "Pen or prison accessible only by secret door, used to hold captives condemned to the maze", 1 },
                { "Shrine dedicated to a god or other entity", 1 },
                { "Storage for food, as well as tools used by the maze's guardians to keep the complex in working order", 2 },
                { "Trap to confound or kill those sent into the maze", 4 },
                { "Well that provides drinking water", 1 },
                { "Workshop where doors, torch sconces, and other furnishings are repaired and maintained", 1 }
            }) },
            { PurposeType.Mine, new WeightedChoiceSet<string>(new Dictionary<string, double>
            {
                { "Barracks for miners", 2 },
                { "Bedroom for a supervisor or manager", 1 },
                { "Chapel dedicated to a patron deity of miners, earth, or protection", 1 },
                { "Cistern providing drinking water for miners", 1 },
                { "Guardroom", 2 },
                { "Kitchen used to feed workers", 1 },
                { "Laboratory used to conduct tests on strange minerals extracted from the mine", 1 },
                { "Lode where metal ore is mined (75 percent chance of being depleted)", 6 },
                { "Office used by the mine supervisor", 1 },
                { "Smithy for repairing damaged tools", 1 },
                { "Storage for tools and other equipment", 2 },
                { "Strong room or vault used to store ore for transport to the surface", 1 }
            }) },
            { PurposeType.PlanarGate, new WeightedChoiceSet<string>(new Dictionary<string, double>
            {
                {"Decorated foyer or antechamber", 3 },
                {"Armory used by the portal's guardians", 5 },
                {"Audience chamber for receiving visitors", 2 },
                {"Barracks used by the portal's guards", 9 },
                {"Bedroom for use by the high-ranking members of the order that guards the portal", 4 },
                {"Chapel dedicated to a deity or deities related to the portal and its defenders", 6 },
                {"Cistern providing fresh water", 5 },
                {"Classroom for use of initiates learning about the portal's secrets", 3 },
                {"Conjuring room for summoning creatures used to investigate or defend the portal", 1 },
                {"Crypt where the remains of those that died guarding the portal are kept", 2 },
                {"Dining room", 6 },
                {"Divination room used to investigate the portal and events tied to it", 3 },
                {"Dormitory for visitors and guards", 5 },
                {"Entry room or vestibule", 2 },
                {"Gallery for displaying trophies and objects related to the portal and those that guard it", 2 },
                {"Guardroom to protect or watch over the portal", 8 },
                {"Kitchen", 5 },
                {"Laboratory for conducting experiments relating to the portal and creatures that emerge from it", 5 },
                {"Library holding books about the portal's history", 3 },
                {"Pen or prison holding captives or creatures that emerge from the portal", 5 },
                {"Planar junction, where the gate to another plane once stood (25 percent chance of being active)", 2 },
                {"Storage", 3 },
                {"Strong room or vault, for guarding valuable treasures connected to the portal or funds used to pay the planar gate's guardians", 1 },
                {"Study", 2 },
                {"Torture chamber, for questioning creatures that pass through the portal or that attempt to clandestinely use it", 1 },
                {"Latrine or bath", 4 },
                {"Workshop for constructing tools and gear needed to study the portal", 2 }

            })},
            { PurposeType.TreasureVault, new WeightedChoiceSet<string>(new Dictionary<string, double>{
                {"Antechamber for visiting dignitaries", 1 },
                {"Armory containing mundane and magic gear used by the treasure vault's guards", 1 },
                {"Barracks for guards", 2 },
                {"Cistern providing fresh water", 1 },
                {"Guardroom to defend against intruders", 4 },
                {"Kennel for trained beasts used to guard the treasure vault", 1 },
                {"Kitchen for feeding guards", 1 },
                {"Watch room that allows guards to observe those who approach the dungeon", 1 },
                {"Prison for holding captured intruders", 1 },
                {"Strong room or vault, for guarding the treasure hidden in the dungeon, accessible only by locked or secret door", 2 },
                {"Torture chamber for extracting information from captured intruders", 1 },
                {"Trap or other trick designed to kill or capture creatures that enter the dungeon", 3 }
            })},
            {PurposeType.General, new WeightedChoiceSet<string>(new Dictionary<string, double>
            {
                {"Antechamber", 1 },
                {"Armory", 2 },
                {"Audience chamber", 1 },
                {"Aviary", 1 },
                {"Banquet Room", 2 },
                {"Barracks", 3 },
                {"Bath or latrine", 1 },
                {"Bedroom", 1 },
                {"Bestiary", 1 },
                {"Cell", 3 },
                {"Chantry", 1 },
                {"Chapel", 1 },
                {"Cistern", 2 },
                {"Classroom", 1 },
                {"Closet", 1 },
                {"Conjuring room", 2 },
                {"Court", 2 },
                {"Crypt", 3 },
                {"Dining room", 2 },
                {"Divination room", 2 },
                {"Dormitory", 1 },
                {"Dressing room", 1 },
                {"Entry room or vestibule", 1 },
                {"Gallery", 2 },
                {"Game room", 2 },
                {"Guardroom", 3 },
                {"Hall", 2 },
                {"Hall, great", 2 },
                {"Hallway", 2 },
                {"Kennel", 1 },
                {"Kitchen", 2 },
                {"Laboratory", 2 },
                {"Library", 3 },
                {"Lounge", 2 },
                {"Meditation chamber", 1 },
                {"Observatory", 1 },
                {"Office", 1 },
                {"Pantry", 2 },
                {"Pen or prison", 2 },
                {"Reception room", 2 },
                {"Refectory", 2 },
                {"Robing room", 1 },
                {"Salon", 1 },
                {"Shrine", 2 },
                {"Sitting room", 2 },
                {"Smithy", 2 },
                {"Stable", 1 },
                {"Storage room", 2 },
                {"Strong room or vault", 2},
                {"Study", 2 },
                {"Temple", 3 },
                {"Throne room", 2 },
                {"Torture chamber", 1 },
                {"Training or exercise room", 2 },
                {"Trophy room or museum", 2 },
                {"Waiting room", 1 },
                {"Nursery or schoolroom", 1 },
                {"Well", 1 },
                {"Workshop", 2 }
            }) },
            {PurposeType.Stronghold, new WeightedChoiceSet<string>(new Dictionary<string, double>
            {
                {"stuff", 100 }
            }) },
            {PurposeType.TempleOrShrine, new WeightedChoiceSet<string>(new Dictionary<string, double>
            {
                {"stuff", 100 }
            }) },
            {PurposeType.Tomb, new WeightedChoiceSet<string>(new Dictionary<string, double>
            {
                {"stuff", 100 }
            }) },
        };

        private readonly WeightedChoiceSet<string> chamberState = new WeightedChoiceSet<string>(new Dictionary<string, double>
        {
            {"Rubble, ceiling partially collapsed", 2 },
            {"Holes, floor partially collapsed", 2 },
            {"Ashes, contents mostly burned", 2 },
            {"Used as a campsite", 2 },
            {"Pool of water, chamber's original contents are water damaged", 2 },
            {"Furniture wrecked but still present", 5 },
            {"Converted to some other use (roll on the General Dungeon Chambers table)", 2 },
            {"Stripped bare", 1 },
            {"Pristine and in original state", 1 }
        });
    }

    public enum PurposeType
    {
        DeathTrap,
        Lair,
        Maze,
        Mine,
        PlanarGate,
        Stronghold,
        TempleOrShrine,
        Tomb,
        TreasureVault,
        General
    }
}
