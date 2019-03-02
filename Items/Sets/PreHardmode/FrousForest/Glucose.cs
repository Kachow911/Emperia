using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.FrousForest   //where is located
{
    public class Glucose : ModItem
    {
		private int canHit = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glucose");
			Tooltip.SetDefault("Hitting an enemy with the blade will cause a ring of leaves to jet into them");
		}
        public override void SetDefaults()
        {
			item.damage = 21;            
            item.melee = true;            
            item.width = 32;              //Sword width
            item.height = 32;             //Sword height
            item.useTime = 32;          //how fast 
            item.useAnimation = 32;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 0f;      //Sword knockback
            item.value = 204000;        
            item.rare = 2;
			item.scale = 1f;
			item.UseSound = SoundID.Item18;
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;              
        }
		public override bool UseItem(Player player)
		{
			
			return true;
		}
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
		    for (int i = 0; i < 8; i++)
			{
				
				Vector2 placePosition = target.Center + new Vector2(0, 128).RotatedBy(MathHelper.ToRadians(45 * i));
                Vector2 speed = (target.Center + 4 * target.velocity) - placePosition;
                speed.Normalize();
				int p = Projectile.NewProjectile(placePosition.X, placePosition.Y, speed.X * 10f, speed.Y * 10f, mod.ProjectileType("VineLeaf2"), 10, 2f, Main.myPlayer, 0, 0);
                Main.projectile[p].penetrate = 1;
                Main.projectile[p].tileCollide = false;
                Main.projectile[p].velocity = new Vector2(speed.X * 5f, speed.Y * 5f);
                Main.projectile[p].usesLocalNPCImmunity = false;
            }
		}
    }
}