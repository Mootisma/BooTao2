using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Projectiles.Qingque
{
    public class QingqueProj : ModProjectile
    {
        public ref float TilePosition => ref Projectile.ai[0];
		public ref float ProjType => ref Projectile.ai[1];
		
		public override void SetStaticDefaults() {
			Main.projFrames[Projectile.type] = 4;
		}
		
		public override void SetDefaults()
        {
            Projectile.width = 70;
            Projectile.height = 110;
            Projectile.timeLeft = 60;
            Projectile.alpha = 0;
			Projectile.hide = false;
			Projectile.friendly = false;
			Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 1;
			Projectile.light = 0.9f;
        }
        
		public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if ((!player.GetModPlayer<BooTaoPlayer>().QingqueHolding || (player.dead && !player.active)) && ProjType == 0){
				Projectile.Kill();
            }
			
			if (ProjType == 0) {
				Projectile.position.X = player.Center.X - 190 + 100 * TilePosition;
				Projectile.position.Y = player.Center.Y + 100;
				//Dust.NewDustPerfect(Projectile.position, 255, null, 120, default, 1f);
				Projectile.velocity = Vector2.Zero;
				Projectile.timeLeft = 20;
				Projectile.penetrate = -1;
				Projectile.frame = player.GetModPlayer<BooTaoPlayer>().QingqueTiles[(int)TilePosition];
			}
			if (ProjType > 0) {
				Projectile.frame = (int)Projectile.ai[2];
				Projectile.friendly = true;
				Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
			}
        }
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			if (ProjType > 1) {
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center + new Vector2(0, -65), Vector2.Zero, ModContent.ProjectileType<QingqueExplosionProj>(), (int)(Projectile.damage / 1.4), Projectile.knockBack, Projectile.owner);
			}
			// follow up attack
			Player player = Main.player[Projectile.owner];
			if (player.GetModPlayer<BooTaoPlayer>().QingqueFUA) {
				player.GetModPlayer<BooTaoPlayer>().QingqueFUA = false;
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<QingqueExplosionProj2>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
			}
		}
		
		public override bool? CanCutTiles() {
			return false;
		}
		//public override void OnKill(int timeLeft) {
    }
}
