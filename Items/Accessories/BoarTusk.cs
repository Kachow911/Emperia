using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories
{
    public class BoarTusk : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Boar's Tusk");
			Tooltip.SetDefault("6% increased damage and movement speed\nYou lose 5 defense");
		}
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.rare = 3;
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
        {
			if (player.statDefense < 5)
				player.statDefense = 0;
			else 
				player.statDefense -= 5;
			player.moveSpeed *= 1.06f;
			player.GetDamage(DamageClass.Melee) += 0.06f;
			player.GetDamage(DamageClass.Magic) += 0.06f;
			player.GetDamage(DamageClass.Ranged) += 0.06f;
			//player.thrownDamage += 0.06f;
        }
		
    }
}
