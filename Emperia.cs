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
using Terraria.GameContent;
using System.Diagnostics;

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
        public static void DrawPixel(Vector2 position, Color color)
        {
            //EmperiaSystem.pixels.Add((position, color));
            EmperiaSystem.drawRectangles.Add((new Rectangle((int)position.X, (int)position.Y, 2, 2), color));
        }
        public static void DrawPixelRect(Rectangle rect, Color color)
        {
            /*for (int i = 0; i < rect.Height; i++)
            {
                for (int j = 0; j< rect.Width; j++)
                {
                    DrawPixel(new Vector2(rect.X + j, rect.Y + i), color);
                }
            }*/
            EmperiaSystem.drawRectangles.Add((rect, color));

        }

        public static void StopStopWatch(Stopwatch timer, string str = "")
        {
            TimeSpan timeTaken = timer.Elapsed;
            timer.Stop();
            Main.NewText(str + timeTaken.ToString());
        }
        public static float AbsoluteSum(Vector2 vec)
        {
            return Math.Abs(vec.X) + Math.Abs(vec.Y);
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
        public static List<LootCycle> lootCycles = new List<LootCycle>();
        public static List<LootCycleStatic> staticLootCycles = new List<LootCycleStatic>();
        public class LootCycle
        {
            public string source;
            public int index;
            public int[] sequence;
            public LootCycle(string source, int length)//, int index)
            {
                this.source = source;
                this.index = 0;
                this.sequence = Emperia.ShuffledArrayOfWholeNumbers(length);
                lootCycles.Add(this);
            }
        }
        public class LootCycleStatic
        {
            public string source;
            public int length;
            public LootCycleStatic(string source, int length)
            {
                this.source = source;
                this.length = length;
                staticLootCycles.Add(this);
            }
        }

        public static LootCycle GetLootCycle(string lootCycleName)
        {
            foreach (LootCycle cycle in lootCycles)
            {
                if (cycle.source == lootCycleName) return cycle;
            }
            Main.NewText("Could not find loot for" + lootCycleName, Color.Red); //this should no longer ever happen
            return null;
        }
        public override void OnWorldLoad()
        {
            foreach (LootCycleStatic staticCycle in staticLootCycles)
            {
                //Emperia.DebugInfo += staticCycle.source + ", ";
                new LootCycle(staticCycle.source, staticCycle.length);
            }
        }
        public override void LoadWorldData(TagCompound tag)
        {
            var list = tag.GetList<TagCompound>("lootCycles");
            foreach (var item in list)
            {
                string source = item.GetString("source");
                int index = item.GetInt("index");
                int[] sequence = item.GetIntArray("sequence");

                if (lootCycles.Any(cycle => cycle.source == source))
                {
                    LootCycle loadedCycle = lootCycles.First(cycle => cycle.source == source);
                    loadedCycle.index = index;
                    loadedCycle.sequence = sequence;
                }
            }
        }
        public override void SaveWorldData(TagCompound tag) // this WILL run when the game autosaves! it's not the same as onworldunload!
        {
            var list = new List<TagCompound>();
            foreach (LootCycle cycle in lootCycles)
            {
                list.Add(new TagCompound() {
                    { "source", cycle.source },
                    { "index", cycle.index },
                    { "sequence", cycle.sequence },
                });
                //Emperia.DebugInfo += $"Saved {cycle.source}, ";
            }
            tag["lootCycles"] = list;
        }
        public override void OnWorldUnload()
        {
            lootCycles.Clear();
        }

        public override void AddRecipeGroups()/* tModPorter Note: Removed. Use ModSystem.AddRecipeGroups */
        {
            MakeRecipeGroup("Silver Bar", ItemID.SilverBar, ItemID.TungstenBar);
            MakeRecipeGroup("Copper Bar", ItemID.CopperBar, ItemID.TinBar);
            //MakeRecipeGroup("Iron Bar", ItemID.IronBar, ItemID.LeadBar);
            MakeRecipeGroup("Silver Bar", ItemID.SilverBar, ItemID.TungstenBar);
            MakeRecipeGroup("Gold Bar", ItemID.GoldBar, ItemID.PlatinumBar);
            //MakeRecipeGroup("Palladium Bar", ItemID.PalladiumBar, ItemID.CobaltBar);
            MakeRecipeGroup("Adamantite Bar", ItemID.AdamantiteBar, ItemID.TitaniumBar);
            MakeRecipeGroup("Evil Ore", ItemID.DemoniteOre, ItemID.CrimtaneOre);
            MakeRecipeGroup("Evil Hide", ItemID.ShadowScale, ItemID.TissueSample);
            MakeRecipeGroup("Evil Mushroom", ItemID.VileMushroom, ItemID.ViciousMushroom);
            MakeRecipeGroup("Evil Chunk", ItemID.RottenChunk, ItemID.Vertebrae);
        }
        public void MakeRecipeGroup(string name, params int[] members)
        {
            RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + name, members);
            RecipeGroup.RegisterGroup("Emperia:" + name.Replace(" ", string.Empty), group);
        }

        public static List<(Rectangle, Color)> drawRectangles = new List<(Rectangle, Color)>();
        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {
            foreach (var rect in drawRectangles)
            {
                var rectOffset = rect.Item1;
                rectOffset.X -= (int)Main.screenPosition.X;
                rectOffset.Y -= (int)Main.screenPosition.Y;

                Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, rectOffset, rect.Item2);
                //Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)(rect.Item1.X - Main.screenPosition.X), (int)(rect.Item1.Y - Main.screenPosition.Y), rect.Item1.Width, rect.Item1.Height), rect.Item2);
            }
            drawRectangles.Clear();
        }
    }
    public class EmperiaDropRule
    {
        public static IItemDropRule OneFromOptionsCycleThroughPerRoll(string lootCycleName, int chanceDenominator, params int[] options)
        {
            new LootCycleStatic(lootCycleName, options.Length);
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
                    DropAmmoIfNeeded(info, this.dropIds[lootCycle.sequence[lootCycle.index % lootCycle.sequence.Length]]);

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
            private void DropAmmoIfNeeded(DropAttemptInfo info, int item)
            {
                if (item == ItemID.GrenadeLauncher) CommonCode.DropItem(info, ItemID.RocketI, Main.rand.Next(50, 150));
                if (item == ItemID.Stynger) CommonCode.DropItem(info, ItemID.StyngerBolt, Main.rand.Next(60, 180)); //wiki says 60 - 100, but source code says 60 - 180 unless im misreading
            }
        }
    }
}
