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
    public class TitianTalwar : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Titian Talwar");
			// Tooltip.SetDefault("Striking an enemy will increase defense briefly, longer on bosses");
		}
        public override void SetDefaults()
        {
            Item.damage = 45;
            Item.DamageType = DamageClass.Melee;
            Item.width = 42;
            Item.height = 50;
            Item.useTime = 33;
            Item.useAnimation = 33;     
            Item.useStyle = 1;
            Item.knockBack = 2.85f;  
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
			recipe.AddIngredient(ItemID.Amber, 8); 
			recipe.AddIngredient(ItemID.OrangeBloodroot, 1); 
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            

        }
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(5) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 158);
			}
		}
		 public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
            if(target.boss)
			    player.AddBuff(ModContent.BuffType<TitianTyranny>(), Main.rand.Next(360, 600) + 300);
            else
                player.AddBuff(ModContent.BuffType<TitianTyranny>(), Main.rand.Next(360, 600));
        }
    }
}
