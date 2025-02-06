using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Projectiles.Kroos
{
	public class KroosProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// If this arrow would have strong effects (like Holy Arrow pierce), we can make it fire fewer projectiles from Daedalus Stormbow for game balance considerations like this:
			//ProjectileID.Sets.FiresFewerFromDaedalusStormbow[Type] = true;
		}
		
		SoundStyle KroosAttackHit = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Kroos/KroosAttackHit") {
			Volume = 0.6f,
			PitchVariance = 0.3f,
			MaxInstances = 6,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		public ref float SPLockout => ref Projectile.ai[0];

		public override void SetDefaults() {
			Projectile.width = 10; // The width of projectile hitbox
			Projectile.height = 10; // The height of projectile hitbox

			Projectile.arrow = true;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.timeLeft = 300;
			
			Projectile.ignoreWater = true;
			Projectile.light = 0.1f;
		}

		public override void AI() {
			// The code below was adapted from the ProjAIStyleID.Arrow behavior. Rather than copy an existing aiStyle using Projectile.aiStyle and AIType,
			// like some examples do, this example has custom AI code that is better suited for modifying directly.
			// See https://github.com/tModLoader/tModLoader/wiki/Basic-Projectile#what-is-ai for more information on custom projectile AI.

			// The projectile is rotated to face the direction of travel
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 + MathHelper.Pi;
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			Player player = Main.player[Projectile.owner];
			if (SPLockout < 1f) {
				player.GetModPlayer<BooTaoPlayer>().KroosSP++;
			}
			SoundEngine.PlaySound(KroosAttackHit, Projectile.Center);
		}
	}
}

/*
https://github.com/tModLoader/tModLoader/blob/1.4.4/ExampleMod/Content/Projectiles/ExampleArrowProjectile.png
*/