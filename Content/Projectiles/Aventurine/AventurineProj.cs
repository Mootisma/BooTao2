using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Projectiles.Aventurine {
	public class AventurineProj : ModProjectile {
		public override void SetDefaults() {
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.6f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 60;
		}
		
		public override bool? CanCutTiles() {
			return false;
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
		}
		
		public override void AI() {
			Dust.NewDust(Projectile.position, 0, 0, 19, 0, 0, 150, default, 1f);
			Dust.NewDust(Projectile.position, 0, 0, 10, 0, 0, 150, default, 1f);
			Dust.NewDust(Projectile.position, 0, 0, 228, 0, 0, 150, default, 1f);
			//Dust.NewDust(Projectile.position, 0, 0, 170, 0, 0, 150, default, 1f);
		}
	}
}