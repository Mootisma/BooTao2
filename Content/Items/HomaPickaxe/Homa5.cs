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
			Item.pick = 350;
            Item.tileBoost = 5;
			Item.value = 3;
			Item.rare = 10;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.maxStack = 9999;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<Homa4>(), 1);
			recipe.AddIngredient(ItemID.Picksaw, 1);
			recipe.AddIngredient(ItemID.Ectoplasm, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		public override void UpdateInventory (Player player) {
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
			//player.scope = true;
			player.accStopwatch = true;
			player.longInvince = true;
            player.panic = true;
			player.manaFlower = true;
            player.pStone = true;
			player.jumpBoost = true;
			player.frogLegJumpBoost = true;
			player.autoJump = true;
			player.jumpSpeedBoost += 2f;
			player.luck += 0.05f;
			//player.blockRange += 10;//
            //Player.tileRangeX += 10;//
            //Player.tileRangeY += 10;//block placement ranges
			player.AddBuff(ModContent.BuffType<Platform1Buff2>(), 10, true);
		}
	}
}