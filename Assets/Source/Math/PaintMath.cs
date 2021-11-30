using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source
{
    public static class PaintMath
    {
        public static Vector3Int[] RayPoints(Vector3Int startCord, int length, GridMath.Direction direction)
        {
            var positions = new Vector3Int[length];
            
            for (var i = 0; i < length; ++i)
            {
                positions[i] = startCord;
                startCord.Move(direction);
            }
            
            return positions;
        }

        public static Vector3Int[] WireCirclePoints(Vector3Int centerCoord, int radius)
        {
            var positions = new Vector3Int[radius * 6];

            var n = 0;
            
            for (var b = 0; b <= radius; ++b)
                positions[n++] =  GridMath.GetPos(centerCoord, radius, b);
            
            for (var b = -radius; b <= 0; ++b)
                positions[n++] =  GridMath.GetPos(centerCoord, -radius, b);
            
            for (var t = 1 - radius; t < radius; ++t)
            {
                positions[n++] =  GridMath.GetPos(centerCoord, t, t > 0 ? t - radius : -radius);
                positions[n++] =  GridMath.GetPos(centerCoord, t, t > 0 ? radius : t + radius);
            }

            return positions;
        }

        public static Vector3Int[] FullCirclePoints(Vector3Int centerCoord, int radius)
        {
            var area = radius * 3 * (radius + 1) + 1;
            var positions = new Vector3Int[area];

            var n = 0;
            
            for (var t =  -radius; t <= radius; ++t)
            {
                var min = t > 0 ? t - radius : -radius;
                var max = t > 0 ? radius : t + radius;
                for (var b = min; b <= max; ++b)
                    positions[n++] =  GridMath.GetPos(centerCoord, t, b);
            }

            return positions;
        }

        public static Vector3Int[] RhombPoints(Vector3Int centerPos, int radius)
        {
            var area = 4 * radius * (radius + 1) + 1;
            var positions = new Vector3Int[area];

            var n = 0;
            
            for (var t =  -radius; t <= radius; ++t)
                for (var b = -radius; b <= radius; ++b)
                    positions[n++] =  GridMath.GetPos(centerPos, t, b);

            return positions;
        }

        public static void Rhomb(Tilemap map, TileBase tileBase,
            Vector3Int centerCoord, int radius)
        {
            var points = RhombPoints(centerCoord, radius);
            Fill(map, tileBase, points);
        }

        public static void FullCircle(Tilemap map, TileBase tileBase,
            Vector3Int centerCoord, int radius)
        {
            var points = FullCirclePoints(centerCoord, radius);
            Fill(map, tileBase, points);
        }

        public static void WireCircle(Tilemap map, TileBase tileBase,
            Vector3Int centerCoord, int radius)
        {
            var points = WireCirclePoints(centerCoord, radius);
            Fill(map, tileBase, points);
        }

        public static void Ray(Tilemap map, TileBase tileBase,
            Vector3Int startCord, int length, GridMath.Direction direction)
        {
            var points = RayPoints(startCord, length, direction);
            Fill(map, tileBase, points);
        }

        public static void Fill(Tilemap map, TileBase tileBase, Vector3Int[] positions)
        {
            var tiles = new TileBase[positions.Length];

            for (var i = 0; i < tiles.Length; ++i)
                tiles[i] = tileBase;
            
            map.SetTiles(positions, tiles);
        }

        public static void Put(Tilemap map, TileBase tileBase, Vector3Int position)
        {
            map.SetTile(position, tileBase);
        }

        public static void Clear(Tilemap map, Vector3Int position)
        {
            map.SetTile(position, null);
        }

        public static void Clear(Tilemap map, Vector3Int[] positions)
        {
            map.SetTiles(positions, null);
        }

        public static TileBase GetTile(Tilemap map, Vector3Int position)
        {
            return map.GetTile(position);
        }
    }
}