using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;
using System;
using BooTao2.Content.Projectiles.Fiammetta;
using BooTao2.Content.Projectiles.Qingque;

namespace BooTao2.Content.Projectiles.Xiao
{
	public class XiaoProj : ModProjectile
	{
		// Define the range of the Spear Projectile. These are overridable properties, in case you'll want to make a class inheriting from this one.
		protected virtual float HoldoutRangeMax => 48f;
		
		public override string Texture => "BooTao2/Content/Projectiles/HuTao/HuTaoHomaProj";
		
		SoundStyle XiaoPlungeImpact = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Xiao/XiaoPlungeImpact") {
			Volume = 0.2f,
			PitchVariance = 0.1f,
			MaxInstances = 3,
		};

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.Spear); // Clone the default values for a vanilla spear. Spear specific values set for width, height, aiStyle, friendly, penetrate, tileCollide, scale, hide, ownerHitCheck, and melee.
			Projectile.width = 50;
			Projectile.height = 50;
			Projectile.timeLeft = 6000;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}

		public override bool PreAI() {
			Player player = Main.player[Projectile.owner]; // Since we access the owner player instance so much, it's useful to create a helper local variable for this
			player.heldProj = Projectile.whoAmI; // Update the player's held projectile id

			if (player.velocity.Y < 0.1f && Projectile.timeLeft > 5 && Projectile.timeLeft < 5999) {
				Projectile.timeLeft = 5;
			}

			Projectile.Center = player.MountedCenter + new Vector2(0, HoldoutRangeMax);

			Projectile.rotation += MathHelper.ToRadians(45f);
			
			Dust.NewDust(Projectile.Center, 3, 3, 109, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), 0, default, 1f);
			Dust.NewDust(Projectile.Center, 3, 3, 89, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), 0, default, 1f);
			Dust.NewDustPerfect(Projectile.Center, 89, null, 0, default, 1f);

			return false; // Don't execute vanilla AI.
		}
		
		public override void OnKill(int timeLeft) {
			Player player = Main.player[Projectile.owner];
			SoundEngine.PlaySound(XiaoPlungeImpact, player.Center);
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2(0, -65), Vector2.Zero, ModContent.ProjectileType<QingqueExplosionProj>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1);
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
		}
	}
	
	public class XiaoSkillProj : ModProjectile
	{
		public override string Texture => "BooTao2/Content/Projectiles/HuTao/HuTaoHomaProj";

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.Spear); // Clone the default values for a vanilla spear. Spear specific values set for width, height, aiStyle, friendly, penetrate, tileCollide, scale, hide, ownerHitCheck, and melee.
			Projectile.width = 50;
			Projectile.height = 50;
			Projectile.timeLeft = 44;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}

		public override bool PreAI() {
			Player player = Main.player[Projectile.owner]; // Since we access the owner player instance so much, it's useful to create a helper local variable for this
			player.heldProj = Projectile.whoAmI; // Update the player's held projectile id

			Projectile.Center = player.MountedCenter + Projectile.velocity * 100;

			// Apply proper rotation to the sprite.
			if (Projectile.spriteDirection == -1) {
				// If sprite is facing left, rotate 45 degrees
				Projectile.rotation += MathHelper.ToRadians(45f);
			}
			else {
				// If sprite is facing right, rotate 135 degrees
				Projectile.rotation += MathHelper.ToRadians(135f);
			}

			Dust.NewDust(Projectile.Center, 3, 3, 109, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), 0, default, 1f);
			Dust.NewDust(Projectile.Center, 3, 3, 89, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), 0, default, 1f);
			Dust.NewDustPerfect(Projectile.Center, 89, null, 0, default, 1f);

			return false; // Don't execute vanilla AI.
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
		}
	}
}
