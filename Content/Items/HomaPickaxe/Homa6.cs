using Terraria;
using Terraria.ID;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using BooTao2.Content.Buffs;

namespace BooTao2.Content.Items.HomaPickaxe
{
	public class Homa6 : ModItem
	{
		public override string Texture => "BooTao2/Content/Items/HomaPickaxe/Homa";
		
		public override void SetDefaults()
		{
			Item.damage = 60;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.useStyle = 1;
			Item.knockBack = 12;
			Item.pick = 250;
            Item.tileBoost = 6;
			Item.value = Item.sellPrice(gold: 25);
			Item.rare = 10;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.maxStack = 1;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<Homa5>(), 1);
			recipe.AddIngredient(ItemID.LunarBar, 3);
			recipe.AddIngredient(ItemID.AvengerEmblem, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		public override void UpdateInventory (Player player) {
			player.AddBuff(ModContent.BuffType<HomaPickaxeBuff>(), 10, true);
			player.GetModPlayer<BooTaoPlayer>().Homa2 = true;
			player.GetModPlayer<BooTaoPlayer>().Homa3 = true;
			player.GetModPlayer<BooTaoPlayer>().Homa4 = true;
			player.GetModPlayer<BooTaoPlayer>().Homa5 = true;
			player.GetModPlayer<BooTaoPlayer>().Homa6 = true;
			
		}
	}
}