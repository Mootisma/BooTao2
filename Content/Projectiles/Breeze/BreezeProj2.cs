using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BooTao2.Systems;
using BooTao2.Content.Projectiles.Wisadel;
using Terraria.Audio;

namespace BooTao2.Content.Projectiles.Breeze
{
    public class BreezeProj2 : ModProjectile
    {
        public ref float skill => ref Projectile.ai[0];
		
		public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.timeLeft = 10;
            Projectile.alpha = 120;
			Projectile.hide = false;
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.light = 0.1f;
        }
        
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
				hitInfo.Damage = 0;
			};
		}
		
		public override bool? CanCutTiles() {
			return false;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ProjectileID.MiniNukeRocketI, Projectile.damage, Projectile.knockBack, Projectile.owner);
			if (skill > 0) {
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ProjectileID.NebulaBlaze2, Projectile.damage, Projectile.knockBack, Projectile.owner);
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<WisadelProjExplosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ProjectileID.InfernoFriendlyBlast, Projectile.damage, Projectile.knockBack, Projectile.owner);
			}
		}
    }
}
