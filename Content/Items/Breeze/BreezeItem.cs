using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;
using BooTao2.Content.Projectiles.Breeze;
using BooTao2.Systems;

namespace BooTao2.Content.Items.Breeze
{
	public class BreezeItem : ModItem
	{
		SoundStyle BreezeSelect1 = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Breeze/BreezeSelect1") {
			Volume = 1.0f,
			PitchVariance = 0f,
			MaxInstances = 1,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle BreezeSelect2 = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Breeze/BreezeSelect2") {
			Volume = 1.0f,
			PitchVariance = 0f,
			MaxInstances = 1,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		public override void SetDefaults() 
		{
			Item.width = 32;
			Item.height = 32;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.sellPrice(0, 2, 67, 0);
			
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 42;
			Item.knockBack = 3f;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 48;
			Item.useTime = 48;
			Item.autoReuse = true;
			Item.noUseGraphic = true; // The sword is actually a "projectile"
			Item.noMelee = true; // The projectile will do the damage and not the item
			
			Item.UseSound = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Breeze/BreezeUse") {
				Volume = 0.7f,
				PitchVariance = 0.2f,
				MaxInstances = 5,
			};

			Item.shoot = ModContent.ProjectileType<BreezeProj>();
			Item.shootSpeed = 22f;
		}
		
		public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            float damageMult = 1f;
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod)) {
				damageMult += 0.2f;
			}
			damage *= damageMult;
        }
		
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			if (Main.rand.NextBool(5)) {// 1 in 5 
				damage = (int)(damage * 1.5);
			}
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			if (player.altFunctionUse == 2) {
				Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
				Projectile.NewProjectile(source, target, Vector2.Zero, ModContent.ProjectileType<BreezeProj2>(), damage * 2, knockback, player.whoAmI, 0, 0);
				return false;
			}
			return true;
		}
		
		int ligma = 0;
		public override void UpdateInventory (Player player) {
			if (ligma > 0) {
				ligma--;
			}
		}
		
		public override void HoldItem(Player player) {
			if (ligma == 0) {
				if (Main.rand.Next(2) == 1) {
					SoundEngine.PlaySound(BreezeSelect1, player.Center);
				}
				else {
					SoundEngine.PlaySound(BreezeSelect2, player.Center);
				}
			}
			ligma = 2;
		}

		public override void AddRecipes() {
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod)) {
				if (calamityMod.TryFind("EssenceofHavoc", out ModItem EssenceofHavoc) ) {
					CreateRecipe()
						.AddIngredient(ItemID.HallowedBar, 1)
						.AddIngredient(ItemID.PrettyPinkRibbon, 1)
						.AddIngredient(ItemID.FoxEars, 1)
						.AddIngredient(EssenceofHavoc.Type)
						.AddTile(TileID.WorkBenches)
						.Register();
				}
				if (calamityMod.TryFind("Hellborn", out ModItem Hellborn) ) {
					CreateRecipe()
						.AddIngredient(ItemID.HallowedBar, 3)
						.AddIngredient(Hellborn.Type)
						.AddTile(TileID.WorkBenches)
						.Register();
				}
			}
			else {
				CreateRecipe()
					.AddIngredient(ItemID.HallowedBar, 1)
					.AddIngredient(ItemID.PrettyPinkRibbon, 1)
					.AddIngredient(ItemID.FoxEars, 1)
					.AddTile(TileID.WorkBenches)
					.Register();
				CreateRecipe()
					.AddIngredient(ItemID.HallowedBar, 3)
					.AddTile(TileID.WorkBenches)
					.Register();
			}
		}
	}
}
/*
https://github.com/tModLoader/tModLoader/wiki/Intermediate-Recipes#cross-mod-recipes
*/