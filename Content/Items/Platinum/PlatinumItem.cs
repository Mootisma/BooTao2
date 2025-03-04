using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;
using BooTao2.Content.Projectiles.Platinum;

namespace BooTao2.Content.Items.Platinum {
	public class PlatinumItem : ModItem {
		public int SP = 0;
		public int counter = 0;
		
		SoundStyle OffSkillATK = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Platinum/OffSkillATK") {
			Volume = 0.6f,
			PitchVariance = 0.2f,
			MaxInstances = 3,
		};
		
		SoundStyle OnSkillATK = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Platinum/OnSkillATK") {
			Volume = 0.6f,
			PitchVariance = 0.2f,
			MaxInstances = 3,
		};
		
		SoundStyle PlatSkill = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Platinum/PlatSkill") {
			Volume = 0.6f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		SoundStyle BreakTheirLegs = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Platinum/BreakTheirLegs") {
			Volume = 0.6f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		public override void SetDefaults() {
			Item.width = 32;
			Item.height = 32;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.sellPrice(0, 4, 5, 25);
			
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 50;
			Item.knockBack = 2.5f;
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod)) {
				Item.damage = 75;
				Item.knockBack = 3.5f;
			}
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 35;
			Item.useTime = 35;
			Item.autoReuse = true;
			Item.noUseGraphic = true;
			Item.noMelee = true;

			Item.shoot = ModContent.ProjectileType<PlatinumProj>();
			Item.shootSpeed = 20f;
		}
		
		public override bool CanUseItem(Player player) {
			if (SP >= 2999) {
				Item.useAnimation = 90;
				Item.useTime = 90;
				SoundEngine.PlaySound(OnSkillATK, player.Center);
			}
			SoundEngine.PlaySound(OffSkillATK, player.Center);
			return true;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			counter = 0;
			if (SP >= 2999) {
				Projectile.NewProjectile(source, position, velocity, type, (int)(damage * 2), knockback, player.whoAmI, 4f);
				return false;
			}
			return true;
		}
		
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			damage = (int)(damage + damage * counter / 150);
		}
		
		public override void HoldItem(Player player)
		{
			if (counter < 150) {
				counter++;
			}
			if (SP < 3000) {
				SP++;
			}
			if (SP == 2999) {
				SoundEngine.PlaySound(PlatSkill, player.Center);
				SoundEngine.PlaySound(BreakTheirLegs, player.Center);
			}
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.AdamantiteBar, 2)
				.AddIngredient(ItemID.PartyMinecart, 1)
				.AddIngredient(ItemID.UnicornHorn, 1)
				.AddIngredient(ItemID.WoodenArrow, 3)
				.AddTile(TileID.WorkBenches)
				.Register();
			CreateRecipe()
				.AddIngredient(ItemID.TitaniumBar, 2)
				.AddIngredient(ItemID.PartyMinecart, 1)
				.AddIngredient(ItemID.UnicornHorn, 1)
				.AddIngredient(ItemID.WoodenArrow, 3)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}