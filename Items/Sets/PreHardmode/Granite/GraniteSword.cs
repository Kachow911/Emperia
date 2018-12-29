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
		private int explodeRadius = 50;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Sword");
			Tooltip.SetDefault("Critical hits release explosions of energy");
		}
        public override void SetDefaults()
        {    //Sword name
            item.damage = 21;           
            item.melee = true;            //if it's melee
            item.width = 16;              //Sword width
            item.height = 16;             //Sword height
            item.useTime = 27;          //how fast 
            item.useAnimation = 27;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 3.5f;      //Sword knockback
            item.value = 100;        
            item.rare = 2;
			item.scale = 1f;
            item.autoReuse = false;   //if it's capable of autoswing.
            item.useTurn = true;             //projectile speed      
			item.UseSound = SoundID.Item1; 
        }
		public override void OnHitNPC (Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (crit)
			{
				Main.PlaySound(2, (int)target.position.X, (int)target.position.Y, 10);
				for (int i = 0; i < Main.npc.Length; i++)
				{
					if (target.Distance(Main.npc[i].Center) < explodeRadius)
						Main.npc[i].StrikeNPC(damage * 2, 0f, 0, false, false, false);
				}
				 for (int i = 0; i < 360; i += 5)
				{
					Vector2 vec = Vector2.Transform(new Vector2(-10, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
					vec.Normalize();
					int num622 = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 15, 0f, 0f, 74, new Color(53f, 67f, 253f), 1.3f);
					Main.dust[num622].velocity += (vec *1.2f);
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