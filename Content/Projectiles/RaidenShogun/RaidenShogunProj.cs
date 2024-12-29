using BooTao2.Content.Items.RaidenShogun;
using BooTao2.Content.Buffs.RaidenShogun;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace BooTao2.Content.Projectiles.RaidenShogun
{
	public class RaidenShogunProj : ModProjectile
	{
		public override void SetStaticDefaults() {
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = false;
		}

		public sealed override void SetDefaults() {
			Projectile.width = 50;
			Projectile.height = 50;
			Projectile.tileCollide = false;
			Projectile.alpha = 100;
			Projectile.friendly = true; // 
			Projectile.minion = true; // Declares this as a minion
			Projectile.DamageType = DamageClass.Summon; // Declares the damage type
			Projectile.minionSlots = 1f; // Amount of slots
			Projectile.penetrate = -1; // Needed so the minion doesn't despawn
			//
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 50;// measured in ticks
		}
		
		// Here you can decide if your minion breaks things like grass or pots
		public override bool? CanCutTiles() {
			return false;
		}

		public override bool MinionContactDamage() {
			return false;
		}

		int counter = 0;
		public override void AI() {
			Player owner = Main.player[Projectile.owner];

			if (!CheckActive(owner)) {
				return;
			}
			
			Projectile.position = owner.position + new Vector2(-42, -70);

			Lighting.AddLight(Projectile.Center, Color.Purple.ToVector3() * 0.5f);
			
			foreach (var player in Main.ActivePlayers) {
				if (player.GetModPlayer<BooTaoPlayer>().RaidenShogunCooldown > 0)
					player.GetModPlayer<BooTaoPlayer>().RaidenShogunCooldown--;
			}
			
			if (counter > 53) {
				owner.GetModPlayer<BooTaoPlayer>().RaidenShogunSkillDamage = Projectile.damage;
				foreach (var player in Main.ActivePlayers) {
					//only buff allies on the same team
					// || player.team == 0 checks if the player is in a team (singleplayer cant pick teams though)
					// (Main.netMode != NetmodeID.SinglePlayer)
					if (owner.team != player.team) {
						continue;
					}
					
					float distancebtwn = Vector2.Distance(Projectile.Center, player.Center);
					if (distancebtwn < 1500) {
						player.ClearBuff(ModContent.BuffType<RaidenShogunAura>());
						player.AddBuff(ModContent.BuffType<RaidenShogunAura>(), 120, true);
					}
				}
				counter = 0;
			}
			counter++;
		}

		// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
		private bool CheckActive(Player owner) {
			if (owner.dead || !owner.active) {
				owner.ClearBuff(ModContent.BuffType<RaidenShogunBuff>());
				return false;
			}
			if (owner.HasBuff(ModContent.BuffType<RaidenShogunBuff>())) {
				Projectile.timeLeft = 2;
			}
			return true;
		}
	}
}
/*

*/