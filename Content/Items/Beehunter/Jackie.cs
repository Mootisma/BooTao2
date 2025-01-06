using BooTao2.Content.Projectiles.Beehunter;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace BooTao2.Content.Items.Beehunter
{
	public class Jackie : ModItem
	{
		public override void SetDefaults() {
			Item.damage = 15;
			Item.knockBack = 4f;
			Item.useStyle = ItemUseStyleID.Rapier; // Makes the player do the proper arm motion
			Item.useAnimation = 18;
			Item.useTime = 9;
			Item.width = 32;
			Item.height = 32;
			//Item.UseSound = SoundID.Item1;
			Item.DamageType = DamageClass.MeleeNoSpeed;
			Item.autoReuse = true;
			Item.noUseGraphic = true; // The sword is actually a "projectile", so the item should not be visible when used
			Item.noMelee = true; // The projectile will do the damage and not the item
			Item.reuseDelay = 4;
			Item.rare = ItemRarityID.White;
			Item.value = Item.sellPrice(0, 0, 0, 20);

			Item.shoot = ModContent.ProjectileType<BeehunterProj>(); // The projectile is what makes a shortsword work
			Item.shootSpeed = 3.1f; // This value bleeds into the behavior of the projectile as velocity, keep that in mind when tweaking values
		}
		
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			SoundEngine.PlaySound(SoundID.Item1, player.Center);
		}
		
		int talent = 0;
		public override void UpdateInventory (Player player) {
			if (player.GetModPlayer<BooTaoPlayer>().JackieDodged) {
				talent++;
			}
			if (talent > 300) {
				player.GetModPlayer<BooTaoPlayer>().JackieDodged = false;
				talent = 0;
			}
			if(player.GetModPlayer<BooTaoPlayer>().JackieHolding > 0)
				player.GetModPlayer<BooTaoPlayer>().JackieHolding--;
		}
		
		public override void HoldItem(Player player) {
			player.GetModPlayer<BooTaoPlayer>().JackieHolding = 2;
		}
		
		public override bool CanUseItem(Player player) {
			if (player.GetModPlayer<BooTaoPlayer>().JackieDodged) {
				Item.useAnimation = 16;
				Item.useTime = 8;
			}
			else {
				Item.useAnimation = 18;
				Item.useTime = 9;
			}
			return true;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.CrimtaneBar)
				.AddTile(TileID.WorkBenches)
				.Register();
			CreateRecipe()
				.AddIngredient(ItemID.DemoniteBar)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}
