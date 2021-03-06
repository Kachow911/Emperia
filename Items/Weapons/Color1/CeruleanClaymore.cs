using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.Color1
{
    public class CeruleanClaymore : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cerulean Claymore");
			Tooltip.SetDefault("Striking an enemy will increase melee damage briefly, longer on bosses");
		}
        public override void SetDefaults()
        {
            item.damage = 41;
            item.melee = true;
            item.width = 44;
            item.height = 44;
            item.useTime = 28;
            item.useAnimation = 28;     
            item.useStyle = 1;
            item.knockBack = 2.25f;  
			item.crit = 2;
            item.value = 48000;        
            item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.scale = 1f;
            item.autoReuse = false;
            item.useTurn = true;            
        }
		
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "Prism", 1); 
			recipe.AddIngredient(ItemID.Sapphire, 8); 
			recipe.AddIngredient(ItemID.BlueBerries, 1); 
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(5) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 59);
			}
		}
		 public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
            if (target.boss)
                player.AddBuff(mod.BuffType("CeruleanCharge"), Main.rand.Next(360, 600) + 300);
            else
                 player.AddBuff(mod.BuffType("CeruleanCharge"), Main.rand.Next(360, 600));
		}
    }
}
