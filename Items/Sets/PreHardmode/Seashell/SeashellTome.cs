using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Seashell  //where is located
{
    public class SeashellTome : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lexiconch");
			Tooltip.SetDefault("Shoots magic ceriths");
		}
        public override void SetDefaults()
        {    //Sword name
            item.damage = 17;            //Sword damage
            item.magic = true;
            item.noMelee = true;          //if it's melee
            item.width = 32;              //Sword width
            item.height = 32;             //Sword height  //Item Description
            item.useTime = 28;          //how fast 
            item.useAnimation = 28;     
            item.useStyle = 5;    
            item.mana = 5;			//Style is how this item is used, 1 is the style of the sword
            item.knockBack = 3.5;      //Sword knockback
            item.value = 16500;        
            item.rare = 1;
			item.shoot = mod.ProjectileType("Cerith"); 
			item.shootSpeed = 6f;
            item.autoReuse = false;   //if it's capable of autoswing.
            item.useTurn = true;             //projectile speed                 
        }
		public override bool CanUseItem(Player player)       //this make that you can shoot only 1 boomerang at once
        {
            for (int i = 0; i < 250; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
    }
}