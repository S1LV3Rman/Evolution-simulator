using UnityEngine.Tilemaps;

namespace Source
{
    public struct Cell
    {
        public TileBase Tile;
        public CellType Type;

        public Cell(CellType type, TileBase tile)
        {
            Tile = tile;
            Type = type;
        }
    }

    public enum CellType
    {
        Empty,
        Grass,
        Life,
        Trail
    }
}