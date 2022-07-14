using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;
using static Terraria.Audio.SoundEngine;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace Emperia.Items.Weapons
{
    public class TricksterSword : ModItem
    {
		 public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Trickter's Blade");
			Tooltip.SetDefault("Swings a random sword");
		}
        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.DamageType = DamageClass.Melee;
            Item.width = 16;
            Item.height = 16;
            Item.useTime = 25;
            Item.useAnimation = 25;     
            Item.useStyle = 1;
            Item.knockBack = 3.5f;
            Item.value = 22000;
            Item.rare = 1;
			Item.scale = 1f;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
        }
        Item chosenSword;
        bool setNewSword;
        public override bool? UseItem(Player player)
        {
            //Main.NewText(player.itemAnimation);
            if (player.itemAnimation == 1) setNewSword = true;
            return false;
        }

        public override void HoldItem(Player player)
        {
            if (setNewSword)
            {
                for (int i = 0; i < 5000; i++)
                {
                    int randomItem = Main.rand.Next(5000);
                    Item sword = new Item();
                    sword.SetDefaults(randomItem);
                    if (!sword.noMelee && sword.CountsAsClass(DamageClass.Melee) && sword.useStyle == 1 && sword.pick == 0 && sword.hammer == 0 && sword.axe == 0)
                    {
                        chosenSword = sword;
                        break;
                    }
                }
                setNewSword = false;
            }
            if (player.itemAnimation == player.itemAnimationMax)
            {
                int p = Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<TricksterSwordProj>(), 0, 0, Main.myPlayer, 0, 0);
                (Main.projectile[p].ModProjectile as TricksterSwordProj).useAnimationMax = Item.useAnimation;
                (Main.projectile[p].ModProjectile as TricksterSwordProj).sword = chosenSword;
                Main.projectile[p].timeLeft = Item.useAnimation;
                Main.projectile[p].scale = chosenSword.scale;
            }
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (chosenSword != null) damage *= (chosenSword.damage / Item.damage);
        }
        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            if (chosenSword == null) return;
            Rectangle drawHitbox = Item.GetDrawHitbox(chosenSword.type, player);
            Main.NewText(drawHitbox);
            float adjustedItemScale = player.GetAdjustedItemScale(chosenSword);

            Rectangle itemRectangle = new Rectangle((int)player.itemLocation.X, (int)player.itemLocation.Y, 32, 32);
            //if (!Main.dedServ)
            {
                int num = drawHitbox.Width;
                int num2 = drawHitbox.Height;
                switch (chosenSword.type)
                {
                    case 5094:
                        num -= 10;
                        num2 -= 10;
                        break;
                    case 5095:
                        num -= 10;
                        num2 -= 10;
                        break;
                    case 5096:
                        num -= 12;
                        num2 -= 12;
                        break;
                    case 5097:
                        num -= 8;
                        num2 -= 8;
                        break;
                }
                itemRectangle = new Rectangle((int)player.itemLocation.X, (int)player.itemLocation.Y, num, num2);
            }


            itemRectangle.Width = (int)(itemRectangle.Width * adjustedItemScale);
            itemRectangle.Height = (int)(itemRectangle.Height * adjustedItemScale);
            if (player.direction == -1)
            {
                itemRectangle.X -= itemRectangle.Width;
            }
            if (player.gravDir == 1f)
            {
                itemRectangle.Y -= itemRectangle.Height;
            }
            if (chosenSword.useStyle == 1)
            {
                if (player.itemAnimation < player.itemAnimationMax * 0.333)
                {
                    if (player.direction == -1)
                    {
                        itemRectangle.X -= (int)(itemRectangle.Width * 1.4 - itemRectangle.Width);
                    }
                    itemRectangle.Width = (int)(itemRectangle.Width * 1.4);
                    itemRectangle.Y += (int)(itemRectangle.Height * 0.5 * player.gravDir);
                    itemRectangle.Height = (int)(itemRectangle.Height * 1.1);
                }
                else if (!(player.itemAnimation < player.itemAnimationMax * 0.666))
                {
                    if (player.direction == 1)
                    {
                        itemRectangle.X -= (int)(itemRectangle.Width * 1.2);
                    }
                    itemRectangle.Width *= 2;
                    itemRectangle.Y -= (int)((itemRectangle.Height * 1.4 - itemRectangle.Height) * player.gravDir);
                    itemRectangle.Height = (int)(itemRectangle.Height * 1.4);
                }
            }
            hitbox = itemRectangle;
        }
    }
    public class TricksterSwordProj : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.damage = 0;
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 60;
        }
        public float useAnimationMax = 0;
        int meleeFrame = 0;
        static Vector2[] handPosition = { new Vector2(-7, -10), new Vector2(2, -11), new Vector2(2, 3) };
        public Item sword;


        public override void OnSpawn(IEntitySource source)
        {
            DrawOriginOffsetX = -12 * Main.player[Projectile.owner].direction;
            DrawOriginOffsetY = -30; //* (int)Main.player[Projectile.owner].gravDir;
            if (Main.player[Projectile.owner].direction == -1) DrawOffsetX = -26;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            //if (!player.ItemAnimationActive) Projectile.Kill();
            player.heldProj = Projectile.whoAmI;

            switch (player.bodyFrame.Y / player.bodyFrame.Height)
            {
                case 1: meleeFrame = 0; break;
                case 2: meleeFrame = 1; break;
                case 3: meleeFrame = 2; break;
                default: meleeFrame = 0; break;
            }
            //Projectile.Center = player.MountedCenter + new Vector2(((Vector2)handPosition.GetValue(meleeFrame)).X * player.direction, ((Vector2)handPosition.GetValue(meleeFrame)).Y * player.gravDir);
            Projectile.Center = player.itemLocation;
            Texture2D texture = ModContent.Request<Texture2D>("Terraria/Images/Item_" + sword.type).Value;
            Projectile.Center += new Vector2(texture.Width * 0.5f * player.direction * (sword.scale - 1f), texture.Height * 0.5f * player.gravDir * (sword.scale - 1f));
            Projectile.rotation = MathHelper.ToRadians(((Projectile.timeLeft - useAnimationMax / 2) / useAnimationMax * 198f) + 15) * -player.direction * player.gravDir; //rotation cannot be used in place of spriteeffects
            Projectile.rotation += player.fullRotation;
            //code beneath this adapted from vanilla medusa head projectile

            //if (velocity.X != base.velocity.X || velocity.Y != base.velocity.Y)
            //{
            //	this.netUpdate = true;
            //}

            Projectile.velocity = player.GetModPlayer<MyPlayer>().MouseDirection();

            Vector2 rotationOffset = new Vector2(-11.5f, -11.5f);
            Projectile.Center = ((Projectile.Center - player.position) + rotationOffset).RotatedBy(player.fullRotation) + player.position - rotationOffset;
            if (player.sleeping.isSleeping)
            {
                Vector2 posOffset;
                player.sleeping.GetSleepingOffsetInfo(player, out posOffset);
                Projectile.Center += posOffset * 2.4f;
                Projectile.Center += new Vector2(0, 10 + (-2 * player.direction));
            }
            Projectile.Center = (Projectile.Center - player.GetModPlayer<MyPlayer>().MouseDirection()).Floor();
            Projectile.gfxOffY = player.gfxOffY;
            Projectile.spriteDirection = player.direction;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            SpriteEffects direction = SpriteEffects.None;
            if (player.direction != player.gravDir) direction = SpriteEffects.FlipHorizontally; //more compact way of checking player direction and gravity direction at once
            if (player.gravDir == -1) direction = 1 - direction | SpriteEffects.FlipVertically; //flips both horizontally and vertically if upside down

            Texture2D texture = ModContent.Request<Texture2D>("Terraria/Images/Item_" + sword.type).Value;
            Vector2 position = Projectile.position + new Vector2(texture.Width * 0.5f * player.direction, -texture.Height * 0.5f * player.gravDir).RotatedBy(Projectile.rotation) - Main.screenPosition; //not sure why 2f
            Main.EntitySpriteDraw(texture, position, null, lightColor, Projectile.rotation, texture.Size() * 0.5f, Projectile.scale, direction, 1);
            return true;
        }
    }
}
