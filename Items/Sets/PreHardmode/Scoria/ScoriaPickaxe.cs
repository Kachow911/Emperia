using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Scoria {
public class ScoriaPickaxe : ModItem
{
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scoria Pickaxe");
		}
    public override void SetDefaults()
    {
        item.damage = 16;
        item.melee = true;
        item.width = 46;
        item.height = 46;
        item.useTime = 14;
        item.useAnimation = 22;
        item.useTurn = true;
        item.pick = 70;
        item.useStyle = 1;
        item.knockBack = 2f;
        item.value = 5000;
        item.rare = 3;
        item.UseSound = SoundID.Item1;
        item.autoReuse = true;
    }
	public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 258);
            }
        }
}}