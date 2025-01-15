using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Buffs.KafkaAK
{
	public class KafkaAKBuff : ModBuff
	{
		public override void SetStaticDefaults() {
			Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
			Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
		}

		public override void Update(NPC npc, ref int buffIndex) {
			npc.velocity *= 0f;
			npc.GetGlobalNPC<BooTaoGlobalNPC>().KafkaAKSleep = true;
		}
	}
}
