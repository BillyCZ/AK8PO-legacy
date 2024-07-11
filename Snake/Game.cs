using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake
{
     public class Game
    {
        private const int InitialScore = 0;
        private const int GameDelay = 500; // Milliseconds
        private const ConsoleColor BorderColor = ConsoleColor.White;
        private const ConsoleColor BerryColor = ConsoleColor.Red;
        private const ConsoleColor BodyColor = ConsoleColor.Green;

        private const ConsoleColor HeadColor = ConsoleColor.White;

        private int screenWidth = 32;
        private int screenHeight = 16;
        private Random random = new Random();
        private int score = InitialScore;
        private bool gameOver = false;
        private GameObject head;
        private List<int> bodyXPositions = new List<int>();
        private List<int> bodyYPositions = new List<int>();
        private int foodX;
        private int foodY;

        public void Initialize()
        {
            Console.WindowHeight = 16;
            Console.WindowWidth = 32;
            head = new GameObject(screenWidth / 2, screenHeight / 2, HeadColor);
            foodX = random.Next(1, screenWidth - 2);
            foodY = random.Next(1, screenHeight - 2);
            SetRenderVisible();
            DrawBorders();
        }

        public void Run()
        {
            while (!gameOver)
            {
                ClearPreviousFrame();
                ProcessCollisions();
                RenderBerry();
                DrawSnake();
                if (gameOver) break;
                UpdateSnakePosition();
                ProcessInput();
                System.Threading.Thread.Sleep(GameDelay);
            }
            DisplayGameOver();
        }

        private void SetRenderVisible()
        {
            Console.CursorVisible = false;
        }

        private void SetRenderInvisible()
        {
            Console.CursorVisible = true;
        }

        private void RenderObject(){
                Console.Write("■");
        }

        private void DrawBorders()
        {
            Console.ForegroundColor = BorderColor;
            for (int i = 0; i < screenWidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                RenderObject();

                Console.SetCursorPosition(i, screenHeight - 1);
                RenderObject();
            }
            for (int i = 0; i < screenHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                RenderObject();
                Console.SetCursorPosition(screenWidth - 1, i);
                RenderObject();
            }
        }

        private void ClearPreviousFrame()
        {
            Console.Clear();
            DrawBorders();
        }

        private void ProcessCollisions()
        {
            if (head.X == 0 || head.X == screenWidth - 1 || head.Y == 0 || head.Y == screenHeight - 1)
                gameOver = true;

            if (head.X == foodX && head.Y == foodY)
            {
                score++;
                foodX = random.Next(1, screenWidth - 2);
                foodY = random.Next(1, screenHeight - 2);
            }

            if (bodyXPositions.Contains(head.X) && bodyYPositions.Contains(head.Y))
                gameOver = true;
        }



        private void RenderBerry()
        {
            Console.SetCursorPosition(foodX, foodY);
            Console.ForegroundColor = BerryColor;
            RenderObject();
        }

        private void DrawSnake()
        {
            foreach (var pos in bodyXPositions.Zip(bodyYPositions, (x, y) => new { X = x, Y = y }))
            {
                Console.SetCursorPosition(pos.X, pos.Y);
                Console.ForegroundColor = BodyColor;
                RenderObject();
            }
            Console.SetCursorPosition(head.X, head.Y);
            Console.ForegroundColor = head.Color;
            RenderObject();
        }

        private void UpdateSnakePosition()
        {
            bodyXPositions.Add(head.X);
            bodyYPositions.Add(head.Y);
            head.Move();

            if (bodyXPositions.Count > score)
            {
                bodyXPositions.RemoveAt(0);
                bodyYPositions.RemoveAt(0);
            }
        }

        private void ProcessInput()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                head.UpdateDirection(key);
            }
        }

        private void DisplayGameOver()
        {
            SetRenderVisible();
            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2);
            Console.WriteLine("Game over, Score: " + score);
        }
    }

}