using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;
using BooTao2.Content.Buffs.Wisadel;

namespace BooTao2.Content.Projectiles.Wisadel {
	public class WisadelProjBasic : ModProjectile {
		//public override string Texture => "BooTao2/Content/Projectiles/Fiammetta/FiammettaProj";
		
		public ref float Something => ref Projectile.ai[0];
		public ref float SkillActive => ref Projectile.ai[1];
		
		SoundStyle WisadelBasicHit = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Wisadel/WisadelBasicHit") {
			Volume = 1.6f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		SoundStyle WisadelSkillBoom = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Wisadel/WisadelSkillBoom") {
			Volume = 1.6f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		public override void SetDefaults() {
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.1f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 210;
			Projectile.penetrate = 1;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			Player player = Main.player[Projectile.owner];
			player.GetModPlayer<BooTaoPlayer>().WisadelAmmo -= 1;
			if (SkillActive > 2) {
				// big boom
				SoundEngine.PlaySound(WisadelSkillBoom, Projectile.Center);
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<WisadelProjExplosion>(), (int)(Projectile.damage * 6), Projectile.knockBack, Projectile.owner);
				// aftershocks
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<WisadelProjAftershock>(), (int)(Projectile.damage * 1.5), Projectile.knockBack, Projectile.owner, 0, 4);
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<WisadelProjAftershock>(), (int)(Projectile.damage * 1.5), Projectile.knockBack, Projectile.owner, 0, 4);
			}
			else {
				SoundEngine.PlaySound(WisadelBasicHit, Projectile.Center);
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<WisadelProjAftershock>(), (int)(Projectile.damage / 2), Projectile.knockBack, Projectile.owner, 0, 0);
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<WisadelProjAftershock>(), (int)(Projectile.damage / 2), Projectile.knockBack, Projectile.owner, 0, 0);
			}
			
			for (int d = 0; d < 5; d++) {
				Dust.NewDust(Projectile.position, 0, 0, 32, 0, 0, 150, default, 1f);
			}
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
		}
		
		public override bool? CanCutTiles() {
			return false;
		}
		
		public override void AI() {
			//Dust.NewDust(Projectile.position, 0, 0, 32, 0, 0, 150, default, 1f);
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
			Dust.NewDust(Projectile.Center, 0, 0, 127, Projectile.velocity.X / 10, Projectile.velocity.Y / 10, 150, default, 1f);
		}
	}
}