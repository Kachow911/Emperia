    using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.Color1
{
    public class TitianTalwar : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Titian Talwar");
			Tooltip.SetDefault("Striking an enemy will increase defense briefly, longer on bosses");
		}
        public override void SetDefaults()
        {
            item.damage = 45;
            item.melee = true;
            item.width = 42;
            item.height = 50;
            item.useTime = 33;
            item.useAnimation = 33;     
            item.useStyle = 1;
            item.knockBack = 2.85f;  
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
			recipe.AddIngredient(ItemID.Amber, 8); 
			recipe.AddIngredient(ItemID.OrangeBloodroot, 1); 
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(5) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 158);
			}
		}
		 public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
            if(target.boss)
			    player.AddBuff(mod.BuffType("TitianTyranny"), Main.rand.Next(360, 600) + 300);
            else
                player.AddBuff(mod.BuffType("TitianTyranny"), Main.rand.Next(360, 600));
        }
    }
}
