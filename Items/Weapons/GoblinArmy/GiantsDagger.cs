using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.GoblinArmy
{
	public class GiantsDagger : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Giant's Dagger");
			Tooltip.SetDefault("Sticks in enemies\nEnemies impaled with the dagger bleed, losing life and defense\nEnemies that die with the dagger in them drop more coins");
		}


        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.width = 16;
            item.height = 16;
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item1;
            item.thrown = true;
            item.channel = true;
            item.noMelee = true;
            item.consumable = true;
            item.maxStack = 999;
            item.shoot = mod.ProjectileType("GiantsDaggerProj");
            item.useAnimation = 32;
            item.useTime = 32;
            item.shootSpeed = 8.0f;
            item.damage = 22;
            item.knockBack = 3.5f;
			item.value = Item.sellPrice(0, 0, 1, 50);
            item.crit = 4;
            item.rare = 2;
            item.autoReuse = true;
            item.maxStack = 1;
            item.consumable = false;
        }
    }
}