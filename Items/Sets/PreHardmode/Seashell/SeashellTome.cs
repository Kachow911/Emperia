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
			DisplayName.SetDefault("Seashell Tome");
			Tooltip.SetDefault("blank");
		}
        public override void SetDefaults()
        {    //Sword name
            item.damage = 9;            //Sword damage
            item.magic = true;
            item.noMelee = true;          //if it's melee
            item.width = 32;              //Sword width
            item.height = 32;             //Sword height  //Item Description
            item.useTime = 32;          //how fast 
            item.useAnimation = 32;     
            item.useStyle = 5;    
            item.mana = 17;			//Style is how this item is used, 1 is the style of the sword
            item.knockBack = 5;      //Sword knockback
            item.value = 100;        
            item.rare = 1;
			item.shoot = mod.ProjectileType("Cerith"); 
			item.shootSpeed = 8f;
            item.autoReuse = false;   //if it's capable of autoswing.
            item.useTurn = true;             //projectile speed                 
        }
    }
}