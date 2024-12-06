using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Projectiles.Fiammetta {
	public class FiammettaProj : ModProjectile {
		public override void SetDefaults() {
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.1f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 50;
			Projectile.penetrate = -1;
		}
		
		public override void AI() {
			if (Projectile.owner == Main.myPlayer) {
				if (player.GetModPlayer<BooTaoPlayer>().FiammettaS3) {
					if (Projectile.timeLeft < 26) {
						Projectile.velocity = (player.GetModPlayer<BooTaoPlayer>().FiammettaStoreMouse - Projectile.Center).SafeNormalize(Vector2.UnitX);
						Projectile.velocity.X *= 10f;
						Projectile.velocity.Y *= 30f;
					}
				}
			}
			//
		}
		// Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel, ModContent.ProjectileType<HertaDiamondProj>(), Projectile.damage * 2, Projectile.knockBack, Projectile.owner, 0, 1);
		// ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers)
		// public override void OnKill(int timeLeft) {}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			Player player = Main.LocalPlayer;
			if (player.GetModPlayer<BooTaoPlayer>().FiammettaSP < 15 && player.GetModPlayer<BooTaoPlayer>().FiammettaS3 == false) {
				player.GetModPlayer<BooTaoPlayer>().FiammettaSP++;
			}
		}
	}
}