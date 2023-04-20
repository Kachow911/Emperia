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
using Emperia.Projectiles.Lightning;


namespace Emperia.Items.Sets.Hardmode.Lightning
{
    public class PulsarFlail : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 52;
            Item.noUseGraphic = true;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.DamageType = DamageClass.Melee;
            Item.width = 18;
            Item.height = 40;
            Item.shoot = ModContent.ProjectileType<PulsarFlailProj>();
            Item.shootSpeed = 13f;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5f;
            Item.value = Item.sellPrice(0, 0, 60, 0);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
			Item.consumable = false;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Pulsar Flail");
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.lightningSet)
                Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0, 0, ModContent.ProjectileType<LightningSetEffect>(), 25, knockBack, player.whoAmI);
            return true;
        }
        public override bool CanUseItem(Player player)
        {
			int count= 0;
            for (int i = 0; i < 255; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == ModContent.ProjectileType<PulsarFlailProj>())
                {
                    count++;
                }
            }
            return count < 1;
		}
    }
}
