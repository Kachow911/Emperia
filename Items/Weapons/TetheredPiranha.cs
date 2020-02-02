using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons
{
	public class TetheredPiranha : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tethered Piranha");
			Tooltip.SetDefault("Latches onto enemies, dealing rapid damage");
		}
		public override void SetDefaults()
		{
			item.damage = 16;
            item.crit = 4;
			item.melee = true;
			item.width = 22;
			item.height = 22;
			item.useTime = 26;
			item.useAnimation = 26;
			item.useStyle = 5;
			item.knockBack = 1;
			item.value = 1000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.noUseGraphic = true;
			item.noMelee = true;
			item.shoot = mod.ProjectileType("PiranhaProjectile");
            item.shootSpeed = 10.5f;
			item.channel = true;
        }
		
		public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
	}
}
