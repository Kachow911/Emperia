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
			TooltipLine line = new TooltipLine(Mod, "Debug", StringWithLineBreaks(Emperia.DebugInfo));
			tooltips.Add(line);
		}

		public string StringWithLineBreaks(string str)
        {
			bool insertBreakSoon = false;
			for (int i = 0; i < str.Length; i++)
            {
				if ((i + 1) % 170 == 0) insertBreakSoon = true;
				if (str[i] == ' ' && insertBreakSoon)
				{
					str = str.Insert(i + 1, "\n");
					insertBreakSoon = false;
				}
            }
			return str;
        }
        public override bool AltFunctionUse(Player player)
        {
			Emperia.DebugInfo = "";
			return true;
        }
    }
}
