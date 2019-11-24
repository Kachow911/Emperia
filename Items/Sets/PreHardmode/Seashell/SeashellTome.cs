using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Seashell  //where is located
{
    public class SeashellTome : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lexiconch");
			Tooltip.SetDefault("Shoots magic ceriths");
		}
        public override void SetDefaults()
        {
            item.damage = 21;
            item.magic = true;
            item.noMelee = true;
            item.width = 32;
            item.height = 32;
            item.useTime = 26;
            item.useAnimation = 26;     
            item.useStyle = 5;    
            item.mana = 5;
	    item.UseSound = SoundID.Item39;
            item.knockBack = 4f;
            item.value = 16500;        
            item.rare = 1;
	    item.shoot = mod.ProjectileType("Cerith"); 
	    item.shootSpeed = 5f;
            item.autoReuse = false;
            item.useTurn = true;        
        }
		public override bool CanUseItem(Player player)       //this make that you can shoot only 1 boomerang at once
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
