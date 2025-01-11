using BooTao2.Content.Projectiles.Qingque;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BooTao2.Content.Items.Qingque
{
	public class QingqueItem : ModItem
	{
		public bool CalamityActive = ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);
		public int QingqueStacks = 0;
		private int TileNum = 0;
		public int ChosenTile = 0;
		
		public override void SetDefaults() {
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.autoReuse = false;
			//
			Item.shootSpeed = 20f;
			Item.shoot = ModContent.ProjectileType<QingqueProj>();
			//
			Item.width = 50;
			Item.height = 50;
			Item.UseSound = SoundID.Item20;

			// Weapon Properties
			Item.damage = (CalamityActive) ? 50 : 40;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 20;
			Item.crit = 6;
			Item.knockBack = 3.5f;

			Item.rare = ItemRarityID.Purple;
			Item.value = Item.sellPrice(silver: 30);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.ShadowScale, 1);
			recipe.AddIngredient(ItemID.Obsidian, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.TissueSample, 1);
			recipe.AddIngredient(ItemID.Obsidian, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		//proj.ai[]: tile position (for holding), basic/enhanced basic, basic/enhanced's tile type
		public override void HoldItem(Player player) {
			player.GetModPlayer<BooTaoPlayer>().QingqueHolding = true;
			if (player.ownedProjectileCounts[ModContent.ProjectileType<QingqueProj>()] < 4) {
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y + 30, 0, 0, ModContent.ProjectileType<QingqueProj>(), 0, 0, player.whoAmI, TileNum, 0f, 0f);
				TileNum++;
			}
			if (TileNum == 4)
				TileNum = 0;
			base.HoldItem(player);
		}
		
		//public override void UpdateInventory (Player player) { }
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}
		
		public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2){
				// REQUIRE 20 MANA TO ROLL!!!
				if (player.statMana < 20) 
					return false;
				
				GetMostCommonTile(player);
				// cant use skill when fully ready
				if (IsEnhancedReady(player, ChosenTile)){
					return false;
				}
				
				// time to gamba heehee (i took great liberties in the rng emulation that qingque actually uses
				int i = 2;
				int n = 0;
				foreach (int tile in player.GetModPlayer<BooTaoPlayer>().QingqueTiles) {
					if (tile != ChosenTile) {
						i--;
						player.GetModPlayer<BooTaoPlayer>().QingqueTiles[n] = Main.rand.Next(4);
					}
					n++;
					if (i == 0)
						break;
				}
				
				if (Main.rand.Next(4) == 0)
					player.GetModPlayer<BooTaoPlayer>().QingqueFUA = true;
				QingqueStacks++;
			}
			return true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			if (player.altFunctionUse == 2)
				return false;
			GetMostCommonTile(player);
			if (IsEnhancedReady(player, ChosenTile)){
				int newdmg = (int)(Item.damage * 2.12) + (int)(QingqueStacks * Item.damage * 0.28);
				var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, newdmg, 20f, Main.myPlayer, 5f, 2f, ChosenTile);
				//projectile.originalDamage = (int)(Item.damage * 2.12) + (int)(QingqueStacks * Item.damage * 0.28);
				QingqueStacks = 0;
				for (int neef = 0; neef < 3; neef++) {
					player.GetModPlayer<BooTaoPlayer>().QingqueTiles[neef] = Main.rand.Next(4);
				}
			}
			else {
				var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer, 5f, 1f, Main.rand.Next(4));
				projectile.originalDamage = Item.damage;
				if(player.statMana < player.statManaMax2 - 10)
					player.statMana += 10;
			}
			return false;
		}
		
		//helpers YAY
		private bool IsEnhancedReady(Player player, int num){
			for (int qq = 0; qq < 4; qq++){
				if (player.GetModPlayer<BooTaoPlayer>().QingqueTiles[qq] != num) {
					return false;
				}
			}
			return true;
		}
		
		private void GetMostCommonTile(Player player){
			var dictionary = new Dictionary<int, int>();
			foreach (int tile in player.GetModPlayer<BooTaoPlayer>().QingqueTiles){
				if (dictionary.ContainsKey(tile)) {
					dictionary[tile]++;
					if (dictionary[tile] == 2) {
						ChosenTile = tile;
						return;
					}
				}
				else
					dictionary.Add(tile, 1);
			}
			ChosenTile = dictionary.First().Value;
		}
	}
}
/*
https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iteration-statements
https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/arrays
https://stackoverflow.com/questions/15862191/counting-the-number-of-times-a-value-appears-in-an-array
https://x.com/honkaistarrail/status/1618851422811856898
https://docs.tmodloader.net/docs/stable/class_mod_projectile.html
https://docs.tmodloader.net/docs/stable/class_projectile.html
https://docs.tmodloader.net/docs/stable/class_player.html
https://github.com/tModLoader/tModLoader/blob/1.4.4/ExampleMod/Content/Pets/ExampleLightPet/ExampleLightPetProjectile.cs
https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=net-9.0
https://sr.yatta.moe/en/archive/avatar/1201/qingque?mode=skill
https://honkai-star-rail.fandom.com/wiki/Qingque/Voice-Overs

*/