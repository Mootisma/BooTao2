using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using BooTao2.Systems;

namespace BooTao2.Content.Buffs
{
	public class Magnet2Buff : ModBuff
	{
		public override string Texture => "BooTao2/Content/Buffs/HomaPickaxeBuff";
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if ( player.GetModPlayer<BooTaoPlayer>().GetMagnet2Config() ) {
				player.GetModPlayer<BooTaoPlayer>().Magnet2 = true;
			}
		}
	}
}
/*
https://docs.tmodloader.net/docs/stable/class_player.html
*/