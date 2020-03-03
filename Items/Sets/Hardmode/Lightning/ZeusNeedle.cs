using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;


namespace Emperia.Items.Sets.Hardmode.Lightning
{
    public class ZeusNeedle : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 32;
            item.noUseGraphic = true;
            item.useTime = 28;
            item.useAnimation = 28;
            item.thrown = true;
            item.width = 18;
            item.height = 40;
            item.shoot = mod.ProjectileType("ZeusNeedleProj");
            item.shootSpeed = 16f;
            item.useStyle = 1;
            item.knockBack = 5f;
            item.value = Item.sellPrice(0, 10, 12, 0);
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.consumable = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zeus' Needle");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type1, ref int damage, ref float knockBack)
        {
            int num250 = Dust.NewDust(new Vector2(player.position.X, player.position.Y - 16), 8, 8, 226, (float)(player.direction * 2), 0f, 226, new Color(53f, 67f, 253f), 1.3f);
            return true;
        }
    }
}
