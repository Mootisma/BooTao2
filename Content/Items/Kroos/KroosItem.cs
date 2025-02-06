using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;
using BooTao2.Content.Projectiles.Kroos;

namespace BooTao2.Content.Items.Kroos {
	public class KroosItem : ModItem {
		public override void SetDefaults() {
			Item.width = 32;
			Item.height = 32;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.sellPrice(0, 0, 15, 20);
			
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 8;
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod)) {
				Item.damage = 10;
			}
			Item.knockBack = 0.2f;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.autoReuse = true;
			Item.noUseGraphic = true; // The sword is actually a "projectile"
			Item.noMelee = true; // The projectile will do the damage and not the item
			
			Item.UseSound = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Kroos/KroosAttacking") {
				Volume = 0.9f,
				PitchVariance = 0.2f,
				MaxInstances = 3,
			};

			Item.shoot = ModContent.ProjectileType<KroosProj>();
			Item.shootSpeed = 15f;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			if (player.GetModPlayer<BooTaoPlayer>().KroosSP >= 4) {
				Projectile.NewProjectile(source, position, velocity, type, (int)(damage * 1.4), knockback, player.whoAmI, 2f);
				Projectile.NewProjectile(source, position, velocity, type, (int)(damage * 1.4), knockback, player.whoAmI, 2f);
				player.GetModPlayer<BooTaoPlayer>().KroosSP = 0;
				return false;
			}
			return true;
		}
		
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			if (Main.rand.NextBool(5)) {// 1 in 5 
				damage = (int)(damage * 1.6);
			}
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.GoldBar, 8)
				.AddIngredient(ItemID.Wood, 4)
				.AddIngredient(ItemID.Silk, 3)
				.AddTile(TileID.WorkBenches)
				.Register();
			CreateRecipe()
				.AddIngredient(ItemID.PlatinumBar, 8)
				.AddIngredient(ItemID.Wood, 4)
				.AddIngredient(ItemID.Silk, 3)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}