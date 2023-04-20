using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories
 {
public class MycelialShield : ModItem
{
	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mycelial Shield");
			// Tooltip.SetDefault("Critical Hits have a chance to surround you in spores\nWhile these spores are active, damage is increased \nSpores also damage enemies");
		}
	public override void SetDefaults()
	{
		Item.width = 38;
		Item.height = 44;
		Item.value = 50000;
		Item.expert = true;
		Item.defense = 5;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
	{
		MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
		modPlayer.sporeFriend = true;
		player.lifeRegen += 2;
		
	}

}}
