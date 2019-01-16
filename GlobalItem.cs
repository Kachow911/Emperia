using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using Emperia;

namespace Emperia
{
	public class ExampleInstancedGlobalItem : GlobalItem
	{
		public override bool UseItem(Item item, Player player)
        {
			if (item.type == 28 || item.type == 188 || item.type == 499 || item.type == 3544 || item.type == 226 || item.type == 227 || item.type == 3001)
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
		public override void OpenVanillaBag(string context, Player player, int arg)
		{
			if (context == "bossBag" && arg == ItemID.SkeletronBossBag)
			{
				int x = Main.rand.Next(3);
				if (x == 0)
				{
					Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("Skelebow")); 
				}
				else if (x == 1)
				{
					Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("NecromanticFlame")); 
				}
				else if (x == 2)
				{
					Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("BoneWhip")); 
				}
			}
		}
	}
}
