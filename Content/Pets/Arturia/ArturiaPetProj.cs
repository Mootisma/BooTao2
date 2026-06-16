using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using System;
using System.IO;
// https://github.com/tModLoader/tModLoader/blob/1.4.5/ExampleMod/Content/Pets/ExamplePet/ExamplePetProjectile.cs
// https://docs.tmodloader.net/docs/stable/class_player.html
// https://github.com/ThePaperLuigi/The-Stars-Above
// examplemod's ExampleOnBuyItem
namespace BooTao2.Content.Pets.Arturia
{
	public class ArturiaPetProj : ModProjectile
	{
		public int still = 0;
		public static LocalizedText DeathMessage { get; private set; }
		
		public override void SetStaticDefaults() {
			Main.projPet[Type] = true;
			
			DeathMessage = this.GetLocalization(nameof(DeathMessage));
		}

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.DirtiestBlock); // Copy the stats of the Dirtiest Block

			AIType = ProjectileID.DirtiestBlock; // Mimic as the Dirtiest Block during AI.
			
			Projectile.light = 0.1f;
		}

		public override bool PreAI() {
			Player player = Main.player[Projectile.owner];

			player.petFlagDirtiestBlock = false; // Relic from AIType

			return true;
		}

		public override void AI() {
			Player player = Main.player[Projectile.owner];

			// Keep the projectile from disappearing as long as the player isn't dead and has the pet buff.
			if (!player.dead && player.HasBuff(ModContent.BuffType<ArturiaPetBuff>())) {
				Projectile.timeLeft = 2;
			}
			
			// if you stand still for 30 seconds straight, you die
			if (player.velocity == Vector2.Zero){
				still++;
			}
			else {
				still = 0;
			}
			if (still > 10800) {
				player.KillMe(PlayerDeathReason.ByCustomReason(DeathMessage.ToNetworkText(player.name)), 9999, 0);
				still = 0;
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2(0, -65), Vector2.Zero, ModContent.ProjectileType<ArturiaExplosionProj>(), 9999, 20f, Projectile.owner);
			}
		}
	}
}