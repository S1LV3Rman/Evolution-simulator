using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HexGrid : MonoBehaviour
{
    public int radius = 10;
    
    public TileBase grassTile;
    public TileBase dirtTile;
    public Tilemap groundMap;

    private void Awake()
    {
        // var positions = new Vector3Int[Sqr(radius * 2 - 1)];
        // var tiles = new TileBase[positions.Length];
        //
        // var sum = 0;
        // for (var r = 0; r < radius; ++r)
        // {
        //     var newSum = sum + (r > 0 ? 6 * r : 1);
        //     for (var i = sum; i < newSum; ++i)
        //     {
        //         positions[i] = new Vector3Int(i % size.x, i / size.y, 0);
        //         tiles[i] = i % 2 == 0 ? tileA : tileB;
        //     }
        // }

        GenerateStraightLine(Vector3Int.zero, dirtTile, radius, GridOffset.Neighbour.TopRight);
        GenerateStraightLine(Vector3Int.zero, dirtTile, radius, GridOffset.Neighbour.Right);
        GenerateStraightLine(Vector3Int.zero, dirtTile, radius, GridOffset.Neighbour.BottomRight);
        GenerateStraightLine(Vector3Int.zero, dirtTile, radius, GridOffset.Neighbour.BottomLeft);
        GenerateStraightLine(Vector3Int.zero, dirtTile, radius, GridOffset.Neighbour.Left);
        GenerateStraightLine(Vector3Int.zero, dirtTile, radius, GridOffset.Neighbour.TopLeft);
    }

    private void GenerateStraightLine(Vector3Int startCord, TileBase tile, int length, GridOffset.Neighbour direction)
    {
        var positions = new Vector3Int[length];
        var tiles = new TileBase[length];
        
        for (var i = 0; i < radius; ++i)
        {
            positions[i] = i == 0 ? startCord : GridOffset.GetNeighbourCoord(positions[i - 1], direction);
            tiles[i] = tile;
        }
        
        groundMap.SetTiles(positions, tiles);
    }

    private static int Sqr(int num)
    {
        return num > 0 ? num * num : 0;
    }
}