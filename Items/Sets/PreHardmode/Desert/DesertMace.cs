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
			// Tooltip.SetDefault("Slams airborne enemies downwards, damaging them if they hit the ground");
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

		public override void ModifyHitNPC (Player player, NPC target, ref NPC.HitModifiers modifiers)
		{
			if (target.velocity.Y != 0 && target.noTileCollide == false && target.knockBackResist >= 0 && !target.boss)// && player.itemAnimation <= 22 if the effect is too strong
			{
				modifiers.Knockback *= 0.7f;
				//target.velocity.Y += 12 * target.knockBackResist;
				if (target.velocity.Y < 0) target.velocity.Y -= (target.velocity.Y < -10) ? -5f : target.velocity.Y * 0.5f;
				target.velocity.Y += 6 + (3 * target.knockBackResist);
			}
		}
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
			target.GetGlobalNPC<MyNPC>().maceSlam = 25;
			target.GetGlobalNPC<MyNPC>().maceSlamDamage = hit.SourceDamage;
		}
    }
}