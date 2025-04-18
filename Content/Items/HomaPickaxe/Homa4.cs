using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using BooTao2.Content.Buffs;
using BooTao2.Systems;

namespace BooTao2.Content.Items.HomaPickaxe
{
	public class Homa4 : ModItem
	{
		public override string Texture => "BooTao2/Content/Items/HomaPickaxe/Homa";
		
		public override void SetDefaults()
		{
			Item.damage = 22;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.useStyle = 1;
			Item.knockBack = 12;
			Item.pick = 200;
            Item.tileBoost = 4;
			Item.value = 2;
			Item.rare = 10;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.maxStack = 1;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<Homa3>(), 1);
			recipe.AddIngredient(ItemID.HallowedBar, 2);
			if (ModContent.GetInstance<BooTaoServerConfig>().Homa4) {
				recipe.AddIngredient(ItemID.CrossNecklace, 1);
				recipe.AddIngredient(ItemID.PhilosophersStone, 1);
				recipe.AddIngredient(ItemID.PanicNecklace, 1);
				recipe.AddIngredient(ItemID.FrogLeg, 1);
				recipe.AddIngredient(ItemID.ManaFlower, 1);
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
			player.GetModPlayer<BooTaoPlayer>().HomaPickaxes[3] = ligma[3];
			player.GetModPlayer<BooTaoPlayer>().HomaPickaxes[4] = ligma[4];
		}
	}
}