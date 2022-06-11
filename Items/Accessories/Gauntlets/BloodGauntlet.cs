using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories.Gauntlets
{
    public class BloodGauntlet : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bloodstained Gauntlet");
			Tooltip.SetDefault("Sword damage increased by up to 15% at close range, 30% on large foes\nSword strikes may empower you, letting you heal back the damage taken from one hit by attacking\nAllows you to swing swords while walking backwards");
            //Sword strikes may empower you, allowing you to heal back the damage taken from one hit by attacking
            //Sword strikes may empower you, letting you heal back the damage taken from one hit by attacking
            //Sword strikes may let you heal back the damage taken from your next hit by attacking
            //Sword strikes may let you lifesteal back the damage taken from one hit
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.rare = 4;
            Item.value = 280000;
            Item.accessory = true;
            Item.GetGlobalItem<GItem>().gauntletPower = 0.30f;
        }
        public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
        {
			player.GetModPlayer<MyPlayer>().bloodGauntlet = true;
            player.GetModPlayer<MyPlayer>().wristBrace = true;
        }

    }
}
