using BooTao2.Content.Buffs;
using BooTao2.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace BooTao2.Content.Buffs.Serval {
	public class ServalBuff : ModBuff {
		public override string Texture => "Terraria/Images/Buff_" + 144;//electrified
		
		public override void SetStaticDefaults() {
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
		}

		public override void Update(Player player, ref int buffIndex) {
			player.GetModPlayer<BooTaoPlayer>().ServalDoT = true;
		}
		
		public override void Update(NPC npc, ref int buffIndex) {
			npc.GetGlobalNPC<BooTaoGlobalNPC>().ServalDoT = true;
		}
	}
}