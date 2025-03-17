using BooTao2.Content.Projectiles.Kafka;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace BooTao2.Content.Items.Kafka
{
	public class KafkaItem : ModItem
	{
		SoundStyle KafkaSkill = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Kafka/KafkaSkill") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		SoundStyle KafkaFUA = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Kafka/2KafkaFUA") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		public override void SetDefaults() {
			Item.damage = 30;
			Item.knockBack = 2.4f;
			Item.mana = 12; // mana cost
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 70;
			Item.useAnimation = 70;
			Item.reuseDelay = 0;
			Item.useStyle = ItemUseStyleID.Swing; // how the player's arm moves when using the item
			Item.noUseGraphic = true;
			Item.value = Item.sellPrice(gold: 5);
			Item.rare = ItemRarityID.Purple;

			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.shoot = ModContent.ProjectileType<KafkaProj>();
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			if (player.altFunctionUse != 2) {
				position = Main.MouseWorld;
				velocity = Vector2.Zero;
			}
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			if (player.altFunctionUse == 2) {
				SoundEngine.PlaySound(KafkaFUA, player.Center);
			}
			else {
				SoundEngine.PlaySound(KafkaSkill, player.Center);
			}
			return true;
		}

		public override bool CanUseItem(Player player) {
			if (player.statMana < Item.mana)
				return false;
			if (player.altFunctionUse == 2) {
				Item.useTime = 3;
				Item.useAnimation = 15;
				Item.shoot = ModContent.ProjectileType<KafkaProj2>();
				Item.shootSpeed = 25f;
				Item.reuseDelay = 15;
				//SoundEngine.PlaySound(KafkaFUA, player.Center);
			}
			else {
				Item.useTime = 70;
				Item.useAnimation = 70;
				Item.shoot = ModContent.ProjectileType<KafkaProj>();
				Item.shootSpeed = 0f;
				Item.reuseDelay = 0;
				//SoundEngine.PlaySound(KafkaSkill, player.Center);
			}
			return true;
		}
		
		public override void AddRecipes() {
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Stinger, 1);
			recipe.AddIngredient(ItemID.MeteoriteBar, 1);
			recipe.AddIngredient(ItemID.Cobweb, 1);
			recipe.AddIngredient(ItemID.FlintlockPistol, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
/*
https://docs.tmodloader.net/docs/stable/class_n_p_c.html
https://docs.tmodloader.net/docs/stable/class_projectile.html
https://github.com/ThePaperLuigi/The-Stars-Above/blob/main/Projectiles/Ranged/PleniluneGaze/FrostflakeArrow.cs
https://docs.tmodloader.net/docs/stable/class_projectile.html#a76031be1e4228ce7e8a3ec67e2c293ad
https://docs.tmodloader.net/docs/stable/class_mod_projectile.html
https://hsr.yatta.top/en/archive/avatar/1013/herta?mode=details
https://static.wikia.nocookie.net/houkai-star-rail/images/9/96/Herta_Sticker_02.png/revision/latest?cb=20231220232741

*/