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
    public class CeruleanCharge : ModBuff
    {
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Cerulean Charge");
			Description.SetDefault("6% increased melee damage");
            Main.buffNoSave[Type] = true;
            //Main.buffNoTimeDisplay[Type] = true;

            canBeCleared = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.meleeDamage += 0.06f;
        }
    }
}
