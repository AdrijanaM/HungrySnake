using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using HungrySnake.Properties;

namespace HungrySnake
{
    public partial class Start : Form
    {
        int TogMove;
        int MValX;
        int MValY;
       
        public Start()
        {
            InitializeComponent();
            var music = Resources.track;
            //DYNAMiTE - Unreal Superhero III
            System.Media.SoundPlayer sp = new System.Media.SoundPlayer(music);

            sp.PlayLooping();
            pictureBox5.Hide();
          
            this.Bounds = Screen.PrimaryScreen.Bounds;
            this.FormBorderStyle = FormBorderStyle.None;
            btnNewGame.Top = (this.Height / 2) - btnNewGame.Height;
            btnNewGame.Left = (this.Width / 2) - btnNewGame.Width;
            pictureBox1.Left = (this.Width / 2) ;
            pictureBox1.Top = (this.Height / 2) - btnNewGame.Height - btnNewGame.Height/2 ;
            btnHowToPlay.Top = (this.Height / 2) + btnHowToPlay.Height/2;
            btnHowToPlay.Left = (this.Width / 2) - btnNewGame.Width ;
            pictureBox2.Top= (this.Height / 2) - (btnHowToPlay.Height/2) ;
            pictureBox2.Left = (this.Width / 2);
            pictureBox2.Hide();
            btnExit.Top = (this.Height / 2) +  2*btnExit.Height;
            btnExit.Left = (this.Width / 2) - btnExit.Width ;
            pictureBox3.Left= (this.Width / 2);
            pictureBox3.Top = (this.Height / 2) + (btnExit.Height);
            pictureBox1.Hide();
            pictureBox2.Hide();
            pictureBox3.Hide();
            pictureBox4.Left = this.Width -  2*pictureBox4.Width;
            pictureBox5.Left = this.Width - 2 * pictureBox4.Width;

        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {


            HungrySnakeGame form2 = new HungrySnakeGame();
            form2.ShowDialog();
            pictureBox1.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
           
            Close();
        }

        private void btnHowToPlay_Click(object sender, EventArgs e)
        {
            
            Instructions form3 = new Instructions();
            form3.ShowDialog();
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
          if(e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            }

        private void playground_MouseDown(object sender, MouseEventArgs e)
        {
            TogMove = 1;
            MValX = e.X;
            MValY = e.Y;

        }

        private void playground_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }

        private void playground_MouseMove(object sender, MouseEventArgs e)
        {
            if(TogMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MValX, MousePosition.Y - MValY);

            }
        }


        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
        
            HungrySnakeGame form2 = new HungrySnakeGame();
            form2.ShowDialog();
        }

        private void btnNewGame_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Show();
            pictureBox2.Hide();
            pictureBox3.Hide();
           
        }

         private void pictureBox2_Click(object sender, EventArgs e)
        {

            Instructions form3 = new Instructions();
            form3.ShowDialog();
        }

        private void btnHowToPlay_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Hide();
            pictureBox3.Hide();
            pictureBox2.Show();
           
        }

        private void btnExit_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.Show();
            pictureBox1.Hide();
            pictureBox2.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void playground_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            var music = Resources.track;
            System.Media.SoundPlayer sp = new System.Media.SoundPlayer(music);
            sp.Stop();
            pictureBox5.Show();
           
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            var music = Resources.track;
            System.Media.SoundPlayer sp = new System.Media.SoundPlayer(music);
            sp.PlayLooping();
            pictureBox5.Hide();
           
        }
    }
    }
