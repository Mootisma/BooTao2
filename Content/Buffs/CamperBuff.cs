using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using BooTao2.Systems;

namespace BooTao2.Content.Buffs
{
	public class CamperBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<BooTaoPlayer>().Camper = true;
			player.GetModPlayer<BooTaoPlayer>().Camper2 = true;
			if( player.velocity == Vector2.Zero ){
				player.maxMinions += 2;
				player.maxTurrets += 2;// sentry minion
				player.longInvince = true;
			}
		}
	}
}
/*
https://docs.tmodloader.net/docs/stable/class_player.html
https://docs.tmodloader.net/docs/stable/class_mod_player.html

*/