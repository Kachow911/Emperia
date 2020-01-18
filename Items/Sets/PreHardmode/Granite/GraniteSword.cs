using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Granite   //where is located
{
    public class GraniteSword : ModItem
    {
		private int explodeRadius = 64;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Sword");
			Tooltip.SetDefault("Critical hits release explosions of energy");
		}
        public override void SetDefaults()
        {    //Sword name
            item.damage = 23;           
            item.melee = true;            //if it's melee
            item.width = 16;              //Sword width
            item.height = 16;             //Sword height
            item.useTime = 27;          //how fast 
            item.useAnimation = 27;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 3.5f;      //Sword knockback
            item.value = 22500;        
            item.rare = 2;
			item.scale = 1f;
            item.autoReuse = false;   //if it's capable of autoswing.    
			item.UseSound = SoundID.Item1;
			item.crit = 6;			
        }
		public override void OnHitNPC (Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (crit)
			{
				explodeRadius = 64;
				MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
				if (modPlayer.graniteSet && modPlayer.graniteTime >= 1800)
				{
					explodeRadius *= 2;
					Main.PlaySound(2, (int)target.position.X, (int)target.position.Y, 10);
					for (int i = 0; i < Main.npc.Length; i++)
					{
						if (target.Distance(Main.npc[i].Center) < explodeRadius)
							Main.npc[i].StrikeNPC((int) (damage * 2.56), 0f, 0, false, false, false);
					}
					for (int i = 0; i < 30; ++i)
					{
						int index2 = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 15, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
						Main.dust[index2].noGravity = true;
						Main.dust[index2].velocity *= 4.0f;
					}
					modPlayer.graniteTime = 0;
				}
				else
				{
					Main.PlaySound(2, (int)target.position.X, (int)target.position.Y, 10);
					for (int i = 0; i < Main.npc.Length; i++)
					{
						if (target.Distance(Main.npc[i].Center) < explodeRadius)
							Main.npc[i].StrikeNPC((int) (damage * 1.28), 0f, 0, false, false, false);
					}
					for (int i = 0; i < 30; ++i)
					{
						int index2 = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 15, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
						Main.dust[index2].noGravity = true;
						Main.dust[index2].velocity *= 2.7f;
					}
				}
			}
		}
        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "GraniteBar", 8); 
            recipe.AddTile(TileID.Anvils); 			//you need 1 DirtBlock  //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
