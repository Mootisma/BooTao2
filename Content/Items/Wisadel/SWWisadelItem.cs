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
using BooTao2.Content.Items;

namespace BooTao2.Content.Items.Wisadel {
	public class SWWisadelItem : ModItem {
		public override string Texture => "BooTao2/Content/Items/Wisadel/WisadelItem";
		
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
		
		public override void SetDefaults() {
			Item.width = 32;
			Item.height = 32;
			Item.rare = ItemRarityID.Red;
			Item.value = Item.sellPrice(20, 50, 0, 0);
			
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 500;
			Item.crit = 46;
			Item.knockBack = 4f;
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod)) {
				Item.damage = 650;
				Item.knockBack = 5f;
				Item.crit = 56;
			}
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 12;
			Item.useTime = 12;
			Item.autoReuse = true;
			Item.noUseGraphic = true;
			Item.noMelee = true;

			Item.shoot = ModContent.ProjectileType<WisadelProjBasic>();
			Item.shootSpeed = 25f;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			// spawn one revenant shadow
			if (player.altFunctionUse == 2){
				if (player.ownedProjectileCounts[ModContent.ProjectileType<RevenantShadow>()] < 5) {
					SoundEngine.PlaySound(RevenantShadowSpawning, player.Center);
					player.AddBuff(ModContent.BuffType<WisadelBuff>(), 20);
					var projectile = Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<RevenantShadow>(), damage, knockback, Main.myPlayer);
					projectile.originalDamage = Item.damage;
				}
				return false;
			}
			// shoot big boom skill attack
			SoundEngine.PlaySound(WisadelSkill, player.Center);
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 4f, 4f);
			return false;
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient<Items.Wisadel.WisadelItem>()
				.AddIngredient<Items.SilverWolfMaterial>()
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}