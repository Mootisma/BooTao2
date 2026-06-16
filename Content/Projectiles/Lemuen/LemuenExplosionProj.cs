using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BooTao2.Systems;
using Terraria.Audio;
using Terraria.DataStructures;

namespace BooTao2.Content.Projectiles.Lemuen {
	public class LemuenExplosionProj : ModProjectile {
		public override string Texture => "BooTao2/Content/Projectiles/Fiammetta/FiammettaExplosionProj";
		
		SoundStyle LemuenSkillBoom = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Lemuen/LemuenSkillBoom") {
			Volume = 1f,
			PitchVariance = 0.3f,
			MaxInstances = 9,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		public override void SetStaticDefaults() {
			Main.projFrames[Projectile.type] = 5;
		}
		
		public override void SetDefaults() {
			Projectile.width = 352;
			Projectile.height = 264;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Ranged;
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
			
			float distancebtwn = Vector2.Distance(target.Center, Projectile.Center);
			if (distancebtwn < 100) {
				modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
					hitInfo.Damage = (int)(hitInfo.Damage * 1.5);
				};
			}
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
		
		public override void OnSpawn(IEntitySource source) {
			SoundEngine.PlaySound(LemuenSkillBoom, Projectile.Center);
		}
	}
}
/*

*/