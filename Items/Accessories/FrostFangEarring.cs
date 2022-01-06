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
		Item.width = 18;
		Item.height = 32;
		Item.value = 400000;
		Item.rare = 5;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
	{
		MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
		modPlayer.frostFang = true;
		
	}
}}
