using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BooTao2.Content.Buffs.Entelechia;

namespace BooTao2.Content.Projectiles.LaPluma {
	public class LaPlumaProj : ModProjectile {
		public ref float isSkillProj => ref Projectile.ai[0];
		
		public override void SetStaticDefaults() {
			Main.projFrames[Projectile.type] = 5;
		}
		
		public override void SetDefaults() {
			Projectile.width = 82;
			Projectile.height = 160;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.1f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 30;
			Projectile.penetrate = -1;
			
			Projectile.alpha = 0;
			Projectile.scale = 1f;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			Player owner = Main.player[Projectile.owner];
			
			// i reused this proj for entelechia so i put ente stuff here
			if (owner.HasBuff(ModContent.BuffType<EntelechiaBuff>())) {
				target.AddBuff(ModContent.BuffType<EntelechiaBuff>(), 300);
				if (target.damage > 0) {
					owner.Heal(6);
					if (owner.GetModPlayer<BooTaoPlayer>().EntelechiaMaxHPBuff < 200) {
						owner.GetModPlayer<BooTaoPlayer>().EntelechiaMaxHPBuff += 10;
					}
				}
				return;
			}
			
			target.GetLifeStats(out int statLife, out int statLifeMax);
			if (target.damage > 0) {
				owner.Heal(5);
			}
			
			if (target.damage > 0 && statLife <= 0 && owner.GetModPlayer<BooTaoPlayer>().LaPlumaPassive < 0.35f) {
				owner.GetModPlayer<BooTaoPlayer>().LaPlumaPassive += 0.03f;
			}
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
			target.GetLifeStats(out int statLife, out int statLifeMax);
			if (isSkillProj > 0 && statLife <= statLifeMax / 2) {
				modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
					hitInfo.Damage = (int)(hitInfo.Damage * 1.38);
				};
			}
		}
		
		public override void AI() {
			//position, width, height, type, speedx, speedy, alpha, color, scale
			Dust.NewDust(Projectile.position, 150, 150, 54, 0, 0, 150, default, 1f);
			Projectile.rotation = Projectile.velocity.ToRotation();
			int frameSpeed = 3;

			Projectile.frameCounter++;

			if (Projectile.frameCounter >= frameSpeed) {
				Projectile.frameCounter = 0;
				if (Projectile.frame < 4) {
					Projectile.frame++;
				}
				Projectile.velocity *= 0.75f;
			}
		}
	}
}