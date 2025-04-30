using BooTao2.Content.Projectiles;
using BooTao2.Content.Projectiles.Mostima;
using BooTao2.Content.Buffs.Mostima;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using System;

namespace BooTao2.Content.Items.Mostima
{
	public class MostimaItem : ModItem
	{
		SoundStyle Skill = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Thorns/AtkBoost") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		SoundStyle Select1 = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Mostima/Select1") {
			Volume = 1.3f,
			PitchVariance = 0f,
			MaxInstances = 1,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle Select2 = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Mostima/Select2") {
			Volume = 1.3f,
			PitchVariance = 0f,
			MaxInstances = 1,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle SkillActivate = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Mostima/SkillActivate") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 3,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle Deploy1 = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Mostima/Deploy1") {
			Volume = 1.2f,
			PitchVariance = 0f,
			MaxInstances = 3,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle Deploy2 = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Mostima/Deploy2") {
			Volume = 1.2f,
			PitchVariance = 0f,
			MaxInstances = 3,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle Battle1 = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Mostima/Battle1") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 3,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle Battle2 = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Mostima/Battle1") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 3,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle Battle3 = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Mostima/Battle1") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 3,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle Battle4 = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Mostima/Battle1") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 3,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		bool CalamityActive = ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);
		
		public override void SetDefaults() {
			Item.damage = (CalamityActive) ? 90 : 70;
			Item.DamageType = DamageClass.Summon;
			Item.mana = 24;
			Item.shoot = ModContent.ProjectileType<MostimaMinion>();
			Item.buffType = ModContent.BuffType<MostimaMinionBuff>();
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 60;
			Item.useAnimation = 60;
			Item.knockBack = 3;
			//
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.value = Item.sellPrice(gold: 40);
			Item.rare = ItemRarityID.Blue;
			Item.autoReuse = false;
			Item.useTurn = false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(1111);
			recipe.AddIngredient(ItemID.AngelHalo, 1);
			recipe.AddIngredient(ItemID.SoulofMight, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			// dont shoot anything with the right click
			if (player.altFunctionUse == 2) {
				return false;
			}
			
			// only one mostima
			foreach (var proj in Main.ActiveProjectiles)
            {
                if (proj.active && proj.type == Item.shoot && proj.owner == player.whoAmI)
                {
                    proj.active = false;
                }
            }
			
			player.AddBuff(Item.buffType, 2);
			
			if (Main.rand.Next(2) == 1) {
				SoundEngine.PlaySound(Deploy1, player.Center);
			}
			else {
				SoundEngine.PlaySound(Deploy2, player.Center);
			}
			
			// spawn at mouse position
			var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
			projectile.originalDamage = Item.damage;
			return false;
		}
		
		public override void ModifyShootStats (Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			position = Main.MouseWorld;
			
			// add damage here based on progression
			// https://docs.tmodloader.net/docs/1.4-preview/class_terraria_1_1_n_p_c.html
			// also is there a better way to check these :skull:
			/* if (NPC.downedMechBoss1)//destroyer
				damage += 3;
			if (NPC.downedMechBoss2)//twins
				damage += 3;
			if (NPC.downedMechBoss3)//skeletron prime
				damage += 3;
			if (NPC.downedPlantBoss)
				damage += 11;
			if (NPC.downedGolemBoss)
				damage += 10;
			if (NPC.downedAncientCultist)//lunatic cultist
				damage += 20;
			if (NPC.downedMoonlord)
				damage += 30;*/
		}
		
		public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            // https://github.com/ThePaperLuigi/The-Stars-Above/blob/main/Items/Weapons/Celestial/BrilliantSpectrum.cs
			//bool slimeKing = NPC.downedSlimeKing;
            //bool eye = NPC.downedBoss1;
            //bool evilboss = NPC.downedBoss2;
            //bool queenBee = NPC.downedQueenBee;
            //bool skeletron = NPC.downedBoss3;
            //bool hardmode = Main.hardMode;
            bool anyMech = NPC.downedMechBossAny;
            bool allMechs = NPC.downedMechBoss3 && NPC.downedMechBoss2 && NPC.downedMechBoss1;
            bool plantera = NPC.downedPlantBoss;
            bool golem = NPC.downedGolemBoss;
            bool cultist = NPC.downedAncientCultist;
            bool moonLord = NPC.downedMoonlord;

            float damageMult = 1f +
                //(slimeKing ? 0.1f : 0f) +
                //(eye ? 0.12f : 0f) +
                //(evilboss ? 0.14f : 0f) +
                //(queenBee ? 0.36f : 0f) +
                //(skeletron ? 0.58f : 0f) +
                //(hardmode ? 1.2f : 0f) +
                (anyMech ? 0.05f : 0f) +
                (allMechs ? 0.05f : 0f) +
                (plantera ? 0.05f : 0f) +
                (golem ? 0.05f : 0f) +
                (cultist ? 0.15f : 0f) +
                (moonLord ? 0.15f : 0f);

            damage *= damageMult;

        }
		
		// useless counter so i can play a sound when i hold Mostima
		int ligma = 0;
		public override void UpdateInventory (Player player) {
			if (ligma > 0) {
				ligma--;
			}
		}
		
		public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2){
				if (player.GetModPlayer<BooTaoPlayer>().MostimaSkillSP >= 3780 && !player.GetModPlayer<BooTaoPlayer>().MostimaSkill) {
					player.GetModPlayer<BooTaoPlayer>().MostimaSkill = true;
					player.GetModPlayer<BooTaoPlayer>().MostimaSkillDuration = 1620;
					player.GetModPlayer<BooTaoPlayer>().MostimaSkillSP = 0;
					SoundEngine.PlaySound(Skill, player.Center);
					SoundEngine.PlaySound(SkillActivate, player.Center);
					int choice = Main.rand.Next(5);
					if (choice == 0)
						SoundEngine.PlaySound(Battle1, player.Center);
					if (choice == 1)
						SoundEngine.PlaySound(Battle2, player.Center);
					if (choice == 2)
						SoundEngine.PlaySound(Battle3, player.Center);
					if (choice == 3)
						SoundEngine.PlaySound(Battle4, player.Center);
				}
				return false;
			}// return player.ownedProjectileCounts[Item.shoot] < 1;
			return true;
		}
		
		public override void HoldItem(Player player) {
			if (ligma == 0) {
				if (Main.rand.Next(2) == 1) {
					SoundEngine.PlaySound(Select1, player.Center);
				}
				else {
					SoundEngine.PlaySound(Select2, player.Center);
				}
			}
			ligma = 2;
			if (player.GetModPlayer<BooTaoPlayer>().MostimaSkillSP >= 3780 && !player.GetModPlayer<BooTaoPlayer>().MostimaSkill) {
				player.GetModPlayer<BooTaoPlayer>().SkillReady = true;
				if (player.ownedProjectileCounts[ModContent.ProjectileType<SkillReady>()] < 1) {
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ModContent.ProjectileType<SkillReady>(), 0, 4, player.whoAmI, 0f);
				}
			}
		}
	}
}
/*
https://docs.tmodloader.net/docs/stable/class_main.html#a55969af8fef5fd9819d1854403cf794b
https://docs.tmodloader.net/docs/1.4-preview/class_terraria_1_1_utilities_1_1_unified_random.html
https://github.com/tModLoader/tModLoader/blob/1.4.4/ExampleMod/Content/NPCs/ExampleCustomAISlimeNPC.cs
https://www.w3schools.com/cs/cs_enums.php
https://github.com/tModLoader/tModLoader/wiki/npc-Class-Documentation#downedboss1
https://docs.tmodloader.net/docs/1.4-preview/class_terraria_1_1_n_p_c.html
*/