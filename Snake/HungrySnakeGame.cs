using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace HungrySnake
{
    public partial class HungrySnakeGame : Form
    {
        public HungrySnake Snake;
        private Graphics graphics;
        private Bitmap doubleBuffer;
        private Stopwatch sw = new Stopwatch();
        static public Timer timer2 { get; set; }

        private Image backGround;
        private bool pause;

        public HungrySnakeGame()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            timer2 = new Timer();
            pause = true;
            timer2.Enabled = true;
            timer2.Interval = 160;
            backGround = Properties.Resources.playground1;
               

            Snake = new HungrySnake(30, 20, 5, 15);

            graphics = this.CreateGraphics();
            doubleBuffer = new Bitmap(800, 600);
            

            timer2.Tick += new EventHandler(timer2_Tick);

        }

        


        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up)
            {
                if (Snake.currentDirection == HungrySnake.DIRECTION.RIGHT || Snake.currentDirection == HungrySnake.DIRECTION.LEFT)
                    Snake.direction = HungrySnake.DIRECTION.UP;
            }
            else if (e.KeyData == Keys.Down)
            {
                if (Snake.currentDirection == HungrySnake.DIRECTION.RIGHT || Snake.currentDirection == HungrySnake.DIRECTION.LEFT)
                    Snake.direction = HungrySnake.DIRECTION.DOWN;
            }
            else if (e.KeyData == Keys.Left)
            {
                if (Snake.currentDirection == HungrySnake.DIRECTION.UP || Snake.currentDirection == HungrySnake.DIRECTION.DOWN)
                    Snake.direction = HungrySnake.DIRECTION.LEFT;
            }
            else if (e.KeyData == Keys.Right)
            {
                if (Snake.currentDirection == HungrySnake.DIRECTION.UP || Snake.currentDirection == HungrySnake.DIRECTION.DOWN)
                    Snake.direction = HungrySnake.DIRECTION.RIGHT;
            }
            else
                if(e.KeyCode == Keys.Escape)
            {
                 timer2.Stop();
                 sw.Stop();
                 DialogResult result = MessageBox.Show("Are you sure you want to close the game?", 
                     "Confirm close ", MessageBoxButtons.OKCancel);

                 if (result == DialogResult.OK) 
                 {
                     this.Close();

                 }
                 if(result == DialogResult.Cancel)
                 {
                     timer2.Start();
                     sw.Start();
                 }
                 
                
                
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(doubleBuffer);

            g.Clear(Color.LightCyan);
            g.DrawImage(backGround, 25, 50, 450, 300);
            
            Snake.Move();
            if (Snake.GameOver())
            {
                timer2.Stop();
                timer2.Dispose();
                DialogResult result = MessageBox.Show(string.Format("You Lose!\n You have {0} points for {1:0.00} seconds \n Do you want to play another one ?", Snake.points, (float)sw.Elapsed.TotalSeconds), "Do you want to play another game ? ", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    this.Close();
                    this.Hide();
                    HungrySnakeGame form2 = new HungrySnakeGame();
                    form2.ShowDialog();
                }
                if (result == DialogResult.No)
                {
                    timer2.Start();
                    sw.Start();
                    pause = false;
                    this.Close();
                }
            }

            Snake.Put(g);
            Snake.Draw(g);

            lblPoints.Text = Snake.points.ToString();
            lblTime.Text = string.Format("{0:0.00}", sw.Elapsed.TotalSeconds);

            graphics.DrawImageUnscaled(doubleBuffer, 0, 0);



        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {

            timer2.Stop();
            this.DialogResult = DialogResult.OK;

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            sw.Start();
        }


        private void Form2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
            {
                if (pause == false)
                {
                    timer2.Start();
                    sw.Start();
                    pause = true;
                }
                else
                {
                    timer2.Stop();
                    sw.Stop();
                    pause = false;
                }
            }
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            if (pause == false)
            {
                graphics.DrawImage(backGround, 25, 50, 450, 300);
                Snake.Draw(graphics);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}