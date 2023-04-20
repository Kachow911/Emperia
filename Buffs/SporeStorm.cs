﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia;

namespace Emperia.Buffs
{
    public class SporeStorm : ModBuff
    {
        public override void SetStaticDefaults()
        {
           // DisplayName.SetDefault("Spore Storm");
			// Description.SetDefault("Losing life");         
            Main.debuff[Type] = true;   //Tells the game if this is a buf or not.
            Main.pvpBuff[Type] = true;  //Tells the game if pvp buff or not. 
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(NPC NPC, ref int buffIndex)
        {
            NPC.GetGlobalNPC<MyNPC>().sporeStorm = true;    //this tells the game to use the public bool customdebuff from NPCsINFO.cs
            int num1 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.BlueTorch);    //this is the dust/flame effect that will apear on NPC or player if is hit by this buff   
			Main.dust[num1].noGravity = true;
			Main.dust[num1].velocity *= 2f;
        }
        

    }
}
