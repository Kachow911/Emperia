using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

namespace Emperia.Items.Sets.Hardmode.Mystique   //where is located
{
    public class FloralCutter : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Floral Cutter");
			// Tooltip.SetDefault("Enemies Killed by the sword explode into homing leaves");
		}
        public override void SetDefaults()
        {
			Item.damage = 63;            
            Item.DamageType = DamageClass.Melee;            //if it's melee
            Item.width = 64;              //Sword width
            Item.height = 64;             //Sword height
            Item.useTime = 28;          //how fast 
            Item.useAnimation = 28;     
            Item.useStyle = ItemUseStyleID.Swing;        //Style is how this Item is used, 1 is the style of the sword
            Item.knockBack = 5f;      //Sword knockback
            Item.value = 100;        
            Item.rare = ItemRarityID.LightPurple;
			Item.scale = 1f;
			Item.UseSound = SoundID.Item18;
			//Item.shoot = ModContent.ProjectileType<Leafy>();
			Item.shootSpeed = 8f;
            Item.autoReuse = true;   //if it's capable of autoswing.
            Item.useTurn = true;            
        }
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (target.life <= 0)
			{
				for (int i = 0; i < 50; ++i) //Create dust after teleport
				{
					int dust = Dust.NewDust(target.position, target.width, target.height, DustID.Snow, (float)0, (float)0, 0, new Color(255, 113, 182), 1.1f);
					int dust1 = Dust.NewDust(target.position, target.width, target.height, DustID.Snow, (float)0, (float)0, 0, new Color(255, 113, 182), 1.1f);
					Main.dust[dust1].scale = 0.8f;
					Main.dust[dust1].velocity *= 2f;
				}
				for (int i = 0; i < 12; i++)
				{
				
					Vector2 perturbedSpeed = new Vector2(0, 3).RotatedBy(MathHelper.ToRadians(90 + 30 * i));
					Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<HomingLeaf>(), hit.SourceDamage / 3, 1, Main.myPlayer, 0, 0);
				
				}
			}
		}
    }
}