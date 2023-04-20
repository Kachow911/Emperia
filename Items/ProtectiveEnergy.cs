using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Buffs;

namespace Emperia.Items
{
	public class ProtectiveEnergy : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Metallic Energy");
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 4));
			ItemID.Sets.ItemIconPulse[Item.type] = true;
			ItemID.Sets.ItemNoGravity[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.rare = ItemRarityID.White;
		}
		public override bool OnPickup(Player player)
		{
			player.AddBuff(ModContent.BuffType<ProtectiveBoost>(), 960);
			Item.active = false;
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Grab, player.position);
			return false;
		}
		public override void GrabRange(Player player, ref int grabRange)
		{
			grabRange = (int)(grabRange * 1f);
			base.GrabRange(player, ref grabRange);
			
		}
		public override void PostUpdate()
        {

                Lighting.AddLight((int)((Item.position.X + Item.width / 2) / 16f), (int)((Item.position.Y + Item.height / 2) / 16f), 0.6f, 0.6f, 0.6f);
        }
		/*public override void PostDrawInWorld(ref Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
		Texture2D texture = Mod.Assets.Request<Texture2D>("Items/ProtectiveEnergy").Value;
		   	Main.EntitySpriteDraw
		   	(
		   		texture,
		   		new Vector2
		   		(
		   			Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
		   			Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
		   		),
		   		new Rectangle(0, 0, texture.Width, texture.Height),
		   		Color.White,
		   		rotation,
		   		texture.Size() * 0.5f,
		   		scale, 
		   		SpriteEffects.None, 
		   		0 
		   	);
		}*/
	}
}