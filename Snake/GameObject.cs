public class GameObject
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public ConsoleColor Color { get; set; }
        private string direction = "RIGHT"; //initial direction

        public GameObject(int xpos, int ypos, ConsoleColor color)
        {
            XPosition = xpos;
            YPosition = ypos;
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

        public void ChangePosition()
        {
            switch (direction)
            {
                case "UP":
                    YPosition--;
                    break;
                case "DOWN":
                    YPosition++;
                    break;
                case "LEFT":
                    XPosition--;
                    break;
                case "RIGHT":
                    XPosition++;
                    break;
            }
        }
    }