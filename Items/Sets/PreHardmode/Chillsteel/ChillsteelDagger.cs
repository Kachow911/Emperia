using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Chillsteel
{
	public class ChillsteelDagger : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chillsteel Dagger");
			Tooltip.SetDefault("Pierces one enemy, inflicting 'Crushing Freeze'");
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
            item.shoot = mod.ProjectileType("ChillDaggerProj");
            item.useAnimation = 25;
            item.useTime = 25;
            item.shootSpeed = 8.0f;
            item.damage = 16;
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