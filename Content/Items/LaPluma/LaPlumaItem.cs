using BooTao2.Content.Projectiles.LaPluma;
using BooTao2.Content.Projectiles;
using BooTao2.Content.Buffs.LaPluma;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace BooTao2.Content.Items.LaPluma
{
	public class LaPlumaItem : ModItem
	{
		private int LaPlumaSP = 0;
		private int timer = 0;
		private bool LaPlumaSkillActive = false;
		private int SkillDuration = 25;
		SoundStyle Skill = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Thorns/AtkBoost") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		SoundStyle Attack = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/LaPluma/Attack") {
			Volume = 0.5f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		SoundStyle SkillAttack = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/LaPluma/SkillAttack") {
			Volume = 0.5f,
			PitchVariance = 0.5f,
			MaxInstances = 3,
		};
		
		SoundStyle SkillActivationVO = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/LaPluma/SkillActivationVO") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		public override void SetDefaults() {
			Item.damage = 40;
			Item.knockBack = 5f;
			Item.useStyle = 1;
			Item.useAnimation = 60;
			Item.useTime = 60;
			Item.width = 32;
			Item.height = 32;
			//Item.UseSound = SoundID.Item1;
			Item.DamageType = DamageClass.Melee;
			Item.autoReuse = true;
			Item.noUseGraphic = true; // The sword is actually a "projectile"
			Item.noMelee = true; // The projectile will do the damage and not the item
			Item.rare = ItemRarityID.Gray;
			Item.value = Item.sellPrice(0, 6, 20, 2);

			Item.shoot = ModContent.ProjectileType<LaPlumaProj>();
			Item.shootSpeed = 20f;
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			if (player.altFunctionUse == 2)
				return false;
			if (LaPlumaSkillActive) {
				SoundEngine.PlaySound(SkillAttack, player.Center);
				Projectile.NewProjectile(source, position, velocity, type, (int)(damage * 1.7), knockback, player.whoAmI, 5f);
				return false;
			}
			SoundEngine.PlaySound(Attack, player.Center);
			return true;
		}
		
		public override void HoldItem(Player player) {
			player.GetModPlayer<BooTaoPlayer>().LaPlumaHolding = true;
			player.AddBuff(ModContent.BuffType<LaPlumaBuff>(), (int)(2000 * player.GetModPlayer<BooTaoPlayer>().LaPlumaPassive + 59), true);
			if (!LaPlumaSkillActive && LaPlumaSP >= 40) {
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
				LaPlumaSP++;
				if (LaPlumaSkillActive) {
					LaPlumaSP = 0;
					SkillDuration--;
					if (SkillDuration <= 0) {
						SkillDuration = 0;
						LaPlumaSkillActive = false;
					}
				}
			}
		}
		
		public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2){
				if (!LaPlumaSkillActive && LaPlumaSP >= 40) {
					timer = 60;
					LaPlumaSkillActive = true;
					SkillDuration = 25;
					SoundEngine.PlaySound(Skill, player.Center);
					SoundEngine.PlaySound(SkillActivationVO, player.Center);
				}
				return false;
			}
			if (LaPlumaSkillActive) {
				Item.useAnimation = 40;
				Item.useTime = 40;
			}
			else {
				Item.useAnimation = 60;
				Item.useTime = 60;
			}
			return true;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.PinaColada)
				.AddIngredient(ItemID.TropicalSmoothie)
				.AddIngredient(ItemID.FruitJuice)
				.AddIngredient(ItemID.IceCream)
				.AddIngredient(ItemID.LunarBar)
				.AddTile(TileID.WorkBenches)
				.Register();
			CreateRecipe()
				.AddIngredient(ItemID.CrimtaneBar)
				.AddIngredient(ItemID.HellstoneBar)
				.AddIngredient(ItemID.DemonScythe)
				.AddIngredient(ItemID.Feather)
				.AddTile(TileID.WorkBenches)
				.Register();
			CreateRecipe()
				.AddIngredient(ItemID.DemoniteBar)
				.AddIngredient(ItemID.HellstoneBar)
				.AddIngredient(ItemID.DemonScythe)
				.AddIngredient(ItemID.Feather)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}
