using BooTao2.Content.Items.Mostima;
using BooTao2.Content.Projectiles.Mostima;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Buffs.Mostima
{
	public class MostimaMinionBuff : ModBuff
	{
		public override void SetStaticDefaults() {
			Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
			Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
		}

		public override void Update(Player player, ref int buffIndex) {
			// If the minions exist reset the buff time, otherwise remove the buff from the player
			if (player.ownedProjectileCounts[ModContent.ProjectileType<MostimaMinion>()] > 0) {
				player.buffTime[buffIndex] = 18000;
			}
			else {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			
			//
			if (NPC.downedMoonlord) {
				player.lifeRegen += 2;
				player.statDefense += 4;
			}
		}
	}
	
	public class MostimaSlow1 : ModBuff
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
			npc.velocity *= 0.8f;
			if (Math.Abs(npc.velocity.Y) <= 0.1f && Math.Abs(npc.velocity.X) <= 0.1f) {
				npc.velocity *= 2f;
			}
			//npc.GetGlobalNPC<BooTaoGlobalNPC>().test = true;
		}
	}
	
	public class MostimaSlow2 : ModBuff
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
			npc.velocity *= 0.8f;
			if (Math.Abs(npc.velocity.Y) <= 0.1f && Math.Abs(npc.velocity.X) <= 0.1f) {
				npc.velocity *= 2f;
			}
			//npc.GetGlobalNPC<BooTaoGlobalNPC>().test = true;
		}
	}
	
	public class MostimaSlow3 : ModBuff
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
			npc.velocity *= 0.8f;
			if (Math.Abs(npc.velocity.Y) <= 0.1f && Math.Abs(npc.velocity.X) <= 0.1f) {
				npc.velocity *= 2f;
			}
			//npc.GetGlobalNPC<BooTaoGlobalNPC>().test = true;
		}
	}
}
