using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories.Gauntlets
{
    public class ScalyGauntlet : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scaly Gauntlet");
			Tooltip.SetDefault("While not in the ocean, you become 'Enraged'\nThis state makes you deal increased damage and have increased flight time and move speed ");
		}
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.rare = 6;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.accessory = true;
			Item.GetGlobalItem<GItem>().gauntletPower = 0.30f; //temp
		}
        public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
        {
			if (!player.ZoneBeach)
			{
				//player.thrownDamage += 0.10f;
				player.GetDamage(DamageClass.Melee) += 0.10f;
				player.GetDamage(DamageClass.Summon) += 0.10f;
				player.GetDamage(DamageClass.Magic) += 0.10f;
				player.GetDamage(DamageClass.Ranged) += 0.10f;
				player.moveSpeed += 0.25f;
				player.wingTimeMax = (int) (player.wingTimeMax * 1.1f);
			}
        }
    }
}
