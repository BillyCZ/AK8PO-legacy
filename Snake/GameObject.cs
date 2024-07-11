public class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ConsoleColor Color { get; set; }
        private string direction = "RIGHT";

        public GameObject(int x, int y, ConsoleColor color)
        {
            X = x;
            Y = y;
            Color = color;
        }

        public void UpdateDirection(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow when direction != "DOWN":
                    direction = "UP";
                    break;
                case ConsoleKey.DownArrow when direction != "UP":
                    direction = "DOWN";
                    break;
                case ConsoleKey.LeftArrow when direction != "RIGHT":
                    direction = "LEFT";
                    break;
                case ConsoleKey.RightArrow when direction != "LEFT":
                    direction = "RIGHT";
                    break;
            }
        }

        public void Move()
        {
            switch (direction)
            {
                case "UP":
                    Y--;
                    break;
                case "DOWN":
                    Y++;
                    break;
                case "LEFT":
                    X--;
                    break;
                case "RIGHT":
                    X++;
                    break;
            }
        }
    }