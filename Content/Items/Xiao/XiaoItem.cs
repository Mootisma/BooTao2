using BooTao2.Content.Projectiles.HuTao;
using BooTao2.Content.Items.HuTao;
using BooTao2.Content.Buffs.Xiao;
using BooTao2.Content.Projectiles.Xiao;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace BooTao2.Content.Items.Xiao
{ 
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class XiaoItem : ModItem
	{
		SoundStyle XiaoSkill = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Xiao/XiaoSkill") {
			Volume = 0.9f,
			PitchVariance = 0.1f,
			MaxInstances = 3,
		};//SoundEngine.PlaySound(XiaoSkill, player.Center);
		
		SoundStyle XiaoBORING = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Xiao/XiaoBORING") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		SoundStyle XiaoUSELESS = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Xiao/XiaoUSELESS") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		SoundStyle XiaoPlungeStart = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Xiao/XiaoPlungeStart") {
			Volume = 0.2f,
			PitchVariance = 0.1f,
			MaxInstances = 3,
		};
		
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.BooTao2.hjson' file.
		public override void SetStaticDefaults() {
			ItemID.Sets.SkipsInitialUseSound[Item.type] = true; // This skips use animation-tied sound playback, so that we're able to make it be tied to use time instead in the UseItem() hook.
			ItemID.Sets.Spears[Item.type] = true; // This allows the game to recognize our new item as a spear.
		}

		public override void SetDefaults() {
			// Common Properties
			Item.rare = ItemRarityID.Lime;
			Item.value = Item.sellPrice(gold: 12);

			// Use Properties
			Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
			Item.useAnimation = 50; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.useTime = 50; // The length of the item's use time in ticks (60 ticks == 1 second.)
			// Item.UseSound = SoundID.Item71; // The sound that this item plays when used.
			Item.autoReuse = true; // Allows the player to hold click to automatically use the item again. Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()
			
			// Weapon Properties
			Item.damage = 70;
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod)) {
				Item.damage = 130;
			}
			Item.crit = 6;
			Item.knockBack = 9f;
			Item.noUseGraphic = true; // When true, the item's sprite will not be visible while the item is in use. This is true because the spear projectile is what's shown so we do not want to show the spear sprite as well.
			Item.DamageType = DamageClass.Melee;
			Item.noMelee = true; // Allows the item's animation to do damage. This is important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.

			// Projectile Properties
			Item.shootSpeed = 1f; // The speed of the projectile measured in pixels per frame.
			Item.shoot = ModContent.ProjectileType<XiaoProj>(); // The projectile that is fired from this weapon
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}

		public override bool CanUseItem(Player player) {
			bool LeftClickActive = player.ownedProjectileCounts[Item.shoot] < 1;
			bool RightClickActive = player.ownedProjectileCounts[ModContent.ProjectileType<XiaoSkillProj>()] < 1;
			bool HasECharge = player.GetModPlayer<BooTaoPlayer>().XiaoSP > 600;
			if (player.altFunctionUse == 2){
				if (HasECharge && LeftClickActive && RightClickActive) {
					player.GetModPlayer<BooTaoPlayer>().XiaoSP -= 600;
					SoundEngine.PlaySound(XiaoSkill, player.Center);
					if (Main.rand.Next(2) == 1) {
						SoundEngine.PlaySound(XiaoBORING, player.Center);
					}
					else {
						SoundEngine.PlaySound(XiaoUSELESS, player.Center);
					}
					return true;
				}
				return false;
			}
			return LeftClickActive && RightClickActive && Math.Abs(player.velocity.Y) > 0.1f;
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			if (player.altFunctionUse != 2){
				SoundEngine.PlaySound(XiaoPlungeStart, player.Center);
				velocity = new Vector2(0, 40);
			}
			else {
				type = ModContent.ProjectileType<XiaoSkillProj>();
			}
		}
		
		int counter = 0;
		public override void HoldItem(Player player)
		{
			player.GetModPlayer<ExampleDashPlayer>().XiaoPlungeEquipped = false;
			player.GetModPlayer<ExampleDashPlayer>().DashAccessoryEquipped = false;
			player.GetModPlayer<ExampleDashPlayer>().XiaoSkillDash = false;
			player.AddBuff(ModContent.BuffType<XiaoBuff>(), 10, true);
			counter++;
			if (player.statLife > 2 && counter >= 24) {
				player.statLife -= (int)(player.statLife * 0.009) + 1;
				counter = 0;
			}
		}
		
		public override void UpdateInventory (Player player) {
			if (player.GetModPlayer<BooTaoPlayer>().XiaoSP < 1200)
				player.GetModPlayer<BooTaoPlayer>().XiaoSP++;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			if (player.altFunctionUse != 2){
				player.GetModPlayer<ExampleDashPlayer>().XiaoPlungeEquipped = true;
			}
			else {
				player.GetModPlayer<ExampleDashPlayer>().XiaoSkillDash = true;
				player.GetModPlayer<ExampleDashPlayer>().DashAccessoryEquipped = true;
			}
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod) && calamityMod.TryFind("PurifiedGel", out ModItem purifiedGel) ) {
				recipe.AddIngredient(purifiedGel.Type);
			}
			recipe.AddIngredient(ItemID.Bone, 4);
			recipe.AddIngredient(ItemID.HallowedBar, 3);
			recipe.AddIngredient(ItemID.CursedFlame, 3);
			recipe.AddIngredient(ItemID.SoulofNight, 3);
			recipe.AddIngredient(ItemID.CrystalShard, 3);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
/*
https://github.com/tModLoader/tModLoader/wiki/Geometry
https://github.com/tModLoader/tModLoader/wiki/Coordinates
https://github.com/tModLoader/tModLoader/wiki/Basic-Sounds
https://github.com/tModLoader/tModLoader/wiki/Useful-Vanilla-Fields#player-fields
https://github.com/tModLoader/tModLoader/blob/1.4.4/ExampleMod/Content/Items/Weapons/ExampleSpear.cs
https://docs.tmodloader.net/docs/stable/class_player.html
https://ambr.top/en/archive/avatar/10000046/hu-tao?mode=talent
rgb color code

*/