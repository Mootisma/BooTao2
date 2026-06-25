using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BooTao2.Systems;
using BooTao2.Content.Projectiles.Wisadel;
using Terraria.Audio;

namespace BooTao2.Content.Projectiles.Breeze
{
    public class BreezeProj : ModProjectile
    {
        SoundStyle BreezeHit = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Breeze/BreezeHit") {
			Volume = 0.8f,
			PitchVariance = 0.2f,
			MaxInstances = 6,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		public override void SetDefaults()
        {
            Projectile.width = 128;
            Projectile.height = 128;
            Projectile.timeLeft = 40;
            Projectile.alpha = 0;
			Projectile.hide = false;
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.light = 1f;
        }
        
		public override void AI()
        {
            Dust.NewDust(Projectile.Center, 0, 0, 10, Projectile.velocity.X / 10, Projectile.velocity.Y / 10, 190, default, 1f);
        }
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
			if (target.boss) {
				modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
					hitInfo.Damage = (int)(hitInfo.Damage * 0.8);
				};
			}
		}
		
		public override bool? CanCutTiles() {
			return false;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			Player player = Main.player[Projectile.owner];
			player.Heal((int)(damageDone / 20));
			SoundEngine.PlaySound(BreezeHit, Projectile.Center);
			// Projectile.velocity Vector2.Zero
			int cold = (target.boss) ? 1 : 4;
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<BreezeProj2>(), (int)(Projectile.damage * cold / 4), Projectile.knockBack, Projectile.owner);
		}
    }
}
