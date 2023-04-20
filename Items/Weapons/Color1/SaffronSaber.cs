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
    public class SaffronSaber : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Saffron Sabre");
			// Tooltip.SetDefault("Striking an enemy will increase movement speed briefly, longer on bosses");
		}
        public override void SetDefaults()
        {
            Item.damage = 32;
            Item.DamageType = DamageClass.Melee;
            Item.width = 36;
            Item.height = 40;
            Item.useTime = 18;
            Item.useAnimation = 18;     
            Item.useStyle = 1;
            Item.knockBack = 2f;  
			Item.crit = 2;
            Item.value = 48000;        
            Item.rare = 3;
			Item.scale = 1f;
			Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;                
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
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(null, "Prism", 1); 
			recipe.AddIngredient(ItemID.Topaz, 8); 
			recipe.AddIngredient(ItemID.YellowMarigold, 1); 
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            

        }
		 public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
            if (target.boss)
			    player.AddBuff(ModContent.BuffType<SaffronSadism>(), Main.rand.Next(360, 600) + 300);
            else
                player.AddBuff(ModContent.BuffType<SaffronSadism>(), Main.rand.Next(360, 600));
        }
    }
}
