using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;

namespace Emperia
{
	class Emperia : Mod
	{
		public Emperia()
		{
			Properties = new ModProperties()
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
		}
		public override void AddRecipeGroups()
		{
			RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + "Any Palladium Bar", new int[]
			{
			ItemID.PalladiumBar,
			ItemID.CobaltBar
			});
			RecipeGroup.RegisterGroup("Emperia:PalBar", group);
			RecipeGroup group2 = new RecipeGroup(() => Lang.misc[37] + "Any Adamantite Bar", new int[]
			{
			ItemID.AdamantiteBar,
			ItemID.TitaniumBar
			});
			RecipeGroup.RegisterGroup("Emperia:AdBar", group2);
            RecipeGroup group3 = new RecipeGroup(() => Lang.misc[37] + "Any Iron Bar", new int[]
            {
            ItemID.IronBar,
            ItemID.LeadBar
            });
            RecipeGroup.RegisterGroup("Emperia:AnyIronBar", group2);
            RecipeGroup group4 = new RecipeGroup(() => Lang.misc[37] + "Any Silver Bar", new int[]
            {
            ItemID.SilverBar,
            ItemID.TungstenBar
            });
            RecipeGroup.RegisterGroup("Emperia:AnySilverBar", group2);
            RecipeGroup group5 = new RecipeGroup(() => Lang.misc[37] + "Any Gold Bar", new int[]
            {
            ItemID.GoldBar,
            ItemID.PlatinumBar
            });
            RecipeGroup.RegisterGroup("Emperia:AnyGoldBar", group2);
        }
		public override void UpdateMusic(ref int music)
		{
			Player player = Main.LocalPlayer;
			if (player.active && NPC.AnyNPCs(NPCType("Yeti")))
			{
					music = MusicID.Boss1;
			}
			
		}
	}
}
