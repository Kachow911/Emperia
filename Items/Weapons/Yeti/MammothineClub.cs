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
            Item.damage = 29;
            Item.DamageType = DamageClass.Melee;
            Item.width = 42;
            Item.height = 42;
            Item.useTime = 40;
            Item.useAnimation = 40;     
            Item.useStyle = 1;
            Item.knockBack = 7f;
            Item.value = 52500;        
            Item.rare = 1;
			Item.scale = 1.3f;
            Item.autoReuse = false;
            Item.useTurn = false;
			Item.UseSound = SoundID.Item1;	
            //Item.reuseDelay = 10; doesnt work!!
        }
        public override bool? UseItem(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.clubSwing == -2) //prevents club from creating spikes if it hits an enemy frame 1
            {
                modPlayer.clubSwing = -1;
            }
            else {
                modPlayer.clubSwing = (int)(Item.useTime * player.GetAttackSpeed(DamageClass.Melee)) - 1;
            }
            modPlayer.projItemOrigin = Item;
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
