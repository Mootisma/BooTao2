using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace BooTao2.Content.Buffs.LaPluma
{
	public class LaPlumaBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<BooTaoPlayer>().LaPlumaLifeRegen = true;
			player.GetAttackSpeed(DamageClass.Melee) += player.GetModPlayer<BooTaoPlayer>().LaPlumaPassive;
			if (player.GetModPlayer<BooTaoPlayer>().LaPlumaPassive > 0.36f) {
				player.GetDamage(DamageClass.Melee) += 0.08f;
			}
			if (!player.GetModPlayer<BooTaoPlayer>().LaPlumaHolding) {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
/*
https://docs.tmodloader.net/docs/stable/class_player.html
*/