using System;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceShooter
{
    class Enemy : GameObject
    {
        float speedX = 2;
        float speedY;
        float shootTimer;
        Random rand;

        public List<Bullet> Bullets = new List<Bullet>();

        public Enemy(float x, float y, Random r)
        {
            X = x;
            Y = y;
            Width = 36;
            Height = 30;
            rand = r;
            speedY = 0.5f + (float)r.NextDouble();
            shootTimer = r.Next(60, 180);
        }

        public override void Update()
        {
            X += speedX;
            Y += speedY;

            if (X <= 0 || X >= 800 - Width)
                speedX *= -1;

            shootTimer--;
            if (shootTimer <= 0)
            {
                Bullets.Add(new Bullet(X + Width / 2 - 2, Y + Height, 5, Color.OrangeRed));
                shootTimer = rand.Next(60, 180);
            }

            foreach (var b in Bullets)
                b.Update();

            Bullets.RemoveAll(b => !b.Active);
        }

        public override void Draw(Graphics g)
        {
            PointF[] shape = new PointF[]
            {
                new PointF(X + Width / 2, Y + Height),
                new PointF(X + Width, Y),
                new PointF(X + Width / 2, Y + 8),
                new PointF(X, Y)
            };

            using (var brush = new SolidBrush(Color.LimeGreen))
                g.FillPolygon(brush, shape);

            using (var red = new SolidBrush(Color.Red))
            {
                g.FillEllipse(red, X + 8, Y + 6, 6, 6);
                g.FillEllipse(red, X + Width - 14, Y + 6, 6, 6);
            }

            foreach (var b in Bullets)
                b.Draw(g);
        }
    }
}
