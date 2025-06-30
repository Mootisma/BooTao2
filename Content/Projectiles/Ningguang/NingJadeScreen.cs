using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BooTao2.Content.Projectiles.Ningguang;
using BooTao2.Content.Buffs.Ningguang;

namespace BooTao2.Content.Projectiles.Ningguang {
	public class NingJadeScreen : ModProjectile {
		public override void SetDefaults() {
			Projectile.width = 300;
			Projectile.height = 100;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.6f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 3000;
			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 70;
		}
		
		public override bool? CanCutTiles() {
			return false;
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
		}
		
		int counter = 0;
		public override void AI() {
			Player player = Main.player[Projectile.owner];
			if (counter > 60) {
				float distancebtwn = Vector2.Distance(Projectile.Center, player.Center);
				if (distancebtwn < 200) {
					//player.ClearBuff(ModContent.BuffType<NingBuff>());
					player.AddBuff(ModContent.BuffType<NingBuff>(), 120, true);
				}
				Projectile.friendly = false;
				counter = 0;
			}
			counter++;
			if (counter % 5 == 0) {
				Dust.NewDust(Projectile.position, 300, 100, 32, 0, 0, 150, default, 1f);
				Dust.NewDust(Projectile.position, 300, 100, 19, 0, 0, 150, default, 1f);
				Dust.NewDust(Projectile.position, 300, 100, 10, 0, 0, 150, default, 1f);
				Dust.NewDust(Projectile.position, 300, 100, 228, 0, 0, 150, default, 1f);
			}
		}
	}
}