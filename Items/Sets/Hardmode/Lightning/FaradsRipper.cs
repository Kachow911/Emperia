using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles.Lightning;
using Emperia.Buffs;

namespace Emperia.Items.Sets.Hardmode.Lightning
{
    public class FaradsRipper : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Farad's Ripper");
			// Tooltip.SetDefault("High chance of inflicting electrified\n10 defense penetration");
		}


        public override void SetDefaults()
        {
            Item.damage = 33;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.DamageType = DamageClass.Melee;            
            Item.width = 32;              
            Item.height = 32;             
            Item.useStyle = 1;        
            Item.knockBack = 3.75f;
            Item.value = 258000;
            Item.crit = 6;
            Item.rare = 4;
            Item.UseSound = SoundID.Item1;   
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.shoot = 2;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.Next(2) != 0)
            target.AddBuff(ModContent.BuffType<ElecHostile>(), 240);
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.lightningSet)
                modPlayer.lightningDamage += damageDone;
                 
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 226);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0f;

            }
        }
        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.ArmorPenetration += 5;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.lightningSet)
                Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0, 0, ModContent.ProjectileType<LightningSetEffect>(), 25, knockBack, player.whoAmI);
            return false;

        }


        /*public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Muramasa, 1);
            recipe.AddIngredient(ItemID.BladeofGrass, 1);
            recipe.AddIngredient(ItemID.FieryGreatsword, 1);
            recipe.AddIngredient(ItemID.BloodButcherer, 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
            
        }*/
    }
}
