using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using BooTao2.Content.Buffs;
using BooTao2.Systems;

namespace BooTao2.Content.Items.HomaPickaxe
{
	public class Homa2 : ModItem
	{
		public override string Texture => "BooTao2/Content/Items/HomaPickaxe/Homa";
		
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
			Item.maxStack = 1;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<Homa>(), 1);
			recipe.AddIngredient(ItemID.DeathbringerPickaxe, 1);
			if (ModContent.GetInstance<BooTaoServerConfig>().Homa2) {
				recipe.AddIngredient(ItemID.HunterPotion, 1);
				recipe.AddIngredient(ItemID.SpelunkerPotion, 1);
				recipe.AddIngredient(ItemID.Campfire, 1);
				recipe.AddIngredient(ItemID.StarinaBottle, 1);
			}
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<Homa>(), 1);
			recipe.AddIngredient(ItemID.NightmarePickaxe, 1);
			if (ModContent.GetInstance<BooTaoServerConfig>().Homa2) {
				recipe.AddIngredient(ItemID.HunterPotion, 1);
				recipe.AddIngredient(ItemID.SpelunkerPotion, 1);
				recipe.AddIngredient(ItemID.Campfire, 1);
				recipe.AddIngredient(ItemID.StarinaBottle, 1);
			}
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		public override void UpdateInventory (Player player) {
			player.AddBuff(ModContent.BuffType<HomaPickaxeBuff>(), 10, true);
			bool[] ligma = player.GetModPlayer<BooTaoPlayer>().GetHomaConfig();
			player.GetModPlayer<BooTaoPlayer>().HomaPickaxes[0] = ligma[0];
			player.GetModPlayer<BooTaoPlayer>().HomaPickaxes[1] = ligma[1];
			player.GetModPlayer<BooTaoPlayer>().HomaPickaxes[2] = ligma[2];
		}
	}
}