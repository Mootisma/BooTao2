using BooTao2.Content.Projectiles.Ningguang;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace BooTao2.Content.Items.Ningguang
{
	public class NingItem : ModItem
	{
		bool CalamityActive = ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);
		
		public override void SetDefaults() {
			// DefaultToStaff handles setting various Item values that magic staff weapons use.
			// DefaultToMagicWeapon(projType, singleShotTime, pushForwardSpeed, hasAutoReuse: true); mana = manaPerShot; width = 40; height = 40; UseSound = SoundID.Item43;
			// Item.DefaultToStaff(ModContent.ProjectileType<NingGeoProj>(), 7, 20, 11);
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useAnimation = 30;
			Item.useTime = 15;
			Item.autoReuse = true;
			//
			Item.shootSpeed = 7f;
			Item.shoot = ModContent.ProjectileType<NingGeoProj>();
			//
			Item.width = 50;
			Item.height = 50;
			Item.UseSound = SoundID.Item20;

			// Weapon Properties
			if (CalamityActive)
				Item.damage = 80;
			else
				Item.damage = 40;
			Item.DamageType = DamageClass.Summon;
			Item.mana = 20;
			Item.crit = 5;
			Item.knockBack = 5.5f;

			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.sellPrice(silver: 30);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod) && calamityMod.TryFind("EssenceofHavoc", out ModItem essenceofHavoc) ) {
				recipe.AddIngredient(essenceofHavoc.Type);
			}
			recipe.AddIngredient(ItemID.SoulofLight, 1);
			recipe.AddIngredient(ItemID.SoulofNight, 1);
			recipe.AddIngredient(ItemID.CrystalShard, 1);
			recipe.AddIngredient(ItemID.StoneBlock, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		public override void HoldItem(Player player) {
			player.GetModPlayer<BooTaoPlayer>().NingHolding = true;
			if (player.ownedProjectileCounts[ModContent.ProjectileType<NingHoldProj>()] < 1) {
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ModContent.ProjectileType<NingHoldProj>(), 0, 4, player.whoAmI, 0f);
			}
			base.HoldItem(player);
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}
		
		bool Jade1Flag = false;
		bool Jade2Flag = false;
		bool Jade3Flag = false;
		public override bool CanUseItem(Player player) {
			if (CalamityActive)
				Item.damage = 30;
			else
				Item.damage = 15;
			if (player.altFunctionUse == 2){
				if (Jade1Flag) {
					player.GetModPlayer<BooTaoPlayer>().NingJade1State = true;
					Jade1Flag = false;
					Item.damage += 15;
				}
				if (Jade2Flag) {
					player.GetModPlayer<BooTaoPlayer>().NingJade2State = true;
					Jade2Flag = false;
					Item.damage += 15;
				}
				if (Jade3Flag) {
					player.GetModPlayer<BooTaoPlayer>().NingJade3State = true;
					Jade3Flag = false;
					Item.damage += 15;
				}
				Item.shoot = ModContent.ProjectileType<NingStrongGeoProj>();
				Item.mana = (player.GetModPlayer<BooTaoPlayer>().NingNumBuff > 0) ? 0 : 40;
				player.GetModPlayer<BooTaoPlayer>().NingNumBuff = -1;
				Item.useTime = 30;
			}
			else {
				Item.mana = 20;
				Item.shoot = ModContent.ProjectileType<NingGeoProj>();
				Item.useTime = 15;
			}
			return true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			if (player.GetModPlayer<BooTaoPlayer>().NingNumBuff == 1) {
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ModContent.ProjectileType<NingJade1>(), Item.damage / 2, 4, player.whoAmI, 0f);
				Jade1Flag = true;
			}
			else if (player.GetModPlayer<BooTaoPlayer>().NingNumBuff == 3) {
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ModContent.ProjectileType<NingJade2>(), Item.damage / 2, 4, player.whoAmI, 0f);
				Jade2Flag = true;
			}
			else if (player.GetModPlayer<BooTaoPlayer>().NingNumBuff == 5) {
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ModContent.ProjectileType<NingJade3>(), Item.damage / 2, 4, player.whoAmI, 0f);
				Jade3Flag = true;
			}
			if (player.GetModPlayer<BooTaoPlayer>().NingNumBuff < 6)
				player.GetModPlayer<BooTaoPlayer>().NingNumBuff++;
			return true;
		}

		/* public override void ModifyManaCost(Player player, ref float reduce, ref float mult) {
			// We can use ModifyManaCost to dynamically adjust the mana cost of this item, similar to how Space Gun works with the Meteor armor set.
			// See ExampleHood to see how accessories give the reduce mana cost effect.
			if (player.statLife < player.statLifeMax2 / 2) {
				mult *= 0f; // Make sure to use multiplication with the mult parameter.
			}
		}*/
	}
}
/*
https://ambr.top/en/archive/avatar/10000027/ningguang?mode=talent
https://docs.tmodloader.net/docs/1.4-stable/class_terraria_1_1_item.html
https://docs.tmodloader.net/docs/1.4-stable/class_terraria_1_1_mod_loader_1_1_mod_item.html
https://docs.tmodloader.net/docs/stable/class_mod_projectile.html
https://docs.tmodloader.net/docs/stable/class_projectile.html
https://github.com/ThePaperLuigi/The-Stars-Above/blob/main/Projectiles/Magic/IrminsulDream/IrminsulHeld.cs

*/