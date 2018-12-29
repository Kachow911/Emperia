using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.Yeti   //where is located
{
    public class MammothineClub : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mammothine Club");
			Tooltip.SetDefault("Forged from the ancient Yeti\nCritical Hits launch enemies");
		}
        public override void SetDefaults()
        {
			item.damage = 35;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 32;              //Sword width
            item.height = 32;             //Sword height
            item.useTime = 25;          //how fast 
            item.useAnimation = 25;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 2f;  
			item.crit = 4;			//Sword knockback
            item.value = 100;        
            item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.scale = 1f;
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;     
        }
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (crit && !target.boss)
			{
				for (int i = 0; i < 360; i++)
				{
					Vector2 vec = Vector2.Transform(new Vector2(-5, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));

					if (i % 8 == 0)
					{   //odd
					Dust.NewDust(target.Center + vec, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 51);
					}
				}
				Main.PlaySound(SoundID.Item, target.Center, 14);
				if (player.Center.X > target.Center.X)
				{
					target.velocity = new Vector2(-7, -7);
				}
				if (player.Center.X < target.Center.X)
				{
					target.velocity = new Vector2(7, -7);
				}
			}
		}
    }
}