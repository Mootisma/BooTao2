using BooTao2.Content.Items.Mostima;
using BooTao2.Content.Buffs.Mostima;
using BooTao2.Content.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace BooTao2.Content.Projectiles.Mostima
{
	public class MostimaMinion : ModProjectile
	{
		SoundStyle SkillAttack = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Mostima/SkillAttack") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 6,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle BasicAttack = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Mostima/BasicAttack") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 6,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle BasicAttack2 = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Mostima/BasicAttack2") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 6,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		public override void SetStaticDefaults() {
			// Sets the amount of frames this minion has on its spritesheet
			Main.projFrames[Projectile.type] = 123;
			// This is necessary for right-click targeting
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = false;
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = false;
		}

		public sealed override void SetDefaults() {
			Projectile.width = 125;
			Projectile.height = 125;
			Projectile.tileCollide = false; // Makes the minion go through tiles freely
			Projectile.alpha = 250;

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
		
		public override void OnKill(int timeLeft) {
			Player owner = Main.player[Projectile.owner];
			owner.GetModPlayer<BooTaoPlayer>().MostimaSkillSP = 0;
			owner.GetModPlayer<BooTaoPlayer>().MostimaSkillDuration = 0;
			owner.GetModPlayer<BooTaoPlayer>().MostimaSkill = false;
		}

		public override void AI() {
			Player owner = Main.player[Projectile.owner];

			if (!CheckActive(owner)) {
				return;
			}
			
			if (Projectile.alpha > 0) {
				Projectile.alpha -= 25;
			}
			
			TimeStop(owner.GetModPlayer<BooTaoPlayer>().MostimaSkill);
			if (owner.GetModPlayer<BooTaoPlayer>().MostimaSkillDuration == 1) {
				TimeResume();
			}
			if (owner.GetModPlayer<BooTaoPlayer>().MostimaSkillDuration > 0) {
				owner.GetModPlayer<BooTaoPlayer>().MostimaSkillDuration--;
			}
			if (owner.GetModPlayer<BooTaoPlayer>().MostimaSkillSP < 3780 && !owner.GetModPlayer<BooTaoPlayer>().MostimaSkill)
				owner.GetModPlayer<BooTaoPlayer>().MostimaSkillSP++;
			if (owner.GetModPlayer<BooTaoPlayer>().MostimaSkillDuration <= 0) {
				owner.GetModPlayer<BooTaoPlayer>().MostimaSkill = false;
			}
			
			GeneralBehavior(owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
			Movement(distanceToIdlePosition, vectorToIdlePosition, out Vector2 vel);
			
			if (Main.myPlayer != Projectile.owner) {
				Projectile.frameCounter++;
				if (Projectile.frameCounter >= 4) {
					Projectile.frameCounter = 0;
					Projectile.frame++;
				}
				if (Projectile.frame >= 58) {
					Projectile.frame = 10;
				}
			}
			else {
				Projectile.spriteDirection = (Projectile.Center.X < Main.MouseWorld.X) ? 1 : -1;
				SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
				Visuals(foundTarget, distanceFromTarget, targetCenter, owner);
			}
			//Projectile.netUpdate = true;
		}

		// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
		private bool CheckActive(Player owner) {
			if (owner.dead || !owner.active) {
				owner.ClearBuff(ModContent.BuffType<MostimaMinionBuff>());

				return false;
			}

			if (owner.HasBuff(ModContent.BuffType<MostimaMinionBuff>())) {
				Projectile.timeLeft = 2;
			}

			return true;
		}

		private void GeneralBehavior(Player owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition) {
			Vector2 idlePosition = owner.Center;
			idlePosition.Y -= 68f; // Go up 68 coordinates (three tiles from the center of the player)

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
		
		private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter) {
			// Starting search distance
			distanceFromTarget = 300f;
			targetCenter = Main.MouseWorld;
			foundTarget = false;

			// This code is required if your minion weapon has the targeting feature
			if (owner.HasMinionAttackTargetNPC) {
				NPC npc = Main.npc[owner.MinionAttackTargetNPC];
				float between = Vector2.Distance(npc.Center, Main.MouseWorld);

				// Reasonable distance away so it doesn't target across multiple screens
				if (between < 600f) {
					distanceFromTarget = between;
					targetCenter = npc.Center;
					foundTarget = true;
					
					Projectile.spriteDirection = (targetCenter.X < Projectile.Center.X) ? -1 : 1;
				}
			}

			if (!foundTarget) {
				// This code is required either way, used for finding a target
				foreach (var npc in Main.ActiveNPCs) {
					if (npc.CanBeChasedBy()) {
						float between = Vector2.Distance(npc.Center, Main.MouseWorld);
						bool closest = Vector2.Distance(Main.MouseWorld, targetCenter) > between;
						bool inRange = between < distanceFromTarget;
						bool closeThroughWall = between < 300f;

						if (((closest && inRange) || !foundTarget) && (closeThroughWall)) {
							distanceFromTarget = between;
							targetCenter = npc.Center;
							foundTarget = true;
							
							Projectile.spriteDirection = (targetCenter.X < Projectile.Center.X) ? -1 : 1;
						}
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
		
		private enum StateMachine
		{
			Start,
			Idle,
			Attack,
			SkillStart,
			SkillIdle,
			SkillAttack,
			SkillEnd
		}
		StateMachine myVar = StateMachine.Start;
		int frameSpeed = 3;

		private void Visuals(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, Player player) {
			/* 
			start: 10 frames, 20 fps
			idle: 48 frames, 12 fps
			attack: 17 frames, 20 fps
			skill start: 7 frames, 20 fps
			skill idle: 20 frames, 10 fps ( i think this was a mistake but its too late now )
			skill attack: 18 frames, 20 fps
			skill end: 3 frames, 20 fps
			total: 123 frames
			
			3 framespeed = 20 fps
			4 framespeed = 12 fps
			6 framespeed = 10 fps
			
			this will effectively be a state machine that depends on a few flags:
				start on start
				go to idle after the 10 frames are played (half a second, this is automatically set up in the sprite sheet)
				stay in idle unless a target is found or skill is activated
				if target found and not skill activated, stay in attack
				when skill activated, go to skill start
				after the 7 frames have played, go to skill attack if found target, or go to skill idle if no found target
				stay in skill idle until target is found or skill duration ends
				stay in skill attack until no target is found or skill duration ends
				when skill duration ends go to skill end
				after the 3 frames go to idle if no found target or attack if found target
			
			bool player.GetModPlayer<BooTaoPlayer>().MostimaSkill
			*/
			
			// basic functionality
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= frameSpeed) {
				Projectile.frameCounter = 0;
				Projectile.frame++;
			}
			
			switch(myVar)
			{
				case StateMachine.Start:
					// there is no need to loop this anim
					frameSpeed = 3;
					if (Projectile.frame >= 10) {
						// if skill is active on the same frame as start needs to move on, go to skill start
						// if found target, go to attack,
						// otherwise idle
						myVar = StateMachine.Idle;
						frameSpeed = 4;
						if (foundTarget) {
							myVar = StateMachine.Attack;
							frameSpeed = 3;
							Projectile.frame = 58;
						}
						if (player.GetModPlayer<BooTaoPlayer>().MostimaSkill) {
							myVar = StateMachine.SkillStart;
							frameSpeed = 3;
							Projectile.frame = 75;
						}
					}
					break;
				case StateMachine.Idle:
					// loop idle anim
					frameSpeed = 4;
					if (Projectile.frame >= 58) {
						Projectile.frame = 10;
					}
					
					// checks to go into new state
					if (foundTarget) {
						myVar = StateMachine.Attack;
						frameSpeed = 3;
						Projectile.frame = 58;
						Projectile.frameCounter = 0;
					}
					if (player.GetModPlayer<BooTaoPlayer>().MostimaSkill) {
						myVar = StateMachine.SkillStart;
						frameSpeed = 3;
						Projectile.frame = 75;
						Projectile.frameCounter = 0;
					}
					break;
				case StateMachine.Attack:
					// loop attack anim
					frameSpeed = 3;
					
					// checks to go into new state
					if (Projectile.frame >= 75) {
						Projectile.frame = 58;
						if (!foundTarget) {
							myVar = StateMachine.Idle;
							frameSpeed = 4;
							Projectile.frame = 10;
							Projectile.frameCounter = 0;
						}
					}
					if (player.GetModPlayer<BooTaoPlayer>().MostimaSkill) {
						myVar = StateMachine.SkillStart;
						frameSpeed = 3;
						Projectile.frame = 75;
						Projectile.frameCounter = 0;
					}
					
					// if on the 7th frame, spawn the projectile
					if (Projectile.frame == 64 && Projectile.frameCounter == 0) {
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), targetCenter, Vector2.Zero, ModContent.ProjectileType<MostimaBasicProj>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1);
						SoundEngine.PlaySound(BasicAttack, Projectile.Center);
						SoundEngine.PlaySound(BasicAttack2, Projectile.Center);
					}
					break;
				case StateMachine.SkillStart:
					// no need to loop this anim
					if (Projectile.frame >= 82) {
						// if found target, go to attack,
						// otherwise idle
						myVar = StateMachine.SkillIdle;
						frameSpeed = 6;
						if (foundTarget) {
							myVar = StateMachine.SkillAttack;
							frameSpeed = 3;
							Projectile.frame = 102;
						}
					}
					break;
				case StateMachine.SkillIdle:
					// loop idle anim
					frameSpeed = 6;
					if (Projectile.frame >= 102) {
						Projectile.frame = 82;
					}
					
					// checks to go into new state
					if (foundTarget) {
						myVar = StateMachine.SkillAttack;
						frameSpeed = 3;
						Projectile.frame = 102;
						Projectile.frameCounter = 0;
					}
					if (!player.GetModPlayer<BooTaoPlayer>().MostimaSkill) {
						myVar = StateMachine.SkillEnd;
						frameSpeed = 3;
						Projectile.frame = 120;
						Projectile.frameCounter = 0;
					}
					break;
				case StateMachine.SkillAttack:
					// loop attack anim
					frameSpeed = 3;
					
					// checks to go into new state
					if (Projectile.frame >= 120) {
						Projectile.frame = 102;
						if (!foundTarget) {
							myVar = StateMachine.SkillIdle;
							frameSpeed = 6;
							Projectile.frame = 82;
							Projectile.frameCounter = 0;
						}
					}
					if (!player.GetModPlayer<BooTaoPlayer>().MostimaSkill) {
						myVar = StateMachine.SkillEnd;
						frameSpeed = 3;
						Projectile.frame = 120;
						Projectile.frameCounter = 0;
					}
					
					// if on the 7th frame, spawn the projectile
					if (Projectile.frame == 109 && Projectile.frameCounter == 0) {
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, (targetCenter - Projectile.Center).SafeNormalize(Vector2.UnitX) * new Vector2(70f, 70f), ModContent.ProjectileType<MostimaSkillProj>(), Projectile.damage * 3, Projectile.knockBack * 5f, Projectile.owner, 0, 1);
						SoundEngine.PlaySound(SkillAttack, Projectile.Center);
					}
					
					break;
				case StateMachine.SkillEnd:
					frameSpeed = 3;
					if (Projectile.frame >= 123) {
						Projectile.frame = 10;
						myVar = StateMachine.Idle;
					}
					break;
			}

			if (Projectile.frame >= Main.projFrames[Projectile.type]) {
				Projectile.frame = 10;
				myVar = StateMachine.Idle;
			}

			// Some visuals here
			Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.78f);
		}
		
		int counter = 0;
		private void TimeStop(bool isSkillActive) {
			if (counter > 6) { //https://docs.tmodloader.net/docs/stable/class_main.html#a55969af8fef5fd9819d1854403cf794b
				foreach (var proj in Main.ActiveProjectiles) {
					if (proj.owner == Projectile.owner || !proj.hostile) {
						continue;
					}
					float between = Vector2.Distance(proj.Center, Projectile.Center);
					if (between < 1000) {
						if (isSkillActive) {
							proj.velocity *= 0.01f;//Vector2.Zero
						}
						else {
							proj.velocity *= 0.66f;
						}
						//position, width, height, type, speedx, speedy, alpha, color, scale
						Dust.NewDust(proj.Center, 0, 0, 56, 0, 0, 150, default, 1f);
					}
					else {
						proj.velocity *= 0.82f;
						Dust.NewDust(proj.Center, 0, 0, 56, 0, 0, 150, default, 1f);
					}
					// if they reach zero velocity give them a nudge
					if (Math.Abs(proj.velocity.Y) <= 0.1f && Math.Abs(proj.velocity.X) <= 0.1f) {
						proj.velocity *= 2f;
					}
					proj.netUpdate = true;
				}
				foreach (var npc in Main.ActiveNPCs) {
					float between = Vector2.Distance(npc.Center, Projectile.Center);
					if (between < 1000) {
						if (isSkillActive) {
							npc.velocity *= 0.01f;
						}
						else {
							npc.velocity *= 0.66f;
						}
					}
					else {
						npc.velocity *= 0.82f;
					}
					Dust.NewDust(npc.Center, 0, 0, 56, 0, 0, 150, default, 1f);
					// if they approach zero velocity give them a nudge
					if (Math.Abs(npc.velocity.Y) <= 0.1f && Math.Abs(npc.velocity.X) <= 0.1f) {
						if (isSkillActive) {
							npc.velocity *= 2f;
						}
						else {
							npc.velocity *= 4f;
						}
					}
					if (Math.Abs(npc.velocity.Y) <= 0.5f && Math.Abs(npc.velocity.X) <= 0.5f && npc.boss) {
						if (isSkillActive) {
							npc.velocity *= 2f;
						}
						else {
							npc.velocity *= 5f;
						}
					}
					npc.netUpdate = true;
				}
				counter = 0;
			}
			
			counter++;
		}
		
		private void TimeResume() {
			foreach (var proj in Main.ActiveProjectiles) {
				if (proj.owner == Projectile.owner) {
					continue;
				}
				proj.velocity *= 10f;
			}
			foreach (var npc in Main.ActiveNPCs) {
				npc.velocity *= 10f;
			}
		}
	}
}
/*
had to change pixel format to RGBA so the compression wouldnt make mostima ugly :(
*/