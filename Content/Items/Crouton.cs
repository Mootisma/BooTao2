using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Items
{
	public class Crouton : ModItem
	{
		public override void SetDefaults() {
			Item.damage = 1;
			Item.DamageType = DamageClass.Melee;
			Item.crit = 1;
			Item.width = 33;
			Item.height = 31;
			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 11;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.axe = 70; // the axe power is this value multiplied by 5
			Item.hammer = 150;
			Item.tileBoost = 5;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.CopperAxe, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		public override void HoldItem(Player player)
		{
			player.statDefense += 500;
			player.lifeRegen += 100;
			base.HoldItem(player);
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}
		public override void ModifyWeaponDamage(Player player, ref StatModifier damage){
			if (player.altFunctionUse == 2){
				damage *= 500;
			}
		}
	}
}