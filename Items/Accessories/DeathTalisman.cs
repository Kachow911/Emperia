using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories
{
    public class DeathTalisman : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Talisman of Death");
			Tooltip.SetDefault("Critical hits alight enemies with fate's flames\nEnemies that die under this effect explode into additional flames\nFlames deal more damage based on the maximum life of the enemy");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 4;
            item.value = 199800;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<MyPlayer>().deathTalisman = true;
            //make the effect have a 50% chance to fail when above a 20% crit chance, minus a 1 in 5 chance to always succeed (essentially nerfing high crit chance builds)
            //reduce drop rate to make it rare? probably a good idea considering how strong it is. Maybe one in 200
        }
		
    }
}
