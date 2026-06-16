using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using BooTao2.Systems;

namespace BooTao2.Content.Projectiles.Lemuen
{
    public class LemuenTargetProj : ModProjectile
    {
        private bool dir = false;
		public ref float delay => ref Projectile.ai[0];
		private bool flag = false;
		
		SoundStyle LemuenSkillAim = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Lemuen/LemuenSkillAim") {
			Volume = 1f,
			PitchVariance = 0.0f,
			MaxInstances = 8,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		public override void SetDefaults()
        {
            Projectile.width = 23;
            Projectile.height = 14;
            Projectile.timeLeft = 60;
            Projectile.alpha = 5;
			Projectile.hide = false;
			Projectile.friendly = false;
			Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = -1;
			Projectile.light = 1f;
        }
        
		public override void AI()
        {
            Player player = Main.player[Projectile.owner];
			if (player.dead && !player.active){
				Projectile.Kill();
            }
			
			// make the sprite 'blink'
			Projectile.alpha += (dir) ? 5 : -5;
			if (Projectile.alpha == 0 || Projectile.alpha == 200)
				dir ^= true;
			
			Projectile.velocity = Vector2.Zero;
			
			if(!flag){
				// if skill ends, put the proj on a timer to explode; else, keep it alive
				if (player.GetModPlayer<BooTaoPlayer>().LemuenAmmo == 0) {
					Projectile.timeLeft = 60 + (int)(delay);
					flag = true;
				}
				else {
					Projectile.timeLeft = 60;
				}
		    }
        }
		
		public override bool? CanCutTiles() {
			return false;
		}
		
		public override void OnKill(int timeLeft) {
			Player player = Main.player[Projectile.owner];
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<LemuenExplosionProj>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1);
		}
		
		public override void OnSpawn(IEntitySource source) {
			SoundEngine.PlaySound(LemuenSkillAim, Projectile.Center);
		}
    }
}
