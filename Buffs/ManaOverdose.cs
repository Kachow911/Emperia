using Terraria;
using Terraria.ModLoader;

namespace Emperia.Buffs
{
    public class ManaOverdose : ModBuff
    {
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Mana Overdose");
			Description.SetDefault("Cannot consume any more mana restoring items");
            Main.buffNoSave[Type] = true;
            //Main.buffNoTimeDisplay[Type] = true;

            canBeCleared = false;
        }
    }
}
