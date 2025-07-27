using BooTao2.Content.Buffs.Lappland;
using BooTao2.Content.Projectiles.Lappland;
using BooTao2.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BooTao2.Content.Items.Lappland
{
	public class LapplandItem : ModItem
	{
		private bool CalamityActive = ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);
		private int LapplandSP = 38;
		private int timer = 0;
		private int SkillDuration = 0;
		SoundStyle Skill = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Thorns/AtkBoost") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		SoundStyle SpawnMinion = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Lappland/Skill3") {
				Volume = 0.8f,
				PitchVariance = 0f,
				MaxInstances = 1,
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew//ReplaceOldest
			};
		
		public override void SetStaticDefaults() {
			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

			ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f; // The default value is 1, but other values are supported. See the docs for more guidance. 
		}
		
		public override void SetDefaults() {
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useAnimation = 25;
			Item.useTime = 25;
			Item.autoReuse = true;
			//
			Item.shootSpeed = 20f;
			Item.shoot = ModContent.ProjectileType<LapplandMinionProj>();
			Item.buffType = ModContent.BuffType<LapplandBuff>();
			//
			Item.width = 50;
			Item.height = 50;

			// Weapon Properties
			Item.damage = (CalamityActive) ? 130 : 100;
			Item.DamageType = DamageClass.Summon;
			Item.mana = 10;
			Item.crit = 1;
			Item.knockBack = 3.5f;

			Item.rare = ItemRarityID.Gray;
			Item.value = Item.sellPrice(gold: 50);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod) && calamityMod.TryFind("RuinousSoul", out ModItem RuinousSoul) ) {
				recipe.AddIngredient(RuinousSoul.Type);
			}
			recipe.AddIngredient(ItemID.LunarBar, 1);
			recipe.AddIngredient(ItemID.MoonCharm, 1);
			recipe.AddIngredient(ItemID.FireworksBox, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		public override void HoldItem(Player player) {
			if (LapplandSP >= 54) {
				player.GetModPlayer<BooTaoPlayer>().SkillReady = true;
				if (player.ownedProjectileCounts[ModContent.ProjectileType<SkillReady>()] < 1) {
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ModContent.ProjectileType<SkillReady>(), 0, 4, player.whoAmI, 0f);
				}
			}
		}
		
		public override void UpdateInventory (Player player) {
			timer++;
			if (timer >= 60) {//60s timer but designed by a psychotic animal (me)
				timer = 0;
				// if skill is active
				if (SkillDuration > 0) {
					SkillDuration--;
				}
				else if (LapplandSP < 60) {
					LapplandSP++;
				}
			}
			if (SkillDuration > 0) {
				player.GetModPlayer<BooTaoPlayer>().LapplandSkill = true;
			}
			else {
				player.GetModPlayer<BooTaoPlayer>().LapplandSkill = false;
			}
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}
		
		public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2){
				if (LapplandSP >= 54) {
					SkillDuration = 40;
					LapplandSP = 0;
					timer = 0;
					SoundEngine.PlaySound(Skill, player.Center);
					player.GetModPlayer<BooTaoPlayer>().LapplandSkill = true;
				}
				return false;
			}
			return true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			if (player.altFunctionUse == 2)
				return false;
			
			// maximum four wolves (forgoing the passive's warm up for simplicity)
			if (player.ownedProjectileCounts[type] < 4 /*&& player.maxMinions > 3*/ && SkillDuration < 1) {
				// This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
				player.AddBuff(Item.buffType, 2);

				// Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
				var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
				projectile.originalDamage = Item.damage;
				
				SoundEngine.PlaySound(SpawnMinion, player.Center);
				
				return false;
			}
			
			// after the max wolves are out, lets be able to attack like a normal weapon
			if (SkillDuration > 0) {
				Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<LapplandWeaponProj2>(), (int)(damage * 1.8), knockback, player.whoAmI, 5f);
			}
			else {
				Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<LapplandWeaponProj>(), damage, knockback, player.whoAmI, 0f);
			}
			return false;
		}
	}
}
/*
https://docs.tmodloader.net/docs/stable/class_mod_projectile.html
https://docs.tmodloader.net/docs/stable/class_projectile.html
https://docs.tmodloader.net/docs/stable/class_player.html

*/