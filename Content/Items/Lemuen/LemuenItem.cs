using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using BooTao2.Content.Projectiles.Lemuen;
using BooTao2.Content.Projectiles;
using BooTao2.Systems;

namespace BooTao2.Content.Items.Lemuen
{
	public class LemuenItem : ModItem
	{
		private bool CalamityActive = ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);
		private int timer = 0;
		private float LemuenBombOrder = 0f;
		// im learning what good coding practices are
		private const int SPCOST = 38;
		SoundStyle Skill = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Lemuen/LemuenSkillStart") {
			Volume = 0.8f,
			PitchVariance = 0.0f,
			MaxInstances = 6,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		public override void SetDefaults() {
			Item.damage = (CalamityActive) ? 1000 : 600;
			Item.crit = 16;
			Item.DamageType = DamageClass.Ranged;
			Item.shoot = ModContent.ProjectileType<LemuenProj>();
			Item.shootSpeed = 60f;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 120;
			Item.useAnimation = 120;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 5;
			//
			Item.value = Item.sellPrice(gold: 40);
			Item.rare = 5;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.noUseGraphic = true;
			Item.noMelee = true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod) && calamityMod.TryFind("UelibloomBar", out ModItem uelibloom) ) {
				recipe.AddIngredient(uelibloom.Type);
			}
			recipe.AddIngredient(ItemID.LunarBar, 1);
			recipe.AddIngredient(ItemID.SniperRifle, 1);
			recipe.AddIngredient(ItemID.Minecart, 1);
			recipe.AddIngredient(ItemID.AngelHalo, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}
		/*
		* Right click to activate skill
		* Left click every 0.5s to mark the cursor's location and consume 1 ammo.
		* Upon using all ammo, bombards the marked locations, dealing 300-450% ATK.
		*/
		public override void UpdateInventory(Player player) {
			timer++;
			if( timer >= 60 ) {
				if ((player.GetModPlayer<BooTaoPlayer>().LemuenSP < SPCOST) && (player.GetModPlayer<BooTaoPlayer>().LemuenAmmo == 0) ){
					player.GetModPlayer<BooTaoPlayer>().LemuenSP += 1;
				}
				timer = 0;
			}
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			// left click while skill is active
			if (player.GetModPlayer<BooTaoPlayer>().LemuenAmmo > 0){
				Projectile.NewProjectile(source, Main.MouseWorld + new Vector2(-30, -30), Vector2.Zero, ModContent.ProjectileType<LemuenTargetProj>(), damage * 3, knockback * 2, player.whoAmI, LemuenBombOrder);
				player.GetModPlayer<BooTaoPlayer>().LemuenAmmo -= 1;
				player.GetModPlayer<BooTaoPlayer>().LemuenSP = 0;
				LemuenBombOrder += 20f;
				timer = 0;
				return false;
			}
			// left click without skill
			return true;
		}
		
		public override bool CanUseItem(Player player) {
			// right click to activate skill
			if (player.altFunctionUse == 2) {
				if (player.GetModPlayer<BooTaoPlayer>().LemuenSP >= SPCOST){
					player.GetModPlayer<BooTaoPlayer>().LemuenAmmo = 7;
					player.GetModPlayer<BooTaoPlayer>().LemuenSP = 0;
					LemuenBombOrder = 0f;
					SoundEngine.PlaySound(Skill, player.Center);
					Item.useTime = 30;
					Item.useAnimation = 30;
				}
				return false;
			}
			if (player.GetModPlayer<BooTaoPlayer>().LemuenAmmo == 0){
				Item.useTime = 120;
				Item.useAnimation = 120;
			}
			return true;
		}
		
		public override void HoldItem(Player player) {
			if (player.GetModPlayer<BooTaoPlayer>().LemuenSP >= SPCOST) {
				player.GetModPlayer<BooTaoPlayer>().SkillReady = true;
				if (player.ownedProjectileCounts[ModContent.ProjectileType<SkillReady>()] < 1) {
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ModContent.ProjectileType<SkillReady>(), 0, 4, player.whoAmI, 0f);
				}
			}
		}
		
		//public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
		//	position = Main.MouseWorld;
		//}
	}
}
/*
https://puppiizsunniiz.github.io/AN-EN-Tags/akhrchars.html?opname=Lemuen
https://terraria.wiki.gg/wiki/Bullets
https://terraria.wiki.gg/wiki/Item_IDs
https://terraria.wiki.gg/wiki/Projectile_IDs
https://docs.tmodloader.net/docs/stable/class_mod_player.html
https://docs.tmodloader.net/docs/stable/class_player.html
*/