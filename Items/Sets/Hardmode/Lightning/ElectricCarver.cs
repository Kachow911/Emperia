using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
namespace Emperia.Items.Sets.Hardmode.Lightning
{
    public class ElectricCarver : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Electric Carver");
			Tooltip.SetDefault("Low chance of inflicting electrified\nElectrified enemies take double damage");
		}


        public override void SetDefaults()
        {
            item.damage = 42;
            item.useTime = 35;
            item.useAnimation = 35;
            item.melee = true;            
            item.width = 60;              
            item.height = 66;             
            item.useStyle = 1;        
            item.knockBack = 5f;
            item.value = 258000;
            item.crit = 6;
            item.rare = 4;
            item.UseSound = SoundID.Item1;   
            item.autoReuse = true;
            item.useTurn = true;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(8) == 0)
            target.AddBuff(mod.BuffType("ElecHostile"), 240);
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
           if (target.GetGlobalNPC<MyNPC>().electrified)
           {
                damage *= 2;
           }
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
