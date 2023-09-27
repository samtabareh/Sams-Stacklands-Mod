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
            Debug.LogError("POPULATESUBPRINTS PATCHED.");
        }
        
        public override void Ready()
        {
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.CookingIdea, "exotic_blueprint_luxury_fruit_salad", 1);
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.CookingIdea, "exotic_blueprint_pineapple", 1);
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.CookingIdea, "exotic_blueprint_alive_truffle", 1);
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.AdvancedFood, "exotic_pineapple", 1);
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.AdvancedFood, "exotic_mango", 1);
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.Island_BasicHarvestable, "exotic_coconut_tree", 1);
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.AdvancedFood, "exotic_coconut_tree", 1);
            
            Harvestable forest = (Harvestable)WorldManager.instance.GetCardPrefab("forest");
            CardBag bag = forest.MyCardBag;
            bag.Chances.Add(new CardChance("exotic_alive_truffle", 1));
            
            CardData bone = WorldManager.instance.GetCardPrefab("bone");
            bone.descriptionOverride = "Some say it has magical properties within!";

            Harmony.PatchAll();
        }
    }
}