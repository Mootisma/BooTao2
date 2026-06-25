using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace BooTao2.Content.Pets.Breeze
{
	public class BreezePetItem : ModItem
	{
		// Names and descriptions of all ExamplePetX classes are defined using .hjson files in the Localization folder
		public override void SetDefaults() {
			Item.CloneDefaults(ItemID.ZephyrFish); // Copy the Defaults of the Zephyr Fish Item.

			Item.shoot = ModContent.ProjectileType<BreezePetProj>(); // "Shoot" your pet projectile.
			Item.buffType = ModContent.BuffType<BreezePetBuff>(); // Apply buff upon usage of the Item.
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.buyPrice(silver: 1, copper: 50);
			
			Item.UseSound = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Breeze/SoundA") {
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
			CreateRecipe()
				.AddIngredient(ItemID.IceBlock, 1)
				.AddIngredient(ItemID.ColdWatersintheWhiteLand, 1)
				.AddIngredient(ItemID.PaintingColdSnap, 1)
				.AddTile(TileID.WorkBenches)
				.Register();
			CreateRecipe()
				.AddIngredient(ItemID.Meteorite, 1)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}