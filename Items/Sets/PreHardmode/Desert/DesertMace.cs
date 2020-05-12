using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Emperia.Items.Sets.PreHardmode.Desert
{
	public class DesertMace : ModItem
	{
		NPC npc;

		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Slams airborne enemies downwards, damaging them if they hit the ground");
		}
		
		public override void SetDefaults() {
			item.damage = 26;
			item.melee = true;
			item.width = 42;
			item.height = 40;
			item.useTime = 32;
			item.useAnimation = 32;
			item.useStyle = 1;
			item.knockBack = 5f;
			item.value = 62000;
			item.rare = 1;
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

		public override void ModifyHitNPC (Player player, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
			if (target.velocity.Y != 0 && target.noTileCollide == false && target.knockBackResist >= 0 && !target.boss)// && player.itemAnimation <= 22 if the effect is too strong
			{
				//target.velocity.Y += 12 * target.knockBackResist;
				target.velocity.Y += 4 + (6 * target.knockBackResist);
				npc = target;
            	npc.GetGlobalNPC<MyNPC>().maceSlam = 25;
				npc.GetGlobalNPC<MyNPC>().maceSlamDamage = damage;
			}
		}
	}
}