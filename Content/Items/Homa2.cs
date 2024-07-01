using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using BooTao2.Content.Dusts;//https://github.com/tModLoader/tModLoader/tree/1.4.4/ExampleMod/Content/Dusts

namespace BooTao2.Content.Items
{
	public class Homa2 : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 11;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.useStyle = 1;
			Item.knockBack = 9;
			Item.pick = 90;
            Item.tileBoost = 2;
			Item.value = 1;
			Item.rare = 10;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.maxStack = 9999;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<Homa>(), 1);
			recipe.AddIngredient(ItemID.DeathbringerPickaxe, 1);
			recipe.AddIngredient(ItemID.HunterPotion, 1);
			recipe.AddIngredient(ItemID.SpelunkerPotion, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		public override void UpdateInventory (Player player) {
			player.wallSpeed += 60;
			player.tileSpeed += 60;
			player.pickSpeed -= 0.35f;
			Lighting.AddLight(player.position, 1f, 1f, 1f);
			player.nightVision = true;
			player.GetModPlayer<BooTaoPlayer>().Magnet = true;
			player.detectCreature = true;
			player.findTreasure = true;
		}
	}
}