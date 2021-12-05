using System;
using UnityEngine;

namespace Source
{
    public struct MapCoord
    {
        public int Top { get; private set; }
        public int Bot { get; private set; }
        public int Layer { get; private set; }

        public MapCoord(int layer)
        {
            Top = 0;
            Bot = 0;
            Layer = layer;
        }

        public MapCoord(int top, int bot)
        {
            Top = top;
            Bot = bot;
            Layer = 0;
        }

        public MapCoord(int top, int bot, int layer)
        {
            Top = top;
            Bot = bot;
            Layer = layer;
        }

        public MapCoord(Vector3Int coord)
        {
            Top = coord.x;
            Bot = coord.y;
            Layer = coord.z;
        }


        public void InvertOverEdge(int edge)
        {
            Top = InvertOverEdge(Top, edge);
            Bot = InvertOverEdge(Bot, edge);
        }

        private int InvertOverEdge(int value, int edge)
        {
            if(value > 0)
            {
                var overstep = value - edge;
                if (overstep > 0)
                    value = overstep - edge - 1;
            }
            else
            {
                var overstep = value + edge;
                if (overstep < 0)
                    value = overstep + edge + 1;
            }

            return value;
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

        public Vector3Int GetPos()
        {
            return GridMath.GetPos(this);
        }

        public static MapCoord operator +(MapCoord a, MapCoord b) =>
            new MapCoord(a.Top + b.Top, a.Bot + b.Bot, a.Layer + b.Layer);
    }
}