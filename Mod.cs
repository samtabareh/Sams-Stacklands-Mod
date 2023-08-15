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
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.CookingIdea, "exotic_blueprint_luxury_fruit_salad", 1);
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.CookingIdea, "exotic_blueprint_pineapple", 1);
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.CookingIdea, "exotic_blueprint_alive_truffle", 1);
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.AdvancedFood, "exotic_pineapple", 1);
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.AdvancedFood, "exotic_mango", 1);
            Harvestable forest = (Harvestable)WorldManager.instance.GetCardPrefab("forest");
            CardBag bag = forest.MyCardBag;
            bag.Chances.Add(new CardChance("exotic_alive_truffle", 1));
        }
    }
}