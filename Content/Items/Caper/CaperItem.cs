using BooTao2.Content.Projectiles;
using BooTao2.Content.Projectiles.Caper;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace BooTao2.Content.Items.Caper
{ 
	public class CaperItem : ModItem
	{
		SoundStyle Skill = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Thorns/AtkBoost") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		private int timer_hi = 0;
		
		public override void SetDefaults() {
			// Common Properties
			Item.rare = ItemRarityID.Red;
			Item.value = Item.sellPrice(silver: 30);
			
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.noUseGraphic = true;
			
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 31;
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod)) {
				Item.damage = 36;
			}
			Item.crit = 1;
			Item.knockBack = 3.5f;
			Item.noMelee = true;
			
			Item.shoot = ModContent.ProjectileType<CaperProj>();
			Item.shootSpeed = 22f;
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}
		
		public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2){
				if (player.GetModPlayer<BooTaoPlayer>().CaperSP >= 25) {
					player.GetModPlayer<BooTaoPlayer>().CaperSP = 0;
					timer_hi = 1200;
					SoundEngine.PlaySound(Skill, player.Center);
				}
				return false;
			}
			return player.ownedProjectileCounts[Item.shoot] < 1;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			if (player.altFunctionUse == 2)
				return false;
			Vector2 aim = Main.MouseWorld;
			if (timer_hi > 0) {
				//for (int i = 0; i < 2; i++) {
				//	position -= Vector2.Normalize(velocity) * 35f * i;
				//	Projectile.NewProjectile(source, position, velocity, type, (int)(damage * 1.6), knockback, player.whoAmI, 5f);
				//}
				Projectile.NewProjectile(source, position, velocity, type, (int)(damage * 1.6), knockback, player.whoAmI, 3f, aim.X, aim.Y);
				Projectile.NewProjectile(source, position - Vector2.Normalize(velocity) * 35f, velocity, type, (int)(damage * 1.6), knockback, player.whoAmI, 3f, aim.X, aim.Y);
				return false;
			}
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, aim.X, aim.Y);
			return false;
		}
		
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			if (Main.rand.NextBool(4)) {// 1 in 4
				damage = (int)(damage * 1.6);
			}
		}
		
		public override void UpdateInventory (Player player) {
			if (timer_hi > 0) {
				timer_hi--;
			}
		}
		
		public override void HoldItem(Player player) {
			if (player.GetModPlayer<BooTaoPlayer>().CaperSP >= 25) {
				player.GetModPlayer<BooTaoPlayer>().SkillReady = true;
				if (player.ownedProjectileCounts[ModContent.ProjectileType<SkillReady>()] < 1) {
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ModContent.ProjectileType<SkillReady>(), 0, 4, player.whoAmI, 0f);
				}
			}
		}
		
		public override void AddRecipes() {
			//if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod)
			CreateRecipe()
				.AddIngredient(ItemID.ShadowScale, 8)
				.AddIngredient(ItemID.Bone, 15)
				.AddIngredient(ItemID.BeeWax, 1)
				.AddIngredient(ItemID.FlinxFur, 5)
				.AddTile(TileID.WorkBenches)
				.Register();
			CreateRecipe()
				.AddIngredient(ItemID.TissueSample, 8)
				.AddIngredient(ItemID.Bone, 15)
				.AddIngredient(ItemID.BeeWax, 1)
				.AddIngredient(ItemID.FlinxFur, 5)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}