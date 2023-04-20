using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

using Emperia.Projectiles;
using Emperia.Projectiles.Lightning;


namespace Emperia.Items.Sets.Hardmode.Lightning
{
    public class ZeusNeedle : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 32;
            Item.noUseGraphic = true;
            Item.useTime = 28;
            Item.useAnimation = 28;
            //Item.thrown = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 18;
            Item.height = 40;
            Item.shoot = ModContent.ProjectileType<ZeusNeedleProj>();
            Item.shootSpeed = 16f;
            Item.useStyle = 1;
            Item.knockBack = 5f;
            Item.value = Item.sellPrice(0, 10, 12, 0);
            Item.rare = 4;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.consumable = false;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Zeus' Needle");
        }
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.lightningSet)
                Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0, 0, ModContent.ProjectileType<LightningSetEffect>(), 25, knockBack, player.whoAmI);
            int num250 = Dust.NewDust(new Vector2(player.position.X, player.position.Y - 16), 8, 8, 226, (float)(player.direction * 2), 0f, 226, new Color(53f, 67f, 253f), 1.3f);
            return true;
        }
    }
}
