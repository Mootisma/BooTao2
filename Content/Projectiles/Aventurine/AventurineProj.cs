using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BooTao2.Content.Projectiles.Aventurine;
using BooTao2.Content.Buffs.Aventurine;

namespace BooTao2.Content.Projectiles.Aventurine {
	public class AventurineProj : ModProjectile {
		public ref float RealBool => ref Projectile.ai[0];
		public ref float PlayerDefense => ref Projectile.ai[1];
		
		public override void SetDefaults() {
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.6f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 60;
		}
		
		public override bool? CanCutTiles() {
			return false;
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
		}
		
		public override void AI() {
			Player player = Main.player[Projectile.owner];
			if (RealBool > 0 && Projectile.timeLeft <= 50 && Projectile.timeLeft > 3) {
				foreach (var ligma in Main.ActivePlayers) {
					if (ligma.team != player.team) {
						continue;
					}
					ligma.GetModPlayer<BooTaoPlayer>().AventurineShieldHP += player.statDefense;
					if (ligma.GetModPlayer<BooTaoPlayer>().AventurineShieldHP > player.statDefense * 2)
						ligma.GetModPlayer<BooTaoPlayer>().AventurineShieldHP = player.statDefense * 2;
					ligma.AddBuff(ModContent.BuffType<AventurineShield>(), 59);
					ligma.ClearBuff(ModContent.BuffType<AventurineShieldOrigin>());
				}
				player.AddBuff(ModContent.BuffType<AventurineShieldOrigin>(), 59);
				Projectile.timeLeft = 3;
			}
			Dust.NewDust(Projectile.position, 0, 0, 19, 0, 0, 150, default, 1f);
			Dust.NewDust(Projectile.position, 0, 0, 10, 0, 0, 150, default, 1f);
			Dust.NewDust(Projectile.position, 0, 0, 228, 0, 0, 150, default, 1f);
			//Dust.NewDust(Projectile.position, 0, 0, 170, 0, 0, 150, default, 1f);
		}
	}
}