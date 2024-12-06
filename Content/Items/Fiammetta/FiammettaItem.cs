using BooTao2.Content.Projectiles.Fiammetta;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using BooTao2.Content.Dusts;
using BooTao2.Content.Buffs;
//using BooTao2.Content.Buffs.Thorns;
using Terraria.Audio;

namespace BooTao2.Content.Items.Fiammetta
{
	public class FiammettaItem : ModItem
	{
		bool CalamityActive = ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);
		
		SoundStyle Skill = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Thorns/AtkBoost") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		public override void SetDefaults() {
			Item.damage = (CalamityActive) ? 120 : 80;
			Item.DamageType = DamageClass.Ranged;
			Item.shoot = ModContent.ProjectileType<FiammettaProj>();
			Item.shootSpeed = 16f;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = (player.GetModPlayer<BooTaoPlayer>().FiammettaS3) ? 60 : 40;
			Item.useAnimation = (player.GetModPlayer<BooTaoPlayer>().FiammettaS3) ? 60 : 40;
			Item.knockBack = 5;
			//
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.value = Item.sellPrice(gold: 30);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			// recipe.AddIngredient(ModContent.ItemType<Homa4>(), 1);
			recipe.AddIngredient(ItemID.PearlsandBlock, 1);
			recipe.AddIngredient(ItemID.SandBlock, 1);
			recipe.AddIngredient(ItemID.Sandgun, 1);
			recipe.AddIngredient(ItemID.SoulofMight, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			if (player.altFunctionUse == 2) {
				return false;
			}
			if (player.GetModPlayer<BooTaoPlayer>().FiammettaS3) {
				Vector2 aim = Main.MouseWorld;
				player.GetModPlayer<BooTaoPlayer>().FiammettaStoreMouse = aim;
				velocity.X = 0f;
				velocity.Y = 20f;
				//
				var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, (int)(damage / 10), knockback, Main.myPlayer);
				projectile.originalDamage = Item.damage;
				return false;
			}
			return true;
		}
		
		// public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers) {}
		
		public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2){
				if (player.GetModPlayer<BooTaoPlayer>().FiammettaSP >= 15) {
					player.GetModPlayer<BooTaoPlayer>().FiammettaSP = 0;
					player.GetModPlayer<BooTaoPlayer>().FiammettaS3 = true;
				}
				return false;
			}
			return true;
		}
	}
}