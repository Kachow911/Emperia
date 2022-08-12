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
using Terraria.GameInput;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.IO;
using static Emperia.EmperiaSystem;
using System.Text;

namespace Emperia
{
	class Emperia : Mod
	{
        internal static Emperia instance;

        public static string DebugInfo;

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
		public override void AddRecipeGroups()/* tModPorter Note: Removed. Use ModSystem.AddRecipeGroups */
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
        /*public static GlobalType GetGlobal(Entity entity)
        {
            if (entity is NPC) return (entity as NPC).GetGlobalNPC<MyNPC>();
            if (entity is Item) return (entity as Item).GetGlobalItem<GItem>();
            //if (entity is Player) return (entity as Player).GetModPlayer<MyPlayer>();
            return null;
        }*/
        public static float MouseControlledFloatX(bool haveNegatives = false, int maxValue = 1)
        {
            float value;
            value = (Main.MouseScreen.X / (float)(Main.screenWidth)); //varies from 0 to 1
            value = ApplyMouseFloatParameters(value, haveNegatives, maxValue);
            return value;
        }
        public static float MouseControlledFloatY(bool haveNegatives = false, int maxValue = 1)
        {
            float value;
            value = (Main.MouseScreen.Y / (float)(Main.screenHeight)); //varies from 0 to 1
            value = ApplyMouseFloatParameters(value, haveNegatives, maxValue);
            return value;
        }
        internal static float ApplyMouseFloatParameters(float value, bool haveNegatives, int maxValue)
        {
            if (haveNegatives)
            {
                value *= 2;
                value--;
            }
            value *= maxValue;
            return value;
        }
        public static void Shuffle<T>(T[] array)
        {
            for (int i = array.Length - 1; i >= 0; i--)
            {
                Utils.Swap(ref array[Main.rand.Next(i)], ref array[i]);
            }
        }
        public static int[] ShuffledArrayOfWholeNumbers(int length)
        {
            int[] nums = new int[length];
            for (int i = 0; i < length; i++)
            {
                nums[i] = i;
            }
            Shuffle(nums);
            return nums;
        }
        public static string ToCamelCase(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            var sb = new StringBuilder();

            foreach (var c in str)
            {
                if (char.IsWhiteSpace(c))
                {
                    if (sb.Length > 0)
                        sb[0] = char.ToUpper(sb[0]);
                }
                else
                    sb.Append(c);
            }

            return sb.ToString();
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
        public static int cursorIsFreeForUI = 1;
        public static List<UIElement> smallPaintIconList = new List<UIElement>();
        public static List<UIElement> largePaintIconList = new List<UIElement>();
        public static UIState CurrentPaintUI = null;

        public static bool cursorUIActive = false;
        public static bool canStartDrawingCursorUI = false;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                MyInterface = new UserInterface();

                MyPaintUI = new PaintUI();
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
                /*if (paintUIActive)
                {
                    if (CurrentPaintUI != null)
                    {
                        foreach (UIElement element in CurrentPaintUI.Children)
                        {
                            element.Update(gameTime);
                        }
                    }
                }*/
            }

            if (Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem].type == ItemType<Items.OldMastersPalette>())
            {
                Items.OldMastersPalette mastersPalette = Main.LocalPlayer.HeldItem.ModItem as Items.OldMastersPalette;
                //if (Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem])
                if (Main.mouseRightRelease) canRightClick = true;
                if (!PlayerInput.LockGamepadTileUseButton && Main.LocalPlayer.noThrow == 0 && !Main.HoveringOverAnNPC && Main.LocalPlayer.talkNPC == -1)
                {
                    if (cursorIsFreeForUI < 1) cursorIsFreeForUI++;
                }
                else cursorIsFreeForUI = -1;

                if (paintUIActive)
                {
                    if (Main.mouseRight && canRightClick && cursorIsFreeForUI == 1
                        || !mastersPalette.curatedMode && Main.LocalPlayer.mouseInterface && (Math.Abs(Main.MouseScreen.X - paintUIActivationPosition.X) > 84 || Math.Abs(Main.MouseScreen.Y - paintUIActivationPosition.Y) > 84)
                        || mastersPalette.curatedMode && Main.LocalPlayer.mouseInterface && Vector2.Distance(paintUIActivationPosition, Main.MouseScreen) > 64f
                        )
                    {
                        paintUIActive = false;
                        //canRightClick = false;
                    }
                }
                else
                {
                    if (Main.mouseRight && canRightClick && !Main.LocalPlayer.mouseInterface && cursorIsFreeForUI == 1)
                    {
                        paintUIActive = true;
                        paintUIActivationPosition = Main.MouseScreen;
                        //canRightClick = false;
                    }
                }
                if (Main.mouseRight) canRightClick = false;
            }
            else paintUIActive = false;

