using HarmonyLib;
using SimplyBetterFoodsNS;
using UnityEngine;

namespace ExoticNS
{
    public class Exotic : Mod
    {
        public static Exotic exotic = new Exotic();
        public static SimplyBetterFoods simply = new SimplyBetterFoods();
        public static WorldManager world = new WorldManager();

        [HarmonyPatch(typeof(BlueprintGrowth), "PopulateSubprints")]
        [HarmonyPrefix]
        public static void BlueprintGrowth__PopulateSubprints_Prefix(GameDataLoader loader, List<BlueprintGrowth.Growable> ___growables)
        {
            Debug.LogWarning($"{exotic.Manifest.Name} Patch Applied.");
            ___growables.Add(new BlueprintGrowth.Growable("exotic_mango", "exotic_mango_grow", "exotic_mango", 2, 120f));
            ___growables.Add(new BlueprintGrowth.Growable("exotic_coconut", "exotic_coconut_tree_grow", "exotic_coconut_tree", 1, 120f));
            ___growables.Add(new BlueprintGrowth.Growable("exotic_pineapple", "exotic_pineapple_grow", "exotic_pineapple", 2, 120f));
            ___growables.Add(new BlueprintGrowth.Growable("exotic_truffle", "exotic_idea_alive_truffle_status", "exotic_alive_truffle", 2, 120f));
        }
        private void Awake()
        {
            Harmony.PatchAll(typeof(Exotic));
        }
        public override void Ready()
        {
            foreach (Mod mod in ModManager.LoadedMods)
            {
                for (int i = 0; i < exotic.Manifest.Dependencies.Count; i++)
                {
                    if (mod.Manifest.Id == exotic.Manifest.Dependencies[i])
                        Debug.LogWarning($"Exotic Co. Dependency [Name: {mod.Manifest.Name}, Id: {mod.Manifest.Id}] Is Loaded.");
                }
                for (int i = 0; i < exotic.Manifest.OptionalDependencies.Count; i++)
                {
                    if (mod.Manifest.Id == exotic.Manifest.OptionalDependencies[i])
                        Debug.LogWarning($"Exotic Co. Optional Dependency [Name: {mod.Manifest.Name}, Id: {mod.Manifest.Id}] Is Loaded.");
                }
            }

            {
                WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.CookingIdea, "exotic_blueprint_luxury_fruit_salad", 1);
                WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.CookingIdea, "exotic_blueprint_pineapple", 1);
                WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.CookingIdea, "exotic_blueprint_alive_truffle", 1);
                WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.CookingIdea, "exotic_blueprint_truffle_caviar", 1);
                WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.AdvancedFood, "exotic_blueprint_luxury_plate", 1);
                WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.Island_AdvancedFood, "exotic_blueprint_luxury_plate", 1);
                WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.Island_CookingIdea, "exotic_blueprint_truffle_caviar", 1);
                WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.Island_CookingIdea, "exotic_blueprint_shrimp_sushi", 1);
                WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.Island_CookingIdea, "exotic_blueprint_plate_seafood", 1);
                WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.AdvancedFood, "exotic_pineapple", 1);
                WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.AdvancedFood, "exotic_mango", 1);
                WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.Island_AdvancedFood, "exotic_alive_truffle", 1);
                WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.Island_BasicHarvestable, "exotic_coconut_tree", 1);
                WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.AdvancedFood, "exotic_coconut_tree", 1);
            } // Bags
            {
                //Step 1: Make the Vars

                FishTrap trap = (FishTrap)WorldManager.instance.GetCardPrefab("fish_trap"); //Get the Fish Trap
                BaitBag fishbag = new BaitBag(); // Make A BaitBag.
                CardChance chance = new CardChance(); // Make A CardChance

                //Step 2: Edit the CardChance

                chance.Id = "exotic_shrimp"; // The ID of the fish you want to drop from fishing
                chance.MaxCountToGive = 1; // Max amount of fish to drop from fishing
                chance.PercentageChance = 1; // chance of which card to choose from (we only have 1 card tho so just set it to 1)

                //Step 3: Edit our BaitBag (fishtrap) and add our CardChance (chance) to our BaitBag (fishbag)

                fishbag.CardsInPack = 1; // How many cards in 1 BaitBag
                fishbag.Chances = new List<CardChance>(); fishbag.Chances.Add(chance);// Add our CardChance (chance) to our BaitBag (fishbag)
                fishbag.BaitId = "seaweed"; // What Food will always drop {chance.Id} from fishing

                //Step 4: Add our BaitBag (fishbag) to the FishTrap.BaitBags

                trap.BaitBags.Add(fishbag); // Add our BaitBag (fishbag) to the FishTrap.BaitBags
            } // Fish Trap
            {
                Animal Eel = (Animal)WorldManager.instance.GetCardPrefab("eel");
                CardChance ec = new CardChance(); // eel chance
                List<CardChance> EC = Eel.Drops.Chances; // Eel Chances
                ec.Id = "exotic_caviar";
                ec.PercentageChance = .75f;
                EC.Add(ec);
            } // Eel
            {
                Animal Cod = (Animal)WorldManager.instance.GetCardPrefab("cod");
                CardChance cc = new CardChance(); // cod chance
                List<CardChance> CC = Cod.Drops.Chances; // Cod Chances
                cc.Id = "exotic_caviar";
                cc.PercentageChance = .5f;
                CC.Add(cc);
            } // Cod

            Harvestable forest = (Harvestable)WorldManager.instance.GetCardPrefab("forest");
            CardBag forestbag = forest.MyCardBag;
            forestbag.Chances.Add(new CardChance("exotic_alive_truffle", 1));

            CardData bone = WorldManager.instance.GetCardPrefab("bone");
            bone.descriptionOverride = "Some say it has magical properties within!";
            SimplyBetterFoodsNS.FoodEffects.CBDebuff
        }
    }
}