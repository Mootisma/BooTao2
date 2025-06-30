using BooTao2.Content.Items.Ningguang;
using BooTao2.Content.Projectiles.Ningguang;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Buffs.Ningguang
{
	public class NingBuff : ModBuff
	{
		public override void SetStaticDefaults() {
			Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
			Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
		}

		public override void Update(Player player, ref int buffIndex) {
			player.GetDamage(DamageClass.Generic) += 0.12f;// 12% dmg buff
			player.GetModPlayer<BooTaoPlayer>().NingJadeScreen = true;
		}
	}
}
