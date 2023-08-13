using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

public class Growable
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
