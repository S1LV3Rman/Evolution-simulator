using System;
using UnityEngine;

namespace Source
{
    public static partial class GridMath
    {
        private static int even(this int value) => value % 2 == 0 ? 1 : 0;
        private static int  odd(this int value) => value % 2 == 0 ? 0 : 1;
    

        
        public static void Move(ref this Vector3Int pos, Direction direction)
        {
            switch (direction)
            {
                case Direction.TopRight:    pos.MoveTopRight();    break;
                case Direction.Right:       pos.MoveRight();       break;
                case Direction.BottomRight: pos.MoveBottomRight(); break;
                case Direction.BottomLeft:  pos.MoveBottomLeft();  break;
                case Direction.Left:        pos.MoveLeft();        break;
                case Direction.TopLeft:     pos.MoveTopLeft();     break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
        
        public static void Move(ref this Vector3Int pos, Direction direction, int distance)
        {
            if (distance == 0) return;

            var reverse = distance < 0;

            switch (direction)
            {
                case Direction.TopRight:
                    if (reverse) pos.MoveBottomLeft(distance);
                    else pos.MoveTopRight(distance);
                    break;
                case Direction.Right:
                    if (reverse) pos.MoveLeft(distance);
                    else pos.MoveRight(distance);
                    break;
                case Direction.BottomRight:
                    if (reverse) pos.MoveTopLeft(distance);
                    else pos.MoveBottomRight(distance);
                    break;
                case Direction.BottomLeft:
                    if (reverse)  pos.MoveTopRight(distance);
                    else pos.MoveBottomLeft(distance);
                    break;
                case Direction.Left:
                    if (reverse) pos.MoveRight(distance);
                    else pos.MoveLeft(distance);
                    break;
                case Direction.TopLeft:
                    if (reverse) pos.MoveBottomRight(distance);
                    else pos.MoveTopLeft(distance);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public static Vector3Int GetNeighbourCoord(Vector3Int coord, Direction direction)
        {
            return direction switch
            {
                Direction.TopRight => TopRight(coord),
                Direction.Right => Right(coord),
                Direction.BottomRight => BottomRight(coord),
                Direction.BottomLeft => BottomLeft(coord),
                Direction.Left => Left(coord),
                Direction.TopLeft => TopLeft(coord),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        public static Vector3Int GetPos(MapCoord globalCoord)
        {
            return GetPos(new Vector3Int(0, 0, globalCoord.Layer), globalCoord.Top, globalCoord.Bot);
        }

        public static Vector3Int GetPos(Vector3Int relativePos, int top, int bot)
        {
            return top == bot ? 
                Right(relativePos, bot) : 
                TopRight(BottomRight(relativePos, bot), top);
        }

        public static Vector3Int GetPos(Vector3Int relativePos, MapCoord localCoord)
        {
            return GetPos(relativePos, localCoord.Top, localCoord.Bot);
        }
    }
}