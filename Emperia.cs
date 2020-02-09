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
using static Terraria.ModLoader.ModContent;

namespace Emperia
{
	class Emperia : Mod
	{
        internal static Emperia instance;
        public Emperia()
		{
			Properties = new ModProperties()
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true,
                AutoloadBackgrounds = true
            };
		}
		public override void AddRecipeGroups()
		{
			RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " Palladium Bar", new int[]
			{
			ItemID.PalladiumBar,
			ItemID.CobaltBar
			});
			RecipeGroup.RegisterGroup("Emperia:PalBar", group);
			RecipeGroup group2 = new RecipeGroup(() => Lang.misc[37] + " Adamantite Bar", new int[]
			{
			ItemID.AdamantiteBar,
			ItemID.TitaniumBar
			});
			RecipeGroup.RegisterGroup("Emperia:AdBar", group2);
            RecipeGroup group3 = new RecipeGroup(() => Lang.misc[37] + " Iron Bar", new int[]
            {
            ItemID.IronBar,
            ItemID.LeadBar
            });
            RecipeGroup.RegisterGroup("Emperia:AnyIronBar", group2);
            RecipeGroup group4 = new RecipeGroup(() => Lang.misc[37] + " Silver Bar", new int[]
            {
            ItemID.SilverBar,
            ItemID.TungstenBar
            });
            RecipeGroup.RegisterGroup("Emperia:AnySilverBar", group2);
            RecipeGroup group5 = new RecipeGroup(() => Lang.misc[37] + " Gold Bar", new int[]
            {
            ItemID.GoldBar,
            ItemID.PlatinumBar
            });
            RecipeGroup.RegisterGroup("Emperia:AnyGoldBar", group2);
			RecipeGroup group6 = new RecipeGroup(() => Lang.misc[37] + " Evil Ore", new int[]
            {
            ItemID.DemoniteOre,
            ItemID.CrimtaneOre
            });
            RecipeGroup.RegisterGroup("Emperia:EvilOre", group6);
			RecipeGroup group7 = new RecipeGroup(() => Lang.misc[37] + " Evil Hide", new int[]
            {
            ItemID.ShadowScale,
            ItemID.TissueSample
            });
            RecipeGroup.RegisterGroup("Emperia:EvilHide", group7);

        }
		public override void UpdateMusic(ref int music)
		{
			Player player = Main.LocalPlayer;
			if (player.active && NPC.AnyNPCs(NPCType("Yeti")))
			{
					music = MusicID.Boss1;
			}
			
		}
        public override void Load()
        {
            instance = this;
            if (!Main.dedServ)
			{
				Filters.Scene["Emperia:Volcano"] = new Filter(new VolcanoScreenShaderData("FilterMiniTower").UseColor(0.8f, 0.2f, 0.1f).UseOpacity(0.5f), EffectPriority.VeryHigh);
				SkyManager.Instance["Emperia:Volcano"] = new VolcanoSky();
			}
        }
    }
}
