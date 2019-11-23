using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Granite
{
	[AutoloadEquip(EquipType.Shield)]
	public class GraniteShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Shield");
			Tooltip.SetDefault("Increased invinicility frames duration\n'Made of real rock'");
		}
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 28;
			item.value = 1000;
			item.rare = 2;
			item.accessory = true;
			item.defense = 3;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.immuneTime += 30;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "GraniteBar", 8); 	
			recipe.AddTile(TileID.Anvils);  	
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
