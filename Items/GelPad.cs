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
            Item.width = 20;
            Item.height = 20;
            Item.rare = 1;
            Item.value = 1750;
        }

		public virtual bool CanApply(Item Item)
		{
            if (Item.GetGlobalItem<GItem>().isGauntlet == true && Item.GetGlobalItem<GItem>().gelPad == false)
            {
                return true;
            }
			else return false;
		}

		public sealed override bool CanRightClick()
		{
			Item Item = Main.LocalPlayer.HeldItem;
            return CanApply(Item);
		}

		public override void RightClick(Player player)
		{
            Item Item = Main.LocalPlayer.HeldItem;
            Item.GetGlobalItem<GItem>().gelPad = true;
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Gel, 40);
            recipe.AddIngredient(ItemID.PinkGel, 10); //or some twilight forest related drop mayhaps
            recipe.AddTile(TileID.Solidifier);
            recipe.Register();
            
        }
    }
}
