using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace BooTao2.Content.Pets.Arturia
{
	public class ArturiaPetItem : ModItem
	{
		// Names and descriptions of all ExamplePetX classes are defined using .hjson files in the Localization folder
		public override void SetDefaults() {
			Item.CloneDefaults(ItemID.ZephyrFish); // Copy the Defaults of the Zephyr Fish Item.

			Item.shoot = ModContent.ProjectileType<ArturiaPetProj>(); // "Shoot" your pet projectile.
			Item.buffType = ModContent.BuffType<ArturiaPetBuff>(); // Apply buff upon usage of the Item.
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.buyPrice(silver: 1, copper: 50);
			
			Item.UseSound = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/ArturiaVO1") {
				Volume = 1f,
				PitchVariance = 0f,
				MaxInstances = 2,
			};
		}

		public override bool? UseItem(Player player) {
			if (player.whoAmI == Main.myPlayer) {
				player.AddBuff(Item.buffType, 3600);
			}
			return true;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Harp, 1);
			recipe.AddIngredient(ItemID.SoulofLight, 1);
			recipe.AddIngredient(ItemID.SoulofNight, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}