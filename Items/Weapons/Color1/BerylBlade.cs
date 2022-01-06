using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Buffs;

namespace Emperia.Items.Weapons.Color1
{
    public class BerylBlade : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Beryl Blade");
			Tooltip.SetDefault("Striking an enemy will increase melee speed briefly, longer on bosses");
		}
        public override void SetDefaults()
        {
            Item.damage = 46;
            Item.DamageType = DamageClass.Melee;
            Item.width = 42;
            Item.height = 42;
            Item.useTime = 32;
            Item.useAnimation = 32;     
            Item.useStyle = 1;
            Item.knockBack = 2.25f;  
            Item.value = 48000;        
            Item.rare = 3;
			Item.scale = 1f;
            Item.autoReuse = true;
			Item.UseSound = SoundID.Item1;
            Item.useTurn = true;             
        }
		
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(null, "Prism", 1); 
			recipe.AddIngredient(ItemID.Emerald, 8); 
			recipe.AddIngredient(ItemID.GreenMushroom, 1); 
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            

        }
		 public override void MeleeEffects(Player player, Rectangle hitbox)
		 {
			if (Main.rand.Next(5) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 61);
			}
		 }
		 public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		 {
            if (target.boss)
                player.AddBuff(ModContent.BuffType<BerylBrutalism>(), Main.rand.Next(360, 600) + 300);
            else
                player.AddBuff(ModContent.BuffType<BerylBrutalism>(), Main.rand.Next(360, 600));
		 }
    }
}
