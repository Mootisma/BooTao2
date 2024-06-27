﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Projectiles.Ningguang
{
    public class NingJade3 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 16;
            Projectile.timeLeft = 240;
            Projectile.penetrate = -1;
            Projectile.hide = false;
            Projectile.alpha = 0;
            Projectile.netImportant = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
			Projectile.scale = 1f;
        }
        int idlePause;
        bool floatUpOrDown; //false is Up, true is Down
		bool damageState = false;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
			if (player.GetModPlayer<BooTaoPlayer>().NingJade3State || damageState) {
				damageState = true;
				Projectile.DamageType = DamageClass.Summon;
				Projectile.friendly = true;
				if (Main.rand.Next(6) == 0) {
					int dustnumber = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemTopaz);
					Main.dust[dustnumber].noGravity = true;
				}
				
				float maxDetectRadius = 500f;
				float projSpeed = 10f;

				NPC closestNPC = FindClosestNPC(maxDetectRadius);
				if (closestNPC == null) {
					// if no nearest npc, point to cursor
					Projectile.velocity = projSpeed * ((Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY)) - player.Center).SafeNormalize(Vector2.UnitX);
					Projectile.rotation = Projectile.velocity.ToRotation();
					return;
				}

				Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
				Projectile.rotation = Projectile.velocity.ToRotation();
			}
			else {
				if (!player.GetModPlayer<BooTaoPlayer>().NingHolding || (player.GetModPlayer<BooTaoPlayer>().NingNumBuff <= 0) || (player.dead && !player.active))
				{
					Projectile.Kill();
				}
				Projectile.timeLeft = 240;

				Vector2 ownerMountedCenter = player.RotatedRelativePoint(player.MountedCenter, true);
				Projectile.direction = player.direction;

				Projectile.spriteDirection = Projectile.direction;
				Projectile.rotation = player.velocity.X * 0.05f;

				if (Projectile.spriteDirection == 1)
				{
					Projectile.position.X = player.Center.X - 30;
				}
				else
				{
					Projectile.position.X = player.Center.X + 20;

				}
				Projectile.position.Y = ownerMountedCenter.Y - Projectile.height / 2 + 9;
				UpdateMovement();
			}
        }
        private void UpdateMovement()
        {
            if (floatUpOrDown)//Up
            {
                if (Projectile.ai[0] > 7)
                {
                    DrawOriginOffsetY++;
                    Projectile.ai[0] = 0;
                }
            }
            else
            {
                if (Projectile.ai[0] > 7)
                {
                    DrawOriginOffsetY--;
                    Projectile.ai[0] = 0;
                }
            }
            if (DrawOriginOffsetY > -10)
            {
                idlePause = 10;
                DrawOriginOffsetY = -10;
                floatUpOrDown = false;

            }
            if (DrawOriginOffsetY < -20)
            {
                idlePause = 10;
                DrawOriginOffsetY = -20;
                floatUpOrDown = true;

            }
            if (idlePause < 0)
            {
                if (DrawOriginOffsetY < -12 && DrawOriginOffsetY > -18)
                {
                    Projectile.ai[0] += 2;
                }
                else
                {
                    Projectile.ai[0]++;
                }
            }
            idlePause--;
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
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			Player player = Main.LocalPlayer;
			player.GetModPlayer<BooTaoPlayer>().NingJade3State = false;
			damageState = false;
		}
		
		public override void OnKill(int timeLeft) {
			Player player = Main.LocalPlayer;
			player.GetModPlayer<BooTaoPlayer>().NingJade3State = false;
			damageState = false;
		}
    }
}
