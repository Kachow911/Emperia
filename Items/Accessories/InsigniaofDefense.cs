using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories
{
    public class InsigniaofDefense : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ironclad Insignia");
			Tooltip.SetDefault("Attacks dealing high damage may spawn metallic energy, increasing defenses when collected\nThe stronger the attack, the higher the chance");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 5;
            item.value = Item.sellPrice(0, 2, 50, 0);
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<MyPlayer>().defenseInsignia = true;
        }
    }
}
