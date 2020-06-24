using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

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
            item.damage = 43;
            item.noMelee = true;
            item.ranged = true;
            item.width = 40;
            item.height = 20;
            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = 5;
			item.shoot = mod.ProjectileType("IceCannonball");
			item.useAmmo = AmmoID.Bullet;
            item.knockBack = 5.5f;
			item.value = 52500;
            item.rare = 1;
			item.crit = 4;
            item.autoReuse = false;
            item.shootSpeed = 16f;
        }
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();	
			if (!(player.altFunctionUse == 2))
			{
				if (modPlayer.iceCannonLoad > 0)
				{
					for (int i = 0; i < 6; i++)
					{
						int index2 = Dust.NewDust(player.position + new Vector2(18, 0), player.width, player.height, 68, player.velocity.X / 5, (float) player.velocity.Y, 0, default(Color), 0.9f);
						Main.dust[index2].noGravity = true;
					}
					Main.PlaySound(SoundID.Item, player.Center, 11);
					modPlayer.iceCannonLoad--;
					type = mod.ProjectileType("IceCannonball");
					return true;
				}
			}
			return false;  
		}
		public override bool AltFunctionUse(Player player)
		{
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			modPlayer.iceCannonLoad = item.useTime * -1;
			Main.PlaySound(SoundID.Item28, player.Center);
			//for (int i = 0; i < 8; i++)
			//{
			//	int index2 = Dust.NewDust(Vector2.Normalize(new Vector2(speedX, speedY)) * 25f + position, 16, 8, 68, player.velocity.X / 5, (float) player.velocity.Y, 0, default(Color), 1.2f);						
			//	Main.dust[index2].noGravity = true;
			//	Main.dust[index2].velocity *= 0f;
			//}
			return true;
		}
		public override bool UseItem(Player player)
		{
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			return modPlayer.iceCannonLoad > 0;
		}
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
		public override bool ConsumeAmmo(Player player)
		{
			if (player.altFunctionUse == 2) return false;
			else return true;
		}
    }
}