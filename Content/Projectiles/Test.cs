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
		
		int counter = 0;
		public override void AI() {
			if (Main.myPlayer != Projectile.owner)
				return;
			
			Projectile.Kill();
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity) {
			Projectile.velocity = Vector2.Zero;
			return false;
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
			Item.damage = 1;
			Item.knockBack = 1f;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.LightRed;
			Item.noMelee = true; 
			Item.DamageType = DamageClass.Magic;
		}
		
		//public override void UpdateInventory (Player player) {
		//	player.GetModPlayer<BooTaoPlayer>().damageTest++;
		//}
		
		//public override void HoldItem(Player player) {
		//	player.GetModPlayer<BooTaoPlayer>().damageTest++;
		//}
		
		public override bool CanUseItem(Player player) {
			return true;
		}
	}
	
	public class TestBuff : ModBuff
	{
		public override string Texture => "Terraria/Images/Buff_" + 3;
		
		public override void SetStaticDefaults() {
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
		}
	}
}
/* 
https://prts.wiki/w/%E6%B5%8A%E5%BF%83%E6%96%AF%E5%8D%A1%E8%92%82#%E6%B3%A8%E9%87%8A%E4%B8%8E%E9%93%BE%E6%8E%A5
https://github.com/tModLoader/tModLoader/blob/ARCHIVED-2022.11-1.4.3/ExampleMod/Content/NPCs/ExampleCustomAISlimeNPC.cs
https://ezgif.com
*/