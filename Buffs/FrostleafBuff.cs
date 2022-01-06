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
    public class FrostleafBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Nature Attuned");
			Description.SetDefault("12% increased damage and 25% increased movement speed");
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
			MyPlayer p = player.GetModPlayer<MyPlayer>();
            player.GetDamage(DamageClass.Generic) *= 1.12f;	
			player.moveSpeed *= 1.25f;
        }
    }
}
