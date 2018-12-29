using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories
 {
public class AncientPelt : ModItem
{
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ancient Pelt");
			Tooltip.SetDefault("Nearby enemies become chilled and frostburned\nYou deal 5% more damage in the Ice Biome");
		}
	public override void SetDefaults()
	{
		item.width = 38;
		item.height = 44;
		item.value = 50000;
		item.expert = true;
		item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (player.ZoneSnow)
		{
			player.meleeDamage += 0.05f;
			player.magicDamage += 0.05f;
			player.rangedDamage += 0.05f;
			player.thrownDamage += 0.05f;
		}
		MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
		modPlayer.ancientPelt = true;
		
	}

}}