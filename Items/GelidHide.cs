using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Npcs.Yeti;
using static Terraria.ModLoader.ModContent;

namespace Emperia.Items
{
    public class GelidHide : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Gelid Hide");
			// Tooltip.SetDefault("Looks Rotten. I wonder what it could lure?.\nUsed in the snow biome");
		}
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;
                        Item.rare = ItemRarityID.Orange;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item44;
            Item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            //return NPC.downedBoss3;
            return player.ZoneSnow;
        }

        public override bool? UseItem(Player player)
        {
			Main.NewText("The slumber of the Arctic Guardian has been disturbed...");
            int dist = 0;
            if (Main.rand.NextBool(2))
                dist = -1200;
            else
                dist = 1200;
        
			NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)player.Center.X + dist, (int)player.Center.Y - 400, NPCType<Yeti>());
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Roar, player.position);
			MyPlayer modPlayer1 = Main.player[Main.myPlayer].GetModPlayer<MyPlayer>();

            return true;
        }
    }
}
