using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Emperia.Systems
{
    public class HiltSystem : ModSystem
    {
        public static Rectangle GetHiltHitbox(Player player, Item item, Rectangle hitbox, float hiltScale)
        {
            Rectangle hiltHitbox = new();
            int hiltSize = (int)(Item.GetDrawHitbox(item.type, player).Width * player.GetAdjustedItemScale(item) * hiltScale);

            int currentHitbox = 0;
            if (player.itemAnimation < player.itemAnimationMax * 0.666) currentHitbox = 1;
            if (player.itemAnimation < player.itemAnimationMax * 0.333) currentHitbox = 2; //referenced from vanilla source code. should match vanilla logic frame perfect

            switch (currentHitbox) //melee weapons cycle through 3 hitboxes. this makes the hilt hitbox adjust accordingly and match with where the hilt is visually
            {
                case 0:
                    hiltHitbox.Height = hiltSize;
                    hiltHitbox.Width = hiltSize * 2;
                    hiltHitbox.X = (int)player.Center.X + 2 * player.direction - hiltSize;
                    hiltHitbox.Y = player.gravDir == 1 ? hitbox.Bottom - hiltSize : hitbox.Top;
                    break;
                case 1:
                    hiltHitbox.Height = hiltSize;
                    hiltHitbox.Width = hiltSize;
                    hiltHitbox.X = player.direction == 1 ? hitbox.Left : hitbox.Right - hiltSize;
                    hiltHitbox.Y = player.gravDir == 1 ? hitbox.Bottom - hiltSize : hitbox.Top;
                    break;
                case 2:
                    hiltHitbox.Height = hitbox.Height;
                    hiltHitbox.Width = hiltSize;
                    hiltHitbox.X = player.direction == 1 ? hitbox.Left : hitbox.Right - hiltSize;
                    hiltHitbox.Y = hitbox.Top;
                    break;
            }
            return hiltHitbox;
        }
        /*public static Rectangle GetMeleeHitbox(Player player, Item item)
        {
			Rectangle baseHitbox = Item.GetDrawHitbox(item.type, player);

			Rectangle hitbox = new Rectangle((int)player.itemLocation.X, (int)player.itemLocation.Y, 32, 32);
			if (!Main.dedServ)
			{
				int num = baseHitbox.Width;
				int num2 = baseHitbox.Height;
				hitbox = new Rectangle((int)player.itemLocation.X, (int)player.itemLocation.Y, num, num2);
			}
			float adjustedItemScale = player.GetAdjustedItemScale(item);
			hitbox.Width = (int)(hitbox.Width * adjustedItemScale);
			hitbox.Height = (int)(hitbox.Height * adjustedItemScale);
			if (player.direction == -1)
			{
				hitbox.X -= hitbox.Width;
			}
			if (player.gravDir == 1f)
			{
				hitbox.Y -= hitbox.Height;
			}
			if (player.itemAnimation < player.itemAnimationMax * 0.333)
			{
				if (player.direction == -1)
				{
					hitbox.X -= (int)(hitbox.Width * 1.4 - hitbox.Width);
				}
				hitbox.Width = (int)(hitbox.Width * 1.4);
				hitbox.Y += (int)(hitbox.Height * 0.5 * player.gravDir);
				hitbox.Height = (int)(hitbox.Height * 1.1);
			}
			else if (!(player.itemAnimation < player.itemAnimationMax * 0.666))
			{
				if (player.direction == 1)
				{
					hitbox.X -= (int)(hitbox.Width * 1.2);
				}
				hitbox.Width *= 2;
				hitbox.Y -= (int)((hitbox.Height * 1.4 - hitbox.Height) * player.gravDir);
				hitbox.Height = (int)(hitbox.Height * 1.4);
			}

			return hitbox;
		}*/
    }
    public class HiltSystemItem : GlobalItem
    {
        public override bool InstancePerEntity => true;

        Rectangle hiltHitbox = Rectangle.Empty;
        public Rectangle HiltHitbox => hiltHitbox;

        public float hiltScale = 0f;
        public bool HasHilt => hiltScale > 0f;

        public override void UseItemHitbox(Item item, Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            if (!HasHilt) return;
            hiltHitbox = HiltSystem.GetHiltHitbox(player, item, hitbox, hiltScale);
            //EmperiaSystem.drawRectangles.Add((hitbox, Color.Blue * 0.35f));
            //EmperiaSystem.drawRectangles.Add((hiltHitbox, Color.Red * 0.5f));
        }
        public bool IsHiltStrike(NPC target)
        {
            return HasHilt && target.getRect().Intersects(HiltHitbox);
        }
    }
}
