using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace Emperia.Buffs
{
    public class LifesFateBuff : ModBuff
    {
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Renewed Life");
			Description.SetDefault("Life's Fate damage increased by 20%, sword strikes will steal life\n'You are overflowing with life'");
            Main.buffNoSave[Type] = true;
            //Main.buffNoTimeDisplay[Type] = true;

            canBeCleared = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
			MyPlayer p = player.GetModPlayer<MyPlayer>(mod);
			p.renewedLife = true;
        }
    }
}
