using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;
using BooTao2.Content.Buffs.Wisadel;

namespace BooTao2.Content.Projectiles.Wisadel {
	public class RevenantShadow : ModProjectile {
		//public override string Texture => "BooTao2/Content/Projectiles/Yelan/YelanMinionProj";
		
		SoundStyle Laser = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Wisadel/RevenantShadowLaser") {
			Volume = 1.6f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		public override void SetStaticDefaults() {
			// This is necessary for right-click targeting
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = false;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = false;
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = false;
		}
		
		public override void SetDefaults() {
			Projectile.width = 73;
			Projectile.height = 73;
			Projectile.aiStyle = 0;
			Projectile.friendly = false;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.9f;
			Projectile.tileCollide = true;
			Projectile.penetrate = -1;
			Projectile.minion = true;//Projectile.sentry = true
			Projectile.DamageType = DamageClass.Summon;
			Projectile.minionSlots = 0f;
		}
		
		int counter = 0;
		public override void AI() {
			Player wisadel = Main.player[Projectile.owner];
			
			if (!CheckActive(wisadel)) {
				return;
			}
			
			Projectile.velocity = Vector2.Zero;
			SearchForTargets(wisadel, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
			
			if (counter % 60 == 0) {
				float distancebtwn = Vector2.Distance(Projectile.Center, wisadel.Center);
				if (distancebtwn < 200) {
					wisadel.AddBuff(BuffID.Invisibility, 60, true);
				}
			}
			
			if (foundTarget && (counter >= 300) && (Main.myPlayer == Projectile.owner)) {
				Vector2 direction = targetCenter - Projectile.Center;
				direction.Normalize();
				direction *= 40;
				SoundEngine.PlaySound(Laser);
				counter = 60 * Main.rand.Next(2);
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, direction, ModContent.ProjectileType<RevenantShadowLaser>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1);
			}
			if (counter < 600) {
				counter++;
			}
			else {
				counter = 301;
			}
		}
		
		private bool CheckActive(Player owner) {
			if (owner.dead || !owner.active) {
				owner.ClearBuff(ModContent.BuffType<WisadelBuff>());

				return false;
			}

			if (owner.HasBuff(ModContent.BuffType<WisadelBuff>())) {
				Projectile.timeLeft = 2;
			}

			return true;
		}
		
		private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter) {
			// Starting search distance
			distanceFromTarget = 1000f;
			targetCenter = Projectile.position;
			foundTarget = false;

			// This code is required if your minion weapon has the targeting feature
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
				// This code is required either way, used for finding a target
				foreach (var npc in Main.ActiveNPCs) {
					if (npc.CanBeChasedBy()) {
						float between = Vector2.Distance(npc.Center, Projectile.Center);
						bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
						bool inRange = between < distanceFromTarget;
						bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
						// Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
						// The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
						bool closeThroughWall = between < 850f;

						if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall)) {
							distanceFromTarget = between;
							targetCenter = npc.Center;
							foundTarget = true;
						}
					}
				}
			}
		}
		
		// Here you can decide if your minion breaks things like grass or pots
		public override bool? CanCutTiles() {
			return false;
		}

		// This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
		public override bool MinionContactDamage() {
			return false;
		}
	}
}