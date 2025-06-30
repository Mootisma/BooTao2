using BooTao2.Content.Projectiles.Ningguang;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace BooTao2.Content.Items.Ningguang
{
	public class SWNingItem : ModItem
	{
		public override string Texture => "BooTao2/Content/Items/Ningguang/NingItem";
		
		private bool CalamityActive = ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);
		public int JadeScreenCD = 0;
		
		public override void SetDefaults() {
			// DefaultToStaff handles setting various Item values that magic staff weapons use.
			// DefaultToMagicWeapon(projType, singleShotTime, pushForwardSpeed, hasAutoReuse: true); mana = manaPerShot; width = 40; height = 40; UseSound = SoundID.Item43;
			// Item.DefaultToStaff(ModContent.ProjectileType<NingGeoProj>(), 7, 20, 11);
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useAnimation = 13;
			Item.useTime = 13;
			Item.autoReuse = true;
			//
			Item.shootSpeed = 16f;
			Item.shoot = ModContent.ProjectileType<NingStrongGeoProj>();
			//
			Item.width = 50;
			Item.height = 50;
			Item.UseSound = SoundID.Item20;

			// Weapon Properties
			Item.damage = (CalamityActive) ? 360 : 120;
			Item.DamageType = DamageClass.Magic;
			//Item.mana = 0;
			Item.crit = 36;
			Item.knockBack = 5.5f;

			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.sellPrice(platinum: 30);
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient<Items.Ningguang.NingItem>()
				.AddIngredient<Items.SilverWolfMaterial>()
				.AddTile(TileID.WorkBenches)
				.Register();
		}
		
		public override void HoldItem(Player player) {
			player.GetModPlayer<BooTaoPlayer>().NingHolding = true;
			if (player.ownedProjectileCounts[ModContent.ProjectileType<NingHoldProj>()] < 1) {
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ModContent.ProjectileType<NingHoldProj>(), 0, 4, player.whoAmI, 0f);
			}
			if (JadeScreenCD > 0)
				JadeScreenCD--;
			base.HoldItem(player);
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}
		
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			if (player.altFunctionUse == 2) {
				position = Main.MouseWorld;
				velocity = Vector2.Zero;
			}
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			if (player.altFunctionUse == 2 && JadeScreenCD <= 0){
				foreach (var proj in Main.ActiveProjectiles)
				{
					if (proj.active && proj.type == ModContent.ProjectileType<NingJadeScreen>() && proj.owner == player.whoAmI)
					{
						proj.active = false;
					}
				}
				JadeScreenCD = 300;
				Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<NingJadeScreen>(), damage, knockback, player.whoAmI, 0f);
				return false;
			}
			float numProjs = 7;
			float rotation = MathHelper.ToRadians(7);
			position += Vector2.Normalize(velocity) * 10f;
			for (int i = 0; i < numProjs; i++) {
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numProjs - 1)));
				Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<NingJade1>(), damage, knockback, player.whoAmI, 0f, 50f);
			}
			Projectile.NewProjectile(source, position, velocity, type, (int)(damage * 3.5), knockback, player.whoAmI, 0f, 50f);
			return false;
		}
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