using System;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceShooter
{
    class Enemy : GameObject
    {
        float dx = 2;
        float dy;
        float fireTimer;
        Random rng;

        public List<Bullet> Bullets = new List<Bullet>();

        public Enemy(float x, float y, Random r)
        {
            X = x;
            Y = y;
            Width = 36;
            Height = 30;
            rng = r;
            dy = 0.5f + (float)r.NextDouble();
            fireTimer = r.Next(60, 180);
        }

        public override void Update()
        {
            X += dx;
            Y += dy;

            if (X <= 0 || X >= 800 - Width)
                dx *= -1;

            fireTimer--;
            if (fireTimer <= 0)
            {
                Bullets.Add(new Bullet(X + Width / 2 - 2, Y + Height, 5, Color.OrangeRed));
                fireTimer = rng.Next(60, 180);
            }

            foreach (var b in Bullets)
                b.Update();

            Bullets.RemoveAll(b => !b.Active);
        }

        public override void Draw(Graphics g)
        {
            PointF[] pts = new PointF[]
            {
                new PointF(X + Width / 2, Y + Height),
                new PointF(X + Width, Y),
                new PointF(X + Width / 2, Y + 8),
                new PointF(X, Y)
            };

            using (var br = new SolidBrush(Color.LimeGreen))
                g.FillPolygon(br, pts);

            using (var r = new SolidBrush(Color.Red))
            {
                g.FillEllipse(r, X + 8, Y + 6, 6, 6);
                g.FillEllipse(r, X + Width - 14, Y + 6, 6, 6);
            }

            foreach (var b in Bullets)
                b.Draw(g);
        }
    }
}
