using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Chat;
using Microsoft.Xna.Framework.Graphics;
using System; 
using Emperia.Projectiles;

namespace Emperia.Items.Sets.PreHardmode.Scoria    
{
    public class ScoriaCrystal : ModItem
    {
		public override void SetDefaults()
		{

			Item.damage = 26;
			Item.noMelee = true;
			Item.noUseGraphic = false;
			Item.DamageType = DamageClass.Magic;
			Item.scale = 1f;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = 5;
			Item.knockBack = 3f;

			Item.staff[Item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
			Item.rare = 3;
			Item.value = Item.sellPrice(0, 0, 40, 0);
			Item.UseSound = SoundID.Item43;
			Item.autoReuse = true;
			Item.shootSpeed = 7f;
			Item.mana = 7;
			Item.shoot = ModContent.ProjectileType<ScorchBlast>();
		}

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Scoria Crystal");
            // Tooltip.SetDefault("Shoots scorch blasts");
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
           for (int i = 0; i < 2; i++)
           {
               Projectile.NewProjectile(source, position.X, position.Y, velocity.X * Main.rand.Next(3, 8), velocity.Y * Main.rand.Next(3, 8), type, damage, knockBack, player.whoAmI);
           }
            return false;
        }
    }
}
