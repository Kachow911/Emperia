using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Emperia.Projectiles;


namespace Emperia.Items.Sets.PreHardmode.Scoria
{
    public class ScoriaKnife : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 25;
            Item.noUseGraphic = true;
            Item.useTime = 22;
            Item.useAnimation = 22;
			//Item.thrown = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 18;
            Item.height = 40;
            Item.shoot = ModContent.ProjectileType<Projectiles.ScoriaKnife>();
            Item.shootSpeed = 8f;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5f;
            Item.value = Item.sellPrice(0, 0, 12, 0);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
			Item.consumable = true;
			        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Scoria Knife");
        }
    }
}
