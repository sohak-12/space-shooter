using System;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceShooter
{
    class Player : GameObject
    {
        public int Lives = 3;
        public int Score = 0;

        float shootCooldown = 0;
        float invTimer = 0;

        public List<Bullet> Bullets = new List<Bullet>();

        public Player(int screenW)
        {
            Width = 40;
            Height = 40;
            X = screenW / 2 - Width / 2;
            Y = 560;
        }

        public void HandleInput(bool left, bool right, bool up, bool down, bool shoot, int screenW, int screenH)
        {
            if (left)  X -= 5;
            if (right) X += 5;
            if (up)    Y -= 5;
            if (down)  Y += 5;

            if (X < 0) X = 0;
            if (X > screenW - Width) X = screenW - Width;
            if (Y < 0) Y = 0;
            if (Y > screenH - Height) Y = screenH - Height;

            shootCooldown--;
            if (shoot && shootCooldown <= 0)
            {
                Bullets.Add(new Bullet(X + Width / 2 - 2, Y - 10, -12, Color.Cyan));
                shootCooldown = 12;
            }
        }

        public bool IsInvincible()
        {
            return invTimer > 0;
        }

        public void Hit()
        {
            if (invTimer > 0) return;
            Lives--;
            invTimer = 120;
        }

        public override void Update()
        {
            if (invTimer > 0) invTimer--;

            foreach (var b in Bullets)
                b.Update();

            Bullets.RemoveAll(b => !b.Active);
        }

        public override void Draw(Graphics g)
        {
            if (invTimer > 0 && (invTimer % 10) < 5) return;

            PointF[] ship = new PointF[]
            {
                new PointF(X + Width / 2, Y),
                new PointF(X + Width, Y + Height),
                new PointF(X + Width / 2, Y + Height - 10),
                new PointF(X, Y + Height)
            };

            using (var brush = new SolidBrush(Color.DeepSkyBlue))
                g.FillPolygon(brush, ship);

            using (var engine = new SolidBrush(Color.OrangeRed))
                g.FillEllipse(engine, X + Width / 2 - 6, Y + Height - 6, 12, 10);

            foreach (var b in Bullets)
                b.Draw(g);
        }
    }
}
