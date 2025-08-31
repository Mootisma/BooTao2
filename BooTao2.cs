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
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			recipe = Recipe.Create(ItemID.SharkFin, 5);
			recipe.AddIngredient(ItemID.Goldfish, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.JungleRose);
			recipe.AddIngredient(ItemID.JungleSpores, 5);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			recipe = Recipe.Create(ItemID.FeralClaws);
			recipe.AddIngredient(ItemID.JungleSpores, 5);
			recipe.AddTile(TileID.WorkBenches);
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
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.Revolver, 1);
			recipe.AddIngredient(ItemID.DemoniteBar, 9);
			recipe.Register();
			recipe = Recipe.Create(ItemID.Revolver, 1);
			recipe.AddIngredient(ItemID.CrimtaneBar, 9);
			recipe.Register();
			recipe = Recipe.Create(ItemID.Spear, 1);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 3);
			recipe.Register();
			recipe = Recipe.Create(ItemID.CobaltChainsaw, 1);
			recipe.AddIngredient(ItemID.PalladiumChainsaw, 1);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.SummoningPotion, 1);
			recipe.AddIngredient(ItemID.SoulofLight, 2);
			recipe.Register();
			recipe = Recipe.Create(ItemID.Pho, 1);
			recipe.AddIngredient(ItemID.GlowingMushroom, 1);
			recipe.Register();
			recipe = Recipe.Create(ItemID.ChocolateChipCookie, 1);
			recipe.AddIngredient(ItemID.GlowingMushroom, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			recipe = Recipe.Create(ItemID.Bass, 1);
			recipe.AddIngredient(ItemID.GlowingMushroom, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			recipe = Recipe.Create(ItemID.BlueBerries, 1);
			recipe.AddIngredient(ItemID.GlowingMushroom, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			recipe = Recipe.Create(ItemID.SliceOfCake, 1);
			recipe.AddIngredient(ItemID.GlowingMushroom, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			recipe = Recipe.Create(ItemID.IceCream, 1);
			recipe.AddIngredient(ItemID.GlowingMushroom, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.ZapinatorOrange, 1);
			recipe.AddIngredient(ItemID.SoulofLight, 10);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.FrogLeg, 1);
			recipe.AddIngredient(ItemID.ShinyRedBalloon, 1);
			recipe.Register();
			recipe = Recipe.Create(ItemID.PanicNecklace, 1);
			recipe.AddIngredient(ItemID.Aglet, 1);
			recipe.AddIngredient(ItemID.HealingPotion, 2);
			recipe.Register();
			recipe = Recipe.Create(ItemID.ManaFlower, 1);
			recipe.AddIngredient(ItemID.JungleRose, 1);
			recipe.AddIngredient(ItemID.ManaPotion, 2);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.Shellphone, 1);
			recipe.AddIngredient(ItemID.LunarBar, 4);
			recipe.AddIngredient(ItemID.MagicMirror, 1);
			recipe.Register();
			recipe = Recipe.Create(ItemID.DPSMeter, 1);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 2);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.BouncingShield, 1);
			recipe.AddIngredient(ItemID.LunarBar, 4);
			recipe.Register();
			recipe = Recipe.Create(ItemID.Gatligator, 1);
			recipe.AddIngredient(ItemID.LunarBar, 4);
			recipe.Register();
			recipe = Recipe.Create(ItemID.Katana, 1);
			recipe.AddIngredient(ItemID.LunarBar, 4);
			recipe.Register();
			recipe = Recipe.Create(ItemID.ZapinatorGray, 1);
			recipe.AddIngredient(ItemID.LunarBar, 4);
			recipe.Register();
			recipe = Recipe.Create(ItemID.CoinGun, 1);
			recipe.AddIngredient(ItemID.LunarBar, 4);
			recipe.Register();
			recipe = Recipe.Create(ItemID.PaladinsShield, 1);
			recipe.AddIngredient(ItemID.LunarBar, 4);
			recipe.Register();
			recipe = Recipe.Create(ItemID.AnkhCharm, 1);
			recipe.AddIngredient(ItemID.LunarBar, 4);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.IceSkates, 1);
			recipe.AddIngredient(ItemID.IceBlock, 20);
			recipe.AddIngredient(ItemID.HermesBoots, 1);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.StylistKilLaKillScissorsIWish, 1);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 4);
			recipe.Register();
			recipe = Recipe.Create(ItemID.EskimoCoat, 1);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 4);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.CompanionCube, 1);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 4);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.SharpTears, 1);
			recipe.AddIngredient(ItemID.LunarBar, 14);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.Burger, 1);
			recipe.AddIngredient(ItemID.LunarBar, 1);
			recipe.Register();
			recipe = Recipe.Create(ItemID.BloodyMoscato, 1);
			recipe.AddIngredient(ItemID.LunarBar, 1);
			recipe.Register();
			recipe = Recipe.Create(ItemID.BlackBelt, 1);
			recipe.AddIngredient(ItemID.LunarBar, 1);
			recipe.Register();
			//
			if (ModLoader.TryGetMod("SOTS", out Mod sots)) {
				if (sots.TryFind("TinyPlanetFish", out ModItem planetfish)) {
					recipe = Recipe.Create(planetfish.Type, 1);
					recipe.AddIngredient(ItemID.SoulofLight, 1);
					recipe.AddIngredient(ItemID.Feather, 1);
					recipe.Register();
				}
				if (sots.TryFind("PhaseBar", out ModItem phasebar) && sots.TryFind("PhaseOre", out ModItem phaseore) && sots.TryFind("ParticleRelocator", out ModItem particlerelocator)){
					recipe = Recipe.Create(phasebar.Type, 1);
					recipe.AddIngredient(phaseore.Type, 3);
					recipe.AddTile(TileID.AdamantiteForge);
					recipe.Register();
					recipe = Recipe.Create(particlerelocator.Type, 1);
					recipe.AddIngredient(phasebar.Type, 25);
					recipe.AddTile(TileID.AdamantiteForge);
					recipe.Register();
				}
			}
		}
	}
	
	public class BooTaoPlayer : ModPlayer
	{
		public bool Magnet;
		public bool[] HomaPickaxes = new bool[7];
		public static bool[] HomaConfig = new bool[7];
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
		//
		public int FiammettaS3 = 0;
		public int FiammettaSP = 0;
		public Vector2 FiammettaStoreMouse = Vector2.Zero;
		//
		public float SkadiATK = 0;
		public int SkadiSP = 0;
		//
		public int MostimaSkillDuration = 0;
		public int MostimaSkillSP = 2400;
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
		
		public override void ModifyHurt(ref Player.HurtModifiers modifiers) {
			if (der > 0) {
				modifiers.FinalDamage *= 0.1f;
			}
			
			// if holding Entelechia item, and revive is on cooldown, gain 20% damage reduction (idk if i can make it physical hits only)
			if (Player.HasBuff(ModContent.BuffType<EntelechiaBuff>()) && !EntelechiaRevive && EnteReviveCD > 0){
				modifiers.FinalDamage *= 0.8f;
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
		}
		
		public override void OnRespawn() {
			AventurineShieldHP = 0;//https://docs.tmodloader.net/docs/stable/class_mod_player.html
			AventurineBlindBet = 0;
		}
		
		/*public override void NaturalLifeRegen (ref float regen)
		{
			if(Shrek){
				regen = regen * 12.1f;
			}
		}*/
		
		public override void UpdateLifeRegen() {
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
	}
	
	public class BooTaoGlobalItem : GlobalItem
    {
        public override void GrabRange(Item item, Player player, ref int grabRange)
		{
			if(player.GetModPlayer<BooTaoPlayer>().Magnet)
			{
				//player.GetModPlayer<BooTaoPlayer>().Magnet = true; //paste to accessory :)
				grabRange += 2000;
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
		public int BloodBlossomDmg = 12;
		public bool tnspeepee;
		public int ThornsDOTstack = 0;
		public int ThornsDOTdmg = 0;
		public int ThornsDOTduration = 0;
		public bool test;
		public bool KafkaAKSleep;
		//
		public bool kfpeepee;
		public int KafkaDOTdmg = 0;
		public int KafkaDOTduration = 0;
		//
		public bool MostimaSlow1;
		public bool MostimaSlow2;
		public bool MostimaSlow3;
		//
		public bool bspeepee;
		public int Arcana = 0;
		public int bsTimer = 0;
		public int bsDmgDone = 0;
		public int bsDefDownDur = 0;
		public bool bsDefDebuff;
		//
		public bool EscoffierDebuff;
		//
		public bool entelechiadot;

		public override void ResetEffects(NPC npc) {
			BloodBlossom = false;
			test = false;
			KafkaAKSleep = false;
			bspeepee = false;
			kfpeepee = false;
			tnspeepee = false;
			EscoffierDebuff = false;
			bsDefDebuff = false;
			entelechiadot = false;
		}

		public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers) {
			if (BloodBlossom) {
				// For best results, defense debuffs should be multiplicative
				modifiers.Defense *= BloodBlossomBuff.DefenseMultiplier;
			}
			if (EscoffierDebuff) {
				modifiers.Defense *= EscoffierBuff.DefenseMultiplier;
			}
			if (bsDefDebuff) {
				modifiers.Defense *= 0.75f;
			}
		}

		public override void DrawEffects(NPC npc, ref Color drawColor) {
			// This simple color effect indicates that the buff is active
			if (BloodBlossom || ThornsDOTstack > 0 || test || bspeepee) {
				drawColor.G = 0;
			}
		}
		//https://docs.tmodloader.net/docs/stable/class_global_n_p_c.html
		public override bool PreAI(NPC npc) {
			if (KafkaAKSleep) {
				//Dust.NewDust(npc.Center, 0, 0, 34, 0, 0, 150, default, 1f);
				return false;
			}
			if (test && !npc.boss) {
				//https://discord.com/channels/103110554649894912/534215632795729922/1266852701870882826
				//https://discord.com/channels/103110554649894912/534215632795729922/1280133031839010908
				//Vector2 ligmaballs = Vector2.Lerp(npc.position, npc.oldPos[1], 1);
				//npc.position -= ligmaballs;
				
				//the current combination below slows the enemy by 50%, i *think*
				// in reverse (multiplying here and dividing in postai) it seems to speed them up
				npc.velocity /= 0.01f;
			}
			if (MostimaSlow1) {
				npc.velocity /= 0.01f;
			}
			bsTimer++;
			if (bsTimer >= 300) {
				if (Arcana > 1) {
					Arcana = 1;
				}
				bsTimer = 0;
			}
			if (bsDefDownDur > 0) {
				bsDefDownDur--;
			}
			return true;
		}
		
		public override void PostAI(NPC npc) {
			if (test && !npc.boss) {
				npc.velocity *= 0.01f;
			}
		}
		
		public override bool? CanBeHitByItem(NPC npc, Player player, Item item) {
			if (KafkaAKSleep)
				return false;
			return null;
		}
		
		public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile){
			if (KafkaAKSleep)
				return false;
			return null;
		}
		
		public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot){
			if (KafkaAKSleep)
				return false;
			return true;
		}
		
		public override void UpdateLifeRegen(NPC npc, ref int damage) {
			if (BloodBlossom) {
				if (npc.lifeRegen > 0) {
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 12 * 2;//BloodBlossomDmg;
			}
			if (tnspeepee ){//&& ThornsDOTstack > 0 && ThornsDOTduration > 0) {
				//ThornsDOTduration--;
				if (npc.lifeRegen > 0) {
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 135 * 2;//ThornsDOTdmg;
			}
			//else {
			//	ThornsDOTstack = 0;
			//	ThornsDOTduration = 0;
			//}
			if (kfpeepee){// && KafkaDOTduration > 0) {
				//KafkaDOTduration--;
				if (npc.lifeRegen > 0) {
					npc.lifeRegen = 0;
				}
				//if (KafkaDOTdmg < 30)
				npc.lifeRegen -= 75 * 2;
				//else
				//	npc.lifeRegen -= KafkaDOTdmg;
			}
			if (bspeepee) {
				if (npc.lifeRegen > 0) {
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 300 * 2;//(int)(bsDmgDone * (2.4 + 0.12 * Arcana));
			}
			if (entelechiadot) {
				if (npc.lifeRegen > 0) {
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 200 * 2;
			}
		}
		
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {
			if (npc.type == NPCID.WallofFlesh){
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HuTaoHoma>(), 40));
            }
			if (npc.type == NPCID.MeteorHead || npc.type == NPCID.AngryBones || npc.type == NPCID.DungeonSpirit){
				npcLoot.Add(ItemDropRule.Common(ItemID.Meteorite, 3, 1, 4));
			}
			if (npc.type == NPCID.MartianSaucer){
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RaidenShogunItem>(), 40));
			}
			if (npc.type == NPCID.IceElemental){
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HertaMinionItem>(), 40));
			}
		}
	}
}