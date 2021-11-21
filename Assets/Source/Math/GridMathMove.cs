using UnityEngine;

namespace Source
{
    public static partial class GridMath
    {
        private static void MoveBottomRight(ref this Vector3Int pos)
        {
            pos.x += pos.y.odd();
            pos.y -= 1;
        }
        private static void MoveRight(ref this Vector3Int pos)
        {
            pos.x += 1;
        }
        private static void MoveTopRight(ref this Vector3Int pos)
        {
            pos.x += pos.y.odd();
            pos.y += 1;
        }
        private static void MoveBottomLeft(ref this Vector3Int pos)
        {
            pos.x -= pos.y.even();
            pos.y -= 1;
        }
        private static void MoveLeft(ref this Vector3Int pos)
        {
            pos.x -= 1;
        }
        private static void MoveTopLeft(ref this Vector3Int pos)
        {
            pos.x -= pos.y.even();
            pos.y += 1;
        }
        
        
        private static void MoveBottomRight(ref this Vector3Int pos, int offset)
        {
            if (offset == 0) return;
            if (offset > 0)
                pos.MoveBottomRightWithoutCheck(offset);
            else
                pos.MoveTopLeftWithoutCheck(-offset);
        }
        private static void MoveRight(ref this Vector3Int pos, int offset)
        {
            if (offset == 0) return;
            if (offset > 0)
                pos.MoveRightWithoutCheck(offset);
            else
                pos.MoveLeftWithoutCheck(-offset);
        }
        private static void MoveTopRight(ref this Vector3Int pos, int offset)
        {
            if (offset == 0) return;
            if (offset > 0)
                pos.MoveTopRightWithoutCheck(offset);
            else
                pos.MoveBottomLeftWithoutCheck(-offset);
        }
        private static void MoveBottomLeft(ref this Vector3Int pos, int offset)
        {
            if (offset == 0) return;
            if (offset > 0)
                pos.MoveBottomLeftWithoutCheck(offset);
            else
                pos.MoveTopRightWithoutCheck(-offset);
        }
        private static void MoveLeft(ref this Vector3Int pos, int offset)
        {
            if (offset == 0) return;
            if (offset > 0)
                pos.MoveLeftWithoutCheck(offset);
            else
                pos.MoveRightWithoutCheck(-offset);
        }
        private static void MoveTopLeft(ref this Vector3Int pos, int offset)
        {
            if (offset == 0) return;
            if (offset > 0)
                pos.MoveTopLeftWithoutCheck(offset);
            else
                pos.MoveBottomRightWithoutCheck(-offset);
        }
        

        private static void MoveBottomRightWithoutCheck(ref this Vector3Int pos, int offset)
        {
            pos.x += (pos.y.odd() + offset) / 2;
            pos.y -= offset;
        }
        private static void MoveRightWithoutCheck(ref this Vector3Int pos, int offset)
        {
            pos.x += offset;
        }
        private static void MoveTopRightWithoutCheck(ref this Vector3Int pos, int offset)
        {
            pos.x += (pos.y.odd() + offset) / 2;
            pos.y += offset;
        }
        private static void MoveBottomLeftWithoutCheck(ref this Vector3Int pos, int offset)
        {
            pos.x -= (pos.y.even() + offset) / 2;
            pos.y -= offset;
        }
        private static void MoveLeftWithoutCheck(ref this Vector3Int pos, int offset)
        {
            pos.x -= offset;
        }
        private static void MoveTopLeftWithoutCheck(ref this Vector3Int pos, int offset)
        {
            pos.x -= (pos.y.even() + offset) / 2;
            pos.y += offset;
        }
    }
}