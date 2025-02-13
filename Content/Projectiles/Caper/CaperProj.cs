using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Projectiles.Caper
{
	public class CaperProj : ModProjectile
	{
		public ref float SPLockout => ref Projectile.ai[0];
		public ref float AIM_X => ref Projectile.ai[1];
		public ref float AIM_Y => ref Projectile.ai[2];
		public int State = 0;// 0 = outgoing, 1 = incoming
		// state will start in 0, and will change to 1 if colliding with enemy or reaching cursor aim

		public override void SetDefaults() {
			Projectile.width = 32; // The width of projectile hitbox
			Projectile.height = 32; // The height of projectile hitbox

			Projectile.arrow = true;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.timeLeft = 800;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.1f;
			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 20;// measured in ticks
		}

		public override void AI() {
			Vector2 aim = new Vector2(AIM_X, AIM_Y);
			
			// if outgoing and reaches cursor , change to incoming
			if (State == 0 && Vector2.Distance(aim, Projectile.Center) < 16f) {
				State = 1;
			}
			
			// incoming state
			int numbb = 10 + (int)(10 * (800 - Projectile.timeLeft) / 60);
			if (State == 1) {
				float targetAngle = Projectile.AngleTo(Main.player[Projectile.owner].Center);
				Projectile.velocity = Projectile.velocity.ToRotation().AngleTowards(targetAngle, MathHelper.ToRadians(numbb)).ToRotationVector2() * 15f;
				Projectile.netUpdate = true;
			}
			
			// proj dies when reaching the proj owner
			if (State == 1 && Vector2.Distance(Main.player[Projectile.owner].Center, Projectile.Center) < 40f) {
				Projectile.Kill();
			}
			//Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 + MathHelper.Pi;
			Projectile.rotation = Projectile.rotation + MathHelper.ToRadians(10);
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			Player player = Main.player[Projectile.owner];
			if (SPLockout < 1f) {
				player.GetModPlayer<BooTaoPlayer>().CaperSP++;
			}
			if (State == 0) {
				State = 1;
				//Projectile.velocity *= -1;
				float targetAngle = Projectile.AngleTo(Main.player[Projectile.owner].Center);
				Projectile.velocity = Projectile.velocity.ToRotation().AngleTowards(targetAngle, MathHelper.ToRadians(135)).ToRotationVector2() * 15f;
			}
			//SoundEngine.PlaySound(KroosAttackHit, Projectile.Center);
		}
	}
}

/*
https://github.com/tModLoader/tModLoader/blob/1.4.4/ExampleMod/Content/Projectiles/ExampleArrowProjectile.png
*/