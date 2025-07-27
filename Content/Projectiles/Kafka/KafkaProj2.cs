using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BooTao2.Content.Buffs.Kafka;

namespace BooTao2.Content.Projectiles.Kafka {
	public class KafkaProj2 : ModProjectile {
		public override void SetDefaults() {
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.1f;
			Projectile.tileCollide = true;
			Projectile.timeLeft = 60;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			//target.AddBuff(ModContent.BuffType<ThornsBuff>(), 480);//
			//target.GetGlobalNPC<BooTaoGlobalNPC>().KafkaDOTduration = 180;
			//target.GetGlobalNPC<BooTaoGlobalNPC>().KafkaDOTdmg = damageDone * 5;
			target.AddBuff(ModContent.BuffType<KafkaBuff>(), 600);
			
			for (int d = 0; d < 5; d++) {
				Dust.NewDust(Projectile.position, 0, 0, 27, 0, 0, 150, default, 1f);
			}
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
			modifiers.DisableCrit();
			modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
				hitInfo.Damage /= 5;
			};
		}
		
		public override void AI() {
			Dust.NewDust(Projectile.position, 0, 0, 27, 0, 0, 150, default, 1f);
		}
	}
}