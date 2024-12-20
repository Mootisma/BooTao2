using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;
using BooTao2.Content.Projectiles.Lancet2;
using BooTao2.Content.Buffs.Lancet2;

namespace BooTao2.Content.Items.Lancet2 {
	public class Lancet2Item : ModItem {
		public override void SetDefaults() {
			Item.damage = 10;
			Item.knockBack = 0f;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noUseGraphic = true;
			Item.value = Item.sellPrice(silver: 1);
			Item.rare = ItemRarityID.White;

			Item.noMelee = true; 
			Item.DamageType = DamageClass.Summon;
			Item.buffType = ModContent.BuffType<Lancet2Buff>();
			Item.shoot = ModContent.ProjectileType<Lancet2Minion>();
			Item.shootSpeed = 1f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 1);
			recipe.AddIngredient(ItemID.LifeCrystal, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		public override void ModifyShootStats (Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			position = Main.MouseWorld;
			velocity = Vector2.Zero;
		}
		
		int counter = 0;
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			// only one mostima
			foreach (var proj in Main.ActiveProjectiles)
            {
                if (proj.active && proj.type == Item.shoot && proj.owner == player.whoAmI)
                {
                    proj.active = false;
                }
            }
			
			player.AddBuff(Item.buffType, 2);
			
			// spawn at mouse position
			var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
			projectile.originalDamage = Item.damage;
			counter = 7200;
			return false;
		}
		
		public override bool CanUseItem(Player player) {
			if (counter <= 0) {
				return true;
			}
			else {
				return false;
			}
		}
		
		public override void UpdateInventory (Player player) {
			if (counter > 0)
				counter--;
		}
	}
}