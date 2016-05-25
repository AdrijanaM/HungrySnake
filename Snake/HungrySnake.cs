using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HungrySnake
{
    public class HungrySnake
    {
        private Image fish;
        private Image frog;
        public int X { get; set; }
        public int Y { get; set; }

        public Random random;
        public int foodX { get; set; }
        public int foodY { get; set; }

        public int height { get; set; }
        public int weight { get; set; }

        public Brush color { get; set; }
        public Rectangle[] square { get; set; }
        public int size { get; set; }
        public int snakeLength { get; set; }

        public static readonly int upBound = 50;
        public static readonly int leftBound = 25;


        public Pen pen { get; set; }

        public enum DIRECTION
        {
            UP, DOWN, RIGHT, LEFT
        }
        public DIRECTION direction { get; set; }
        public DIRECTION currentDirection { get; set; }

        public Rectangle food { get; set; }

        public int points { get; set; }
        public HungrySnake(int h, int w, int length, int s)
        {
            fish = Properties.Resources.fish;
            frog = Properties.Resources.frog;
            points = 0;
            this.height = h;
            this.weight = w;
            this.snakeLength = length;
            this.size = s;
            this.random = new Random();
            this.foodX = this.random.Next(this.height) * this.size + leftBound;
            this.foodY = this.random.Next(this.weight) * this.size + upBound ;
            this.food = new Rectangle(this.foodX, this.foodY, this.size, this.size);
            this.X = leftBound;
            this.Y = upBound;

            this.color = new SolidBrush(Color.Peru);
            this.direction = DIRECTION.RIGHT;

            this.pen = new Pen(Color.White, 3);
            this.square = new Rectangle[100];

            for (int i = 0; i < this.snakeLength; i++)
            {
               
                
                this.square[i] = new Rectangle(X, Y, this.size - 1, this.size - 1);
            }
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.DarkRed),this.square[0]);
            
            for (int i = 1; i < this.snakeLength - 1; i++)
            {
              
                g.FillRectangle(this.color, this.square[i]);
            }
            g.DrawRectangle(this.pen, leftBound, upBound, this.height * this.size, this.weight * this.size);
           this.currentDirection = this.direction;   
        }

        public void Move()
        {
            if (this.direction == DIRECTION.LEFT)
            {
                if (this.X <= leftBound)
                {
                    this.X = this.size * this.height + leftBound - this.size;
                }
                else
                    this.X -= this.size;
            }
            else if (this.direction == DIRECTION.DOWN)
            {
                if (this.Y >= this.size * this.weight + upBound - this.size)
                {
                    this.Y = upBound;
                }
                else
                    this.Y += this.size;
            }
            else if (this.direction == DIRECTION.UP)
            {
                if (Y <= upBound)
                {
                    this.Y = this.size * this.weight + upBound - this.size;
                }
                else
                    this.Y -= this.size;
            }
            else if (this.direction == DIRECTION.RIGHT)
            {
                if (this.X >= this.size * this.height + leftBound - this.size)
                {
                    this.X = leftBound;
                }
                else
                    this.X += this.size;
            }

            for (int i = this.snakeLength - 1; i >= 1; i--)
            {
                this.square[i] = this.square[i - 1];
            }

            this.square[0].X = this.X;
            this.square[0].Y = this.Y;
        }

        public Rectangle Calculate()
        {
            bool tmp = true;
            while (tmp == true)
            {
                this.foodX = this.random.Next(this.height);
                this.foodY = this.random.Next(this.weight);
                tmp = false;

                for (int i = 0; i < this.snakeLength; i++)
                {
                    if ((foodX * this.size + leftBound == square[i].X) && (foodY * this.size + upBound == square[i].Y))
                    {
                        tmp = true;
                        break;
                    }
                }
            }

            return new Rectangle(foodX * this.size + leftBound, foodY * this.size + upBound, this.size , this.size); ;
        }

        public bool Check(bool zname)
        {
            if ((this.square[0].X == this.food.X) && (this.square[0].Y == this.food.Y))
            {
                if (!zname)
                {
                    points += 10;
                }
                else
                {
                    points += 20;
                }
                if (HungrySnakeGame.timer2.Interval >= 110)
                {
                    HungrySnakeGame.timer2.Interval -= 10;
                }
                else if (HungrySnakeGame.timer2.Interval >= 90)
                {
                    HungrySnakeGame.timer2.Interval -= 5;
                }
                else if (HungrySnakeGame.timer2.Interval >= 70)
                {
                    HungrySnakeGame.timer2.Interval -= 2;
                }
                else if (HungrySnakeGame.timer2.Interval >= 40)
                {
                    HungrySnakeGame.timer2.Interval -= 1;
                }
                else if (HungrySnakeGame.timer2.Interval >= 20)
                {
                    HungrySnakeGame.timer2.Interval = HungrySnakeGame.timer2.Interval;
                }

                this.snakeLength++;
                this.square[snakeLength - 1] = new Rectangle(X, Y, this.size, this.size);
                return true;
            }
            return false;
        }
        bool zname = true;
        public void Put(Graphics g)
        {

            if (Check(zname))
            {
                this.food = Calculate();
                Random R = new Random();
                var rand = R.Next(1, 5);

                if (rand == 4 || rand == 5)
                {
                    g.DrawImage(fish, this.food);
                    zname = false;
                }
                else
                {
                    zname = true;
                }
            }
            if (zname)
            {
                g.DrawImage(frog, this.food);
            }
            else
            {
                g.DrawImage(fish, this.food);
            }

        }

        public bool GameOver()
        {
            for (int i = 1; i < this.snakeLength; i++)
            {
                if ((this.square[0].X == this.square[i].X) && (this.square[0].Y == this.square[i].Y))
                {
                    return true;
                }
            }
            return false;
        }

    }
}