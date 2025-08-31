using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BooTao2.Content.Buffs.Entelechia;

namespace BooTao2.Content.Projectiles.Entelechia
{
    public class EntelechiaProj : ModProjectile
    {
        public ref float isSkillProj => ref Projectile.ai[0];
		
		//public override void SetStaticDefaults() {
		//	Main.projFrames[Projectile.type] = 8;
		//}
		
		public override void SetDefaults()
        {
            Projectile.width = 160;
            Projectile.height = 160;
            Projectile.timeLeft = 720;
            Projectile.alpha = 0;
			Projectile.hide = false;
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;
			Projectile.light = 1.3f;
			//Projectile.scale = 2f;
			
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 29;
        }
        
		public override void AI() {
			Player owner = Main.player[Projectile.owner];
			if (owner.dead || !owner.active) { 
				Projectile.Kill(); 
				return;
			}
			
			//position, width, height, type, speedx, speedy, alpha, color, scale
			Dust.NewDust(Projectile.position, 160, 160, 235, 0, 0, 90, default, 1f);
			Dust.NewDust(Projectile.position, 160, 160, 235, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), 90, default, 1f);
			/*int frameSpeed = 3;

			Projectile.frameCounter++;

			if (Projectile.frameCounter >= frameSpeed) {
				Projectile.frameCounter = 0;
				Projectile.frame++;

				if (Projectile.frame >= Main.projFrames[Projectile.type]) {
					Projectile.frame = 0;
				}
			}*/
			Projectile.rotation = Projectile.rotation + MathHelper.ToRadians(15);
			
			
			if (isSkillProj > 4) {
				Projectile.Center = owner.GetModPlayer<BooTaoPlayer>().EntelechiaStoreMouse;// + new Vector2(-36, -47);
			}
			else {
				Projectile.Center = owner.Center;// + new Vector2(-36, -44);
			}
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			Player owner = Main.player[Projectile.owner];
			target.AddBuff(ModContent.BuffType<EntelechiaBuff>(), 300);
			
			//target.GetLifeStats(out int statLife, out int statLifeMax);
			if (target.damage > 0) {
				owner.Heal(6);
				if (owner.GetModPlayer<BooTaoPlayer>().EntelechiaMaxHPBuff < 200) {
					owner.GetModPlayer<BooTaoPlayer>().EntelechiaMaxHPBuff += 10;
				}
			}
		}
		
		public override bool? CanCutTiles() {
			return false;
		}
		
		
		//public override void OnKill(int timeLeft) {
    }
}
