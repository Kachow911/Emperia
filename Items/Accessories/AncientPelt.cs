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
			// DisplayName.SetDefault("Ancient Pelt");
			// Tooltip.SetDefault("Nearby enemies become chilled and frostburned\nYou deal 5% more damage in the Ice Biome");
		}
	public override void SetDefaults()
	{
		Item.width = 38;
		Item.height = 44;
		Item.value = 50000;
		Item.expert = true;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
	{
		if (player.ZoneSnow)
		{
			player.GetDamage(DamageClass.Melee) += 0.05f;
			player.GetDamage(DamageClass.Magic) += 0.05f;
			player.GetDamage(DamageClass.Ranged) += 0.05f;
			//player.thrownDamage += 0.05f;
		}
		MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
		modPlayer.ancientPelt = true;
		
	}

}}
