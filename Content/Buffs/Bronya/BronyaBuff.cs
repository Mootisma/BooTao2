using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Buffs.Bronya
{
	public class BronyaBuff : ModBuff
	{
		public override string Texture => "Terraria/Images/Buff_" + 3;
		
		public override void SetStaticDefaults() {
			Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
			Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
		}

		public override void Update(Player player, ref int buffIndex) {
			player.moveSpeed *= 1.15f;
			player.maxFallSpeed += 2f;
			player.jumpSpeedBoost += 1f;
			player.manaRegenBonus += 5;
			//player.lifeRegen -= 16;
		}
	}
}
