using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BooTao2.Content.Projectiles.BlackSwan
{
    public class BlackSwanHold : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 90;
            Projectile.height = 90;
            Projectile.timeLeft = 60;
            Projectile.penetrate = -1;
            Projectile.hide = false;
            Projectile.alpha = 255;
            Projectile.netImportant = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
			Projectile.scale = 0.7f;
        }
        int idlePause;
        bool floatUpOrDown; //false is Up, true is Down
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (!player.GetModPlayer<BooTaoPlayer>().BlackSwanHolding || (player.dead && !player.active))
            {
                Projectile.Kill();
            }
            Projectile.timeLeft = 10;

            player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, (player.Center -
                new Vector2(Projectile.Center.X, Projectile.Center.Y + DrawOriginOffsetY + 20)
                ).ToRotation() + MathHelper.PiOver2);

            Vector2 ownerMountedCenter = player.RotatedRelativePoint(player.MountedCenter, true);
            Projectile.direction = player.direction;

            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation = player.velocity.X * 0.05f;

            if (Projectile.spriteDirection == 1)
            {
                Projectile.position.X = player.Center.X;
            }
            else
            {
                Projectile.position.X = player.Center.X - 88;

            }
            Projectile.position.Y = ownerMountedCenter.Y - Projectile.height / 2 - 12;

            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 5;
            }
            UpdateMovement();
        }
        private void UpdateMovement()
        {
            if (floatUpOrDown)//Up
            {
                if (Projectile.ai[0] > 7)
                {
                    DrawOriginOffsetY++;
                    Projectile.ai[0] = 0;
                }
            }
            else
            {
                if (Projectile.ai[0] > 7)
                {
                    DrawOriginOffsetY--;
                    Projectile.ai[0] = 0;
                }
            }
            if (DrawOriginOffsetY > -10)
            {
                idlePause = 10;
                DrawOriginOffsetY = -10;
                floatUpOrDown = false;

            }
            if (DrawOriginOffsetY < -20)
            {
                idlePause = 10;
                DrawOriginOffsetY = -20;
                floatUpOrDown = true;

            }
            if (idlePause < 0)
            {
                if (DrawOriginOffsetY < -12 && DrawOriginOffsetY > -18)
                {
                    Projectile.ai[0] += 2;
                }
                else
                {
                    Projectile.ai[0]++;
                }
            }
            idlePause--;
        }
    }
}
