using BooTao2.Content.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace BooTao2.Systems
{
	public class BooTaoGlobalProjectile : GlobalProjectile
	{
		public override bool InstancePerEntity => true;
		public bool test;
		public float speedMultiplier = 0.66f;
		private static float MostimaSlowA = 0.82f;
		private static float MostimaSlowB = 0.66f;
		private static float MostimaSlowC = 0.01f;
		public bool MostimaSlow1 = false;
		public bool MostimaSlow2 = false;
		public bool MostimaSlow3 = false;
		
		//public override bool PreAI(Projectile projectile) {
			//if (test) {
				//projectile.velocity *= speedMultiplier;
				//Vector2 ligmaballs = Vector2.Lerp(projectile.position, projectile.oldPos[1], 0.5f);
				//projectile.position -= ligmaballs;
			//}
			//return base.PreAI(projectile);
			//return true;//return base.PreAI(projectile);
		//}
		
		public override bool PreDraw (Projectile projectile, ref Color lightColor) {
			//if (projectile.hostile) {
				//projectile.velocity /= speedMultiplier;
			if (MostimaSlow1) {
				projectile.velocity /= MostimaSlowA;
				//projectile.netUpdate = true;
			}
			if (MostimaSlow2) {
				projectile.velocity /= MostimaSlowB;
			}
			if (MostimaSlow3) {
				projectile.velocity /= MostimaSlowC;
			}
			//}
			return base.PreDraw(projectile, ref lightColor);
		}
		public override void PostAI(Projectile projectile) {
			//if (projectile.hostile) {
				//projectile.velocity *= speedMultiplier;
			if (MostimaSlow1) {
				projectile.velocity *= MostimaSlowA;
			}
			if (MostimaSlow2) {
				projectile.velocity *= MostimaSlowB;
			}
			if (MostimaSlow3) {
				projectile.velocity *= MostimaSlowC;
			}
			//}
		}
		public override void OnSpawn(Projectile projectile, IEntitySource source) {
			//if (test) {
			//	projectile.velocity /= speedMultiplier;
			//}
		}
	}
}
/*
https://docs.tmodloader.net/docs/stable/class_projectile.html
https://docs.tmodloader.net/docs/stable/class_global_projectile.html
https://github.com/tModLoader/tModLoader/blob/1.4.5/ExampleMod/Common/GlobalProjectiles/ExampleProjectileModifications.cs
https://github.com/tModLoader/tModLoader/blob/2e5a51dffe1bc51b59232dd10027fadc77a222fe/ExampleMod/Content/Items/Weapons/ExampleModifiedProjectilesItem.cs#L38

basic explanation of the concept behind slowing projectiles down
the order of operations (to my knowledge) are conceptually: predraw -> ai -> postai -> [position updated here]
preai() didnt work where predraw is for reasons i dont know
the idea is that the velocity is determined in ai() without interference, then reduced by a fraction in postai
position gets updated with the reduced velocity, then the velocity is restored to original value in predraw, then
ai() continues as normal and it has no idea it was even reduced.
*/