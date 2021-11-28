using System;
using UnityEngine;

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


        public void Move(GridMath.Direction direction, int distance = 1)
        {
            switch (direction)
            {
                case GridMath.Direction.TopRight: MoveTopRight(distance); break;
                case GridMath.Direction.Right: MoveRight(distance); break;
                case GridMath.Direction.BottomRight: MoveBottomRight(distance); break;
                case GridMath.Direction.BottomLeft: MoveBottomLeft(distance); break;
                case GridMath.Direction.Left: MoveLeft(distance); break;
                case GridMath.Direction.TopLeft: MoveTopLeft(distance); break;
                default: throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
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