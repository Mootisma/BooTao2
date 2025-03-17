using Terraria;
using Terraria.ModLoader;

namespace BooTao2.Content.Buffs.Kafka
{
	public class KafkaBuff : ModBuff
	{
		public override void Update(NPC npc, ref int buffIndex) {
			npc.GetGlobalNPC<BooTaoGlobalNPC>().kfpeepee = true;
		}
	}
}
