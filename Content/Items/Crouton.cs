using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Items
{
	public class Crouton : ModItem
	{
		//public override void SetStaticDefaults()
		//{
		//	DisplayName.SetDefault("Crouton"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
		//	Tooltip.SetDefault("it is a hammer");
		//}//https://terraria.wiki.gg/wiki/Chat#Tags
		
		public override void SetDefaults() {
			Item.damage = 1;
			Item.DamageType = DamageClass.Melee;
			Item.crit = -1;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 11;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true; // Automatically re-swing/re-use this item after its swinging animation is over.
			Item.maxStack = 9999;
			Item.axe = 30; // How much axe power the weapon has, note that the axe power displayed in-game is this value multiplied by 5
			Item.hammer = 100; // How much hammer power the weapon has
			Item.tileBoost = 5;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{//Recipe recipe = Recipe.Create(ModContent.ItemType<Items.ExampleItem>(), 999);
			Recipe recipe = CreateRecipe();
			//recipe.AddIngredient(ModContent.ItemType<Homa2>(), 1);
			recipe.AddIngredient(ItemID.DirtBlock, 1);
			//recipe.AddIngredient(ItemID.HunterPotion, 1);
			//recipe.AddIngredient(ItemID.SpelunkerPotion, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		//int cooldown = 0;
		public override void HoldItem(Player player)
		{
			/*cooldown++;
			
			if (cooldown >= 20)
			{
				player.GetModPlayer<WeaponPlayer>().aegisGauge += 1;
				cooldown = 0;
			}
			
			
			*/
			player.statDefense += 500;
			player.lifeRegen += 100;
			base.HoldItem(player);
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}
		public override bool CanUseItem(Player player){
			if (player.altFunctionUse == 2){
				Item.damage = 1250;
				Item.scale = 2f;
				Item.crit = 96;
				Item.axe = 0;
				Item.hammer = 0;
				Item.tileBoost = 0;
				Item.useTime = 18;
				Item.useAnimation = 18;
			}
			else{
				Item.damage = 1;
				Item.scale = 1f;
				Item.crit = -1;
				Item.axe = 30;
				Item.hammer = 100;
				Item.tileBoost = 5;
				Item.useTime = 6;
				Item.useAnimation = 6;
			}
			return base.CanUseItem(player);
		}
	}
}