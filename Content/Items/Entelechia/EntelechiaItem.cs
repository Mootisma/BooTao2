using BooTao2.Content.Projectiles.LaPluma;
using BooTao2.Content.Projectiles.Entelechia;
using BooTao2.Content.Projectiles;
using BooTao2.Content.Buffs.LaPluma;
using BooTao2.Content.Buffs.Entelechia;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace BooTao2.Content.Items.Entelechia
{
	public class EntelechiaItem : ModItem
	{
		private int SP = 15;
		private int timer = 0;
		private bool SkillActive = false;
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
		
		SoundStyle SkillActivationVO = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Entelechia/EnteSkillVO") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		public override void SetDefaults() {
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod)) {
				Item.damage = 1000;
			}
			else {
				Item.damage = 250;
			}
			Item.knockBack = 5f;
			Item.useStyle = 1;
			Item.useAnimation = 50;
			Item.useTime = 50;
			Item.width = 32;
			Item.height = 32;
			//Item.UseSound = SoundID.Item1;
			Item.DamageType = DamageClass.Melee;
			Item.autoReuse = true;
			Item.noUseGraphic = true; // The sword is actually a "projectile"
			Item.noMelee = true; // The projectile will do the damage and not the item
			Item.rare = ItemRarityID.LightRed;
			Item.value = Item.sellPrice(0, 35, 0, 0);

			Item.shoot = ModContent.ProjectileType<LaPlumaProj>();
			Item.shootSpeed = 20f;
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			if (SkillActive) {
				Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<EntelechiaProj>(), (int)(damage * 2), knockback, player.whoAmI, 0f);
				Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<EntelechiaProj>(), (int)(damage * 2), knockback, player.whoAmI, 8f);
				return false;
			}
			SoundEngine.PlaySound(Attack, player.Center);
			return true;
		}
		
		public override void HoldItem(Player player) {
			player.GetModPlayer<BooTaoPlayer>().LaPlumaHolding = true;
			player.AddBuff(ModContent.BuffType<EntelechiaBuff>(), 600, true);
			//
			if (!SkillActive && SP >= 20) {
				player.GetModPlayer<BooTaoPlayer>().SkillReady = true;
				if (player.ownedProjectileCounts[ModContent.ProjectileType<SkillReady>()] < 1) {
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ModContent.ProjectileType<SkillReady>(), 0, 4, player.whoAmI, 0f);
				}
			}
		}
		
		public override void UpdateInventory (Player player) {
			if (timer > 0) { timer--; }
			player.GetModPlayer<BooTaoPlayer>().EntelechiaStoreMouse = Main.MouseWorld;
			if (timer == 0) {
				timer = 60;
				if (SP < 30) { SP++; }
				if (player.GetModPlayer<BooTaoPlayer>().EnteReviveCD > 0) {
					player.GetModPlayer<BooTaoPlayer>().EnteReviveCD--;
				}
				if (SkillActive) {
					SP = 0;
					SkillDuration--;
					if (SkillDuration <= 0) {
						SkillDuration = 0;
						SkillActive = false;
					}
				}
			}
		}
		
		public override bool CanUseItem(Player player) {
			if (SkillActive){ return false; }
			if (player.altFunctionUse == 2){
				if (!SkillActive && SP >= 20) {
					timer = 60;
					SkillActive = true;
					SkillDuration = 12;
					SoundEngine.PlaySound(Skill, player.Center);
					SoundEngine.PlaySound(SkillActivationVO, player.Center);
					return true;
				}
				return false;
			}
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod) && calamityMod.TryFind("TwistingNether", out ModItem TwistingNether) ) {
				recipe.AddIngredient(TwistingNether.Type);
			}
			recipe.AddIngredient(ItemID.LunarBar, 1);
			recipe.AddIngredient(ItemID.BloodyMoscato, 1);
			recipe.AddIngredient(ItemID.ScytheWhip, 1);//dark harvest
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
