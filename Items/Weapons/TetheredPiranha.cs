using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

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
			Item.damage = 15;
            Item.crit = 2;
			Item.DamageType = DamageClass.Melee;
			Item.width = 22;
			Item.height = 22;
			Item.useTime = 26;
			Item.useAnimation = 26;
			Item.useStyle = 5;
			Item.knockBack = 1;
			Item.value = 1000;
			Item.rare = 2;
			Item.UseSound = SoundID.Item1;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.shoot = ModContent.ProjectileType<PiranhaProjectile>();
            Item.shootSpeed = 8f;
			Item.channel = true;
        }
		
		public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
	}
}
