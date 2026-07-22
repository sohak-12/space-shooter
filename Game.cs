using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceShooter
{
    public class Game : Form
    {
        const int W = 800;
        const int H = 650;

        Timer gameTimer = new Timer();
        Player player;
        List<Enemy> enemies = new List<Enemy>();
        List<float[]> stars = new List<float[]>();
        Random rng = new Random();
        Bitmap buffer;
        Graphics canvas;
        Image bgImg;

        int wave = 1;
        int spawnTick = 0;
        bool gameOver = false;

        bool left, right, up, down, shoot;

        public Game()
        {
            Text = "Space Shooter";
            ClientSize = new Size(W, H);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Color.Black;
            DoubleBuffered = true;

            buffer = new Bitmap(W, H);
            canvas = Graphics.FromImage(buffer);

            string assetsPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Assets");
            try { bgImg = Image.FromFile(System.IO.Path.Combine(assetsPath, "Background.jpg")); } catch { bgImg = null; }
            player = new Player(W);



            SpawnWave();

            gameTimer.Interval = 16;
            gameTimer.Tick += OnTick;
            gameTimer.Start();
        }

        void OnTick(object sender, EventArgs e)
        {
            GameUpdate();
            Invalidate();
        }

        void SpawnWave()
        {
            int count = 5 + wave * 2;
            for (int i = 0; i < count; i++)
                enemies.Add(new Enemy(rng.Next(50, W - 86), rng.Next(-200, -30), rng));
        }

        void GameUpdate()
        {
            if (gameOver) return;

            player.HandleInput(left, right, up, down, shoot, W, H);
            player.Update();

            spawnTick++;
            if (spawnTick > 300)
            {
                enemies.Add(new Enemy(rng.Next(50, W - 86), -40, rng));
                spawnTick = 0;
            }

            foreach (var e in enemies)
                e.Update();

            foreach (var bullet in player.Bullets)
            {
                foreach (var enemy in enemies)
                {
                    if (bullet.CollidesWith(enemy))
                    {
                        bullet.Active = false;
                        enemy.Active = false;
                        player.Score += 10;
                    }
                }
            }

            foreach (var enemy in enemies)
            {
                foreach (var bullet in enemy.Bullets)
                {
                    if (bullet.CollidesWith(player))
                    {
                        bullet.Active = false;
                        player.Hit();
                    }
                }

                if (enemy.CollidesWith(player))
                {
                    enemy.Active = false;
                    player.Hit();
                }

                if (enemy.Y > H)
                    enemy.Active = false;
            }

            enemies.RemoveAll(e => !e.Active);

            if (enemies.Count == 0)
            {
                wave++;
                SpawnWave();
            }

            if (player.Lives <= 0)
                gameOver = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (bgImg != null)
                canvas.DrawImage(bgImg, 0, 0, W, H);
            else
                canvas.Clear(Color.Black);

            if (!gameOver)
            {
                player.Draw(canvas);

                foreach (var enemy in enemies)
                    enemy.Draw(canvas);

                using (var font = new Font("Consolas", 14, FontStyle.Bold))
                    canvas.DrawString("Score: " + player.Score + "   Lives: " + player.Lives + "   Wave: " + wave, font, Brushes.White, 10, 10);
            }
            else
            {
                using (var big = new Font("Consolas", 36, FontStyle.Bold))
                using (var small = new Font("Consolas", 16))
                {
                    canvas.DrawString("GAME OVER", big, Brushes.Red, 220, 260);
                    canvas.DrawString("Score: " + player.Score, small, Brushes.White, 330, 320);
                    canvas.DrawString("Press R to play again", small, Brushes.Gray, 265, 355);
                }
            }

            e.Graphics.DrawImage(buffer, 0, 0);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left  || e.KeyCode == Keys.A) left  = true;
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D) right = true;
            if (e.KeyCode == Keys.Up    || e.KeyCode == Keys.W) up    = true;
            if (e.KeyCode == Keys.Down  || e.KeyCode == Keys.S) down  = true;
            if (e.KeyCode == Keys.Space) shoot = true;
            if (e.KeyCode == Keys.R && gameOver) Restart();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left  || e.KeyCode == Keys.A) left  = false;
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D) right = false;
            if (e.KeyCode == Keys.Up    || e.KeyCode == Keys.W) up    = false;
            if (e.KeyCode == Keys.Down  || e.KeyCode == Keys.S) down  = false;
            if (e.KeyCode == Keys.Space) shoot = false;
        }

        void Restart()
        {
            player.Lives = 3;
            player.Score = 0;
            player.X = W / 2 - player.Width / 2;
            player.Y = 560;
            player.Bullets.Clear();
            enemies.Clear();
            wave = 1;
            gameOver = false;
            SpawnWave();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                canvas.Dispose();
                buffer.Dispose();
                gameTimer.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
