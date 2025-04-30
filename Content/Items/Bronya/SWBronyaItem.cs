using BooTao2.Content.Buffs.Bronya;
using BooTao2.Content.Items;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace BooTao2.Content.Items.Bronya
{
	public class SWBronyaItem : ModItem
	{
		public override string Texture => "BooTao2/Content/Items/Bronya/BronyaItem";
		
		Vector2 TPposition = new Vector2(33450, 4923);
		bool CalamityActive = ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);
		
		public override void SetDefaults() {
			Item.damage = (CalamityActive) ? 210 : 120;
			Item.crit = 96;
			Item.knockBack = 5.5f;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 8;
			Item.useAnimation = 8;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noUseGraphic = true;
			Item.value = Item.sellPrice(20, 50, 0, 0);
			Item.rare = ItemRarityID.Green;
			//Item.UseSound = SoundID.Item11;

			Item.autoReuse = true;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Ranged;
			Item.shoot = ProjectileID.ExplosiveBullet;
			Item.shootSpeed = 30f;
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			if (player.altFunctionUse == 2) {
				player.AddBuff(ModContent.BuffType<BronyaDmgBuff>(), 900, true);
				return false;
				//SoundEngine.PlaySound(KafkaFUA, player.Center);
			}
			float numProjs = 4;
			float rotation = MathHelper.ToRadians(4);
			position += Vector2.Normalize(velocity) * 10f;
			for (int i = 0; i < numProjs; i++) {
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numProjs - 1)));
				Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
			}
			SoundEngine.PlaySound(SoundID.Item11, player.Center);
			player.AddBuff(ModContent.BuffType<BronyaBuff>(), 900, true);
			return true;
		}

		public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2 && Main.myPlayer == player.whoAmI) {
				TPposition = Main.MouseWorld;
				NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, (float)player.whoAmI, TPposition.X, TPposition.Y, 1, 0, 0);
				player.Teleport(TPposition, 1, 0);
			}
			return true;
		}
		
		public override void AddRecipes() {
			CreateRecipe(1)
				.AddIngredient<Items.Bronya.BronyaItem>()
				.AddIngredient<Items.SilverWolfMaterial>()
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}
/*
https://docs.tmodloader.net/docs/stable/class_n_p_c.html
https://docs.tmodloader.net/docs/stable/class_projectile.html
https://docs.tmodloader.net/docs/stable/class_projectile.html#a76031be1e4228ce7e8a3ec67e2c293ad
https://docs.tmodloader.net/docs/stable/class_mod_projectile.html
https://github.com/ThePaperLuigi/The-Stars-Above/blob/main/Items/Weapons/Summon/PhantomInTheMirror.cs

*/