using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Buffs;

namespace Emperia.Items.Weapons.Color1   //where is located
{
    public class RougeRapier : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rouge Rapier");
			// Tooltip.SetDefault("Striking an enemy will increase critical hit damage briefly, longer on bosses");
		}
        public override void SetDefaults()
        {
            Item.damage = 35;
            Item.DamageType = DamageClass.Melee;
            Item.width = 44;
            Item.height = 44;
            Item.useTime = 21;
            Item.useAnimation = 21;     
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2.25f;  
			Item.crit = 6;
            Item.value = 48000;        
            Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.scale = 1f;
            Item.autoReuse = false;
            Item.useTurn = true;
        }
		
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(null, "Prism", 1); 
			recipe.AddIngredient(ItemID.Ruby, 8); 
			recipe.AddIngredient(ItemID.RedHusk, 1); 
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            

        }
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(5) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.RedTorch);
			}
		}
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
            if (target.boss)
			    player.AddBuff(ModContent.BuffType<RougeRage>(), Main.rand.Next(360, 600) + 300);
            else
                player.AddBuff(ModContent.BuffType<RougeRage>(), Main.rand.Next(360, 600));
        }
    }
}
