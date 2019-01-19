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
        public override void SetDefaults()
        {
           DisplayName.SetDefault("Vermillion Venom");
			Description.SetDefault("Decreased Contact Damage");         
            Main.debuff[Type] = true;   //Tells the game if this is a buf or not.
            Main.pvpBuff[Type] = true;  //Tells the game if pvp buff or not. 
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<MyNPC>(mod).burningNight = true;    //this tells the game to use the public bool customdebuff from NPCsINFO.cs
            int num1 = Dust.NewDust(npc.position, npc.width, npc.height, 15);    //this is the dust/flame effect that will apear on npc or player if is hit by this buff   
            Main.dust[num1].noGravity = true;
            Main.dust[num1].velocity *= 2f;
            npc.velocity = Vector2.Zero;
        }
        

    }
}
