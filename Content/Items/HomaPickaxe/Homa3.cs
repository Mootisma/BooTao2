using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using BooTao2.Content.Buffs;

namespace BooTao2.Content.Items.HomaPickaxe
{
	public class Homa3 : ModItem
	{
		//public override void SetStaticDefaults()
		//{
		//	DisplayName.SetDefault("R3 Homa"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
		//	Tooltip.SetDefault("[c/FF9696:HP increased by 30%. Additionally, provides an ATK Bonus based on 1.2% of the wielder's Max HP.]\n[c/FF9696:When the wielder's HP is less than 50%, this ATK bonus is increased by an additional 1.4% of Max HP.]\nBuffs the Shrek Platform: Adds Bast Statue and Bewitching Table");
		//}//https://terraria.wiki.gg/wiki/Chat#Tags

		public override string Texture => "BooTao2/Content/Items/HomaPickaxe/Homa";
		
		public override void SetDefaults()
		{
			Item.damage = 15;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			//Item.scale = 1f;
			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.useStyle = 1;
			Item.knockBack = 12;
			Item.pick = 110;
            Item.tileBoost = 3;
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
			recipe.AddIngredient(ModContent.ItemType<Homa2>(), 1);
			recipe.AddIngredient(ItemID.HellstoneBar, 1);
			recipe.AddIngredient(ItemID.CatBast, 1);
			recipe.AddIngredient(ItemID.BewitchingTable, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		public override void UpdateInventory (Player player) {
			player.AddBuff(ModContent.BuffType<HomaPickaxeBuff>(), 10, true);
			player.GetModPlayer<BooTaoPlayer>().Homa2 = true;
			player.GetModPlayer<BooTaoPlayer>().Homa3 = true;
		}
	}
}