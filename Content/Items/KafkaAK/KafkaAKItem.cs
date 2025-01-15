using BooTao2.Content.Projectiles;
using BooTao2.Content.Projectiles.KafkaAK;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace BooTao2.Content.Items.KafkaAK
{
	public class KafkaAKItem : ModItem
	{
		public override void SetDefaults() {
			Item.damage = 50;
			Item.knockBack = 1f;
			Item.mana = 12; // mana cost
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 20;
			Item.useAnimation = 20;
			//Item.reuseDelay = 300;
			Item.useStyle = ItemUseStyleID.HoldUp; // how the player's arm moves when using the item
			Item.noUseGraphic = true;
			Item.value = Item.sellPrice(gold: 5);
			Item.rare = ItemRarityID.Yellow;

			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.shoot = ModContent.ProjectileType<KafkaAKProj>();
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			position = Main.MouseWorld;
			velocity = Vector2.Zero;
		}
		
		int counter = 0;
		public override bool CanUseItem(Player player) {
			if (counter <= 0) {
				counter = 900;
				return true;
			}
			else {
				return false;
			}
		}
		
		public override void UpdateInventory (Player player) {
			if (counter > 0)
				counter--;
		}
		
		public override void HoldItem(Player player) {
			if (counter <= 0) {
				player.GetModPlayer<BooTaoPlayer>().SkillReady = true;
				if (player.ownedProjectileCounts[ModContent.ProjectileType<SkillReady>()] < 1) {
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ModContent.ProjectileType<SkillReady>(), 0, 4, player.whoAmI, 0f);
				}
			}
		}
		
		public override void AddRecipes() {
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.StylistKilLaKillScissorsIWish, 1);
			recipe.AddIngredient(ItemID.CompanionCube, 1);
			recipe.AddIngredient(ItemID.Bed, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
/*
https://docs.tmodloader.net/docs/stable/class_mod_projectile.html
https://docs.tmodloader.net/docs/stable/class_utils.html
https://docs.tmodloader.net/docs/stable/class_global_n_p_c.html
https://docs.tmodloader.net/docs/stable/class_n_p_c.html
https://aceship.github.io/AN-EN-Tags/akhrchars.html?opname=Kafka
https://terraria.wiki.gg/wiki/Dust_IDs
https://terraria.wiki.gg/wiki/Item_IDs
https://terraria.wiki.gg/wiki/Companion_Cube
https://github.com/tModLoader/tModLoader/wiki
https://github.com/tModLoader/tModLoader/wiki/Geometry

*/