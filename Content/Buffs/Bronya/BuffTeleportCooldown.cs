using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Buffs.Bronya
{
	public class BuffTeleportCooldown : ModBuff
	{
		public override string Texture => "Terraria/Images/Buff_" + 88;
		
		public override void SetStaticDefaults() {
			Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
			Main.buffNoTimeDisplay[Type] = false;
		}
	}
}
