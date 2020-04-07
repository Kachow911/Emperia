using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader.IO;
using Terraria.GameInput;


namespace Emperia.Items.Sets.Hardmode.Lightning
{
    public class PulsarFlail : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 52;
            item.noUseGraphic = true;
            item.useTime = 22;
            item.useAnimation = 22;
            item.melee = true;
            item.width = 18;
            item.height = 40;
            item.shoot = mod.ProjectileType("PulsarFlailProj");
            item.shootSpeed = 13f;
            item.useStyle = 1;
            item.knockBack = 5f;
            item.value = Item.sellPrice(0, 0, 60, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
			item.consumable = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pulsar Flail");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.lightningSet)
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, 0, mod.ProjectileType("LightningSetEffect"), 25, knockBack, player.whoAmI);
            return true;
        }
        public override bool CanUseItem(Player player)
        {
			int count= 0;
            for (int i = 0; i < 255; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == mod.ProjectileType("PulsarFlailProj"))
                {
                    count++;
                }
            }
            return count < 1;
		}
    }
}
