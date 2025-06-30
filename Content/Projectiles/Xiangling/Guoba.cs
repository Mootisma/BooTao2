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
	public class Guoba : ModProjectile
	{
		public ref float Cooldown => ref Projectile.ai[0];
		
		public override void SetStaticDefaults() {
			Main.projPet[Projectile.type] = true;
		}
		
		public override void SetDefaults() {
			Projectile.width = 56;
			Projectile.height = 56;
			Projectile.aiStyle = 0;
			Projectile.friendly = false;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.9f;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.sentry = true;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.timeLeft = Projectile.SentryLifeTime;
		}
		
		int counter = 0;
		public override void AI() {
			int Start = 180 - (int)Cooldown;
			int End = 201 - (int)Cooldown;
			counter++;
			Player owner = Main.player[Projectile.owner];
			if (counter >= Start && Main.myPlayer == owner.whoAmI) {
				if (counter > End) {
					counter = 0;
				}
				if (counter % 5 == 0){
					Vector2 todokete = (owner.GetModPlayer<BooTaoPlayer>().GuobaStoreMouse - Projectile.Center).SafeNormalize(Vector2.UnitX) * 7f;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, todokete, ProjectileID.Flames, Projectile.damage, Projectile.knockBack, Projectile.owner);
				}
			}
		}
		
		public override bool? CanCutTiles() {
			return false;
		}

		public override bool MinionContactDamage() {
			return false;
		}
	}
}
/*
https://docs.tmodloader.net/docs/stable/class_projectile.html
https://github.com/tModLoader/tModLoader/tree/1.4.4/ExampleMod/Content/Items/Weapons
https://github.com/ThePaperLuigi/The-Stars-Above/blob/main/Projectiles/Summon/CandiedSugarball/Charsugar.cs
https://hst.sh/iqebivoluk.cs

*/