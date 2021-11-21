namespace Source
{
    public struct MapCoord
    {
        public int Top { get; private set; }
        public int Bot { get; private set; }

        public MapCoord(int top, int bot)
        {
            Top = top;
            Bot = bot;
        }
        

        public void MoveTopRight(int distance = 1)
        {
            Top += distance;
        }

        public void MoveRight(int distance = 1)
        {
            Top += distance;
            Bot += distance;
        }

        public void MoveBottomRight(int distance = 1)
        {
            Bot += distance;
        }

        public void MoveBottomLeft(int distance = 1)
        {
            Top -= distance;
        }

        public void MoveLeft(int distance = 1)
        {
            Top -= distance;
            Bot -= distance;
        }

        public void MoveTopLeft(int distance = 1)
        {
            Bot -= distance;
        }
    }
}