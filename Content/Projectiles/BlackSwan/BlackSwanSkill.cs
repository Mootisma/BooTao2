using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using BooTao2.Content.Buffs.BlackSwan;

namespace BooTao2.Content.Projectiles.BlackSwan {
	public class BlackSwanSkill : ModProjectile {
		public override void SetDefaults() {
			Projectile.width = 199;
			Projectile.height = 20;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.alpha = 250;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 45;
			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
			/* When used in conjunction with usesLocalNPCImmunity, 
			determines how many ticks must pass before this projectile can deal damage again 
			to the same npc. A value of -1 indicates that it can only hit a specific npc once. 
			The default value of -2 has no effect, so this must be assigned if usesLocalNPCImmunity is true. */
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			//target.AddBuff(ModContent.BuffType<ThornsBuff>(), 480);//
			//target.GetGlobalNPC<BooTaoGlobalNPC>().bsTimer = 0;
			//target.GetGlobalNPC<BooTaoGlobalNPC>().Arcana += 3;
			//target.GetGlobalNPC<BooTaoGlobalNPC>().bsDmgDone = damageDone;
			target.AddBuff(ModContent.BuffType<BlackSwanDefenseDebuff>(), 1800);
			target.AddBuff(ModContent.BuffType<BlackSwanBuff>(), 16000);
			
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
			modifiers.DisableCrit();
			// life regen / 2 to get the hp/s --> so 10 life regen is 5 hp/s (i think
		}
		
		public override void AI() {
			Lighting.AddLight(Projectile.Center, Color.Purple.ToVector3() * 2f);
			//Dust.NewDust(Projectile.position, 200, 200, 27, 0, 0, 15, default, 1f);
			//Dust.NewDust(Projectile.position, 200, 200, 15, 0, 0, 15, default, 1f);
			if (Projectile.timeLeft > 25) {
				Projectile.alpha -= 7;
				Dust.NewDust(Projectile.position, 200, 20, 173, 0, 0, 15, default, 1f);
			}
			else {
				Dust.NewDust(Projectile.position, 200, 20, 27, 0, 0, 15, default, 1f);
				Dust.NewDust(Projectile.position, 200, 20, 15, 0, 0, 15, default, 1f);
				Projectile.velocity = new Vector2(0, 20f);
			}
			if (Projectile.timeLeft < 15) {
				Projectile.alpha += 4;
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