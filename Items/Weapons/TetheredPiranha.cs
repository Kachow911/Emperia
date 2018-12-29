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
			Tooltip.SetDefault("yeah");
		}
		public override void SetDefaults()
		{
			item.damage = 13;
			item.melee = true;
			item.width = 22;
			item.height = 22;
			item.useTime = 45;
			item.useAnimation = 45;
			item.useStyle = 1;
			item.knockBack = 1;
			item.value = 1000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.noUseGraphic = true;
			item.noMelee = true;
			item.shoot = mod.ProjectileType("PiranhaProjectile");
            item.shootSpeed = 10f;
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
