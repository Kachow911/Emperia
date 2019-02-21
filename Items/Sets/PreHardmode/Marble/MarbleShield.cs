using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Marble
{
	[AutoloadEquip(EquipType.Shield)]
	public class MarbleShield : ModItem
	{
	
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Marble Shield");
			Tooltip.SetDefault("Polished and Strong");
		}
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 28;
			item.value = 1000;
			item.rare = 2;
			item.accessory = true;
			item.defense = 4;
		}


		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "MarbleBar", 6); 	
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}