using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles;
 
namespace Emperia.Items.Weapons.GoblinArmy
{
    public class OversizedFemur : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Oversized Femur");
			Tooltip.SetDefault("Right click to throw");
		}
        public override void SetDefaults()
        {
            Item.damage = 32;
            Item.DamageType = DamageClass.Melee;
            Item.width = 46;
            Item.height = 66;
            Item.useTime = 34;
            Item.useAnimation = 34;
            Item.useStyle = 1;
            Item.knockBack = 4;
            Item.value = 10000;
            Item.shoot = ModContent.ProjectileType<FemurProj>();
            Item.shootSpeed = 7f;
            Item.rare = 2;
            Item.autoReuse = true;
            Item.useTurn = true;
 
 
        }
 
 
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
 
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)     //2 is right click
            { 
                for (int i = 0; i < Main.projectile.Length; i++)
			{
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
                {
					
                    return false;
					Item.noUseGraphic = false;
                } 
				else {
					Item.noUseGraphic = true;
                    Item.noMelee = true;
				    return true;
					
				}
				
			}
			return false;
                
            }
           else {
                 Item.noUseGraphic = false;
				 Item.noMelee = false;
                return base.CanUseItem(player);
		   }
           Item.noUseGraphic = false;
        }
	
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			if (player.altFunctionUse == 2)     //2 is right click
            { 
			return true;
			}
			else {
				
				return false;
			}
		}
    
    }
}