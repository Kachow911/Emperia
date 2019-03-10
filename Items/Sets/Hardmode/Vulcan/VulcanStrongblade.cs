using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.Hardmode.Vulcan   //where is located
{
    public class VulcanStrongblade : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vulcan Strongblade");
			Tooltip.SetDefault("");
		}
        public override void SetDefaults()
        {
			item.damage = 56;            
            item.melee = true;            //if it's melee
            item.width = 64;              //Sword width
            item.height = 68;             //Sword height
            item.useTime = 28;          //how fast 
            item.useAnimation = 28;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 4f;      //Sword knockback
            item.value = 100;        
            item.rare = 5;
			item.scale = 1f;
			item.UseSound = SoundID.Item18;
			item.shoot = mod.ProjectileType("VulcanMeteor");
			item.shootSpeed = 8f;
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;            
        }
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
           for (int i = 0; i < 2 + Main.rand.Next(2); i++)
           {
               Projectile.NewProjectile(position.X, position.Y, speedX * Main.rand.Next(3, 8), speedY * Main.rand.Next(3, 8), type, damage / 2, knockBack, player.whoAmI);
           }
            return false;
        }
    }
}