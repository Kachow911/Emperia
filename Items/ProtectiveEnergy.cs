using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items
{
	public class ProtectiveEnergy : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Metallic Energy");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 4));
			ItemID.Sets.ItemIconPulse[item.type] = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.rare = 0;
		}
		public override bool OnPickup(Player player)
		{
			player.AddBuff(mod.BuffType("ProtectiveBoost"), 960);
			item.active = false;
			Main.PlaySound(SoundID.Grab, (int)player.position.X, (int)player.position.Y, 10);
			return false;
		}
		public override void GrabRange(Player player, ref int grabRange)
		{
			grabRange = (int)(grabRange * 1f);
			base.GrabRange(player, ref grabRange);
			
		}
		public override void PostUpdate()
        {

                Lighting.AddLight((int)((item.position.X + item.width / 2) / 16f), (int)((item.position.Y + item.height / 2) / 16f), 0.6f, 0.6f, 0.6f);
        }
		/*public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
		Texture2D texture = mod.GetTexture("Items/ProtectiveEnergy");
		   	spriteBatch.Draw
		   	(
		   		texture,
		   		new Vector2
		   		(
		   			item.position.X - Main.screenPosition.X + item.width * 0.5f,
		   			item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
		   		),
		   		new Rectangle(0, 0, texture.Width, texture.Height),
		   		Color.White,
		   		rotation,
		   		texture.Size() * 0.5f,
		   		scale, 
		   		SpriteEffects.None, 
		   		0f 
		   	);
		}*/
	}
}