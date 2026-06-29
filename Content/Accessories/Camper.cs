using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using BooTao2.Systems;
using BooTao2.Content.Buffs;

namespace BooTao2.Content.Accessories
{
	public class Camper : ModItem
	{
		public override void SetDefaults() {
			Item.width = 64;
			Item.height = 64;
			Item.accessory = true;
			
			Item.value = Item.sellPrice(silver: 50);
			Item.rare = ItemRarityID.Blue;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LifeCrystal, 1);
			recipe.AddIngredient(ItemID.Campfire, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<BooTaoPlayer>().Camper = true;
		}
	}
}
/*



*/