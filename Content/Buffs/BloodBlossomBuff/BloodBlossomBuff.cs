using Terraria;
using Terraria.ModLoader;

namespace BooTao2.Content.Buffs.BloodBlossomBuff
{
	public class BloodBlossomBuff : ModBuff
	{
		public const int DefenseReductionPercent = 25;
		public static float DefenseMultiplier = 1 - DefenseReductionPercent / 100f;

		public override void SetStaticDefaults() {
			Main.pvpBuff[Type] = true; // This buff can be applied by other players in Pvp, so we need this to be true.
		}

		public override void Update(NPC npc, ref int buffIndex) {
			npc.GetGlobalNPC<BooTaoGlobalNPC>().BloodBlossom = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			player.GetModPlayer<BooTaoPlayer>().BloodBlossom = true;
			//player.statDefense *= DefenseMultiplier;
		}
	}
}
