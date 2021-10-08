using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridOffset
{
    public static Vector3Int BottomRight(Vector3Int pos) => new Vector3Int(pos.y % 2 == 0 ? pos.x : pos.x + 1, pos.y - 1, 0);
    public static Vector3Int Right(Vector3Int pos) =>       new Vector3Int(pos.x + 1, pos.y, 0);
    public static Vector3Int TopRight(Vector3Int pos) =>    new Vector3Int(pos.y % 2 == 0 ? pos.x : pos.x + 1, pos.y + 1, 0);
    public static Vector3Int BottomLeft(Vector3Int pos) =>  new Vector3Int(pos.y % 2 == 0 ? pos.x - 1 : pos.x, pos.y - 1, 0);
    public static Vector3Int Left(Vector3Int pos) =>        new Vector3Int(pos.x - 1, pos.y, 0);
    public static Vector3Int TopLeft(Vector3Int pos) =>     new Vector3Int(pos.y % 2 == 0 ? pos.x - 1 : pos.x, pos.y + 1, 0);
}
