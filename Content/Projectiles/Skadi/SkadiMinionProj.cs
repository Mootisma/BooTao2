using BooTao2.Content.Items.Skadi;
using BooTao2.Content.Buffs.Skadi;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace BooTao2.Content.Projectiles.Skadi
{
	public class SkadiMinionProj : ModProjectile
	{
		SoundStyle Skill = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Skadi/Skill2") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 1,
			SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
		};
		
		public override void SetStaticDefaults() {
			// Sets the amount of frames this minion has on its spritesheet
			Main.projFrames[Projectile.type] = 53;
			// This is necessary for right-click targeting
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = false;
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = false;
		}

		public sealed override void SetDefaults() {
			Projectile.width = 200;
			Projectile.height = 200;
			Projectile.tileCollide = false; // Makes the minion go through tiles freely
			Projectile.alpha = 255;

			// These below are needed for a minion weapon
			Projectile.friendly = true; // Only controls if it deals damage to enemies on contact (more on that later)
			Projectile.minion = true; // Declares this as a minion (has many effects)
			Projectile.DamageType = DamageClass.Summon; // Declares the damage type (needed for it to deal damage)
			Projectile.minionSlots = 1f; // Amount of slots this minion occupies from the total minion slots available to the player (more on that later)
			Projectile.penetrate = -1; // Needed so the minion doesn't despawn on collision with enemies or tiles
		}

		// Here you can decide if your minion breaks things like grass or pots
		public override bool? CanCutTiles() {
			return false;
		}

		// This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
		public override bool MinionContactDamage() {
			return false;
		}

		int counter = 0;
		// The AI of this minion is split into multiple methods to avoid bloat. This method just passes values between calls actual parts of the AI.
		public override void AI() {
			Player owner = Main.player[Projectile.owner];

			if (!CheckActive(owner)) {
				return;
			}
			
			if (counter > 60) {
				foreach (var player in Main.ActivePlayers) {
					//only buff allies on the same team
					// || player.team == 0 checks if the player is in a team (singleplayer cant pick teams though)
					// (Main.netMode != NetmodeID.SinglePlayer)
					if (owner.team != player.team) {
						continue;
					}
					
					float distancebtwn = Vector2.Distance(Projectile.Center, player.Center);
					if (distancebtwn < 1500) {
						player.ClearBuff(ModContent.BuffType<SkadiS2Buff>());
						player.AddBuff(ModContent.BuffType<SkadiS2Buff>(), 120, true);
					}
				}
				if (owner.GetModPlayer<BooTaoPlayer>().SkadiSP < 58) {
					owner.GetModPlayer<BooTaoPlayer>().SkadiSP++;
				}
				if (owner.GetModPlayer<BooTaoPlayer>().SkadiSP == 56) {
					SoundEngine.PlaySound(Skill, Projectile.Center);
				}
				counter = 0;
			}
			counter++;
			
			if (Projectile.alpha > 0) {
				Projectile.alpha -= 5;
			}

			GeneralBehavior(owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
			Movement(distanceToIdlePosition, vectorToIdlePosition, out Vector2 vel);
			Visuals();
		}

		// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
		private bool CheckActive(Player owner) {
			if (owner.dead || !owner.active) {
				owner.ClearBuff(ModContent.BuffType<SkadiMinionBuff>());

				return false;
			}

			if (owner.HasBuff(ModContent.BuffType<SkadiMinionBuff>())) {
				Projectile.timeLeft = 2;
			}

			return true;
		}

		private void GeneralBehavior(Player owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition) {
			Vector2 idlePosition = owner.Center;
			idlePosition.Y -= 68f; // Go up 48 coordinates (three tiles from the center of the player)

			// If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
			// The index is projectile.minionPos
			float minionPositionOffsetX = (10 + Projectile.minionPos * 40) * -owner.direction;
			idlePosition.X += minionPositionOffsetX; // Go behind the player

			// All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

			// Teleport to player if distance is too big
			vectorToIdlePosition = idlePosition - Projectile.Center;
			distanceToIdlePosition = vectorToIdlePosition.Length();

			if (Main.myPlayer == owner.whoAmI && distanceToIdlePosition > 2000f) {
				// Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
				// and then set netUpdate to true
				Projectile.position = idlePosition;
				Projectile.velocity *= 0.1f;
				Projectile.netUpdate = true;
			}

			// If your minion is flying, you want to do this independently of any conditions
			float overlapVelocity = 0.04f;

			// Fix overlap with other minions
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

		private void Movement(float distanceToIdlePosition, Vector2 vectorToIdlePosition, out Vector2 vel) {
			// Default movement parameters (here for attacking)
			float speed = 6f;
			float inertia = 10f;

			if (distanceToIdlePosition > 300f) {
				// Speed up the minion if it's away from the player
				speed = 20f;
				inertia = 30f;
			}
			else {
				// Slow down the minion if closer to the player
				speed = 1.5f;
				inertia = 40f;
			}

			if (distanceToIdlePosition > 20f) {
				// The immediate range around the player (when it passively floats about)

				// This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
				vectorToIdlePosition.Normalize();
				vectorToIdlePosition *= speed;
				Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
			}
			else if (Projectile.velocity == Vector2.Zero) {
				// If there is a case where it's not moving at all, give it a little "poke"
				Projectile.velocity.X = -0.15f;
				Projectile.velocity.Y = -0.05f;
			}
			vel = Projectile.velocity;
		}

		private void Visuals() {
			// So it will lean slightly towards the direction it's moving
			// Projectile.rotation = Projectile.velocity.X * 0.05f;

			// This is a simple "loop through all frames from top to bottom" animation
			int frameSpeed = 3;

			Projectile.frameCounter++;

			if (Projectile.frameCounter >= frameSpeed) {
				Projectile.frameCounter = 0;
				Projectile.frame++;

				if (Projectile.frame >= Main.projFrames[Projectile.type]) {
					Projectile.frame = 0;
				}
			}

			// Some visuals here
			Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.78f);
		}
	}
}
/*
https://docs.tmodloader.net/docs/stable/class_main.html#aa9306db95b98d19297cf4cf9c4f6dd27
https://prts.wiki/w/%E6%B5%8A%E5%BF%83%E6%96%AF%E5%8D%A1%E8%92%82#%E6%B3%A8%E9%87%8A%E4%B8%8E%E9%93%BE%E6%8E%A5
*/