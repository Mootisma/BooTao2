using BooTao2.Content.Items.Escoffier;
using BooTao2.Content.Buffs.Escoffier;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace BooTao2.Content.Projectiles.Escoffier
{
	public class EscoffierSkill : ModProjectile
	{
		public override void SetStaticDefaults() {
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = false;
		}

		public sealed override void SetDefaults() {
			Projectile.width = 80;
			Projectile.height = 80;
			Projectile.tileCollide = false;
			// These below are needed for a minion weapon
			Projectile.friendly = true; // 
			Projectile.minion = true; // Declares this as a minion
			Projectile.DamageType = DamageClass.Summon; // Declares the damage type
			Projectile.minionSlots = 1f; // Amount of slots
			Projectile.penetrate = -1; // Needed so the minion doesn't despawn
			Projectile.alpha = 0;
			Projectile.scale = 1f;
		}
		
		// Here you can decide if your minion breaks things like grass or pots
		public override bool? CanCutTiles() {
			return false;
		}

		// This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
		public override bool MinionContactDamage() {
			return false;
		}

		public int counter = 0;
		public override void AI() {
			Player owner = Main.player[Projectile.owner];
			if (!CheckActive(owner)) {
				return;
			}

			GeneralBehavior(owner);
			SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
			Lighting.AddLight(Projectile.Center, Color.White.ToVector3());
			Vector2 direction = targetCenter - Projectile.Center;
			direction.Normalize();
			
			if (foundTarget) {
				if (counter >= 60) {
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, direction * 16f, ModContent.ProjectileType<FrostyParfait>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
					counter = 0;
				}
			}
			counter++;
			if (!foundTarget) {
				counter = 0;
			}
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			Player player = Main.player[Projectile.owner];
			target.AddBuff(ModContent.BuffType<EscoffierBuff>(), 480);//
		}

		// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
		private bool CheckActive(Player owner) {
			if (owner.dead || !owner.active) {
				owner.ClearBuff(ModContent.BuffType<EscoffierBuff>());
				return false;
			}
			if (owner.HasBuff(ModContent.BuffType<EscoffierBuff>())) {
				Projectile.timeLeft = 2;
			}
			return true;
		}

		int idlePause;
        bool floatUpOrDown; //false is Up, true is Down
		private void GeneralBehavior(Player owner)
        {
            Vector2 ownerMountedCenter = owner.RotatedRelativePoint(owner.MountedCenter, true);
            Projectile.direction = owner.direction;

            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation = owner.velocity.X * 0.05f;

            if (Projectile.spriteDirection == 1) {//facing right, the projectile will be to the right of the player
                Projectile.position.X = owner.Center.X + 20;
            }
            else {
                Projectile.position.X = owner.Center.X - 100;

            }
            Projectile.position.Y = ownerMountedCenter.Y - Projectile.height / 2 - 38;
            UpdateMovement();
        }
        private void UpdateMovement()
        {
            if (floatUpOrDown) {//up
                if (Projectile.ai[0] > 7) {
                    DrawOriginOffsetY++;
                    Projectile.ai[0] = 0;
                }
            }
            else {
                if (Projectile.ai[0] > 7) {
                    DrawOriginOffsetY--;
                    Projectile.ai[0] = 0;
                }
            }
            if (DrawOriginOffsetY > -10) {
                idlePause = 10;
                DrawOriginOffsetY = -10;
                floatUpOrDown = false;

            }
            if (DrawOriginOffsetY < -20) {
                idlePause = 10;
                DrawOriginOffsetY = -20;
                floatUpOrDown = true;

            }
            if (idlePause < 0) {
                if (DrawOriginOffsetY < -12 && DrawOriginOffsetY > -18) {
                    Projectile.ai[0] += 2;
                }
                else {
                    Projectile.ai[0]++;
                }
            }
            idlePause--;
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
						bool closeThroughWall = between < 1000f;

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
	}
}
