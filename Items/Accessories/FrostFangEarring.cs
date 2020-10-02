using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories
 {
public class FrostFangEarring : ModItem
{
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frost Fang Earring");
			Tooltip.SetDefault("When not moving, summons a frost viper to freeze nearby enemies in their tracks");
		}
	public override void SetDefaults()
	{
		item.width = 18;
		item.height = 32;
		item.value = 400000;
		item.rare = 5;
		item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
		modPlayer.frostFang = true;
		
	}
}}
