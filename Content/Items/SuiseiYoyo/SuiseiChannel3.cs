using System;
using BooTao2.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Items.SuiseiYoyo
{
	public class SuiseiChannel3 : ModItem
	{
		public override string Texture => "BooTao2/Content/Items/SuiseiYoyo/SuiseiChannel";
		
		public override void SetStaticDefaults() {
			ItemID.Sets.Yoyo[Item.type] = true; 
			ItemID.Sets.GamepadExtraRange[Item.type] = 15; 
			ItemID.Sets.GamepadSmartQuickReach[Item.type] = true; 
		}

		public override void SetDefaults() {
			Item.width = 50;
			Item.height = 50;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.UseSound = SoundID.Item1;

			Item.damage = 39;
			Item.DamageType = DamageClass.MeleeNoSpeed;
			Item.knockBack = 4f;
			Item.crit = 46;
			Item.channel = true;
			Item.value = 1;
			Item.rare = 4;
			Item.shoot = ModContent.ProjectileType<SuiseiChannelProj>();
			Item.shootSpeed = 32f;		
		}

		private static readonly int[] unwantedPrefixes = new int[] { PrefixID.Terrible, PrefixID.Dull, PrefixID.Shameful, PrefixID.Annoying, PrefixID.Broken, PrefixID.Damaged, PrefixID.Shoddy };

		public override bool AllowPrefix(int pre) {
			if (Array.IndexOf(unwantedPrefixes, pre) > -1) {
				return false;
			}

			// Don't reroll
			return true;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<SuiseiChannel2>(), 1)
				.AddIngredient(ItemID.Chik, 1)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}