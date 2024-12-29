using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Buffs.Aventurine
{
	public class AventurineShield : ModBuff
	{
		public override void SetStaticDefaults() {
			Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
			Main.buffNoTimeDisplay[Type] = false; // The time remaining won't display on this buff
		}

		public override void Update(Player player, ref int buffIndex) {
			if (player.GetModPlayer<BooTaoPlayer>().AventurineShieldHP > 0) {
				player.buffTime[buffIndex] = player.GetModPlayer<BooTaoPlayer>().AventurineShieldHP * 60;
			}
			else {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
