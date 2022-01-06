using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Emperia.Projectiles.Skeleton;


namespace Emperia.Items.Weapons.Skeletron
{
    public class BoneWhip : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 25;
            Item.noUseGraphic = true;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.DamageType = DamageClass.Melee;
            Item.width = 18;
            Item.height = 40;
            Item.shoot = ModContent.ProjectileType<BoneWhipProj>();
            Item.shootSpeed = 13f;
            Item.useStyle = 1;
            Item.knockBack = 5f;
            Item.value = Item.sellPrice(0, 0, 60, 0);
            Item.rare = 3;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
			Item.consumable = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bone Whip");
        }
		
        public override bool CanUseItem(Player player)
        {
			int count= 0;
            for (int i = 0; i < 255; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == ModContent.ProjectileType<BoneWhipProj>())
                {
                    count++;
                }
            }
            return count < 1;
		}
    }
}
