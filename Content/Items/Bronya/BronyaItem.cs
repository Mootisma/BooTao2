using BooTao2.Content.Buffs.Bronya;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace BooTao2.Content.Items.Bronya
{
	public class BronyaItem : ModItem
	{
		Vector2 TPposition = new Vector2(33450, 4923);
		
		public override void SetDefaults() {
			Item.damage = 20;
			Item.knockBack = 1.5f;
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod)) {
				Item.damage = 25;
				Item.knockBack = 2f;
			}
			Item.crit = 6;
			Item.mana = 120;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noUseGraphic = true;
			Item.value = Item.sellPrice(0, 4, 20, 69);
			Item.rare = ItemRarityID.Green;
			//Item.UseSound = SoundID.Item11;

			Item.autoReuse = true;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Ranged;
			Item.shoot = ProjectileID.BulletHighVelocity;
			Item.shootSpeed = 15f;
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			if (player.altFunctionUse == 2) {
				player.AddBuff(ModContent.BuffType<BuffTeleportCooldown>(), 480, true);
				player.AddBuff(ModContent.BuffType<BronyaDmgBuff>(), 240, true);
				return false;
				//SoundEngine.PlaySound(KafkaFUA, player.Center);
			}
			else {
				SoundEngine.PlaySound(SoundID.Item11, player.Center);
				player.AddBuff(ModContent.BuffType<BronyaBuff>(), 240, true);
			}
			return true;
		}

		public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2 && Main.myPlayer == player.whoAmI) {
				if (player.GetModPlayer<BooTaoPlayer>().TeleportCooldown > 0 || player.statMana < Item.mana) {
					return false;
				}
				player.GetModPlayer<BooTaoPlayer>().TeleportCooldown = 480;
				TPposition = Main.MouseWorld;
				NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, (float)player.whoAmI, TPposition.X, TPposition.Y, 1, 0, 0);
				player.Teleport(TPposition, 1, 0);
			}
			return true;
		}
		
		public override void AddRecipes() {
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.MeteoriteBar, 1);
			recipe.AddIngredient(ItemID.GreenBanner, 1);
			recipe.AddIngredient(ItemID.Musket, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		public override void UpdateInventory (Player player) {
			if (player.GetModPlayer<BooTaoPlayer>().TeleportCooldown > 0) {
				player.GetModPlayer<BooTaoPlayer>().TeleportCooldown--;
			}
		}
		
		public override void ModifyManaCost(Player player, ref float reduce, ref float mult) {
			if (player.altFunctionUse != 2) {
				mult *= 0f; // Make sure to use multiplication with the mult parameter.
			}
		}
	}
}
/*
https://docs.tmodloader.net/docs/stable/class_n_p_c.html
https://docs.tmodloader.net/docs/stable/class_projectile.html
https://docs.tmodloader.net/docs/stable/class_projectile.html#a76031be1e4228ce7e8a3ec67e2c293ad
https://docs.tmodloader.net/docs/stable/class_mod_projectile.html
https://github.com/ThePaperLuigi/The-Stars-Above/blob/main/Items/Weapons/Summon/PhantomInTheMirror.cs

*/