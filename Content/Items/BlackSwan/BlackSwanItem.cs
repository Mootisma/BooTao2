using BooTao2.Content.Projectiles.BlackSwan;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace BooTao2.Content.Items.BlackSwan
{
	public class BlackSwanItem : ModItem
	{
		public override void SetDefaults() {
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.autoReuse = true;
			//
			Item.shootSpeed = 0f;
			Item.shoot = ModContent.ProjectileType<BlackSwanBasicProj>();
			//
			Item.width = 50;
			Item.height = 50;
			Item.UseSound = SoundID.Item67;

			// Weapon Properties
			Item.damage = 70;
			Item.knockBack = 2.5f;
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod)) {
				Item.damage = 110;
				Item.knockBack = 3.5f;
			}
			Item.DamageType = DamageClass.Magic;
			Item.mana = 20;

			Item.rare = ItemRarityID.Purple;
			Item.value = Item.sellPrice(gold: 20);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LunarBar, 1);
			recipe.AddIngredient(ItemID.CrystalShard, 12);
			recipe.AddIngredient(ItemID.CrystalBall, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}
		
		public override void HoldItem(Player player) {
			player.GetModPlayer<BooTaoPlayer>().BlackSwanHolding = true;
			if (player.ownedProjectileCounts[ModContent.ProjectileType<BlackSwanHold>()] < 1) {
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ModContent.ProjectileType<BlackSwanHold>(), 0, 4, player.whoAmI, 0f);
			}
			base.HoldItem(player);
		}
		
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			position = Main.MouseWorld;
			velocity = Vector2.Zero;
			if (player.altFunctionUse == 2) {
				position.Y -= 400;
				type = ModContent.ProjectileType<BlackSwanSkill>();
			}
		}
		
		public override void ModifyManaCost(Player player, ref float reduce, ref float mult) {
			if (player.altFunctionUse != 2) {
				mult *= 0.5f; // Make sure to use multiplication with the mult parameter.
			}
		}
	}
}
/*
https://honkai-star-rail.fandom.com/wiki/Black_Swan/Media?file=Weibo_Time_Limited_Expression_2024_Black_Swan.png
https://sr.yatta.moe/en/archive/avatar/1307/black-swan?mode=skill

https://docs.tmodloader.net/docs/stable/class_dust.html
https://github.com/tModLoader/tModLoader/wiki/IEntitySource

*/