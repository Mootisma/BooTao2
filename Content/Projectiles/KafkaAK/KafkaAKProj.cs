using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using BooTao2.Content.Buffs.KafkaAK;
using Terraria.Audio;

namespace BooTao2.Content.Projectiles.KafkaAK {
	public class KafkaAKProj : ModProjectile {
		SoundStyle KafkaSkill = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/KafkaAK/KafkaAKSkillStart") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		SoundStyle KafkaEnd = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/KafkaAK/KafkaAKSkillEnd") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		public override void SetDefaults() {
			Projectile.width = 200;
			Projectile.height = 200;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = false;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.alpha = 0;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 360;
			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}
		
		public override void OnSpawn (IEntitySource source){
			SoundEngine.PlaySound(KafkaSkill, Projectile.Center);
			foreach (var npc in Main.ActiveNPCs) {
				if (!npc.boss && !npc.friendly){//cannot put bosses and town npcs to sleep
					float distancebtwn = Vector2.Distance(Projectile.Center, npc.Center);
					if (distancebtwn < 140) {
						npc.AddBuff(ModContent.BuffType<KafkaAKBuff>(), 300);
					}
				}
			}
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
		}
		
		public override void AI() {
			//Lighting.AddLight(Projectile.Center, Color.Purple.ToVector3() * 2f);
			if (Projectile.timeLeft == 59) {
				SoundEngine.PlaySound(KafkaEnd, Projectile.Center);
				Projectile.friendly = true;
				for (int d = 0; d < 150; d++) {
					Vector2 speed = Main.rand.NextVector2Circular(100f, 100f);
					Dust.NewDustPerfect(Projectile.Center + speed, 61, null, 120, default, 1f);
					Dust.NewDustPerfect(Projectile.Center + speed, 75, null, 120, default, 1f);
				}
			}
			if (Projectile.timeLeft > 60) {
				for (int d = 0; d < 3; d++) {
					Vector2 speed = Main.rand.NextVector2Circular(1f, 1f);
					Dust.NewDustPerfect(Projectile.Center + speed * 100, 56, null, 120, default, 1f);
				}
			}
			
		}
		
		public override bool? CanCutTiles() {
			return false;
		}
	}
}
/*

https://terraria.wiki.gg/wiki/Dust_IDs
https://honkai-star-rail.fandom.com/wiki/Kafka/Media?file=Kafka_Character_Preview_Details_7.gif#Introduction_Details
https://docs.tmodloader.net/docs/stable/struct_n_p_c_1_1_hit_modifiers.html
https://docs.tmodloader.net/docs/stable/class_dust.html
https://docs.tmodloader.net/docs/stable/class_mod_projectile.html

*/