using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Scoria //where is located
{
    public class ScoriaSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scoria Sword");
            Tooltip.SetDefault("Enemies Struck by the sword release powerful explosions when killed\nExplosions deal damage based on the strength of the enemy");
        }
        public override void SetDefaults()
        {   //Sword name
            item.damage = 28;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 16;              //Sword width
            item.height = 16;             //Sword height
            item.useTime = 27;          //how fast 
            item.useAnimation = 27;
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 3.5f;      //Sword knockback
            item.value = 100;
            item.rare = 3;
            item.scale = 1f;
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;             //projectile speed
            item.UseSound = SoundID.Item1;
        }
        public override bool UseItem(Player player)
        {

            return true;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.GetGlobalNPC<MyNPC>().scoriaExplosion = true;
        }
    }
}
