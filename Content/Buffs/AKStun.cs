using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BooTao2.Systems;

namespace BooTao2.Content.Buffs
{
	public class AKStun : ModBuff
	{
		public override string Texture => "Terraria/Images/Buff_" + 149;
		
		public override void SetStaticDefaults() {
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
			Main.buffNoTimeDisplay[Type] = false; // The time remaining won't display on this buff
			//BuffID.Sets.BuffTimeIsExtendedWithGameDifficulty[Type] = true; // Makes higher game difficulties extend this buff's duration
		}

		public override void Update(NPC npc, ref int buffIndex) {
			npc.velocity *= 0f;
			npc.GetGlobalNPC<BooTaoGlobalNPC>().AKstun = true;
		}
		
		public override void Update(Player player, ref int buffIndex) {
			player.stoned = true;
			player.frozen = true;
			player.webbed = true;
			player.cursed = true;
			
			//player.SetCCed();
			player.controlJump = false;
			player.controlDown = false;
			player.controlLeft = false;
			player.controlRight = false;
			player.controlUp = false;
			player.controlUseItem = false;
			player.controlUseTile = false;
			player.controlThrow = false;
			player.gravDir = 1f;
			
			player.noItems = true;
			player.moveSpeed *= 0f;
			player.velocity = Vector2.Zero;
		}
	}
}
/*

https://github.com/tModLoader/tModLoader/blob/1.4.5/ExampleMod/Content/Buffs/ExampleCrowdControlledDebuff.cs
https://docs.tmodloader.net/docs/stable/class_player.html

*/