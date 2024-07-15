using BooTao2.Content.Items.Furina;
using BooTao2.Content.Buffs.Furina;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace BooTao2.Content.Projectiles.Furina
{
	public class FurinaMinionProj : ModProjectile
	{
		public override void SetStaticDefaults() {
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = false;
		}

		public sealed override void SetDefaults() {
			Projectile.width = 50;
			Projectile.height = 50;
			Projectile.tileCollide = false;
			// These below are needed for a minion weapon
			Projectile.friendly = true; // 
			Projectile.minion = true; // Declares this as a minion
			Projectile.DamageType = DamageClass.Summon; // Declares the damage type
			Projectile.minionSlots = 1f; // Amount of slots
			Projectile.penetrate = -1; // Needed so the minion doesn't despawn
		}
		
		// Here you can decide if your minion breaks things like grass or pots
		public override bool? CanCutTiles() {
			return false;
		}

		// This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
		int counter = 0;
		int bufftimer = 0;
		public override bool MinionContactDamage() {
			return (counter >= 269);
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			Player player = Main.LocalPlayer;
			if (player.statLife > (int)(player.statLifeMax2 * 0.5) + 15) {
				player.statLife -= 15;
				//player.GetDamage(DamageClass.Generic) -= player.GetModPlayer<BooTaoPlayer>().FurinaDmgBuff;
				player.GetModPlayer<BooTaoPlayer>().FurinaDmgBuff += 0.03f;
				//player.GetDamage(DamageClass.Generic) += player.GetModPlayer<BooTaoPlayer>().FurinaDmgBuff;
			}
		}

		// The AI of this minion is split into multiple methods to avoid bloat. This method just passes values between calls actual parts of the AI.
		public override void AI() {
			Player owner = Main.player[Projectile.owner];

			if (!CheckActive(owner)) {
				return;
			}

			GeneralBehavior(owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
			SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
			Movement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition, out Vector2 vel);
			Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.78f);
			
			counter++;
			bufftimer++;
			owner.GetDamage(DamageClass.Generic) += owner.GetModPlayer<BooTaoPlayer>().FurinaDmgBuff;
			if (bufftimer > 1200) {
				//owner.GetDamage(DamageClass.Generic) -= owner.GetModPlayer<BooTaoPlayer>().FurinaDmgBuff;
				owner.GetModPlayer<BooTaoPlayer>().FurinaDmgBuff = 0f;
				bufftimer = 0;
			}
			if (!foundTarget) {
				counter = 0;
				bufftimer = 0;
				//owner.GetDamage(DamageClass.Generic) -= owner.GetModPlayer<BooTaoPlayer>().FurinaDmgBuff;
				owner.GetModPlayer<BooTaoPlayer>().FurinaDmgBuff = 0f;
			}
		}

		// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
		private bool CheckActive(Player owner) {
			if (owner.dead || !owner.active) {
				owner.ClearBuff(ModContent.BuffType<FurinaMinionBuff>());

				return false;
			}

			if (owner.HasBuff(ModContent.BuffType<FurinaMinionBuff>())) {
				Projectile.timeLeft = 2;
			}

			return true;
		}

		private void GeneralBehavior(Player owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition) {
			Vector2 idlePosition = owner.Center;
			idlePosition.Y -= 48f; // Go up 48 coordinates (three tiles from the center of the player)

			float minionPositionOffsetX = (10 + Projectile.minionPos * 40) * -owner.direction;
			idlePosition.X += minionPositionOffsetX; // Go behind the player

			vectorToIdlePosition = idlePosition - Projectile.Center;
			distanceToIdlePosition = vectorToIdlePosition.Length();

			if (Main.myPlayer == owner.whoAmI && distanceToIdlePosition > 2000f) {
				Projectile.position = idlePosition;
				Projectile.velocity *= 0.1f;
				Projectile.netUpdate = true;
			}

			float overlapVelocity = 0.04f;

			foreach (var other in Main.ActiveProjectiles) {
				if (other.whoAmI != Projectile.whoAmI && other.owner == Projectile.owner && Math.Abs(Projectile.position.X - other.position.X) + Math.Abs(Projectile.position.Y - other.position.Y) < Projectile.width) {
					if (Projectile.position.X < other.position.X) {
						Projectile.velocity.X -= overlapVelocity;
					}
					else {
						Projectile.velocity.X += overlapVelocity;
					}

					if (Projectile.position.Y < other.position.Y) {
						Projectile.velocity.Y -= overlapVelocity;
					}
					else {
						Projectile.velocity.Y += overlapVelocity;
					}
				}
			}
		}

		private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter) {
			distanceFromTarget = 1000f;
			targetCenter = Projectile.position;
			foundTarget = false;

			if (owner.HasMinionAttackTargetNPC) {
				NPC npc = Main.npc[owner.MinionAttackTargetNPC];
				float between = Vector2.Distance(npc.Center, Projectile.Center);

				// Reasonable distance away so it doesn't target across multiple screens
				if (between < 1700f) {
					distanceFromTarget = between;
					targetCenter = npc.Center;
					foundTarget = true;
				}
			}

			if (!foundTarget) {
				foreach (var npc in Main.ActiveNPCs) {
					if (npc.CanBeChasedBy()) {
						float between = Vector2.Distance(npc.Center, Projectile.Center);
						bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
						bool inRange = between < distanceFromTarget;
						bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
						bool closeThroughWall = between < 850f;

						if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall)) {
							distanceFromTarget = between;
							targetCenter = npc.Center;
							foundTarget = true;
						}
					}
				}
			}
			Projectile.friendly = foundTarget;
		}

		private void Movement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition, out Vector2 vel) {
			float speed = 9f;
			float inertia = 60f;
			Vector2 direction = Projectile.Center;

			if (foundTarget) {
				// Minion has a target: attack (here, fly towards the enemy)
				if (distanceFromTarget > 300f) {
					// towards
					direction = targetCenter - Projectile.Center;
				}
				else {
					//float rotation = MathHelper.ToRadians(45);
					if (counter < 270 && (Main.myPlayer == Projectile.owner)) {
						direction = Projectile.Center - targetCenter;//away
					}
					if (counter >= 270 && (Main.myPlayer == Projectile.owner)) {
						speed = 16f;
						inertia = 30f;
						direction = targetCenter - Projectile.Center;//towards
					}
					if (counter >= 330 && (Main.myPlayer == Projectile.owner)) {
						direction = Projectile.Center - targetCenter;//away
						counter = 0;
					}
					//direction = direction.RotatedBy(MathHelper.Lerp(-rotation, rotation, 0.1f));
				}
				direction.Normalize();
				direction *= speed;
				Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
				//float desiredRotation = direction.ToRotation();
				//Projectile.rotation = Projectile.rotation.AngleTowards(desiredRotation, 0.02f);
			}
			else {
				// Minion doesn't have a target: return to player and idle
				if (distanceToIdlePosition > 600f) {
					// Speed up the minion if it's away from the player
					speed = 20f;
					inertia = 60f;
				}
				else {
					// Slow down the minion if closer to the player
					speed = 4f;
					inertia = 80f;
				}

				if (distanceToIdlePosition > 20f) {
					vectorToIdlePosition.Normalize();
					vectorToIdlePosition *= speed;
					Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
				}
				else if (Projectile.velocity == Vector2.Zero) {
					// If there is a case where it's not moving at all, give it a little "poke"
					Projectile.velocity.X = -0.15f;
					Projectile.velocity.Y = -0.05f;
				}
				//Projectile.rotation = Projectile.velocity.X * 0.05f;
			}
			Projectile.rotation = Projectile.velocity.X * 0.05f;
			vel = Projectile.velocity;
		}
	}
}
