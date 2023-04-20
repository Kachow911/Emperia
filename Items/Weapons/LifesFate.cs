using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Buffs;
namespace Emperia.Items.Weapons
{
    public class LifesFate : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Life's Fate");
			// Tooltip.SetDefault("Critical strikes will empower the blade and enable you to steal life for a short while");
		}


        public override void SetDefaults()
        {
            Item.damage = 58;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.DamageType = DamageClass.Melee;            
            Item.width = 32;              
            Item.height = 32;             
            Item.useStyle = 1;        
            Item.knockBack = 3.75f;
            Item.value = 258000;
            Item.crit = 6;
            Item.rare = 3;
            Item.UseSound = SoundID.Item1;   
            Item.autoReuse = false;
            Item.scale = 1.15f;
        }

        //MyPlayer player = Main.player[Item.FindOwner(Item)].GetModPlayer<MyPlayer>();
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            /*{
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 5);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0f;

            }*/
            if (Main.rand.Next(4) == 0)
            {
                /*int smoke = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 183, player.direction * 2, 0f, 0, default(Color), 1.4f);
                Main.dust[smoke].noGravity = true;*/
                //int smoke = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 117, player.direction * 2, 0f, 150, default(Color), 1.4f);
                //Main.dust[smoke].noGravity = true;
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 14, player.direction * 2, 0f, 180, new Color(255, 0, 0), 1.2f);
            }
            if (Main.rand.Next(3) > 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 60, player.velocity.X * 0.2f + (player.direction * 3), player.velocity.Y * 0.2f, 0, default(Color), 1.4f);
                Main.dust[dust].noGravity = true;
            }
            else
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 27, player.velocity.X * 0.2f + (player.direction * 3), player.velocity.Y * 0.2f, 0, new Color(255, 0, 0), 1.4f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].noLight = true;
            }
            //Main.dust[dust].velocity.X /= 2f;
            //Main.dust[dust].velocity.Y /= 2f;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (hit.Crit)
            {
                player.AddBuff(ModContent.BuffType<LifesFateBuff>(), Main.rand.Next(840, 960));
            }
            if (modPlayer.renewedLife)
            {
                int x = Main.rand.Next(1, 3);
                player.statLife += x;
                player.HealEffect(x);
            }
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.renewedLife) damage *= 1.15f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BloodButcherer, 1);
            recipe.AddIngredient(ItemID.Muramasa, 1);
            recipe.AddIngredient(ItemID.BladeofGrass, 1);
            recipe.AddIngredient(ItemID.FieryGreatsword, 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
            
        }
    }
}
