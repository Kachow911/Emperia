using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.Yeti
{
    public class MammothineClub : ModItem
    {
		 public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mammothine Club");
			Tooltip.SetDefault("Swinging without hitting an enemy will raise icy spikes from the ground");
		}
        public override void SetDefaults()
        {
            item.damage = 29;
            item.melee = true;
            item.width = 42;
            item.height = 42;
            item.useTime = 40;
            item.useAnimation = 40;     
            item.useStyle = 1;
            item.knockBack = 7f;
            item.value = 52500;        
            item.rare = 1;
			item.scale = 1.3f;
            item.autoReuse = false;
            item.useTurn = false;
			item.UseSound = SoundID.Item1;	
            //item.reuseDelay = 10; doesnt work!!
        }
        public override bool UseItem(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.clubSwing == -2) //prevents club from creating spikes if it hits an enemy frame 1
            {
                modPlayer.clubSwing = -1;
            }
            else {
                modPlayer.clubSwing = (int)(item.useTime * player.meleeSpeed) - 1;
            }
            return true;
        }
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.clubSwing == -1) modPlayer.clubSwing = -2; //this only occurs if the club hits frame 1
            else modPlayer.clubSwing = -1;
		}
    }
}
