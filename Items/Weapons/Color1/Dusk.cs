using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles;
using Emperia.Buffs;

namespace Emperia.Items.Weapons.Color1
{
    public class Dusk : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dusk");
			// Tooltip.SetDefault("Striking an enemy will increase melee damage and life regeneration briefly, longer on bosses\nWhen empowered, fires a Projectile inflicting a burn that worsens as your life rises");
		}
        public override void SetDefaults()
        {
            Item.damage = 126;
            Item.DamageType = DamageClass.Melee;
            Item.width = 48;
            Item.height = 62;
            Item.useTime = 33;
            Item.useAnimation = 33;     
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5.5f;  
            Item.value = 232500;        
            Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ModContent.ProjectileType<DuskProj>();
			Item.shootSpeed = 16f;
			Item.scale = 1f;
            Item.autoReuse = true;
            Item.useTurn = true;                
        }
		
        public override void AddRecipes()
        {
			Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(null, "CeruleanClaymore", 1); 
			recipe.AddIngredient(null, "IndigoIaito", 1); 
			recipe.AddIngredient(null, "PearlyPrism", 1); 
			recipe.AddIngredient(ItemID.SoulofMight, 4); 	
			recipe.AddIngredient(ItemID.CobaltBar, 2); 
			recipe.AddIngredient(ItemID.OrichalcumBar, 2); 
            recipe.AddTile(TileID.MythrilAnvil); 			
            recipe.Register();
             

        }
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.VilePowder);
			}
		}
		 public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
            if (target.boss)
                player.AddBuff(ModContent.BuffType<IndigoIntensity>(), Main.rand.Next(420, 600) + 300);
            else
                player.AddBuff(ModContent.BuffType<IndigoIntensity>(), Main.rand.Next(420, 600));
		}
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			damage = damage / 2;
		}
    }
}
