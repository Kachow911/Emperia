using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

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
			Item.damage = 21;            
            Item.DamageType = DamageClass.Melee;            
            Item.width = 32;              //Sword width
            Item.height = 32;             //Sword height
            Item.useTime = 32;          //how fast 
            Item.useAnimation = 32;     
            Item.useStyle = 1;        //Style is how this Item is used, 1 is the style of the sword
            Item.knockBack = 0f;      //Sword knockback
            Item.value = 204000;        
            Item.rare = 2;
			Item.scale = 1f;
			Item.UseSound = SoundID.Item18;
            Item.autoReuse = true;   //if it's capable of autoswing.
            Item.useTurn = true;              
        }
		public override bool? UseItem(Player player)
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
				int p = Projectile.NewProjectile(player.GetSource_ItemUse(Item), placePosition.X, placePosition.Y, speed.X * 10f, speed.Y * 10f, ModContent.ProjectileType<VineLeaf2>(), 10, 2f, Main.myPlayer, 0, 0);
                Main.projectile[p].penetrate = 1;
                Main.projectile[p].tileCollide = false;
                Main.projectile[p].velocity = new Vector2(speed.X * 5f, speed.Y * 5f);
                Main.projectile[p].usesLocalNPCImmunity = false;
            }
		}
    }
}