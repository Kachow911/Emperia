using Emperia.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Audio.SoundEngine;

namespace Emperia.Items.Sets.PreHardmode.Granite   //where is located
{
    public class GraniteSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Granite Sword");
            // Tooltip.SetDefault("Hilt strikes have an increased critical hit chance\nCritical hits release explosions of energy");
        }
        public override void SetDefaults()
        {
            Item.damage = 23;
            Item.DamageType = DamageClass.Melee;
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 27;
            Item.useAnimation = 27;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4f;
            Item.value = 27000;
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = false;
            Item.UseSound = SoundID.Item1;
            //Item.crit = 6;
            Item.GetGlobalItem<HiltSystemItem>().hiltScale = 0.55f;

        }
        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers hit)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            HiltSystemItem hiltSystemItem = Item.GetGlobalItem<HiltSystemItem>();

            if (hiltSystemItem.IsHiltStrike(target))
            {
                if (Main.rand.Next(99) + 1 > (Item.crit + player.GetCritChance(DamageClass.Melee) + player.GetCritChance(DamageClass.Generic) + 15)) hit.DisableCrit(); //bad for mod compatability
                else hit.SetCrit();
            }
            if (modPlayer.graniteSet && modPlayer.graniteTime >= 900)
            {
                hit.CritDamage *= 1.875f;
            }
            else
            {
                hit.CritDamage *= 1.25f;
            }
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            HiltSystemItem hiltSystemItem = Item.GetGlobalItem<HiltSystemItem>();
            if (hiltSystemItem.IsHiltStrike(target))
            {
                MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
                if (hit.Crit && modPlayer.graniteSet && modPlayer.graniteTime >= 900) PlaySound(new SoundStyle("Emperia/Sounds/Custom/HeavyThud3") with { Volume = 1.35f, PitchVariance = 0.2f }, player.Center);
                else PlaySound(new SoundStyle("Emperia/Sounds/Custom/HeavyThud2") with { Volume = 1.0f, PitchVariance = 0.2f }, player.Center);
            }

            if (hit.Crit)
            {
                MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
                if (modPlayer.graniteSet && modPlayer.graniteTime >= 900)
                {
                    PlaySound(SoundID.Item14, target.position);
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        if (target.Distance(Main.npc[i].Center) < 126 && Main.npc[i] != target)
                            Main.npc[i].SimpleStrikeNPC(hit.Damage, 0);
                    }
                    for (int i = 0; i < 45; ++i)
                    {
                        int index2 = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, DustID.MagicMirror, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
                        Main.dust[index2].noGravity = true;
                        Main.dust[index2].velocity *= 5.5f;
                    }
                    modPlayer.graniteTime = 0;
                }
                else
                {
                    PlaySound(SoundID.Item10, target.position);
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        if (target.Distance(Main.npc[i].Center) < 90 && Main.npc[i] != target)
                            Main.npc[i].SimpleStrikeNPC(hit.Damage, 0);
                    }
                    for (int i = 0; i < 30; ++i)
                    {
                        int index2 = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, DustID.MagicMirror, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 1.5f);
                        Main.dust[index2].noGravity = true;
                        Main.dust[index2].velocity *= 3.75f;
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "GraniteBar", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
