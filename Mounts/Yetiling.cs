using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Emperia.Buffs;

namespace Emperia.Mounts
{
	public class Yetiling : ModMount
	{
		public override void SetStaticDefaults()
		{
			MountData.spawnDust = 51;
			MountData.buff = ModContent.BuffType<YetiMount>();
			MountData.heightBoost = 20;
			MountData.fallDamage = 0.5f;
			MountData.runSpeed = 6f;
			MountData.dashSpeed = 5f;
			MountData.flightTimeMax = 0;
			MountData.fatigueMax = 0;
			MountData.jumpHeight = 5;
			MountData.acceleration = 0.19f;
			MountData.jumpSpeed = 6f;
			MountData.blockExtraJumps = false;
			MountData.totalFrames = 6;
			MountData.constantJump = true;
			int[] array = new int[MountData.totalFrames];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = 20;
			}
			MountData.playerYOffsets = array;
			MountData.xOffset = 11;
			MountData.bodyFrame = 3;
			MountData.yOffset = 6;
			MountData.playerHeadOffset = 22;
			MountData.standingFrameCount = 4;
			MountData.standingFrameDelay = 12;
			MountData.standingFrameStart = 0;
			MountData.runningFrameCount = 4;
			MountData.runningFrameDelay = 12;
			MountData.runningFrameStart = 0;
			MountData.flyingFrameCount = 0;
			MountData.flyingFrameDelay = 0;
			MountData.flyingFrameStart = 0;
			MountData.inAirFrameCount = 1;
			MountData.inAirFrameDelay = 12;
			MountData.inAirFrameStart = 0;
			MountData.idleFrameCount = 4;
			MountData.idleFrameDelay = 12;
			MountData.idleFrameStart = 0;
			MountData.idleFrameLoop = true;
			MountData.swimFrameCount = MountData.inAirFrameCount;
			MountData.swimFrameDelay = MountData.inAirFrameDelay;
			MountData.swimFrameStart = MountData.inAirFrameStart;
			if (Main.netMode != 2)
			{
				MountData.textureWidth = MountData.backTexture.Width();
				MountData.textureHeight = MountData.backTexture.Height(); //dont know if this works lol
			}
		}

		public override void UpdateEffects(Player player)
		{
			if (Math.Abs(player.velocity.X) > 4f)
			{
				Rectangle rect = player.getRect();
				Dust.NewDust(new Vector2(rect.X, rect.Y), rect.Width, rect.Height, 51);
			}
		}
	}
}