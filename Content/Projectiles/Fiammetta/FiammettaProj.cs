using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Projectiles.Fiammetta {
	public class FiammettaProj : ModProjectile {
		public override void SetDefaults() {
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.1f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 80;
			Projectile.penetrate = 1;
		}
		
		public override void AI() {
			if (Projectile.owner == Main.myPlayer) {
				Player player = Main.LocalPlayer;
				if (player.GetModPlayer<BooTaoPlayer>().FiammettaS3 == 1) {
					Projectile.penetrate = -1;
					// redirect the projectile to the mouse after it was sent straight up
					if (Projectile.timeLeft == 55) {
						Projectile.velocity = (player.GetModPlayer<BooTaoPlayer>().FiammettaStoreMouse - Projectile.Center).SafeNormalize(Vector2.UnitX);
						Projectile.velocity.X *= 40f;
						Projectile.velocity.Y *= 40f;
					}
				}
				
				// kill the projectile when it gets to the mouse position
				float distancebtwn = Vector2.Distance(player.GetModPlayer<BooTaoPlayer>().FiammettaStoreMouse, Projectile.Center);
				if (distancebtwn < 10 && ((Projectile.timeLeft < 55 && player.GetModPlayer<BooTaoPlayer>().FiammettaS3 == 1) || player.GetModPlayer<BooTaoPlayer>().FiammettaS3 == 0)) {
					Projectile.Kill();//
				}
			}
			//modifiers.SourceDamage.Flat += (int)(target.statLifeMax * 0.2);
		}
		
		public override void OnKill(int timeLeft) {
			Player player = Main.LocalPlayer;
			// spawn the explosion projectile after this projectile does its job
			//var ffaf = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(0, 0), ProjectileID.Flames, Projectile.damage, Projectile.knockBack, Projectile.owner);
			//ffaf.scale = 2f;
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(0, 0), ModContent.ProjectileType<FiammettaExplosionProj>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1);
		}// + new Vector2(0, -60)
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			//Player player = Main.LocalPlayer;
			modifiers.DamageVariationScale *= 0f;
			modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
				hitInfo.Damage = 1;
			};
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			Player player = Main.LocalPlayer;
			// Projectile.damage = 1;
			if (player.GetModPlayer<BooTaoPlayer>().FiammettaSP < 15 && player.GetModPlayer<BooTaoPlayer>().FiammettaS3 == 0) {
				player.GetModPlayer<BooTaoPlayer>().FiammettaSP++;
			}
		}
	}
}
/*
https://docs.tmodloader.net/docs/stable/class_mod_item.html
https://docs.tmodloader.net/docs/stable/class_mod_projectile.html
https://docs.tmodloader.net/docs/stable/struct_n_p_c_1_1_hit_modifiers.html
https://docs.tmodloader.net/docs/stable/struct_stat_modifier.html#a9176f23c60202e7bfc569a487a2d1f40
https://github.com/tModLoader/tModLoader/blob/1.4.4/ExampleMod/Content/Items/Weapons/HitModifiersShowcase.cs
https://docs.tmodloader.net/docs/stable/class_player.html
https://terraria.wiki.gg/wiki/Projectile_IDs
https://terraria.wiki.gg/wiki/File:Flamethrower_(projectile).gif
https://github.com/tModLoader/tModLoader/wiki/Coordinates
https://github.com/ThePaperLuigi/The-Stars-Above/blob/main/Projectiles/Summon/KroniicPrincipality/TemporalTimepiece2.cs
*/