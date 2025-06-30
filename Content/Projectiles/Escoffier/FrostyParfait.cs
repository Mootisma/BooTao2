using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BooTao2.Content.Buffs.Escoffier;

namespace BooTao2.Content.Projectiles.Escoffier
{
	public class FrostyParfait : ModProjectile
	{
		public override void SetStaticDefaults() {
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = false;
		}

		// https://github.com/tModLoader/tModLoader/wiki/Projectile-Class-Documentation
		public override void SetDefaults() {
			Projectile.width = 60;
			Projectile.height = 38;

			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 1f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 160;
		}

		// Custom AI
		public override void AI() {
			float maxDetectRadius = 650f;
			int dustnumber = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.BlueTorch);
			Main.dust[dustnumber].noGravity = true;
			
			// Trying to find NPC closest to the projectile
			NPC closestNPC = FindClosestNPC(maxDetectRadius);
			if (closestNPC == null)
				return;

			// We only rotate by 3 degrees an update to give it a smooth trajectory. Increase the rotation speed here to make tighter turns
			float length = Projectile.velocity.Length();
			float targetAngle = Projectile.AngleTo(closestNPC.Center);
			Projectile.velocity = Projectile.velocity.ToRotation().AngleTowards(targetAngle, MathHelper.ToRadians(4)).ToRotationVector2() * length;
			Projectile.rotation = Projectile.velocity.ToRotation();
			
			Dust.NewDust(Projectile.position, 0, 0, 33, 0, 0, 150, default, 1f);
		}

		public NPC FindClosestNPC(float maxDetectDistance) {
			NPC closestNPC = null;
			float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;
			foreach (var target in Main.ActiveNPCs) {
				if (target.CanBeChasedBy()) {
					float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);
					if (sqrDistanceToTarget < sqrMaxDetectDistance) {
						sqrMaxDetectDistance = sqrDistanceToTarget;
						closestNPC = target;
					}
				}
			}

			return closestNPC;
		}
		
		public override bool? CanCutTiles() {
			return false;
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			Player player = Main.player[Projectile.owner];
			target.AddBuff(ModContent.BuffType<EscoffierBuff>(), 480);//
		}
	}
}
