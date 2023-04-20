using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
    public class ItemSwingVisual : ModProjectile
    {
        public static ItemSwingVisual NewItemSwingVisual(Player player, Item item, string texturePath, OverlayType overlayType = null)
        {
            ItemSwingVisual newSwing = (Main.projectile[Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<ItemSwingVisual>(), 0, 0, Main.myPlayer, 0, 0)].ModProjectile as ItemSwingVisual);
            newSwing.useAnimationMax = newSwing.Projectile.timeLeft = item.useAnimation;
            newSwing.overlayType = overlayType;
            newSwing.texture = ModContent.Request<Texture2D>(texturePath, AssetRequestMode.ImmediateLoad).Value;
            return newSwing;
        }

        public override void SetDefaults()
        {
            Projectile.damage = 0;
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 60;
        }
        public float useAnimationMax = 0;
        public Texture2D texture = null;
        //Texture2D overlayTexture = null;
        public OverlayType overlayType = null;
        public override void AI()
        {
            DrawOriginOffsetY = -texture.Height;

            Player player = Main.player[Projectile.owner];
            if (!player.ItemAnimationActive) Projectile.Kill();
            player.heldProj = Projectile.whoAmI;

            Projectile.Center = player.itemLocation;
            Projectile.rotation = MathHelper.ToRadians(((Projectile.timeLeft - useAnimationMax / 2) / useAnimationMax * 198f) + 15) * -player.direction * player.gravDir; //rotation cannot be used in place of spriteeffects
            Projectile.rotation += player.fullRotation;
            //code beneath this adapted from vanilla medusa head projectile

            //if (velocity.X != base.velocity.X || velocity.Y != base.velocity.Y)
            //{
            //	this.netUpdate = true;
            //}

            Projectile.velocity = player.GetModPlayer<MyPlayer>().MouseDirection();

            Vector2 rotationOffset = new Vector2(-11.5f, -texture.Height);//experimentally setting Y to texture height
            if (player.sleeping.isSleeping) rotationOffset.Y = -11.5f;
            Projectile.Center = ((Projectile.Center - player.position) + rotationOffset).RotatedBy(player.fullRotation) + player.position - rotationOffset;
            if (player.sleeping.isSleeping)
            {
                Vector2 posOffset;
                player.sleeping.GetSleepingOffsetInfo(player, out posOffset);
                Projectile.Center += posOffset * 2.4f;
                Projectile.Center += new Vector2(0, 10 + (-2 * player.direction));
            }
            Projectile.Center = (Projectile.Center - player.GetModPlayer<MyPlayer>().MouseDirection()).Floor();
            Projectile.position.Y += Projectile.gfxOffY = player.gfxOffY;
            Projectile.spriteDirection = player.direction;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            SpriteEffects direction = SpriteEffects.None;
            if (player.direction != player.gravDir) direction = SpriteEffects.FlipHorizontally; //more compact way of checking player direction and gravity direction at once
            if (player.gravDir == -1) direction = 1 - direction | SpriteEffects.FlipVertically; //flips both horizontally and vertically if upside down

            Vector2 position = Projectile.position + new Vector2(texture.Width * 0.5f * player.direction, -texture.Height * 0.5f * player.gravDir).RotatedBy(Projectile.rotation) - Main.screenPosition; //not sure why 2f
            Main.EntitySpriteDraw(texture, position, null, lightColor, Projectile.rotation, texture.Size() * 0.5f, Projectile.scale, direction, 1);

            //if (drawOverlay != null) drawOverlay(ref lightColor, position, direction);
            if (overlayType != null) DrawOverlay(overlayType, ref lightColor, position, null, Color.White, Projectile.rotation, texture.Size() * 0.5f, Projectile.scale, direction);
            return true;
        }

        public delegate void OverlayType(Player player, ref Color lightColor, ref Texture2D overlayTexture, ref Vector2 position, ref Rectangle? rectangle, ref Color color, ref float rotation, ref Vector2 origin, ref float scale, ref SpriteEffects direction);
        public void DrawOverlay(OverlayType overlayType, ref Color lightColor, Vector2 position, Rectangle? rectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects direction)
        {
            Texture2D overlayTexture = texture;
            overlayType(Main.player[Projectile.owner], ref lightColor, ref overlayTexture, ref position, ref rectangle, ref color, ref rotation, ref origin, ref scale, ref direction);
            Main.EntitySpriteDraw(overlayTexture, position, rectangle, color, rotation, origin, scale, direction, 1);
        }
    }
}