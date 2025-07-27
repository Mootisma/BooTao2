using Terraria;
using Terraria.ModLoader;

namespace BooTao2.Content.Buffs.BlackSwan
{
	public class BlackSwanDefenseDebuff : ModBuff
	{
		public override void Update(NPC npc, ref int buffIndex) {
			npc.GetGlobalNPC<BooTaoGlobalNPC>().bsDefDebuff = true;
			//npc.GetGlobalNPC<BooTaoGlobalNPC>().Arcana += 1;
		}
	}
}
