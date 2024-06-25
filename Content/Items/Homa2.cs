using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using BooTao2.Content.Dusts;//https://github.com/tModLoader/tModLoader/tree/1.4.4/ExampleMod/Content/Dusts

namespace BooTao2.Content.Items
{
	public class Homa2 : ModItem
	{
		//public override void SetStaticDefaults()
		//{
		//	DisplayName.SetDefault("R2 Homa"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
		//	Tooltip.SetDefault("[c/FF9696:HP increased by 25%. Additionally, provides an ATK Bonus based on 1% of the wielder's Max HP. When the wielder's HP is less than 50%, this ATK bonus is increased by an additional 1.2% of Max HP.]");
		//}//https://terraria.wiki.gg/wiki/Chat#Tags

		public override void SetDefaults()
		{
			Item.damage = 11;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.useStyle = 1;
			Item.knockBack = 9;
			Item.pick = 90;
            Item.axe = 20;
            Item.tileBoost = 2;
			Item.value = 1;
			Item.rare = 10;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.maxStack = 9999;
			//Item.defense = 1;https://docs.tmodloader.net/docs/1.4-stable/class_terraria_1_1_item.html
		}

		public override void AddRecipes()
		{//Recipe recipe = Recipe.Create(ModContent.ItemType<Items.ExampleItem>(), 999);
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<Homa>(), 1);
			recipe.AddIngredient(ItemID.DeathbringerPickaxe, 1);
			recipe.AddIngredient(ItemID.HunterPotion, 1);
			recipe.AddIngredient(ItemID.SpelunkerPotion, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}
		public override bool CanUseItem(Player player){
			if (player.altFunctionUse == 2){
				Item.useTime = 55;
				Item.useAnimation = 55;
				Item.shoot = ProjectileID.StarWrath; // ID of the projectiles the sword will shoot
				Item.shootSpeed = 8f; // Speed of the projectiles the sword will shoot
			}
			else{
				Item.useTime = 6;
				Item.useAnimation = 6;
				Item.shoot = 0;
				Item.shootSpeed = 0f;
			}
			return base.CanUseItem(player);
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
			float ceilingLimit = target.Y;
			if (ceilingLimit > player.Center.Y - 200f) {
				ceilingLimit = player.Center.Y - 200f;
			}
			// Loop these functions 3 times.
			for (int i = 0; i < 3; i++) {
				position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
				position.Y -= 100 * i;
				Vector2 heading = target - position;

				if (heading.Y < 0f) {
					heading.Y *= -1f;
				}

				if (heading.Y < 20f) {
					heading.Y = 20f;
				}

				heading.Normalize();
				heading *= velocity.Length();
				heading.Y += Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(source, position, heading, type, damage * 2, knockback, player.whoAmI, 0f, ceilingLimit);
			}

			return false;
		}
		
		public override void MeleeEffects(Player player, Rectangle hitbox) {
			if (Main.rand.NextBool(3)) {
				// Emit dusts when the sword is swung
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.Sparkle>());
			}
		}
		
		public override void UpdateInventory (Player player) {
			//player.tileRangeX += 10;//
            //player.tileRangeY += 10;//block placement ranges
			player.wallSpeed += 60;
			player.tileSpeed += 60;
			player.pickSpeed -= 0.35f;
			Lighting.AddLight(player.position, 1f, 1f, 1f);
			player.nightVision = true;
			player.GetModPlayer<BooTaoPlayer>().Magnet = true;
			player.detectCreature = true;
			player.findTreasure = true;
		}
	}
}