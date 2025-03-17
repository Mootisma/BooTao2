using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Projectiles.Platinum
{
	public class PlatinumProj : ModProjectile
	{
		public override string Texture => "BooTao2/Content/Projectiles/Kroos/KroosProj";
		
		SoundStyle KroosAttackHit = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Kroos/KroosAttackHit") {
			Volume = 0.6f,
			PitchVariance = 0.3f,
			MaxInstances = 6,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		public ref float FlyFarther => ref Projectile.ai[0];

		public override void SetDefaults() {
			Projectile.width = 10; // The width of projectile hitbox
			Projectile.height = 10; // The height of projectile hitbox

			Projectile.arrow = true;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.timeLeft = 120 + (int)(100 * FlyFarther);
			
			Projectile.ignoreWater = true;
			Projectile.light = 0.1f;
		}

		public override void AI() {
			// The projectile is rotated to face the direction of travel
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 + MathHelper.Pi;
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			SoundEngine.PlaySound(KroosAttackHit, Projectile.Center);
		}
	}
}

/*
https://github.com/tModLoader/tModLoader/blob/1.4.4/ExampleMod/Content/Projectiles/ExampleArrowProjectile.png
*/