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
    public class ElectricCarver : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Electric Carver");
			// Tooltip.SetDefault("Low chance of inflicting electrified\nElectrified enemies take double damage");
		}


        public override void SetDefaults()
        {
            Item.damage = 42;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.DamageType = DamageClass.Melee;            
            Item.width = 60;              
            Item.height = 66;             
            Item.useStyle = ItemUseStyleID.Swing;        
            Item.knockBack = 5f;
            Item.value = 258000;
            Item.crit = 6;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;   
            Item.autoReuse = true;
            Item.useTurn = false;
            Item.shoot = ProjectileID.FireArrow;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.Next(8) == 0)
            target.AddBuff(ModContent.BuffType<ElecHostile>(), 240);
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.lightningSet)
                modPlayer.lightningDamage += damageDone;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Electric);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0f;

            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.lightningSet)
                Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0, 0, ModContent.ProjectileType<LightningSetEffect>(), 25, knockBack, player.whoAmI);
            return false;
        }
        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
           if (target.GetGlobalNPC<MyNPC>().electrified)  modifiers.SourceDamage *= 2;
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
