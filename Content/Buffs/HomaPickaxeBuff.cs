using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using BooTao2.Systems;

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
				if (player.GetModPlayer<BooTaoPlayer>().BuffClutter) {
					Lighting.AddLight(player.position, 1f, 1f, 1f);
					player.nightVision = true;
				}
				else {
					player.AddBuff(11, 30);// Shine potion
					player.AddBuff(12, 30);// Night Owl potion
				}
			}
			if (player.GetModPlayer<BooTaoPlayer>().HomaPickaxes[2] == true){
				if (player.GetModPlayer<BooTaoPlayer>().BuffClutter) {
					player.detectCreature = true;
					player.findTreasure = true;
					player.GetModPlayer<BooTaoPlayer>().HomaHealing1 = true;
					player.manaRegenBonus += 5;
				}
				else {
					player.AddBuff(17, 30);// Hunter potion
					player.AddBuff(9, 30);//  Spelunker potion
					player.AddBuff(87, 30);// Campfire buff
					player.AddBuff(158, 30);// Star in a Bottle buff
				}
			}
			if (player.GetModPlayer<BooTaoPlayer>().HomaPickaxes[3] == true){
				player.dontHurtCritters = true;
				//player.accLavaFishing = true;
				//player.accTackleBox = true;
				player.accOreFinder = true;// metal detector
				//player.accWeatherRadio = true;
				//player.accThirdEye = true;
				//player.accStopwatch = true;
				if (player.GetModPlayer<BooTaoPlayer>().BuffClutter) {
					player.statDefense += 5;
					player.maxMinions += 1;
					player.GetModPlayer<BooTaoPlayer>().HomaHealing2 = true;
				}
				else {
					player.AddBuff(215, 30);// Bast Statue buff
					player.AddBuff(150, 30);// Bewitching Table buff
					player.AddBuff(89, 30);//  Heart Lantern buff
				}
			}
			if (player.GetModPlayer<BooTaoPlayer>().HomaPickaxes[4] == true){
				/*
				player.longInvince = true;
				player.panic = true;
				player.manaFlower = true;
				player.pStone = true;
				player.jumpBoost = true;
				player.frogLegJumpBoost = true;
				player.autoJump = true;
				player.jumpSpeedBoost += 2f;
				player.luck += 0.05f;
				*/
				if (player.GetModPlayer<BooTaoPlayer>().BuffClutter) {
					player.ammoBox = true;
					player.GetArmorPenetration(DamageClass.Melee) += 12;
					
					player.GetDamage(DamageClass.Magic) += 0.05f;
					player.GetCritChance(DamageClass.Magic) += 2;
					player.statManaMax2 += 20;
					player.manaCost -= 0.02f;
				}
				else {
					player.AddBuff(93, 30);// Ammo Box buff
					player.AddBuff(29, 30);// Crystal Ball buff
					player.AddBuff(159, 30);// Sharpening Station buff
				}
			}
			if (player.GetModPlayer<BooTaoPlayer>().HomaPickaxes[5] == true){
				//https://docs.tmodloader.net/docs/stable/class_extra_jump.html
				//https://github.com/tModLoader/tModLoader/blob/57a888322fe33bd693c25f02894ba4b84c91301a/ExampleMod/Content/Items/Accessories/ExampleExtraJumpAccessory.cs#L31
				/*
				player.GetJumpState<SandstormInABottleJump>().Enable();
				player.GetJumpState<BlizzardInABottleJump>().Enable();
				player.GetJumpState<CloudInABottleJump>().Enable();
				player.noFallDmg = true;
				player.noKnockback = true;
				*/
				if (player.GetModPlayer<BooTaoPlayer>().BuffClutter) {
					player.maxTurrets += 1;
					player.moveSpeed += 0.2f;
					player.pickSpeed -= 0.2f;
				}
				else {
					player.AddBuff(348, 30);// War Table buff
					player.AddBuff(192, 30);// Slice of Cake buff
				}
			}
			if (player.GetModPlayer<BooTaoPlayer>().HomaPickaxes[6] == true){
				/*
				player.GetDamage(DamageClass.Generic) += 0.12f;
				if (player.velocity == Vector2.Zero){
					//player.GetModPlayer<BooTaoPlayer>().damageTest = 1;
					player.lifeRegen += 4;
					player.statDefense += 5;
					player.maxMinions += 1;
					player.maxTurrets += 1;// sentry minion
				}
				// else {}
				*/
			}
		}
	}
}
/*
https://docs.tmodloader.net/docs/stable/class_player.html
https://github.com/tModLoader/tModLoader/tree/1.4.5/ExampleMod
https://terraria.wiki.gg/wiki/Buffs
*/