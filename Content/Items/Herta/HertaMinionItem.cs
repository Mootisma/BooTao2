using BooTao2.Content.Buffs.Herta;
using BooTao2.Content.Projectiles.Herta;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace BooTao2.Content.Items.Herta
{
	public class HertaMinionItem : ModItem
	{
		SoundStyle KuruKuru = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Herta/kurukuru") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		public override void SetStaticDefaults() {
			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

			ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f; // The default value is 1, but other values are supported. See the docs for more guidance. 
		}

		public override void SetDefaults() {
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod)) {
				Item.damage = 40;
			}
			else {
				Item.damage = 20;
			}
			Item.knockBack = 3.2f;
			Item.mana = 10; // mana cost
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing; // how the player's arm moves when using the item
			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = KuruKuru; // What sound should play when using the item

			// These below are needed for a minion weapon
			Item.noMelee = true; // this item doesn't do any melee damage
			Item.DamageType = DamageClass.Summon; // Makes the damage register as summon. If your item does not have any damage type, it becomes true damage (which means that damage scalars will not affect it). Be sure to have a damage type
			Item.buffType = ModContent.BuffType<HertaMinionBuff>();
			// No buffTime because otherwise the item tooltip would say something like "1 minute duration"
			Item.shoot = ModContent.ProjectileType<HertaMinionProj>(); // This item creates the minion projectile
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			// Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
			position = Main.MouseWorld;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			// This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
			player.AddBuff(Item.buffType, 2);

			// Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
			var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
			projectile.originalDamage = Item.damage;

			// Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
			return false;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {
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
}
/*
https://docs.tmodloader.net/docs/stable/class_n_p_c.html
https://docs.tmodloader.net/docs/stable/class_projectile.html
https://github.com/ThePaperLuigi/The-Stars-Above/blob/main/Projectiles/Ranged/PleniluneGaze/FrostflakeArrow.cs
https://docs.tmodloader.net/docs/stable/class_projectile.html#a76031be1e4228ce7e8a3ec67e2c293ad
https://docs.tmodloader.net/docs/stable/class_mod_projectile.html
https://hsr.yatta.top/en/archive/avatar/1013/herta?mode=details
https://static.wikia.nocookie.net/houkai-star-rail/images/9/96/Herta_Sticker_02.png/revision/latest?cb=20231220232741

*/