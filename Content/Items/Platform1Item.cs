using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using BooTao2.Content.Dusts;//https://github.com/tModLoader/tModLoader/tree/1.4.4/ExampleMod/Content/Dusts

namespace BooTao2.Content.Items
{
	public class Platform1Item : ModItem
	{
		//public override void SetStaticDefaults()
		//{
		//	DisplayName.SetDefault("Shrek Platform"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
		//	Tooltip.SetDefault("I promise this is QOL");
		//}//https://terraria.wiki.gg/wiki/Chat#Tags

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 36;
			Item.maxStack = 9999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 8;
			Item.useTime = 8;
			Item.useStyle = 1;
			//item.consumable = true;
			Item.value = 2;
			Item.rare = 5;
			//Item.createTile = mod.TileType("Platform1");
			Item.createTile = ModContent.TileType<Tiles.Platform1>();
		}

		public override void AddRecipes()
		{//Recipe recipe = Recipe.Create(ModContent.ItemType<Items.ExampleItem>(), 999);
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}