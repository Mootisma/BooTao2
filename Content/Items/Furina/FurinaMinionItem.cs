using BooTao2.Content.Buffs.Furina;
using BooTao2.Content.Projectiles.Furina;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace BooTao2.Content.Items.Furina
{
	public class FurinaMinionItem : ModItem//114000440_p0
	{
		public override void SetStaticDefaults() {
			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
			ItemID.Sets.StaffMinionSlotsRequired[Type] = 3f;
		}

		public override void SetDefaults() {
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod)) {
				Item.damage = 70;
			}
			else {
				Item.damage = 45;
			}
			Item.knockBack = 3.2f;
			Item.mana = 10;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Furina/FurinasShowBegins") {
				Volume = 0.9f,
				PitchVariance = 0f,
				MaxInstances = 1,
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew//ReplaceOldest
			};
			// These below are needed for a minion weapon
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.DamageType = DamageClass.Summon;
			Item.buffType = ModContent.BuffType<FurinaMinionBuff>();
			Item.shoot = ModContent.ProjectileType<FurinaMinionProj>();
		}
		
		public override bool CanUseItem(Player player) {
			return true;//(player.ownedProjectileCounts[ModContent.ProjectileType<FurinaMinionProj>()] < 1)
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			position = Main.MouseWorld;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			foreach (var proj in Main.ActiveProjectiles)
            {
                if (proj.active && (proj.type == Item.shoot || proj.type == ModContent.ProjectileType<FurinaMinion2Proj>() || proj.type == ModContent.ProjectileType<FurinaMinion3Proj>()) && proj.owner == player.whoAmI)
                {
                    proj.active = false;
                }
            }
			player.AddBuff(Item.buffType, 2);

			var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
			var projectile2 = Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<FurinaMinion2Proj>(), damage, knockback, Main.myPlayer);
			var projectile3 = Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<FurinaMinion3Proj>(), damage, knockback, Main.myPlayer);
			projectile.originalDamage = Item.damage;
			projectile2.originalDamage = (int)(Item.damage * 0.8);
			projectile3.originalDamage = (int)(Item.damage * 0.6);

			return false;
		}

		public override void AddRecipes() {
			Recipe recipe = CreateRecipe();
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod) && calamityMod.TryFind("EssenceofHavoc", out ModItem essenceofHavoc) ) {
				recipe.AddIngredient(essenceofHavoc.Type);
			}
			recipe.AddIngredient(ItemID.SoulofLight, 1);
			recipe.AddIngredient(ItemID.SoulofNight, 1);
			recipe.AddIngredient(ItemID.BottledWater, 1);
			recipe.AddIngredient(ItemID.Waterleaf, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
/*
https://docs.tmodloader.net/docs/stable/class_player.html
https://docs.tmodloader.net/docs/stable/class_item.html
https://docs.tmodloader.net/docs/stable/class_mod_projectile.html
https://docs.tmodloader.net/docs/stable/class_projectile.html
https://genshin-impact.fandom.com/wiki/Furina/Voice-Overs
https://www.youtube.com/watch?v=9Hv0QlZrjbs
https://ambr.top/en/archive/avatar/10000089/furina?mode=talent
https://static.wikia.nocookie.net/gensin-impact/images/3/30/VO_Furina_Elemental_Skill_1_06.ogg/revision/latest?cb=20231111061828
https://github.com/tModLoader/tModLoader/wiki/Geometry

*/