using BooTao2.Content.Projectiles;
using BooTao2.Content.Projectiles.Fiammetta;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace BooTao2.Content.Items.Fiammetta
{
	public class FiammettaItem : ModItem
	{
		bool CalamityActive = ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);
		
		SoundStyle Skill = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Thorns/AtkBoost") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		SoundStyle BasicAttack = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Fiammetta/BasicAttack") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		SoundStyle S3Attack = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Fiammetta/S3Attack") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		SoundStyle phoenix = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Fiammetta/phoenix") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		SoundStyle SkillActivate = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Fiammetta/SkillActivate") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		SoundStyle SightsLocked = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Fiammetta/Select1") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 1,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		SoundStyle Boring = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Fiammetta/Select2") {
			Volume = 1f,
			PitchVariance = 0f,
			MaxInstances = 1,
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};
		
		public override void SetDefaults() {
			Item.damage = (CalamityActive) ? 225 : 150;
			Item.DamageType = DamageClass.Ranged;
			Item.crit = 16;
			Item.shoot = ModContent.ProjectileType<FiammettaProj>();
			Item.shootSpeed = 20f;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 60;
			Item.useAnimation = 60;
			Item.knockBack = 3;
			//
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.value = Item.sellPrice(gold: 40);
			Item.rare = ItemRarityID.Red;
			//Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LivingFireBlock, 1);
			recipe.AddIngredient(ItemID.AngelHalo, 1);
			recipe.AddIngredient(ItemID.SoulofFright, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			// dont shoot anything with the right click
			if (player.altFunctionUse == 2) {
				return false;
			}
			return true;
		}
		
		public override void ModifyShootStats (Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			if (player.statLife >= (player.statLifeMax2 * 0.5) ) {
				damage = (int)(damage * 1.3);
			}
			if (player.statLife >= (player.statLifeMax2 * 0.8) ) {
				damage = (int)(damage * 1.3);
			}
			// store the mouse position for use in fia's projectile
			Vector2 aim = Main.MouseWorld;
			player.GetModPlayer<BooTaoPlayer>().FiammettaStoreMouse = aim;
			if (player.GetModPlayer<BooTaoPlayer>().FiammettaS3 == 1) {
				// during s3, shoot straight up
				velocity.X = 0f;
				velocity.Y = -40f;
				knockback += 5f;
				damage = (int)(damage * 1.25);
				//var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
				//projectile.originalDamage = Item.damage;
				//var projectile2 = Projectile.NewProjectileDirect(source, position, new Vector2(0, 0), ModContent.ProjectileType<HertaDiamondProj>(), (int)(damage / 10), knockback, Main.myPlayer);
				//Projectile.NewProjectile(source, aim, new Vector2(0, 0), ModContent.ProjectileType<HertaDiamondProj>(), 2, knockback, Main.myPlayer, 0, 1);
				//return false;
			}
			//return true;
		}
		
		// useless counter so i can play a sound when i hold fiammetta
		int ligma = 0;
		public override void UpdateInventory (Player player) {
			if (ligma > 0) {
				ligma--;
			}
		}
		
		public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2){
				if (player.GetModPlayer<BooTaoPlayer>().FiammettaSP >= 15) {
					player.GetModPlayer<BooTaoPlayer>().FiammettaSP = 0;
					player.GetModPlayer<BooTaoPlayer>().FiammettaS3 = 1;
					SoundEngine.PlaySound(Skill, player.Center);
					SoundEngine.PlaySound(SkillActivate, player.Center);
				}
				return false;
			}
			if (player.GetModPlayer<BooTaoPlayer>().FiammettaS3 == 1) {
				Item.useTime = 80;
				Item.useAnimation = 80;
				SoundEngine.PlaySound(S3Attack, player.Center);
				SoundEngine.PlaySound(phoenix, player.Center);
			}
			else {
				Item.useTime = 60;
				Item.useAnimation = 60;
				SoundEngine.PlaySound(BasicAttack, player.Center);
			}
			return true;
		}
		
		int counter = 0;
		public override void HoldItem(Player player) {
			if (ligma == 0) {
				if (Main.rand.Next(2) == 1) {
					SoundEngine.PlaySound(SightsLocked, player.Center);
				}
				else {
					SoundEngine.PlaySound(Boring, player.Center);
				}
			}
			ligma = 2;
			counter++;
			if (player.statLife > (player.statLifeMax2 / 2) && counter >= 12) {
				player.statLife -= (int)(player.statLife * 0.009) + 1;
				counter = 0;
			}
			// player.lifeRegen -= 16;
			
			if ((player.GetModPlayer<BooTaoPlayer>().FiammettaSP >= 15) && (player.GetModPlayer<BooTaoPlayer>().FiammettaS3 == 0)) {
				player.GetModPlayer<BooTaoPlayer>().SkillReady = true;
				if (player.ownedProjectileCounts[ModContent.ProjectileType<SkillReady>()] < 1) {
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ModContent.ProjectileType<SkillReady>(), 0, 4, player.whoAmI, 0f);
				}
			}
		}
	}
}
/*
https://docs.tmodloader.net/docs/1.4-preview/class_terraria_1_1_utilities_1_1_unified_random.html
*/