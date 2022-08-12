using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items
{
	public class DebugSheet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Debug Sheet");
		}
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.rare = 1;
		}
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
			TooltipLine line = new TooltipLine(Mod, "Debug", Emperia.DebugInfo);
			tooltips.Add(line);
		}
        public override bool AltFunctionUse(Player player)
        {
			Emperia.DebugInfo = "";
			return true;
        }
    }
}
