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
			// DisplayName.SetDefault("Scoria Pickaxe");
		}
    public override void SetDefaults()
    {
        Item.damage = 16;
        Item.DamageType = DamageClass.Melee;
        Item.width = 46;
        Item.height = 46;
        Item.useTime = 14;
        Item.useAnimation = 22;
        Item.useTurn = true;
        Item.pick = 70;
        Item.useStyle = 1;
        Item.knockBack = 2f;
        Item.value = 5000;
        Item.rare = 3;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }
	public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 258);
            }
        }
}}