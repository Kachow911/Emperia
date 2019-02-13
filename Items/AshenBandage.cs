using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Npcs.Yeti;

namespace Emperia.Items
{
    public class AshenBandage : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ashen Bandage");
			Tooltip.SetDefault("The ashen clumps seem to have some healing properties\nHeals 60HP\nLower than normal cooldown");
		}
        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 36;
            item.maxStack = 999;
            item.rare = 2;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item44;
            item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return !player.HasBuff(BuffID.PotionSickness);
        }

        public override bool UseItem(Player player)
        {
            player.statLife += 60;
            player.HealEffect(60);
            player.AddBuff(BuffID.PotionSickness, 2700);

            return true;
        }
        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AshenStrips", 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();

        }
    }
}
