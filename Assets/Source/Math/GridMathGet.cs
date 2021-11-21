using UnityEngine;

namespace Source
{
    public static partial class GridMath
    {
        private static Vector3Int BottomRight(Vector3Int pos) => new Vector3Int(pos.x + pos.y.odd(), pos.y - 1, pos.z);
        private static Vector3Int Right(Vector3Int pos)       => new Vector3Int(pos.x + 1, pos.y, pos.z);
        private static Vector3Int TopRight(Vector3Int pos)    => new Vector3Int(pos.x + pos.y.odd(), pos.y + 1, pos.z);
        private static Vector3Int BottomLeft(Vector3Int pos)  => new Vector3Int(pos.x - pos.y.even(), pos.y - 1, pos.z);
        private static Vector3Int Left(Vector3Int pos)        => new Vector3Int(pos.x - 1, pos.y, pos.z);
        private static Vector3Int TopLeft(Vector3Int pos)     => new Vector3Int(pos.x - pos.y.even(), pos.y + 1, pos.z);

        private static Vector3Int BottomRight(Vector3Int pos, int offset) => offset == 0 ? pos : offset > 0 ?
                BottomRightWithoutCheck(pos, offset) :
                TopLeftWithoutCheck(pos, -offset);
        private static Vector3Int Right(Vector3Int pos, int offset) => offset == 0 ? pos : offset > 0 ?    
                RightWithoutCheck(pos, offset) :
                LeftWithoutCheck(pos, -offset);
        private static Vector3Int TopRight(Vector3Int pos, int offset) => offset == 0 ? pos : offset > 0 ? 
                TopRightWithoutCheck(pos, offset) :
                BottomLeftWithoutCheck(pos, -offset);
        private static Vector3Int BottomLeft(Vector3Int pos, int offset) => offset == 0 ? pos : offset > 0 ?
                BottomLeftWithoutCheck(pos, offset) :
                TopRightWithoutCheck(pos, -offset);
        private static Vector3Int Left(Vector3Int pos, int offset) => offset == 0 ? pos : offset > 0 ?      
                LeftWithoutCheck(pos, offset) :
                RightWithoutCheck(pos, -offset);
        private static Vector3Int TopLeft(Vector3Int pos, int offset) => offset == 0 ? pos : offset > 0 ?   
                TopLeftWithoutCheck(pos, offset) :
                BottomRightWithoutCheck(pos, -offset);

        private static Vector3Int BottomRightWithoutCheck(Vector3Int pos, int offset) =>
            new Vector3Int(pos.x + (pos.y.odd() + offset) / 2, pos.y - offset, pos.z);

        private static Vector3Int RightWithoutCheck(Vector3Int pos, int offset) =>
            new Vector3Int(pos.x + offset, pos.y, pos.z);

        private static Vector3Int TopRightWithoutCheck(Vector3Int pos, int offset) =>
            new Vector3Int(pos.x + (pos.y.odd() + offset) / 2, pos.y + offset, pos.z);

        private static Vector3Int BottomLeftWithoutCheck(Vector3Int pos, int offset) =>
            new Vector3Int(pos.x - (pos.y.even() + offset) / 2, pos.y - offset, pos.z);

        private static Vector3Int LeftWithoutCheck(Vector3Int pos, int offset) =>
            new Vector3Int(pos.x - offset, pos.y, pos.z);

        private static Vector3Int TopLeftWithoutCheck(Vector3Int pos, int offset) =>
            new Vector3Int(pos.x - (pos.y.even() + offset) / 2, pos.y + offset, pos.z);
    }
}