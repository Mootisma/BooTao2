using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;
using BooTao2.Content.Projectiles.Lancet2;

namespace BooTao2.Content.Projectiles {
	public class Test : ModProjectile {
		public override string Texture => "Terraria/Images/Projectile_" + 3;
		
		public override void SetDefaults() {
			Projectile.width = 50;
			Projectile.height = 50;
			Projectile.aiStyle = 0;
			Projectile.friendly = false;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.9f;
			Projectile.tileCollide = true;
			Projectile.timeLeft = 30000;
			Projectile.penetrate = -1;
		}
		
		public override void AI() {
			if (Main.myPlayer != Projectile.owner)
				return;
			
			Projectile.Kill();
		}
	}
	
	public class TestProj2 : ModProjectile {
		public override string Texture => "Terraria/Images/Projectile_" + 5;
		
		public override void SetDefaults() {
			Projectile.width = 50;
			Projectile.height = 50;
			Projectile.aiStyle = 0;
			Projectile.friendly = false;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.9f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 300;
			Projectile.penetrate = -1;
		}
		
		public override void AI() {
			Projectile.Kill();
		}
	}
	
	public class TestItem : ModItem {
		// You can use a vanilla texture for your item by using the format: "Terraria/Item_<Item ID>".
		public override string Texture => "Terraria/Images/Item_" + ItemID.Meowmere;
		
		public override void SetDefaults() {
			Item.damage = 2;
			Item.knockBack = 1f;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.LightRed;
			Item.DamageType = DamageClass.Melee;
		}
		
		public override bool CanUseItem(Player player) {
			foreach (var npc in Main.ActiveNPCs) {
				npc.AddBuff(ModContent.BuffType<TestBuff>(), 300);
			}
			//foreach (var teehee in Main.ActivePlayers) {
			//	teehee.AddBuff(ModContent.BuffType<TestBuff>(), 300, true);
			//}
			return true;
		}
		
		//public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone){}
		public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers) {
			modifiers.DamageVariationScale *= 0f;
			if (target.lifeRegen < 0) {
				modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {
					hitInfo.Damage += Math.Abs(target.lifeRegen);
				};
			}
		}
	}
	
	public class TestBuff : ModBuff
	{
		public override string Texture => "Terraria/Images/Buff_" + 3;
		
		public override void SetStaticDefaults() {
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			player.moveSpeed *= 0.5f;
		}
		
		public override void Update(NPC npc, ref int buffIndex) {
			npc.velocity *= 0f;
			//npc.GetGlobalNPC<BooTaoGlobalNPC>().test = true;
		}
	}
}
/* 
https://prts.wiki/w/%E6%B5%8A%E5%BF%83%E6%96%AF%E5%8D%A1%E8%92%82#%E6%B3%A8%E9%87%8A%E4%B8%8E%E9%93%BE%E6%8E%A5
https://github.com/tModLoader/tModLoader/blob/ARCHIVED-2022.11-1.4.3/ExampleMod/Content/NPCs/ExampleCustomAISlimeNPC.cs
https://ezgif.com
*/