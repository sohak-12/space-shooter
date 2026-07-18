using System.Drawing;

namespace SpaceShooter
{
    class Bullet : GameObject
    {
        float speed;
        Color color;

        public Bullet(float x, float y, float spd, Color col)
        {
            X = x;
            Y = y;
            Width = 4;
            Height = 12;
            speed = spd;
            color = col;
        }

        public override void Update()
        {
            Y += speed;

            if (Y < -20 || Y > 670)
                Active = false;
        }

        public override void Draw(Graphics g)
        {
            using (var b = new SolidBrush(color))
                g.FillRectangle(b, X, Y, Width, Height);
        }
    }
}
