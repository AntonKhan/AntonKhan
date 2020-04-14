using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Snake2
{
    class Program
    {
        static readonly int x = 60;
        static readonly int y = 25;

        static Walls walls;
        static Snake snake;
        static Food food;
        static Timer time;


        static void Main()
        {
            Console.WriteLine("Выберете сложность игры: 1 - легко, 2 - средне, 3 - сложно");
            int n = 0;
            n = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            if (n == 1)
            {
                int x1 = 10;
                int y1 = 10;
                walls = new Walls(x1, y1, '0');
                snake = new Snake(x1 / 2, y1 / 2, 3);
                food = new Food(x1, y1, '+');
            }
            if (n == 2)
            {
                int x1 = 15;
                int y1 = 15;
                walls = new Walls(x1, y1, '0');
                snake = new Snake(x1 / 2, y1 / 2, 3);

                food = new Food(x1, y1, '+');
            }
            if (n == 3)
            {
                int x1 = 20;
                int y1 = 20;
                walls = new Walls(x1, y1, '0');
                snake = new Snake(x1 / 2, y1 / 2, 3);
                food = new Food(x1, y1, '+');

            }
            Console.SetWindowSize(x + 1, y + 1);
            Console.SetBufferSize(x + 1, y + 1);
            Console.CursorVisible = false;
            food.CreateFood();

            switch (n)
            {
                case 1:
                    {
                        time = new Timer(Move, null, 0, 400);
                        break;
                    }
                case 2:
                    {
                        time = new Timer(Move, null, 0, 300);

                        break;
                    }
                case 3:
                    {
                        time = new Timer(Move, null, 0, 200);

                        break;
                    }
            }
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.Rotation(key.Key);
                }
            }
        }

        static void Move(object obj)
        {
            if (walls.IsHit(snake.GetHead()) || snake.IsHit(snake.GetHead()))
            {
                time.Change(0, Timeout.Infinite);
            }
            else if (snake.Eat(food.food))
            {
                food.CreateFood();
            }
            else
            {
                snake.Move();
            }
        }
    }
}

struct Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public char ch { get; set; }

        public static implicit operator Point((int, int, char) value) =>
              new Point { x = value.Item1, y = value.Item2, ch = value.Item3 };

        public static bool operator ==(Point a, Point b) =>
                (a.x == b.x && a.y == b.y) ? true : false;
        public static bool operator !=(Point a, Point b) =>
                (a.x != b.x || a.y != b.y) ? true : false;

        public void Draw()
        {
            DrawPoint(ch);
        }
        public void Clear()
        {
            DrawPoint(' ');
        }

        private void DrawPoint(char _ch)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(_ch);
        }
    }
    class Walls
    {
        private char ch;
        private List<Point> wall = new List<Point>();

        public Walls(int x, int y, char ch)
        {
            this.ch = ch;

            DrawHorizontal(x, 0);
            DrawHorizontal(x, y);
            DrawVertical(0, y);
            DrawVertical(x, y);
        }
        private void DrawHorizontal(int x, int y)
        {
            for (int i = 0; i < x; i++)
            {
                Point p = (i, y, ch);
                p.Draw();
                wall.Add(p);
            }
        }
        private void DrawVertical(int x, int y)
        {
            for (int i = 0; i < y; i++)
            {
                Point p = (x, i, ch);
                p.Draw();
                wall.Add(p);
            }
        }
        public bool IsHit(Point p)
        {
            foreach (var w in wall)
            {
                if (p == w)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
