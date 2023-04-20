using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles;

namespace Emperia.Items.Sets.Hardmode.Vulcan   //where is located
{
    public class VulcanStrongblade : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Vulcan Strongblade");
			// Tooltip.SetDefault("");
		}
        public override void SetDefaults()
        {
			Item.damage = 56;            
            Item.DamageType = DamageClass.Melee;            //if it's melee
            Item.width = 64;              //Sword width
            Item.height = 68;             //Sword height
            Item.useTime = 28;          //how fast 
            Item.useAnimation = 28;     
            Item.useStyle = 1;        //Style is how this Item is used, 1 is the style of the sword
            Item.knockBack = 4f;      //Sword knockback
            Item.value = 100;        
            Item.rare = 5;
			Item.scale = 1f;
			Item.UseSound = SoundID.Item18;
			Item.shoot = ModContent.ProjectileType<VulcanMeteor>();
			Item.shootSpeed = 8f;
            Item.autoReuse = true;   //if it's capable of autoswing.
            Item.useTurn = true;            
        }
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
           for (int i = 0; i < 2 + Main.rand.Next(2); i++)
           {
               Projectile.NewProjectile(source, position.X, position.Y, velocity.X * Main.rand.Next(3, 8), velocity.Y * Main.rand.Next(3, 8), type, damage / 2, knockBack, player.whoAmI);
           }
            return false;
        }
    }
}