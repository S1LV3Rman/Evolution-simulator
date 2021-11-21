using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source
{
    public static class PaintMath
    {
        public static Vector3Int[] GenerateStraightLine(Vector3Int startCord, int length, GridMath.Direction direction)
        {
            var positions = new Vector3Int[length];
            
            for (var i = 0; i < length; ++i)
            {
                positions[i] = startCord;
                startCord.Move(direction);
            }
            
            return positions;
        }

        public static Vector3Int[] GenerateWireCircle(Vector3Int centerCoord, int radius)
        {
            var positions = new Vector3Int[radius * 6];

            for (var i = 0; i < radius; ++i)
                centerCoord.Move(GridMath.Direction.Left);

            var n = 0;
            for (GridMath.Direction d = 0; d < (GridMath.Direction) 6; ++d)
                for (var i = 0; i < radius; ++i, ++n)
                {
                    positions[n] = centerCoord;
                    centerCoord.Move(d);
                }

            return positions;
        }

        public static Vector3Int[] GenerateFullCircle(Vector3Int centerCoord, int radius)
        {
            var area = radius * 3 * (radius + 1) + 1;
            var positions = new Vector3Int[area];

            if (radius > 0) positions[0] = centerCoord;

            var n = 1;
            for (var r = 1; r <= radius; ++r)
            {
                centerCoord.Move(GridMath.Direction.Left);
                for (GridMath.Direction d = 0; d < (GridMath.Direction) 6; ++d)
                    for (var i = 0; i < r; ++i, ++n)
                    {
                        positions[n] = centerCoord;
                        centerCoord.Move(d);
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

        public static void Put(Tilemap map, Vector3Int position, TileBase tileBase)
        {
            map.SetTile(position, tileBase);
        }
    }
}