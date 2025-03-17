using Terraria;
using Terraria.ModLoader;

namespace BooTao2.Content.Buffs.BlackSwan
{
	public class BlackSwanBuff : ModBuff
	{
		public override void Update(NPC npc, ref int buffIndex) {
			npc.GetGlobalNPC<BooTaoGlobalNPC>().bspeepee = true;
			//npc.GetGlobalNPC<BooTaoGlobalNPC>().Arcana += 1;
		}
	}
}
