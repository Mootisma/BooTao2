using BooTao2.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace BooTao2.Content.Buffs.Entelechia {
	public class EntelechiaBuff : ModBuff {
		public override void SetStaticDefaults() {
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
		}

		public override void Update(Player player, ref int buffIndex) {
			player.GetModPlayer<BooTaoPlayer>().LaPlumaLifeRegen = true;
			if (!player.GetModPlayer<BooTaoPlayer>().LaPlumaHolding) {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			
			player.statLifeMax2 += player.GetModPlayer<BooTaoPlayer>().EntelechiaMaxHPBuff;
			if (player.GetModPlayer<BooTaoPlayer>().EnteReviveCD <= 0) {
				player.GetModPlayer<BooTaoPlayer>().EntelechiaRevive = true;
			}
		}
		
		public override void Update(NPC npc, ref int buffIndex) {
			npc.GetGlobalNPC<BooTaoGlobalNPC>().tnspeepee = true;
		}
	}
}