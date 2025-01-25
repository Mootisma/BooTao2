using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using BooTao2.Content.Buffs;
using BooTao2.Systems;

namespace BooTao2.Content.Items.HomaPickaxe
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
		}

		public override void AddRecipes()
		{
			//ModContent.GetInstance<BooTaoServerConfig>().Homa1
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.CopperPickaxe, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		public override void UpdateInventory (Player player) {
			player.AddBuff(ModContent.BuffType<HomaPickaxeBuff>(), 10, true);
			bool[] ligma = player.GetModPlayer<BooTaoPlayer>().GetHomaConfig();
			player.GetModPlayer<BooTaoPlayer>().HomaPickaxes[0] = ligma[0];
			player.GetModPlayer<BooTaoPlayer>().HomaPickaxes[1] = ligma[1];
		}
	}
}