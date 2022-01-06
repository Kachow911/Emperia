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
    public class Supercharged : ModBuff
    {
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Supercharged!");
			Description.SetDefault("20% increased movement speed and 10% increased damage");
            Main.buffNoSave[Type] = true;
            //Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
			MyPlayer p = player.GetModPlayer<MyPlayer>();
			
			player.moveSpeed *= 1.20f;
            player.GetDamage(DamageClass.Melee) *= 1.10f;
            //player.thrownDamage *= 1.10f;
            player.GetDamage(DamageClass.Ranged) *= 1.10f;
            player.GetDamage(DamageClass.Magic) *= 1.10f;
            player.GetDamage(DamageClass.Summon) *= 1.10f;
        }
    }
}
