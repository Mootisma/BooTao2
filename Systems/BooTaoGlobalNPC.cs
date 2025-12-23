using BooTao2.Content.Buffs;
using BooTao2.Content.Buffs.BloodBlossomBuff;
using BooTao2.Content.Buffs.Aventurine;
using BooTao2.Content.Buffs.Escoffier;
using BooTao2.Content.Items.HuTao;
using BooTao2.Content.Items.RaidenShogun;
using BooTao2.Content.Items.Herta;
using BooTao2.Content.Projectiles.RaidenShogun;
using BooTao2.Content.Buffs.RaidenShogun;
using BooTao2.Content.Buffs.Entelechia;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace BooTao2.Systems
{
	public class BooTaoGlobalNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;
		public bool BloodBlossom;
		public int BloodBlossomDmg = 12;
		public bool tnspeepee;
		public int ThornsDOTstack = 0;
		public int ThornsDOTdmg = 0;
		public int ThornsDOTduration = 0;
		public bool test;
		public bool KafkaAKSleep;
		//
		public bool kfpeepee;
		public int KafkaDOTdmg = 0;
		public int KafkaDOTduration = 0;
		//
		public bool MostimaSlow1;
		public bool MostimaSlow2;
		public bool MostimaSlow3;
		//
		public bool bspeepee;
		public int Arcana = 0;
		public int bsTimer = 0;
		public int bsDmgDone = 0;
		public int bsDefDownDur = 0;
		public bool bsDefDebuff;
		//
		public bool EscoffierDebuff;
		//
		public bool entelechiadot;

		public override void ResetEffects(NPC npc) {
			BloodBlossom = false;
			test = false;
			KafkaAKSleep = false;
			bspeepee = false;
			kfpeepee = false;
			tnspeepee = false;
			EscoffierDebuff = false;
			bsDefDebuff = false;
			entelechiadot = false;
		}

		public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers) {
			if (BloodBlossom) {
				// For best results, defense debuffs should be multiplicative
				modifiers.Defense *= BloodBlossomBuff.DefenseMultiplier;
			}
			if (EscoffierDebuff) {
				modifiers.Defense *= EscoffierBuff.DefenseMultiplier;
			}
			if (bsDefDebuff) {
				modifiers.Defense *= 0.75f;
			}
		}

		public override void DrawEffects(NPC npc, ref Color drawColor) {
			// This simple color effect indicates that the buff is active
			if (BloodBlossom || ThornsDOTstack > 0 || test || bspeepee) {
				drawColor.G = 0;
			}
		}
		//https://docs.tmodloader.net/docs/stable/class_global_n_p_c.html
		public override bool PreAI(NPC npc) {
			if (KafkaAKSleep) {
				//Dust.NewDust(npc.Center, 0, 0, 34, 0, 0, 150, default, 1f);
				return false;
			}
			if (test && !npc.boss) {
				//https://discord.com/channels/103110554649894912/534215632795729922/1266852701870882826
				//https://discord.com/channels/103110554649894912/534215632795729922/1280133031839010908
				//Vector2 ligmaballs = Vector2.Lerp(npc.position, npc.oldPos[1], 1);
				//npc.position -= ligmaballs;
				
				//the current combination below slows the enemy by 50%, i *think*
				// in reverse (multiplying here and dividing in postai) it seems to speed them up
				npc.velocity /= 0.01f;
			}
			if (MostimaSlow1) {
				npc.velocity /= 0.01f;
			}
			bsTimer++;
			if (bsTimer >= 300) {
				if (Arcana > 1) {
					Arcana = 1;
				}
				bsTimer = 0;
			}
			if (bsDefDownDur > 0) {
				bsDefDownDur--;
			}
			return true;
		}
		
		public override void PostAI(NPC npc) {
			if (test && !npc.boss) {
				npc.velocity *= 0.01f;
			}
		}
		
		public override bool? CanBeHitByItem(NPC npc, Player player, Item item) {
			if (KafkaAKSleep)
				return false;
			return null;
		}
		
		public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile){
			if (KafkaAKSleep)
				return false;
			return null;
		}
		
		public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot){
			if (KafkaAKSleep)
				return false;
			return true;
		}
		
		public override void UpdateLifeRegen(NPC npc, ref int damage) {
			if (BloodBlossom) {
				if (npc.lifeRegen > 0) {
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 12 * 2;//BloodBlossomDmg;
			}
			if (tnspeepee ){//&& ThornsDOTstack > 0 && ThornsDOTduration > 0) {
				//ThornsDOTduration--;
				if (npc.lifeRegen > 0) {
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 135 * 2;//ThornsDOTdmg;
			}
			//else {
			//	ThornsDOTstack = 0;
			//	ThornsDOTduration = 0;
			//}
			if (kfpeepee){// && KafkaDOTduration > 0) {
				//KafkaDOTduration--;
				if (npc.lifeRegen > 0) {
					npc.lifeRegen = 0;
				}
				//if (KafkaDOTdmg < 30)
				npc.lifeRegen -= 75 * 2;
				//else
				//	npc.lifeRegen -= KafkaDOTdmg;
			}
			if (bspeepee) {
				if (npc.lifeRegen > 0) {
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 300 * 2;//(int)(bsDmgDone * (2.4 + 0.12 * Arcana));
			}
			if (entelechiadot) {
				if (npc.lifeRegen > 0) {
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 200 * 2;
			}
		}
		
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {
			if (npc.type == NPCID.WallofFlesh){
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HuTaoHoma>(), 40));
            }
			if (npc.type == NPCID.MeteorHead || npc.type == NPCID.AngryBones || npc.type == NPCID.DungeonSpirit){
				npcLoot.Add(ItemDropRule.Common(ItemID.Meteorite, 3, 1, 4));
			}
			if (npc.type == NPCID.MartianSaucer){
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RaidenShogunItem>(), 40));
			}
			if (npc.type == NPCID.IceElemental){
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HertaMinionItem>(), 40));
			}
		}
	}
}