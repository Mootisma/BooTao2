using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BooTao2.Systems;
using BooTao2.Content.Buffs.Serval;

namespace BooTao2.Content.Projectiles.Serval
{
    public class ServalProj : ModProjectile
    {
        public ref float isSkillProj => ref Projectile.ai[0];
		
		public override void SetDefaults()
        {
            Projectile.width = 23;
            Projectile.height = 14;
            Projectile.timeLeft = 25;
            Projectile.alpha = 0;
			Projectile.hide = false;
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = -1;
			Projectile.light = 0.1f;
        }
        
		public override void AI()
        {
            Dust.NewDust(Projectile.Center, 3, 3, 173, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), 120, default, 1f);
			Dust.NewDustPerfect(Projectile.Center, 255, null, 120, default, 1f);
			Lighting.AddLight(Projectile.Center, Color.Purple.ToVector3() * 0.5f);
        }
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
		}
		
		public override bool? CanCutTiles() {
			return false;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			Player owner = Main.player[Projectile.owner];
			if (isSkillProj > 1f) {
				target.AddBuff(ModContent.BuffType<ServalBuff>(), 300);
			}
		}
    }
}
