using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
    class Program
    {
        public const int MapHeight = 20;
        public const int MapWidth = 30;

        public const int ScrenHeight = MapHeight * 3;
        public const int ScrenWidth = MapWidth * 3;

        public const int Fps = 200; 

        public const ConsoleColor BorderColor = ConsoleColor.Green;
        public const ConsoleColor HeadColor = ConsoleColor.DarkBlue;
        public const ConsoleColor BodyColor = ConsoleColor.Blue;
        public const ConsoleColor FoodColor = ConsoleColor.Red;

        static void Main(string[] args)
        {
            Console.SetWindowSize(ScrenWidth, ScrenHeight);
            Console.SetBufferSize(ScrenWidth, ScrenHeight);
            Console.CursorVisible = false;
            
            while(true)
            {
                StartGame();

                Thread.Sleep(300);
            }
;
        }

        /// <summary>
        /// Main Logic Method 
        /// </summary>
        static void StartGame()
        {
            Console.Clear();

            DrawBorder();

            var snake = new Snake(5, 5, HeadColor, BodyColor);

            int score = 0;

            Direction direction = Direction.Right;

            Pixel food = GenFood(snake);
            food.Draw();

            Stopwatch sw = new Stopwatch();

            while (true)
            {
                sw.Restart();

                DrawScore(score);

                Direction oldDirection = direction;
                while (sw.ElapsedMilliseconds <= Fps)
                {
                    if (direction == oldDirection)
                    {
                        direction = Movment(direction);
                    }

                }

                if(snake.Head.X == food.X && snake.Head.Y == food.Y)
                {
                    snake.Move(direction, true);

                    food = GenFood(snake);
                    food.Draw();

                    score++;
                }
                else
                {
                    snake.Move(direction);
                }
                

                if (snake.Head.X == MapWidth - 1
                    || snake.Head.X == 0
                    || snake.Head.Y == MapHeight - 1
                    || snake.Head.Y == 0
                    || snake.Body.Any(b => b.X == snake.Head.X && b.Y == snake.Head.Y))
                {
                    break;
                }
            }

            snake.Clear();

            Console.SetCursorPosition(ScrenWidth / 3, ScrenHeight / 2);
            Console.WriteLine($"Game over {score}");
        }

        /// <summary>
        /// Draw score
        /// </summary>
        /// <param name="score"></param>
        static void DrawScore(int score)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write($"Score: {score}" );
        }

        /// <summary>
        /// Generate Food
        /// </summary>
        /// <param name="snake"></param>
        /// <returns>Food`s Pixel</returns>
        static Pixel GenFood(Snake snake)
        {
            Pixel food;
            Random random = new Random();   
            do
            {
                food = new Pixel(random.Next(1, MapWidth - 2), random.Next(1, MapHeight - 2), FoodColor);
            } while (snake.Head.X == food.X && snake.Head.Y == food.Y
                        || snake.Body.Any(b => b.X == food.X || b.Y == food.Y));

            return food;
        }
        /// <summary>
        /// defines snakes direction
        /// </summary>
        /// <param name="currentDirection"></param>
        /// <returns>Direction of snake</returns>
        static Direction Movment(Direction currentDirection)
        {
            if(!Console.KeyAvailable)
            {
                return currentDirection;
            }

            ConsoleKey key = Console.ReadKey(true).Key;

            currentDirection = key switch 
            {
                ConsoleKey.W when currentDirection  != Direction.Down => Direction.Up,
                ConsoleKey.S when currentDirection != Direction.Up => Direction.Down,
                ConsoleKey.D when currentDirection != Direction.Left => Direction.Right,
                ConsoleKey.A when currentDirection != Direction.Right => Direction.Left,
                _ => currentDirection
            };

            return currentDirection;
        }

        /// <summary>
        /// Draw Border 
        /// </summary>
        static void DrawBorder()
        {
            for (int i = 0; i < MapWidth; i++)
            {
                new Pixel(i, 1, BorderColor).Draw();
                new Pixel(i, MapHeight - 1, BorderColor).Draw();
            }
            for (int i = 1; i < MapHeight; i++)
            {
                new Pixel(0, i, BorderColor).Draw();
                new Pixel(MapWidth - 1, i, BorderColor).Draw();
            }
        }
    }
}