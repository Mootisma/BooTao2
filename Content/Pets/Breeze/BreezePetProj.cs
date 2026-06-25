using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using BooTao2.Content.Projectiles.Breeze;

namespace BooTao2.Content.Pets.Breeze
{
	public class BreezePetProj : ModProjectile
	{
		SoundStyle BreezeVO1 = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Breeze/BreezeVO1") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 2,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle BreezeVO2 = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Breeze/BreezeVO2") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 2,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle BreezeVO3 = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Breeze/BreezeVO3") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 2,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle BreezeVO4 = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Breeze/BreezeVO4") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 2,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle BreezeVO5 = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Breeze/BreezeVO5") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 2,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle BreezeVO6 = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Breeze/BreezeVO6") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 2,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle BreezeVO7 = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Breeze/BreezeVO7") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 2,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle BreezeVO8 = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Breeze/BreezeVO8") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 2,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle BreezeVOBeach = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Breeze/BreezeVOBeach") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 2,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle BreezeVOForest = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Breeze/BreezeVOForest") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 2,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle BreezeVOMushroom = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Breeze/BreezeVOMushroom") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 2,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle BreezeVOWindy = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Breeze/BreezeVOWindy") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 2,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle BreezeVOweird = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Breeze/BreezeVOweird") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 2,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		private int timer1 = 0;
		private int timer2 = 0;
		private int timer3 = 0;
		private bool foundTarget = false;
		private float distanceFromTarget = 1f;
		private Vector2 targetCenter = Vector2.Zero;
		
		public override void SetStaticDefaults() {
			Main.projPet[Type] = true;
		}

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.BabySkeletronHead); // Copy the stats of the BabySkeletronHead

			AIType = ProjectileID.BabySkeletronHead; // Mimic as the BabySkeletronHead during AI.
			
			Projectile.light = 1f;
		}

		public override bool PreAI() {
			Player player = Main.player[Projectile.owner];

			player.petFlagSkeletronPet = false; // Relic from AIType

			return true;
		}

		public override void AI() {
			Player player = Main.player[Projectile.owner];

			// Keep the projectile from disappearing as long as the player isn't dead and has the pet buff.
			if (!player.dead && player.HasBuff(ModContent.BuffType<BreezePetBuff>())) {
				Projectile.timeLeft = 2;
			}
			
			// only run SearchForTargets once every 60 frames to potentially reduce lag
			timer3++;
			if (timer3 >= 60) {
				SearchForTargets(player, out foundTarget, out distanceFromTarget, out targetCenter);
				timer3 = 0;
				if (!foundTarget) {
					timer1++;
				}
				else {
					timer2++;
				}
			}
			
			// out of combat for roughly X cumulative seconds, play a voiceline
			if (timer1 >= 60) {
				timer1 = 0;
				
				int choosevo = Main.rand.Next(7);//1 in x chance; 0 inclusive to x exclusive
				switch (choosevo)
				{
					case 0:
						SoundEngine.PlaySound(BreezeVO1, Projectile.Center);
						break;
					case 1:
						SoundEngine.PlaySound(BreezeVO8, Projectile.Center);
						break;
					case 2:
						SoundEngine.PlaySound(BreezeVO3, Projectile.Center);
						break;
					case 3:
						SoundEngine.PlaySound(BreezeVO4, Projectile.Center);
						break;
					case 4:
						SoundEngine.PlaySound(BreezeVO5, Projectile.Center);
						break;
					case 5:
						SoundEngine.PlaySound(BreezeVO6, Projectile.Center);
						break;
					case 6:
						if (player.ZoneForest) { SoundEngine.PlaySound(BreezeVOForest, Projectile.Center); }
						else if (Condition.HappyWindyDay.IsMet()) { SoundEngine.PlaySound(BreezeVOWindy, Projectile.Center); }
						else if (player.ZoneBeach) { SoundEngine.PlaySound(BreezeVOBeach, Projectile.Center); }
						else if (player.ZoneGlowshroom) { SoundEngine.PlaySound(BreezeVOMushroom, Projectile.Center); }
						else if (player.ZoneShimmer) { SoundEngine.PlaySound(BreezeVOweird, Projectile.Center); }
						else if (player.velocity == Vector2.Zero) { SoundEngine.PlaySound(BreezeVO2, Projectile.Center); }
						else { SoundEngine.PlaySound(BreezeVO7, Projectile.Center); }
						break;
				}
			}
			
			// in combat for roughly X cumulative seconds, violently explode
			if (timer2 >= 180) {
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), targetCenter, Vector2.Zero, ModContent.ProjectileType<BreezeProj2>(), 5, 1f, Projectile.owner);
				timer2 = 0;
			}
		}
		
		private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter) {
			distanceFromTarget = 1000f;
			targetCenter = Projectile.position;
			foundTarget = false;

			foreach (var npc in Main.ActiveNPCs) {
				if (npc.CanBeChasedBy()) {
					float between = Vector2.Distance(npc.Center, Projectile.Center);
					bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
					bool inRange = between < distanceFromTarget;

					if ((closest && inRange) || !foundTarget) {
						distanceFromTarget = between;
						targetCenter = npc.Center;
						foundTarget = true;
					}
				}
			}
		}
	}
}
/*
https://github.com/tModLoader/tModLoader/blob/1.4.5/ExampleMod/Content/Pets/ExamplePet/ExamplePetProjectile.cs
https://docs.tmodloader.net/docs/stable/class_player.html
https://github.com/ThePaperLuigi/The-Stars-Above
examplemod's ExampleOnBuyItem
https://github.com/tModLoader/tModLoader/blob/1.4.5/ExampleMod/Content/Items/Weapons/ExampleGun.cs Main.rand.Next(3)
https://www.w3schools.com/cs/cs_switch.php
https://github.com/tModLoader/tModLoader/wiki/Conditions
https://github.com/tModLoader/tModLoader/blob/stable/patches/tModLoader/Terraria/Condition.cs
https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/selection-statements

*/