using Terraria;
using Terraria.ModLoader;
using BooTao2.Systems;

namespace BooTao2.Content.Buffs.BlackSwan
{
	public class BlackSwanBuff : ModBuff
	{
		public override string Texture => "BooTao2/Content/Buffs/HomaPickaxeBuff";
		public override void Update(NPC npc, ref int buffIndex) {
			npc.GetGlobalNPC<BooTaoGlobalNPC>().bspeepee = true;
			//npc.GetGlobalNPC<BooTaoGlobalNPC>().Arcana += 1;
		}
	}
}
