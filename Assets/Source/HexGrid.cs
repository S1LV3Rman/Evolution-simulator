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
        
        var positions = new Vector3Int[radius];
        var tiles = new TileBase[radius];
        
        for (var i = 0; i < radius; ++i)
        {
                positions[i] = i == 0 ? Vector3Int.zero : GridOffset.BottomRight(positions[i - 1]);
                tiles[i] = dirtTile;
        }
        
        groundMap.SetTiles(positions, tiles);
        
        for (var i = 0; i < radius; ++i)
        {
            positions[i] = i == 0 ? Vector3Int.zero : GridOffset.Right(positions[i - 1]);
        }
        
        groundMap.SetTiles(positions, tiles);
        
        for (var i = 0; i < radius; ++i)
        {
            positions[i] = i == 0 ? Vector3Int.zero : GridOffset.TopRight(positions[i - 1]);
        }
        
        groundMap.SetTiles(positions, tiles);
        
        for (var i = 0; i < radius; ++i)
        {
            positions[i] = i == 0 ? Vector3Int.zero : GridOffset.Left(positions[i - 1]);
        }
        
        groundMap.SetTiles(positions, tiles);
        
        for (var i = 0; i < radius; ++i)
        {
            positions[i] = i == 0 ? Vector3Int.zero : GridOffset.BottomLeft(positions[i - 1]);
        }
        
        groundMap.SetTiles(positions, tiles);
        
        for (var i = 0; i < radius; ++i)
        {
            positions[i] = i == 0 ? Vector3Int.zero : GridOffset.TopLeft(positions[i - 1]);
        }
        
        groundMap.SetTiles(positions, tiles);
    }

    private static int Sqr(int num)
    {
        return num > 0 ? num * num : 0;
    }
}