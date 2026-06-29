using BooTao2.Content.Buffs.Serval;
using BooTao2.Content.Projectiles.Serval;
using BooTao2.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace BooTao2.Content.Items.Serval
{
	public class ServalItem : ModItem
	{
		public override void SetDefaults() {
			Item.damage = 20;
			Item.knockBack = 3.0f;
			Item.mana = 10; // mana cost
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 25;
			Item.useAnimation = 25;
			
			Item.useStyle = ItemUseStyleID.Swing;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.noUseGraphic = true;
			Item.value = Item.sellPrice(gold: 3, silver: 67, copper: 69);
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item94 with { Volume = 0.3f };

			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.shoot = ModContent.ProjectileType<ServalProj>();
			Item.shootSpeed = 10f;
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			position = Main.MouseWorld;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			if (player.altFunctionUse == 2){
				Projectile.NewProjectile(source, position, velocity, type, damage * 2, knockback, player.whoAmI, 3f);
				Projectile.NewProjectile(source, position, velocity * new Vector2(-1, 1), type, damage * 2, knockback, player.whoAmI, 3f);
				Projectile.NewProjectile(source, position, velocity * new Vector2(-1, -1), type, damage * 2, knockback, player.whoAmI, 3f);
				Projectile.NewProjectile(source, position, velocity * new Vector2(1, -1), type, damage * 2, knockback, player.whoAmI, 3f);
				return false;
			}
			return true;
		}
		
		public override void ModifyManaCost(Player player, ref float reduce, ref float mult) {
			if (player.altFunctionUse == 2){ mult *= 2f; }
		}
		
		public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            float damageMult = 1f;
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod)) {
				damageMult += 0.2f;
			}
			damage *= damageMult;
        }

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.CarbonGuitar, 1);
			recipe.AddIngredient(ItemID.MeteoriteBar, 1);
			recipe.AddIngredient(ItemID.TissueSample, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.CarbonGuitar, 1);
			recipe.AddIngredient(ItemID.MeteoriteBar, 1);
			recipe.AddIngredient(ItemID.ShadowScale, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.MeteoriteBar, 1);
			recipe.AddIngredient(ItemID.HellstoneBar, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		public override bool CanUseItem(Player player) {
			return player.ownedProjectileCounts[Item.shoot] < 2;
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