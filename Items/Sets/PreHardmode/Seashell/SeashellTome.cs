using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

namespace Emperia.Items.Sets.PreHardmode.Seashell
{
    public class SeashellTome : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Lexiconch");
			// Tooltip.SetDefault("Fires a powerful magic cerith, but only one can be airborne at a time");
		}
        public override void SetDefaults()
        {
            Item.damage = 23;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 20;
            Item.useAnimation = 20;     
            Item.useStyle = ItemUseStyleID.Shoot;    
            Item.mana = 6;
	        Item.UseSound = SoundID.Item101;
            Item.knockBack = 3.25f;
            Item.value = 22000;
            Item.rare = ItemRarityID.Blue;
	        Item.shoot = ModContent.ProjectileType<Cerith>(); 
	        Item.shootSpeed = 4f;
            Item.autoReuse = true;
            //Item.useTurn = true; okay i guess this makes tomes turn you around but only for one frame so dont use it lol       
        }
		public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < 250; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ItemID.Seashell, 4);
            recipe.AddIngredient(ItemID.Coral, 4);
            recipe.AddIngredient(null, "SeaCrystal", 1); 			
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
        }
    }
}
