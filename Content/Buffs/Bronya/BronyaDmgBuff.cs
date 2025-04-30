using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Buffs.Bronya
{
	public class BronyaDmgBuff : ModBuff
	{
		public override string Texture => "Terraria/Images/Buff_" + 159;
		
		public override void SetStaticDefaults() {
			Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
			Main.buffNoTimeDisplay[Type] = false; // The time remaining won't display on this buff
		}

		public override void Update(Player player, ref int buffIndex) {
			player.GetDamage(DamageClass.Generic) += 0.5f;
		}
	}
}
