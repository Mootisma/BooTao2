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
		public override void SetDefaults() {
			Item.damage = 27;
			Item.knockBack = 2.4f;
			Item.mana = 12; // mana cost
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing; // how the player's arm moves when using the item
			Item.noUseGraphic = true;
			Item.value = Item.sellPrice(gold: 5);
			Item.rare = ItemRarityID.Purple;
			/*Item.UseSound = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Herta/DoYouKnowHerta") {
				Volume = 0.9f,
				PitchVariance = 0f,
				MaxInstances = 1,
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew//ReplaceOldest
			};*/

			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.shoot = ModContent.ProjectileType<KafkaProj>();
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			position = Main.MouseWorld;
			velocity = Vector2.Zero;
		}

		//public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {}

		public override void AddRecipes() {
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.GoldenKey, 1);
			recipe.AddIngredient(ItemID.MeteoriteBar, 1);
			recipe.AddIngredient(ItemID.HellstoneBar, 1);
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