using HarmonyLib;
using UnityEngine;

namespace ExoticNS
{
    public class Exotic : Mod
    {
        [HarmonyPatch(typeof(BlueprintGrowth), "PopulateSubprints")]
        [HarmonyPrefix]
        public static void BlueprintGrowth__PopulateSubprints_Prefix(GameDataLoader loader, List<BlueprintGrowth.Growable> ___growables)
        {
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
            {
                WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.CookingIdea, "exotic_blueprint_luxury_fruit_salad", 1);
                WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.CookingIdea, "exotic_blueprint_pineapple", 1);
                WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.CookingIdea, "exotic_blueprint_alive_truffle", 1);
                WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.AdvancedFood, "exotic_pineapple", 1);
                WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.AdvancedFood, "exotic_mango", 1);
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
                fishbag.BaitId = "raw_fish"; // What Food will always drop {chance.Id} from fishing

                //Step 4: Add our BaitBag (fishbag) to the FishTrap.BaitBags

                trap.BaitBags.Add(fishbag); // Add our BaitBag (fishbag) to the FishTrap.BaitBags
            } // Fish

            Harvestable harvestable = (Harvestable)WorldManager.instance.GetCardPrefab("forest");
            CardBag bag = harvestable.MyCardBag;
            bag.Chances.Add(new CardChance("exotic_alive_truffle", 1));
            
            CardData card = WorldManager.instance.GetCardPrefab("bone");
            card.descriptionOverride = "Some say it has magical properties within!";
        }
    }
}