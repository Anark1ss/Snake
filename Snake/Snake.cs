using System;
using System.Collections.Generic;


namespace Snake
{
    public class Snake
    {
        private readonly ConsoleColor _headColor;
        private readonly ConsoleColor _bodyColor;


        public Snake(int initialX, int initialY, ConsoleColor headColor, ConsoleColor bodyColor, int bodyLenth = 2)
        {
            _headColor = headColor;
            _bodyColor = bodyColor;
            Head = new Pixel(initialX, initialY, _headColor);
            
            for(int i = bodyLenth; i >= 0; i--)
            {
                Body.Enqueue(new Pixel(Head.X - i - 1, initialY, _bodyColor));
            }

            Draw();
        }


        /// <summary>
        /// Snake`s movement
        /// </summary>
        /// <param name="direction">Movement direction</param>
        /// <param name="eat"> Food Flag</param>
        public void Move(Direction direction, bool eat = false)
        {
            Clear();

            Body.Enqueue(new Pixel(Head.X, Head.Y, _bodyColor));

            if(!eat)
            {
                Body.Dequeue();
            }
           

            Head = direction switch
            {
                Direction.Down => new Pixel(Head.X, Head.Y + 1, _headColor),
                Direction.Up => new Pixel(Head.X, Head.Y - 1, _headColor),
                Direction.Right => new Pixel(Head.X + 1, Head.Y, _headColor),
                Direction.Left => new Pixel(Head.X - 1, Head.Y, _headColor),
                _ => Head
            };

            Draw();
        }

        public Pixel Head { get; private set; }
        public Queue<Pixel> Body { get; } = new Queue<Pixel>();
        public ConsoleColor BodyColor { get; }

        /// <summary>
        /// Draw Snake
        /// </summary>
        public void Draw()
        {
            Head.Draw();

            foreach(var pixel in Body)
            {
                pixel.Draw();
            }
        }

        /// <summary>
        /// Clear Snake
        /// </summary>
        public void Clear()
        {
            Head.Clear();

            foreach (var pixel in Body)
            {
                pixel.Clear();
            }
        }

    }
}
