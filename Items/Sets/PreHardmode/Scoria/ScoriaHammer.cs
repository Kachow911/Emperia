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
            Item.width = 38;
            Item.height = 30;
            Item.value = 5000;
            Item.rare = 3;
            Item.hammer = 75;
            Item.damage = 20;
            Item.knockBack = 4;
            Item.useStyle = 1;
            Item.useTime = 20;
            Item.useAnimation = 25;
            Item.DamageType = DamageClass.Melee;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
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
