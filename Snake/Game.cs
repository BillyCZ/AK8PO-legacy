using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake
{
    public class Game
    {
        private const int InitialScore = 0;
        private const int GameDelay = 350; // Milliseconds
        private const ConsoleColor BorderColor = ConsoleColor.White;
        private const ConsoleColor BerryColor = ConsoleColor.Red;
        private const ConsoleColor BodyColor = ConsoleColor.Green;

        private const ConsoleColor HeadColor = ConsoleColor.White;

        private int screenWidth = 32;
        private int screenHeight = 16;
        private Random random = new Random();
        private int score = InitialScore;
        private bool gameOver = false;
        private GameObject gameObject;
        private List<int> bodyXPositions = new List<int>();
        private List<int> bodyYPositions = new List<int>();
        private int foodX;
        private int foodY;

        public void Initialize()
        {
            Console.WindowHeight = screenHeight;
            Console.WindowWidth = screenWidth;
            gameObject = new GameObject(screenWidth / 2, screenHeight / 2, HeadColor);
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
                checkGameOver();
                checkFoodCapture();
                RenderFood();
                RenderSnakeHead();
                RenderSnakeBody();
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


        private void RenderObject()
        {
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

        private void checkGameOver()
        {
            if (gameObject.XPosition == 0 || gameObject.XPosition == screenWidth - 1 ||
                gameObject.YPosition == 0 || gameObject.YPosition == screenHeight - 1)
                gameOver = true;

            if (bodyXPositions.Contains(gameObject.XPosition) && bodyYPositions.Contains(gameObject.YPosition))
                gameOver = true;
        }

        private void checkFoodCapture()
        {

            if (gameObject.XPosition == foodX && gameObject.YPosition == foodY)
            {
                score++;
                foodX = random.Next(1, screenWidth - 2);
                foodY = random.Next(1, screenHeight - 2);
            }
        }



        private void RenderFood()
        {
            Console.SetCursorPosition(foodX, foodY);
            Console.ForegroundColor = BerryColor;
            RenderObject();
        }

        private void RenderSnakeBody()
        {
            foreach (var position in bodyXPositions.Zip(bodyYPositions, (x, y) => new { X = x, Y = y }))
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.ForegroundColor = BodyColor;
                RenderObject();
            }

        }

        private void RenderSnakeHead()
        {
            Console.SetCursorPosition(gameObject.XPosition, gameObject.YPosition);
            Console.ForegroundColor = gameObject.Color;
            RenderObject();
        }

        private void UpdateSnakePosition()
        {
            bodyXPositions.Add(gameObject.XPosition);
            bodyYPositions.Add(gameObject.YPosition);
            gameObject.ChangePosition();

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
                gameObject.UpdateDirection(key);
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