using System;
using UnityEngine;

namespace Source
{
    public static class GridMath
    {
        private static Vector3Int BottomRight(Vector3Int pos) => new Vector3Int(pos.y % 2 == 0 ? pos.x : pos.x + 1, pos.y - 1, 0);
        private static Vector3Int Right(Vector3Int pos) =>       new Vector3Int(pos.x + 1, pos.y, 0);
        private static Vector3Int TopRight(Vector3Int pos) =>    new Vector3Int(pos.y % 2 == 0 ? pos.x : pos.x + 1, pos.y + 1, 0);
        private static Vector3Int BottomLeft(Vector3Int pos) =>  new Vector3Int(pos.y % 2 == 0 ? pos.x - 1 : pos.x, pos.y - 1, 0);
        private static Vector3Int Left(Vector3Int pos) =>        new Vector3Int(pos.x - 1, pos.y, 0);
        private static Vector3Int TopLeft(Vector3Int pos) =>     new Vector3Int(pos.y % 2 == 0 ? pos.x - 1 : pos.x, pos.y + 1, 0);
    

        public enum Neighbour
        {
            TopRight    = 0,
            Right       = 1,
            BottomRight = 2,
            BottomLeft  = 3,
            Left        = 4,
            TopLeft     = 5,
        }

        public static Vector3Int GetNeighbourCoord(Vector3Int coord, Neighbour neighbour)
        {
            return neighbour switch
            {
                Neighbour.TopRight => TopRight(coord),
                Neighbour.Right => Right(coord),
                Neighbour.BottomRight => BottomRight(coord),
                Neighbour.BottomLeft => BottomLeft(coord),
                Neighbour.Left => Left(coord),
                Neighbour.TopLeft => TopLeft(coord),
                _ => throw new ArgumentOutOfRangeException(nameof(neighbour), neighbour, null)
            };
        }
    }
}