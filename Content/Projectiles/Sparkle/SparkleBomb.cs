using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BooTao2.Content.Projectiles.Wisadel;

namespace BooTao2.Content.Projectiles.Sparkle {
	public class SparkleBomb : ModProjectile {
		//public const int TotalDuration = 900;
		
		public int Timer {
			get => (int)Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}
		
		public override void SetDefaults() {
			Projectile.width = 100;
			Projectile.height = 100;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.friendly = false;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.9f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 900;
			Projectile.penetrate = -1;
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
		}
		
		public override bool? CanCutTiles() {
			return false;
		}
		
		public override void OnKill(int timeLeft) {
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<WisadelProjExplosion>(), (int)(Projectile.damage * Main.rand.Next(5)), Projectile.knockBack, Projectile.owner);
		}
		
		public override void AI() {
			Timer += 1;
			if (Timer > 20) {
				foreach (var npc in Main.ActiveNPCs) {
					if (npc.CanBeChasedBy() && Projectile.getRect().Intersects(npc.getRect())) {
						Projectile.Kill();
					}
				}
				Timer = 0;
			}
		}
	}
}
/*
https://learn.microsoft.com/en-us/dotnet/api/system.drawing.rectangle.intersectswith?view=net-9.0#system-drawing-rectangle-intersectswith(system-drawing-rectangle)
https://learn.microsoft.com/en-us/previous-versions/windows/silverlight/dotnet-windows-silverlight/bb198628(v=xnagamestudio.35)
theres two different Rectangle structs...
*/