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
	public class BreezeItemSW : ModItem
	{
		public override string Texture => "BooTao2/Content/Items/Breeze/BreezeItem";
		
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
			Item.value = Item.sellPrice(2, 1, 0, 0);
			
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 200;
			Item.knockBack = 6f;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 18;
			Item.useTime = 18;
			Item.autoReuse = true;
			Item.noUseGraphic = true; // The sword is actually a "projectile"
			Item.noMelee = true; // The projectile will do the damage and not the item
			
			Item.UseSound = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Breeze/BreezeUse") {
				Volume = 0.6f,
				PitchVariance = 0.2f,
				MaxInstances = 2,
			};

			Item.shoot = ModContent.ProjectileType<BreezeProjSW>();
			Item.shootSpeed = 25f;
		}
		
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			if (Main.rand.NextBool(4)) {// 1 in 4 
				damage = (int)(damage * 2);
			}
		}
		
		public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            float damageMult = 1f;
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod)) {
				damageMult += 2f;
			}
			damage *= damageMult;
        }
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			if (player.altFunctionUse == 2) {
				Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
				Projectile.NewProjectile(source, target, Vector2.Zero, ModContent.ProjectileType<BreezeProj2>(), damage * 2, knockback, player.whoAmI, 10, 10);
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
			CreateRecipe()
				.AddIngredient<Items.Breeze.BreezeItem>()
				.AddIngredient<Items.SilverWolfMaterial>()
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}
/*
https://github.com/tModLoader/tModLoader/wiki/Intermediate-Recipes#cross-mod-recipes
*/