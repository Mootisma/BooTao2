using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;
using BooTao2.Content.Buffs.Lancet2;

namespace BooTao2.Content.Projectiles.Lancet2 {
	public class Lancet2Minion : ModProjectile {
		
		SoundStyle Heal = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Lancet2/Heal") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 3,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle RogerThat = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Lancet2/RogerThat") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 3,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		public override void SetStaticDefaults() {
			// Sets the amount of frames this minion has on its spritesheet
			Main.projFrames[Projectile.type] = 55;
			// This is necessary for right-click targeting
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = false;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = false;
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = false;
		}
		
		public override void SetDefaults() {
			Projectile.width = 125;
			Projectile.height = 125;
			Projectile.aiStyle = 0;
			Projectile.friendly = false;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.9f;
			Projectile.tileCollide = true;
			Projectile.penetrate = -1;
			Projectile.minion = true;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.minionSlots = 0f;
			Projectile.penetrate = -1;
		}
		
		public override void OnSpawn(IEntitySource source) {
			SoundEngine.PlaySound(RogerThat, Projectile.Center);
			Projectile.frame = 23;
			foreach (var player in Main.ActivePlayers) {
				player.Heal(Projectile.damage);
			}
		}
		
		int counter = 0;
		bool Healing = false;
		public override void AI() {
			if (Main.myPlayer != Projectile.owner)
				return;
			if (!CheckActive(Main.player[Projectile.owner])) {
				return;
			}
			
			int frameSpeed = 3;
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= frameSpeed) {
				Projectile.frameCounter = 0;
				Projectile.frame++;
			}
			
			//
			
			counter++;
			if (counter >= 10 && (Projectile.velocity == Vector2.Zero) && !Healing) {
				SearchForTargets(out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter, out int trackLife);
				if (foundTarget) {
					Healing = true;
					Projectile.frame = 0;
					Projectile.frameCounter = 0;
				}
				counter = 0;
			}
			
			if (Healing) {
				if (Projectile.frame == 7 && Projectile.frameCounter == 0) {
					SearchForTargets(out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter, out int trackLife);
					if (foundTarget) {
						SoundEngine.PlaySound(Heal, Projectile.Center);
						Vector2 todokete = (targetCenter - Projectile.Center).SafeNormalize(Vector2.UnitX) * 12f;
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2(30, -20), todokete, ModContent.ProjectileType<Lancet2Proj>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1);
					}
				}
				if (Projectile.frame >= Main.projFrames[Projectile.type]) {
					SearchForTargets(out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter, out int trackLife);
					if (foundTarget) {
						Projectile.frame = 0;
						Projectile.frameCounter = 0;
					}
					else {
						Healing = false;
						Projectile.frame = 24;
						Projectile.frameCounter = 0;
					}
				}
			}
			
			
			if (Projectile.frame >= Main.projFrames[Projectile.type]) {
				Projectile.frame = 24;
			}
			
			Projectile.velocity.Y += 0.12f;
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity) {
			Projectile.velocity = Vector2.Zero;
			return false;
		}
		
		private bool CheckActive(Player owner) {
			if (owner.dead || !owner.active) {
				owner.ClearBuff(ModContent.BuffType<Lancet2Buff>());

				return false;
			}

			if (owner.HasBuff(ModContent.BuffType<Lancet2Buff>())) {
				Projectile.timeLeft = 2;
			}

			return true;
		}
		
		private void SearchForTargets(out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter, out int trackLife) {
			distanceFromTarget = 500f;
			targetCenter = Projectile.Center;
			foundTarget = false;
			trackLife = 0;
			
			foreach (var player in Main.ActivePlayers) {
				//dont heal players on a different team than the owner
				// || player.team == 0
				if (Main.player[Projectile.owner].team != player.team) {
					continue;
				}
				float between = Vector2.Distance(player.Center, Projectile.Center);
				bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
				bool inRange = between < distanceFromTarget;
				bool inRange2 = between < 500f;
				bool NotFull = player.statLife < player.statLifeMax2;
				if (((closest && inRange && player.statLife < trackLife) || !foundTarget) && inRange2 && NotFull) {
					distanceFromTarget = between;
					targetCenter = player.Center;
					foundTarget = true;
					trackLife = player.statLife;
				}
			}
		}
	}
}