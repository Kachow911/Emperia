using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.Color1
{
    public class IndigoIaito : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Indigo Iaito");
			Tooltip.SetDefault("Striking an enemy will increase life regeneration briefly, longer on bosses");
		}
        public override void SetDefaults()
        {
            item.damage = 37;
            item.melee = true;
            item.width = 34;
            item.height = 40;
            item.useTime = 24;
            item.useAnimation = 24;     
            item.useStyle = 1;
            item.knockBack = 2f;  
            item.value = 48000;        
            item.rare = 3;
			item.scale = 1f;
            item.autoReuse = true; 
			item.UseSound = SoundID.Item1;
            item.useTurn = true;       
        }
		
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "Prism", 1); 
			recipe.AddIngredient(ItemID.Amethyst, 8); 
			recipe.AddIngredient(ItemID.VioletHusk, 1); 
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(5) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 62);
			}
		}
		 public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		 {
            if (target.boss)
			    player.AddBuff(mod.BuffType("IndigoInertia"), Main.rand.Next(360, 600) + 300);
            else
                player.AddBuff(mod.BuffType("IndigoInertia"), Main.rand.Next(360, 600));
        }
    }
}
