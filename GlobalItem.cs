using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using Emperia;

namespace ExampleMod.Items
{
	public class ExampleInstancedGlobalItem : GlobalItem
	{
		public override bool UseItem(Item item, Player player)
        {
			if (item.type == 28 || item.type == 188 || item.type == 499 || item.type == 3544)
			{
				MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
				if (modPlayer.vitalityCrystal)
				{
					player.statLife += 25;
					player.HealEffect(25);
				}
			}
			return false;
		}
	}
}
