using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.PreHardmode.Scoria
{
    public class ScoriaHammer : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scoria Hammer");
		}


        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 30;
            item.value = 5000;
            item.rare = 3;
            item.hammer = 75;
            item.damage = 20;
            item.knockBack = 4;
            item.useStyle = 1;
            item.useTime = 20;
            item.useAnimation = 25;
            item.melee = true;
            item.useTurn = true;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
        }
		public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 258);
            }
        }

    }
}
