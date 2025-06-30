using BooTao2.Content.Projectiles.Sparkle;
using BooTao2.Content.Buffs.Bronya;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace BooTao2.Content.Items.Sparkle
{
	public class SparkleItem : ModItem
	{
		public override void SetDefaults() {
			Item.damage = 65;
			Item.knockBack = 4f;
			Item.mana = 100;
			Item.useStyle = ItemUseStyleID.Rapier; // Makes the player do the proper arm motion
			Item.useAnimation = 9;
			Item.useTime = 9;
			Item.width = 32;
			Item.height = 32;
			Item.UseSound = SoundID.Item1;
			Item.DamageType = DamageClass.MeleeNoSpeed;
			Item.autoReuse = true;
			Item.noUseGraphic = true; // The sword is actually a "projectile", so the item should not be visible when used
			Item.noMelee = true; // The projectile will do the damage and not the item
			Item.reuseDelay = 1;
			Item.rare = ItemRarityID.Red;
			Item.value = Item.sellPrice(0, 9, 50, 50);

			Item.shoot = ModContent.ProjectileType<SparkleProj>(); // The projectile is what makes a shortsword work
			Item.shootSpeed = 3.1f; // This value bleeds into the behavior of the projectile as velocity, keep that in mind when tweaking values
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			if (player.altFunctionUse == 2 && player.GetModPlayer<BooTaoPlayer>().TeleportCooldown >= 294) {
				player.AddBuff(ModContent.BuffType<BuffTeleportCooldown>(), 300, true);
				player.AddBuff(ModContent.BuffType<BronyaDmgBuff>(), 300, true);
				
				return false;
			}
			if (player.altFunctionUse == 2 && player.GetModPlayer<BooTaoPlayer>().TeleportCooldown < 294) {
				Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
				Projectile.NewProjectile(source, target, Vector2.Zero, ModContent.ProjectileType<SparkleBomb>(), damage, knockback, player.whoAmI);
				return false;
			}
			return true;
		}

		public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2 && Main.myPlayer == player.whoAmI) {
				if (player.GetModPlayer<BooTaoPlayer>().TeleportCooldown > 0 || player.statMana < Item.mana) {
					//
					return true;
				}
				player.GetModPlayer<BooTaoPlayer>().TeleportCooldown = 300;//5 second cd
				Vector2 TPposition = Main.MouseWorld;
				NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, (float)player.whoAmI, TPposition.X, TPposition.Y, 1, 0, 0);
				player.Teleport(TPposition, 1, 0);
			}
			return true;
		}
		
		public override void ModifyManaCost(Player player, ref float reduce, ref float mult) {
			if (player.altFunctionUse != 2) {
				mult *= 0f; // Make sure to use multiplication with the mult parameter.
			}
			if (player.altFunctionUse == 2 && player.GetModPlayer<BooTaoPlayer>().TeleportCooldown < 294) {
				mult *= 0.2f; // Make sure to use multiplication with the mult parameter.
			}
		}
		
		public override void AddRecipes() {
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HallowedBar, 1);
			recipe.AddIngredient(ItemID.GuideVoodooDoll, 1);
			recipe.AddIngredient(ItemID.FireworksBox, 1);
			recipe.AddIngredient(ItemID.Bass, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		public override void UpdateInventory (Player player) {
			if (player.GetModPlayer<BooTaoPlayer>().TeleportCooldown > 0) {
				player.GetModPlayer<BooTaoPlayer>().TeleportCooldown--;
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
https://docs.tmodloader.net/docs/stable/class_utils.html#a0f5347faf5b45c4175e236b265168370

*/