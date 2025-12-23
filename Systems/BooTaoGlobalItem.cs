using BooTao2.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace BooTao2.Systems
{
	public class BooTaoGlobalItem : GlobalItem
    {
        public override void GrabRange(Item item, Player player, ref int grabRange)
		{
			if(player.GetModPlayer<BooTaoPlayer>().Magnet)
			{
				//player.GetModPlayer<BooTaoPlayer>().Magnet = true; //paste to accessory :)
				grabRange += 2000;
			}
		}
		
		//public override void PostUpdate(Item item) {
		//	item.velocity *= 2;
		//}
		
		public override bool GrabStyle(Item item, Player player) {
			if(player.GetModPlayer<BooTaoPlayer>().Magnet)
			{
				Vector2 movement = item.DirectionTo(player.Center) * 20f;
				item.velocity = movement;
				// item.velocity = Collision.TileCollision(item.position, item.velocity, item.width, item.height);
				return true;
			}
			return false;
		}
    }
}