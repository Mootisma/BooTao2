using BooTao2.Content.Projectiles.Absinthe;
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

namespace BooTao2.Content.Items.Absinthe
{
	public class AbsintheItem : ModItem
	{
		public bool CalamityActive = ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);
		private int AbsintheSP = 0;
		private int timer = 0;
		private bool AbsintheSkillActive = false;
		private int SkillDuration = 30;
		SoundStyle Skill = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Thorns/AtkBoost") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		public override void SetDefaults() {
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useAnimation = 40;
			Item.useTime = 40;
			Item.autoReuse = true;
			//
			Item.shootSpeed = 20f;
			Item.shoot = ModContent.ProjectileType<AbsintheProj>();
			//
			Item.width = 50;
			Item.height = 50;
			Item.UseSound = SoundID.Item11;

			// Weapon Properties
			Item.damage = (CalamityActive) ? 55 : 45;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 10;
			Item.crit = 1;
			Item.knockBack = 3.5f;

			Item.rare = ItemRarityID.Orange;
			Item.value = Item.sellPrice(silver: 50);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.CobaltBar, 1);
			recipe.AddIngredient(ItemID.Obsidian, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.PalladiumBar, 1);
			recipe.AddIngredient(ItemID.Obsidian, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		public override void HoldItem(Player player) {
			if (!AbsintheSkillActive && AbsintheSP >= 50) {
				player.GetModPlayer<BooTaoPlayer>().SkillReady = true;
				if (player.ownedProjectileCounts[ModContent.ProjectileType<SkillReady>()] < 1) {
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ModContent.ProjectileType<SkillReady>(), 0, 4, player.whoAmI, 0f);
				}
			}
		}
		
		public override void UpdateInventory (Player player) {
			if (timer > 0) {
				timer--;
			}
			if (timer == 0) {
				timer = 60;
				AbsintheSP++;
				if (AbsintheSkillActive) {
					AbsintheSP = 0;
					SkillDuration--;
					if (SkillDuration <= 0) {
						SkillDuration = 0;
						AbsintheSkillActive = false;
					}
				}
			}
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}
		
		public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2){
				if (!AbsintheSkillActive && AbsintheSP >= 50) {
					AbsintheSkillActive = true;
					SkillDuration = 30;
					SoundEngine.PlaySound(Skill, player.Center);
				}
				return false;
			}
			return true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			if (player.altFunctionUse == 2)
				return false;
			if (AbsintheSkillActive) {
				float numProjs = 4;
				float rotation = MathHelper.ToRadians(4);
				position += Vector2.Normalize(velocity) * 45f;
				for (int i = 0; i < numProjs; i++) {
					Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numProjs - 1)));
					Projectile.NewProjectile(source, position, perturbedSpeed, type, (int)(damage * 0.9), knockback, player.whoAmI, 5f);
				}
				return false;
			}
			return true;
		}
	}
}
/*
https://docs.tmodloader.net/docs/stable/class_mod_projectile.html
https://docs.tmodloader.net/docs/stable/class_projectile.html
https://docs.tmodloader.net/docs/stable/class_player.html

*/