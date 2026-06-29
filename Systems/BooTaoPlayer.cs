using BooTao2.Content.Buffs;
using BooTao2.Content.Buffs.BloodBlossomBuff;
using BooTao2.Content.Buffs.Aventurine;
using BooTao2.Content.Buffs.Escoffier;
using BooTao2.Content.Items.HuTao;
using BooTao2.Content.Items.RaidenShogun;
using BooTao2.Content.Items.Herta;
using BooTao2.Content.Projectiles.RaidenShogun;
using BooTao2.Content.Buffs.RaidenShogun;
using BooTao2.Content.Buffs.Entelechia;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace BooTao2.Systems
{
	public class BooTaoPlayer : ModPlayer
	{
		public bool Magnet;
		public bool[] HomaPickaxes = new bool[7];
		public static bool[] HomaConfig = new bool[7];
		public static bool Magnet2Config;
		public bool Magnet2;
		public static bool BuffClutterConfig;
		public bool BuffClutter;
		
		public bool HomaHealing1;
		public bool HomaHealing2;
		public bool lifeRegenDebuff;
		public bool BloodBlossom;
		public bool NingHolding; // player.GetModPlayer<BooTaoPlayer>().NingHolding
		public bool NingJade1State;
		public bool NingJade2State;
		public bool NingJade3State;
		public bool NingJadeScreen;
		//
		public int coold = 0;
		public int der = 0;
		public float HuTaoHPDmgBuff = 0;
		//
		public int NingNumBuff = 0;
		//
		public float FurinaDmgBuff = 0;
		//
		public float YelanDmgBuff = 0f;
		//
		public int ThornsSP = 0;
		public int ThornsS3numUses = 0;
		public int ThornsDOTstack = 0;
		public int ThornsS3duration = 0;
		public int ThornsHealingCD = 0;
		public bool ThornsHealing;
		//
		public int FiammettaS3 = 0;
		public int FiammettaSP = 0;
		public Vector2 FiammettaStoreMouse = Vector2.Zero;
		public bool FiammettaHealing;
		//
		public float SkadiATK = 0;
		public int SkadiSP = 0;
		public bool SkadiHealing;
		//
		public int MostimaSkillDuration = 0;
		public int MostimaSkillSP = 2400;
		public bool MostimaSkill;
		public bool MostimaHealing;
		//
		public bool SkillReady;
		//
		public int AventurineShieldHP = 0;
		public int AventurineBlindBet = 0;
		//
		public bool RaidenShogunSkill;
		public int RaidenShogunSkillDamage = 0;
		public int RaidenShogunCooldown = 0;
		//
		public int BeehunterHolding = 0;
		public int BeehunterStacks = 0;
		public int JackieHolding = 0;
		public int JackieSP = 0;
		public bool JackieDodged;
		//
		public bool QingqueHolding;
		public int[] QingqueTiles = [0,1,2,3];
		public bool QingqueFUA;
		//
		public float LaPlumaPassive = 0f;
		public bool LaPlumaHolding;
		public bool LaPlumaLifeRegen;
		//
		public int KroosSP = 0;
		//
		public int CaperSP = 15;
		//
		public int XiaoSP = 1200;
		//
		public bool BlackSwanHolding;
		//
		public int GreyThroatSP = 15;
		//
		public int WisadelAmmo = 0;
		//
		public int TeleportCooldown = 0;//bronya, sparkle
		//
		public Vector2 GuobaStoreMouse = Vector2.Zero;
		//
		public bool LapplandSkill;
		//
		public int JuFufuMight = 0;
		//
		public Vector2 EntelechiaStoreMouse = Vector2.Zero;
		public int EntelechiaMaxHPBuff = 0;
		public bool EntelechiaRevive;
		public int EnteReviveCD = 0;
		//
		public int LemuenAmmo = 0;
		public int LemuenSP = 32;
		//
		public bool BreezeHealing;
		public bool croutonHealing;
		public bool RoyalBrooch;
		public int RBcd = 0;
		public bool MedicineSticks;
		public int MScd = 0;
		public bool MSn;
		public bool Camper;
		public bool Camper2;
		public bool ServalDoT;
		
		public override void ResetEffects()
		{
			Magnet = false;
			Magnet2 = false;
			lifeRegenDebuff = false;
			BloodBlossom = false;
			NingHolding = false;
			NingJade1State = false;
			NingJade2State = false;
			NingJade3State = false;
			SkillReady = false;
			RaidenShogunSkill = false;
			QingqueHolding = false;
			LaPlumaHolding = false;
			LaPlumaLifeRegen = false;
			if (!Player.HasBuff(ModContent.BuffType<HomaPickaxeBuff>())) {
				HomaPickaxes = new bool[7];
			}
			BlackSwanHolding = false;
			NingJadeScreen = false;
			LapplandSkill = false;
			EntelechiaRevive = false;
			RoyalBrooch = false;
			MedicineSticks = false;
			MSn = false;
			HomaHealing1 = false;
			HomaHealing2 = false;
			ThornsHealing = false;
			SkadiHealing = false;
			BreezeHealing = false;
			FiammettaHealing = false;
			MostimaHealing = false;
			croutonHealing = false;
			ServalDoT = false;
			Camper = false;
			Camper2 = false;
		}
		
		public bool CanUseHuTaoE() {
			if (coold == 0) {
				HuTaoHPDmgBuff = (float)((Player.statLife * 0.3) / 100);
				Player.statLife = (int)(Player.statLife * 0.7);
				coold = 960;
				der = 540;
				return true;
			}
			return false;
		}
		
		//https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/ranges-indexes
		public bool[] GetHomaConfig() {
			return HomaConfig;
		}
		public bool GetMagnet2Config() {
			return Magnet2Config;
		}
		public bool GetBuffClutterConfig() {
			return BuffClutterConfig;
		}
		
		public override void ModifyHurt(ref Player.HurtModifiers modifiers) {
			if (der > 0) {
				modifiers.FinalDamage *= 0.1f;
			}
			
			// if holding Entelechia item, and revive is on cooldown, gain 20% damage reduction (idk if i can make it physical hits only)
			if (Player.HasBuff(ModContent.BuffType<EntelechiaBuff>()) && !EntelechiaRevive && EnteReviveCD > 0){
				modifiers.FinalDamage *= 0.8f;
			}
			
			//https://docs.tmodloader.net/docs/stable/class_main_1_1_current_frame_flags.html
			if (Camper2) {
				if (Main.CurrentFrameFlags.AnyActiveBossNPC || Player.velocity != Vector2.Zero) modifiers.FinalDamage *= 10f;
				else modifiers.FinalDamage *= 0.3f;
			}
		}
		
		public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource) {
			ThornsSP = 0;
			ThornsS3numUses = 0;
			FiammettaS3 = 0;
			FiammettaSP = 0;
			MostimaSkillDuration = 0;
			MostimaSkillSP = 0;
			SkadiSP = 0;
			AventurineShieldHP = 0;
			JackieDodged = false;
			LaPlumaPassive = 0f;
			KroosSP = 0;
			EntelechiaMaxHPBuff = 0;
			EntelechiaRevive = false;
			EnteReviveCD = 0;
			LemuenAmmo = 0;
			LemuenSP = 0;
		}
		
		public override void OnRespawn() {
			AventurineShieldHP = 0;//https://docs.tmodloader.net/docs/stable/class_mod_player.html
			AventurineBlindBet = 0;
		}
		
		public override void NaturalLifeRegen (ref float regen)
		{
			if(HomaHealing1){
				regen *= 1.1f;
			}
		}
		
		public override void UpdateLifeRegen() {
			if (HomaHealing1) {//campfire
				Player.lifeRegen += 1;
			}
			if (HomaHealing2) {//heart lantern
				Player.lifeRegen += 2;
			}
			if (ThornsHealing) {
				Player.lifeRegen += 10;
			}
			if (SkadiHealing) {
				if (SkadiSP > 56){
					Player.lifeRegen += (int)(SkadiATK / 10);
				}
				Player.lifeRegen += (int)(SkadiATK / 10);
			}
			if (MostimaHealing) {
				Player.lifeRegen += 2;
			}
			if (croutonHealing) {
				Player.lifeRegen += 20;
			}
			if (BreezeHealing) {
				Player.lifeRegen += 1;
			}
			
			if (LaPlumaLifeRegen) {
				if (Player.lifeRegen > 0)
					Player.lifeRegen = 0;
				Player.lifeRegenTime = 0;
			}
        }
		
		public override void UpdateBadLifeRegen() {
			if (lifeRegenDebuff) {
				// These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects
				if (Player.lifeRegen > 0)
					Player.lifeRegen = 0;
				// Player.lifeRegenTime uses to increase the speed at which the player reaches its maximum natural life regeneration
				// So we set it to 0, and while this debuff is active, it never reaches it
				Player.lifeRegenTime = 0;
				// lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second
				Player.lifeRegen -= 16;
			}
			if (ServalDoT) {
				if (Player.lifeRegen > 0)
					Player.lifeRegen = 0;
				Player.lifeRegenTime = 0;
				Player.lifeRegen -= 16;
			}
		}
		
		public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright) {
			if (BloodBlossom) {
				// These color adjustments match the withered armor debuff visuals.
				g *= 0.5f;
				r *= 0.75f;
			}
		}
		
		//https://docs.tmodloader.net/docs/stable/class_mod_player.html
		
		/*public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot) {
			if (BeehunterHolding > 0) {
				if (Main.rand.Next(10) <= 3) {
					Player.SetImmuneTimeForAllTypes(Player.longInvince ? 90 : 60);
					return false;
				}
				return true;
			}
			return true;
		}*/
		
		public override bool FreeDodge(Player.HurtInfo info) {
			/*if(info.DamageSource.TryGetCausingEntity(out Entity entity) && entity is Projectile proj) {
				SoundEngine.PlaySound(new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Aventurine/GlassBreaking"));
				Player.SetImmuneTimeForAllTypes(Player.longInvince ? 90 : 60);
				return false;
			}
			else {
				SoundEngine.PlaySound(new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/RaidenShogun/ElementalSkill_NoEscape"));
				return false;
			}*/
			if (BeehunterHolding > 0 && Main.rand.Next(10) <= 5) {
				//can't dodge projectiles
				if(info.DamageSource.TryGetCausingEntity(out Entity entity) && entity is Projectile proj) {
					return false;
				}
				Player.SetImmuneTimeForAllTypes(Player.longInvince ? 90 : 60);
				return true;
			}
			if (JackieHolding > 0 && Main.rand.Next(10) <= 2) {
				//can't dodge projectiles
				if(info.DamageSource.TryGetCausingEntity(out Entity entity) && entity is Projectile proj) {
					return false;
				}
				Player.SetImmuneTimeForAllTypes(Player.longInvince ? 90 : 60);
				JackieDodged = true;
				return true;
			}
			if (NingJadeScreen) {
				if(info.DamageSource.TryGetCausingEntity(out Entity entity) && entity is Projectile proj) {
					return true;
				}
			}
			return false;
		}
		
		public override bool ConsumableDodge(Player.HurtInfo info) {
			if (AventurineShieldHP > 0) {
				foreach (var ligma in Main.ActivePlayers) {
					if (ligma.HasBuff(ModContent.BuffType<AventurineShieldOrigin>())) {
						ligma.GetModPlayer<BooTaoPlayer>().AventurineBlindBet++;
					}
				}
				//SoundEngine.PlaySound(SoundID.Shatter with { Pitch = 0.5f });
				// if the shield can tank the hit
				if (AventurineShieldHP >= info.Damage) {
					AventurineShieldHP -= info.Damage;
				}
				// link to ByCustomReason, no idea if this works, too lazy to find out
				// https://discord.com/channels/103110554649894912/534215632795729922/1433321106877448232
				// else, break the shield and take the remaining dmg
				else {
					SoundEngine.PlaySound(new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Aventurine/GlassBreaking"));
					Player.HurtInfo ALLORNOTHING = new Player.HurtInfo();
					ALLORNOTHING.Damage = info.Damage - AventurineShieldHP;
					ALLORNOTHING.DamageSource = PlayerDeathReason.ByCustomReason(NetworkText.FromKey("AventurineDead",Player.name));
					ALLORNOTHING.PvP = false;
					ALLORNOTHING.Dodgeable = true;
					ALLORNOTHING.Knockback = 4f;
					ALLORNOTHING.HitDirection = Player.direction;
					Player.Hurt(ALLORNOTHING);
					AventurineShieldHP = 0;
				}
				Player.SetImmuneTimeForAllTypes(Player.longInvince ? 90 : 60);
				
				//if (Player.whoAmI != Main.myPlayer) { return; }
				//if (Main.netMode != NetmodeID.SinglePlayer) {
					//SendExampleDodgeMessage;
				//}
				return true;
			}
			if (RoyalBrooch && RBcd <= 0) {
				Player.SetImmuneTimeForAllTypes(Player.longInvince ? 90 : 60);
				RBcd = 7200;
				return true;
			}
			if (MedicineSticks && MScd <= 0) {
				// Medicine Sticks grants two shields. For this implementation,
				// I use the bool MSn to track which of the two shields the player is currently on.
				// If the player has two shields, MSn should be true. Upon dodging, it will be set to false.
				// If the player has one shield left, MSn should be false. Upon dodging, set to true and raise the CD to 2 mins
				Player.SetImmuneTimeForAllTypes(Player.longInvince ? 90 : 60);
				if (!MSn) {
					MSn = true;
					MScd = 7200;
				}
				else { MSn = false; }
				return true;
			}
			if (EntelechiaRevive) {
				if (Player.statLife - info.Damage < Player.statLifeMax2 / 4) {
					Player.SetImmuneTimeForAllTypes(Player.longInvince ? 90 : 60);
					Player.Heal(Player.statLifeMax2);
					EnteReviveCD = 10800;
					for (int ibby = 0; ibby < 20; ibby++) {
						Dust.NewDust(Player.position, 0, 0, 235, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), 90, default, 1f);
						Dust.NewDustPerfect(Player.position, 235, null, 110, default, 1f);
					}
					SoundEngine.PlaySound(new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Entelechia/EnteReviveVO"));
					return true;
				}
			}
			return false;
		}
		
		public override void OnHitAnything(float x, float y, Entity victim){
			if (RaidenShogunSkill && RaidenShogunCooldown == 0) {
				int damage = (int)(RaidenShogunSkillDamage / 5) + 1;
				if (Player.HasBuff(ModContent.BuffType<RaidenShogunBuff>()))
					damage = RaidenShogunSkillDamage;
				Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(x, y), Vector2.Zero, ModContent.ProjectileType<RaidenShogunSkillProj>(), damage, 1, Player.whoAmI);
				RaidenShogunCooldown = 54;
				SoundEngine.PlaySound(SoundID.Item94 with { Volume = 0.3f });
			}
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers) {
			if (Camper) {
				modifiers.DamageVariationScale *= 0f;
				if (!Main.CurrentFrameFlags.AnyActiveBossNPC && Player.velocity == Vector2.Zero) {
					modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
						hitInfo.Damage = (int)(hitInfo.Damage * 1.5);
					};
				}
				else {
					modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
						hitInfo.Damage = (int)(hitInfo.Damage * 0.1);
					};
				}
			}
			if (Camper2) {
				modifiers.DamageVariationScale *= 0f;
				if (!Main.CurrentFrameFlags.AnyActiveBossNPC && Player.velocity == Vector2.Zero) {
					modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
						hitInfo.Damage = (int)(hitInfo.Damage * 1.5);
					};
				}
				else {
					modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
						hitInfo.Damage = (int)(hitInfo.Damage * 0.1);
					};
				}
			}
		}
	}
}
/*
https://github.com/ThePaperLuigi/The-Stars-Above
https://github.com/tModLoader/tModLoader/wiki/Basic-Dust
https://docs.tmodloader.net/docs/stable/class_n_p_c.html#a8d6296daef89c984bc583ecb51593f4a
https://docs.tmodloader.net/docs/stable/class_mod_projectile.html

*/