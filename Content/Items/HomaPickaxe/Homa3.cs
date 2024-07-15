using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using BooTao2.Content.Dusts;//https://github.com/tModLoader/tModLoader/tree/1.4.4/ExampleMod/Content/Dusts

namespace BooTao2.Content.Items.HomaPickaxe
{
	public class Homa3 : ModItem
	{
		//public override void SetStaticDefaults()
		//{
		//	DisplayName.SetDefault("R3 Homa"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
		//	Tooltip.SetDefault("[c/FF9696:HP increased by 30%. Additionally, provides an ATK Bonus based on 1.2% of the wielder's Max HP.]\n[c/FF9696:When the wielder's HP is less than 50%, this ATK bonus is increased by an additional 1.4% of Max HP.]\nBuffs the Shrek Platform: Adds Bast Statue and Bewitching Table");
		//}//https://terraria.wiki.gg/wiki/Chat#Tags

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
			Item.maxStack = 9999;
			//Item.defense = 1;https://docs.tmodloader.net/docs/1.4-stable/class_terraria_1_1_item.html
		}

		public override void AddRecipes()
		{//Recipe recipe = Recipe.Create(ModContent.ItemType<Items.ExampleItem>(), 999);
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<Homa2>(), 1);
			recipe.AddIngredient(ItemID.HellstoneBar, 1);
			//recipe.AddIngredient(ItemID.HunterPotion, 1);
			//recipe.AddIngredient(ItemID.SpelunkerPotion, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		public override void UpdateInventory (Player player) {
			//player.tileRangeX += 10;//
            //player.tileRangeY += 10;//block placement ranges
			player.wallSpeed += 60;
			player.tileSpeed += 60;
			player.pickSpeed -= 0.35f;
			Lighting.AddLight(player.position, 1.1f, 1.1f, 1.1f);//crank it up to like 5 for something funny
			player.nightVision = true;
			//https://docs.tmodloader.net/docs/1.4-stable/class_terraria_1_1_player.html
			player.GetModPlayer<BooTaoPlayer>().Magnet = true;
			player.detectCreature = true;
			player.findTreasure = true;
			player.dangerSense = true;
			
			player.dontHurtCritters = true;
			player.accLavaFishing = true;
			player.accTackleBox = true;
			player.accOreFinder = true;
			player.accWeatherRadio = true;
			player.accThirdEye = true;
			player.GetModPlayer<BooTaoPlayer>().Shrek = true;
			//player.enemySpawns = true;
		}
	}
}