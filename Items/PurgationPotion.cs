using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Buffs;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using System;

namespace Emperia.Items
{
	public class PurgationPotion : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Purgation Potion");
            Tooltip.SetDefault("Shows the location of infectious blocks");
            ItemID.Sets.DrinkParticleColors[Item.type] = new Color[2] { new Color(127, 98, 182), new Color(68, 51, 99) };
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.useStyle = 9;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = 1;
            Item.value = 1000;
            Item.buffType = (ModContent.BuffType<Purgation>());
            Item.buffTime = 36000;
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(ItemID.Deathweed);
            recipe.AddIngredient(ItemID.PixieDust, 2);
            recipe.AddTile(TileID.Bottles);
			recipe.Register();
		}

        /*public override void UseItemFrame(Player player)
        {
            if (player.itemAnimation >= 1)
            {
                Color[] array = ItemID.Sets.DrinkParticleColors[ItemID.LuckPotionGreater];
                if (array != null && array.Length != 0)
                {
                    Vector2 val = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: false, addGfxOffY: false) + Utils.RotatedBy(new Vector2((float)(player.direction * 8), player.gravDir * -4f), player.fullRotation) + Main.rand.NextVector2Square(-4f, 4f);
                    Vector2 spinningpoint = new Vector2(player.direction * 0.1f, (0f - player.gravDir) * 0.1f);
                    Dust.NewDustPerfect(val, 284, 1.3f * spinningpoint.RotatedBy(-(float)Math.PI / 5f * Main.rand.NextFloatDirection()), 0, array[Main.rand.Next(array.Length)] * 0.7f, 0.8f + 0.2f * Main.rand.NextFloat()).fadeIn = 0f;
                }
            }
        }*/
    }
}
