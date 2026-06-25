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
	public class MedicineSticks : ModItem
	{
		public override void SetDefaults() {
			Item.width = 80;
			Item.height = 79;
			Item.accessory = true;
			Item.defense = 1;
			
			Item.value = Item.sellPrice(gold: 3);
			Item.rare = ItemRarityID.Red;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod) && calamityMod.TryFind("UelibloomBar", out ModItem uelibloom) ) {
				recipe.AddIngredient(uelibloom.Type);
			}
			recipe.AddIngredient(ItemID.FragmentSolar, 10);
			recipe.AddIngredient(ModContent.ItemType<RoyalBrooch>(), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<BooTaoPlayer>().MedicineSticks = true;
			if (player.GetModPlayer<BooTaoPlayer>().MScd > 0) {
				player.GetModPlayer<BooTaoPlayer>().MScd--;
			}
		}
	}
}
/*
https://github.com/tModLoader/tModLoader/blob/1.4.5/ExampleMod/Content/Items/Accessories/ExampleStatBonusAccessory.cs
https://github.com/tModLoader/tModLoader/wiki/Expert-Cross-Mod-Content
https://arknights.wiki.gg/wiki/Medicine_Sticks
*/