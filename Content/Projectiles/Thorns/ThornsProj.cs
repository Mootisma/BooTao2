using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BooTao2.Content.Buffs.Thorns;

namespace BooTao2.Content.Projectiles.Thorns {
	public class ThornsProj : ModProjectile {
		public override void SetDefaults() {
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.1f;
			Projectile.tileCollide = true;
			Projectile.timeLeft = 80;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			Player player = Main.player[Projectile.owner];
			player.GetModPlayer<BooTaoPlayer>().ThornsHealingCD = 121;
			if (player.GetModPlayer<BooTaoPlayer>().ThornsS3duration <= 0 && player.GetModPlayer<BooTaoPlayer>().ThornsS3numUses < 2) {
				player.GetModPlayer<BooTaoPlayer>().ThornsSP++;
			}
			target.AddBuff(ModContent.BuffType<ThornsRegen>(), 180);
			target.GetGlobalNPC<BooTaoGlobalNPC>().ThornsDOTduration = 180;
			if (target.GetGlobalNPC<BooTaoGlobalNPC>().ThornsDOTstack < 4){
				target.GetGlobalNPC<BooTaoGlobalNPC>().ThornsDOTstack++;
			}
			target.GetGlobalNPC<BooTaoGlobalNPC>().ThornsDOTdmg = (int)(damageDone * target.GetGlobalNPC<BooTaoGlobalNPC>().ThornsDOTstack / 4);
			
			for (int d = 0; d < 5; d++) {
				Dust.NewDust(Projectile.position, 0, 0, 32, 0, 0, 150, default, 1f);
			}
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
		}
		
		public override void AI() {
			Dust.NewDust(Projectile.position, 0, 0, 32, 0, 0, 150, default, 1f);
		}
	}
}