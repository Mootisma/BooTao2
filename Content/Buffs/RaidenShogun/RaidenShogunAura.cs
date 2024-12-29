using BooTao2.Content.Items.RaidenShogun;
using BooTao2.Content.Projectiles.RaidenShogun;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Buffs.RaidenShogun
{
	public class RaidenShogunAura : ModBuff
	{
		public override void SetStaticDefaults() {
			Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
			Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
		}

		public override void Update(Player player, ref int buffIndex) {
			//
			player.manaRegen += 4;
			//player.manaRegenBonus += 2;
			player.GetDamage(DamageClass.Generic) += 0.05f;// 5% dmg buff
			player.GetModPlayer<BooTaoPlayer>().RaidenShogunSkill = true;
		}
	}
}
