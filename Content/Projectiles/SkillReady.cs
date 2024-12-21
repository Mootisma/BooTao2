using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Projectiles
{
    public class SkillReady : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 61;
            Projectile.height = 61;
            Projectile.timeLeft = 10;
            Projectile.penetrate = -1;
            Projectile.hide = false;
            Projectile.alpha = 0;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
			Projectile.scale = 0.7f;
        }
        int idlePause;
        bool floatUpOrDown; //false is Up, true is Down
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if ((player.dead && !player.active) || !player.GetModPlayer<BooTaoPlayer>().SkillReady)
            {
                Projectile.Kill();
            }
            Projectile.timeLeft = 10;

            Projectile.position.X = player.Center.X - 30;
            Projectile.position.Y = player.Center.Y - 90;
        }
    }
}
