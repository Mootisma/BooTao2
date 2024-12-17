using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Projectiles.Mostima {
	public class MostimaSkillProj : ModProjectile {
		public override void SetStaticDefaults() {
			Main.projFrames[Projectile.type] = 5;
		}
		
		public override void SetDefaults() {
			Projectile.width = 164;
			Projectile.height = 320;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.9f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 20;
			Projectile.penetrate = -1;
			
			Projectile.alpha = 0;
			Projectile.scale = 1f;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
		}
		
		public override void AI() {
			//position, width, height, type, speedx, speedy, alpha, color, scale
			for (int d = 0; d < 8; d++) {
				Dust.NewDust(Projectile.position, 300, 300, 68, 0, 0, 150, default, 1f);
				Dust.NewDust(Projectile.position, 300, 300, 69, 0, 0, 150, default, 1f);
				Dust.NewDust(Projectile.position, 300, 300, 70, 0, 0, 150, default, 1f);
				Dust.NewDust(Projectile.position, 300, 300, 88, 0, 0, 150, default, 1f);
				Dust.NewDust(Projectile.position, 300, 300, 156, 0, 0, 150, default, 1f);
			}
			Projectile.rotation = Projectile.velocity.ToRotation();
			
			int frameSpeed = 4;

			Projectile.frameCounter++;

			if (Projectile.frameCounter >= frameSpeed) {
				Projectile.frameCounter = 0;
				Projectile.frame++;
				Projectile.alpha += 10 * Projectile.frame;
			}
		}
	}
}