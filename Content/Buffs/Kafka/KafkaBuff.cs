using Terraria;
using Terraria.ModLoader;
using BooTao2.Systems;

namespace BooTao2.Content.Buffs.Kafka
{
	public class KafkaBuff : ModBuff
	{
		public override string Texture => "BooTao2/Content/Buffs/HomaPickaxeBuff";
		
		public override void SetStaticDefaults() {
			Main.debuff[Type] = true;  // Is it a debuff?
			Main.pvpBuff[Type] = true; // Players can give other players buffs, which are listed as pvpBuff
			Main.buffNoSave[Type] = true; // Causes this buff not to persist when exiting and rejoining the world
			//BuffID.Sets.BuffTimeIsExtendedWithGameDifficulty[Type] = false; // Makes higher game difficulties extend this buff's duration
		}
		
		public override void Update(NPC npc, ref int buffIndex) {
			npc.GetGlobalNPC<BooTaoGlobalNPC>().kfpeepee = true;
		}
	}
}
/*
https://github.com/tModLoader/tModLoader/blob/1.4.5/ExampleMod/Content/Buffs/ExampleLifeRegenDebuff.cs
*/