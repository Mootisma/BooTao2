using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using BooTao2.Systems;

namespace BooTao2.Content.Accessories
{
	public class OrangeStorm : ModItem
	{
		public override void SetDefaults() {
			Item.width = 75;
			Item.height = 81;
			Item.accessory = true;
			Item.defense = 1;
			
			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ItemRarityID.Orange;
		}
		
		public override void AddRecipes()
		{
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod) && calamityMod.TryFind("PurifiedGel", out ModItem purifiedGel) ) {
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(purifiedGel.Type);
				recipe.AddIngredient(ItemID.Bone, 1);
				recipe.AddIngredient(ItemID.OrangeBloodroot, 1);
				recipe.AddTile(TileID.WorkBenches);
				recipe.Register();
				
				recipe = CreateRecipe();
				recipe.AddIngredient(purifiedGel.Type);
				recipe.AddIngredient(ItemID.Bone, 1);
				recipe.AddIngredient(ItemID.HellstoneBar, 1);
				recipe.AddTile(TileID.WorkBenches);
				recipe.Register();
			}
			else {
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ItemID.Bone, 1);
				recipe.AddIngredient(ItemID.OrangeBloodroot, 1);
				recipe.AddTile(TileID.WorkBenches);
				recipe.Register();
				
				recipe = CreateRecipe();
				recipe.AddIngredient(ItemID.Bone, 1);
				recipe.AddIngredient(ItemID.HellstoneBar, 1);
				recipe.AddTile(TileID.WorkBenches);
				recipe.Register();
			}
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.maxMinions += 1;
			player.GetDamage(DamageClass.Generic) += 0.07f;
		}
	}
}
/*
https://arknights.wiki.gg/wiki/Orange_Storm
https://github.com/tModLoader/tModLoader/wiki/Expert-Cross-Mod-Content
https://terraria.wiki.gg/wiki/Stardust_Fragment
https://github.com/tModLoader/tModLoader/blob/1.4.5/ExampleMod/Content/Items/Accessories/ExampleStatBonusAccessory.cs
*/