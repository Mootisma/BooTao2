using BooTao2.Content.Buffs.BloodBlossomBuff;
using BooTao2.Content.Items.HuTao;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace BooTao2
{
	public class BooTao2 : Mod
	{
		public override void AddRecipes()
		{//https://docs.tmodloader.net/docs/1.4-stable/class_terraria_1_1_i_d_1_1_item_i_d.html
			Recipe recipe = Recipe.Create(/*ModContent.ItemType<Items.ExampleItem>()*/ItemID.BlackInk, 1);
			recipe.AddIngredient(ItemID.StoneBlock, 2);
			recipe.Register();
			//
			//
			recipe = Recipe.Create(ItemID.JungleRose);
			recipe.AddIngredient(ItemID.JungleSpores, 5);
			recipe.Register();
			//
			recipe = Recipe.Create(ItemID.FeralClaws);
			recipe.AddIngredient(ItemID.JungleSpores, 5);
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
		}
	}
	
	public class BooTaoPlayer : ModPlayer
	{
		public bool Magnet;
		public bool Shrek;
		public bool lifeRegenDebuff;
		public bool BloodBlossom;
		public bool NingHolding; // player.GetModPlayer<BooTaoPlayer>().NingHolding
		public bool NingJade1State;
		public bool NingJade2State;
		public bool NingJade3State;
		//
		public const int HuTaoEDuration = 540;
		public const int HuTaoECD = 960;
		public int coold = 0;
		public int der = 0;
		public float HuTaoHPDmgBuff = 0;
		//
		public int NingNumBuff = 0;
		//
		public float FurinaDmgBuff = 0;
		//
		public float YelanDmgBuff = 0f;
		
		public override void ResetEffects()
		{
			Magnet = false;
			Shrek = false;
			lifeRegenDebuff = false;
			BloodBlossom = false;
			NingHolding = false;
			NingJade1State = false;
			NingJade2State = false;
			NingJade3State = false;
		}
		
		public bool CanUseHuTaoE() {
			if (coold == 0) {
				Player player = Main.LocalPlayer;
				HuTaoHPDmgBuff = (float)((player.statLife * 0.3) / 100);
				player.statLife = (int)(player.statLife * 0.7);
				coold = HuTaoECD;
				der = HuTaoEDuration;
				return true;
			}
			return false;
		}
		
		public override void ModifyHurt(ref Player.HurtModifiers modifiers) {
			if (der > 0) {
				modifiers.FinalDamage *= 0.1f;
			}
		}
		
		/*public override void NaturalLifeRegen (ref float regen)
		{
			if(Shrek){
				regen = regen * 12.1f;
			}
		}*/
		
		/*public override void UpdateLifeRegen() {
            /*Player player = Main.LocalPlayer;
            Tile tile;
            foreach(Point coord in player.TouchedTiles){
                tile = Main.tile[coord.X, coord.Y];
                if(tile.type == ModContent.TileType<Tiles.HolyDirt>() && HolyGrace /*player.HasItem(ModContent.ItemType<Items.Accesories.HolyFeet>()) && player.HasItem(mod.ItemType("HolyFeet")))*//*) {
                    //player.AddBuff(ModContent.BuffType<Buffs.HolyGrace>(),100);
                    player.lifeRegen += 6;
                    Main.NewText("Healing"); // Debug
                }
            }
        }*/
		
		public override void UpdateBadLifeRegen() {
			if (lifeRegenDebuff) {
				// These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects
				if (Player.lifeRegen > 0)
					Player.lifeRegen = 0;
				// Player.lifeRegenTime uses to increase the speed at which the player reaches its maximum natural life regeneration
				// So we set it to 0, and while this debuff is active, it never reaches it
				Player.lifeRegenTime = 0;
				// lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second
				Player.lifeRegen -= 16;
			}
		}
		
		public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright) {
			if (BloodBlossom) {
				// These color adjustments match the withered armor debuff visuals.
				g *= 0.5f;
				r *= 0.75f;
			}
		}
		
		//https://docs.tmodloader.net/docs/1.4-stable/class_terraria_1_1_mod_loader_1_1_mod_player.html#ae8dbfbc03aeba36009cca97321fb8754
	}
	
	public class BooTaoGlobalItem : GlobalItem
    {
        public override void GrabRange(Item item, Player player, ref int grabRange)
		{
			if(player.GetModPlayer<BooTaoPlayer>().Magnet)
			{
				//player.GetModPlayer<BooTaoPlayer>().Magnet = true; //paste to accessory :)
				grabRange += 3700;
			}
		}
		
		//public override void PostUpdate(Item item) {
		//	item.velocity *= 2;
		//}
		
		public override bool GrabStyle(Item item, Player player) {
			if(player.GetModPlayer<BooTaoPlayer>().Magnet)
			{
				Vector2 movement = item.DirectionTo(player.Center) * 20f;
				item.velocity = movement;
				// item.velocity = Collision.TileCollision(item.position, item.velocity, item.width, item.height);
				return true;
			}
			return false;
		}
    }
	
	public class BooTaoGlobalNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;
		public bool BloodBlossom;
		public int BloodBlossomDmg = 8;

		public override void ResetEffects(NPC npc) {
			BloodBlossom = false;
		}

		/*public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers) {
			if (BloodBlossom) {
				// For best results, defense debuffs should be multiplicative
				modifiers.Defense *= BloodBlossomBuff.DefenseMultiplier;
			}
		}*/

		public override void DrawEffects(NPC npc, ref Color drawColor) {
			// This simple color effect indicates that the buff is active
			if (BloodBlossom) {
				drawColor.G = 0;
			}
		}
		
		//public override void AI(NPC npc) {}
		
		public override void UpdateLifeRegen(NPC npc, ref int damage) {
			if (BloodBlossom) {
				if (npc.lifeRegen > 0) {
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= BloodBlossomDmg;
				damage = BloodBlossomDmg;
			}
		}
		
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {
			if (npc.type == NPCID.WallofFlesh)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HuTaoHoma>(), 50));
            }
		}
	}
}