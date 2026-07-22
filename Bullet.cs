using System.Drawing;

namespace SpaceShooter
{
    class Bullet : GameObject
    {
        float speed;
        Color clr;
        bool fromPlayer;

        static Image img;

        static Bullet()
        {
            try
            {
                img = Image.FromFile(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Assets", "Bullets.png"));
            }
            catch
            {
                img = null;
            }
        }

        public Bullet(float x, float y, float spd, Color c, bool playerShot = false)
        {
            X = x;
            Y = y;
            speed = spd;
            clr = c;
            fromPlayer = playerShot;

            if (fromPlayer)
            {
                Width = 40;
                Height = 40;
            }
            else
            {
                Width = 4;
                Height = 12;
            }
        }

        public override void Update()
        {
            Y += speed;

            if (Y < -20 || Y > 670)
                Active = false;
        }

        public override void Draw(Graphics g)
        {
            if (fromPlayer && img != null)
            {
                g.DrawImage(img, X, Y, Width, Height);
                return;
            }

            using (var b = new SolidBrush(clr))
                g.FillRectangle(b, X, Y, Width, Height);
        }
    }
}
