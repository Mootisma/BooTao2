using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;
using BooTao2.Content.Projectiles;
using BooTao2.Content.Projectiles.GreyThroat;

namespace BooTao2.Content.Items.GreyThroat {
	public class GreyThroatItem : ModItem {
		SoundStyle Skill = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Thorns/AtkBoost") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		private int timer_hi = 0;
		
		public override void SetDefaults() {
			Item.width = 32;
			Item.height = 32;
			Item.rare = ItemRarityID.Gray;
			Item.value = Item.sellPrice(0, 4, 0, 0);
			
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 42;
			Item.knockBack = 2.5f;
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod)) {
				Item.damage = 62;
				Item.knockBack = 3f;
			}
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 27;
			Item.useTime = 27;
			Item.autoReuse = true;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.UseSound = SoundID.Item5;

			Item.shoot = ModContent.ProjectileType<GreyThroatProj>();
			Item.shootSpeed = 17f;
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}
		
		public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2){
				if (player.GetModPlayer<BooTaoPlayer>().GreyThroatSP >= 30) {
					player.GetModPlayer<BooTaoPlayer>().GreyThroatSP = 0;
					timer_hi = 1200;
					SoundEngine.PlaySound(Skill, player.Center);
				}
				return false;
			}
			if (timer_hi > 0) {
				Item.useTime = 6;
				Item.useAnimation = 18;
				Item.reuseDelay = 9;
			}
			else {
				Item.useTime = 27;
				Item.useAnimation = 27;
				Item.reuseDelay = 0;
			}
			return true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			if (timer_hi > 0) {
				Projectile.NewProjectile(source, position, velocity, type, (int)(damage * 1.4), knockback, player.whoAmI, 4f);
				Projectile.NewProjectile(source, position, velocity, type, (int)(damage * 1.4), knockback, player.whoAmI, 4f);
				Projectile.NewProjectile(source, position, velocity, type, (int)(damage * 1.4), knockback, player.whoAmI, 4f);
				return false;
			}
			return true;
		}
		
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			if (Main.rand.NextBool(6)) {// 1 in 6
				damage = (int)(damage * 1.5);
			}
		}
		
		public override void UpdateInventory (Player player) {
			if (timer_hi > 0) {
				timer_hi--;
			}
		}
		
		public override void HoldItem(Player player) {
			if (player.GetModPlayer<BooTaoPlayer>().GreyThroatSP >= 30) {
				player.GetModPlayer<BooTaoPlayer>().SkillReady = true;
				if (player.ownedProjectileCounts[ModContent.ProjectileType<SkillReady>()] < 1) {
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ModContent.ProjectileType<SkillReady>(), 0, 4, player.whoAmI, 0f);
				}
			}
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.CobaltChainsaw, 4)
				.AddIngredient(ItemID.Feather, 4)
				.AddIngredient(ItemID.DaedalusStormbow, 1)
				.AddTile(TileID.WorkBenches)
				.Register();
			CreateRecipe()
				.AddIngredient(ItemID.PalladiumChainsaw, 4)
				.AddIngredient(ItemID.Feather, 4)
				.AddIngredient(ItemID.DaedalusStormbow, 1)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}