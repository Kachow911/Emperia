﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Buffs
{
    public class Frozen : ModBuff
    {
        public override void SetStaticDefaults()
        {
           // DisplayName.SetDefault("Vermillion Venom");
			// Description.SetDefault("Decreased Contact Damage");         
            Main.debuff[Type] = true;   //Tells the game if this is a buf or not.
            Main.pvpBuff[Type] = true;  //Tells the game if pvp buff or not. 
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(NPC NPC, ref int buffIndex)
        {
            NPC.GetGlobalNPC<MyNPC>().burningNight = true;    //this tells the game to use the public bool customdebuff from NPCsINFO.cs
            int num1 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.MagicMirror);    //this is the dust/flame effect that will apear on NPC or player if is hit by this buff   
            Main.dust[num1].noGravity = true;
            Main.dust[num1].velocity *= 2f;
            NPC.velocity = Vector2.Zero;
        }
        

    }
}
