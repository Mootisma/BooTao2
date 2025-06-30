using Terraria;
using Terraria.ModLoader;
using BooTao2.Content.Projectiles.Escoffier;

namespace BooTao2.Content.Buffs.Escoffier
{
	public class EscoffierBuff : ModBuff
	{
		public const int DefenseReductionPercent = 50;
		public static float DefenseMultiplier = 1 - DefenseReductionPercent / 100f;

		public override void SetStaticDefaults() {
			Main.pvpBuff[Type] = true; // This buff can be applied by other players in Pvp
			Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex) {
			npc.GetGlobalNPC<BooTaoGlobalNPC>().EscoffierDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			//player.GetModPlayer<BooTaoPlayer>().Escoffier = true;
			//player.statDefense *= DefenseMultiplier;
			
			// If the minions exist reset the buff time, otherwise remove the buff from the player
			if (player.ownedProjectileCounts[ModContent.ProjectileType<EscoffierSkill>()] > 0) {
				player.buffTime[buffIndex] = 18000;
			}
			else {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
