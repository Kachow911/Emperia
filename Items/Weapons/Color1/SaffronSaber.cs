using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.Color1
{
    public class SaffronSaber : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Saffron Sabre");
			Tooltip.SetDefault("Striking an enemy will increase movement speed briefly, longer on bosses");
		}
        public override void SetDefaults()
        {
            item.damage = 32;
            item.melee = true;
            item.width = 36;
            item.height = 40;
            item.useTime = 18;
            item.useAnimation = 18;     
            item.useStyle = 1;
            item.knockBack = 2f;  
			item.crit = 2;
            item.value = 48000;        
            item.rare = 3;
			item.scale = 1f;
			item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;                
        }
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(5) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 64);
			}
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "Prism", 1); 
			recipe.AddIngredient(ItemID.Topaz, 8); 
			recipe.AddIngredient(ItemID.YellowMarigold, 1); 
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
		 public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
            if (target.boss)
			    player.AddBuff(mod.BuffType("SaffronSadism"), Main.rand.Next(360, 600) + 300);
            else
                player.AddBuff(mod.BuffType("SaffronSadism"), Main.rand.Next(360, 600));
        }
    }
}
