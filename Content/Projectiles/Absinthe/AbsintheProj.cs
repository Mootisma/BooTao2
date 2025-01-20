using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Projectiles.Absinthe
{
    public class AbsintheProj : ModProjectile
    {
        public ref float isSkillProj => ref Projectile.ai[0];
		
		public override void SetDefaults()
        {
            Projectile.width = 23;
            Projectile.height = 14;
            Projectile.timeLeft = 60;
            Projectile.alpha = 0;
			Projectile.hide = false;
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 1;
			Projectile.light = 1f;
        }
        
		public override void AI()
        {
            Dust.NewDust(Projectile.Center, 0, 0, 127, Projectile.velocity.X / 10, Projectile.velocity.Y / 10, 190, default, 1f);
			Projectile.rotation = Projectile.velocity.ToRotation();//+ MathHelper.PiOver2
        }
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
			modifiers.ArmorPenetration += 10f;
			target.GetLifeStats(out int statLife, out int statLifeMax);
			if (statLife <= statLifeMax / 2) {
				modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
					hitInfo.Damage = (int)(hitInfo.Damage * 1.38);
				};
			}
			if (isSkillProj > 0 && statLife > statLifeMax / 2) {
				modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
					hitInfo.Damage = 1;
				};
			}
		}
		
		public override bool? CanCutTiles() {
			return false;
		}
		//public override void OnKill(int timeLeft) {
    }
}
