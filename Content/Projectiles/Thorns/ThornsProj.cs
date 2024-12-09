using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
			Projectile.timeLeft = 60;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			Player player = Main.LocalPlayer;
			player.GetModPlayer<BooTaoPlayer>().ThornsHealingCD = 121;
			if (player.GetModPlayer<BooTaoPlayer>().ThornsS3duration <= 0 && player.GetModPlayer<BooTaoPlayer>().ThornsS3numUses < 2) {
				player.GetModPlayer<BooTaoPlayer>().ThornsSP++;
			}
			//target.AddBuff(ModContent.BuffType<ThornsBuff>(), 480);//
			target.GetGlobalNPC<BooTaoGlobalNPC>().ThornsDOTduration = 180;
			if (target.GetGlobalNPC<BooTaoGlobalNPC>().ThornsDOTstack < 4){
				target.GetGlobalNPC<BooTaoGlobalNPC>().ThornsDOTstack++;
			}
			target.GetGlobalNPC<BooTaoGlobalNPC>().ThornsDOTdmg = (int)(damageDone * target.GetGlobalNPC<BooTaoGlobalNPC>().ThornsDOTstack / 4);
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
		}
	}
}