using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
namespace Emperia.Items.Sets.Hardmode.Lightning
{
    public class FaradsRipper : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Farad's Ripper");
			Tooltip.SetDefault("High chance of inflicting electrified\n10 defense penetration");
		}


        public override void SetDefaults()
        {
            item.damage = 33;
            item.useTime = 25;
            item.useAnimation = 25;
            item.melee = true;            
            item.width = 32;              
            item.height = 32;             
            item.useStyle = 1;        
            item.knockBack = 3.75f;
            item.value = 258000;
            item.crit = 6;
            item.rare = 4;
            item.UseSound = SoundID.Item1;   
            item.autoReuse = true;
            item.useTurn = true;
            item.shoot = 2;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(2) != 0)
            target.AddBuff(mod.BuffType("ElecHostile"), 240);
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.lightningSet)
                modPlayer.lightningDamage += damage;
                 
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 226);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0f;

            }
        }
        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            if (target.defense >= 10)
            {
                damage += 5;
            }
            else
            {
                damage += target.defense / 2;
            }
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.lightningSet)
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, 0, mod.ProjectileType("LightningSetEffect"), 25, knockBack, player.whoAmI);
            return false;

        }


        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Muramasa, 1);
            recipe.AddIngredient(ItemID.BladeofGrass, 1);
            recipe.AddIngredient(ItemID.FieryGreatsword, 1);
            recipe.AddIngredient(ItemID.BloodButcherer, 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
    }
}
