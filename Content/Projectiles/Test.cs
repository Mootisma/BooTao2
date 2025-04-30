using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;

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
			Projectile.timeLeft = 20;
			Projectile.penetrate = -1;
		}
		
		public override void AI() {
			if (Main.myPlayer != Projectile.owner)
				return;
			
			Player player = Main.player[Projectile.owner];
			player.velocity.Y += 2f;
			//Projectile.Kill();
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
			//Item.shoot = ModContent.ProjectileType<Test>();
			//Item.shootSpeed = 15f;
		}
		
		public override bool CanUseItem(Player player) {
			//foreach (var npc in Main.ActiveNPCs) {
			//	npc.AddBuff(ModContent.BuffType<TestBuff>(), 300);
			//}
			//foreach (var teehee in Main.ActivePlayers) {
			//	teehee.AddBuff(ModContent.BuffType<TestBuff>(), 300, true);
			//}
			Vector2 tp = Main.MouseWorld;
			NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, (float)player.whoAmI, tp.X, tp.Y, 1, 0, 0);
			player.Teleport(tp, 1, 0);
			return true;
		}
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone){
			target.AddBuff(ModContent.BuffType<TestBuff>(), 300);
			//target.GetGlobalNPC<BooTaoGlobalNPC>().test = true;
		}
		//public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers) {}
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
			//npc.velocity *= 0.1f;
			//if (Math.Abs(npc.velocity.Y) <= 0.1f && Math.Abs(npc.velocity.X) <= 0.1f) {
			//	npc.velocity *= 2f;
			//}
			npc.GetGlobalNPC<BooTaoGlobalNPC>().test = true;
		}
	}
}
/* 
https://prts.wiki/w/%E6%B5%8A%E5%BF%83%E6%96%AF%E5%8D%A1%E8%92%82#%E6%B3%A8%E9%87%8A%E4%B8%8E%E9%93%BE%E6%8E%A5
https://github.com/tModLoader/tModLoader/blob/ARCHIVED-2022.11-1.4.3/ExampleMod/Content/NPCs/ExampleCustomAISlimeNPC.cs
https://ezgif.com
*/