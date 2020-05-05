using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Emperia.Items.Weapons.Mushor
{
	public class Fungallows : ModItem
	{
		NPC npc;

		public override void SetStaticDefaults() {
			Tooltip.SetDefault("hmmm test");
		}
		
		public override void SetDefaults() {
			item.damage = 34;
			item.melee = true;
			item.width = 42;
			item.height = 40;
			item.useTime = 22;
			item.useAnimation = 22;
			item.useStyle = 1;
			item.knockBack = 5f;
			item.value = 60000;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
		}

		/*public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Somethingidk, uhh);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}*/

		/*public override void ModifyHitNPC (Player player, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
			if (target.life >= damage)
			{
				string testText = target.velocity.Y.ToString();
				Main.NewText(testText, 255, 240, 20, false);
				damage = target.life - 1;
				//npc = target;
            	//npc.GetGlobalNPC<MyNPC>().maceSlam = true;
			}
		}*/
	}
}