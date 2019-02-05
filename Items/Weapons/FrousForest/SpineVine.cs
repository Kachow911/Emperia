using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.FrousForest
{
	public class SpineVine : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spine Vine");
			Tooltip.SetDefault("Sticks in enemies and poisons them");
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
            item.shoot = mod.ProjectileType("SpineVineProj");
            item.useAnimation = 36;
            item.useTime = 36;
            item.shootSpeed = 6.0f;
            item.damage = 24;
            item.knockBack = 3.5f;
			item.value = Item.sellPrice(0, 0, 1, 50);
            item.crit = 4;
            item.rare = 2;
            item.autoReuse = true;
            item.maxStack = 999;
            item.consumable = true;
        }
    }
}