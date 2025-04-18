﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Projectiles.Furina
{
	public class FurinaBubble3Proj : ModProjectile
	{
		public override string Texture => "BooTao2/Content/Projectiles/Furina/FurinaBubble2Proj";
		
		public override void SetStaticDefaults() {
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = false; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
		}

		public override void SetDefaults() {
			Projectile.width = 32;
			Projectile.height = 32;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 1f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 200;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			Player player = Main.player[Projectile.owner];
			if (player.statLife > (int)(player.statLifeMax2 * 0.5) + 5) {
				player.statLife -= 5;
				//player.GetDamage(DamageClass.Generic) -= player.GetModPlayer<BooTaoPlayer>().FurinaDmgBuff;
				player.GetModPlayer<BooTaoPlayer>().FurinaDmgBuff += 0.01f;
				//player.GetDamage(DamageClass.Generic) += player.GetModPlayer<BooTaoPlayer>().FurinaDmgBuff;
			}
		}

		// Custom AI
		public override void AI() {
			float maxDetectRadius = 500f; //
			float projSpeed = 10f; // 

			NPC closestNPC = FindClosestNPC(maxDetectRadius);
			if (closestNPC == null)
				return;

			float targetAngle = Projectile.AngleTo(closestNPC.Center);
			Projectile.velocity = Projectile.velocity.ToRotation().AngleTowards(targetAngle, MathHelper.ToRadians(5)).ToRotationVector2() * projSpeed;
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
	}
}
