using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

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
            item.damage = 22;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 46;              //Sword width
            item.height = 46;             //Sword height
            item.useTime = 26;          //how fast 
            item.useAnimation = 26;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 2f;      //Sword knockback
            item.value = 1000;        
            item.rare = 3;
			item.scale = 1f;
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;             //player speed
			item.UseSound = SoundID.Item1; 			
        }
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.GetGlobalNPC<MyNPC>().chillStacks += 1;
            target.AddBuff(mod.BuffType("CrushingFreeze"), 600);

        }
        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AridScale", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }*/
    }
}
