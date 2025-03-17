using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace BooTao2.Content.Projectiles.BlackSwan {
	public class BlackSwanBasicProj : ModProjectile {
		public override void SetDefaults() {
			Projectile.width = 70;
			Projectile.height = 121;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = false;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.alpha = 250;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 40;
		}
		
		public override void AI() {
			Lighting.AddLight(Projectile.Center, Color.Purple.ToVector3() * 2f);
			Dust.NewDust(Projectile.position, 70, 120, 27, 0, 0, 15, default, 1f);
			Dust.NewDust(Projectile.position, 70, 120, 15, 0, 0, 15, default, 1f);
			if (Projectile.alpha > 10 && Projectile.timeLeft > 25) {
				Projectile.alpha -= 10;
			}
			if (Projectile.timeLeft == 25) {
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BSPurple>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 1, 1);
			}
			if (Projectile.timeLeft < 25) {
				Projectile.alpha += 9;
			}
		}
		
		public override bool? CanCutTiles() {
			return false;
		}
	}
}
/*

https://terraria.wiki.gg/wiki/Dust_IDs
https://docs.tmodloader.net/docs/stable/class_dust.html
https://docs.tmodloader.net/docs/stable/class_mod_projectile.html

*/