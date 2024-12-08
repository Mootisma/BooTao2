using BooTao2.Content.Items.Skadi;
using BooTao2.Content.Projectiles.Skadi;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Buffs.Skadi
{
	public class SkadiS2Buff : ModBuff
	{
		public override void SetStaticDefaults() {
			Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
			Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
		}

		public override void Update(Player player, ref int buffIndex) {
			int healing = (int)(player.GetModPlayer<BooTaoPlayer>().SkadiATK / 10);
			if (player.GetModPlayer<BooTaoPlayer>().SkadiSP >= 56) {
				// 10% of skadi's attack as life regen. if skadi has 100 atk, life regen is 10, or 5 HP/s
				player.lifeRegen += healing;
				// 20% of skadi's attack as defense. 
				player.statDefense += (int)(player.GetModPlayer<BooTaoPlayer>().SkadiATK / 5);
				// 20% of skadi's attack, as a percentage increase
				// if skadi has 100 atk, player gets a 20% damage increase
				player.GetDamage(DamageClass.Generic) += player.GetModPlayer<BooTaoPlayer>().SkadiATK / 500;
			}
			player.lifeRegen += healing;
		}
	}
}
