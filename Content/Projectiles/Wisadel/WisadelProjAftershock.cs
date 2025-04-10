using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;

namespace BooTao2.Content.Projectiles.Wisadel {
	public class WisadelProjAftershock : ModProjectile {
		//public override string Texture => "BooTao2/Content/Projectiles/Fiammetta/FiammettaExplosionProj2";
		
		public ref float Behavior => ref Projectile.ai[0];
		public ref float SkillActive => ref Projectile.ai[1];
		
		SoundStyle WisadelAftershock = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Wisadel/WisadelAftershock") {
			Volume = 1.6f,
			PitchVariance = 0f,
			MaxInstances = 6,
		};
		
		SoundStyle WisadelAftershockTwo = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Wisadel/WisadelAftershockTwo") {
			Volume = 1.6f,
			PitchVariance = 0f,
			MaxInstances = 6,
		};
		
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
			//modifiers.ArmorPenetration += 10f;
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
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			if ((Main.rand.NextBool(6) && Behavior <= 1) || SkillActive >= 1) {// 1 in 6
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<WisadelProjAftershock>(), (int)(Projectile.damage * 3), Projectile.knockBack, Projectile.owner, 4, 0);
			}
			if (Behavior < 2) {
				SoundEngine.PlaySound(WisadelAftershock, Projectile.Center);
			}
			else {
				SoundEngine.PlaySound(WisadelAftershockTwo, Projectile.Center);
			}
			for (int d = 0; d < 5; d++) {
				Dust.NewDust(target.position, 0, 0, 32, 0, 0, 150, default, 1f);
			}
		}
	}
}