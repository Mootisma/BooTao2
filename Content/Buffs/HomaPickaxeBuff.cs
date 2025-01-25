using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace BooTao2.Content.Buffs
{
	public class HomaPickaxeBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.GetModPlayer<BooTaoPlayer>().HomaPickaxes[0] == true){
				player.GetModPlayer<BooTaoPlayer>().Magnet = true;
			}
			if (player.GetModPlayer<BooTaoPlayer>().HomaPickaxes[1] == true){
				player.wallSpeed += 60;
				player.tileSpeed += 60;
				player.pickSpeed -= 0.35f;
				Lighting.AddLight(player.position, 1f, 1f, 1f);
				player.nightVision = true;
			}
			if (player.GetModPlayer<BooTaoPlayer>().HomaPickaxes[2] == true){
				player.detectCreature = true;
				player.findTreasure = true;
				player.lifeRegen += 4;
				player.manaRegenBonus += 5;
			}
			if (player.GetModPlayer<BooTaoPlayer>().HomaPickaxes[3] == true){
				player.dontHurtCritters = true;
				player.accLavaFishing = true;
				player.accTackleBox = true;
				player.accOreFinder = true;
				player.accWeatherRadio = true;
				player.accThirdEye = true;
				player.accStopwatch = true;
				player.statDefense += 5;
				player.maxMinions += 1;
			}
			if (player.GetModPlayer<BooTaoPlayer>().HomaPickaxes[4] == true){
				player.longInvince = true;
				player.panic = true;
				player.manaFlower = true;
				player.pStone = true;
				player.jumpBoost = true;
				player.frogLegJumpBoost = true;
				player.autoJump = true;
				player.jumpSpeedBoost += 2f;
				player.luck += 0.05f;
			}
			if (player.GetModPlayer<BooTaoPlayer>().HomaPickaxes[5] == true){
				//https://docs.tmodloader.net/docs/stable/class_extra_jump.html
				//https://github.com/tModLoader/tModLoader/blob/57a888322fe33bd693c25f02894ba4b84c91301a/ExampleMod/Content/Items/Accessories/ExampleExtraJumpAccessory.cs#L31
				player.GetJumpState<SandstormInABottleJump>().Enable();
				player.GetJumpState<BlizzardInABottleJump>().Enable();
				player.GetJumpState<CloudInABottleJump>().Enable();
				player.noFallDmg = true;
				player.noKnockback = true;
			}
			if (player.GetModPlayer<BooTaoPlayer>().HomaPickaxes[6] == true){
				player.GetDamage(DamageClass.Generic) += 0.12f;
				if (player.velocity == Vector2.Zero){
					//player.GetModPlayer<BooTaoPlayer>().damageTest = 1;
					player.lifeRegen += 4;
					player.statDefense += 5;
					player.maxMinions += 1;
					player.maxTurrets += 1;// sentry minion
				}
				// else {}
			}
		}
	}
}
/*
https://docs.tmodloader.net/docs/stable/class_player.html
*/