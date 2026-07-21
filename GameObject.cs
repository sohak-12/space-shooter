using System.Drawing;

namespace SpaceShooter
{
    abstract class GameObject
    {
        public float X, Y;
        public int Width, Height;
        public bool Active = true;

        public RectangleF Bounds
        {
            get { return new RectangleF(X, Y, Width, Height); }
        }

        public bool CollidesWith(GameObject other)
        {
            if (!Active || !other.Active)
                return false;

            return Bounds.IntersectsWith(other.Bounds);
        }

        public abstract void Update();
        public abstract void Draw(System.Drawing.Graphics g);
    }
}
