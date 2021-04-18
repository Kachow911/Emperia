using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items
{
    public class GelPad : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gel Pad");
			Tooltip.SetDefault("Right Click while holding a gauntlet to attach\nWhile attached to an equipped gauntlet, sword strikes on knockback-immune foes bounce you slightly back");
		}
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.rare = 1;
            item.value = 1750;
        }

		public virtual bool CanApply(Item item)
		{
            if (item.GetGlobalItem<GItem>().isGauntlet == true && item.GetGlobalItem<GItem>().gelPad == false)
            {
                return true;
            }
			else return false;
		}

		public sealed override bool CanRightClick()
		{
			Item item = Main.LocalPlayer.HeldItem;
            return CanApply(item);
		}

		public override void RightClick(Player player)
		{
            Item item = Main.LocalPlayer.HeldItem;
            item.GetGlobalItem<GItem>().gelPad = true;
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Gel, 40);
            recipe.AddIngredient(ItemID.PinkGel, 10); //or some twilight forest related drop mayhaps
            recipe.AddTile(TileID.Solidifier);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
