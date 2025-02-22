using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Buffs.Xiao
{
	public class XiaoBuff : ModBuff
	{
		public override string Texture => "Terraria/Images/Buff_" + 3;
		
		public override void SetStaticDefaults() {
			Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
			Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
		}

		public override void Update(Player player, ref int buffIndex) {
			player.moveSpeed *= 1.1f;
			player.maxFallSpeed += 10f;
			player.jumpSpeedBoost += 3f;
			player.noKnockback = true;
			player.noFallDmg = true;
			//player.lifeRegen -= 16;
		}
	}
}
