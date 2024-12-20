using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;

namespace BooTao2.Content.Projectiles.Lancet2 {
	public class Lancet2Proj : ModProjectile {
		public override void SetDefaults() {
			Projectile.width = 12;
			Projectile.height = 12;
			Projectile.aiStyle = 0;
			Projectile.friendly = false;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.9f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 360;
			Projectile.penetrate = -1;
		}
		
		public override void AI() {
			if (Main.myPlayer != Projectile.owner)
				return;
			
			SearchForTargets(out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter, out int trackLife, out Player target);
			//Projectile.Intersects(player.Hitbox)
			if (foundTarget) {
				if (Vector2.Distance(target.Center, Projectile.Center) < 20f) {
					Projectile.Kill();
					target.Heal(Projectile.damage);
					return;
				}
				float targetAngle = Projectile.AngleTo(target.Center);
				Projectile.velocity = Projectile.velocity.ToRotation().AngleTowards(targetAngle, MathHelper.ToRadians(10)).ToRotationVector2() * 12f;
				//Projectile.rotation = Projectile.velocity.ToRotation();
			}
		}
		
		//public override void OnKill(int timeLeft) {
		//	SearchForTargets(out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter, out int trackLife, out Player target);
		//	target.Heal(Projectile.damage);
		//}
		
		private void SearchForTargets(out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter, out int trackLife, out Player target) {
			distanceFromTarget = 300f;
			targetCenter = Projectile.Center;
			foundTarget = false;
			trackLife = 0;
			target = Main.player[Projectile.owner];
			
			foreach (var player in Main.ActivePlayers) {
				float between = Vector2.Distance(player.Center, Projectile.Center);
				bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
				bool inRange = between < distanceFromTarget;
				bool inRange2 = between < 300f;
				if (((closest && inRange && player.statLife < trackLife) || !foundTarget) && inRange2) {
					distanceFromTarget = between;
					targetCenter = player.Center;
					foundTarget = true;
					trackLife = player.statLife;
					target = player;
				}
			}
		}
	}
}