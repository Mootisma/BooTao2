using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Projectiles.Fiammetta {
	public class FiammettaExplosionProj : ModProjectile {
		// You can use a vanilla texture for your item by using the format: "Terraria/Item_<Item ID>".
		//public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Flames;
		
		public override void SetStaticDefaults() {
			Main.projFrames[Projectile.type] = 5;
		}
		
		public override void SetDefaults() {
			//Projectile.CloneDefaults(ProjectileID.Flames);
			//AIType = ProjectileID.Flames;
			
			Projectile.width = 160;
			Projectile.height = 132;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.9f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 30;
			Projectile.penetrate = -1;
			
			Projectile.scale = 1f;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
			/* When used in conjunction with usesLocalNPCImmunity, 
			determines how many ticks must pass before this projectile can deal damage again 
			to the same npc. A value of -1 indicates that it can only hit a specific npc once. 
			The default value of -2 has no effect, so this must be assigned if usesLocalNPCImmunity is true. */
		}
		
		//public override void Resize (int newWidth, int newHeight) {}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
			float distancebtwn = Vector2.Distance(target.Center, Projectile.Center);
			if (distancebtwn < 30) {
				// modifiers.CritDamage += 2f;
				// modifiers.ScalingArmorPenetration += 0.5f;
				// modifiers.ArmorPenetration += 10f;
				modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
					hitInfo.Damage *= 2;
				};
			}
		}
		
		public override void AI() {
			int frameSpeed = 5;

			Projectile.frameCounter++;

			if (Projectile.frameCounter >= frameSpeed) {
				Projectile.frameCounter = 0;
				Projectile.frame++;

				if (Projectile.frame >= Main.projFrames[Projectile.type]) {
					Projectile.Kill();
				}
			}
		}
	}
}
/*
https://github.com/tModLoader/tModLoader/blob/1.4.4/ExampleMod/Content/Items/Weapons/HitModifiersShowcase.cs
*/