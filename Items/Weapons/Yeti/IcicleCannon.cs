using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles.Yeti;
using static Terraria.Audio.SoundEngine;

namespace Emperia.Items.Weapons.Yeti
{
    public class IcicleCannon : ModItem
    {
		private int reloadSound = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Icicle Cannon");
			Tooltip.SetDefault("Right Click to reload the gun by freezing the water in the air\n'There is no water in the air!'");
		}
        public override void SetDefaults()
        {
            Item.damage = 43;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 20;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = 5;
			Item.shoot = ModContent.ProjectileType<IceCannonball>();
			Item.useAmmo = AmmoID.Bullet;
            Item.knockBack = 5.5f;
			Item.value = 52500;
            Item.rare = 1;
			Item.crit = 4;
            Item.autoReuse = false;
            Item.shootSpeed = 16f;
        }

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			type = ModContent.ProjectileType<IceCannonball>();
		}
		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();	
			if (!(player.altFunctionUse == 2))
			{
				if (modPlayer.iceCannonLoad > 0)
				{
					for (int i = 0; i < 6; i++)
					{
						int index2 = Dust.NewDust(player.position + new Vector2(18, 0), player.width, player.height, 68, player.velocity.X / 5, player.velocity.Y, 0, default(Color), 0.9f);
						Main.dust[index2].noGravity = true;
					}
					PlaySound(SoundID.Item, player.Center, 11);
					modPlayer.iceCannonLoad--;
					return true;
				}
			}
			return false;  
		}
		public override bool AltFunctionUse(Player player)
		{
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			modPlayer.iceCannonLoad = Item.useTime * -1;
			PlaySound(SoundID.Item28, player.Center);
			//for (int i = 0; i < 8; i++)
			//{
			//	int index2 = Dust.NewDust(Vector2.Normalize(new Vector2(speedX, speedY)) * 25f + position, 16, 8, 68, player.velocity.X / 5, (float) player.velocity.Y, 0, default(Color), 1.2f);						
			//	Main.dust[index2].noGravity = true;
			//	Main.dust[index2].velocity *= 0f;
			//}
			return true;
		}
		/*public override bool? UseItem(Player player)
		{
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			return modPlayer.iceCannonLoad > 0;
		}*/
		public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, -1);
        }
		public override bool CanUseItem(Player player)
        {
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.altFunctionUse == 2) return true;
			else return modPlayer.iceCannonLoad > 0;
        }
		public override bool CanConsumeAmmo(Player player)
		{
			if (player.altFunctionUse == 2) return false;
			else return true;
		}
    }
}