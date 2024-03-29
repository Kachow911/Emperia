using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using static Terraria.Audio.SoundEngine;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles;
using Emperia.Buffs;

namespace Emperia.Items.Weapons
{
    public class HarpoonBlade : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Harpoon Blade");
			// Tooltip.SetDefault("Fires up to seven powerful harpoons that pierce into enemies\nStrike an enemy to dislodge harpoons for extra damage\nSwinging the blade with all harpoons fired will reel you in");
		}
        public override void SetDefaults()
        {
            Item.damage = 44; //47 for slight buff
            Item.DamageType = DamageClass.Melee;
            Item.width = 48;
            Item.height = 48;
            Item.useTime = 30;
            Item.useAnimation = 30;     
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5f;  
            Item.value = 232500;        
            Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<HarpoonBladeProj>();
            Item.shootSpeed = 14f;
			Item.scale = 1f;
            Item.autoReuse = false;
            //Item.reuseDelay = 60;
            //Item.useTurn = true;                
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Harpoon);
            recipe.AddIngredient(ItemID.Cutlass);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            //maybe make this halfway through the swing in meleeeffects?
            int hooksOut = 0;
            NPC sameLatchedNpcCheck = null;
            bool failedCheck = false;
            for (int l = 0; l < 1000; l++)
            {
                if (Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && Main.projectile[l].type == ModContent.ProjectileType<HarpoonBladeProj>())
                {
                    hooksOut++;
                    if (Main.projectile[l].GetGlobalProjectile<GProj>().latchedNPC != sameLatchedNpcCheck && sameLatchedNpcCheck != null
                    || Main.projectile[l].GetGlobalProjectile<GProj>().latchedNPC == null) failedCheck = true;
                    sameLatchedNpcCheck = Main.projectile[l].GetGlobalProjectile<GProj>().latchedNPC;
                }
            }
            if (hooksOut == 6) Terraria.Audio.SoundEngine.PlaySound(SoundID.Item149, player.Center);
            if (hooksOut >= 7 && !failedCheck)
            {
                for (int l = 0; l < 1000; l++)
                {
                    if (Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && Main.projectile[l].type == ModContent.ProjectileType<HarpoonBladeProj>())
                    {
                        (Main.projectile[l].ModProjectile as HarpoonBladeProj).ReelToggle(player);
                    }
                }
            }
            if (hooksOut >= 7) return false;
            else
            {

                PlaySound(new SoundStyle("Emperia/Sounds/Custom/Chain") with { MaxInstances = 0 }, player.Center);
                //PlaySound(SoundID.Coins, player.Center);
                return true;
            }
            //return false;
		}
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            damage = (int)(damage * 1.5f);
            velocity = Vector2.Normalize(velocity) * Item.shootSpeed;
        }
        /*public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            swingFrame++;
            /*if (swingFrame == 20)
            {
                int hooksOut = 0;
                for (int l = 0; l < 1000; l++)
                {
                    if (Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && Main.projectile[l].type == ModContent.ProjectileType<HarpoonBladeProj>())
                    {
                        hooksOut++;
                    }
                }
                if (hooksOut < 7)
                {
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.Coins, player.Center);
                    Vector2 direction = Main.MouseWorld - player.Center;
                    direction.Normalize();
                    //Shoot(player, new EntitySource_ItemUse_WithAmmo(player, Item), player.position, Item.shootSpeed, Item.shoot, Item.damage, Item.knockBack);//Projectile.NewProjectile(new ProjectileSource_Item(player, Item), position.X - (velocity.X * i), position.Y - (velocity.Y * i), perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                    Projectile.NewProjectile(player.GetSource_Item(Item), player.Center.X, player.Center.Y, direction.X * Item.shootSpeed, direction.Y * Item.shootSpeed, Item.shoot, Item.damage, 1, Main.myPlayer, 0, 0);
                }
            }


            if (swingFrame == 40)
            {
                swingFrame = 0;
                delay = 20;
            }
            Main.NewText(swingFrame.ToString());

        }*/ //looking back this could just use player.itemanimation. could be an interesting idea if there was a weapon where it really made sense for the projectile to not come out at the start
    }
}
