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
            // DisplayName.SetDefault("Scoria Sword");
            // Tooltip.SetDefault("Enemies Struck by the sword release powerful explosions when killed\nExplosions deal damage based on the strength of the enemy");
        }
        public override void SetDefaults()
        {   //Sword name
            Item.damage = 28;            //Sword damage
            Item.DamageType = DamageClass.Melee;            //if it's melee
            Item.width = 16;              //Sword width
            Item.height = 16;             //Sword height
            Item.useTime = 27;          //how fast 
            Item.useAnimation = 27;
            Item.useStyle = 1;        //Style is how this Item is used, 1 is the style of the sword
            Item.knockBack = 3.5f;      //Sword knockback
            Item.value = 100;
            Item.rare = 3;
            Item.scale = 1f;
            Item.autoReuse = true;   //if it's capable of autoswing.
            Item.useTurn = true;             //Projectile speed
            Item.UseSound = SoundID.Item1;
        }
        public override bool? UseItem(Player player)
        {

            return true;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.GetGlobalNPC<MyNPC>().scoriaExplosion = true;
        }
    }
}
