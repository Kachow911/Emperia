using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Emperia.Items.Sets.PreHardmode.Desert
{
	public class DesertMace : ModItem
	{
		NPC NPC;

		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Slams airborne enemies downwards, damaging them if they hit the ground");
		}
		
		public override void SetDefaults() {
			Item.damage = 26;
			Item.DamageType = DamageClass.Melee;
			Item.width = 42;
			Item.height = 40;
			Item.useTime = 32;
			Item.useAnimation = 32;
			Item.useStyle = 1;
			Item.knockBack = 5f;
			Item.value = 27000;
			Item.rare = 1;
			Item.UseSound = SoundID.Item1;
		}

		/*public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Somethingidk, uhh);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
			
		}*/

		public override void ModifyHitNPC (Player player, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
			if (target.velocity.Y != 0 && target.noTileCollide == false && target.knockBackResist >= 0 && !target.boss)// && player.itemAnimation <= 22 if the effect is too strong
			{
				//target.velocity.Y += 12 * target.knockBackResist;
				target.velocity.Y += 4 + (6 * target.knockBackResist);
				NPC = target;
            	NPC.GetGlobalNPC<MyNPC>().maceSlam = 25;
				NPC.GetGlobalNPC<MyNPC>().maceSlamDamage = damage;
			}
		}
	}
}