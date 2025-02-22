using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Projectiles.Qingque {
	public class QingqueExplosionProj : ModProjectile {
		public override string Texture => "BooTao2/Content/Projectiles/Fiammetta/FiammettaExplosionProj";
		
		public override void SetStaticDefaults() {
			Main.projFrames[Projectile.type] = 5;
		}
		
		public override void SetDefaults() {
			Projectile.width = 352;
			Projectile.height = 264;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.9f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 16;
			Projectile.penetrate = -1;
			
			Projectile.scale = 1f;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
		}
		
		public override void AI() {
			for (int d = 0; d < 10; d++) {
				Dust.NewDust(Projectile.Center, 3, 3, 114, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), 120, default, 1f);
				Dust.NewDustPerfect(Projectile.Center, 114, null, 120, default, 1f);
			}
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
/*

*/