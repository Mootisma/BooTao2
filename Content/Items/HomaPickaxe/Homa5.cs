using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using BooTao2.Content.Dusts;
using BooTao2.Content.Buffs;

namespace BooTao2.Content.Items.HomaPickaxe
{
	public class Homa5 : ModItem
	{
		public override string Texture => "BooTao2/Content/Items/HomaPickaxe/Homa";
		
		public override void SetDefaults()
		{
			Item.damage = 39;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.useStyle = 1;
			Item.knockBack = 12;
			Item.pick = 220;
            Item.tileBoost = 5;
			Item.value = Item.sellPrice(gold: 20);
			Item.rare = 10;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.maxStack = 1;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<Homa4>(), 1);
			recipe.AddIngredient(ItemID.Picksaw, 1);
			recipe.AddIngredient(ItemID.Ectoplasm, 1);
			recipe.AddIngredient(ItemID.BundleofBalloons, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		public override void UpdateInventory (Player player) {
			player.AddBuff(ModContent.BuffType<HomaPickaxeBuff>(), 10, true);
			player.GetModPlayer<BooTaoPlayer>().Homa2 = true;
			player.GetModPlayer<BooTaoPlayer>().Homa3 = true;
			player.GetModPlayer<BooTaoPlayer>().Homa4 = true;
			player.GetModPlayer<BooTaoPlayer>().Homa5 = true;
		}
	}
}