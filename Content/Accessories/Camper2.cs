using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using BooTao2.Systems;
using BooTao2.Content.Buffs;

namespace BooTao2.Content.Accessories
{
	public class Camper2 : ModItem
	{
		public override string Texture => "BooTao2/Content/Accessories/Camper";
		
		public override void SetDefaults() {
			Item.width = 64;
			Item.height = 64;
			Item.accessory = true;
			Item.buffType = ModContent.BuffType<CamperBuff>();
			Item.defense = 1;
			
			Item.value = Item.sellPrice(gold: 20);
			Item.rare = ItemRarityID.Green;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SpectreBar, 4);
			recipe.AddIngredient(ItemID.EyeoftheGolem, 1);
			recipe.AddIngredient(ItemID.DukeFishronTrophy, 1);
			recipe.AddIngredient(ItemID.FrozenClock, 1);
			recipe.AddIngredient(ItemID.BunnyCage, 1);
			recipe.AddIngredient(ItemID.FrostCore, 1);
			recipe.AddIngredient(ItemID.FleshGrinder, 1);
			recipe.AddIngredient(ItemID.Campfire, 1);
			recipe.AddIngredient(ModContent.ItemType<Camper>(), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			//player.GetModPlayer<BooTaoPlayer>().Camper = true;
			//player.GetModPlayer<BooTaoPlayer>().Camper2 = true;
			player.AddBuff(Item.buffType, 30);
		}
	}
}
/*



*/