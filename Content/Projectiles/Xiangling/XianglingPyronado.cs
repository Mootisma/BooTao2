using BooTao2.Content.Items.Xiangling;
using BooTao2.Content.Buffs.Xiangling;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace BooTao2.Content.Projectiles.Xiangling
{
	public class XianglingPyronado : ModProjectile
	{
		public override void SetStaticDefaults() {
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = false;
		}

		public sealed override void SetDefaults() {
			Projectile.width = 80;
			Projectile.height = 80;
			Projectile.tileCollide = false;
			// These below are needed for a minion weapon
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
			return true;
		}

		int deg = 0;
		public override void AI() {
			Player owner = Main.player[Projectile.owner];

			if (!CheckActive(owner)) {
				return;
			}
			
			/*
			float speed = 6f;
			float rotation = MathHelper.ToRadians(90);
			Vector2 direction = Projectile.Center - owner.Center;
			direction = direction.SafeNormalize(Vector2.UnitX);
			direction *= speed;
			direction = direction.RotatedBy(rotation);
			Projectile.velocity = direction;
			*/
			
			deg = deg + 3 % 360;
            float rad = MathHelper.ToRadians(deg); //Convert degrees to radians
            float dist = 200f; //Distance away from the player
			Projectile.position.X = owner.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = owner.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
			Projectile.rotation = Projectile.rotation + MathHelper.ToRadians(10);

			Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.2f);
			for (int d = 0; d < 3; d++) {
				Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
				Dust.NewDustPerfect(Projectile.Center + speed * 32, 127, speed * 2, 120, default, 1f);
			}
		}

		// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
		private bool CheckActive(Player owner) {
			if (owner.dead || !owner.active) {
				owner.ClearBuff(ModContent.BuffType<XianglingBuff>());
				return false;
			}
			if (owner.HasBuff(ModContent.BuffType<XianglingBuff>())) {
				Projectile.timeLeft = 2;
			}
			return true;
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
		}
	}
}
/*
https://docs.tmodloader.net/docs/stable/class_projectile.html
https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators#remainder-operator-
https://github.com/ThePaperLuigi/The-Stars-Above/blob/main/Projectiles/Summon/KroniicPrincipality/TemporalTimepiece2.cs
https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/Items/Weapons/ExampleGun.cs#L79
https://learn.microsoft.com/en-us/previous-versions/windows/silverlight/dotnet-windows-silverlight/bb197816(v=xnagamestudio.35)
https://docs.tmodloader.net/docs/1.4-stable/class_terraria_1_1_utils.html
https://github.com/tModLoader/tModLoader/wiki/Geometry
*/