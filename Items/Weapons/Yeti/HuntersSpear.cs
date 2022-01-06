using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Emperia.Projectiles.Yeti;


namespace Emperia.Items.Weapons.Yeti
{
    public class HuntersSpear : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.noUseGraphic = true;
            Item.useTime = 22;
            Item.useAnimation = 22;
			//Item.thrown = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 18;
            Item.height = 40;
            Item.shoot = ModContent.ProjectileType<Projectiles.Yeti.HuntersSpear>();
            Item.shootSpeed = 8f;
            Item.useStyle = 1;
            Item.knockBack = 5f;
            Item.value = Item.sellPrice(0, 0, 12, 0);
            Item.rare = 3;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
			Item.consumable = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hunter's Spear");
        }
    }
}
