using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Seashell
{
    public class SeashellTome : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lexiconch");
			Tooltip.SetDefault("Fires a powerful magic cerith, but only one can be airborne at a time");
		}
        public override void SetDefaults()
        {
            item.damage = 23;
            item.magic = true;
            item.noMelee = true;
            item.width = 32;
            item.height = 32;
            item.useTime = 20;
            item.useAnimation = 20;     
            item.useStyle = 5;    
            item.mana = 6;
	        item.UseSound = SoundID.Item39;
            item.knockBack = 3.25f;
            item.value = 16500;        
            item.rare = 1;
	        item.shoot = mod.ProjectileType("Cerith"); 
	        item.shootSpeed = 4f;
            item.autoReuse = true;
            item.useTurn = true;        
        }
		public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < 250; ++i)
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
