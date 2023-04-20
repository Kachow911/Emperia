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
    public class CeruleanClaymore : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cerulean Claymore");
			// Tooltip.SetDefault("Striking an enemy will increase melee damage briefly, longer on bosses");
		}
        public override void SetDefaults()
        {
            Item.damage = 41;
            Item.DamageType = DamageClass.Melee;
            Item.width = 44;
            Item.height = 44;
            Item.useTime = 28;
            Item.useAnimation = 28;     
            Item.useStyle = 1;
            Item.knockBack = 2.25f;  
			Item.crit = 2;
            Item.value = 48000;        
            Item.rare = 3;
			Item.UseSound = SoundID.Item1;
			Item.scale = 1f;
            Item.autoReuse = false;
            Item.useTurn = true;            
        }
		
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(null, "Prism", 1); 
			recipe.AddIngredient(ItemID.Sapphire, 8); 
			recipe.AddIngredient(ItemID.BlueBerries, 1); 
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            

        }
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(5) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 59);
			}
		}
		 public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
            if (target.boss)
                player.AddBuff(ModContent.BuffType<CeruleanCharge>(), Main.rand.Next(360, 600) + 300);
            else
                 player.AddBuff(ModContent.BuffType<CeruleanCharge>(), Main.rand.Next(360, 600));
		}
    }
}
