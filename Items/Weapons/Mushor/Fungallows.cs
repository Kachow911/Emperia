using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Emperia.Items.Weapons.Mushor
{
	public class Fungallows : ModItem
	{
		NPC NPC;

		public override void SetStaticDefaults() {
			// Tooltip.SetDefault("hmmm test");
		}
		
		public override void SetDefaults() {
			Item.damage = 34;
			Item.DamageType = DamageClass.Melee;
			Item.width = 42;
			Item.height = 40;
			Item.useTime = 22;
			Item.useAnimation = 22;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5f;
			Item.value = 60000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
		}

		/*public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Somethingidk, uhh);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
			
		}*/

		/*public override void ModifyHitNPC (Player player, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
			if (target.life >= damage)
			{
				string testText = target.velocity.Y.ToString();
				Main.NewText(testText, 255, 240, 20, false);
				damage = target.life - 1;
				//NPC = target;
            	//NPC.GetGlobalNPC<MyNPC>().maceSlam = true;
			}
		}*/
	}
}