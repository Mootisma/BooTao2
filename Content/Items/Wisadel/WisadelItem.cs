using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;
using BooTao2.Content.Projectiles.Wisadel;
using BooTao2.Content.Projectiles;
using BooTao2.Content.Buffs.Wisadel;

namespace BooTao2.Content.Items.Wisadel {
	public class WisadelItem : ModItem {
		private int SP = 2700;
		private bool SkillActive = false;
		
		SoundStyle WisadelBasic = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Wisadel/WisadelBasic") {
			Volume = 1.6f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		SoundStyle WisadelSkill = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Wisadel/WisadelSkill") {
			Volume = 1.6f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		SoundStyle RevenantShadowSpawning = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Wisadel/RevenantShadowSpawning") {
			Volume = 1.6f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		SoundStyle InBattle1 = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Wisadel/InBattle1") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		SoundStyle Skill = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Thorns/AtkBoost") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		public override void SetDefaults() {
			Item.width = 32;
			Item.height = 32;
			Item.rare = ItemRarityID.Red;
			Item.value = Item.sellPrice(1, 50, 0, 0);
			
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 500;
			Item.knockBack = 3f;
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod)) {
				Item.damage = 600;
				Item.knockBack = 4f;
			}
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 90;
			Item.useTime = 90;
			Item.autoReuse = true;
			Item.noUseGraphic = true;
			Item.noMelee = true;

			Item.shoot = ModContent.ProjectileType<WisadelProjBasic>();
			Item.shootSpeed = 20f;
		}
		
		public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2){
				if (!SkillActive && SP >= 3000) {
					SkillActive = true;
					SP = -1;
					player.GetModPlayer<BooTaoPlayer>().WisadelAmmo = 6;
					SoundEngine.PlaySound(Skill, player.Center);
					SoundEngine.PlaySound(InBattle1, player.Center);
				}
				return true;
			}
			if (SkillActive) {
				Item.useAnimation = 180;
				Item.useTime = 180;
			}
			else {
				Item.useAnimation = 90;
				Item.useTime = 90;
			}
			return true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			// spawn one revenant shadow
			if (player.altFunctionUse == 2 && SP == -1){
				SP = 0;
				if (player.ownedProjectileCounts[ModContent.ProjectileType<RevenantShadow>()] < 3) {
					SoundEngine.PlaySound(RevenantShadowSpawning, player.Center);
					player.AddBuff(ModContent.BuffType<WisadelBuff>(), 20);
					var projectile = Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<RevenantShadow>(), damage, knockback, Main.myPlayer);
					projectile.originalDamage = Item.damage;
				}
				return false;
			}
			// shoot big boom skill attack
			if (SkillActive) {
				SoundEngine.PlaySound(WisadelSkill, player.Center);
				Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 4f, 4f);
				return false;
			}
			// shoot normal atk
			SoundEngine.PlaySound(WisadelBasic, player.Center);
			return true;
		}
		
		public override void HoldItem(Player player)
		{
			if (player.GetModPlayer<BooTaoPlayer>().WisadelAmmo <= 0) {
				SkillActive = false;
			}
			if (SP < 3000 && !SkillActive) {
				SP++;
			}
			if (!SkillActive && SP >= 3000) {
				player.GetModPlayer<BooTaoPlayer>().SkillReady = true;
				if (player.ownedProjectileCounts[ModContent.ProjectileType<SkillReady>()] < 1) {
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ModContent.ProjectileType<SkillReady>(), 0, 4, player.whoAmI, 0f);
				}
			}
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.LunarBar, 10)
				.AddIngredient(ItemID.Dynamite, 10)
				.AddIngredient(ItemID.RocketLauncher, 1)
				.AddIngredient(ItemID.SharpTears, 1)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}