using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using BooTao2.Content.Dusts;

namespace BooTao2.Content.Items
{
	public class Homa : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 7;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.useStyle = 1;
			Item.knockBack = 9;
			Item.pick = 59;
            Item.tileBoost = 1;
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
			recipe.AddIngredient(ItemID.CopperPickaxe, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		public override void UpdateInventory (Player player) {
			player.wallSpeed += 60;
			player.tileSpeed += 60;
			player.pickSpeed -= 0.35f;
			Lighting.AddLight(player.position, 1f, 1f, 1f);
			player.nightVision = true;
		}
	}
}