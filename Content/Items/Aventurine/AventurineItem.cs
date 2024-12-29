using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using BooTao2.Content.Projectiles.Aventurine;
using BooTao2.Content.Buffs.Aventurine;

namespace BooTao2.Content.Items.Aventurine
{
	public class AventurineItem : ModItem
	{
		SoundStyle AventurineSkill = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Aventurine/AventurineSkill") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		SoundStyle AventurineTalent = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Aventurine/AventurineSkill") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		SoundStyle FingerSnap = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Aventurine/FingerSnap") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		public override void SetDefaults() {
			Item.damage = 0;
			Item.crit = 0;
			//Item.DamageType = DamageClass.Melee;
			Item.shoot = ModContent.ProjectileType<AventurineProj>();
			Item.shootSpeed = 1f;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 80;
			Item.useAnimation = 80;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.knockBack = 1;
			//
			Item.value = Item.sellPrice(gold: 30);
			Item.rare = 8;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.noUseGraphic = true;
			Item.noMelee = true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod) && calamityMod.TryFind("OldDie", out ModItem OldDie) ) {
				recipe.AddIngredient(OldDie.Type);
			}
			recipe.AddIngredient(ItemID.PaladinsShield, 1);
			recipe.AddIngredient(ItemID.CoinGun, 1);
			recipe.AddIngredient(ItemID.PlatinumCoin, 1);
			recipe.AddIngredient(ItemID.GoldBar, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}
		
		int cooldown = 0;
		int seconds = 0;
		public override void UpdateInventory(Player player) {
			seconds++;
			if (seconds >= 60) {
				seconds = 0;
				if (cooldown > 0) {
					cooldown--;
				}
			}
			if (player.GetModPlayer<BooTaoPlayer>().AventurineBlindBet >= 7) {
				player.GetModPlayer<BooTaoPlayer>().AventurineBlindBet -= 7;
				//player.GetSource_Accessory(itemInstance) player.GetSource_ItemUse(Item)
				SoundEngine.PlaySound(AventurineTalent, player.Center);
				Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
				target.Y = player.Center.Y - 900f;
				Vector2 heading = new Vector2(0, 30f);
				for (int i = 0; i < 7; i++) {
					target.X += Main.rand.NextFloat(30) * player.direction * i;
					Projectile.NewProjectile(player.GetSource_FromThis(), target, heading, ModContent.ProjectileType<AventurineProj>(), (int)(Item.damage / 4), 3, player.whoAmI);
				}
				foreach (var ligma in Main.ActivePlayers) {
					if (ligma.team != player.team) {
						continue;
					}
					ligma.ClearBuff(ModContent.BuffType<AventurineShieldOrigin>());
					ligma.AddBuff(ModContent.BuffType<AventurineShield>(), 59);
					ligma.GetModPlayer<BooTaoPlayer>().AventurineShieldHP += (int)(Item.damage / 3);
					if (ligma.GetModPlayer<BooTaoPlayer>().AventurineShieldHP >= Item.damage * 2) {
						ligma.GetModPlayer<BooTaoPlayer>().AventurineShieldHP = Item.damage * 2;
					}
				}
				player.AddBuff(ModContent.BuffType<AventurineShieldOrigin>(), 59);
			}
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			// dont shoot anything with the right click
			if (player.altFunctionUse == 2) {
				return false;
			}
			
			Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
			position.Y = player.Center.Y - 900f;
			Vector2 heading = new Vector2(0, 30f);
			// Loop these functions 3 times.
			for (int i = 0; i < 3; i++) {
				position.X = target.X;
				position.X += Main.rand.NextFloat(30) * player.direction * i;

				Projectile.NewProjectile(source, position, heading, type, damage, knockback, player.whoAmI, 0f, 1);
			}
			return false;
		}
		
		public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2){
				if (cooldown > 0) { return false; }
				foreach (var ligma in Main.ActivePlayers) {
					if (ligma.team != player.team) {
						continue;
					}
					ligma.ClearBuff(ModContent.BuffType<AventurineShieldOrigin>());
					ligma.AddBuff(ModContent.BuffType<AventurineShield>(), 59);
					ligma.GetModPlayer<BooTaoPlayer>().AventurineShieldHP += player.statDefense;
					if (ligma.GetModPlayer<BooTaoPlayer>().AventurineShieldHP >= player.statDefense * 2) {
						ligma.GetModPlayer<BooTaoPlayer>().AventurineShieldHP = player.statDefense * 2;
					}
				}
				player.AddBuff(ModContent.BuffType<AventurineShieldOrigin>(), 59);
				cooldown = 120;
				SoundEngine.PlaySound(AventurineSkill, player.Center);
				return false;
			}
			Item.damage = player.statDefense;
			Item.crit = (int)(player.statDefense / 2);
			SoundEngine.PlaySound(FingerSnap, player.Center);
			return true;
		}
	}
}
/*
https://github.com/tModLoader/tModLoader/blob/1.4.4/ExampleMod/Common/Players/ExampleDamageModificationPlayer.cs
https://github.com/tModLoader/tModLoader/blob/1.4.4/ExampleMod/Content/Items/Accessories/AbsorbTeamDamageAccessory.cs
https://docs.tmodloader.net/docs/stable/struct_player_1_1_hurt_info.html
https://docs.tmodloader.net/docs/stable/struct_player_1_1_hurt_modifiers.html
https://docs.tmodloader.net/docs/stable/class_mod_player.html#a4afd4db6bd12cec91f783a4bf0fd10bf
https://sr.yatta.moe/en/archive/avatar/1304/aventurine?mode=skill
https://docs.tmodloader.net/docs/stable/class_mod_player.html
https://docs.tmodloader.net/docs/stable/class_player.html
https://github.com/tModLoader/tModLoader/wiki/IEntitySource
*/