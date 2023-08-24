using HarmonyLib;
using System;
using System.Collections;
using UnityEngine;

namespace ExoticNS
{
    public class Exotic : Mod
    {
        private static Harmony harmony;

        [HarmonyPatch(typeof(BlueprintGrowth), "Init")]
        [HarmonyPostfix]
        public static void BlueprintGrowth__Init_Postfix(BlueprintGrowth forest)
        {
            growables.Add(new Growable("exotic_mango", "exotic_idea_mango_grow", "exotic_mango", 2, 120f));
            growables.Add(new Growable("exotic_pineapple", "exotic_idea_pineapple_grow", "exotic_pineapple", 2, 120f));
            growables.Add(new Growable("exotic_truffle", "exotic_idea_alive_truffle_status", "exotic_alive_truffle", 2, 120f));
        }
        public override void Ready()
        {
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.CookingIdea, "exotic_blueprint_luxury_fruit_salad", 1);
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.CookingIdea, "exotic_blueprint_pineapple", 1);
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.CookingIdea, "exotic_blueprint_alive_truffle", 1);
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.AdvancedFood, "exotic_pineapple", 1);
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.AdvancedFood, "exotic_mango", 1);
            Harvestable forest = (Harvestable)WorldManager.instance.GetCardPrefab("forest");
            CardBag bag = forest.MyCardBag;
            bag.Chances.Add(new CardChance("exotic_alive_truffle", 1));
            CardData bone = WorldManager.instance.GetCardPrefab("bone");
            bone.descriptionOverride = "Some say it has magical properties within!";
            harmony.PatchAll();
            
        }
    }
}