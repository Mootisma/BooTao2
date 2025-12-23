using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BooTao2.Systems;

namespace BooTao2.Content.Projectiles.Lemuen
{
    public class LemuenProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 23;
            Projectile.height = 14;
            Projectile.timeLeft = 90;
            Projectile.alpha = 0;
			Projectile.hide = false;
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.light = 1f;
        }
        
		public override void AI()
        {
            //Dust.NewDust(Projectile.Center, 0, 0, 127, Projectile.velocity.X / 10, Projectile.velocity.Y / 10, 190, default, 1f);
			// Player player = Main.player[Projectile.owner];
			Projectile.rotation = Projectile.velocity.ToRotation();//+ MathHelper.PiOver2
        }
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
			
		}
		
		public override bool? CanCutTiles() {
			return false;
		}
		
		public override void OnKill(int timeLeft) {
			Player player = Main.player[Projectile.owner];
			// Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<FiammettaExplosionProj2>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1);
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			Player player = Main.player[Projectile.owner];
			// Projectile.damage = 1;
			if (player.GetModPlayer<BooTaoPlayer>().LemuenSP < 39 && player.GetModPlayer<BooTaoPlayer>().LemuenAmmo <= 0) {
				player.GetModPlayer<BooTaoPlayer>().LemuenSP++;
			}
		}
    }
}
