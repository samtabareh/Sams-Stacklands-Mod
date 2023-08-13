using System;
using System.Collections.Generic;
using System.Text;

namespace ExoticNS
{
	public class BlueprintGrowth : Blueprint
	{
		private class Growable
		{
			public string ToGrow;

			public string StatusTerm;

			public string ResultItem;

			public int ResultCount;

			public float GrowSpeed;

			public string ResultAction = "";

			public Growable(string toGrow, string statusTerm, string resultItem, int resultCount, float growSpeed)
			{
				ToGrow = toGrow;
				StatusTerm = statusTerm;
				ResultItem = resultItem;
				ResultCount = resultCount;
				GrowSpeed = growSpeed;
			}
		}

		private List<Growable> growables = new List<Growable>();

		private string[] growMethods = new string[5] { "soil", "poop", "garden", "farm", "greenhouse" };

		private float[] growSpeedMultiplier = new float[5] { 1f, 1f, 0.75f, 0.5f, 0.5f };

		public override void Init(GameDataLoader loader)
		{
			growables.Clear();
			//Exotic©
			growables.Add(new Growable("exotic_pineapple", "exotic_pineapple_grow", "exotic_pineapple", 2, 120f));
			PopulateSubprints(loader);
			base.Init(loader);
		}

		private void PopulateSubprints(GameDataLoader loader)
		{
			Subprints.Clear();
			for (int i = 0; i < growables.Count; i++)
			{
				Growable growable = growables[i];
				if (loader.GetCardFromId(growable.ToGrow, throwError: false) == null || loader.GetCardFromId(growable.ResultItem, throwError: false) == null)
				{
					continue;
				}
				for (int j = 0; j < growMethods.Length; j++)
				{
					string text = growMethods[j];
					List<string> list = new List<string>();
					for (int k = 0; k < growable.ResultCount; k++)
					{
						list.Add(growable.ResultItem);
					}
					Subprints.Add(new Subprint
					{
						RequiredCards = new string[2] { growable.ToGrow, text },
						ExtraResultCards = list.ToArray(),
						StatusTerm = growable.StatusTerm,
						Time = growable.GrowSpeed * growSpeedMultiplier[j],
						ResultAction = growable.ResultAction
					});
				}
			}
		}

		public override void BlueprintComplete(GameCard rootCard, List<GameCard> involvedCards, Subprint print)
		{
			base.BlueprintComplete(rootCard, involvedCards, print);
			if (rootCard.CardData is HeavyFoundation && rootCard.Child != null)
			{
				rootCard = rootCard.Child;
			}
			if (!(rootCard.CardData.Id == "greenhouse"))
			{
				return;
			}
			CardData cardData = allResultCards.FirstOrDefault((CardData c) => growables.Any((Growable x) => x.ToGrow == c.Id));
			if (cardData != null)
			{
				cardData.MyGameCard.BounceTarget = null;
				cardData.MyGameCard.Velocity = null;
				cardData.MyGameCard.SetParent(rootCard);
				allResultCards.Remove(cardData);
				WorldManager.instance.Restack(allResultCards.Select((CardData x) => x.MyGameCard).ToList());
				WorldManager.instance.StackSend(allResultCards[0].MyGameCard, rootCard);
			}
		}

	}
}
