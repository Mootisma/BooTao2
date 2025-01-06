using BooTao2.Content.Buffs;
using BooTao2.Content.Buffs.BloodBlossomBuff;
using BooTao2.Content.Buffs.Aventurine;
using BooTao2.Content.Items.HuTao;
using BooTao2.Content.Items.RaidenShogun;
using BooTao2.Content.Items.Herta;
using BooTao2.Content.Projectiles.RaidenShogun;
using BooTao2.Content.Buffs.RaidenShogun;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace BooTao2
{
	public class BooTao2 : Mod
	{
		public override void AddRecipes()
		{//https://docs.tmodloader.net/docs/1.4-stable/class_terraria_1_1_i_d_1_1_item_i_d.html
			Recipe recipe = Recipe.Create(/*ModContent.ItemType<Items.ExampleItem>()*/ItemID.BlackInk, 1);
			recipe.AddIngredient(ItemID.StoneBlock, 2);
			recipe.Register();
			//
			//
			recipe = Recipe.Create(ItemID.JungleRose);
			recipe.AddIngredient(ItemID.JungleSpores, 5);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.FeralClaws);
			recipe.AddIngredient(ItemID.JungleSpores, 5);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.LavaCharm);
			recipe.AddIngredient(ItemID.HellstoneBar, 7);
			recipe.AddIngredient(ItemID.Obsidian);
			recipe.AddIngredient(ItemID.Shackle);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.ChlorophyteBar);
			recipe.AddIngredient(ItemID.ChlorophyteOre);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.PixieDust);
			recipe.AddIngredient(ItemID.SoulofLight);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.AngelHalo);
			recipe.AddIngredient(ItemID.SoulofLight, 10);
			recipe.AddIngredient(ItemID.CrystalBullet, 10);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.WormholePotion, 30);
			recipe.AddIngredient(ItemID.DirtBlock, 1);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.Revolver, 1);
			recipe.AddIngredient(ItemID.DemoniteBar, 9);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.Revolver, 1);
			recipe.AddIngredient(ItemID.CrimtaneBar, 9);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.Pho, 1);
			recipe.AddIngredient(ItemID.Mushroom, 1);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.DPSMeter, 1);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 2);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.Spear, 1);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 3);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.SummoningPotion, 1);
			recipe.AddIngredient(ItemID.SoulofLight, 2);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.ChocolateChipCookie, 1);
			recipe.AddIngredient(ItemID.Mushroom, 2);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.ZapinatorOrange, 1);
			recipe.AddIngredient(ItemID.SoulofLight, 2);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.FrogLeg, 1);
			recipe.AddIngredient(ItemID.ShinyRedBalloon, 1);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.Shellphone, 1);
			recipe.AddIngredient(ItemID.LunarBar, 2);
			recipe.AddIngredient(ItemID.MagicMirror, 1);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.BouncingShield, 1);
			recipe.AddIngredient(ItemID.LunarBar, 2);
			recipe.Register();
			recipe = Recipe.Create(ItemID.Gatligator, 1);
			recipe.AddIngredient(ItemID.LunarBar, 2);
			recipe.Register();
			recipe = Recipe.Create(ItemID.Katana, 1);
			recipe.AddIngredient(ItemID.LunarBar, 2);
			recipe.Register();
			recipe = Recipe.Create(ItemID.ZapinatorGray, 1);
			recipe.AddIngredient(ItemID.LunarBar, 2);
			recipe.Register();
			recipe = Recipe.Create(ItemID.CoinGun, 1);
			recipe.AddIngredient(ItemID.LunarBar, 2);
			recipe.Register();
			recipe = Recipe.Create(ItemID.PaladinsShield, 1);
			recipe.AddIngredient(ItemID.LunarBar, 2);
			recipe.Register();
			recipe = Recipe.Create(ItemID.BlueBerries, 1);
			recipe.AddIngredient(ItemID.LunarBar, 2);
			recipe.Register();
			recipe = Recipe.Create(ItemID.AnkhCharm, 1);
			recipe.AddIngredient(ItemID.LunarBar, 2);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.IceSkates, 1);
			recipe.AddIngredient(ItemID.IceBlock, 2);
			recipe.AddIngredient(ItemID.HermesBoots, 1);
			recipe.Register();
			//
			if (ModLoader.TryGetMod("SOTS", out Mod sots)) {
				if (sots.TryFind("TinyPlanetFish", out ModItem planetfish)) {
					recipe = Recipe.Create(planetfish.Type, 1);
					recipe.AddIngredient(ItemID.SoulofLight, 1);
					recipe.AddIngredient(ItemID.Feather, 1);
					recipe.Register();
				}
			}
		}
	}
	
	public class BooTaoPlayer : ModPlayer
	{
		public bool Magnet;
		public bool Homa2;
		public bool Homa3;
		public bool Homa4;
		public bool Homa5;
		public bool Homa6;
		public bool lifeRegenDebuff;
		public bool BloodBlossom;
		public bool NingHolding; // player.GetModPlayer<BooTaoPlayer>().NingHolding
		public bool NingJade1State;
		public bool NingJade2State;
		public bool NingJade3State;
		//
		public const int HuTaoEDuration = 540;
		public const int HuTaoECD = 960;
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
		//
		public int FiammettaS3 = 0;
		public int FiammettaSP = 0;
		public Vector2 FiammettaStoreMouse = Vector2.Zero;
		//
		public float SkadiATK = 0;
		public int SkadiSP = 0;
		//
		public int MostimaSkillDuration = 0;
		public int MostimaSkillSP = 0;
		public bool MostimaSkill;
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
		
		public override void ResetEffects()
		{
			Magnet = false;
			lifeRegenDebuff = false;
			BloodBlossom = false;
			NingHolding = false;
			NingJade1State = false;
			NingJade2State = false;
			NingJade3State = false;
			SkillReady = false;
			RaidenShogunSkill = false;
			if (!Player.HasBuff(ModContent.BuffType<HomaPickaxeBuff>())) {
				Homa2 = false;
				Homa3 = false;
				Homa4 = false;
				Homa5 = false;
				Homa6 = false;
			}
		}
		
		public bool CanUseHuTaoE() {
			if (coold == 0) {
				Player player = Main.LocalPlayer;
				HuTaoHPDmgBuff = (float)((player.statLife * 0.3) / 100);
				player.statLife = (int)(player.statLife * 0.7);
				coold = HuTaoECD;
				der = HuTaoEDuration;
				return true;
			}
			return false;
		}
		
		public override void ModifyHurt(ref Player.HurtModifiers modifiers) {
			if (der > 0) {
				modifiers.FinalDamage *= 0.1f;
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
		}
		
		public override void OnRespawn() {
			AventurineShieldHP = 0;//https://docs.tmodloader.net/docs/stable/class_mod_player.html
		}
		
		/*public override void NaturalLifeRegen (ref float regen)
		{
			if(Shrek){
				regen = regen * 12.1f;
			}
		}*/
		
		/*public override void UpdateLifeRegen() {
        }*/
		
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
		}
		
		// public override void UpdateLifeRegen() {}
		
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
				// else, break the shield and take the remaining dmg
				else {
					SoundEngine.PlaySound(new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Aventurine/GlassBreaking"));
					Player.HurtInfo ALLORNOTHING = new Player.HurtInfo();
					ALLORNOTHING.Damage = info.Damage - AventurineShieldHP;
					ALLORNOTHING.DamageSource = PlayerDeathReason.ByCustomReason("Lost it all...");
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
			return false;
		}
		
		public override void OnHitAnything(float x, float y, Entity victim){
			if (RaidenShogunSkill && RaidenShogunCooldown == 0) {
				int damage = (int)(RaidenShogunSkillDamage / 5);
				if (Player.HasBuff(ModContent.BuffType<RaidenShogunBuff>()))
					damage = RaidenShogunSkillDamage;
				Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(x, y), Vector2.Zero, ModContent.ProjectileType<RaidenShogunSkillProj>(), damage, 1, Player.whoAmI);
				RaidenShogunCooldown = 54;
				SoundEngine.PlaySound(SoundID.Item94);
			}
		}
	}
	
	public class BooTaoGlobalItem : GlobalItem
    {
        public override void GrabRange(Item item, Player player, ref int grabRange)
		{
			if(player.GetModPlayer<BooTaoPlayer>().Magnet)
			{
				//player.GetModPlayer<BooTaoPlayer>().Magnet = true; //paste to accessory :)
				grabRange += 3700;
			}
		}
		
		//public override void PostUpdate(Item item) {
		//	item.velocity *= 2;
		//}
		
		public override bool GrabStyle(Item item, Player player) {
			if(player.GetModPlayer<BooTaoPlayer>().Magnet)
			{
				Vector2 movement = item.DirectionTo(player.Center) * 20f;
				item.velocity = movement;
				// item.velocity = Collision.TileCollision(item.position, item.velocity, item.width, item.height);
				return true;
			}
			return false;
		}
    }
	
	public class BooTaoGlobalNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;
		public bool BloodBlossom;
		public int BloodBlossomDmg = 8;
		public int ThornsDOTstack = 0;
		public int ThornsDOTdmg = 0;
		public int ThornsDOTduration = 0;

		public override void ResetEffects(NPC npc) {
			BloodBlossom = false;
		}

		/*public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers) {
			if (BloodBlossom) {
				// For best results, defense debuffs should be multiplicative
				modifiers.Defense *= BloodBlossomBuff.DefenseMultiplier;
			}
		}*/

		public override void DrawEffects(NPC npc, ref Color drawColor) {
			// This simple color effect indicates that the buff is active
			if (BloodBlossom || ThornsDOTstack > 0) {
				drawColor.G = 0;
			}
		}
		
		//public override void AI(NPC npc) {}
		
		public override void UpdateLifeRegen(NPC npc, ref int damage) {
			if (BloodBlossom) {
				if (npc.lifeRegen > 0) {
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= BloodBlossomDmg;
				damage = BloodBlossomDmg;
			}
			if (ThornsDOTstack > 0 && ThornsDOTduration > 0) {
				if (npc.lifeRegen > 0) {
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= ThornsDOTdmg;
				damage = ThornsDOTdmg;
			}
			else {
				ThornsDOTstack = 0;
				ThornsDOTduration = 0;
			}
			ThornsDOTduration--;
		}
		
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {
			if (npc.type == NPCID.WallofFlesh){
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HuTaoHoma>(), 40));
            }
			if (npc.type == NPCID.MeteorHead || npc.type == NPCID.AngryBones || npc.type == NPCID.DungeonSpirit){
				npcLoot.Add(ItemDropRule.Common(ItemID.Meteorite, 3, 1, 4));
			}
			if (npc.type == NPCID.MartianSaucer){
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RaidenShogunItem>(), 30));
			}
			if (npc.type == NPCID.IceElemental){
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HertaMinionItem>(), 30));
			}
		}
	}
}