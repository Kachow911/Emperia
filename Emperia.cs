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
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace Emperia
{
	class Emperia : Mod
	{
        internal static Emperia instance;
        public Emperia()
		{
			/*Properties = new ModProperties()
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true,
                AutoloadBackgrounds = true
            }; i believe this simply got removed*/
		}
		public override void AddRecipeGroups()
		{
			RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Palladium Bar", new int[]
			{
			ItemID.PalladiumBar,
			ItemID.CobaltBar
			});
			RecipeGroup.RegisterGroup("Emperia:PalBar", group);
			RecipeGroup group2 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Adamantite Bar", new int[]
			{
			ItemID.AdamantiteBar,
			ItemID.TitaniumBar
			});
			RecipeGroup.RegisterGroup("Emperia:AdBar", group2);
            RecipeGroup group3 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Iron Bar", new int[]
            {
            ItemID.IronBar,
            ItemID.LeadBar
            });
            RecipeGroup.RegisterGroup("Emperia:AnyIronBar", group3);
            RecipeGroup group4 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Silver Bar", new int[]
            {
            ItemID.SilverBar,
            ItemID.TungstenBar
            });
            RecipeGroup.RegisterGroup("Emperia:AnySilverBar", group4);
            RecipeGroup group5 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Gold Bar", new int[]
            {
            ItemID.GoldBar,
            ItemID.PlatinumBar
            });
            RecipeGroup.RegisterGroup("Emperia:AnyGoldBar", group5);
			RecipeGroup group6 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Evil Ore", new int[]
            {
            ItemID.DemoniteOre,
            ItemID.CrimtaneOre
            });
            RecipeGroup.RegisterGroup("Emperia:EvilOre", group6);
			RecipeGroup group7 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Evil Hide", new int[]
            {
            ItemID.ShadowScale,
            ItemID.TissueSample
            });
            RecipeGroup.RegisterGroup("Emperia:EvilHide", group7);
            RecipeGroup group8 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Copper Bar", new int[]
            {
            ItemID.CopperBar,
            ItemID.TinBar
            });
            RecipeGroup.RegisterGroup("Emperia:AnyCopperBar", group8);
            RecipeGroup group9 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Evil Mushroom", new int[]
            {
            ItemID.VileMushroom,
            ItemID.ViciousMushroom
            });
            RecipeGroup.RegisterGroup("Emperia:EvilMushroom", group9);
            RecipeGroup group10 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Evil Chunk", new int[]
            {
            ItemID.RottenChunk,
            ItemID.Vertebrae
            });
            RecipeGroup.RegisterGroup("Emperia:EvilChunk", group10);

        }
        /*public override void UpdateMusic(ref int music)
		{
			Player player = Main.LocalPlayer;
			if (player.active && NPC.AnyNPCs(NPCType<Npcs.Yeti.Yeti>()))
			{
					music = MusicID.Boss1;
			}
			
		}*/
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
    public class EmperiaMusic : ModSceneEffect
    {
        public override int Music
        {
            get
            {
                Player player = Main.LocalPlayer;
                if (player.active && NPC.AnyNPCs(NPCType<Npcs.Yeti.Yeti>()))
                {
                    return MusicID.Boss1;
                }
                return MusicID.ConsoleMenu; //experimental no lie
            }
        }
    }
}
