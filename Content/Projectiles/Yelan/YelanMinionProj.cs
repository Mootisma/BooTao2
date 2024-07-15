using BooTao2.Content.Items.Yelan;
using BooTao2.Content.Buffs.Yelan;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace BooTao2.Content.Projectiles.Yelan
{
	public class YelanMinionProj : ModProjectile
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
			Projectile.alpha = 0;
			Projectile.scale = 0.8f;
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
		int bufftimer = 0;
		public override void AI() {
			Player owner = Main.player[Projectile.owner];
			if (!CheckActive(owner)) {
				return;
			}

			GeneralBehavior(owner);
			SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
			Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.78f);
			
			if (foundTarget && (counter == 60) && (Main.myPlayer == Projectile.owner)) {
				Vector2 projvel = 10 * ((Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY)) - Projectile.Center).SafeNormalize(Vector2.UnitX);
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, projvel, ModContent.ProjectileType<YelanDiceAttackProj>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1);
			}
			if (foundTarget && (counter == 62) && (Main.myPlayer == Projectile.owner)) {
				Vector2 projvel = 10 * ((Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY)) - Projectile.Center).SafeNormalize(Vector2.UnitX);
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, projvel, ModContent.ProjectileType<YelanDiceAttackProj>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1);
			}
			if (foundTarget && (counter == 64) && (Main.myPlayer == Projectile.owner)) {
				counter = 0;
				//
				//owner.GetDamage(DamageClass.Generic) -= owner.GetModPlayer<BooTaoPlayer>().YelanDmgBuff;
				owner.GetModPlayer<BooTaoPlayer>().YelanDmgBuff += 0.035f;
				if (owner.GetModPlayer<BooTaoPlayer>().YelanDmgBuff >= 0.5f) {
					owner.GetModPlayer<BooTaoPlayer>().YelanDmgBuff = 0.5f;
				}
				//owner.GetDamage(DamageClass.Generic) += owner.GetModPlayer<BooTaoPlayer>().YelanDmgBuff;
				//
				Vector2 projvel = 10 * ((Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY)) - Projectile.Center).SafeNormalize(Vector2.UnitX);
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, projvel, ModContent.ProjectileType<YelanDiceAttackProj>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1);
			}
			// the game handles this thing on its own; no need to subtract values from it
			owner.GetDamage(DamageClass.Generic) += owner.GetModPlayer<BooTaoPlayer>().YelanDmgBuff;
			counter++;
			bufftimer++;
			if (bufftimer > 1200) {
				//owner.GetDamage(DamageClass.Generic) -= owner.GetModPlayer<BooTaoPlayer>().YelanDmgBuff;
				owner.GetModPlayer<BooTaoPlayer>().YelanDmgBuff = 0.01f;
				bufftimer = 0;
			}
			if (!foundTarget) {
				counter = 0;
				bufftimer = 0;
				//owner.GetDamage(DamageClass.Generic) -= owner.GetModPlayer<BooTaoPlayer>().YelanDmgBuff;
				owner.GetModPlayer<BooTaoPlayer>().YelanDmgBuff = 0f;
			}
		}

		// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
		private bool CheckActive(Player owner) {
			if (owner.dead || !owner.active) {
				owner.ClearBuff(ModContent.BuffType<YelanMinionBuff>());
				return false;
			}
			if (owner.HasBuff(ModContent.BuffType<YelanMinionBuff>())) {
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

            if (Projectile.spriteDirection == 1) {
                Projectile.position.X = owner.Center.X + 48;
            }
            else {
                Projectile.position.X = owner.Center.X - 88;

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
	}
}
