using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using BooTao2.Systems;

namespace BooTao2.Systems
{
	public class BooTaoRecipes : ModSystem
	{
		// Other methods and fields here...

		public override void AddRecipes()
		{//https://docs.tmodloader.net/docs/1.4-stable/class_terraria_1_1_i_d_1_1_item_i_d.html
			Recipe recipe = Recipe.Create(/*ModContent.ItemType<Items.ExampleItem>()*/ItemID.BlackInk, 1);
			recipe.AddIngredient(ItemID.StoneBlock, 2);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			recipe = Recipe.Create(ItemID.SharkFin, 5);
			recipe.AddIngredient(ItemID.Goldfish, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.JungleRose);
			recipe.AddIngredient(ItemID.JungleSpores, 5);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			recipe = Recipe.Create(ItemID.FeralClaws);
			recipe.AddIngredient(ItemID.JungleSpores, 5);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.LavaCharm);
			recipe.AddIngredient(ItemID.HellstoneBar, 7);
			recipe.AddIngredient(ItemID.Obsidian);
			recipe.AddIngredient(ItemID.Shackle);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.ChlorophyteBar);
			recipe.AddIngredient(ItemID.ChlorophyteOre);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.PixieDust);
			recipe.AddIngredient(ItemID.SoulofLight);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.AngelHalo);
			recipe.AddIngredient(ItemID.SoulofLight, 10);
			recipe.AddIngredient(ItemID.CrystalBullet, 10);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.WormholePotion, 30);
			recipe.AddIngredient(ItemID.DirtBlock, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.Revolver, 1);
			recipe.AddIngredient(ItemID.DemoniteBar, 9);
			recipe.Register();
			recipe = Recipe.Create(ItemID.Revolver, 1);
			recipe.AddIngredient(ItemID.CrimtaneBar, 9);
			recipe.Register();
			recipe = Recipe.Create(ItemID.Spear, 1);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 3);
			recipe.Register();
			recipe = Recipe.Create(ItemID.CobaltChainsaw, 1);
			recipe.AddIngredient(ItemID.PalladiumChainsaw, 1);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.SummoningPotion, 1);
			recipe.AddIngredient(ItemID.SoulofLight, 2);
			recipe.Register();
			recipe = Recipe.Create(ItemID.Pho, 1);
			recipe.AddIngredient(ItemID.GlowingMushroom, 1);
			recipe.Register();
			recipe = Recipe.Create(ItemID.ChocolateChipCookie, 1);
			recipe.AddIngredient(ItemID.GlowingMushroom, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			recipe = Recipe.Create(ItemID.Bass, 1);
			recipe.AddIngredient(ItemID.GlowingMushroom, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			recipe = Recipe.Create(ItemID.BlueBerries, 1);
			recipe.AddIngredient(ItemID.GlowingMushroom, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			recipe = Recipe.Create(ItemID.SliceOfCake, 1);
			recipe.AddIngredient(ItemID.GlowingMushroom, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			recipe = Recipe.Create(ItemID.IceCream, 1);
			recipe.AddIngredient(ItemID.GlowingMushroom, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.ZapinatorOrange, 1);
			recipe.AddIngredient(ItemID.SoulofLight, 10);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.FrogLeg, 1);
			recipe.AddIngredient(ItemID.ShinyRedBalloon, 1);
			recipe.Register();
			recipe = Recipe.Create(ItemID.PanicNecklace, 1);
			recipe.AddIngredient(ItemID.Aglet, 1);
			recipe.AddIngredient(ItemID.HealingPotion, 2);
			recipe.Register();
			recipe = Recipe.Create(ItemID.ManaFlower, 1);
			recipe.AddIngredient(ItemID.JungleRose, 1);
			recipe.AddIngredient(ItemID.ManaPotion, 2);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.Shellphone, 1);
			recipe.AddIngredient(ItemID.LunarBar, 4);
			recipe.AddIngredient(ItemID.MagicMirror, 1);
			recipe.Register();
			recipe = Recipe.Create(ItemID.DPSMeter, 1);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 2);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.BouncingShield, 1);
			recipe.AddIngredient(ItemID.LunarBar, 4);
			recipe.Register();
			recipe = Recipe.Create(ItemID.Gatligator, 1);
			recipe.AddIngredient(ItemID.LunarBar, 4);
			recipe.Register();
			recipe = Recipe.Create(ItemID.Katana, 1);
			recipe.AddIngredient(ItemID.LunarBar, 4);
			recipe.Register();
			recipe = Recipe.Create(ItemID.ZapinatorGray, 1);
			recipe.AddIngredient(ItemID.LunarBar, 4);
			recipe.Register();
			recipe = Recipe.Create(ItemID.CoinGun, 1);
			recipe.AddIngredient(ItemID.LunarBar, 4);
			recipe.Register();
			recipe = Recipe.Create(ItemID.PaladinsShield, 1);
			recipe.AddIngredient(ItemID.LunarBar, 4);
			recipe.Register();
			recipe = Recipe.Create(ItemID.AnkhCharm, 1);
			recipe.AddIngredient(ItemID.LunarBar, 4);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.IceSkates, 1);
			recipe.AddIngredient(ItemID.IceBlock, 20);
			recipe.AddIngredient(ItemID.HermesBoots, 1);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.StylistKilLaKillScissorsIWish, 1);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 4);
			recipe.Register();
			recipe = Recipe.Create(ItemID.EskimoCoat, 1);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 4);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.CompanionCube, 1);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 4);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.SharpTears, 1);
			recipe.AddIngredient(ItemID.LunarBar, 14);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.Burger, 1);
			recipe.AddIngredient(ItemID.LunarBar, 1);
			recipe.Register();
			recipe = Recipe.Create(ItemID.BloodyMoscato, 1);
			recipe.AddIngredient(ItemID.LunarBar, 1);
			recipe.Register();
			recipe = Recipe.Create(ItemID.BlackBelt, 1);
			recipe.AddIngredient(ItemID.LunarBar, 1);
			recipe.Register();
			//
			if (ModLoader.TryGetMod("SOTS", out Mod sots)) {
				if (sots.TryFind("TinyPlanetFish", out ModItem planetfish)) {
					recipe = Recipe.Create(planetfish.Type, 1);
					recipe.AddIngredient(ItemID.SoulofLight, 1);
					recipe.AddIngredient(ItemID.Feather, 1);
					recipe.Register();
				}
				if (sots.TryFind("PhaseBar", out ModItem phasebar) && sots.TryFind("PhaseOre", out ModItem phaseore) && sots.TryFind("ParticleRelocator", out ModItem particlerelocator)){
					recipe = Recipe.Create(phasebar.Type, 1);
					recipe.AddIngredient(phaseore.Type, 3);
					recipe.AddTile(TileID.AdamantiteForge);
					recipe.Register();
					recipe = Recipe.Create(particlerelocator.Type, 1);
					recipe.AddIngredient(phasebar.Type, 25);
					recipe.AddTile(TileID.AdamantiteForge);
					recipe.Register();
				}
			}
		}
	}
}