            if (MyInterface?.CurrentState != null)
            {
                if (!paintUIActive && MyInterface.CurrentState.ToString() == "Emperia.UI.PaintUI")
                {
                    HideMyUI();
                }
                else if ((!cursorUIActive || Main.LocalPlayer.mouseInterface) && MyInterface.CurrentState.ToString() == "Emperia.UI.CursorUI")
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
                       InterfaceScaleType.UI));
            }
        }
        internal void ShowMyUI(string UIType)
        {
            if (UIType == "PaintUI")
            {
                HideMyUI();
                MyInterface?.SetState(new PaintUI());
            }
            if (UIType == "CursorUI")
            {
                MyInterface?.SetState(new CursorUI());
                cursorUIActive = true;
            }
            //MyInterface?.SetState(MyUI);
        }

        internal void HideMyUI()
        {
            MyInterface?.SetState(null);
            smallPaintIconList?.Clear();
            largePaintIconList?.Clear();
            CurrentPaintUI = null;

            cursorUIActive = false;
        }


        public static List<LootCycle> lootCycles = new List<LootCycle>();
        public static LootCycle mushorLootCycle;
        public static LootCycle yetiLootCycle;

        public static LootCycle GetLootCycle(string lootCycle)
        {
            switch (lootCycle)
            {
                case "mushor": return mushorLootCycle;
                case "yeti": return yetiLootCycle;
                default: return null;
            }
        }
        public override void OnWorldLoad()
        {
            mushorLootCycle = new LootCycle("mushor", 4);
            yetiLootCycle = new LootCycle("yeti", 4);
        }
        public class LootCycle
        {
            public string lootType;
            public int index;
            public int[] sequence;
            public LootCycle(string lootType, int length)//, int index)
            {
                this.lootType = lootType;
                this.index = 0;
                this.sequence = Emperia.ShuffledArrayOfWholeNumbers(length);
                lootCycles.Add(this);
            }
        }
        public override void LoadWorldData(TagCompound tag)
        {
            List<int[]> lootCyclesSeq = tag.Get<List<int[]>>("lootCyclesSeq");
            List<int> lootCyclesIndex = tag.Get<List<int>>("lootCyclesIndex");
            Emperia.DebugInfo += "|P| ";

            foreach (int num in mushorLootCycle.sequence)
            {
                Emperia.DebugInfo += num + " ";
            }
            foreach (LootCycle cycle in lootCycles)
            {
                if (!lootCyclesSeq.Any()) break;
                cycle.sequence = lootCyclesSeq.First();
                lootCyclesSeq.RemoveAt(0);
                cycle.index = lootCyclesIndex.First();
                lootCyclesIndex.RemoveAt(0);
                Emperia.DebugInfo += "|C| ";
            }
            Emperia.DebugInfo += "|L| ";

            foreach (int num in mushorLootCycle.sequence)
            {
                Emperia.DebugInfo += num + " ";
            }
        }
        public override void SaveWorldData(TagCompound tag)
        {
            List<int[]> lootCyclesSeq = new List<int[]>();
            List<int> lootCyclesIndex = new List<int>();
            foreach (LootCycle cycle in lootCycles)
            {
                lootCyclesSeq.Add(cycle.sequence);
                lootCyclesIndex.Add(cycle.index);
            }
            //tag.Add("lootCyclesSeq", lootCyclesSeq);
            //tag.Add("lootCyclesIndex", lootCyclesIndex);
            tag["lootCyclesSeq"] = lootCyclesSeq;
            tag["lootCyclesIndex"] = lootCyclesIndex;
            Emperia.DebugInfo += "|S| ";

            foreach (int num in mushorLootCycle.sequence)
            {
                Emperia.DebugInfo += num + " ";
            }
            Emperia.DebugInfo += "\n";
            lootCycles.Clear();
        }
    }
    public class EmperiaDropRule
    {
        public static IItemDropRule OneFromOptionsCycleThroughPerRoll(string lootCycleName, int chanceDenominator, params int[] options)
        {
            return new OneFromOptionsCycleThroughPerRollDropRule(lootCycleName, chanceDenominator, 1, options);
        }
        public class OneFromOptionsCycleThroughPerRollDropRule : IItemDropRule
        {
            public int[] dropIds;

            public int chanceDenominator;

            public int chanceNumerator;

            public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

            public string lootCycleName;
            public OneFromOptionsCycleThroughPerRollDropRule(string lootCycleName, int chanceDenominator, int chanceNumerator, params int[] options)
            {
                this.chanceDenominator = chanceDenominator;
                this.dropIds = options;
                this.chanceNumerator = chanceNumerator;
                this.ChainedRules = new List<IItemDropRuleChainAttempt>();
                this.lootCycleName = lootCycleName;
            }

            public bool CanDrop(DropAttemptInfo info)
            {
                return true;
            }

            public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
            {
                ItemDropAttemptResult result;
                if (info.rng.Next(this.chanceDenominator) < this.chanceNumerator)
                {
                    LootCycle lootCycle = GetLootCycle(lootCycleName);

                    CommonCode.DropItem(info, this.dropIds[lootCycle.sequence[lootCycle.index % lootCycle.sequence.Length]], 1);

                    lootCycle.index++;
                    if (lootCycle.index % lootCycle.sequence.Length == 0) Emperia.Shuffle(lootCycle.sequence);

                    result = default(ItemDropAttemptResult);
                    result.State = ItemDropAttemptResultState.Success;
                    return result;
                }
                result = default(ItemDropAttemptResult);
                result.State = ItemDropAttemptResultState.FailedRandomRoll;
                return result;
            }

            public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
            {
                float num = (float)this.chanceNumerator / (float)this.chanceDenominator;
                float num2 = num * ratesInfo.parentDroprateChance;
                float dropRate = 1f / (float)this.dropIds.Length * num2;
                for (int i = 0; i < this.dropIds.Length; i++)
                {
                    drops.Add(new DropRateInfo(this.dropIds[i], 1, 1, dropRate, ratesInfo.conditions));
                }
                Chains.ReportDroprates(this.ChainedRules, num, drops, ratesInfo);

            }
        }
    }
}
