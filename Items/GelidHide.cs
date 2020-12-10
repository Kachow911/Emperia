using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Npcs.Yeti;

namespace Emperia.Items
{
    public class GelidHide : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gelid Hide");
			Tooltip.SetDefault("Looks Rotten. I wonder what it could lure?.\nUsed in the snow biome");
		}
        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 36;
            item.maxStack = 999;
            item.rare = 3;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item44;
            item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            //return NPC.downedBoss3;
            return player.ZoneSnow;
        }

        public override bool UseItem(Player player)
        {
			Main.NewText("The slumber of the Arctic Guardian has been disturbed...");
            int dist = 0;
            if (Main.rand.NextBool(2))
                dist = -1200;
            else
                dist = 1200;
        
			NPC.NewNPC((int)player.Center.X + dist, (int)player.Center.Y - 400, mod.NPCType("Yeti"));
            Main.PlaySound(SoundID.Roar, player.position, 0);
			MyPlayer modPlayer1 = Main.player[Main.myPlayer].GetModPlayer<MyPlayer>();

            return true;
        }
    }
}
