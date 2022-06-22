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
using Emperia.UI;
using Terraria.GameContent.UI.Elements;

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
    public class EmperiaSystem : ModSystem
    {
        internal UserInterface MyInterface;
        internal PaintUI MyPaintUI;
        internal CursorUI MyCursorUI;

        private GameTime _lastUpdateUiGameTime;

        public static bool paintUIActive;
        public static Vector2 paintUIActivationPosition;
        public static bool canRightClick = false;
        public static List<UIElement> smallPaintIconList = new List<UIElement>();
        public static List<UIElement> largePaintIconList = new List<UIElement>();
        public static UIElement modeSwapActive = null;

        public static bool cursorUIActive = false;
        public static bool canStartDrawingCursorUI = false;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                MyInterface = new UserInterface();

                MyPaintUI = new PaintUI(paintUIActivationPosition);
                MyPaintUI.Activate();

                MyCursorUI = new CursorUI();
                MyCursorUI.Activate();
            }
        }
        public override void Unload()
        {
            MyPaintUI = null;
            MyCursorUI = null;
        }
        public override void OnWorldUnload()
        {
            paintUIActive = false;
        }
        public override void UpdateUI(GameTime gameTime)
        {
            //Main.NewText(paintUIActive.ToString(), 0, 255, 255);
            //Main.NewText(cursorUIActive.ToString());
            _lastUpdateUiGameTime = gameTime;
            if (MyInterface?.CurrentState != null)
            {
                MyInterface.Update(gameTime);
                if (paintUIActive)
                {
                    if (smallPaintIconList.Any())
                    {
                        for (int i = 0; i < 32; i++)
                        {
                            smallPaintIconList[i].Update(gameTime);
                        }
                    }
                    if (largePaintIconList.Any())
                    {
                        for (int i = 0; i < largePaintIconList.Count; i++)
                        {
                            largePaintIconList[i].Update(gameTime); //idk why this doesnt work lol??
                        }
                    }
                    if (modeSwapActive != null) modeSwapActive.Update(gameTime);
                }
            }

            if (Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem].type == ItemType<Items.OldMastersPalette>())
            {
                Items.OldMastersPalette mastersPalette = Main.LocalPlayer.HeldItem.ModItem as Items.OldMastersPalette;

                if (Main.mouseRightRelease) canRightClick = true;

                if (paintUIActive)
                {
                    if (canRightClick && Main.mouseRight || !mastersPalette.curatedPalette && Main.LocalPlayer.mouseInterface && (Math.Abs(Main.MouseScreen.X - paintUIActivationPosition.X) > 84 || Math.Abs(Main.MouseScreen.Y - paintUIActivationPosition.Y) > 84) || mastersPalette.curatedPalette && Main.LocalPlayer.mouseInterface && Vector2.Distance(paintUIActivationPosition, Main.MouseScreen) > 64f)
                    {
                        paintUIActive = false;
                        canRightClick = false;
                    }
                }
                else
                {
                    if (Main.mouseRight && canRightClick && !Main.LocalPlayer.mouseInterface)
                    {
                        paintUIActive = true;
                        paintUIActivationPosition = Main.MouseScreen;
                        canRightClick = false;
                    }
                }
            }
            else paintUIActive = false;

            if (!paintUIActive && MyInterface?.CurrentState != null && MyInterface.CurrentState.ToString() == "Emperia.UI.PaintUI")
            {
                HideMyUI();
            }
            if (MyInterface?.CurrentState != null && MyInterface.CurrentState.ToString() == "Emperia.UI.CursorUI")
            {
                if (!cursorUIActive || Main.LocalPlayer.mouseInterface)
                {
                    HideMyUI();
                }
            }

            if (paintUIActive && (MyInterface?.CurrentState == null || cursorUIActive)) ShowMyUI("PaintUI");
            else if (canStartDrawingCursorUI && !paintUIActive && MyInterface?.CurrentState == null && !Main.LocalPlayer.mouseInterface)
            {
                ShowMyUI("CursorUI");
            }
            canStartDrawingCursorUI = false; //use it or lose it
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "Emperia: MyInterface",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && MyInterface?.CurrentState != null)
                        {
                            MyInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));//check if this works differently than grand design with UI scaling lmao
            }
        }
        internal void ShowMyUI(string UIType)
        {
            if (UIType == "PaintUI")
            {
                HideMyUI();
                MyInterface?.SetState(new UI.PaintUI(paintUIActivationPosition));
            }
            if (UIType == "CursorUI")
            {
                MyInterface?.SetState(new UI.CursorUI());
                cursorUIActive = true;
            }
            //MyInterface?.SetState(MyUI);
        }

        internal void HideMyUI()
        {
            MyInterface?.SetState(null);
            smallPaintIconList?.Clear();
            largePaintIconList?.Clear();
            modeSwapActive = null;

            cursorUIActive = false;
        }
    }
}
