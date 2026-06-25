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
	public class CoffeeBeans : ModItem
	{
		public override void SetDefaults() {
			Item.width = 80;
			Item.height = 79;
			Item.accessory = true;
			Item.defense = 1;
			
			Item.value = Item.sellPrice(gold: 2);
			Item.rare = ItemRarityID.Cyan;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod) && calamityMod.TryFind("UnholyEssence", out ModItem unholyessence) ) {
				recipe.AddIngredient(unholyessence.Type);
			}
			recipe.AddIngredient(ItemID.FragmentStardust, 10);
			recipe.AddIngredient(ModContent.ItemType<WaterKettle>(), 1);
			recipe.AddIngredient(ModContent.ItemType<OrangeStorm>(), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.maxMinions += 1;
			player.maxTurrets += 1;
			player.statLifeMax2 += 20;
		}
	}
}
/*
https://arknights.wiki.gg/wiki/Coffee_Plains_Coffee_Candy
https://github.com/tModLoader/tModLoader/wiki/Expert-Cross-Mod-Content
https://terraria.wiki.gg/wiki/Stardust_Fragment
https://github.com/tModLoader/tModLoader/blob/1.4.5/ExampleMod/Content/Items/Accessories/ExampleStatBonusAccessory.cs
*/