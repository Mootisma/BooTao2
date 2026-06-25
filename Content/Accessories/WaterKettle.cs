using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using BooTao2.Systems;

namespace BooTao2.Content.Accessories
{
	public class WaterKettle : ModItem
	{
		public override void SetDefaults() {
			Item.width = 80;
			Item.height = 79;
			Item.accessory = true;
			
			Item.value = Item.sellPrice(silver: 2);
			Item.rare = ItemRarityID.Blue;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BottledWater, 2);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 1);
			recipe.AddIngredient(ItemID.LifeCrystal, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.statLifeMax2 += 20;
		}
	}
}
/*
https://arknights.wiki.gg/wiki/Hot_Water_Kettle
https://docs.tmodloader.net/docs/stable/class_player.html#a6d60c82c9303000b6b36720705290b5a


*/