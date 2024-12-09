using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;

namespace BooTao2.Content.Projectiles {
	public class Test : ModProjectile {
		public override string Texture => "Terraria/Images/Projectile_" + 3;
		// public override void SetStaticDefaults() {}
		
		public override void SetDefaults() {
			Projectile.width = 50;
			Projectile.height = 50;
			Projectile.aiStyle = 0;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.9f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 3000;
			Projectile.penetrate = -1;
		}
		
		int counter = 0;
		public override void AI() {
			if (counter > 120) { //https://docs.tmodloader.net/docs/stable/class_main.html#a55969af8fef5fd9819d1854403cf794b
				foreach (var proj in Main.ActiveProjectiles) {
					proj.velocity = Vector2.Zero;
				}
				foreach (var npc in Main.ActiveNPCs) {
					npc.velocity = Vector2.Zero;
				}
				counter = 0;
			}
			
			counter++;
		}
	}
	
	public class TestItem : ModItem {
		// You can use a vanilla texture for your item by using the format: "Terraria/Item_<Item ID>".
		public override string Texture => "Terraria/Images/Item_" + ItemID.Meowmere;
		
		public override void SetStaticDefaults() {
			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
			ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f; // The default value is 1, but other values are supported. See the docs for more guidance. 
		}

		public override void SetDefaults() {
			Item.damage = 1;
			Item.knockBack = 1f;
			Item.mana = 1;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.useStyle = ItemUseStyleID.Swing;
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
			Item.buffType = ModContent.BuffType<TestBuff>();
			// No buffTime because otherwise the item tooltip would say something like "1 minute duration"
			Item.shoot = ModContent.ProjectileType<Test>();
		}
		
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			// Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
			position = Main.MouseWorld;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			foreach (var proj in Main.ActiveProjectiles)
            {
                if (proj.active && proj.type == Item.shoot && proj.owner == player.whoAmI)
                {
                    proj.active = false;
                }
            }
			
			// This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
			player.AddBuff(Item.buffType, 2);

			// Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
			var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
			projectile.originalDamage = Item.damage;

			// Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
			return false;
		}
	}
	
	public class TestBuff : ModBuff
	{
		public override string Texture => "Terraria/Images/Buff_" + 3;
		
		public override void SetStaticDefaults() {
			Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
			Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
		}

		public override void Update(Player player, ref int buffIndex) {
			// If the minions exist reset the buff time, otherwise remove the buff from the player
			if (player.ownedProjectileCounts[ModContent.ProjectileType<Test>()] > 0) {
				player.buffTime[buffIndex] = 18000;
			}
			else {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
/* 
https://prts.wiki/w/%E6%B5%8A%E5%BF%83%E6%96%AF%E5%8D%A1%E8%92%82#%E6%B3%A8%E9%87%8A%E4%B8%8E%E9%93%BE%E6%8E%A5
https://github.com/tModLoader/tModLoader/blob/ARCHIVED-2022.11-1.4.3/ExampleMod/Content/NPCs/ExampleCustomAISlimeNPC.cs
https://ezgif.com
*/