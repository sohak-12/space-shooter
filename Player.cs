using System;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceShooter
{
    class Player : GameObject
    {
        public int Lives = 3;
        public int Score = 0;

        float shotCooldown = 0;
        float iFrames = 0;

        public List<Bullet> Bullets = new List<Bullet>();

        Image shipImg;

        public Player(int sw)
        {
            Width = 60;
            Height = 75;
            X = sw / 2 - Width / 2;
            Y = 560;

            try { shipImg = Image.FromFile("Assets\\Rocket.jpeg"); }
            catch { shipImg = null; }
        }

        public void HandleInput(bool left, bool right, bool up, bool down, bool fire, int sw, int sh)
        {
            if (left) X -= 5;
            if (right) X += 5;
            if (up) Y -= 5;
            if (down) Y += 5;

            if (X < 0) X = 0;
            if (Y < 0) Y = 0;
            if (X > sw - Width) X = sw - Width;
            if (Y > sh - Height) Y = sh - Height;

            shotCooldown--;
            if (fire && shotCooldown <= 0)
            {
                Bullets.Add(new Bullet(X + Width / 2 - 10, Y - 10, -12, Color.Cyan, true));
                shotCooldown = 12;
            }
        }

        public void Hit()
        {
            if (iFrames > 0) return;
            Lives--;
            iFrames = 120;
        }

        public override void Update()
        {
            if (iFrames > 0) iFrames--;

            foreach (var b in Bullets)
                b.Update();

            Bullets.RemoveAll(b => !b.Active);
        }

        public override void Draw(Graphics g)
        {
            if (iFrames > 0 && (iFrames % 10) < 5)
                return;

            if (shipImg != null)
            {
                g.DrawImage(shipImg, X, Y, Width, Height);
            }
            else
            {
                PointF[] pts = new PointF[]
                {
                    new PointF(X + Width / 2, Y),
                    new PointF(X + Width, Y + Height),
                    new PointF(X + Width / 2, Y + Height - 10),
                    new PointF(X, Y + Height)
                };

                using (var br = new SolidBrush(Color.DeepSkyBlue))
                    g.FillPolygon(br, pts);
            }

            foreach (var b in Bullets)
                b.Draw(g);
        }
    }
}
