using Terraria;
using Terraria.ModLoader;
using BooTao2.Systems;

namespace BooTao2.Content.Pets.Breeze
{
	public class BreezePetBuff : ModBuff
	{
		public override void SetStaticDefaults() {
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) { // This method gets called every frame your buff is active on your player.
			bool unused = false;
			player.BuffHandle_SpawnPetIfNeededAndSetTime(buffIndex, ref unused, ModContent.ProjectileType<BreezePetProj>());
			
			player.GetModPlayer<BooTaoPlayer>().BreezeHealing = true;
		}
	}
}