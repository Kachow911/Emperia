using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
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
            item.damage = 32;
            item.melee = true;
            item.width = 46;
            item.height = 66;
            item.useTime = 34;
            item.useAnimation = 34;
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = 10000;
            item.shoot = mod.ProjectileType("FemurProj");
            item.shootSpeed = 7f;
            item.rare = 2;
            item.autoReuse = true;
            item.useTurn = true;
 
 
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
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == item.shoot)
                {
					
                    return false;
					item.noUseGraphic = false;
                } 
				else {
					item.noUseGraphic = true;
                    item.noMelee = true;
				    return true;
					
				}
				
			}
			return false;
                
            }
           else {
                 item.noUseGraphic = false;
				 item.noMelee = false;
                return base.CanUseItem(player);
		   }
           item.noUseGraphic = false;
        }
	
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
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