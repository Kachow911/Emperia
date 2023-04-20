using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.PreHardmode.Scoria
{
    public class ScoriaAxe : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Scoria Axe");
		}


        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 30;
            Item.value = 5000;
            Item.rare = ItemRarityID.Orange;
            Item.axe = 16;
            Item.damage = 19;
            Item.knockBack = 4;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime =18;
            Item.useAnimation = 22;
            Item.DamageType = DamageClass.Melee;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
        }
		public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.LavaMoss);
            }
        }

    }
}
