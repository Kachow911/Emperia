using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;
using Emperia.Buffs;

namespace Emperia.Items.Weapons.Color1
{
    public class Vibrance : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Vibrance");
			// Tooltip.SetDefault("Striking an enemy will increase defense and critical hit damage briefly, longer on bosses\nWhen empowered, fires a blade lowering enemy contact damage as your DPS rises");
		}
        public override void SetDefaults()
        {
            Item.damage = 96;
            Item.DamageType = DamageClass.Melee;
            Item.width = 46;
            Item.height = 54;
            Item.useTime = 27;
            Item.useAnimation = 27;     
            Item.useStyle = 1;
            Item.knockBack = 4f;  
			Item.crit = 10;	
            Item.value = 232500;        
            Item.rare = 5;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ModContent.ProjectileType<VibranceProj>();
			Item.shootSpeed = 8f;
			Item.scale = 1f;
            Item.autoReuse = true;
            Item.useTurn = true;               
        }
		
        public override void AddRecipes()
        {
			Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(null, "RougeRapier", 1); 
			recipe.AddIngredient(null, "TitianTalwar", 1); 
			recipe.AddIngredient(null, "PearlyPrism", 1); 
			recipe.AddIngredient(ItemID.SoulofFright, 4); 	
			recipe.AddIngredient(ItemID.AdamantiteBar, 2); 
			recipe.AddIngredient(ItemID.PalladiumBar, 2); 
            recipe.AddTile(TileID.MythrilAnvil); 			
            recipe.Register();
             

        }
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 158);
			}
		}
		 public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
            if (target.boss)
			    player.AddBuff(ModContent.BuffType<VermillionValor>(), Main.rand.Next(420, 600) + 300);
            else
                player.AddBuff(ModContent.BuffType<VermillionValor>(), Main.rand.Next(420, 600));
        }
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Item69, player.Center);
			damage = 54;
			return true;
		}
    }
}
