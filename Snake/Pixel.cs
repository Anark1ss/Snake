using System;


namespace Snake
{
    public struct Pixel
    {
        private const char PixelChar = '█';
        

        public Pixel(int x, int y, ConsoleColor color = ConsoleColor.White, int pixelsize = 3)
        {
            X = x;
            Y = y;
            Color = color;
            PixelSize = pixelsize;
        }
       
        public int X { get; }
        public int Y { get; }
        public int PixelSize { get; }
        public ConsoleColor Color { get; }

        /// <summary>
        /// Draw Pixel
        /// </summary>
        public void Draw()
        {
            Console.ForegroundColor = Color;
            for (int x = 0; x < PixelSize; x++)
            {
                for (int y = 0; y < PixelSize; y++)
                {
                    Console.SetCursorPosition(X * PixelSize + x, Y * PixelSize + y);
                    Console.Write(PixelChar);
                }
            }
        }

        /// <summary>
        /// Clear Pixel
        /// </summary>
        public void Clear()
        {
            for (int x = 0; x < PixelSize; x++)
            {
                for (int y = 0; y < PixelSize; y++)
                {
                    Console.SetCursorPosition(X * PixelSize + x, Y * PixelSize + y);
                    Console.Write(' ');
                }
            }
        }
    }
}
