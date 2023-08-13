using HarmonyLib;
using System;
using System.Collections;
using UnityEngine;

namespace ExoticNS
{
    public class Exotic : Mod
    {
        public override void Ready()
        {
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.CookingIdea, "exotic_blueprint_pineapple", 1);
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.BasicFood, "exotic_pineapple", 1);
        }
        private void Awake()
        {
            this.Harmony.PatchAll(typeof(Patches));
        }
    }

    public static class Patches
    {
        [HarmonyPatch(typeof(BlueprintGrowth), "PopulateSubprints")]
        [HarmonyPrefix]
        public static void stuff(List<Growable> ___growables)
        {
            ___growables.Add(new Growable("pineapple", "exotic_pineapple_growth_status_", "pineapple", 2, 1f));
        }
    }
}