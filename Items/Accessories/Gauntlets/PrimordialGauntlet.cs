using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories.Gauntlets
{
    public class PrimordialGauntlet : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Primordial Gauntlet");
			Tooltip.SetDefault("Your attacks have a chance to Deal Double damage\nYour attacks have a chance to fully lifesteal\nWhen you take damage, you gain increased speed, damage and Defense\nAttacks inflict multipile potent debuffs\nEnemies expldoe into damage dealing and healing projectiles");
		}
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.rare = 9;
            Item.value = Item.sellPrice(1, 0, 0, 0);
            Item.accessory = true;
            Item.GetGlobalItem<GItem>().isGauntlet = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
        {
			player.statLifeMax2 += 100;
			player.statDefense += 10;
			player.statManaMax2 += 100;
			player.GetModPlayer<MyPlayer>().floralGauntlet = true;
			//player.GetModPlayer<MyPlayer>().terraGauntlet = true;
            //player.GetModPlayer<MyPlayer>().terraGauntlet2 = Main.Item[Item.NewItem(Player.getRect(), Item.type)];
        }
    }
}
