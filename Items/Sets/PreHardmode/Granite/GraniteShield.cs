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
			// DisplayName.SetDefault("Granite Shield");
			// Tooltip.SetDefault("Increased invinicility frames duration\n'Made of real rock'");
		}
		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 28;
            Item.value = 27000;
			Item.rare = 1;
			Item.accessory = true;
			Item.defense = 3;
		}
		public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
        {
			player.immuneTime += 30;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "GraniteBar", 8); 	
			recipe.AddTile(TileID.Anvils);  	
			recipe.Register();
			
		}
	}
}
