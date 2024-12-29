using BooTao2.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace BooTao2.Content.Buffs.Aventurine {
	public class AventurineShieldOrigin : ModBuff {
		public override void SetStaticDefaults() {
			Main.buffNoTimeDisplay[Type] = false;
			Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
		}
		
		public override void Update(Player player, ref int buffIndex) {
			if (player.GetModPlayer<BooTaoPlayer>().AventurineShieldHP > 0) {
				player.buffTime[buffIndex] = 59 + player.GetModPlayer<BooTaoPlayer>().AventurineBlindBet * 60;
			}
			else {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}