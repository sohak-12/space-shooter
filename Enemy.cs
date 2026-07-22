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
        static Image enemyImg;

        static Enemy()
        {
            try { enemyImg = Image.FromFile(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Assets", "Enemies.png")); }
            catch { enemyImg = null; }
        }

        public List<Bullet> Bullets = new List<Bullet>();

        public Enemy(float x, float y, Random r)
        {
            X = x;
            Y = y;
            Width = 50;
            Height = 50;
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
                Bullets.Add(new Bullet(X + Width / 2 - 2, Y + Height, 5, Color.Yellow));
                fireTimer = rng.Next(60, 180);
            }

            foreach (var b in Bullets)
                b.Update();

            Bullets.RemoveAll(b => !b.Active);
        }

        public override void Draw(Graphics g)
        {
            if (enemyImg != null)
            {
                g.DrawImage(enemyImg, X, Y, Width, Height);
            }
            else
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
            }

            foreach (var b in Bullets)
                b.Draw(g);
        }
    }
}
