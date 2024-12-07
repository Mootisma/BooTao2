using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Projectiles.Fiammetta {
	public class FiammettaExplosionProj2 : ModProjectile {
		public override void SetStaticDefaults() {
			Main.projFrames[Projectile.type] = 5;
		}
		
		public override void SetDefaults() {
			Projectile.width = 50;
			Projectile.height = 50;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.9f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 15;
			Projectile.penetrate = -1;
			
			Projectile.alpha = 0;
			Projectile.scale = 1f;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
			modifiers.ArmorPenetration += 10f;
		}
		
		public override void AI() {
			Dust.NewDust(Projectile.Center, 3, 3, 114, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), 150, default, 1f);
			
			int frameSpeed = 3;

			Projectile.frameCounter++;

			if (Projectile.frameCounter >= frameSpeed) {
				Projectile.frameCounter = 0;
				Projectile.frame++;
				Projectile.alpha += 10 * Projectile.frame;
				Projectile.light -= 0.1f * Projectile.frame;
			}
		}
	}
}