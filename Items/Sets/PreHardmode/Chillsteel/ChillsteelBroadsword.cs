using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Buffs;

namespace Emperia.Items.Sets.PreHardmode.Chillsteel
{
    public class ChillsteelBroadsword : ModItem
    {
		 public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chillsteel Broadsword");
			Tooltip.SetDefault("Inflicts crushing freeze, which damages and weakens enemies, stacking up to 4");
		}
        public override void SetDefaults()
        {    //Sword name
            Item.damage = 22;            //Sword damage
            Item.DamageType = DamageClass.Melee;            //if it's melee
            Item.width = 46;              //Sword width
            Item.height = 46;             //Sword height
            Item.useTime = 26;          //how fast 
            Item.useAnimation = 26;     
            Item.useStyle = 1;        //Style is how this Item is used, 1 is the style of the sword
            Item.knockBack = 2f;      //Sword knockback
            Item.value = 1000;        
            Item.rare = 3;
			Item.scale = 1f;
            Item.autoReuse = true;   //if it's capable of autoswing.
            Item.useTurn = true;             //player speed
			Item.UseSound = SoundID.Item1; 			
        }
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.GetGlobalNPC<MyNPC>().chillStacks += 1;
            target.AddBuff(ModContent.BuffType<CrushingFreeze>(), 600);

        }
        /*public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "AridScale", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            

        }*/
    }
}
