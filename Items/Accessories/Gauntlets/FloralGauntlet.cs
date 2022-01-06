using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories.Gauntlets
{
    public class FloralGauntlet : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Floral Gauntlet");
			Tooltip.SetDefault("Attacks have a chance to lifesteal\nIncreased damage and max life");
		}
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.rare = 5;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.accessory = true;
            Item.GetGlobalItem<GItem>().isGauntlet = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
        {
			player.GetModPlayer<MyPlayer>().floralGauntlet = true;
			//player.thrownDamage += 0.08f;
			player.GetDamage(DamageClass.Melee) += 0.08f;
			player.GetDamage(DamageClass.Summon) += 0.08f;
			player.GetDamage(DamageClass.Magic) += 0.08f;
			player.GetDamage(DamageClass.Ranged) += 0.08f;
			player.statLifeMax2 += 75;
        }
    }
}
