using BooTao2.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace BooTao2.Content.Buffs
{
	public class Platform1Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;//https://www.youtube.com/watch?v=sXZpVf-gqGU
			Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
			//DisplayName.SetDefault("shrek platform buff");
			//Description.SetDefault("increased life and mana regen and item pickup range");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			//player.jumpSpeedBoost += 2f;//200% :thinking:
            player.lifeRegen += 4;
			player.manaRegenBonus += 5;
			//if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			player.GetModPlayer<BooTaoPlayer>().Magnet = true;
            //player.tileSpeed += 70;
            //player.wallSpeed += 70;//A massive object devastated Uranus a long time ago and it never fully recovered
		}
	}
}
//if(!player.HasBuff(BuffType<MirageBladeCooldown>()) && player.statMana >= 50)
/*public override void AddRecipes()
		{
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				CreateRecipe(1)
					.AddIngredient(calamityMod.Find<ModItem>("BlossomPickaxe").Type, 1)
					.AddIngredient(ItemID.IronPickaxe, 1)
					.AddIngredient(ItemID.ExplosivePowder, 100)
					.AddIngredient(ItemID.LunarBar, 10)
					.AddIngredient(ItemType<EssenceOfTheAbyss>())
					.AddTile(TileID.Anvils)
					.Register();

				CreateRecipe(1)
					.AddIngredient(calamityMod.Find<ModItem>("BlossomPickaxe").Type, 1)
					.AddIngredient(ItemID.LeadPickaxe, 1)
					.AddIngredient(ItemID.ExplosivePowder, 100)
					.AddIngredient(ItemID.LunarBar, 10)
					.AddIngredient(ItemType<EssenceOfTheAbyss>())
					.AddTile(TileID.Anvils)
					.Register();

			}
			else
			{
				CreateRecipe(1)
					.AddIngredient(ItemID.IronPickaxe, 1)
					.AddIngredient(ItemID.ExplosivePowder, 100)
					.AddIngredient(ItemID.LunarBar, 10)
					.AddIngredient(ItemType<EssenceOfTheAbyss>())
					.AddTile(TileID.Anvils)
					.Register();

				CreateRecipe(1)
					.AddIngredient(ItemID.LeadPickaxe, 1)
					.AddIngredient(ItemID.ExplosivePowder, 100)
					.AddIngredient(ItemID.LunarBar, 10)
					.AddIngredient(ItemType<EssenceOfTheAbyss>())
					.AddTile(TileID.Anvils)
					.Register();
			}
			

		}*/