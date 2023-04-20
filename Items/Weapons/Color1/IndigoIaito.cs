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
    public class IndigoIaito : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Indigo Iaito");
			// Tooltip.SetDefault("Striking an enemy will increase life regeneration briefly, longer on bosses");
		}
        public override void SetDefaults()
        {
            Item.damage = 37;
            Item.DamageType = DamageClass.Melee;
            Item.width = 34;
            Item.height = 40;
            Item.useTime = 24;
            Item.useAnimation = 24;     
            Item.useStyle = 1;
            Item.knockBack = 2f;  
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
			recipe.AddIngredient(ItemID.Amethyst, 8); 
			recipe.AddIngredient(ItemID.VioletHusk, 1); 
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            

        }
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(5) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 62);
			}
		}
		 public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		 {
            if (target.boss)
			    player.AddBuff(ModContent.BuffType<IndigoInertia>(), Main.rand.Next(360, 600) + 300);
            else
                player.AddBuff(ModContent.BuffType<IndigoInertia>(), Main.rand.Next(360, 600));
        }
    }
}
