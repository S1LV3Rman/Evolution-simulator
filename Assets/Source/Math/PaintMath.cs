using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source
{
    public static class PaintMath
    {
        public static Vector3Int[] GenerateStraightLine(Vector3Int startCord, int length, GridMath.Neighbour direction)
        {
            var positions = new Vector3Int[length];
            
            for (var i = 0; i < length; ++i)
            {
                positions[i] = i == 0 ? startCord : GridMath.GetNeighbourCoord(positions[i - 1], direction);
            }
            
            return positions;
        }

        public static Vector3Int[] GenerateWireCircle(Vector3Int centerCoord, int radius)
        {
            var positions = new Vector3Int[radius * 6];

            var currentPoint = centerCoord;
            for (var i = 0; i < radius; ++i)
                currentPoint = GridMath.GetNeighbourCoord(currentPoint, GridMath.Neighbour.Left);

            var n = 0;
            for (GridMath.Neighbour d = 0; d < (GridMath.Neighbour) 6; ++d)
                for (var i = 0; i < radius; ++i, ++n)
                {
                    positions[n] = currentPoint;
                    currentPoint = GridMath.GetNeighbourCoord(currentPoint, d);
                }

            return positions;
        }

        public static Vector3Int[] GenerateFullCircle(Vector3Int centerCoord, int radius)
        {
            var area = radius * 3 * (radius + 1) + 1;
            var positions = new Vector3Int[area];

            if (radius > 0) positions[0] = centerCoord;
            
            var currentPoint = centerCoord;
            var n = 1;
            for (var r = 1; r <= radius; ++r)
            {
                currentPoint = GridMath.GetNeighbourCoord(currentPoint, GridMath.Neighbour.Left);
                for (GridMath.Neighbour d = 0; d < (GridMath.Neighbour) 6; ++d)
                    for (var i = 0; i < r; ++i, ++n)
                    {
                        positions[n] = currentPoint;
                        currentPoint = GridMath.GetNeighbourCoord(currentPoint, d);
                    }
            }

            return positions;
        }

        public static void Fill(Tilemap map, Vector3Int[] positions, TileBase tileBase)
        {
            var tiles = new TileBase[positions.Length];

            for (var i = 0; i < tiles.Length; ++i)
                tiles[i] = tileBase;
            
            map.SetTiles(positions, tiles);
        }
    }
}