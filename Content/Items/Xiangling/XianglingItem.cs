using BooTao2.Content.Buffs.Xiangling;
using BooTao2.Content.Projectiles.Xiangling;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace BooTao2.Content.Items.Xiangling
{
	public class XianglingItem : ModItem
	{
		bool CalamityActive = ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);
		
		public override void SetStaticDefaults() {
			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
			ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f; // The default value is 1, but other values are supported. See the docs for more guidance. 
		}

		public override void SetDefaults() {
			Item.damage = (CalamityActive) ? 80 : 60;
			Item.knockBack = 5f;
			Item.mana = 10;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noUseGraphic = true;
			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Xiangling/EatThis") {
				Volume = 0.9f,
				PitchVariance = 0f,
				MaxInstances = 1,
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew//ReplaceOldest
			};

			// These below are needed for a minion weapon
			Item.noMelee = true; // this item doesn't do any melee damage
			Item.DamageType = DamageClass.Summon; // Makes the damage register as summon.
			Item.buffType = ModContent.BuffType<XianglingBuff>();
			// No buffTime because otherwise the item tooltip would say something like "1 minute duration"
			Item.shoot = ModContent.ProjectileType<XianglingPyronado>();
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			bool flag = false; // Checking if Pyronado has already spawned
			
			foreach (var proj in Main.ActiveProjectiles)
            {
                if (proj.active && proj.type == Item.shoot && proj.owner == player.whoAmI)
                {
                    //proj.active = false;
					flag = true;
                }
            }
			
			if (flag) {
				Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<Guoba>(), damage, knockback, player.whoAmI, 0f);
				player.UpdateMaxTurrets();
				return false;
			}
			
			// This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
			player.AddBuff(Item.buffType, 2);

			// Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
			var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
			projectile.originalDamage = Item.damage;

			// Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
			return false;
		}
		
		public override void HoldItem(Player player) {
			player.GetModPlayer<BooTaoPlayer>().GuobaStoreMouse = Main.MouseWorld;
		}

		public override void AddRecipes() {
			Recipe recipe = CreateRecipe();
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod) && calamityMod.TryFind("PurifiedGel", out ModItem purifiedGel) ) {
				recipe.AddIngredient(purifiedGel.Type);
			}
			recipe.AddIngredient(ItemID.Gel, 1);
			recipe.AddIngredient(ItemID.HellstoneBar, 1);
			recipe.AddIngredient(ItemID.CookingPot, 1);
			recipe.AddIngredient(ItemID.Spear, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
/*
https://www.reddit.com/r/Genshin_Impact/comments/l78506/ningguang_xiangling_and_xiao_emotes/
*/