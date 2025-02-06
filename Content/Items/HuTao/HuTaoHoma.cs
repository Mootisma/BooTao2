using BooTao2.Content.Projectiles.HuTao;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace BooTao2.Content.Items.HuTao
{ 
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class HuTaoHoma : ModItem
	{
		SoundStyle NoECharge = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/HuTao/NoECharge") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		SoundStyle HuTaoE = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/HuTao/HuTaoE") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		SoundStyle ECharged = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/HuTao/ECharged") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.BooTao2.hjson' file.
		public override void SetStaticDefaults() {
			ItemID.Sets.SkipsInitialUseSound[Item.type] = true; // This skips use animation-tied sound playback, so that we're able to make it be tied to use time instead in the UseItem() hook.
			ItemID.Sets.Spears[Item.type] = true; // This allows the game to recognize our new item as a spear.
		}

		public override void SetDefaults() {
			// Common Properties
			Item.rare = ItemRarityID.Red;
			Item.value = Item.sellPrice(silver: 30);

			// Use Properties
			Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
			Item.useAnimation = 40; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.useTime = 60; // The length of the item's use time in ticks (60 ticks == 1 second.)
			// Item.UseSound = SoundID.Item71; // The sound that this item plays when used.
			Item.autoReuse = true; // Allows the player to hold click to automatically use the item again. Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()
			
			// Weapon Properties
			Item.damage = 55;
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod)) {
				Item.damage = 100;
			}
			Item.crit = 5;
			Item.knockBack = 6.5f;
			Item.noUseGraphic = true; // When true, the item's sprite will not be visible while the item is in use. This is true because the spear projectile is what's shown so we do not want to show the spear sprite as well.
			Item.DamageType = DamageClass.Melee;
			Item.noMelee = true; // Allows the item's animation to do damage. This is important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.

			// Projectile Properties
			Item.shootSpeed = 3.7f; // The speed of the projectile measured in pixels per frame.
			Item.shoot = ModContent.ProjectileType<HuTaoHomaProj>(); // The projectile that is fired from this weapon
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}

		public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2){
				//Item.buffType = ModContent.BuffType<Buffs.ExampleDefenseBuff>(); // Specify an existing buff to be applied when used.
				//Item.buffTime = 5400; // The amount of time the buff declared in Item.buffType will last in ticks.
				if (player.GetModPlayer<BooTaoPlayer>().CanUseHuTaoE()) {
					Item.useTime = 20;
					Item.useAnimation = 20;
					Item.shoot = 0;
					Item.shootSpeed = 0f;
					SoundEngine.PlaySound(HuTaoE, player.Center);
				}
				else {
					return false;
				}
			}
			else {
				if (player.GetModPlayer<BooTaoPlayer>().der > 0) {
					Item.useAnimation = 26;
					Item.useTime = 40;
				}
				else {
					Item.useAnimation = 40;
					Item.useTime = 60;
				}
				Item.shootSpeed = 3.7f;
				Item.shoot = ModContent.ProjectileType<HuTaoHomaProj>();
			}
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			return player.ownedProjectileCounts[Item.shoot] < 1;
		}

		/* public override bool? UseItem(Player player) {
			// Because we're skipping sound playback on use animation start, we have to play it ourselves whenever the item is actually used.
			if (!Main.dedServ && Item.UseSound.HasValue) {
				SoundEngine.PlaySound(silence, player.Center);
			}

			return null;
		} */
		
		public override void HoldItem(Player player)
		{
			if (player.GetModPlayer<ExampleDashPlayer>().DashAccessoryEquipped = true) {
				player.GetModPlayer<ExampleDashPlayer>().DashAccessoryEquipped = false;
			}
			if (player.GetModPlayer<BooTaoPlayer>().der > 0) {
				player.GetDamage(DamageClass.Generic) += player.GetModPlayer<BooTaoPlayer>().HuTaoHPDmgBuff; // Increase ALL player damage
				//player.endurance = 1f - (0.1f * (1f - player.endurance));
				// player.statLifeMax2 += 100;
				//player.statDefense += 50;
				player.noKnockback = true;
				player.noFallDmg = true;
				if (player.statLife <= (player.statLifeMax2 * 0.5) ) {
					player.GetDamage(DamageClass.Generic) += 0.33f;
				}
			}
		}
		
		public override void UpdateInventory (Player player) {
			if (player.GetModPlayer<BooTaoPlayer>().coold > 0) {
				player.GetModPlayer<BooTaoPlayer>().coold--;
			}
			if (player.GetModPlayer<BooTaoPlayer>().der > 0) {
				player.GetModPlayer<BooTaoPlayer>().der--;
			}
			if ((player.GetModPlayer<BooTaoPlayer>().coold > 0) && (player.GetModPlayer<BooTaoPlayer>().der == 0)){
				player.GetCritChance(DamageClass.Generic) += 12;
			}
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			player.GetModPlayer<ExampleDashPlayer>().DashAccessoryEquipped = true;
			if (player.GetModPlayer<BooTaoPlayer>().der > 0) {
				SoundEngine.PlaySound(ECharged, player.Center);
			}
			else {
				SoundEngine.PlaySound(NoECharge, player.Center);
			}
			return true;
		}
		
		/*public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			player.GetModPlayer<ExampleDashPlayer>().DashAccessoryEquipped = 30;
		}*/

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod) && calamityMod.TryFind("PurifiedGel", out ModItem purifiedGel) ) {
				recipe.AddIngredient(purifiedGel.Type);
			}
			else {
				recipe.AddIngredient(ItemID.Bone, 1);
			}
			recipe.AddIngredient(ItemID.HellstoneBar, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
	
	public class ExampleDashPlayer : ModPlayer
	{
		public const int DashCooldown = 20; // Time (frames) between starting dashes.
		
		// The initial velocity.  10 velocity is about 37.5 tiles/second or 50 mph
		public const float DashVelocity = 12f;
		public const int DashDuration = 35; // Duration of the dash afterimage effect in frames
		
		// The fields related to the dash accessory
		public bool DashAccessoryEquipped = false;
		public bool XiaoPlungeEquipped = false;
		public int DashDelay = 0; // frames remaining till we can dash again
		public int DashTimer = 0; // frames remaining in the dash
		
		public override void PreUpdateMovement() {
			if (DashAccessoryEquipped && DashDelay == 0) {
				// store the new velocity
				Vector2 newVelocity = Player.velocity;
				
				// get position of mouse
				// Main.MouseWorld
				Vector2 aim = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
				
				// obtain the difference between mouse position and player position
				Vector2 target = aim - Player.Center;
				
				// use it to draw a vector to obtain the direction
				Vector2 direction = target.SafeNormalize(Vector2.UnitX);
				
				// set the new velocities 
				newVelocity.Y = direction.Y * DashVelocity * 1.5f;
				newVelocity.X = direction.X * DashVelocity;
				
				// start our dash
				DashDelay = DashCooldown;
				DashTimer = DashDuration;
				Player.velocity = newVelocity;
			}
			if (XiaoPlungeEquipped && DashDelay == 0) {
				
				// start our dash
				DashDelay = DashCooldown;
				DashTimer = DashDuration;
				Player.velocity = new Vector2(0, 70);
			}
			if (DashDelay > 0)
				DashDelay--;
			
			if (DashTimer > 0) { // dash is active
				// This is where we set the afterimage effect.  You can replace these two lines with whatever you want to happen during the dash
				// Some examples include:  spawning dust where the player is, adding buffs, making the player immune, etc.
				// Here we take advantage of "player.eocDash" and "player.armorEffectDrawShadowEOCShield" to get the Shield of Cthulhu's afterimage effect
				// Player.eocDash = DashTimer;
				Player.armorEffectDrawShadowEOCShield = true;

				// count down frames remaining
				DashTimer--;
			}
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