using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using BooTao2.Content.Dusts;

namespace BooTao2.Content.Items
{
	public class Homa4 : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 22;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			//Item.scale = 1f;
			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.useStyle = 1;
			Item.knockBack = 12;
			Item.pick = 205;
            Item.tileBoost = 4;
			Item.value = 2;
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
			recipe.AddIngredient(ModContent.ItemType<Homa3>(), 1);
			recipe.AddIngredient(ItemID.HallowedBar, 2);
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
			//
			player.scope = true;
			player.accStopwatch = true;
			player.longInvince = true;
            player.starCloak = true;
            player.panic = true;
			player.jumpBoost = true;
			player.doubleJumpCloud = true;
            player.doubleJumpBlizzard = true;
			player.doubleJumpSandstorm = true;
			player.autoJump = true;
			player.jumpSpeedBoost += 7f;
			player.extraFall += 40;
			player.luck += 0.05f;
			//player.blockRange += 10;//
            //Player.tileRangeX += 10;//
            //Player.tileRangeY += 10;//block placement ranges
		}
	}
}