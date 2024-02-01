using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public class Game : Form
{
    private Bitmap bmp;
    private Graphics g;
    private Parallax parallax;
    private FoodLanche foodLanche;
    private BreakImg breakImg;
    private Player player;
    private Pause pause;
    private bool moveLeft, moveRight, moveUp, moveDown;
    private bool isPaused = false;
    public string PlayerName { get; }
    private GameHUD gameHUD;

    public Game(string playerName)
    {
        PlayerName = playerName;

        this.WindowState = FormWindowState.Maximized;
        this.Text = "Dangerous Delivery";

        var pb = new PictureBox { Dock = DockStyle.Fill };
        var timer = new Timer { Interval = 1000 / 60, };

        gameHUD = new GameHUD(this);
        player = new Player(gameHUD);
        parallax = new Parallax();
        foodLanche = new FoodLanche(gameHUD);
        breakImg = new BreakImg(gameHUD);
        pause = new Pause();
        pause.ResumeGame += (sender, e) => ResumeGame();

        parallax.Layers.Add(new SkyLayer());
        parallax.Layers.Add(new CloudLayer(40));
        parallax.Layers.Add(new CityLayer(70));
        parallax.Layers.Add(new SlumLayer(110));
        parallax.Layers.Add(new CasasLayer(150));
        parallax.Layers.Add(new RuasLayer(210));
        parallax.Layers.Add(new CalcadasLayer(180));
        gameHUD.Player(playerName);
        parallax.Layers.Add(new RainLayer(10));

        this.Load += (sender, e) =>
        {
            bmp = new Bitmap(pb.Width, pb.Height);
            g = Graphics.FromImage(bmp);

            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.CompositingQuality = CompositingQuality.AssumeLinear;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;

            g.Clear(Color.Black);
            pb.Image = bmp;
            timer.Start();
        };

        timer.Tick += (sender, e) =>
        {
            if (!isPaused)
            {
                UpdatePlayerMovement();

                parallax.Draw(g);
                foodLanche.Draw(g);
                breakImg.Draw(g);
                player.Draw(g);
                gameHUD.Draw(g);
                pb.Refresh();
            }
        };

        this.KeyDown += (o, e) =>
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
            else if (!isPaused && e.KeyCode == Keys.P)
            {
                TogglePause();
            }
            else if (isPaused && e.KeyCode == Keys.P)
            {
                TogglePause();
            }
            else if (!isPaused)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        moveLeft = true;
                        break;
                    case Keys.D:
                        moveRight = true;
                        break;
                    case Keys.W:
                        moveUp = true;
                        break;
                    case Keys.S:
                        moveDown = true;
                        break;
                }
            }
        };

        this.KeyUp += (o, e) =>
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    moveLeft = false;
                    break;
                case Keys.D:
                    moveRight = false;
                    break;
                case Keys.W:
                    moveUp = false;
                    break;
                case Keys.S:
                    moveDown = false;
                    break;
            }
        };

        FormClosed += delegate
        {
            Application.Exit();
        };

        this.Controls.Add(pb);
    }

    private void UpdatePlayerMovement()
    {
        if (moveLeft) player.MoveLeft();
        if (moveRight) player.MoveRight();
        if (moveUp) player.MoveUp();
        if (moveDown) player.MoveDown();
    }

    private void ResumeGame()
    {
        isPaused = false;
        pause.HidePause();
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pause.ShowPause();
        }
        else
        {
            ResumeGame();
        }
    }
}
