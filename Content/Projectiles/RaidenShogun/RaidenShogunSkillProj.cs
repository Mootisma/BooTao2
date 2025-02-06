using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace BooTao2.Content.Projectiles.RaidenShogun {
	public class RaidenShogunSkillProj : ModProjectile {
		public override void SetStaticDefaults() {
			Main.projFrames[Projectile.type] = 7;
		}
		
		public override void SetDefaults() {
			Projectile.width = 100;
			Projectile.height = 100;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.1f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 21;
			Projectile.penetrate = -1;
			
			Projectile.scale = 1f;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
			/* When used in conjunction with usesLocalNPCImmunity, 
			determines how many ticks must pass before this projectile can deal damage again 
			to the same npc. A value of -1 indicates that it can only hit a specific npc once. 
			The default value of -2 has no effect, so this must be assigned if usesLocalNPCImmunity is true. */
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
		}
		
		// public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone){}
		
		public override void AI() {
			Dust.NewDust(Projectile.Center, 3, 3, 173, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), 120, default, 1f);
			Dust.NewDustPerfect(Projectile.Center, 255, null, 120, default, 1f);
			int frameSpeed = 3;

			Projectile.frameCounter++;

			if (Projectile.frameCounter >= frameSpeed) {
				Projectile.frameCounter = 0;
				Projectile.frame++;
			}
			
			if (Projectile.timeLeft == 19) {
				foreach (var player in Main.ActivePlayers) {
					if (Main.player[Projectile.owner].team == player.team) {
						player.GetModPlayer<BooTaoPlayer>().RaidenShogunCooldown = 52;
					}
				}
			}
		}
		
		//public override void OnSpawn(IEntitySource source) {}
	}
}
/*
https://ugokawaii.com/en/others/slash-effect-2/
*/