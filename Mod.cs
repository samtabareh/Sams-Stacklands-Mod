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
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.CookingIdea, "exotic_blueprint_alive_truffle", 1);
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.AdvancedFood, "exotic_pineapple", 1);
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.AdvancedFood, "exotic_alive_truffle", 1);
        }
    }
}