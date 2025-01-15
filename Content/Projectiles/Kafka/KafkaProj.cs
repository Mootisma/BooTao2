using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace BooTao2.Content.Projectiles.Kafka {
	public class KafkaProj : ModProjectile {
		public override void SetStaticDefaults() {
			Main.projFrames[Projectile.type] = 3;
		}
		public override void SetDefaults() {
			Projectile.width = 120;
			Projectile.height = 120;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.alpha = 250;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 69;
			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 27;
			/* When used in conjunction with usesLocalNPCImmunity, 
			determines how many ticks must pass before this projectile can deal damage again 
			to the same npc. A value of -1 indicates that it can only hit a specific npc once. 
			The default value of -2 has no effect, so this must be assigned if usesLocalNPCImmunity is true. */
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
			modifiers.DisableCrit();
			if (target.lifeRegen < 0 && Projectile.frame == 1) {
				modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
					hitInfo.Damage += (int)(Math.Abs(target.lifeRegen) * 3 / 8);
				};
			}// life regen / 2 to get the hp/s --> so 10 life regen is 5 hp/s (i think
		}
		
		public override void AI() {
			Lighting.AddLight(Projectile.Center, Color.Purple.ToVector3() * 2f);
			if (Projectile.timeLeft > 42) {
				if (Projectile.alpha > 0 && Projectile.timeLeft > 60)
					Projectile.alpha -= 25;
				if (Projectile.timeLeft <= 52)// 56 55 54 53 52 51 50 49 48 47  
					Projectile.alpha += 25;
				Projectile.frame = 2;
				
			}
			if (Projectile.timeLeft <= 42 && Projectile.timeLeft >= 23) {
				if (Projectile.timeLeft >= 38)//46 45 44   43 42 41   40 39 38   37
					Projectile.alpha -= 50;
				if (Projectile.timeLeft <= 28)//23 24 25   26 27 28   29 30 31   32
					Projectile.alpha += 50;
				Projectile.frame = 0;
			}
			if (Projectile.timeLeft < 23) {
				if (Projectile.timeLeft >= 13)// 22 21 20   19 18 17   16 15 14  13
					Projectile.alpha -= 25;
				if (Projectile.timeLeft <= 10)
					Projectile.alpha += 10;
				Projectile.frame = 1;
			}
			
		}
		
		public override bool? CanCutTiles() {
			return false;
		}
	}
}
/*

https://terraria.wiki.gg/wiki/Dust_IDs
https://honkai-star-rail.fandom.com/wiki/Kafka/Media?file=Kafka_Character_Preview_Details_7.gif#Introduction_Details
https://docs.tmodloader.net/docs/stable/struct_n_p_c_1_1_hit_modifiers.html
https://docs.tmodloader.net/docs/stable/class_dust.html
https://docs.tmodloader.net/docs/stable/class_mod_projectile.html

*/