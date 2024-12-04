using BooTao2.Content.Projectiles.Thorns;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using BooTao2.Content.Dusts;
using BooTao2.Content.Buffs;
using BooTao2.Content.Buffs.Thorns;
using Terraria.Audio;

namespace BooTao2.Content.Items.Thorns
{
	public class ThornsItem : ModItem
	{
		bool CalamityActive = ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);
		
		SoundStyle Skill = new SoundStyle($"{nameof(BooTao2)}/Assets/Sounds/Items/Thorns/AtkBoost") {
			Volume = 0.9f,
			PitchVariance = 0f,
			MaxInstances = 3,
		};
		
		public override void SetDefaults() {
			Item.damage = (CalamityActive) ? 75 : 50;
			Item.DamageType = DamageClass.Melee;
			Item.shoot = ModContent.ProjectileType<ThornsProj>();
			Item.shootSpeed = 16f;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 40;
			Item.useAnimation = 40;
			Item.useStyle = 1;
			Item.knockBack = 6;
			//
			Item.value = Item.sellPrice(gold: 30);
			Item.rare = 3;
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
		
		public override void UpdateInventory (Player player) {
			if (player.GetModPlayer<BooTaoPlayer>().ThornsS3duration > 0) {
				player.GetModPlayer<BooTaoPlayer>().ThornsS3duration--;
			}
		}
		
		public override void HoldItem(Player player) {
			if (player.GetModPlayer<BooTaoPlayer>().ThornsHealingCD > 0){
				player.GetModPlayer<BooTaoPlayer>().ThornsHealingCD--;
			}
			if (player.GetModPlayer<BooTaoPlayer>().ThornsHealingCD <= 0){
				player.AddBuff(ModContent.BuffType<ThornsRegen>(), 2, true);
			}
		}
		
		public override void MeleeEffects(Player player, Rectangle hitbox){
			player.GetModPlayer<BooTaoPlayer>().ThornsHealingCD = 121;
		}
		
		public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2){
				if (player.GetModPlayer<BooTaoPlayer>().ThornsSP >= 15 && player.GetModPlayer<BooTaoPlayer>().ThornsS3numUses < 2) {
					Item.shoot = 0;
					Item.shootSpeed = 0f;
					if (player.GetModPlayer<BooTaoPlayer>().ThornsS3numUses == 1){
						player.GetModPlayer<BooTaoPlayer>().ThornsS3numUses = 2;
						player.GetModPlayer<BooTaoPlayer>().ThornsSP = 0;
						SoundEngine.PlaySound(Skill, player.Center);
					}
					if (player.GetModPlayer<BooTaoPlayer>().ThornsS3numUses == 0){
						player.GetModPlayer<BooTaoPlayer>().ThornsSP = 0;
						player.GetModPlayer<BooTaoPlayer>().ThornsS3numUses = 1;
						player.GetModPlayer<BooTaoPlayer>().ThornsS3duration = 1800;
						SoundEngine.PlaySound(Skill, player.Center);
					}
				}
				else {
					return false;
				}
			}
			else {
				Item.shoot = ModContent.ProjectileType<ThornsProj>();
				Item.shootSpeed = 16f;
				if (player.GetModPlayer<BooTaoPlayer>().ThornsS3numUses == 1 && player.GetModPlayer<BooTaoPlayer>().ThornsS3duration > 0){
					Item.useAnimation = 30;
					Item.useTime = 30;
					Item.damage = (CalamityActive) ? 100 : 65;
				}
				else if (player.GetModPlayer<BooTaoPlayer>().ThornsS3numUses == 2){
					Item.useAnimation = 20;
					Item.useTime = 20;
					Item.damage = (CalamityActive) ? 120 : 78;
				}
				else {
					Item.useAnimation = 40;
					Item.useTime = 40;
					Item.damage = (CalamityActive) ? 80 : 52;
				}
			}
			return true;
		}
	}
}