using System;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace Emperia.Buffs
{
    public class Waxwing : ModBuff
    {
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Waxwing");
			Description.SetDefault("25% increased wing speed, but 15% decreased flight time");
            Main.buffNoSave[Type] = true;
            canBeCleared = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
			MyPlayer p = player.GetModPlayer<MyPlayer>();
			player.wingTime *= 0.85f;
            if (player.wingTime > 0f)
            {
                string speedText = player.velocity.X.ToString();
                Main.NewText(speedText, 255, 240, 20, false);
                //player.moveSpeed *= 1.25f;
                //player.maxRunSpeed *= 1.25f;
                //player.velocity.X -= (player.velocity.X / 4);
            }
        }
    }
}
