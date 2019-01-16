using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Aquatic
{
    public class Wave : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wave");
			Tooltip.SetDefault("Summons magic waves to defeat enemies");
		}


        public override void SetDefaults()
        {
            item.damage = 34;
            item.magic = true;
            item.mana = 13;
            item.width = 46;
            item.height = 46;
            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = 5;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = Terraria.Item.sellPrice(0, 0, 50, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item34;
            item.autoReuse = false;
            item.shoot = mod.ProjectileType("WaveProj");
            item.shootSpeed = 8f;
            item.autoReuse = false;
        }
    }
}
